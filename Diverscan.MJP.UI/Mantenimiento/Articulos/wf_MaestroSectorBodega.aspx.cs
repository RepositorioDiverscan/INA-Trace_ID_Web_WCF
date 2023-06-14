using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.GestorImpresiones.Utilidades;
using Diverscan.MJP.Negocio.SectorWarehouse;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Mantenimiento.Articulos
{
    public partial class wf_MaestroSectorBodega : System.Web.UI.Page
    {
        private static List<ESectorWarehouse> _sectorsWarehouseList;
        private e_Usuario UsrLogged = new e_Usuario();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {               
                FillDDBodega();
            }
        }

        private void FillDDBodega()
        {
            NConsultas nConsultas = new NConsultas();
            List<EBodega> ListBodegas = nConsultas.GETBODEGAS();
            ddBodega.DataSource = ListBodegas;
            ddBodega.DataTextField = "Nombre";
            ddBodega.DataValueField = "IdBodega";
            ddBodega.DataBind();
            ddBodega.Items.Insert(0, new ListItem("--Seleccione--", "0"));
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            int  idBodega = -1;
            try
            {
                if (ddBodega.SelectedIndex > 0)
                {
                    idBodega = Convert.ToInt32(ddBodega.SelectedValue);
                }

                    fillSectorsWarehouse(idBodega);
                Mensaje("ok", "Se han encontrado registros", "");
            }
            catch (Exception ex)
            {
                var cl = new GestorImpresiones.Utilidades.clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }
        }

        private void fillSectorsWarehouse(int idBodega)
        {
              FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
             NSectorWareHouse nSectorWareHouse = new NSectorWareHouse(fileExceptionWriter);
            _sectorsWarehouseList = nSectorWareHouse.GetSectorsWarehouse(idBodega);
            RadGridSectorsWareHouse.DataSource = _sectorsWarehouseList;
            RadGridSectorsWareHouse.DataBind();
        }

        protected void RadGridSectorsWareHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idSectorWarehouse=-1;
            try
            {
                foreach (GridDataItem item in RadGridSectorsWareHouse.SelectedItems)
                {
                    idSectorWarehouse = Convert.ToInt32(item["IdSectorWarehouse"].Text);
                }

                if (idSectorWarehouse > 0)
                {
                    setDataSector(idSectorWarehouse);
                }


            }
            catch (Exception ex)
            {
                var cl = new GestorImpresiones.Utilidades.clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }
        }

        private void setDataSector(int idSectorWarehouse)
        {
            foreach (ESectorWarehouse temp in _sectorsWarehouseList)
            {
                if (temp.IdSectorWarehouse == idSectorWarehouse)
                {
                    txtIdSector.Text = ""+temp.IdSectorWarehouse;
                    txtName.Text = temp.Name;
                    txtDecription.Text = temp.Description;
                    ddBodega.ClearSelection();
                    ddBodega.Items.FindByValue(""+temp.IdBodega).Selected = true;
                    chkActive.Checked = temp.Active;
                }
            }
        }

        private void Mensaje(string sTipo, string sMensaje, string sLLenado)
        {
            switch (sTipo)
            {
                case "error":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "error('" + sMensaje + "');", true);
                    break;
                case "info":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "notificacion('" + sMensaje + "');", true);
                    break;
                case "ok":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "ok('" + sMensaje + "');", true);
                    break;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string sector = txtIdSector.Text.ToString().Trim();
            string name = txtName.Text.ToString().Trim(); ;
            string description = txtDecription.Text.ToString().Trim(); ;            
            bool active = chkActive.Checked;
            int idWarehouse = Convert.ToInt32(ddBodega.SelectedValue);

            if (string.IsNullOrEmpty(name)) {
                Mensaje("error", "Debe ingresar un nombre!!!", "");
                return;
            }

            if (string.IsNullOrEmpty(description))
            {
                Mensaje("error", "Debe ingresar una descripción!!!", "");
                return;
            }

            if (ddBodega.SelectedIndex == 0)
            {
                Mensaje("error", "Debe selecionar una bodega!!!", "");
                return;
            }

            try
            {
                ESectorWarehouse sectorWarehouse;
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                NSectorWareHouse nSectorWareHouse = new NSectorWareHouse(fileExceptionWriter);
                string mesagge = "";
                if (String.IsNullOrEmpty(sector))
                {
                    sectorWarehouse = new ESectorWarehouse(idWarehouse,name,description,active);
                    mesagge = nSectorWareHouse.InsertSectorWarehouse(sectorWarehouse);
                }
                else
                {
                    int idSector= Convert.ToInt32(sector);
                    sectorWarehouse = new ESectorWarehouse(idSector, idWarehouse , name, description, active);
                    mesagge = nSectorWareHouse.UpDateSectorWarehouse(sectorWarehouse);
                }

                fillSectorsWarehouse(idWarehouse);
                Mensaje("info", mesagge, "");
            }
            catch (Exception ex)
            {
                var cl = new GestorImpresiones.Utilidades.clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtIdSector.Text = "";
            txtName.Text = "";
            txtDecription.Text = "";
            ddBodega.ClearSelection();
            ddBodega.Items.FindByValue("" +0).Selected = true;
            chkActive.Checked = false;
        }

        protected void RadGridSectorsWareHouse_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                int idWarehouse = Convert.ToInt32(ddBodega.SelectedValue);
                fillSectorsWarehouse(idWarehouse);
                
            }
            catch (Exception)
            {
              //  Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }
    }
}
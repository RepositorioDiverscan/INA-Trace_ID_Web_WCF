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
using clErrores = Diverscan.MJP.GestorImpresiones.Utilidades.clErrores;

namespace Diverscan.MJP.UI.Mantenimiento.Articulos
{
    public partial class wf_seleccionPasillos : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];
            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
             
                HideColumnsRadGrids();
                FillDDBodega();
            }
         
        }

        private void FillDDBodega()
        {
            NConsultas nConsultas = new NConsultas();
            ListBodegas = nConsultas.GETBODEGAS();
            ddBodega.DataSource = ListBodegas;
            ddBodega.DataTextField = "Nombre";
            ddBodega.DataValueField = "IdBodega";
            ddBodega.DataBind();
            ddBodega.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            ddBodega.Items[0].Attributes["disabled"] = "disabled";
        }

        private List<EBodega> ListBodegas
        {
            get
            {
                var data = ViewState["ListBodegasV"] as List<EBodega>;
                if (data == null)
                {
                    data = new List<EBodega>();
                    ViewState["ListBodegasV"] = data;
                }
                return data;
            }
            set
            {
                ViewState["ListBodegasV"] = value;
            }
        }

        protected void DDropDownBod_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddBodega.Items[0].Attributes["disabled"] = "disabled";
            CargarDropPasillos(Convert.ToInt32(ddBodega.SelectedValue));
           CargarDropSubSector(Convert.ToInt32(ddBodega.SelectedValue));
        }

        private void CargarDropPasillos(int idBodega)
        {
            try
            {
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                NSectorWareHouse nSectorWareHouse = new NSectorWareHouse(fileExceptionWriter);

                ListEstante = nSectorWareHouse.GetEstanteWarehouse(idBodega);
                ddEstante.DataSource = ListEstante;
                ddEstante.DataTextField = "estante";
                ddEstante.DataValueField = "estante";
                ddEstante.DataBind();  
                ddEstante.Items.Insert(0, new ListItem("--Seleccione--", "0"));
                ddEstante.Items[0].Attributes["disabled"] = "disabled";
               
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }

        }

        protected void BtnIngresar_onClick(object sender, EventArgs e)
        {
            try
            {
                int cont=0;
                int IdUbicacion, idSubsector;
                for (int i = 0; i < RadGridPasillo.Items.Count; i++)
                {
                    var item = RadGridPasillo.Items[i];
                    var checkbox = item["ClientSelectColumn1"].Controls[0] as CheckBox;
                    if (checkbox != null && checkbox.Checked)
                    {
                        IdUbicacion = Convert.ToInt32(item["idUbicacion"].Text);
                        idSubsector = Convert.ToInt32(ddSubSector.SelectedValue);
                        ListgvSectores.Add(new EUbicacionXSector(
                            item["estante"].Text,
                            item["descripcion"].Text,
                            IdUbicacion,
                            idSubsector));

                        InsertUbicacionPorSector(idSubsector, IdUbicacion);

                        ListgvPasillos.RemoveAll(x=>x.IdUbicacion == IdUbicacion);

                    }
                }
           
                RadGridSubSector.Rebind();

                RadGridPasillo.Rebind();
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }

        }

        protected void BtnRegresar_onClick(object sender, EventArgs e)
        {
            try
            {
                int IdUbicacion, idSubsector;
                for (int i = 0; i < RadGridSubSector.Items.Count; i++)
                {
                    var item = RadGridSubSector.Items[i];
                    var checkbox = item["ClientSelectColumn1"].Controls[0] as CheckBox;
                    if (checkbox != null && checkbox.Checked)
                    {
                        idSubsector = Convert.ToInt32(ddSubSector.SelectedValue);
                        IdUbicacion = Convert.ToInt32(item["idUbicacion"].Text);
                        ListgvPasillos.Add(new EUbicacionesXEstante(
                            item["estante"].Text,
                            item["descripcion"].Text,
                            IdUbicacion
                            ));

                        DeleteUbicacionPorSector(idSubsector, IdUbicacion);
                        ListgvSectores.RemoveAll(x=> x.IdUbicacion == IdUbicacion);
                      
                    }
                }
                //RadGridPasillo.DataSource = ListgvPasillos;
                //RadGridPasillo.DataBind();

                //RadGridSubSector.DataSource = ListgvSectores;
                //RadGridSubSector.DataBind();
                RadGridSubSector.Rebind();
                RadGridPasillo.Rebind();
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }

        }

        protected void RadGridPasillo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (ListgvPasillos.Count>0) {
                    RadGridPasillo.DataSource = ListgvPasillos;
                    RadGridPasillo.DataBind();
                }
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }
        }

        protected void RadGridSubSector_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (ListgvSectores.Count>0) {
                    RadGridSubSector.DataSource = ListgvSectores;
                    RadGridSubSector.DataBind();
                }
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }
        }

        private void CargarDropSubSector(int idBodega)
        {
            try
            {
                FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
                NSectorWareHouse nSectorWareHouse = new NSectorWareHouse(fileExceptionWriter);

                ListSubSector = nSectorWareHouse.GetSectorsWarehouse(idBodega);
                ddSubSector.DataSource = ListSubSector;
                ddSubSector.DataTextField = "name";
                ddSubSector.DataValueField = "idSectorWarehouse";
                ddSubSector.DataBind();
                ddSubSector.Items.Insert(0, new ListItem("--Seleccione--", "0"));
                ddSubSector.Items[0].Attributes["disabled"] = "disabled";

            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }

        }

        protected void DDropDownPas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddEstante.Items[0].Attributes["disabled"] = "disabled";
            var idBodega = Convert.ToInt32(ddBodega.SelectedValue);
            FillgvPasillo(ddEstante.SelectedValue, idBodega);
           
        }

        private void FillgvPasillo(string pasillo, int idBodega)
        {
            FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
            NSectorWareHouse nSectorWareHouse = new NSectorWareHouse(fileExceptionWriter);
            ListgvPasillos = nSectorWareHouse.GetEstanteXPasilloWarehouse(pasillo, idBodega);
            RadGridPasillo.DataSource = ListgvPasillos;
            RadGridPasillo.DataBind();
        }

        private void FillgvSector(int sector)
        {
            FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
            NSectorWareHouse nSectorWareHouse = new NSectorWareHouse(fileExceptionWriter);
            ListgvSectores = nSectorWareHouse.GetUbicacionXSectorWarehouse(sector);
            RadGridSubSector.DataSource = ListgvSectores;
            RadGridSubSector.DataBind();
        }

        protected void DDropDownSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddSubSector.Items[0].Attributes["disabled"] = "disabled";
            FillgvSector(Convert.ToInt32(ddSubSector.SelectedValue));

        }

        private void InsertUbicacionPorSector(int sector, int idUbicacion)
        {
            FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
            NSectorWareHouse nSectorWareHouse = new NSectorWareHouse(fileExceptionWriter);

            nSectorWareHouse.InsertUbicacionXSectorWarehouse(sector, idUbicacion);

        }

        private void DeleteUbicacionPorSector(int sector, int idUbicacion)
        {
            FileExceptionWriter fileExceptionWriter = new FileExceptionWriter();
            NSectorWareHouse nSectorWareHouse = new NSectorWareHouse(fileExceptionWriter);

            nSectorWareHouse.DeleteUbicacionXSectorWarehouse(sector, idUbicacion);
        }

        private void HideColumnsRadGrids()
        {
            RadGridPasillo.MasterTableView.GetColumn("idUbicacion").Display = false;
            RadGridSubSector.MasterTableView.GetColumn("idUbicacion").Display = false;
            RadGridSubSector.MasterTableView.GetColumn("idSectorBodega").Display = false;

        }

        private List<ESectorWarehouse> ListSubSector
        {
            get
            {
                var data = ViewState["ListSubSector"] as List<ESectorWarehouse>;
                if (data == null)
                {
                    data = new List<ESectorWarehouse>();
                    ViewState["ListSubSector"] = data;
                }
                return data;
            }
            set
            {
                ViewState["ListSubSector"] = value;
            }
        }

        private List<EUbicacionXSector> ListgvSectores
        {
            get
            {
                var data = ViewState["ListgvSectores"] as List<EUbicacionXSector>;
                if (data == null)
                {
                    data = new List<EUbicacionXSector>();
                    ViewState["ListgvSectores"] = data;
                }
                return data;
            }
            set
            {
                ViewState["ListgvSectores"] = value;
            }
        }

        private List<EUbicacionesXEstante> ListgvPasillos
        {
            get
            {
                var data = ViewState["ListgvPasillos"] as List<EUbicacionesXEstante>;
                if (data == null)
                {
                    data = new List<EUbicacionesXEstante>();
                    ViewState["ListgvPasillos"] = data;
                }
                return data;
            }
            set
            {
                ViewState["ListgvPasillos"] = value;
            }
        }

        private List<EEStanteWarehouse> ListEstante
        {
            get
            {
                var data = ViewState["ListPasillos"] as List<EEStanteWarehouse>;
                if (data == null)
                {
                    data = new List<EEStanteWarehouse>();
                    ViewState["ListPasillos"] = data;
                }
                return data;
            }
            set
            {
                ViewState["ListPasillos"] = value;
            }
        }

    }
}
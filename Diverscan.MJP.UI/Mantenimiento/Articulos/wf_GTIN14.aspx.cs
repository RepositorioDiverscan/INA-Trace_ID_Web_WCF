using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Compilation;
using System.Xml;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Negocio.Programa;
using Diverscan.MJP.UI.ServiceMH;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Telerik.Web.UI.Diagram;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;
using HtmlAgilityPack;
using Diverscan.MJP.Negocio.MotorDecisiones;
using Diverscan.MJP.Negocio.LogicaWMS;
using Diverscan.MJP.Utilidades.general;
using Diverscan.MJP.Negocio.MaestroArticulo;
using Diverscan.MJP.Negocio.GS1;
namespace Diverscan.MJP.UI.Mantenimiento.Articulos
{
    public partial class wf_GTIN14 : System.Web.UI.Page
    {
        e_Usuario UsrLogged = new e_Usuario();
        static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        public int ToleranciaAgregar = 110;

        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                CargarDDLS();
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

        private void Mensaje1(string sTipo, string sMensaje)
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



        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel1.Unload += new EventHandler(UpdatePanel_Unload);
        }

        void UpdatePanel_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
        }

        public void RegisterUpdatePanel(UpdatePanel panel)
        {
            foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel1 });

                }
            }
        }

        private void CargarDDLS()
        {
            try
            {
                string[] Msj = n_SmartMaintenance.CargarDDL(ddlidCompania, e_TablasBaseDatos.TblCompania(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

                Msj = n_SmartMaintenance.CargarDDL(ddlDescripcion, e_TablasBaseDatos.VistaNombresArticulos(), UsrLogged.IdUsuario, true);
                if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");

            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Error 11231231" + ex.Message, "");
            }
        }

        protected void BtnGenera_Click(object sender, EventArgs e)
        {
            n_MaestroArticulo genera = new n_MaestroArticulo();
            EntidadesGS1.e_GTIN GTIN = new EntidadesGS1.e_GTIN();
            int Pos = 1;
            int raya = ddlDescripcion.SelectedItem.Text.IndexOf('-');
            string GTIN13 = "";
            GTIN13 = ddlDescripcion.SelectedItem.Text.Substring(raya + 1);
            GTIN.ValorLeido = genera.GeneraGTIN14(GTIN13);
            txtidInterno.Text = ddlDescripcion.SelectedValue.ToString();
            // extrae cada digito del gtin 
            List<EntidadesGS1.e_Digito> Digitos = new List<EntidadesGS1.e_Digito>();
            foreach (Char CR in GTIN.ValorLeido.Reverse())  // estrictamente necesario hacerlo reverse para calcular el código verificador.
            {
                EntidadesGS1.e_Digito D = new EntidadesGS1.e_Digito();
                D.NumDigito = Pos;
                D.Valor = int.Parse(CR.ToString());
                Digitos.Add(D);
                GTIN.Digitos = Digitos;
                Pos++;
            }
            //TBLADMGTIN14VariableLogistica
            int DV = CargarEntidadesGS1.GS1128_DigitoVerificador(GTIN);
            txtConsecutivoGTIN14.Text = GTIN.ValorLeido + DV.ToString();
            ChkGenerado.Checked = true;

    
        }

        #region EventosFrontEnd

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtConsecutivoGTIN14.Text.Length > 14)
                {
                   
                    Mensaje1("error", "El GTIN ingresado tiene mas de 14 numeros, intente de nuevo ");
                }
                else if (txtConsecutivoGTIN14.Text.Length < 14)
                {
                    Mensaje1("error", "El GTIN ingresado tiene menos de 14 numeros, intente de nuevo ");
                }
                else {
                    Control Ctr = (Control)sender;
                    var Panel = Ctr.Parent.Parent.Parent;
                    string[] Msj = n_SmartMaintenance.AgregarDatos(Panel, e_TablasBaseDatos.TBLADMGTIN14VariableLogistica(), ToleranciaAgregar, UsrLogged.IdUsuario);
                    if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                    CargarDDLS();
                    LimpiarGTIN14();
                    CargarGTIN14("", true);
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
                throw;
            }
           
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                    if (txtConsecutivoGTIN14.Text.Length > 14)
                {

                    Mensaje1("error", "El GTIN editado tiene mas de 14 numeros, intente de nuevo ");
                }
                else if (txtConsecutivoGTIN14.Text.Length < 14)
                {
                    Mensaje1("error", "El GTIN editado tiene menos de 14 numeros, intente de nuevo ");
                }
                else
                {
                    Control Ctr = (Control)sender;
                    var Panel = Ctr.Parent.Parent.Parent;
                    string[] Msj = n_SmartMaintenance.EditarDatos(Panel, e_TablasBaseDatos.TBLADMGTIN14VariableLogistica(), UsrLogged.IdUsuario);
                    if (Msj[1] != "") Mensaje(Msj[0], Msj[1], "");
                    CargarDDLS();
                    LimpiarGTIN14();
                    CargarGTIN14("", true);
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
                throw;
            }
        }


        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarGTIN14();
            txtSearch.Text = "";
            CargarGTIN14("", true);
        }


        #endregion //EventosFrontEnd

        #region GTIN14

        private void CargarGTIN14(string buscar, bool pestana)
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                SQL = "EXEC SP_BuscarGTIN14 '" + idCompania + "', '" + buscar + "'";
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RadGridGTIN14.DataSource = DSDatos;
                if (pestana)
                {
                    RadGridGTIN14.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        private void LimpiarGTIN14()
        {
            txtidGTIN14VariableLogistica.Text = "";
            txtidInterno.Text = "";
            ddlidCompania.SelectedValue = "--Seleccionar--";
            txtConsecutivoGTIN14.Text = "";
            ddlDescripcion.SelectedValue = "--Seleccionar--";
            txtCantidad.Text = "";
            txtContenido.Text = "";
            txtNombre.Text = "";
            chkActivo.Checked = true;

            btnAgregar.Visible = true;
            btnEditar.Visible = false;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarGTIN14();
                CargarGTIN14(txtSearch.Text.ToString().Trim(), true);
                //txtSearch.Text = "";
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridGTIN14_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                CargarGTIN14(txtSearch.Text.ToString().Trim(), false);
                //CargarGTIN14("", false);
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        protected void RadGridGTIN14_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    txtidGTIN14VariableLogistica.Text = item["idGTIN14VariableLogistica"].Text.Replace("&nbsp;", "");
                    txtidInterno.Text = item["idInterno"].Text.Replace("&nbsp;", "");
                    ddlidCompania.SelectedValue = item["idCompania"].Text.Replace("&nbsp;", "");
                    txtConsecutivoGTIN14.Text = item["ConsecutivoGTIN14"].Text.Replace("&nbsp;", "");

                    if (ddlDescripcion.Items.FindByValue(item["Descripcion"].Text.Replace("&nbsp;", "")) != null)
                    {
                        ddlDescripcion.SelectedValue = item["Descripcion"].Text.Replace("&nbsp;", "");
                    }
                    else
                    {
                        ddlDescripcion.SelectedValue = "--Seleccionar--";
                    }
                    txtCantidad.Text = item["Cantidad"].Text.Replace("&nbsp;", "");
                    txtContenido.Text = item["Contenido"].Text.Replace("&nbsp;", "");
                    txtNombre.Text = item["Nombre"].Text.Replace("&nbsp;", "");
                    chkActivo.Checked = bool.Parse(item["Activo"].Text);

                    btnAgregar.Visible = false;
                    btnEditar.Visible = true;

                    //txtidGTIN14VariableLogistica
                    //txtidInterno
                    //ddlidCompania
                    //txtConsecutivoGTIN14
                    //ddlDescripcion
                    //txtCantidad
                    //txtContenido
                    //txtNombre
                    //chkActivo
                }
            }
            catch (Exception ex)
            {
                Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }

        #endregion GTIN14
    }
}
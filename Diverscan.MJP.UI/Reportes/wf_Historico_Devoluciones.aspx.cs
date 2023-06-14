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
using Diverscan.MJP.Negocio.Reportes;
using Diverscan.MJP.Utilidades.general;
using Diverscan.MJP.Negocio.MaestroArticulo;
using Diverscan.MJP.Negocio.GS1;

namespace Diverscan.MJP.UI.Reportes
{
    public partial class wf_Historico_Devoluciones : System.Web.UI.Page
    {
        private static DateTime fechaInicioSeleccionada;
        private static DateTime fechaFinSeleccionada;
        private static int idDestinoSeleccionado = 0; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                n_Destino_Devolucion des = new n_Destino_Devolucion();
                des.LlenarDestino(CBDestino);
                CargarGridDestino();
            }
        }

        e_Usuario UsrLogged = new e_Usuario();

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
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Panel1.Unload += new EventHandler(UpdatePanel_Unload);
        }

        void UpdatePanel_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
        }

      

        private void Mensaje1(string sTipo, string sMensaje, string sLLenado)
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

        private void CargarGridDestino()
        {
            try
            {
                n_WMS wms = new n_WMS();
                DataSet DSDatos = new DataSet();
                string SQL = "";
                string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

                SQL = "SP_CONSULTA_HISTORIAL_DEVOLUCIONES_V2" ;
                DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

                RGDevoluciones.DataSource = DSDatos;
                RGDevoluciones.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje1("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
            }
        }
        protected void btnRefrescar_Click(object sender, EventArgs e)
        {
            RDPFechaFinal.Clear();
            RDPFechaInicio.Clear();
            CBDestino.SelectedIndex = 0;
            CargarGridDestino();
            //Recargar();

        }


        private bool validarFechasRangoParaCargarGrid()
        {
            try
            {
                fechaInicioSeleccionada = RDPFechaInicio.SelectedDate.Value;
                fechaFinSeleccionada = RDPFechaFinal.SelectedDate.Value;
                return true;
            }
            catch (Exception)
            {
                Mensaje1("error", "Las fechas ingresadas tienen un formato incorrecto", "");
                return false;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validarFechasRangoParaCargarGrid())
                {

                    string valor = CBDestino.SelectedItem.ToString();
                    string separador = "-";
                    string[] textos = valor.Split(new string[] { separador }, StringSplitOptions.None);
                    string sacarID = textos[0];


                   

                    if (sacarID == "")
                    {
                        idDestinoSeleccionado = 0;
                    }
                    else
                    {
                        idDestinoSeleccionado = Convert.ToInt32(sacarID);
                    }
                    cargarGridDevoluciones();
                }
            }
            catch (Exception)
            {

                Mensaje1("error", "Verifique que todos los datos hayan sido ingresados", "");
            
            }
            
          

        }

        private void cargarGridDevoluciones()
        {
            try
            {
                RGDevoluciones.DataSource = null;
                RGDevoluciones.DataBind();
                if (validarFechasRangoParaCargarGrid())
                {
                   
                    var listaRegistros = n_BusquedaDevoluciones.ObtenerDatosDevoluciones(idDestinoSeleccionado, fechaInicioSeleccionada, fechaFinSeleccionada);
                    RGDevoluciones.DataSource = listaRegistros;
                    RGDevoluciones.DataBind();
                    //RGDevoluciones = listaRegistros;

                    if (listaRegistros.Count < 1)
                    {
                        Mensaje1("info", "No se encontraron datos para el reporte", "");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje1("error", "Algo salió mal, al obtener Datos del reporte: " + ex.ToString(), "");
            }
        }

        private bool validarFechasRangoParaNeedDataGrid()
        {
            try
            {
                fechaInicioSeleccionada = RDPFechaInicio.SelectedDate.Value;
                fechaFinSeleccionada = RDPFechaFinal.SelectedDate.Value;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected void radGridRGDDevoluciones_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (validarFechasRangoParaNeedDataGrid())
                {
                    var listaRegistros = n_BusquedaDevoluciones.ObtenerDatosDevoluciones(idDestinoSeleccionado, fechaInicioSeleccionada, fechaFinSeleccionada);
                    RGDevoluciones.DataSource = listaRegistros;
                }
            }
            catch (Exception)
            {
                Mensaje1("error", "Algo salió mal, al obtener datos de [Ajustes Inventario]", "");
            }
        }

    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.UI.ServiceMH;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.UI.ServiceMH;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Diverscan.MJP.Negocio.LogicaWMS;


namespace Diverscan.MJP.UI.Consultas.Administracion
{
    public partial class wf_Consultar_Inventario : System.Web.UI.Page
    {
        //e_Usuario UsrLogged = new e_Usuario();
        //static string StrConexion = ConfigurationManager.ConnectionStrings["MJPConnectionString"].Name;
        //public int ToleranciaAgregar = 110;

        protected void Page_Load(object sender, EventArgs e)
        {
            //UsrLogged = (e_Usuario)Session["USUARIO"];

            //if (UsrLogged == null)
            //{
            //    Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            //}
            //if (!IsPostBack)
            //{

            //    inicializacionElementos();
            //}
        }
        private static DateTime fechaInicioSeleccionada;
        private static DateTime fechaFinSeleccionada;
        //private void Mensaje(string sTipo, string sMensaje, string sLLenado)
        //{
        //    switch (sTipo)
        //    {
        //        case "error":
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "error('" + sMensaje + "');", true);
        //            break;
        //        case "info":
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "notificacion('" + sMensaje + "');", true);
        //            break;
        //        case "ok":
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "ok('" + sMensaje + "');", true);
        //            break;
        //    }
        //}

        //protected override void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);
        //    this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
        //}

        //void UpdatePanel1_Unload(object sender, EventArgs e)
        //{
        //    this.RegisterUpdatePanel(sender as UpdatePanel);
        //}

        //public void RegisterUpdatePanel(UpdatePanel panel)
        //{
        //    foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
        //    {
        //        if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
        //        {
        //            methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel1 });
        //        }
        //    }
        }


        //private bool validarFechasRangoParaCargarGrid()
        //{
        //    try
        //    {
        //        fechaInicioSeleccionada = RDPFechaInicial.SelectedDate.Value;
        //        fechaFinSeleccionada = RDPFechaFinal.SelectedDate.Value;
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        Mensaje("error", "Las fechas ingresadas tienen un formato incorrecto", "");
        //        return false;
        //    }
        //}

        //protected void _btnBuscar_Click(object sender, EventArgs e)
        //{

        //    if (validarFechasRangoParaCargarGrid())
        //    {

        //        try
        //        {
        //            n_WMS wms = new n_WMS();
        //            DataSet DSDatos = new DataSet();
        //            string SQL = "";
        //            string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);

        //            DateTime DTfechaInicio = RDPFechaInicial.SelectedDate ?? DateTime.Now;
        //            DateTime DTfechaFin = RDPFechaFinal.SelectedDate ?? DateTime.Now;

        //            string fechaInicio = DTfechaInicio.ToString("yyyyMMdd") + " 00:00:00";
        //            string fechaFin = DTfechaFin.ToString("yyyyMMdd") + " 23:59:59";

        //            SQL = "EXEC SP_Consulta_MaestroInventario '" + fechaInicio + "','" + fechaFin + "'";
        //            DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

        //            RadGridMaestroInventario.DataSource = DSDatos;

        //            RadGridMaestroInventario.DataBind();

        //        }
        //        catch (Exception ex)
        //        {
        //            Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
        //        }
        //    }
        //}
        //private void inicializacionElementos()
        //{
        //    try
        //    {
        //        RDPFechaFinal.SelectedDate = DateTime.Now;
        //        RDPFechaInicial.SelectedDate = DateTime.Now;
        //    }
        //    catch (Exception) { }
        //}


        //protected void RadGridMaestroInvetario_ItemCommand(object source, GridCommandEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CommandName == "RowClick")
        //        {
        //            GridDataItem item = (GridDataItem)e.Item;

        //            string idMaestroInventario = item["IdInventarioBasico"].Text.Replace("&nbsp;", "");

        //            CargarDetalleInventario(idMaestroInventario);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
        //    }
        //}

        //private void CargarDetalleInventario(string buscar)
        //{
        //    try
        //    {
        //        n_WMS wms = new n_WMS();
        //        DataSet DSDatos = new DataSet();
        //        string SQL = "";
        //        string idCompania = wms.getIdCompania(UsrLogged.IdUsuario);


        //        SQL = "EXEC SP_Consulta_DetalleInventario " + Convert.ToInt32(buscar) + "";
        //        DSDatos = n_ConsultaDummy.GetDataSet(SQL, UsrLogged.IdUsuario);

        //        RadGridDetalleInvetario.DataSource = DSDatos;

        //        RadGridDetalleInvetario.DataBind();

        //    }
        //    catch (Exception ex)
        //    {
        //        Mensaje("error", "Ops! Ha ocurrido un Error." + ex.Message, "");
        //    }
        //}





    
}
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.Reportes.Kardex;
using Diverscan.MJP.Negocio.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI
{
    public partial class wf_KardexSkuDetalleTra : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    var _eUsuario = (e_Usuario)Session["USUARIO"];

        //    if (_eUsuario == null)
        //    {
        //        Response.Redirect("~/Administracion/wf_Credenciales.aspx");
        //    }
        //    else
        //    {
        //        IdBodega = _eUsuario.IdBodega;
        //    }

        //    if (!IsPostBack)
        //    {
        //        FillDDBodega();
        //        inicializacionElementos();
        //        inicializarVariables();
        //    }
        //}

        //private int IdBodega
        //{
        //    get
        //    {
        //        var idBodega = -1;
        //        var data = ViewState["IdBodega "];
        //        if (data != null)
        //        {
        //            var result = int.TryParse(data.ToString(), out idBodega);
        //            if (result)
        //                ViewState["IdBodega "] = idBodega;
        //        }
        //        return idBodega;
        //    }
        //    set
        //    {
        //        ViewState["IdBodega "] = value;
        //    }
        //}

        //private List<e_kardexSKU> listaKardex
        //{
        //    get
        //    {
        //        var listaKardex = ViewState["listaKardex"] as List<e_kardexSKU>;
        //        if (listaKardex == null)
        //        {
        //            listaKardex = new List<e_kardexSKU>();
        //            ViewState["listaKarex"] = listaKardex;
        //        }
        //        return listaKardex;
        //    }

        //    set
        //    {
        //        ViewState["listaKardex"] = value;
        //    }
        //}

        //#region "Objetos Requeridos y variables"
        //private n_kardexSKU _kardexSKU = new n_kardexSKU();
        //private static bool seCargaronDatos = false;
        //#endregion

        //#region "Variables de Funcionamiento"
        //private static int _idBodega;
        //private static string _SKU;
        //private static string _Lote;
        //private static bool _Transito;
        //private static DateTime _fechaInicioSeleccionada;
        //private static DateTime _fechaFinSeleccionada;      

        //#endregion

        //#region "WebForm"


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
        //}

        //protected override void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);
        //    this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
        //}

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

        ////Visualización de elementos
        //protected void chkVerTrazabilidad_CheckedChanged(object sender, EventArgs e)
        //{
            
        //}

        //protected void chkVerAjustesInventario_CheckedChanged(object sender, EventArgs e)
        //{
            
        //}

        //protected void chkVerDespachos_CheckedChanged(object sender, EventArgs e)
        //{
            
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

        //private bool validarFechasRangoParaCargarGrid()
        //{
        //    try
        //    {
        //        _fechaInicioSeleccionada = RDPFechaInicial.SelectedDate.Value;
        //        _fechaFinSeleccionada = RDPFechaFinal.SelectedDate.Value;
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        Mensaje("error", "Las fechas ingresadas tienen un formato incorrecto", "");
        //        return false;
        //    }
        //}
        //private void inicializarVariables()
        //{
        //    _fechaInicioSeleccionada = DateTime.Now;
        //    _fechaFinSeleccionada = DateTime.Now;
        //}

        //private bool validarFechasRangoParaNeedDataGrid()
        //{
        //    try
        //    {
        //        _fechaInicioSeleccionada = RDPFechaInicial.SelectedDate.Value;
        //        _fechaFinSeleccionada = RDPFechaFinal.SelectedDate.Value;
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}


        //#endregion

        //protected void ddlIDBodega_SelectedIndexChanged(object sender, EventArgs e) { 

        //}

        //protected void _btnBuscar_Click(object sender, EventArgs e)
        //{
        //    if (!validarFechasRangoParaNeedDataGrid())
        //    {
        //        Mensaje("error", "Las fechas ingresadas no son correctas", "");
        //        return;
        //    }

        //    SetVariables();
        //    CargarGridKardex();
        //}

        //protected void ChkTransito_CheckedChanged(object sender, EventArgs e)
        //{

        //}

        //protected void radGridKardexSKU_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    try
        //    {
        //        radGriddGridKardexSKU.DataSource = listaKardex;
        //    }
        //    catch (Exception)
        //    {

        //    }
            
        //}

        //private void FillDDBodega()
        //{
        //    NConsultas nConsultas = new NConsultas();
        //    List<EBodega> ListBodegas = nConsultas.GETBODEGAS();
        //    ddlIDBodega.DataSource = ListBodegas;
        //    ddlIDBodega.DataTextField = "Nombre";
        //    ddlIDBodega.DataValueField = "IdBodega";
        //    ddlIDBodega.DataBind();
        //    ddlIDBodega.Items.Insert(0, new ListItem("--Seleccione--", "0"));
        //}

        //private void CargarGridKardex()
        //{
        //    listaKardex = _kardexSKU.ObtenerListaKardexSKU(_idBodega, _SKU, _Lote, _Transito, _fechaInicioSeleccionada, _fechaFinSeleccionada);
        //    radGriddGridKardexSKU.DataSource = listaKardex;
        //    radGriddGridKardexSKU.DataBind();
        //}

        //private void SetVariables()
        //{
        //    _idBodega = Convert.ToInt32(ddlIDBodega.SelectedValue);
        //    _SKU = txtSKU.Text;
        //    _Lote = txtLote.Text;
        //    _Transito = ChkTransito.Checked;
        //    _fechaInicioSeleccionada = RDPFechaInicial.SelectedDate.Value;
        //    _fechaFinSeleccionada = RDPFechaFinal.SelectedDate.Value;
        //}
    }
}
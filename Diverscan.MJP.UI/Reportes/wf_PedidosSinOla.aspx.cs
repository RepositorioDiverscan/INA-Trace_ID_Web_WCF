using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.AccesoDatos.Reportes.ReportePedidoSinOla;
using Diverscan.MJP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Reportes
{
    //public partial class wf_PedidosSinOla : System.Web.UI.Page
    //{
    //    #region Global Variables
    //    private readonly IReportes reportes;
    //    private e_Usuario UsrLogged = new e_Usuario();
    //    private int _idBodega
    //    {
    //        get
    //        {
    //            var data = ViewState["idBodega"];
    //            if (data == null)
    //            {
    //                data = ((e_Usuario)Session["USUARIO"]).IdBodega;
    //                ViewState["idBodega"] = data;
    //            }
    //            return Int32.Parse(data.ToString());
    //        }
    //        set { ViewState["idBodega"] = value; }
    //    }
    //    private List<EListPedidoSinOla> _listaPreMaestro
    //    {
    //        get
    //        {
    //            var data = ViewState["PreMaestros"] as List<EListPedidoSinOla>;
    //            if (data == null)
    //            {
    //                data = new List<EListPedidoSinOla>();
    //                ViewState["PreMaestros"] = data;
    //            }
    //            return data;
    //        }
    //        set { ViewState["PreMaestros"] = value; }
    //    }
    //    public wf_PedidosSinOla()
    //    {
    //        reportes = new NReporte();
    //    }

    //    #endregion
    //    protected void Page_Load(object sender, EventArgs e)
    //    {
    //        UsrLogged = (e_Usuario)Session["USUARIO"];

    //        if (UsrLogged == null)
    //        {
    //            Response.Redirect("~/Administracion/wf_Credenciales.aspx");
    //        }
    //        if (!IsPostBack)
    //        {
    //            FillControls();
    //        }
    //    }

    //    private void FillControls()
    //    {
    //        GetListaBodegas();
    //        SetDatetime();
    //    }

    //    protected override void OnInit(EventArgs e)
    //    {
    //        base.OnInit(e);
    //        this.Panel1.Unload += new EventHandler(UpdatePanel1_Unload);
    //    }

    //    void UpdatePanel1_Unload(object sender, EventArgs e)
    //    {
    //        this.RegisterUpdatePanel(sender as UpdatePanel);
    //    }

    //    protected void btnBusqueda_Click(object sender, EventArgs e)
    //    {
    //        if (ValidarEspacios())
    //        {
    //            DateTime fechaInicial = Convert.ToDateTime(RDPFechaInicial.SelectedDate);
    //            DateTime fechaFinal = Convert.ToDateTime(RDPFechaFinal.SelectedDate);
    //            _idBodega = Convert.ToInt32(ddlBodegas.SelectedValue.ToString());
    //            EPedidosSinOla pedidosSinOla = new EPedidosSinOla()
    //            {
    //                IdBodega = _idBodega
    //                ,
    //                FechaInicio = fechaInicial
    //                ,
    //                FechaFinal = fechaFinal
    //                ,
    //                Ruta = "Todas"
    //            };
    //            ObtenerListadoPedidoSinOla(pedidosSinOla);
    //        }
           
    //    }

    //    private void GetListaBodegas()
    //    {
    //        List<EListBodega> listBodegas = reportes.ListaBodegas();
    //        ddlBodegas.DataSource = listBodegas;
    //        ddlBodegas.DataTextField = "nombre";
    //        ddlBodegas.DataValueField = "idBodega";
    //        ddlBodegas.DataBind();
    //        ddlBodegas.Items.Insert(0, new ListItem("--Seleccione--", null));

    //    }

    //    private void ObtenerListadoPedidoSinOla(EPedidosSinOla ePedidos)
    //    {
    //        try
    //        {
    //            _listaPreMaestro = reportes.PedidosSinOlas(ePedidos);
    //            RGPreMaestro.DataSource = _listaPreMaestro;
    //            RGPreMaestro.DataBind();
    //        }
    //        catch (Exception ex)
    //        {

    //            Mensaje("error", "Ocurrio el error" + ex.Message, "");
    //        }
    //    }

    //    private void Mensaje(string sTipo, string sMensaje, string sLLenado)
    //    {
    //        switch (sTipo)
    //        {
    //            case "error":
    //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "error('" + sMensaje + "');", true);
    //                break;
    //            case "info":
    //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "notificacion('" + sMensaje + "');", true);
    //                break;
    //            case "ok":
    //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "ok('" + sMensaje + "');", true);
    //                break;
    //        }
    //    }

    //    public void RegisterUpdatePanel(UpdatePanel panel)
    //    {
    //        foreach (MethodInfo methodInfo in typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
    //        {
    //            if (methodInfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
    //            {
    //                methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { UpdatePanel1 });
    //            }
    //        }
    //    }

    //    protected void RGPreMaestro_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    //    {
    //        RGPreMaestro.DataSource = _listaPreMaestro;
    //    }

    //    private void SetDatetime()
    //    {
    //        DateTime datetime = DateTime.Now;

    //        if (datetime.DayOfWeek == DayOfWeek.Monday || datetime.DayOfWeek == DayOfWeek.Tuesday || datetime.DayOfWeek == DayOfWeek.Wednesday || datetime.DayOfWeek == DayOfWeek.Thursday || datetime.DayOfWeek == DayOfWeek.Friday)
    //        {
    //            RDPFechaFinal.SelectedDate = datetime.AddDays(1).Date;
    //        }
    //        else if (datetime.DayOfWeek == DayOfWeek.Saturday)
    //        {
    //            RDPFechaFinal.SelectedDate = datetime.AddDays(2).Date;
    //        }
    //        else
    //        {
    //            RDPFechaFinal.SelectedDate = datetime.AddDays(1).Date;
    //        }

    //        RDPFechaInicial.SelectedDate = datetime;
    //        RDPFechaFinal.SelectedDate = datetime;

    //    }

    //    private bool ValidarEspacios()
    //    {
    //        if (ddlBodegas.SelectedIndex < 1)
    //        {
    //            Mensaje("info", "Debe seleccionar una bodega.", "");
    //            return false;
    //        }
    //        return true;
    //    }
    //}
}
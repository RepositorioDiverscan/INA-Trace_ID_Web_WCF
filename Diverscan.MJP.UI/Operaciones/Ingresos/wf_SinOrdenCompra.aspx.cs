using Diverscan.MJP.AccesoDatos.Operaciones;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;



namespace Diverscan.MJP.UI.Operaciones.Ingresos
{
    public partial class wf_SinOrdenCompra : System.Web.UI.Page
    {
        #region Variables Globales

        e_Usuario UsrLogged = new e_Usuario();
        public static DataTable DTDetalleOC = new DataTable();
        static int IdMaestroSinOrden;
        static bool EstadoProc;
        static int GlobalidMaestroSinOrdenCompra;

        List<eSinOrdenCompra> listcompra;


        private List<eSinOrdenCompra> _SOCEncabezado
        {
            get
            {
                var data = ViewState["SOCEncabezado"] as List<eSinOrdenCompra>;
                if (data == null)
                {
                    data = new List<eSinOrdenCompra>();
                    ViewState["SOCEncabezado"] = data;
                }
                return data;
            }
            set
            {
                ViewState["SOCEncabezado"] = value;
            }
        }

        private List<EDetalleOrdenC> _SOCDetalle
        {
            get
            {
                var data = ViewState["SOCDetalle"] as List<EDetalleOrdenC>;
                if (data == null)
                {
                    data = new List<EDetalleOrdenC>();
                    ViewState["SOCDetalle"] = data;
                }
                return data;
            }
            set
            {
                ViewState["SOCDetalle"] = value;
            }
        }

        #endregion



        #region EVENTOS

        protected void Page_Load(object sender, EventArgs e)
        {
            UsrLogged = (e_Usuario)Session["USUARIO"];

            if (UsrLogged == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
            if (!IsPostBack)
            {
                RadGridOPEINGSinOrdenDeCompraARecibir.Culture = System.Globalization.CultureInfo.GetCultureInfoByIetfLanguageTag("");
                RadGridDetalleIngreso.MasterTableView.GetColumn("IdArticulo").Display = false;
                SetDatetime();
            }

        }

        protected void RadGridArticulosDisponiblesBodega_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGridOPEINGSinOrdenDeCompraARecibir.DataSource = _SOCEncabezado;
        }

        protected void RadGridDetalleSinOrdenCompra_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGridDetalleIngreso.DataSource = _SOCDetalle;
        }

        protected void RadGridDetalleIngreso_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {

                int IdArtilo = 0;

                foreach (GridDataItem item in RadGridDetalleIngreso.SelectedItems)
                {

                    IdArtilo = Convert.ToInt32(item["IdArticulo"].Text);

                }
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();


            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            try
            {
                DateTime? fechaInicioBusqueda = null;
                DateTime? fechaFinBusqueda = null;

                string numTransaccion = txtNumeroTransaccion.Text;

                int idBodega = UsrLogged.IdBodega;

                if (txtFechaInicioBusqueda.SelectedDate == null && txtFechaFinBusqueda.SelectedDate == null && numTransaccion.Equals(""))
                {
                    Mensaje("error", "¡Verifique los campos!", "");
                }
                else if (txtFechaInicioBusqueda.SelectedDate != null && txtFechaFinBusqueda.SelectedDate != null)
                {

                    fechaInicioBusqueda = txtFechaInicioBusqueda.SelectedDate.Value;
                    fechaFinBusqueda = txtFechaFinBusqueda.SelectedDate.Value;

                    if ((fechaInicioBusqueda > fechaFinBusqueda))
                    {
                        Mensaje("error", "La fecha de fin de búsqueda debe ser mayor a la de inicio de búsqueda !", "");
                    }
                    else
                    {
                        buscarDatos(fechaInicioBusqueda, fechaFinBusqueda, numTransaccion, idBodega);
                    }
                }
                else
                {
                    buscarDatos(fechaInicioBusqueda, fechaFinBusqueda, numTransaccion, idBodega);
                }

                if (_SOCDetalle != null)
                {
                    _SOCDetalle.Clear();
                    RadGridDetalleIngreso.DataSource = _SOCDetalle;
                    RadGridDetalleIngreso.DataBind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.SelectCommandName)
            {
                string sSelectedRowValue = "Primary key for the clicked item from ItemCommand: " + (e.Item as GridDataItem).GetDataKeyValue("idInterno").ToString();
            }
        }


        protected void RadGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int idBodega = UsrLogged.IdBodega;
            if (idBodega > 0)
            {
                foreach (GridDataItem item in RadGridOPEINGSinOrdenDeCompraARecibir.SelectedItems)
                {
                    int str = Convert.ToInt32(item["IdMaestroSinOrdenCompra"].Text);
                    IdMaestroSinOrden = Convert.ToInt32(item["IdMaestroSinOrdenCompra"].Text);
                    GlobalidMaestroSinOrdenCompra = str;
                    List<EDetalleOrdenC> listDetalleSinOrdenCompra;
                    NegocioOperaciones negocioOperaciones = new NegocioOperaciones();
                    listDetalleSinOrdenCompra = negocioOperaciones.ObtenerDetalleOrdenCompras(str, idBodega);
                    _SOCDetalle = listDetalleSinOrdenCompra;
                    RadGridDetalleIngreso.DataSource = listDetalleSinOrdenCompra;
                    RadGridDetalleIngreso.DataBind();
                }
            }

        }




        #endregion



        #region METODOS

        private void SetDatetime()
        {
            DateTime datetime = DateTime.Now;

            if (datetime.DayOfWeek == DayOfWeek.Monday || datetime.DayOfWeek == DayOfWeek.Tuesday || datetime.DayOfWeek == DayOfWeek.Wednesday || datetime.DayOfWeek == DayOfWeek.Thursday || datetime.DayOfWeek == DayOfWeek.Friday)
            {
                txtFechaFinBusqueda.SelectedDate = datetime.AddDays(1).Date;
            }
            else if (datetime.DayOfWeek == DayOfWeek.Saturday)
            {
                txtFechaFinBusqueda.SelectedDate = datetime.AddDays(2).Date;
            }
            else
            {
                txtFechaFinBusqueda.SelectedDate = datetime.AddDays(1).Date;
            }

            txtFechaInicioBusqueda.SelectedDate = datetime;
            txtFechaFinBusqueda.SelectedDate = datetime;
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


        private void buscarDatos(DateTime? fechaInicio, DateTime? fechaFin, string ordenCompra, int idBodega)
        {
            try
            {
                
                NegocioOperaciones negocioOperaciones = new NegocioOperaciones();
                listcompra = negocioOperaciones.ObtenerSinOrdenCompraBodega(fechaInicio, fechaFin, ordenCompra, idBodega);
                _SOCEncabezado = listcompra;
                RadGridOPEINGSinOrdenDeCompraARecibir.DataSource = listcompra;
                RadGridOPEINGSinOrdenDeCompraARecibir.DataBind();
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
            }

        }


        #endregion
    }
}
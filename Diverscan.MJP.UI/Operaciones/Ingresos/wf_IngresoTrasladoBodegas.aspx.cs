using Diverscan.MJP.AccesoDatos.Operaciones;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;

namespace Diverscan.MJP.UI.Operaciones.Ingresos
{
    public partial class wf_IngresoTrasladoBodegas : System.Web.UI.Page
    {
        #region VARIABLES GLOBALES

        e_Usuario UsrLogged = new e_Usuario();
        public static DataTable DTDetalleOC = new DataTable();
        static int IdMaestroTrasladoBodega;
        static bool EstadoProc;
        static int GlobalidMaestroTrasladoBodega;

        List<EIngresoTrasladoBodega> listcompra;

        private List<EIngresoTrasladoBodega> _ITBEncabezado
        {
            get
            {
                var data = ViewState["ITBEncabezado"] as List<EIngresoTrasladoBodega>;
                if (data == null)
                {
                    data = new List<EIngresoTrasladoBodega>();
                    ViewState["ITBEncabezado"] = data;
                }
                return data;
            }
            set
            {
                ViewState["ITBEncabezado"] = value;
            }
        }

        private List<EDetalleOrdenC> _ITBDetalle
        {
            get
            {
                var data = ViewState["ITBDetalle"] as List<EDetalleOrdenC>;
                if (data == null)
                {
                    data = new List<EDetalleOrdenC>();
                    ViewState["ITBDetalle"] = data;
                }
                return data;
            }
            set
            {
                ViewState["ITBDetalle"] = value;
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
                RadGridOPEINGTrasladoBodega.Culture = System.Globalization.CultureInfo.GetCultureInfoByIetfLanguageTag("");
                RadGridDetalleIngreso.MasterTableView.GetColumn("IdArticulo").Display = false;
                SetDatetime();
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

                if (_ITBDetalle != null)
                {
                    _ITBDetalle.Clear();
                    RadGridDetalleIngreso.DataSource = _ITBDetalle;
                    RadGridDetalleIngreso.DataBind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected void RadGridArticulosDisponiblesBodega_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGridOPEINGTrasladoBodega.DataSource = _ITBEncabezado;
        }

        protected void RadGridDetalleTrasladoBodega_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGridDetalleIngreso.DataSource = _ITBDetalle;
        }

        protected void RadGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int idBodega = UsrLogged.IdBodega;
            if (idBodega > 0)
            {
                foreach (GridDataItem item in RadGridOPEINGTrasladoBodega.SelectedItems)
                {
                    int str = Convert.ToInt32(item["IdMaestroIngresoTraslado"].Text);
                    IdMaestroTrasladoBodega = Convert.ToInt32(item["IdMaestroIngresoTraslado"].Text);
                    GlobalidMaestroTrasladoBodega = str;
                    List<EDetalleOrdenC> listDetalleIngresoTaslado;
                    NegocioOperaciones negocioOperaciones = new NegocioOperaciones();
                    listDetalleIngresoTaslado = negocioOperaciones.ObtenerDetalleIngresoTraslado(str, idBodega);
                    _ITBDetalle = listDetalleIngresoTaslado;
                    RadGridDetalleIngreso.DataSource = listDetalleIngresoTaslado;
                    RadGridDetalleIngreso.DataBind();
                }
            }

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

        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.SelectCommandName)
            {
                string sSelectedRowValue = "Primary key for the clicked item from ItemCommand: " + (e.Item as GridDataItem).GetDataKeyValue("idInterno").ToString();
            }
        }



        #endregion


        #region MÉTODOS

        private void buscarDatos(DateTime? fechaInicio, DateTime? fechaFin, string ordenCompra, int idBodega)
        {
            try
            {

                NegocioOperaciones negocioOperaciones = new NegocioOperaciones();
                listcompra = negocioOperaciones.ObtenerIngresoTrasladoBodega(fechaInicio, fechaFin, ordenCompra, idBodega);
                _ITBEncabezado = listcompra;
                RadGridOPEINGTrasladoBodega.DataSource = listcompra;
                RadGridOPEINGTrasladoBodega.DataBind();
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
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

        #endregion


    }
}
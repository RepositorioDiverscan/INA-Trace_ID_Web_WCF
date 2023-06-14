using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.Reportes.Despachos;
using Diverscan.MJP.Negocio.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Reportes.RptDespachosPorPedido
{
    public partial class RptDespachosPorPedidoVisorJS : System.Web.UI.Page
    {
        #region "Objetos Requeridos"
        private n_Despachos _Despachos = new n_Despachos();
        public DatosGeneralesRptDespachoPorSolicitud datosGeneralesRpt = new DatosGeneralesRptDespachoPorSolicitud();
        #endregion

        #region "Carga de datos Reporte"
        private void cargarReporte(long numSolicitud)
        {
            var listaNumSolicitudDespachoDetalle = _Despachos.ObtenerDetalleDespachoPorNumeroSolicitudReporte(numSolicitud);
            GVReporte.DataSource = GenerarDatosReporte(listaNumSolicitudDespachoDetalle);
            GVReporte.DataBind();
        }
        private DataTable GenerarDatosReporte(List<e_Detalle_Despacho_Por_Numero_Solicitud_Reporte> listaDatos)
        {
            DataTable dt = new DataTable();
            //Agregar columnas al gridview
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("SSCC");
            dt.Columns.Add("Bultos");
            dt.Columns.Add("Pedido");
            dt.Columns.Add("Alistado");
            dt.Columns.Add("UnidadMedida");
            dt.Columns.Add("Costo");
            dt.Columns.Add("Total");

            //Carga columnas con los datos por filas

            foreach (var item in listaDatos)
            {
                DataRow dru = dt.NewRow();
                dru["Codigo"] = item.Codigo;
                dru["Descripcion"] = item.Descripcion;
                dru["SSCC"] = item.SSCCEtiqueta;
                dru["Bultos"] = item.BultosUnidadMedidaConcatenado;
                dru["Pedido"] = item.PedidoUI;
                dru["Alistado"] = item.AlistadoUI;
                dru["UnidadMedida"] = item.UnidadMedidaUI;
                dru["Costo"] = item.Costo;
                dru["Total"] = item.Total;
                dt.Rows.Add(dru);//Agregar fila al DataTable
            }

            //Se carga unicamente una vez los datos generales del reporte
            if (listaDatos.Count > 0)
            {
                datosGeneralesRpt.ClienteDestino = listaDatos[0].NombreDestino;
                datosGeneralesRpt.IdInternoSolicitudSAP = listaDatos[0].IdInternoSolicitud;
                //datosGeneralesRpt.FechaSolicitud = String.Format("{0:d/M/yyyy HH:mm:ss}", listaDatos[0].FechaSolicitud);
                datosGeneralesRpt.FechaSolicitud = listaDatos[0].FechaPedido.ToString();
                datosGeneralesRpt.CantidadLineas = listaDatos[0].CantidadLineas.ToString();
                datosGeneralesRpt.TotalCostos = listaDatos[0].TotalCosto.ToString();
            }
            else
            {
                datosGeneralesRpt = new DatosGeneralesRptDespachoPorSolicitud();
            }



            return dt;
        }
        #endregion

        #region "Web Form"
        protected void Page_Load(object sender, EventArgs e)
        {
            var _eUsuario = (e_Usuario)Session["USUARIO"];
            if (_eUsuario == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }

            long numSolicitud;
            try
            {
                if (Session["NumSolicitudSeleccionada"] == null)
                {
                    numSolicitud = -1;
                    cargarReporte(numSolicitud);
                }
                else
                {
                    numSolicitud = (long)Convert.ToDouble(Session["NumSolicitudSeleccionada"].ToString());
                    cargarReporte(numSolicitud);
                }
            }
            catch (Exception)
            {
                cargarReporte(-1);
                //throw;
            }
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Session["NumSolicitudSeleccionada"] = null;
            Response.Redirect("wf_DespachosPorPedido.aspx");
        }
        #endregion
    }
    //Clase para datos generales de reporte |Título, Costo Total, Num. Líneas ...|
    public class DatosGeneralesRptDespachoPorSolicitud
    {
        //public string FechaImpresion { get; private set; }
        public readonly string FechaImpresion;
        public readonly string TituloPrincipal;
        public string ClienteDestino;
        public string IdInternoSolicitudSAP;
        public string FechaSolicitud;
        public string CantidadLineas;
        public string TotalCostos;

        public DatosGeneralesRptDespachoPorSolicitud()
        {
            FechaImpresion = DateTime.Now.ToString();
            TituloPrincipal = "Comidas Centroamericanas S.A.";
            ClienteDestino = "";
            IdInternoSolicitudSAP = "";
            FechaSolicitud = "";
            CantidadLineas = "";
            TotalCostos = "";
        }
    }
}
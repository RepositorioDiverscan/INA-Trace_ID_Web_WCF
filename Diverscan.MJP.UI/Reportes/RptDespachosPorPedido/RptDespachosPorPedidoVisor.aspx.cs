using CrystalDecisions.CrystalReports.Engine;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Reportes;
using System;

namespace Diverscan.MJP.UI.Reportes.RptDespachosPorPedido
{
    public partial class RptDespachosPorPedido : System.Web.UI.Page
    {
        private n_Despachos _Despachos = new n_Despachos();
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
                numSolicitud = (long)Convert.ToDouble(Session["NumSolicitudSeleccionada"].ToString());
                cargarReporte(numSolicitud);
            }
            catch (Exception)
            {
                cargarReporte(-1);
                //throw;
            }

        }

        private void cargarReporte(long numSolicitud)
        {
            ReportDocument rptDoc = new ReportDocument();
            DSDespachosPorPedido ds = new DSDespachosPorPedido();
            var listaNumSolicitudDespachoDetalle = _Despachos.ObtenerDetalleDespachoPorNumeroSolicitudReporte(numSolicitud);

            rptDoc.Load(Server.MapPath("CRDespachosPorPedido.rpt"));
            rptDoc.SetDataSource(listaNumSolicitudDespachoDetalle);
            CRDespachoPorSolicitud.ReportSource = rptDoc;
            rptDoc.Close();
            rptDoc.Dispose();
            GC.Collect();
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("wf_DespachosPorPedido.aspx");
        }
    }
}
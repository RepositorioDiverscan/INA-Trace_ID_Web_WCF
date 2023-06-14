using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades.MensajesRespuesta;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Reportes
{
    public class ReporteDespachoN
    {
        private readonly ReporteDespacho _ReporteDespacho;

        public ReporteDespachoN(ReporteDespacho ReporteDespacho)
        {
            if (ReporteDespacho == null)
            {
                throw new ArgumentNullException("Obtener reporte", "Obtener reporte is null");
            }
            _ReporteDespacho = ReporteDespacho;
        }

        public List<IEntidad_Despacho> Obtener_Reporte_Despacho(DateTime FechaInicio, DateTime FechaFinal, int idArticulo, ref Respuestas mensaje)
        {
            List<IEntidad_Despacho> incidentes = new List<IEntidad_Despacho>();
            try
            {
                incidentes = this._ReporteDespacho.Obtener_Reporte_Despacho(FechaInicio, FechaFinal, idArticulo);
                mensaje.Estado = true;
                mensaje.Respuesta = "La consulta fue realizada con exito.";
            }
            catch (Exception ex)
            {
                mensaje.Estado = false;
                mensaje.Respuesta = "Problemas en la busqueda de incidencias.";
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
            }
            return incidentes;
        }
    }

}


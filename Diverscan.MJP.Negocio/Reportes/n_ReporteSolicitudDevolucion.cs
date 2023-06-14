using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Reportes
{
    public class n_ReporteSolicitudDevolucion
    {
        public List<e_ReporteSolicitudDevolucion> ReporteSolicitudDevolucion(DateTime fechaInicio, DateTime fechafin, int idBodega)
        {

            try
            {
                da_ReporteSolicitudDevolucion da_Reporte = new da_ReporteSolicitudDevolucion();
                return da_Reporte.ReporteSolicitudDevolucion(fechaInicio, fechafin, idBodega);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<e_bodega> CargarBodegas()
        {
            try
            {
                //Llamamos a la capa de acceso a datos
                da_ReporteSolicitudDevolucion da_Reporte = new da_ReporteSolicitudDevolucion();
                return da_Reporte.CargarBodegas();

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

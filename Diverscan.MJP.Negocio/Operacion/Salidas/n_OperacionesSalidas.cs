using Diverscan.MJP.AccesoDatos.Operacion.Salidas;
using Diverscan.MJP.Entidades.Operacion.Salidas;
using System;
using System.Collections.Generic;

namespace Diverscan.MJP.Negocio.Operacion.Salidas
{
    public class n_OperacionesSalidas
    {
        da_OperacionSalidasDBA da_OperacionSalidasDBA = new da_OperacionSalidasDBA();

        public List<e_Destino_Solicitud_SSCC_Asociado> Obtener_Destinos_Solicitud_Rango_Fecha_Con_SSCCAsociado(string parametroBusqueda, DateTime fechaInicio, DateTime fechaFin)
        {
            return da_OperacionSalidasDBA.Obtener_Destinos_Solicitud_Rango_Fecha_Con_SSCCAsociado(parametroBusqueda, fechaInicio, fechaFin);            
        }
        public List<e_SSCC_Zona_Transito_Por_Destino_Solicitud> Obtener_SSCC_Zona_Transito_Por_Destino_Solicitud(long numSolicitudTID)
        {
            return da_OperacionSalidasDBA.Obtener_SSCC_Zona_Transito_Por_Destino_Solicitud(numSolicitudTID);
        }
        public List<e_Articulos_SSCC_Procesado> Obtener_Articulos_SSCC_Procesado(long idConsecutivoSSCC)
        {
            return da_OperacionSalidasDBA.Obtener_Articulos_SSCC_Procesado(idConsecutivoSSCC);
        }
    }
}

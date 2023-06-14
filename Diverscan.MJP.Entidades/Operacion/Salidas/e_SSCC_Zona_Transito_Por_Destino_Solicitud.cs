using System;

namespace Diverscan.MJP.Entidades.Operacion.Salidas
{
    [Serializable]
    public class e_SSCC_Zona_Transito_Por_Destino_Solicitud
    {
        private long _idConsecutivoSSCC;
        private string _SSCCGenerado;
        private long _idMaestroSolicitud;

        public e_SSCC_Zona_Transito_Por_Destino_Solicitud() { }

        public e_SSCC_Zona_Transito_Por_Destino_Solicitud(long idConsecutivoSSCC,  string SSCCGenerado, long idMaestroSolicitud )
        {
            _idConsecutivoSSCC = idConsecutivoSSCC;
            _SSCCGenerado = SSCCGenerado;
            _idMaestroSolicitud = idMaestroSolicitud;
        }

        public long IdConsecutivoSSCC { get { return _idConsecutivoSSCC; } set { _idConsecutivoSSCC = value; } }
        public string SSCCGenerado { get { return _SSCCGenerado; } set { _SSCCGenerado = value; } }
        public long IdMaestroSolicitud { get { return _idMaestroSolicitud; } set { _idMaestroSolicitud = value; } }
    }
}

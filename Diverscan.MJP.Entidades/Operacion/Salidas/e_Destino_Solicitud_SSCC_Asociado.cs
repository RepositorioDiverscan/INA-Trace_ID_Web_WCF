using System;

namespace Diverscan.MJP.Entidades.Operacion.Salidas
{
    [Serializable]
    public class e_Destino_Solicitud_SSCC_Asociado
    {
        private long _idMaestroSolicitudTID;
        private string _idSolicitudSAP;
        private DateTime _fechaCreacion;
        private string _nombreDestino;

        public e_Destino_Solicitud_SSCC_Asociado() { }

        public e_Destino_Solicitud_SSCC_Asociado(long idMaestroSolicitudTID, string idSolicitudSAP, DateTime fechaCreacion, string nombreDestino )
        {
            _idMaestroSolicitudTID = idMaestroSolicitudTID;
            _idSolicitudSAP = idSolicitudSAP;
            _fechaCreacion = fechaCreacion;
            _nombreDestino = nombreDestino;
        }

        public long IdMaestroSolicitudTID { get { return _idMaestroSolicitudTID; } set { _idMaestroSolicitudTID = value; } }
        public string IdSolicitudSAP { get { return _idSolicitudSAP; } set { _idSolicitudSAP = value; } }
        public DateTime FechaCreacion { get { return _fechaCreacion; } set { _fechaCreacion = value; } }
        public string NombreDestino { get { return _nombreDestino; } set { _nombreDestino = value; } }
         
    }
}

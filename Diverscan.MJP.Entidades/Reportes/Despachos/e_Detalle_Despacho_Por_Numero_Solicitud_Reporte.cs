using Diverscan.MJP.Entidades.Reportes.Kardex;
using System;

namespace Diverscan.MJP.Entidades.Reportes.Despachos
{
    [Serializable]
    public class e_Detalle_Despacho_Por_Numero_Solicitud_Reporte : e_Detalle_Despacho_Por_Numero_Solicitud
    {
        private string _fechaSolicitud { get; set; }
        private string _nombreDestino { get; set; }
        private string _idSolicitudTID { get; set; }
        private string _idInternoSolicitud { get; set; }

        public e_Detalle_Despacho_Por_Numero_Solicitud_Reporte() { }

        //Carga de datos del reporte
        public e_Detalle_Despacho_Por_Numero_Solicitud_Reporte(
            string codigo, long sscc, string ssccEtiqueta, string lote,
            string descripcion, int bultos, string bultoUnidadMedida, string ubicacion, decimal pedidoUI,
            decimal alistadoUI, string unidadMedidaUI, decimal alistadoUF, string empaqueUF,
            string und, decimal costo, decimal total, DateTime fechaPedido, string placa,
            string marcaVehiculo, string modelo,
            //Encabezado del reporte
            string fechaSolicitud, string nombreDestino, string idSolicitudTID, string idInternoSolicitud) : base(codigo, sscc, ssccEtiqueta, lote, descripcion, bultos, bultoUnidadMedida, ubicacion, pedidoUI, alistadoUI, unidadMedidaUI, alistadoUF, empaqueUF, und, costo, total, fechaPedido, placa, marcaVehiculo, modelo)
        {
            _fechaSolicitud = fechaSolicitud;
            _nombreDestino = nombreDestino;
            _idSolicitudTID = idSolicitudTID;
            _idInternoSolicitud = idInternoSolicitud;
        }

        public string FechaSolicitud
        {
            get { return _fechaSolicitud; }
            set { _fechaSolicitud = value; }
        }
        public string NombreDestino
        {
            get { return _nombreDestino; }
            set { _nombreDestino = value; }
        }
        public string IdSolicitudTID
        {
            get { return _idSolicitudTID; }
            set { _idSolicitudTID = value; }
        }
        public string IdInternoSolicitud
        {
            get { return _idInternoSolicitud; }
            set { _idInternoSolicitud = value; }
        }

        //Totales del reporte
        public decimal TotalCosto { get; set; }       
        public decimal CantidadBultos { get; set; }
        public int CantidadLineas { get; set; }

    }
}

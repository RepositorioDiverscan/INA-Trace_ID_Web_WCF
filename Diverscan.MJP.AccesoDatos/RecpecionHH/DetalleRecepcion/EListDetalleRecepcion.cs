using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Diverscan.MJP.AccesoDatos.RecpecionHH.DetalleRecepcion
{
    [DataContract]
    public class EListDetalleRecepcion
    {
        string _lote;
        DateTime _fechaVencimiento;
        decimal _cantidad;
        string _Ubicacion;
        string _estado;
        string _motivoRechazo;
        string _descripcionRechazo;

        public EListDetalleRecepcion(string lote, DateTime fechaVencimiento, decimal cantidad, string ubicacion, string estado, string motivoRechazo, string descripcionRechazo)
        {
            _lote = lote;
            _fechaVencimiento = fechaVencimiento;
            _cantidad = cantidad;
            _Ubicacion = ubicacion;
            _estado = estado;
            _motivoRechazo = motivoRechazo;
            _descripcionRechazo = descripcionRechazo;
        }

        [DataMember]
        public string Lote { get => _lote; set => _lote = value; }
        [DataMember]
        public DateTime FechaVencimiento { get => _fechaVencimiento; set => _fechaVencimiento = value; }
        [DataMember]
        public decimal Cantidad { get => _cantidad; set => _cantidad = value; }
        [DataMember]
        public string Ubicacion { get => _Ubicacion; set => _Ubicacion = value; }
        [DataMember]
        public string Estado { get => _estado; set => _estado = value; }
        [DataMember]
        public string MotivoRechazo { get => _motivoRechazo; set => _motivoRechazo = value; }
        [DataMember]
        public string DescripcionRechazo { get => _descripcionRechazo; set => _descripcionRechazo = value; }
    }
}

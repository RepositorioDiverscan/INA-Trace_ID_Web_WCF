using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Devolutions
{
    [Serializable]
    [DataContract]
    public class EDetalleRecibirSolicitudDevolucion
    {
        private int _idArticulo, _cantidad, _idDetalleSolicitudDevolucion;
        private string _lote/*, _placa, _nombreArticulo, _marca*/, _fechaVencimiento;

        [DataMember]
        public int _IdArticulo { get => _idArticulo; set => _idArticulo = value; }
        [DataMember]
        public int _Cantidad { get => _cantidad; set => _cantidad = value; }
        //[DataMember]
        //public string _Placa { get => _placa; set => _placa = value; }
        [DataMember]
        public string _Lote { get => _lote; set => _lote = value; }
        [DataMember]
        public string _FechaVencimiento { get => _fechaVencimiento; set => _fechaVencimiento = value; }
        //[DataMember]
        //public string _NombreArticulo { get => _nombreArticulo; set => _nombreArticulo = value; }
        //[DataMember]
        //public string _Marca { get => _marca; set => _marca = value; }
        [DataMember]
        public int _IdDetalleSolicitudDevolucion { get => _idDetalleSolicitudDevolucion; set => _idDetalleSolicitudDevolucion = value; }
    }
}

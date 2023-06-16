using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Diverscan.MJP.AccesoDatos.RecpecionHH.ProductoRecibido
{
    [DataContract]
    public class EProductoRecibido
    {
        int _idUsuario;
        long _idDetalleOrdenCompra;
        long _idArticulo;
        decimal _cantidad;
        string _lote;
        string _fechaVencimiento;
        short _tipoIngreso;
        string _ubicacion;

        [DataMember]
        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        [DataMember]
        public long IdDetalleOrdenCompra { get => _idDetalleOrdenCompra; set => _idDetalleOrdenCompra = value; }
        [DataMember]
        public long IdArticulo { get => _idArticulo; set => _idArticulo = value; }
        [DataMember]
        public decimal Cantidad { get => _cantidad; set => _cantidad = value; }
        [DataMember]
        public string Lote { get => _lote; set => _lote = value; }
        [DataMember]
        public string FechaVencimiento { get => _fechaVencimiento; set => _fechaVencimiento = value; }
        [DataMember]
        public string Ubicacion { get => _ubicacion; set => _ubicacion = value; }
        [DataMember]
        public short TipoIngreso { get => _tipoIngreso; set => _tipoIngreso = value; }
    }
}

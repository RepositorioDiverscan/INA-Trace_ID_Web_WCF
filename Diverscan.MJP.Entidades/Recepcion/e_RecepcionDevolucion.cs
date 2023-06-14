using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.Recepcion
{
    [DataContract]
    public class e_RecepcionDevolucion
    {
        private long _IdArticulo;
        private string _Lote;
        private DateTime _FechaVencimiento;
        private int _IdUbicacion;
        private int _Cantidad;
        private long _IdSecuancia;
        private int _IdEstado;
        private int _IdUsuario;
        private int _IdMetodoAccion;
        private long _IdDetallePedidoOriginal;

        public e_RecepcionDevolucion(long idArticulo, string lote, string fechaVencimiento, int idUbicacion, int cantidad,
            long idSecuancia, int idEstado, int idUsuario, int idMetodoAccion, long idDetallePedidoOriginal)
        {
            this._IdArticulo = idArticulo;
            this._Lote = lote;
            this._FechaVencimiento = DateTime.Parse(fechaVencimiento);
            this._IdUbicacion = idUbicacion;
            this._Cantidad = cantidad;
            this._IdSecuancia = idSecuancia;
            this._IdEstado = idEstado;
            this._IdUsuario = idUsuario;
            this._IdMetodoAccion = idMetodoAccion;
            this._IdDetallePedidoOriginal = idDetallePedidoOriginal;
        }
        [DataMember]
        public long IdArticulo { get => _IdArticulo; set => _IdArticulo = value; }
        [DataMember]
        public string Lote { get => _Lote; set => _Lote = value; }
        [DataMember]
        public string FechaVencimiento { get => _FechaVencimiento.ToString(); set => _FechaVencimiento = DateTime.Parse(value); }
        [DataMember]
        public int IdUbicacion { get => _IdUbicacion; set => _IdUbicacion = value; }
        [DataMember]
        public int Cantidad { get => _Cantidad; set => _Cantidad = value; }
        [DataMember]
        public long IdSecuancia { get => _IdSecuancia; set => _IdSecuancia = value; }
        [DataMember]
        public int IdEstado { get => _IdEstado; set => _IdEstado = value; }
        [DataMember]
        public int IdUsuario { get => _IdUsuario; set => _IdUsuario = value; }
        [DataMember]
        public int IdMetodoAccion { get => _IdMetodoAccion; set => _IdMetodoAccion = value; }
        [DataMember]
        public long IdDetallePedidoOriginal { get => _IdDetallePedidoOriginal; set => _IdDetallePedidoOriginal = value; }
    }
}

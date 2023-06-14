using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.Pedidos
{
    [Serializable]
    [DataContract]
    public class e_PedidoRecibido
    {
        private int _IdPedido;
        private DateTime _FechaTerminado;
        private int _UsuarioEntrega;
        private List<e_DetalleRecibido> _DetalleRecibidos;

        public e_PedidoRecibido(string idPedido, string fechaTerminado, string usuarioEntrega)
        {
            this._IdPedido = Convert.ToInt32(idPedido);
            this._FechaTerminado = DateTime.Parse(fechaTerminado);
            this._UsuarioEntrega = Convert.ToInt32(usuarioEntrega);
        }
        [DataMember]
        public int IdPedido { get => _IdPedido; set => _IdPedido = value; }
        [DataMember]
        public DateTime FechaTerminado { get => _FechaTerminado; set => _FechaTerminado = value; }
        [DataMember]
        public int UsuarioEntrega { get => _UsuarioEntrega; set => _UsuarioEntrega = value; }
        [DataMember]
        public List<e_DetalleRecibido> DetallesRecibidos { get => _DetalleRecibidos; set => _DetalleRecibidos = value; }
    }
}

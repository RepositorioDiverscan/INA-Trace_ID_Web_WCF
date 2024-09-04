using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Pedidos
{
    [Serializable]
    public class e_PedidoSincronizador
    {
        private int _IdPedidoOriginal;
        private string _Destino;
        private DateTime _FechaCreacion;
        private DateTime _FechaEntrega;
        private bool _Tipo;
        private string _NombreProfesor;
        private int _IdTransportista;
        private string _NombreTransportista;

        public e_PedidoSincronizador(int idPedidoOriginal, string destino, DateTime fechaCreacion, DateTime fechaEntrega, bool tipo,
            string nombreProfesor, int idTransportista, string nombreTransportista)
        {
            _IdPedidoOriginal = idPedidoOriginal;
            _Destino = destino;
            _FechaCreacion = fechaCreacion;
            _FechaEntrega = fechaEntrega;
            _Tipo = tipo;
            _NombreProfesor = nombreProfesor;
            _IdTransportista = idTransportista;
            _NombreTransportista = nombreTransportista;
        }

        public int IdPedidoOriginal { get => _IdPedidoOriginal; set => _IdPedidoOriginal = value; }
        public string Destino { get => _Destino; set => _Destino = value; }
        public DateTime FechaCreacion { get => _FechaCreacion; set => _FechaCreacion = value; }
        public DateTime FechaEntrega { get => _FechaEntrega; set => _FechaEntrega = value; }
        public bool Tipo { get => _Tipo; set => _Tipo = value; }
        public string NombreProfesor { get => _NombreProfesor; set => _NombreProfesor = value; }
        public int IdTransportista { get => _IdTransportista; set => _IdTransportista = value; }
        public string NombreTransportista { get => _NombreTransportista; set => _NombreTransportista = value; }
    }
}

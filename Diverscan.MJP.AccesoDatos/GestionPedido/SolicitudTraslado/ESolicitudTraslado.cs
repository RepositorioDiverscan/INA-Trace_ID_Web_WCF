using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.GestionPedido.SolicitudTraslado
{
    public class ESolicitudTraslado
    {
        //Atributos
        private string _numeroTransaccion;
        private int _idUsuarioSolicita;
        private int _idBodegaOrigen;
        private int _idBodegaDestino;
        private int _idPedidoOriginal;
        private int _idArticulo;
        private int _cantidadSolicitada;


        public ESolicitudTraslado(string numeroTransaccion, int idUsuarioSolicita, int idBodegaOrigen, int idBodegaDestino, int idPedidoOriginal, int idArticulo, int cantidadSolicitada)
        {
            NumeroTransaccion = numeroTransaccion;
            _idUsuarioSolicita = idUsuarioSolicita;
            _idBodegaOrigen = idBodegaOrigen;
            _idBodegaDestino = idBodegaDestino;
            _idPedidoOriginal = idPedidoOriginal;
            _idArticulo = idArticulo;
            _cantidadSolicitada = cantidadSolicitada;
        }

        public string NumeroTransaccion { get => _numeroTransaccion; set => _numeroTransaccion = value; }
        public int IdUsuarioSolicita { get => _idUsuarioSolicita; set => _idUsuarioSolicita = value; }
        public int IdBodegaOrigen { get => _idBodegaOrigen; set => _idBodegaOrigen = value; }
        public int IdBodegaDestino { get => _idBodegaDestino; set => _idBodegaDestino = value; }
        public int IdPedidoOriginal { get => _idPedidoOriginal; set => _idPedidoOriginal = value; }
        public int IdArticulo { get => _idArticulo; set => _idArticulo = value; }
        public int CantidadSolicitada { get => _cantidadSolicitada; set => _cantidadSolicitada = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.GestionPedido.SolicitudAlistos
{
    public class ESolicitudAlisto
    {
        private int _idUsuario;
        private int _idPedidoOriginal;
        private int _idArticulo;
        private int _idBodega;
        private int _cantidadAlisto;

        public ESolicitudAlisto(int idUsuario, int idPedidoOriginal, int idArticulo, int idBodega, int cantidadAlisto)
        {
            _idUsuario = idUsuario;
            _idPedidoOriginal = idPedidoOriginal;
            _idArticulo = idArticulo;
            _idBodega = idBodega;
            _cantidadAlisto = cantidadAlisto;
        }

        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public int IdPedidoOriginal { get => _idPedidoOriginal; set => _idPedidoOriginal = value; }
        public int IdArticulo { get => _idArticulo; set => _idArticulo = value; }
        public int IdBodega { get => _idBodega; set => _idBodega = value; }
        public int CantidadAlisto { get => _cantidadAlisto; set => _cantidadAlisto = value; }
    }
}

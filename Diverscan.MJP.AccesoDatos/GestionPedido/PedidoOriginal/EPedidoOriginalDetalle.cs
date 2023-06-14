using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.GestionPedido.PedidoOriginal
{
    public class EPedidoOriginalDetalle
    {
        int _idArticulo;
        string _idArticuloInterno;
        string _nombre;
        float _cantidadSolicitada;
        float _cantidadResolver;
        float _cantidadDisponible;
        int _idPedidoOriginal;

        public EPedidoOriginalDetalle(int idArticulo, string idArticuloInterno, string nombre, float cantidadSolicitada, float cantidadDisponible, int idPedidoOriginal, float cantidadResolver)
        {
            _idArticulo = idArticulo;
            _idArticuloInterno = idArticuloInterno;
            _nombre = nombre;
            _cantidadSolicitada = cantidadSolicitada;
            _cantidadDisponible = cantidadDisponible;
            _idPedidoOriginal = idPedidoOriginal;
            _cantidadResolver = cantidadResolver;
        }

        public int IdArticulo { get => _idArticulo; set => _idArticulo = value; }
        public string IdArticuloInterno { get => _idArticuloInterno; set => _idArticuloInterno = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public float CantidadSolicitada { get => _cantidadSolicitada; set => _cantidadSolicitada = value; }
        public float CantidadDisponible { get => _cantidadDisponible; set => _cantidadDisponible = value; }
        public int IdPedidoOriginal { get => _idPedidoOriginal; set => _idPedidoOriginal = value; }
        public float CantidadResolver { get => _cantidadResolver; set => _cantidadResolver = value; }
    }
}

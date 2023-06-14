using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Pedidos
{
    [Serializable]
    public class e_DetallePedidoSincronizador
    {
        private int _IdDetallePedidoOriginal;
        private int _IdPedidoOriginal;
        private int _IdArticulo;
        private string _IdArticuloInterno;
        private int _CantidadOriginal;
        private int _CantidadAlistada;
        private string _ArticuloNombre;
        private string _GTIN;
        private bool _ConTrazabilidad;

        public e_DetallePedidoSincronizador(int idDetallePedidoOriginal, int idPedidoOriginal, int idArticulo, string idArticuloInterno, 
            int cantidadOriginal, int cantidadAlistada, string articuloNombre, string GTIN, bool conTrazabilidad)
        {
            _IdDetallePedidoOriginal = idDetallePedidoOriginal;
            _IdPedidoOriginal = idPedidoOriginal;
            _IdArticulo = idArticulo;
            _IdArticuloInterno = idArticuloInterno;
            _CantidadOriginal = cantidadOriginal;
            _CantidadAlistada = cantidadAlistada;
            _ArticuloNombre = articuloNombre;
            _GTIN = GTIN;
            _ConTrazabilidad = conTrazabilidad;
        }

        public int IdDetallePedidoOriginal { get => _IdDetallePedidoOriginal; set => _IdDetallePedidoOriginal = value; }
        public int IdPedidoOriginal { get => _IdPedidoOriginal; set => _IdPedidoOriginal = value; }
        public int IdArticulo { get => _IdArticulo; set => _IdArticulo = value; }
        public string IdArticuloInterno { get => _IdArticuloInterno; set => _IdArticuloInterno = value; }
        public int CantidadOriginal { get => _CantidadOriginal; set => _CantidadOriginal = value; }
        public int CantidadAlistada { get => _CantidadAlistada; set => _CantidadAlistada = value; }
        public string ArticuloNombre { get => _ArticuloNombre; set => _ArticuloNombre = value; }
        public string GTIN { get => _GTIN; set => _GTIN = value; }
        public bool ConTrazabilidad { get => _ConTrazabilidad; set => _ConTrazabilidad = value; }
    }
}

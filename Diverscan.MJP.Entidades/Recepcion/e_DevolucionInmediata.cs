using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Recepcion
{
    [Serializable]
    public class e_DevolucionInmediata
    {
        private double _IdDetallePedidoOriginal;
        private double _IdPedidoOriginal;
        private double _IdArticulo;
        private int _CantidadDevolucion;
        private string _NombreArticulo;

        public e_DevolucionInmediata(double idDetallePedidoOriginal, double idPedidoOriginal, double idArticulo, int cantidadDevolucion, string nombreArticulo)
        {
            _IdDetallePedidoOriginal = idDetallePedidoOriginal;
            _IdPedidoOriginal = idPedidoOriginal;
            _IdArticulo = idArticulo;
            _CantidadDevolucion = cantidadDevolucion;
            _NombreArticulo = nombreArticulo;
        }

        public double IdDetallePedidoOriginal { get => _IdDetallePedidoOriginal; set => _IdDetallePedidoOriginal = value; }
        public double IdPedidoOriginal { get => _IdPedidoOriginal; set => _IdPedidoOriginal = value; }
        public double IdArticulo { get => _IdArticulo; set => _IdArticulo = value; }
        public int CantidadDevolucion { get => _CantidadDevolucion; set => _CantidadDevolucion = value; }
        public string NombreArticulo { get => _NombreArticulo; set => _NombreArticulo = value; }

    }
}

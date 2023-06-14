using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Pedidos
{
    [Serializable]
    public class e_DetalleRecibido
    {
        private int _idDetallePedidoOriginal;
        private int _CantidadRecibida;
        private int _CausaDevolucion;

        public e_DetalleRecibido(int idDetallePedidoOriginal, int cantidadRecibida, int causaDevolucion)
        {
            this._idDetallePedidoOriginal = idDetallePedidoOriginal;
            this._CantidadRecibida = cantidadRecibida;
            this._CausaDevolucion = causaDevolucion;
        }

        public int IdDetallePedidoOriginal { get => _idDetallePedidoOriginal; set => _idDetallePedidoOriginal = value; }
        public int CantidadRecibida { get => _CantidadRecibida; set => _CantidadRecibida = value; }

        public int CausaDevolucion { get => _CausaDevolucion; set => _CausaDevolucion = value; }
    }
}

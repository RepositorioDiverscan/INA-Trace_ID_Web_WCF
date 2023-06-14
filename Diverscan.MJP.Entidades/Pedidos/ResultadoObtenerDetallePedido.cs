using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Pedidos
{
    public class ResultadoObtenerDetallePedido : ResultWS
    {
        private List<e_DetallePedidoSincronizador> _DetallesPedidoSincronizador = new List<e_DetallePedidoSincronizador>();
    
        public List<e_DetallePedidoSincronizador> detallesPedidosSincronizador { get => _DetallesPedidoSincronizador; set => _DetallesPedidoSincronizador = value; }
    }
}

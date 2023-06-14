using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Pedidos
{
    public class ResultadoObtenerPedidos : ResultWS
    {
        private List<e_PedidoSincronizador> _PedidosSincronizador = new List<e_PedidoSincronizador>();

        public List<e_PedidoSincronizador> pedidosSincronizador { get => _PedidosSincronizador; set => _PedidosSincronizador = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.Utilidades;

namespace Diverscan.MJP.Entidades.Pedidos
{
    public class ResultadoIngresarPedidosRecibidos : ResultWS
    {
        private List<string> _ListaPedidosRecibidos = new List<string>();

        public List<string> listaPedidosRecibidos { get => _ListaPedidosRecibidos; set => _ListaPedidosRecibidos = value; }
    }
}

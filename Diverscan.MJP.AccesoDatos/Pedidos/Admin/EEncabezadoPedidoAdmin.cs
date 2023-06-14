using System;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.Pedidos.Admin
{
    public class EEncabezadoPedidoAdmin
    {
        //Atributos
        private int _pedido;
        private string _solicitud;
        private string _bodega;
        private string _estado;
        private DateTime _creacion;
        private DateTime _entrega;
        private DateTime _registro;
        private string _nombreUsuario;
        private string _tipoPedido;

        //Constructor
        public EEncabezadoPedidoAdmin(IDataReader reader)
        {
            _pedido = Convert.ToInt32(reader["Pedido"]);
            _solicitud = Convert.ToString(reader["Solicitud"]);
            _bodega = Convert.ToString(reader["Destino"]);
            _estado = Convert.ToString(reader["Estado"]);
            _creacion = Convert.ToDateTime(reader["Creacion"]);
            _entrega = Convert.ToDateTime(reader["Entrega"]);
            _registro = Convert.ToDateTime(reader["Registro"]);
            _nombreUsuario = Convert.ToString(reader["Nombre"]);
            _tipoPedido = Convert.ToString(reader["Tipo"]);
        }

        //Métodos getters y setters
        public int Pedido { get => _pedido; set => _pedido = value; }
        public string Solicitud { get => _solicitud; set => _solicitud = value; }
        public string Bodega { get => _bodega; set => _bodega = value; }
        public string Estado { get => _estado; set => _estado = value; }
        public DateTime Creacion { get => _creacion; set => _creacion = value; }
        public DateTime Entrega { get => _entrega; set => _entrega = value; }
        public DateTime Registro { get => _registro; set => _registro = value; }
        public string NombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
        public string TipoPedido { get => _tipoPedido; set => _tipoPedido = value; }
    }
}

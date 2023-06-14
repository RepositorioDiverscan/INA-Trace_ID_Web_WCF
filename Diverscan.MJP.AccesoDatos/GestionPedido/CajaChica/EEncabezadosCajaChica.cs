using System;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.GestionPedido.CajaChica
{
    public class EEncabezadosCajaChica
    {
        private int _idCaja;
        private int _pedido;
        private string _nombreUsuario;
        private string _estado;
        private DateTime _registro;
        private DateTime _cierre;

        public EEncabezadosCajaChica(IDataReader reader)
        {
            _idCaja = Convert.ToInt32(reader["IDCaja"]);
            _pedido = Convert.ToInt32(reader["Pedido"]);
            _nombreUsuario = Convert.ToString(reader["Usuario"]);
            _estado = Convert.ToString(reader["Estado"]);
            _registro = Convert.ToDateTime(reader["Registro"]);
            _cierre = Convert.ToDateTime(reader["Cierre"]);
        }

        public int IdCaja { get => _idCaja; set => _idCaja = value; }
        public int Pedido { get => _pedido; set => _pedido = value; }
        public string NombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
        public string Estado { get => _estado; set => _estado = value; }
        public DateTime Registro { get => _registro; set => _registro = value; }
        public DateTime Cierre { get => _cierre; set => _cierre = value; }
    }
}

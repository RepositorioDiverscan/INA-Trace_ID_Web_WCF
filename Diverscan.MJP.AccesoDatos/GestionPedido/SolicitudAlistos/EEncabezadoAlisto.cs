using System;
using System.Data;


namespace Diverscan.MJP.AccesoDatos.GestionPedido.SolicitudAlistos
{
    public class EEncabezadoAlisto
    {
        private int _idMaestroSolicitud;
        private DateTime _fechaRegistro;
        private DateTime _fechaCreacion;
        private int _idPedidoOriginal;
        private string _estado;

        public EEncabezadoAlisto(IDataReader reader)
        {
            _idMaestroSolicitud = Convert.ToInt32(reader["ID"]);
            _fechaRegistro = Convert.ToDateTime(reader["Registro"]);
            _fechaCreacion = Convert.ToDateTime(reader["Creacion"]);
            _idPedidoOriginal = Convert.ToInt32(reader["Pedido"]);
            _estado = Convert.ToString(reader["Estado"]);
        }

        public int IdMaestroSolicitud { get => _idMaestroSolicitud; set => _idMaestroSolicitud = value; }
        public DateTime FechaRegistro { get => _fechaRegistro; set => _fechaRegistro = value; }
        public DateTime FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public int IdPedidoOriginal { get => _idPedidoOriginal; set => _idPedidoOriginal = value; }
        public string Estado { get => _estado; set => _estado = value; }
    }
}

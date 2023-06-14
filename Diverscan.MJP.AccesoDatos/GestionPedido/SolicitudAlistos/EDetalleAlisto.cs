using System;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.GestionPedido.SolicitudAlistos
{
    public class EDetalleAlisto
    {
        private int _idDetalle;
        private string _nombre;
        private int _cantidad;

        public EDetalleAlisto(IDataReader reader)
        {
            _idDetalle = Convert.ToInt32(reader["Detalle"]);
            _nombre = Convert.ToString(reader["Articulo"]);
            _cantidad = Convert.ToInt32(reader["Cantidad"]);
        }

        public int IdDetalle { get => _idDetalle; set => _idDetalle = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
    }
}

using System;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.GestionPedido.CajaChica
{
    public class EDetalleCajaChica
    {
        private int _detalle;
        private string _nombreArticulo;
        private int _cantidad;
        private DateTime _fecha;

        public EDetalleCajaChica(IDataReader reader)
        {
            _detalle = Convert.ToInt32(reader["Detalle"]);
            _nombreArticulo = Convert.ToString(reader["Articulo"]);
            _cantidad = Convert.ToInt32(reader["Cantidad"]);
            _fecha = Convert.ToDateTime(reader["Fecha"]);
        }

        public int Detalle { get => _detalle; set => _detalle = value; }
        public string NombreArticulo { get => _nombreArticulo; set => _nombreArticulo = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
        public DateTime Fecha { get => _fecha; set => _fecha = value; }
    }
}

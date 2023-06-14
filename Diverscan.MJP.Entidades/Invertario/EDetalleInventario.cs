using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Invertario
{
   public  class EDetalleInventario
    {
        private int _idArticulo;
        private string _nombreArticulo;
        private int _cantidad;
        private string _descripcion;
        private string _fechaHoraRegistro;
         public EDetalleInventario(IDataReader reader)
        {
            _idArticulo = Convert.ToInt32(reader["IdArticulo"]);
            _nombreArticulo = Convert.ToString(reader["NombreArticulo"]);
            _cantidad = Convert.ToInt32(reader["Cantidad"]);
            _descripcion = Convert.ToString(reader["Descripcion"]);
            _fechaHoraRegistro = Convert.ToString(reader["FechaHoraRegistro"]);
        }
        public int IdArticulo { get => _idArticulo; set => _idArticulo = value; }
        public string NombreArticulo { get => _nombreArticulo; set => _nombreArticulo = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public string FechaHoraRegistro { get => _fechaHoraRegistro; set => _fechaHoraRegistro = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Despacho
{
    [Serializable]
    public class EOlaDespacho
    {
        private long _idInterno;
        private string _nombreOla;
        private int _cantidadSolicitada;
        private int _cantidadAlistada;
        private int _cantidadDisponible;

        public EOlaDespacho()
        {
        }

        public EOlaDespacho(IDataReader reader)
        {
            _idInterno = long.Parse(reader["idInterno"].ToString());

            _nombreOla = reader["Nombre"].ToString();
            _cantidadSolicitada = Convert.ToInt32(reader["CantidadSolicitada"].ToString());
            _cantidadAlistada = Convert.ToInt32(reader["cantidadAlistada"].ToString());
            _cantidadDisponible = Convert.ToInt32(reader["cantidadDisponibleBodega"].ToString());
        }

        public long IdInterno { get => _idInterno; set => _idInterno = value; }
        public string NombreOla { get => _nombreOla; set => _nombreOla = value; }
        public int CantidadSolicitada { get => _cantidadSolicitada; set => _cantidadSolicitada = value; }
        public int CantidadAlistada { get => _cantidadAlistada; set => _cantidadAlistada = value; }
        public int CantidadDisponible { get => _cantidadDisponible; set => _cantidadDisponible = value; }
    }
}

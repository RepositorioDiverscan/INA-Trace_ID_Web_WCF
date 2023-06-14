using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Invoiced.ExtenciasBodegaGeneral
{
   public  class EExistenciasBodega
    {
        private long _cantidadBodega;
        private long _idArticulo;
        private string _idInterno;
        private string _nombre;

        public EExistenciasBodega()
        {
        }

        public EExistenciasBodega(IDataReader reader)
        {
            _cantidadBodega = long.Parse(reader["CantidadBodegaWMS"].ToString());
            _idArticulo = long.Parse(reader["IdArticulo"].ToString());
            _idInterno = reader["idInterno"].ToString();
            _nombre = reader["Nombre"].ToString();
        }

        public EExistenciasBodega(long cantidadBodega, long idArticulo, string idInterno, string nombre)
        {
            _cantidadBodega = cantidadBodega;
            _idArticulo = idArticulo;
            _idInterno = idInterno;
            _nombre = nombre;
        }

        public long CantidadBodega { get => _cantidadBodega; set => _cantidadBodega = value; }
        public long IdArticulo { get => _idArticulo; set => _idArticulo = value; }
        public string IdInterno { get => _idInterno; set => _idInterno = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
    }
}

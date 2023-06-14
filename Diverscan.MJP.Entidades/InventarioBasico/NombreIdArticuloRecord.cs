using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.InventarioBasico
{
    [Serializable]
    public class NombreIdArticuloRecord : IdArticuloRecord
    {
        string _nombreArticulo;
        string _idInterno;

        public NombreIdArticuloRecord(long idArticulo, string nombreArticulo)
        {
            IdArticulo = idArticulo;
            _nombreArticulo = nombreArticulo;
        }

        public NombreIdArticuloRecord(IDataReader reader)
        {
            IdArticulo = long.Parse(reader["IdArticulo"].ToString());
            _nombreArticulo = reader["Nombre"].ToString();
            _idInterno = reader["idInterno"].ToString();            
        }

        public string NombreArticulo
        {
            get { return _nombreArticulo; }
            set { _nombreArticulo = value; }
        }
        public string IdInterno
        {
            get { return _idInterno; }
            set { _idInterno = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.SectorWarehouse
{
    [Serializable]
    public class EUbicacionXSector
    {
        private int _idUbicacion;
        private int _idSectorBodega;
        private string _estante;
        private string _descripcion;

        public EUbicacionXSector(string estante, string descripcion, int idUbicacion,int idSectorBodega)
        {
            _estante = estante;
            _descripcion = descripcion;
            _idUbicacion = idUbicacion;
            _idSectorBodega = idSectorBodega;
        }

        public EUbicacionXSector(IDataReader reader)
        {
            _estante = reader["estante"].ToString();
            _descripcion = reader["descripcion"].ToString();
            _idUbicacion = Convert.ToInt32(reader["idUbicacion"].ToString());
            _idSectorBodega = Convert.ToInt32(reader["idSectorBodega"].ToString());
        }

        public string Estante { get => _estante; set => _estante = value; }
        public int IdUbicacion { get => _idUbicacion; set => _idUbicacion = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public int IdSectorBodega { get => _idSectorBodega; set => _idSectorBodega = value; }
    }
}

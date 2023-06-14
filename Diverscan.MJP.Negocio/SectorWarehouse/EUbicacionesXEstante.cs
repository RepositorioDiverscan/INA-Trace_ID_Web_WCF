using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.SectorWarehouse
{
    [Serializable]
    public class EUbicacionesXEstante
    {
        private int _idUbicacion;
        private string _estante;
        private string _descripcion;

        public EUbicacionesXEstante(string estante, string descripcion,int idUbicacion)
        {
            _estante = estante;
            _descripcion = descripcion;
            _idUbicacion = idUbicacion;
        }

        public EUbicacionesXEstante(IDataReader reader)
        {
            _estante = reader["estante"].ToString();
            _descripcion = reader["descripcion"].ToString();
            _idUbicacion = Convert.ToInt32(reader["idUbicacion"].ToString());
        }

        public string Estante { get => _estante; set => _estante = value; }
        public int IdUbicacion { get => _idUbicacion; set => _idUbicacion = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }

    }
}

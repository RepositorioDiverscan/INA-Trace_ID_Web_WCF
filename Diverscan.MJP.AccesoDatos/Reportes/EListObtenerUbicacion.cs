using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    [Serializable]
    public class EListObtenerUbicacion
    {
        int _idUbicacion;
        string _Descripcion;

        public EListObtenerUbicacion(int idUbicacion, string descripcion)
        {
            _idUbicacion = idUbicacion;
            _Descripcion = descripcion;
        }

        public int IdUbicacion { get => _idUbicacion; set => _idUbicacion = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }
    }
}

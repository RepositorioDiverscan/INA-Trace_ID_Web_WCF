using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Rol
{
    [Serializable]
    public class e_Rol
    {
        long _IdRol;
        string _Nombre;
        string _Descripcion;
        string _idCompania;

        public e_Rol(long IdRol, string Nombre, string Descripcion, string idCompania) 
        {
            _IdRol = IdRol;
            _Nombre = Nombre;
            _Descripcion = Descripcion;
            _idCompania = idCompania;
        }

        public long IdRol
        {
            get { return _IdRol; }
            set { _IdRol = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public string IdCompania
        {
            get { return _idCompania; }
            set { _idCompania = value; }
        }
    }
}

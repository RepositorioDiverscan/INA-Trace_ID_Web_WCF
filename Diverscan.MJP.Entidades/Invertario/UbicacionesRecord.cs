using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Invertario
{
    public class UbicacionesRecord
    {
        long _idUbicacion;
        string _etiqueta;

        public UbicacionesRecord(long idUbicacion, string etiqueta)
        {
            _idUbicacion = idUbicacion;
            _etiqueta = etiqueta;
        }

        public long IdUbicacion
        {
            get { return _idUbicacion; }
            set { _idUbicacion = value; ;}
        }
        public string Etiqueta
        {
            get { return _etiqueta; }
            set { _etiqueta = value; }
        }
    }
}

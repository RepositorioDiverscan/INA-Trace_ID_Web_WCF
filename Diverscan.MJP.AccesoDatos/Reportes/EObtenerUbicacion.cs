using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    [Serializable]
    public class EObtenerUbicacion
    {
        int _idZona;

        public int IdZona { get => _idZona; set => _idZona = value; }
    }
}

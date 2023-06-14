using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.RolUsuarioHH
{
    [Serializable]
    public class ERolUsuarioHH
    {
        private string _permisoHH;

        public string PermisoHH { get => _permisoHH; set => _permisoHH = value; }
    }
}

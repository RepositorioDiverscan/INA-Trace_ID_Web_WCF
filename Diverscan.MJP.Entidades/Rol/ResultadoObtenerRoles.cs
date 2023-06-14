using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.Utilidades;

namespace Diverscan.MJP.Entidades.Rol
{
    public class ResultadoObtenerRoles : ResultWS
    {
        private List<e_RolHH> _RolHHs = new List<e_RolHH>();

        public List<e_RolHH> rolesHH { get => _RolHHs; set => _RolHHs = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.Utilidades;

namespace Diverscan.MJP.Entidades.Usuarios
{
    public class ResultadoObtenerUsuarios : ResultWS
    {
        private List<e_Usuario> _Usuarios = new List<e_Usuario>();

        public List<e_Usuario> usuarios { get => _Usuarios; set => _Usuarios = value; }
    }
}

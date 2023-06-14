using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.GestorImpresiones.Entidades
{
    public class e_MaestroUbicaciones
    {
        public int IdMaestroUbicacion { get; set; }
        public string IdCodigoInterno { get; set; }
        public int IdEstante { get; set; }
        public e_Estante Estante { get; set; }
    }
}

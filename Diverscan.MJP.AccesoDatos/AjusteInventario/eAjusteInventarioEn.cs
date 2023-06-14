using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.AjusteInventario
{
    [Serializable]
    public class eAjusteInventarioEn
    {
        public string FechaSolicitud { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidosUsuario { get; set; }
        public string MotivoInventario { get; set; }
        public string Tipo { get; set; }
    }
}

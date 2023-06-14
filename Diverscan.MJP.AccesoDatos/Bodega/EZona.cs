using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.ModuloConsultas
{
    [Serializable]
    public class EZona
    {       
        public int IdZona { get; set; }
      
        public string Nombre { get; set; }
    }
}

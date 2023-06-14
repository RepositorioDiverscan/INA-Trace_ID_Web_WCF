using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Articulos
{
    [Serializable]
    public class EArticuloId
    {
        public string IdInterno { get; set; }
        public string Nombre { get; set; }
        public int IdArticulo { get; set; }
    }
}

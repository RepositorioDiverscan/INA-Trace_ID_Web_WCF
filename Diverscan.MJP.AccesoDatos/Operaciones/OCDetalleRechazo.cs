using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Operaciones
{
    [Serializable]
    public class OCDetalleRechazo
    {
        public string Nombre { get; set; }
        public double CantidadRechazados { get; set; }
        public string DescripcionRechazo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Operaciones
{
    [Serializable]
    public class eDetalleFacturado
    {
        public string IdInternoArticulo { get; set; }
        public int numLinea { get; set; }
        public string Nombre { get; set; }
        public decimal CantidadSolicitada { get; set; }
        public decimal CantidadAlistada { get; set; }
        public string DocEntry { get; set; }
        public string DocNum { get; set; }
    }
}

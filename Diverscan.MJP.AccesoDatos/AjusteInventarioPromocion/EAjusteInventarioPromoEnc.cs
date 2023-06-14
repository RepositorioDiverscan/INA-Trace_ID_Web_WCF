using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.AjusteInventarioPromocion
{
    [Serializable]
    public class EAjusteInventarioPromoEnc
    {
        public string NombrePromocion { get; set; }
        public string FechaHoy { get; set; }
        public string FechaAnterior { get; set; }
    }
}

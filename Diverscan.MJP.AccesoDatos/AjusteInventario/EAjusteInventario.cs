using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.AjusteInventario
{
    [Serializable]
    public class EAjusteInventario
    {
        public string Nombre { get; set; }
        public string IdSapArticulo { get; set; }
        public int Cantidad { get; set; }
        public string IdSapBodega { get; set; }
        public DateTime FechaAprobado { get; set; }
        public string Justificacion { get; set; }
        public string TipoAjuste { get; set; }
        public string EsRegalia { get; set; }
        public int IdBodega { get; set; }
    }
}

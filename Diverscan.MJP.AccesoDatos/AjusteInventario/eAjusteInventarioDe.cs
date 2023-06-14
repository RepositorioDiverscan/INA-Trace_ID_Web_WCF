using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.AjusteInventario
{
    [Serializable]
    public class eAjusteInventarioDe
    {
        public string IdInterno { get; set; }
        public string NombreArticulo { get; set; }
        public string Lote { get; set; }
        public string FechaVencimiento { get; set; }
        public string UnidadMedida { get; set; }
        public string Ubicacion { get; set; }
        public string CantidadUI { get; set; }
    }
}

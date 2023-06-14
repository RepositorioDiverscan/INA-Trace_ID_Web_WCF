using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.GestorImpresiones.Entidades
{
    public class e_Inventario
    {
        public string codigoInterno { get; set; }
        public string numeroGTIN { get; set; }
        public string clasificacion { get; set; }
        public string cantidad { get; set; }
        public string GTIN14 { get; set; }
        public string Lote { get; set; }
        public string fechaVencimiento { get; set; }
        public string fechaVencimientoform { get; set; }
    }
}

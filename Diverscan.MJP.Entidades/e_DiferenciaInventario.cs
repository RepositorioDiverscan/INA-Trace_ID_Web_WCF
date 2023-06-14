using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    public class e_DiferenciaInventario
    {
        public string NombreArticulo { get; set; }
        public string CodigoInterno { get; set; }

        public double DisponiblesInventario { get; set; }
        public double DisponiblesERP { get; set; }

        public double DiferenciaERPInventario { get; set; }

        public string UnidadMedida { get; set; }
    }
}

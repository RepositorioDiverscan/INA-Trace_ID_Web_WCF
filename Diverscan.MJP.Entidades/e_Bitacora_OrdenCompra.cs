using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    public class e_Bitacora_OrdenCompra
    {
        public Int64 NumeroPedido { get; set; }
        public string Hora { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.GestorImpresiones.Entidades
{
    public class e_Inventario_Movim
    {
        public string idInventario { get; set; }
        public string IdCodInterno { get; set; }
        public string idCatalogo { get; set; }
        public string Cantidad { get; set; }
        public string CantDisponible { get; set; }
        public string CantReservada { get; set; }
    }
}

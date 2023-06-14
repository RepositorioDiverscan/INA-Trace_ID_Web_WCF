using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.GestorImpresiones.Entidades
{
    public class e_Bodega
    {
        public int IdBodega { get; set; }
        public int IdAlmacen { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}

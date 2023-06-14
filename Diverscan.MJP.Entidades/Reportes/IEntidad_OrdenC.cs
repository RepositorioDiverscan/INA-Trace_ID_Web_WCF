using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes
{
    public interface IEntidad_OrdenC
    {

        int Orden_Compra { get; set; }
        string Producto { get; set; }
        decimal Cantidad_Recibida { get; set; }
        decimal Cantidad_Rechazada { get; set; } 
        decimal Articulos_OC { get; set; }
        DateTime FechaRegistro { get; set; } 
        DateTime FechaCreacion { get; set; } 
        

    }
}

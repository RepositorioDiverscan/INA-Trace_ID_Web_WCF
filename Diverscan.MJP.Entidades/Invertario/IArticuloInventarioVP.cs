using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Invertario
{
     public interface IArticuloInventarioVP
    {
        long IdUbicacion { get; }
        string Etiqueta { get; }
        long IdArticulo { get; }
        string IdInterno { get; }
        int Cantidad { get; set; }
        DateTime FechaVencimiento { get; }
        string Lote { get; }
        string UnidadMedida { get; }
        bool EsGranel { get; }
        string NombreArticulo { get; }
        double UnidadInventario { get; }
    }
}

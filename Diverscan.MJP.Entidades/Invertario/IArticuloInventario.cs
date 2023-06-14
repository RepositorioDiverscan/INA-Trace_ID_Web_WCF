using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Invertario
{
    public interface IArticuloInventario
    {
        long IdUbicacion { get; }
        string Etiqueta { get; }
        long IdArticulo { get; }
        int Cantidad { get; set; }
        DateTime FechaVencimiento { get; }
        string Lote { get; }
        string UnidadMedida { get; }
        bool EsGranel { get; }
        string NombreArticulo { get; }
        double UnidadInventario { get; }
        string IdInterno { get; }
    }
}

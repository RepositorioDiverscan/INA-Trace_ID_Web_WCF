using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Reportes
{
    public interface IEntidad_Despacho
    {
        int Solicitud { get; set; }

        string NombreArticulo { get; set; }

        string Referencia { get; set; }

        double Cantidad { get; set; }

        string SSCC { get; set; }

        string Destino { get; set; }

        DateTime FechaDespacho { get; set; }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes
{
    public interface IEntidad_PersonalAlisto
    {

        int Solicitud { get; set; }

        string Destino { get; set; }

        string CantidadAlisto { get; set; }

        string CantidadPedido { get; set; }

        string Referencia_Interno { get; set; }

        string NombreArticulo { get; set; }

        string SSCCAsociado { get; set; }

        string CantidadUnidadAlisto { get; set; }

        string Encargado { get; set; }

        string Alistado { get; set; }

        string Suspendido { get; set; }

        string FechaCreacion { get; set; }

        string FechaRegistro { get; set; }

        string FechaAsignacion { get; set; }
    }
}

using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diverscan.MJP.AccesoDatos.Reportes.Reporte_Ajuste_de_Inventario
{
    public interface IObtenerPromociones
    {
        List<NObtenerEncabezadoPromocion> ObtenerPromociones();

        List<EObtenerArticulosPromocion> ObtenerArticulosDePromocion(int idMaestroPromocion);

        List<AjusteSolicitudRecord> ObtenesAjustesPorFechas(DateTime fechaInicio, DateTime fechaFinal);

        List<EObtieneArticulosSolicitud> ObtenerArticulosPorAjuste(int idSolicitud);
    }

   
}

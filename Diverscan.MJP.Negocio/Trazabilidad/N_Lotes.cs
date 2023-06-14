using Diverscan.MJP.AccesoDatos.Trazabilidad;
using Diverscan.MJP.Entidades.Trazabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Trazabilidad
{
    public class N_Lotes
    {
        public static List<LotesRecord> ObtenerLotesPorArticuloFecha(long idArticulo, DateTime fechaInicio, DateTime fechaFin)
        {
            LotesDBA lotesDBA = new LotesDBA();
            return lotesDBA.ObtenerLotesPorArticuloFecha(idArticulo, fechaInicio, fechaFin);
        }
    }
}

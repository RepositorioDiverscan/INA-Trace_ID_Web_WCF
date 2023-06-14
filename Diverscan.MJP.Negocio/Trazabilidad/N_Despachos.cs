using Diverscan.MJP.AccesoDatos.Trazabilidad;
using Diverscan.MJP.Entidades.Trazabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Trazabilidad
{
    public class N_Despachos
    {
        public static List<DespachosRecord> ObtenerRepuestasCalidadPorLote(long idArticulo, string lote)
        {
            DespachosDBA despachosDBA = new DespachosDBA();
            return despachosDBA.ObtenerRepuestasCalidadPorLote(idArticulo, lote);
        }

        public static List<DespachosRecord> ObtenerRepuestasCalidadPorFechaVencimiento(long idArticulo, DateTime fechaVencimiento)
        {
            DespachosDBA despachosDBA = new DespachosDBA();
            return despachosDBA.ObtenerRepuestasCalidadPorFechaVencimiento(idArticulo, fechaVencimiento);
        }
    }
}

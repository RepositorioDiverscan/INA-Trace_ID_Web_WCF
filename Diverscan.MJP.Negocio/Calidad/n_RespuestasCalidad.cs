using Diverscan.MJP.AccesoDatos.Calidad;
using Diverscan.MJP.Entidades.Calidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Calidad
{
    public class n_RespuestasCalidad
    {
        public static List<RespuestasCalidad> ObtenerRepuestasCalidadPorLote(long idArticulo, string lote)
        {
            RepuestasCalidadPorLoteDBA repuestasCalidadPorLoteDBA = new RepuestasCalidadPorLoteDBA();
            return repuestasCalidadPorLoteDBA.ObtenerRepuestasCalidadPorLote(idArticulo, lote);
        }
        public static List<RespuestasCalidad> ObtenerRepuestasCalidadPorFechaVencimiento(long idArticulo, DateTime dateTime)
        {
            RepuestasCalidadPorLoteDBA repuestasCalidadPorLoteDBA = new RepuestasCalidadPorLoteDBA();
            return repuestasCalidadPorLoteDBA.ObtenerRepuestasCalidadPorFechaVencimiento(idArticulo, dateTime);
        }
        
    }
}

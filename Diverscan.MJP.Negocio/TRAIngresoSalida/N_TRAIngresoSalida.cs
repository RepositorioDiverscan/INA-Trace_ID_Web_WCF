using Diverscan.MJP.AccesoDatos.Kardex;
using Diverscan.MJP.AccesoDatos.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.TRAIngresoSalida
{
    public class N_TRAIngresoSalida
    {
        public static void InsertTRAIngresoSalidaRecord(TRAIngresoSalidaArticulosRecord tRAIngresoSalidaArticulosRecord)
        {
            TRAIngresoSalida_DBA tRAIngresoSalida_DBA = new TRAIngresoSalida_DBA();
            long idRegistroTRA=0;
            tRAIngresoSalida_DBA.InsertTRAIngresoSalidaRecord(tRAIngresoSalidaArticulosRecord, out idRegistroTRA);
        }
        public static List<TRAIngresoSalidaArticulosRecord> GetTRAIngresoSalida(long idArticulo, string lote, DateTime fechaVencimiento,
             long idUbicacion, int idEstado)
        {
            TRAIngresoSalida_DBA tRAIngresoSalida_DBA = new TRAIngresoSalida_DBA();
            return  tRAIngresoSalida_DBA.GetTRAIngresoSalida(idArticulo, lote, fechaVencimiento, idUbicacion, idEstado);
        }

        public static void InsertTRAIngresoSalidaRecord(List<TRAIngresoSalidaArticulosRecord> tRAIngresoSalidaArticulosRecord)
        {
            TRAIngresoSalida_DBA tRAIngresoSalida_DBA = new TRAIngresoSalida_DBA();
            tRAIngresoSalida_DBA.InsertTRAIngresoRecord(tRAIngresoSalidaArticulosRecord);
        }

        public static void InsertTRASalidaRecord(List<TRAIngresoSalidaArticulosRecord> tRAIngresoSalidaArticulosRecord)
        {
            TRAIngresoSalida_DBA tRAIngresoSalida_DBA = new TRAIngresoSalida_DBA();
            tRAIngresoSalida_DBA.InsertTRASalidaRecord(tRAIngresoSalidaArticulosRecord);
        }

        public List<KardexInfoBase> GetTrazabilityProduct(string idInternoArticulo, int idWarehouse, DateTime fechaInicio, DateTime fechaFin)
        {
            TRAIngresoSalida_DBA tRAIngresoSalida_DBA = new TRAIngresoSalida_DBA();
            return tRAIngresoSalida_DBA.GetTrazabilityProduct(idInternoArticulo, idWarehouse, fechaInicio, fechaFin);
        }
    }
}

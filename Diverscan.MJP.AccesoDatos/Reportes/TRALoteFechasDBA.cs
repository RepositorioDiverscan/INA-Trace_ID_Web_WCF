using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Entidades.Trazabilidad;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    public class TRALoteFechasDBA
    {
        public List<TRARecordTrazabilidad> GetTRALote(long idArticulo, string lote)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerMovimientosPorLoteFecha");

            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, lote);       

            List<TRARecordTrazabilidad> TRAList = new List<TRARecordTrazabilidad>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    TRAList.Add(new TRARecordTrazabilidad(reader));
                }
            }
            return TRAList;
        }

        public List<TRARecordTrazabilidad> GetTRAVencimiento(long idArticulo, DateTime fechaVencimiento)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerMovimientosPorVencimientoFecha");

            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.DateTime, fechaVencimiento.Date);

            List<TRARecordTrazabilidad> TRAList = new List<TRARecordTrazabilidad>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    TRAList.Add(new TRARecordTrazabilidad(reader));
                }
            }
            return TRAList;
        }
    }
}

using Diverscan.MJP.Entidades.Trazabilidad;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Trazabilidad
{
    public class DespachosDBA
    {
        public List<DespachosRecord> ObtenerRepuestasCalidadPorLote(long idArticulo, string lote)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerDespachosPorLote");

            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, lote);

            List<DespachosRecord> TRAList = new List<DespachosRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    TRAList.Add(new DespachosRecord(reader));
                }
            }
            return TRAList;
        }

        public List<DespachosRecord> ObtenerRepuestasCalidadPorFechaVencimiento(long idArticulo, DateTime fechaVencimiento)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerDespachosPorFechaVencimiento");

            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.DateTime, fechaVencimiento);

            List<DespachosRecord> TRAList = new List<DespachosRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    TRAList.Add(new DespachosRecord(reader));
                }
            }
            return TRAList;
        }
    }
}

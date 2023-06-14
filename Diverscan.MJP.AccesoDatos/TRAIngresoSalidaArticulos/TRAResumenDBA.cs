using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.TRAIngresoSalidaArticulos
{
    public class TRAResumenDBA
    {
        public List<TRAIngresoSalidaArticulosRecord> ObtenerArticuloSalida(long idArticulo, long idUbicacion,string lote, DateTime fechaVencimiento)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerSalidaArticulos");

            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@IdUbicacion", DbType.Int64, idUbicacion);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, lote);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.DateTime, fechaVencimiento);

            List<TRAIngresoSalidaArticulosRecord> TRAList = new List<TRAIngresoSalidaArticulosRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    TRAList.Add(new TRAIngresoSalidaArticulosRecord(reader));
                }
            }
            return TRAList;
        }

        public int ObtenerCantidadArticulosSalida(long idArticulo, long idUbicacion, string lote, DateTime fechaVencimiento)
        {
          

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerSalidaArticulosDisponibles");

            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@IdUbicacion", DbType.Int64, idUbicacion);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, lote);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.DateTime, fechaVencimiento);

            int cantidad = 0;
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    cantidad = Int32.Parse(reader["Cantidad"].ToString());
                    break;
                }
            }

            return cantidad;
        }
    }
}

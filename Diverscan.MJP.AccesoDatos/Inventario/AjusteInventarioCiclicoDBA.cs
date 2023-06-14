using Diverscan.MJP.AccesoDatos.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Inventario
{
    public class AjusteInventarioCiclicoDBA
    {
        public void AjusteEntrada(TRAIngresoSalidaArticulosRecord tRAIngresoSalidaArticulosRecord, long idInventarioCiclico)
        {
            TRAIngresoSalida_DBA tRAIngresoSalida_DBA = new TRAIngresoSalida_DBA();
            long idRegistroTRA = 0;
            tRAIngresoSalida_DBA.InsertTRAIngresoSalidaRecord(tRAIngresoSalidaArticulosRecord, out idRegistroTRA);

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_AjusteEntradaCopiaSistemaArticuloDetalleCiclico");

            dbTse.AddInParameter(dbCommand, "@IdInventarioCiclico", DbType.Int64, idInventarioCiclico);
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, tRAIngresoSalidaArticulosRecord.IdArticulo);
            dbTse.AddInParameter(dbCommand, "@TRAIdRegistro", DbType.Int64, idRegistroTRA);
            dbTse.AddInParameter(dbCommand, "@IdUbicacion", DbType.Int64, tRAIngresoSalidaArticulosRecord.IdUbicacion);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, tRAIngresoSalidaArticulosRecord.Lote);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.Date, tRAIngresoSalidaArticulosRecord.FechaVencimiento);
            dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Decimal, tRAIngresoSalidaArticulosRecord.Cantidad);
            var result = dbTse.ExecuteNonQuery(dbCommand);
        }

        public void AjusteSalida(TRAIngresoSalidaArticulosRecord tRAIngresoSalidaArticulosRecord, long idInventarioCiclico)
        {
            TRAIngresoSalida_DBA tRAIngresoSalida_DBA = new TRAIngresoSalida_DBA();
            long idRegistroTRA = 0;
            tRAIngresoSalida_DBA.InsertTRAIngresoSalidaRecord(tRAIngresoSalidaArticulosRecord, out idRegistroTRA);

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_AjusteSalidaCopiaSistemaArticuloDetalleCiclico");

            dbTse.AddInParameter(dbCommand, "@IdInventarioCiclico", DbType.Int64, idInventarioCiclico);
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, tRAIngresoSalidaArticulosRecord.IdArticulo);
            dbTse.AddInParameter(dbCommand, "@IdUbicacion", DbType.Int64, tRAIngresoSalidaArticulosRecord.IdUbicacion);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, tRAIngresoSalidaArticulosRecord.Lote);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.Date, tRAIngresoSalidaArticulosRecord.FechaVencimiento);
            dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Decimal, tRAIngresoSalidaArticulosRecord.Cantidad);
            var result = dbTse.ExecuteNonQuery(dbCommand);
        }

        public void AjusteEntrada(List<TRAIngresoSalidaArticulosRecord> tRAIngresoSalidaArticulosRecord, long idInventarioBasico, int idUsuario)
        {
            var dataTable = tRAIngresoSalidaArticulosRecord.ToDataTable();
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InsertarAjusteEntradaCiclico");

            dbTse.AddInParameter(dbCommand, "@IdInventarioCiclico", DbType.Int64, idInventarioBasico);
            dbTse.AddInParameter(dbCommand, "@SAIdUsuario", DbType.Int32, idUsuario);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ArticulosData";
            parameter.SqlDbType = System.Data.SqlDbType.Structured;
            parameter.Value = dataTable;
            dbCommand.Parameters.Add(parameter);

            dbCommand.CommandTimeout = 3600;
            var result = dbTse.ExecuteNonQuery(dbCommand);
        }

        public void AjusteSalida(List<TRAIngresoSalidaArticulosRecord> tRAIngresoSalidaArticulosRecord, long idInventarioBasico, int idUsuario)
        {
            var dataTable = tRAIngresoSalidaArticulosRecord.ToDataTable();
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InsertarAjusteSalidaCiclico");

            dbTse.AddInParameter(dbCommand, "@IdInventarioCiclico", DbType.Int64, idInventarioBasico);
            dbTse.AddInParameter(dbCommand, "@SAIdUsuario", DbType.Int32, idUsuario);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ArticulosData";
            parameter.SqlDbType = System.Data.SqlDbType.Structured;
            parameter.Value = dataTable;
            dbCommand.Parameters.Add(parameter);

            dbCommand.CommandTimeout = 3600;
            var result = dbTse.ExecuteNonQuery(dbCommand);
        }
    }
}

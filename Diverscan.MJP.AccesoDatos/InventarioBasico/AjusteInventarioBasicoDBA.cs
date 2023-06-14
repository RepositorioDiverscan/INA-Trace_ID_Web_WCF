using Diverscan.MJP.AccesoDatos.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.InventarioBasico
{
    public class AjusteInventarioBasicoDBA
    {
        public void AjusteEntrada(TRAIngresoSalidaArticulosRecord tRAIngresoSalidaArticulosRecord,long idInventarioBasico)
        {
            TRAIngresoSalida_DBA tRAIngresoSalida_DBA = new TRAIngresoSalida_DBA();
             long idRegistroTRA=0;
             tRAIngresoSalida_DBA.InsertTRAIngresoSalidaRecord(tRAIngresoSalidaArticulosRecord, out idRegistroTRA);

             var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
             var dbCommand = dbTse.GetStoredProcCommand("SP_AjusteEntradaCopiaSistemaArticuloDetalle");

             dbTse.AddInParameter(dbCommand, "@IdInventarioBasico", DbType.Int64, idInventarioBasico);
             dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, tRAIngresoSalidaArticulosRecord.IdArticulo);
             dbTse.AddInParameter(dbCommand, "@TRAIdRegistro", DbType.Int64, idRegistroTRA);
             dbTse.AddInParameter(dbCommand, "@IdUbicacion", DbType.Int64, tRAIngresoSalidaArticulosRecord.IdUbicacion);
             dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, tRAIngresoSalidaArticulosRecord.Lote);             
             dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.Date, tRAIngresoSalidaArticulosRecord.FechaVencimiento);
             dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Decimal, tRAIngresoSalidaArticulosRecord.Cantidad);
             var result = dbTse.ExecuteNonQuery(dbCommand);
        }

        public void AjusteSalida(TRAIngresoSalidaArticulosRecord tRAIngresoSalidaArticulosRecord, long idInventarioBasico)
        {
            TRAIngresoSalida_DBA tRAIngresoSalida_DBA = new TRAIngresoSalida_DBA();
            long idRegistroTRA = 0;
            tRAIngresoSalida_DBA.InsertTRAIngresoSalidaRecord(tRAIngresoSalidaArticulosRecord, out idRegistroTRA);

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_AjusteSalidaCopiaSistemaArticuloDetalle");

            dbTse.AddInParameter(dbCommand, "@IdInventarioBasico", DbType.Int64, idInventarioBasico);
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, tRAIngresoSalidaArticulosRecord.IdArticulo);           
            dbTse.AddInParameter(dbCommand, "@IdUbicacion", DbType.Int64, tRAIngresoSalidaArticulosRecord.IdUbicacion);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, tRAIngresoSalidaArticulosRecord.Lote);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.Date, tRAIngresoSalidaArticulosRecord.FechaVencimiento);
            dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Decimal, tRAIngresoSalidaArticulosRecord.Cantidad);
            var result = dbTse.ExecuteNonQuery(dbCommand);
        }

        public void AjusteEntrada(List<TRAIngresoSalidaArticulosRecord> tRAIngresoSalidaArticulosRecord, long idInventarioBasico, int idUsuario, int idBodega)
        {
            var dataTable = tRAIngresoSalidaArticulosRecord.ToDataTable();
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InsertarAjusteEntradaBasico");

            dbTse.AddInParameter(dbCommand, "@IdInventarioBasico", DbType.Int64, idInventarioBasico);
            dbTse.AddInParameter(dbCommand, "@SAIdUsuario", DbType.Int32, idUsuario);
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ArticulosData";
            parameter.SqlDbType = System.Data.SqlDbType.Structured;
            parameter.Value = dataTable;
            dbCommand.Parameters.Add(parameter);

            dbCommand.CommandTimeout = 3600;
            var result = dbTse.ExecuteNonQuery(dbCommand);
        }

        public void AjusteSalida(List<TRAIngresoSalidaArticulosRecord> tRAIngresoSalidaArticulosRecord, long idInventarioBasico, int idUsuario, int idBodega)
        {
            var dataTable = tRAIngresoSalidaArticulosRecord.ToDataTable();
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InsertarAjusteSalidaBasico");

            dbTse.AddInParameter(dbCommand, "@IdInventarioBasico", DbType.Int64, idInventarioBasico);
            dbTse.AddInParameter(dbCommand, "@SAIdUsuario", DbType.Int32, idUsuario);
            dbTse.AddInParameter(dbCommand, "@idWarehouse", DbType.Int32, idBodega);

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

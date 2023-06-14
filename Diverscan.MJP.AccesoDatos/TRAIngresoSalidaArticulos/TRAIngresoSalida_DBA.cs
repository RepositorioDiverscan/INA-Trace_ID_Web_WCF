using Diverscan.MJP.AccesoDatos.Kardex;
using Diverscan.MJP.Entidades.TRAIngresoSalidaArticulos;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.TRAIngresoSalidaArticulos
{
    public class TRAIngresoSalida_DBA
    {
        public void InsertTRAIngresoSalidaRecord(TRAIngresoSalidaArticulosRecord tRAIngresoSalidaArticulosRecord, out long idRegistroTRA)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_IngresarTRAIngresoSalida");

            dbTse.AddInParameter(dbCommand, "@SumUno_RestaCero", DbType.Boolean, tRAIngresoSalidaArticulosRecord.SumUno_RestaCero);
            dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, tRAIngresoSalidaArticulosRecord.IdArticulo);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.Date, tRAIngresoSalidaArticulosRecord.FechaVencimiento);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, tRAIngresoSalidaArticulosRecord.Lote);
            dbTse.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, tRAIngresoSalidaArticulosRecord.IdUsuario);
            dbTse.AddInParameter(dbCommand, "@idMetodoAccion", DbType.Int64, tRAIngresoSalidaArticulosRecord.IdMetodoAccion);
            dbTse.AddInParameter(dbCommand, "@idTablaCampoDocumentoAccion", DbType.String, tRAIngresoSalidaArticulosRecord.IdTablaCampoDocumentoAccion);
            dbTse.AddInParameter(dbCommand, "@idCampoDocumentoAccion", DbType.String, tRAIngresoSalidaArticulosRecord.IdCampoDocumentoAccion);
            dbTse.AddInParameter(dbCommand, "@NumDocumentoAccion", DbType.String, tRAIngresoSalidaArticulosRecord.NumDocumentoAccion);
            dbTse.AddInParameter(dbCommand, "@idUbicacion", DbType.Int64, tRAIngresoSalidaArticulosRecord.IdUbicacion);
            dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Decimal, tRAIngresoSalidaArticulosRecord.Cantidad);
            dbTse.AddInParameter(dbCommand, "@Procesado", DbType.Boolean, tRAIngresoSalidaArticulosRecord.Procesado);            
            dbTse.AddInParameter(dbCommand, "@idEstado", DbType.Int32, tRAIngresoSalidaArticulosRecord.IdEstado);
            dbTse.AddOutParameter(dbCommand, "@Identity", DbType.Int64, 0);
            
            var result = dbTse.ExecuteNonQuery(dbCommand);
            idRegistroTRA = Convert.ToInt64(dbCommand.Parameters["@Identity"].Value); 
        }

        public List<TRAIngresoSalidaArticulosRecord> GetTRAIngresoSalida(long idArticulo, string lote, DateTime fechaVencimiento,
            long idUbicacion, int idEstado)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_TRAIngresoSalida");

            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, lote);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.Date, fechaVencimiento);
            dbTse.AddInParameter(dbCommand, "@IdUbicacion", DbType.Int64, idUbicacion);
            dbTse.AddInParameter(dbCommand, "@IdEstado", DbType.Int32, idEstado);

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

        public void InsertTRAIngresoRecord(List<TRAIngresoSalidaArticulosRecord> tRAIngresoSalidaArticulosRecord)
        {
            var dataTable = tRAIngresoSalidaArticulosRecord.ToDataTable();
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InsertarArticulosAjusteEntrada");

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ArticulosData";
            parameter.SqlDbType = System.Data.SqlDbType.Structured;
            parameter.Value = dataTable;
            dbCommand.Parameters.Add(parameter);

            dbCommand.CommandTimeout = 3600;
            var result = dbTse.ExecuteNonQuery(dbCommand);
        }

        public void InsertTRASalidaRecord(List<TRAIngresoSalidaArticulosRecord> tRAIngresoSalidaArticulosRecord)
        {
            var dataTable = tRAIngresoSalidaArticulosRecord.ToDataTable();
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InsertarArticulosAjusteSalida");

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ArticulosData";
            parameter.SqlDbType = System.Data.SqlDbType.Structured;
            parameter.Value = dataTable;
            dbCommand.Parameters.Add(parameter);

            dbCommand.CommandTimeout = 3600;
            var result = dbTse.ExecuteNonQuery(dbCommand);
        }

        public List<KardexInfoBase> GetTrazabilityProduct(string idInternoArticulo, int idWarehouse, DateTime fechaInicio, DateTime fechaFin)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_GetTrazabilityProduct");
            dbTse.AddInParameter(dbCommand, "@idProduct", DbType.String, idInternoArticulo);
            dbTse.AddInParameter(dbCommand, "@idWarehouse", DbType.Int32, idWarehouse);
            dbTse.AddInParameter(dbCommand, "@dateInit", DbType.DateTime, fechaInicio);
            dbTse.AddInParameter(dbCommand, "@dateEnd", DbType.DateTime, fechaFin);

            List<KardexInfoBase> cantidadList = new List<KardexInfoBase>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    KardexInfoBase articulo = new KardexInfoBase();
                    articulo.IdRegistro = int.Parse(reader["IdTrazabilidad"].ToString());
                    articulo.IdArticulo = int.Parse(reader["IdArticulo"].ToString());
                    articulo.NombreArticulo = reader["Articulo"].ToString();
                    articulo.Lote = reader["Lote"].ToString();
                    articulo.FechaVencimiento = DateTime.Parse(reader["FechaVencimiento"].ToString());
                    articulo.NombreBodega = reader["Bodega"].ToString();
                    articulo.IdUbicacion = long.Parse(reader["IdUbicacion"].ToString());
                    articulo.Ubicacion = reader["Ubicacion"].ToString();
                    articulo.Cantidad = int.Parse(reader["Cantidad"].ToString());
                    articulo.IdEstado = int.Parse(reader["idEstado"].ToString());
                    articulo.NombreEstado = reader["Operacion"].ToString();
                    articulo.FechaRegistro = DateTime.Parse(reader["FechaRegistro"].ToString());
                    var data = reader["IdMetodoAccion"].ToString();
                    articulo.IdMetodoAccion = int.Parse(reader["IdMetodoAccion"].ToString());

                    if (articulo.IdMetodoAccion == 8)
                    {
                        articulo.MetodoAccion = "Recepción";
                    }else
                        articulo.MetodoAccion = "Traslado";


                    cantidadList.Add(articulo);
                }
            }
            return cantidadList;
        }
    }
}

using Diverscan.MJP.Entidades.Trazabilidad;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Trazabilidad
{
    public class LotesDBA
    {
        public List<LotesRecord> ObtenerLotesPorArticuloFecha(long idArticulo , DateTime fechaInicio, DateTime fechaFin)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerLotesArticulos");
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, fechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, fechaFin);

            List<LotesRecord> cantidadList = new List<LotesRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                   
                    var lote = reader["Lote"].ToString();                   
                    var fechaMovimiento = DateTime.Parse(reader["FechaVencimiento"].ToString());  
                    cantidadList.Add(new LotesRecord(lote, fechaMovimiento));
                }
            }
            return cantidadList;
        }

        public List<LotesRecord> ObtenerLotesDespachadosPorArticuloFecha(long idArticulo, DateTime fechaInicio, DateTime fechaFin)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerLotesArticulos_Despachados");
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, fechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, fechaFin);

            List<LotesRecord> cantidadList = new List<LotesRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {

                    var lote = reader["Lote"].ToString();
                    var fechaMovimiento = DateTime.Parse(reader["FechaVencimiento"].ToString());
                    cantidadList.Add(new LotesRecord(lote, fechaMovimiento));
                }
            }
            return cantidadList;
        }

        public List<LotesRecord> ObtenerLotesTrazabilidadBodegaArticulo(long idArticulo, DateTime fechaInicio, DateTime fechaFin)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_Lotes_Trazabilidad_Bodega_Articulo");
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, fechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, fechaFin);

            List<LotesRecord> cantidadList = new List<LotesRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {

                    var lote = reader["Lote"].ToString();
                    var fechaMovimiento = DateTime.Parse(reader["FechaVencimiento"].ToString());
                    cantidadList.Add(new LotesRecord(lote, fechaMovimiento));
                }
            }
            return cantidadList;
        }
    }
}

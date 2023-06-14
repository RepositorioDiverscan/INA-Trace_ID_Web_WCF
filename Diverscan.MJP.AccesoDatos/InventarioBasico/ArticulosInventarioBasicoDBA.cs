using Diverscan.MJP.Entidades.Invertario;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.InventarioBasico
{
    public class ArticulosInventarioBasicoDBA
    {
        public int InsertarTomaFisicaInventario(TomaFisicaInventario tomaFisicaInventario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InsertarArticulosInventarioBasico");
            dbTse.AddInParameter(dbCommand, "@IdInventario", DbType.Int64, tomaFisicaInventario.IdInventario);
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, tomaFisicaInventario.IdArticulo);
            dbTse.AddInParameter(dbCommand, "@IdUbicacion", DbType.Int64, tomaFisicaInventario.IdUbicacion);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, tomaFisicaInventario.Lote);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.DateTime, tomaFisicaInventario.FechaVencimiento);
            dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Int32, tomaFisicaInventario.Cantidad);
            dbTse.AddInParameter(dbCommand, "@usuario", DbType.Int32, tomaFisicaInventario.UsuarioID);
            var result = dbTse.ExecuteNonQuery(dbCommand);
            return result;
        }


        public List<ICantidadPorUbicacionArticuloRecord> ObtenerArticulosInventarioBasico(long idInventario, long idArticulo)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerArticulosTomaFisicaBasico");
            dbTse.AddInParameter(dbCommand, "@IdInventario", DbType.Int64, idInventario);
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);

            List<ICantidadPorUbicacionArticuloRecord> cantidadList = new List<ICantidadPorUbicacionArticuloRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    cantidadList.Add(new CantidadPorUbicacionArticuloRecord(reader));
                }
            }
            return cantidadList;
        }

        public List<ICantidadPorUbicacionArticuloRecord> ObtenerTodosArticulosInventarioBasico(long idInventario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerTodosArticulosTomaFisicaBasico");
            dbTse.AddInParameter(dbCommand, "@IdInventario", DbType.Int64, idInventario);

            List<ICantidadPorUbicacionArticuloRecord> cantidadList = new List<ICantidadPorUbicacionArticuloRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    cantidadList.Add(new CantidadPorUbicacionArticuloRecord(reader));
                }
            }
            return cantidadList;
        }
    }
}

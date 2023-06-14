using Diverscan.MJP.Entidades.Invertario;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Inventario
{
    public class TomaFisicaInventarioDBA
    {
        public int InsertarTomaFisicaInventario(TomaFisicaInventario tomaFisicaInventario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InsertarTomaFisicaInventario");
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
    }
}

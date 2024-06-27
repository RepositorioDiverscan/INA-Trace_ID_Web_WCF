using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Diverscan.MJP.AccesoDatos.Operaciones
{
    public class DBIngresoTrasladoBodega
    {
        public string CrearIngresoTrasladoBodegas(EIngresoTrasladoBodega eIngresoTraslado)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");

            var dbCommand = db.GetStoredProcCommand("SP_CrearIngresoTrasladoBodega");
            db.AddInParameter(dbCommand, "@numTransaccion", DbType.String, eIngresoTraslado.NumeroTransaccion);
            db.AddInParameter(dbCommand, "@idBodega", DbType.Int32, eIngresoTraslado.IdBodega);
            db.AddInParameter(dbCommand, "@IdBodegaTraslado", DbType.Int32, eIngresoTraslado.IdBodegaTraslado);
            db.AddInParameter(dbCommand, "@comentario", DbType.String, eIngresoTraslado.Comentario);
            db.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, eIngresoTraslado.IdUsuario);
            db.AddInParameter(dbCommand, "@idArticulo", DbType.Int32, eIngresoTraslado.IdArticulo);
            db.AddInParameter(dbCommand, "@cantidadSolicitada", DbType.Int32, eIngresoTraslado.CantidadSolicitada);

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    return reader["respuesta"].ToString();
                }
            }
            return "Error";
        }
    }
}

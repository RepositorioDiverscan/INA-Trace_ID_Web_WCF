using Diverscan.MJP.Entidades.Articulo;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Existencias
{
    public class ExistenciasPorArticulo
    {
        public int ObtenerExistenciaPorArticulo(ArticuloTrazaInfo articuloTrazaInfo)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerCantidadDisponibleParaSalida_VP");
                  
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, articuloTrazaInfo.IdArticulo);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, articuloTrazaInfo.Lote);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.DateTime, articuloTrazaInfo.FechaVencimiento);
            dbTse.AddInParameter(dbCommand, "@IdUbicacion", DbType.Int64, articuloTrazaInfo.IdUbicacion);           
            dbTse.AddOutParameter(dbCommand, "@Cantidad", DbType.String, 1000);
            dbTse.ExecuteNonQuery(dbCommand);
            int result = Convert.ToInt32(dbCommand.Parameters["@Cantidad"].Value);
            return result;
        }
    }
}

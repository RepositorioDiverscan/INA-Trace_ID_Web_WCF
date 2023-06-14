using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.AjusteInventario
{
    public class DetalleArticulo_DBA
    {
        public ArticuloRecord ObtenerArticuloPorIdArticulo(long idArticulo)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerDetalleArticulo");

            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);

            ArticuloRecord  articuloRecord = null;

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    articuloRecord = new ArticuloRecord(idArticulo, reader["idInterno"].ToString(), reader["Nombre"].ToString(), 
                        reader["Unidad_Medida"].ToString(),bool.Parse(reader["Granel"].ToString()));
                    break;
                }
            }
            return articuloRecord;
        }
    }
}

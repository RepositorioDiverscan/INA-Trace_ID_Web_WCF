using Diverscan.MJP.Entidades.Invertario;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Inventario
{
    public class MaestroArticuloDBA
    {
        public List<MaestroArticuloRecord> GetArticulosInventarioCiclicos(int idCategoriaArticulo)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerArticulosXCategoria");

            dbTse.AddInParameter(dbCommand, "@idCategoriaArticulo", DbType.Int32, idCategoriaArticulo);

            List<MaestroArticuloRecord> articulosList = new List<MaestroArticuloRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    long idArticulo = long.Parse(reader["idArticulo"].ToString());
                    string nombre = reader["Nombre"].ToString();

                    articulosList.Add(new MaestroArticuloRecord(idArticulo, nombre));
                }
            }
            return articulosList;
        }

    }
}

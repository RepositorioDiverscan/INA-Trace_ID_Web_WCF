using Diverscan.MJP.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.CategoriaArticulo
{
    public class CategoriaArticuloDBA
    {
        public void InsertCategoriaArticulo(e_CategoriaArticulo e_categoriaProducto)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("IngresarCategoriaArticulo");

            dbTse.AddInParameter(dbCommand, "@Nombre", DbType.String, e_categoriaProducto.Nombre);                     
            var result = dbTse.ExecuteNonQuery(dbCommand);
        }

        public List<e_CategoriaArticulo> GetCategoriaArticulo()
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_CategoriaArticulo");

            List<e_CategoriaArticulo> categoriaProductoList = new List<e_CategoriaArticulo>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    categoriaProductoList.Add(new e_CategoriaArticulo(reader));
                }
            }
            return categoriaProductoList;
        }

        public void UpdateCategoriaArticulo(e_CategoriaArticulo e_categoriaProducto)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Update_CategoriaArticulo");

            dbTse.AddInParameter(dbCommand, "@IdCategoriaArticulo", DbType.Int32, e_categoriaProducto.IdCategoriaArticulo);
            dbTse.AddInParameter(dbCommand, "@Nombre", DbType.String, e_categoriaProducto.Nombre);         
            var result = dbTse.ExecuteNonQuery(dbCommand);
        }
    }
}

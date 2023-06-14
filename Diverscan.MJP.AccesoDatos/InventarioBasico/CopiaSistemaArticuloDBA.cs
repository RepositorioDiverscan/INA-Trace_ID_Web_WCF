using Diverscan.MJP.Entidades.InventarioBasico;
using Diverscan.MJP.Entidades.Invertario;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.InventarioBasico
{
    public class CopiaSistemaArticuloDBA
    {
        //Método que retorna los artículos de un inventario
        public List<NombreIdArticuloRecord> ObtenerArticulosInventarioBasico(long idInventarioBasico)
        {
            //Instanciar la BD 
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerArticulosInventarioBasico");

            //Agregar los parámetros necesarios
            dbTse.AddInParameter(dbCommand, "@IdInventarioBasico", DbType.Int64, idInventarioBasico);

            //Crear una lista de la clase NombreIdArticuloRecord
            List<NombreIdArticuloRecord> records = new List<NombreIdArticuloRecord>();

            //Ejecutar el SP y leer los resultados
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                //Mientras se lee los datos, agregarlos a un nuevo objeto y agregarlos a la lista
                while (reader.Read())
                {
                    records.Add(new NombreIdArticuloRecord(reader));
                }
            }
            return records;
        }

        public List<ArticulosDisponibles> ObtenerArticulosCopiaSistema(long idInventario, long idArticulo)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerArticulosCopiaSistemaDetalle");
            dbTse.AddInParameter(dbCommand, "@IdInventarioBasico", DbType.Int64, idInventario);
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);

            List<ArticulosDisponibles> cantidadList = new List<ArticulosDisponibles>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    cantidadList.Add(new ArticulosDisponibles(reader));
                }
            }
            return cantidadList;
        }


        public List<ArticulosDisponibles> ObtenerTodosArticulosCopiaSistema(long idInventario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerTodosArticulosCopiaSistemaDetalle");
            dbTse.AddInParameter(dbCommand, "@IdInventarioBasico", DbType.Int64, idInventario);         

            List<ArticulosDisponibles> cantidadList = new List<ArticulosDisponibles>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    cantidadList.Add(new ArticulosDisponibles(reader));
                }
            }
            return cantidadList;
        }
    }
}

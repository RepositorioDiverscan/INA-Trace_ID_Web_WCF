using CodeUtilities.dbconnect;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Trazabilidad
{
    public class DATransitoCantidadEntrada
    {
        //Método para obtener la cantidad entrada de un artículo por bodega
        public int ObtenerCantidadTransitoEntrada(int IdArticulo, int idBodega)
        {
            //Instanciar la BD
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_TrazabilidadCantidadEntrada");
            
            //Agregar los parámetros necesarios
            db.AddInParameter(dbCommand, "@IdArticulo", DbType.Int32, IdArticulo);
            db.AddInParameter(dbCommand, "@IdBodega", DbType.Int32, idBodega);

            //Crear un arreglo de la clase ETrazabilidadCantidadEntrada
            List<ETrazabilidadCantidadEntrada> eTrazabilidadCantidadEntradas = new List<ETrazabilidadCantidadEntrada>();

            //Obtener la cantidad del SP y agregarlo a la trazabilidad
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ETrazabilidadCantidadEntrada datos = new ETrazabilidadCantidadEntrada();
                    datos.Cantidad = Convert.ToInt32(reader["Cantidad"].ToString());
                    eTrazabilidadCantidadEntradas.Add(datos);
                }
            }

            //Comprobar que la cantidad sea mayor a 0
            if (eTrazabilidadCantidadEntradas.Count > 0)
            {
                return eTrazabilidadCantidadEntradas[0].Cantidad;
            }

            return 0;
        }
    }
}

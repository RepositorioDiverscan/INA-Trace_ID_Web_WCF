using CodeUtilities.dbconnect;
using Diverscan.MJP.AccesoDatos.Articulos;
using Diverscan.MJP.AccesoDatos.Bodega;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.ModuloConsultas
{
   public class DAConsultas
    {

        public List<EProveedor> ObtenerProveedores()
        {

            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_ObtenerTodosProveedores");
            List<EProveedor> ListTodosProvee = new List<EProveedor>();
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    EProveedor datos = new EProveedor();
                    datos.Nombre = reader["Nombre"].ToString();
                    datos.IdProveedor = Convert.ToInt32(reader["IdProveedor"].ToString());
                    ListTodosProvee.Add(datos);
                }
            }


            return ListTodosProvee;
        }


        public List<EArticulo> ObtenerArticulos(int idProvedor)
        {

            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_Consulta_ProveedorXArticulo");
            db.AddInParameter(dbCommand, "@IdProveedor", DbType.Int32, idProvedor);
            List<EArticulo> ListTodosArt = new List<EArticulo>();
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    EArticulo datos = new EArticulo();
                    datos.Nombre = reader["Nombre"].ToString();
                    datos.IdArticulo = Convert.ToInt32(reader["IdArticulo"].ToString());
                    ListTodosArt.Add(datos);
                }
            }


            return ListTodosArt;
        }


        public List<EZona> ObtenerZonas(int idArticulo)
        {

            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_OBTENERZONASPORARTICULO");
            db.AddInParameter(dbCommand, "@p_IdArticulo", DbType.Int32, idArticulo);
            List<EZona> ListZonas = new List<EZona>();
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    EZona datos = new EZona();
                    datos.Nombre = reader["Nombre"].ToString();
                    datos.IdZona = Convert.ToInt32(reader["idZona"].ToString());
                    ListZonas.Add(datos);
                }
            }
            return ListZonas;
        }

        internal string ObtenerInventarioBodegaTradeBook(string idInternoProducto, string idInternoBodega)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("ObtenerInventarioBodegaTradeBook_VP");
            db.AddInParameter(dbCommand, "@idInternoArticulo", DbType.String, idInternoProducto);
            db.AddInParameter(dbCommand, "@idInternoBodega", DbType.String, idInternoBodega);
            string cantidadProducto = "0";
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {

                    cantidadProducto = reader["CantidadDisponible"].ToString();                   
                   
                }
            }
            return cantidadProducto;
        }

        public List<EArticulo> ObtenerArticulosXZonas(int idArticulo, DataTable Zonas, int idBodega)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("ObtenerCantidadArticuloZona");
            db.AddInParameter(dbCommand, "@idArticulo", DbType.Int32, idArticulo);
            db.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@zonasIdentificadores";
            parameter.SqlDbType = System.Data.SqlDbType.Structured;
            parameter.Value = Zonas;
            dbCommand.Parameters.Add(parameter); 

            List<EArticulo> ListArticulosZonas = new List<EArticulo>();
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    EArticulo datos = new EArticulo();
                    datos.IdInterno = reader["idInterno"].ToString();
                    datos.Nombre = reader["Nombre"].ToString();
                    datos.IdArticulo = Convert.ToInt32(reader["IdArticulo"].ToString());
                    datos.Cantidad = Convert.ToDouble(reader["CantidadDisponible"].ToString());
                    datos.Descripcion = reader["Descripcion"].ToString();
                    string lot = reader["Lote"].ToString();
                   
                    if (String.IsNullOrEmpty(lot))
                    {
                        datos.Lote = "NA";
                    }
                    else
                    {
                        datos.Lote = lot;
                    }

                                        
                    datos.FechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"].ToString());
                    datos.FechaAndroid =reader["FechaVencimiento"].ToString();
                    string fecha = reader["FUTSAlida"].ToString();
                    if (!String.IsNullOrEmpty(fecha))
                    {
                        datos.FUTSAlida = Convert.ToDateTime(reader["FUTSAlida"].ToString());
                    }                 
                    datos.ConTrazabilidad = Convert.ToBoolean(reader["ConTrazabilidad"].ToString());
                    ListArticulosZonas.Add(datos);
                }
            }
            return ListArticulosZonas;
        }

        //Método para obtener todas las Bodegas del Sistema
        public List<EBodega> GETBODEGAS()
        {
            //Instanciar la BD 
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_GETBODEGA");
            
            //Crear una lista de la clase EBodega
            List<EBodega> ListBodegas = new List<EBodega>();
            
            //Ejecutar el SP y leer los resultados
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                //Mientras se lee los datos, agregarlos a un nuevo objeto y agregarlos a la lista
                while (reader.Read())
                {
                    EBodega datos = new EBodega();
                    datos.Nombre = reader["nombre"].ToString();
                    datos.IdBodega = Convert.ToInt32(reader["idBodega"].ToString());
                    ListBodegas.Add(datos);
                }
            }

            //Retornar la lista
            return ListBodegas;
        }

    }
}

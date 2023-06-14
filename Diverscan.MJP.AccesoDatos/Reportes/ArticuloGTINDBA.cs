using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.MaestroArticulo;
using Diverscan.MJP.Entidades.Reportes.Articulos;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    public class ArticuloGTINDBA
    {
        public List<Entidades.e_Destino_Dev> ObtenerArticulos()
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_ArticulosGTIN");

            List<Entidades.e_Destino_Dev> articulosList = new List<Entidades.e_Destino_Dev>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {                    
                   int idArticulo =  int.Parse(reader["idArticulo"].ToString());;
                   string nombre = reader["Nombre"].ToString();
                   string    GTIN = reader["GTIN"].ToString();
                   articulosList.Add(new Entidades.e_Destino_Dev(idArticulo, nombre, GTIN));                    
                }
            }
            return articulosList;
        }


        public List<Entidades.e_Destino_Dev> ObtenerArticulosPorProvedor(long idProveedor)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_ArticulosGTIN_Por_Proveedor");
            dbTse.AddInParameter(dbCommand, "@IdProveedor", DbType.Int64, idProveedor);
            List<Entidades.e_Destino_Dev> articulosList = new List<Entidades.e_Destino_Dev>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    int idArticulo = int.Parse(reader["idArticulo"].ToString()); ;
                    string nombre = reader["Nombre"].ToString();
                    string GTIN = reader["GTIN"].ToString();
                    articulosList.Add(new Entidades.e_Destino_Dev(idArticulo, nombre, GTIN));
                }
            }
            return articulosList;
        }

        
        public List<e_ArticuloIdERP> ObtenerArticulosIdInternoERP()
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_Articulos_IdInternoERP");

            List<e_ArticuloIdERP> articulosList = new List<e_ArticuloIdERP>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string IdArticuloERP = reader["IdArticuloERP"].ToString();
                    string NombreArticulo = reader["NombreArticulo"].ToString(); 
                    string NombreArticuloIDInterno = reader["NombreArticuloIDInterno"].ToString();
                    int CantidadArticulosPorIdInterno = int.Parse(reader["CantidadArticulosPorIdInterno"].ToString());
                    articulosList.Add(new e_ArticuloIdERP(IdArticuloERP, NombreArticulo, NombreArticuloIDInterno, CantidadArticulosPorIdInterno));
                }
            }
            return articulosList;
        }

        public List<e_ArticuloIdERP> ObtenerArticulosIdInternoERPPorProveedor(long idProveedor)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_Articulos_IdInternoERP_Por_Proveedor");
            dbTse.AddInParameter(dbCommand, "@IdProveedor", DbType.Int64, idProveedor);
            List<e_ArticuloIdERP> articulosList = new List<e_ArticuloIdERP>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string IdArticuloERP = reader["IdArticuloERP"].ToString();
                    string NombreArticulo = reader["NombreArticulo"].ToString();
                    string NombreArticuloIDInterno = reader["NombreArticuloIDInterno"].ToString();
                    int CantidadArticulosPorIdInterno = int.Parse(reader["CantidadArticulosPorIdInterno"].ToString());
                    articulosList.Add(new e_ArticuloIdERP(IdArticuloERP, NombreArticulo, NombreArticuloIDInterno, CantidadArticulosPorIdInterno));
                }
            }
            return articulosList;
        }


        public List<EArticulo> ObtenerArticulosYGTINReport()
        {

            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_GET_ALLPRODUCTSANDGTIN");
            List<EArticulo> ListTodosArt = new List<EArticulo>();
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    EArticulo datos = new EArticulo();
                    datos.IdInterno = Convert.ToInt32(reader["idInterno"].ToString());
                    datos.Nombre = reader["Nombre"].ToString();
                    datos.GTIN13 = reader["GTIN13"].ToString();
                    datos.GTIN14 = reader["GTIN14"].ToString();
                    datos.Descripcion = reader["Descripcion"].ToString();
                    datos.Contenido = Convert.ToDouble(reader["Contenido"].ToString());
                    ListTodosArt.Add(datos);
                }
            }
            return ListTodosArt;
        }

        //public List<e_MaestroArticulo> GetProductsReportStorage()
        //{
        //    List<e_MaestroArticulo> productList = new List<e_MaestroArticulo>();
        //    var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
        //    var dbCommand = dbTse.GetStoredProcCommand("SP_GetProductsReportWarehouse");

          

        //    using (IDataReader reader = dbTse.ExecuteReader(dbCommand))
        //    {
        //        while (reader.Read())
        //        {
        //            e_MaestroArticulo product = new e_MaestroArticulo();
        //            product.IdArticulo = Convert.ToInt32(reader["idArticulo"].ToString());
        //            product.IdInterno = reader["idInterno"].ToString();
        //            product.Nombre = reader["Nombre"].ToString();
        //            product.GTIN = reader["GTIN"].ToString();                   
        //            product.MinPicking = Convert.ToDecimal(reader["MinPicking"].ToString());
        //            productList.Add(product);
        //        }
        //    }

        //    return productList;
        //}

    }
}



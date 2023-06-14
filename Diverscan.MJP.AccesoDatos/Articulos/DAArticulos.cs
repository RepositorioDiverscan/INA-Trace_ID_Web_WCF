using CodeUtilities.dbconnect;
using Diverscan.MJP.AccesoDatos.Articulos;
using Diverscan.MJP.Entidades.MaestroArticulo;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Articulos
{
    public class DAArticulos
    {
        public EArticuloId ObtenerIdInterno_Articulo(string IdInterno)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("ObtenerIdArticulo");
            db.AddInParameter(dbCommand, "@idInterno", DbType.Int32, IdInterno);
            //SqlParameter parameter = new SqlParameter();

            EArticuloId earticuloId = new EArticuloId();

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    earticuloId.IdArticulo = Convert.ToInt32(reader["idArticulo"].ToString());
                    earticuloId.Nombre = reader["Nombre"].ToString();                   
                }
            }
            return earticuloId;
        }

        public EArticuloId ObtenerNombreArticulo(string IdInterno)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("ObtenerIdArticulo");
            db.AddInParameter(dbCommand, "@idInterno", DbType.Int32, IdInterno);

            EArticuloId articulo = new EArticuloId();

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    //EArticuloId datos = new EArticuloId();
                    articulo.Nombre = reader["Nombre"].ToString();
                    //ListIdInterno.Add(datos);
                }
            }
            return articulo;
        }

        public e_MaestroArticulo ObtenerArticulo(string IdInterno)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("ObtenerArticuloXIdInterno_VP");
            db.AddInParameter(dbCommand, "@idInternoArticulo", DbType.Int32, IdInterno);

            e_MaestroArticulo articulo = new e_MaestroArticulo();

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    articulo.IdArticulo = int.Parse(reader["idArticulo"].ToString());                  
                    articulo.Nombre = reader["Nombre"].ToString();
                    articulo.NombreHH = reader["NombreHH"].ToString();
                    articulo.GTIN = reader["GTIN"].ToString();
                    articulo.UnidadesMedidaNombre = reader["idUnidadMedida"].ToString();
                    articulo.TiposEmpaqueNombre = reader["idTipoEmpaque"].ToString();
                    articulo.Granel = bool.Parse(reader["Granel"].ToString());                                      
                    articulo.IdInterno = reader["idInterno"].ToString();
                    articulo.Contenido = decimal.Parse(reader["Contenido"].ToString());
                    articulo.Unidad_Medida = reader["Unidad_Medida"].ToString();
                    articulo.Trazable = bool.Parse(reader["ConTrazabilidad"].ToString());
                }
            }
            return articulo;
        }
    }
}

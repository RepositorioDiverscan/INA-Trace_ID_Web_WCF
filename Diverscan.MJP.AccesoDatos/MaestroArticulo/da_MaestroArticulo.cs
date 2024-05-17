using Diverscan.MJP.Entidades.GTIN14VariableLogistic;
using Diverscan.MJP.Entidades.MaestroArticulo;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.MaestroArticulo
{
    public class da_MaestroArticulo
    {
        public List<e_MaestroArticulo> GetListMaestroArticulo(string prefix, string IdCompania)
        {
            List<e_MaestroArticulo> ListMaestroArticulo = new List<e_MaestroArticulo>();
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select * from Vista_MaestroArticulos where ddlidCompania = @IdCompania AND txtidArticulo like + '%' + @SearchText + '%' or txtNombre like + '%' + @SearchText + '%' or txtGTIN like + '%' + @SearchText + '%' or ddlidBodega like + '%' + @SearchText + '%' or ddlidFamilia like + '%' + @SearchText + '%' or txtidInterno like + '%' + @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@IdCompania", IdCompania);
                        cmd.Parameters.AddWithValue("@SearchText", prefix);
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                long idArticulo = int.Parse(reader["txtidArticulo"].ToString());
                                string idCompania = reader["ddlidCompania"].ToString();
                                string Nombre = reader["txtNombre"].ToString();
                                string NombreHH = reader["txtNombreHH"].ToString();
                                string GTIN = reader["txtGTIN"].ToString();
                                string UnidadesMedidaNombre = reader["ddlidUnidadMedida"].ToString();
                                string TiposEmpaqueNombre = reader["ddlidTipoEmpaque"].ToString();
                                string BodegaNombre = reader["ddlidBodega"].ToString();
                                string FamiliaNombre = reader["ddlidFamilia"].ToString();
                                bool Granel = bool.Parse(reader["chkGranel"].ToString());
                                decimal TemperaturaMaxima = decimal.Parse(reader["txtTemperaturaMaxima"].ToString());
                                decimal TemperaturaMinima = decimal.Parse(reader["txtTemperaturaMinima"].ToString());
                                int DiasMinimosVencimiento = int.Parse(reader["txtDiasMinimosVencimiento"].ToString());
                                string idInterno = reader["txtidInterno"].ToString();
                                decimal Contenido = decimal.Parse(reader["txtContenido"].ToString());
                                string Unidad_Medida = reader["txtUnidadMedida"].ToString();
                                int DiasMinimosVencimientoRestaurantes = int.Parse(reader["txtDiasMinimosVencimientoRestaurantes"].ToString());

                                ListMaestroArticulo.Add(new e_MaestroArticulo(idArticulo, idCompania, Nombre, NombreHH, GTIN, UnidadesMedidaNombre, TiposEmpaqueNombre, BodegaNombre, FamiliaNombre, Granel, TemperaturaMaxima, TemperaturaMinima, DiasMinimosVencimiento, idInterno, Contenido, Unidad_Medida, DiasMinimosVencimientoRestaurantes));
                            }
                        }
                        conn.Close();
                    }
                }
                return ListMaestroArticulo;
            }
            catch (Exception)
            {
                return ListMaestroArticulo;
            }
        }

        public List<e_MaestroArticulo> GetMaestroArticulo(string IdCompania)
        {
            List<e_MaestroArticulo> ListeMaestroArticulo = new List<e_MaestroArticulo>();
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select * from Vista_MaestroArticulos where ddlidCompania = @IdCompania ";
                        cmd.Parameters.AddWithValue("@IdCompania", IdCompania);

                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                long idArticulo = int.Parse(reader["txtidArticulo"].ToString());
                                string idCompania = reader["ddlidCompania"].ToString();
                                string Nombre = reader["txtNombre"].ToString();
                                string NombreHH = reader["txtNombreHH"].ToString();
                                string GTIN = reader["txtGTIN"].ToString();
                                string UnidadesMedidaNombre = reader["ddlidUnidadMedida"].ToString();
                                string TiposEmpaqueNombre = reader["ddlidTipoEmpaque"].ToString();
                                string BodegaNombre = reader["ddlidBodega"].ToString();
                                string FamiliaNombre = reader["ddlidFamilia"].ToString();
                                bool Granel = bool.Parse(reader["chkGranel"].ToString());
                                decimal TemperaturaMaxima = decimal.Parse(reader["txtTemperaturaMaxima"].ToString());
                                decimal TemperaturaMinima = decimal.Parse(reader["txtTemperaturaMinima"].ToString());
                                int DiasMinimosVencimiento = int.Parse(reader["txtDiasMinimosVencimiento"].ToString());
                                string idInterno = reader["txtidInterno"].ToString();
                                decimal Contenido = decimal.Parse(reader["txtContenido"].ToString());
                                string Unidad_Medida = reader["txtUnidadMedida"].ToString();
                                int DiasMinimosVencimientoRestaurantes = int.Parse(reader["txtDiasMinimosVencimientoRestaurantes"].ToString());

                                ListeMaestroArticulo.Add(new e_MaestroArticulo(idArticulo, idCompania, Nombre, NombreHH, GTIN, UnidadesMedidaNombre, TiposEmpaqueNombre, BodegaNombre, FamiliaNombre, Granel, TemperaturaMaxima, TemperaturaMinima, DiasMinimosVencimiento, idInterno, Contenido, Unidad_Medida, DiasMinimosVencimientoRestaurantes));
                            }
                        }
                        conn.Close();
                    }
                }
                return ListeMaestroArticulo;
            }
            catch (Exception)
            {
                return ListeMaestroArticulo;
            }
        }

        public List<e_ArticuloSinUnidadAlistoDefecto> GetArticuloSinUnidadAlistoDefecto()
        {
            List<e_ArticuloSinUnidadAlistoDefecto> listaDatos = new List<e_ArticuloSinUnidadAlistoDefecto>();
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_Articulo_Sin_Unidad_Alisto_Defecto");
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        string idERP = reader["IdERP"].ToString();
                        long idArticulo = (long)Convert.ToDecimal(reader["IdArticulo"].ToString());
                        string nombreMaestro = reader["NombreMaestro"].ToString();
                        string GTIN13 = reader["GTIN13"].ToString();
                        string GTIN13Activo = reader["GTIN13Activo"].ToString();
                        string empaqueMaestro = reader["EmpaqueMaestro"].ToString();
                        string GTIN14 = reader["GTIN14"].ToString();
                        string GTIN14Activo = reader["GTIN14Activo"].ToString();
                        string empaque = reader["Empaque"].ToString();

                        listaDatos.Add(new e_ArticuloSinUnidadAlistoDefecto
                        (
                            idERP,
                            idArticulo,
                            nombreMaestro,
                            GTIN13,
                            GTIN13Activo,
                            empaqueMaestro,
                            GTIN14,
                            GTIN14Activo,
                            empaque
                        ));
                    }
                }
            }
            catch (Exception)
            {
               //throw ex;
            }
            return listaDatos;
        }

        public string GeneraGTIN()
        {
            string resultado = "0";
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_GeneraGTIN");
                var resul = dbTse.ExecuteReader(dbCommand);
                resul.Read();
                resultado = resul["GTIN"].ToString().Trim();

                return resultado;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string GeneraGTIN14(string GTIN13)
        {
            string resultado = "0";
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_GeneraGTIN14");
                dbTse.AddInParameter(dbCommand, "@GTIN13", DbType.String, GTIN13);

                var resul = dbTse.ExecuteReader(dbCommand);
                resul.Read();
                resultado = resul["GTIN"].ToString().Trim();

                return resultado;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }      
        
        public string GETABRZONE(int ID)
        {
            string resultado = "0";
            
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_GETINFOZON");
                dbTse.AddInParameter(dbCommand, "@P_IDZONA", DbType.Int32, ID);

                var resul = dbTse.ExecuteReader(dbCommand);
                resul.Read();
                resultado = resul["Abreviatura"].ToString().Trim();

                return resultado;
            
        }

        public string GETABRBOD(int ID)
        {
            string resultado = "0";

            if (!(ID == 0))
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_GETINFOBOD");
                dbTse.AddInParameter(dbCommand, "@P_IDBOD", DbType.Int32, ID);

                var resul = dbTse.ExecuteReader(dbCommand);
                resul.Read();
                resultado = resul["Abreviatura"].ToString().Trim();
            }
            return resultado;
        }

        public List<EMinPicking> GetMinPicking(int IdBodega)
        {

            List<EMinPicking> ListMinPick = new List<EMinPicking>();
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerMinimoPickingPorBodega");
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, IdBodega);

            using (IDataReader reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ListMinPick.Add(new EMinPicking(reader));

                }

                return ListMinPick;
            }

        }


    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Diverscan.MJP.Entidades.Reportes.Kardex;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    public class da_kardexSKU
    {
        public static bool ValidaIdInterno(string IdInterno)
        {
            bool valida;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MJPConnectionString"].ToString()))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SP_ValidaIdInterno", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdInterno", IdInterno);

                    SqlParameter outputParameter = new SqlParameter("@Res", SqlDbType.Bit);
                    outputParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputParameter);

                    try
                    {
                        command.ExecuteNonQuery();

                        valida = Convert.ToBoolean(outputParameter.Value);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Dispose();
                    }
                }
            }

            return valida;

        }

        public List<e_kardexSKU> ObtenerKardex(int idBodega, string Sku, string Lote, bool Transitos, DateTime f1, DateTime f2)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerKardexSKUDetalleTra");
            dbTse.AddInParameter(dbCommand, "@BodegaId", DbType.Int32, idBodega);
            dbTse.AddInParameter(dbCommand, "@Sku", DbType.String, Sku);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, Lote);
            dbTse.AddInParameter(dbCommand, "@Transitos", DbType.Boolean, Transitos);
            dbTse.AddInParameter(dbCommand, "@Inicio", DbType.DateTime, f1);
            dbTse.AddInParameter(dbCommand, "@Final", DbType.DateTime, f2);

            //Guardar en una lista
            List<e_kardexSKU> listaKardex = new List<e_kardexSKU>();

            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        listaKardex.Add(new e_kardexSKU(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaKardex;
        }
    }
}

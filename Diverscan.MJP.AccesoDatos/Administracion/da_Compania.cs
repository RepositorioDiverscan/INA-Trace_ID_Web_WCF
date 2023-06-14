using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.Administracion
{
    public class da_Compania
    {
        public static List<e_Compania> GetCompanias(string sNombreBaseDatos, string sUsuario)
        {
            try
            {
                Database dbMjp = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                DbCommand dbCommand = dbMjp.GetStoredProcCommand("sp_GetCompanias"); //

                var eCompanias = new List<e_Compania>();

                using (IDataReader reader = dbMjp.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        eCompanias.Add(CargarCompanias(reader));
                    }
                }

                return eCompanias;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "sp_GetCompanias";
                string listaParametros = "";
                int indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0)
                    indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal);
                // Si no existe, está en inglés
                string lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Compania", "GetCompanias()", sUsuario, lineaError,
                                             nombreProcedimiento, listaParametros, exError.Message);

                return null;
            }
        }

        private static e_Compania CargarCompanias(IDataReader reader)
        {
            var eCompania = new e_Compania();

            eCompania.IdCompania = reader["IdCompania"].ToString();
            eCompania.Nombre = reader["Nombre"].ToString();
            eCompania.Descripcion = reader["Descripcion"].ToString();
            eCompania.Codigo = reader["Codigo"].ToString();

            return eCompania;
        }
    }
}

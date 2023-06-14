using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.Programa
{
    public class da_Programa
    {
        #region "Mostrar"
        /// <summary>
        /// Autor:Cristian
        /// Description: Retorna id,nombre corto y un concatenado del nombre corto y la descripcion.
        /// </summary>
        /// <param name="sNombreBaseDatos"></param>
        /// <param name="sUsuario"></param>
        /// <returns></returns>
        public static List<e_Programa> GetTodosProgramas(string sNombreBaseDatos, string sUsuario)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbTse.GetStoredProcCommand("GetProgramas");//

                var eProgramas = new List<e_Programa>();

                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        eProgramas.Add(CargarProgramas(reader));
                    }
                }

                return eProgramas;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "GetProgramas";
                const string listaParametros = "";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0)
                    indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal);
                // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Programa.cs", "GetProgramas()", sUsuario, lineaError,
                                             nombreProcedimiento, listaParametros, exError.Message);

                return null;
            }

        }

        /// <summary>
        /// Autor:Cristian
        /// Description: Retorna id,nombre corto y un concatenado del nombre corto y la descripcion.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static e_Programa CargarProgramas(IDataReader reader)
        {
            var eProgramas = new e_Programa();

            eProgramas.IdPrograma = int.Parse(reader["IdPrograma"].ToString());
            eProgramas.NombreCorto = reader["NombreCorto"].ToString();
            eProgramas.Descripcion = reader["NombreCorto"].ToString() + "-" + reader["Descripcion"].ToString();

            return eProgramas;
        }

        #endregion

        #region "Insertar"

        public static bool InsertarPrograma(string sNombreBaseDatos, string sUsuario, e_Programa ePrograma)
        {
            try
            {
                var dbMjp = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbMjp.GetStoredProcCommand("InsertarPrograma");//

                dbMjp.AddInParameter(dbCommand, "@NombreCorto", DbType.String, ePrograma.NombreCorto);
                dbMjp.AddInParameter(dbCommand, "@Descripcion", DbType.String, ePrograma.Descripcion);

                dbMjp.ExecuteNonQuery(dbCommand);
                return true;

            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "InsertarPrograma";
                const string listaParametros = "";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0)
                    indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal);
                        // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Programa", "InsertarPrograma()", sUsuario, lineaError,
                                             nombreProcedimiento, listaParametros, exError.Message);

                return false;
            }

        }

        #endregion

        #region "Selects"

        public static List<e_Programa> GetProgramas(string sNombreBaseDatos, string sUsuario)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbTse.GetStoredProcCommand("GetProgramas");//

                var eProgramas = new List<e_Programa>();

                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        eProgramas.Add(CargarProgramasBusqueda(reader));
                    }
                }

                return eProgramas;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "GetProgramas";
                const string listaParametros = "";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0)
                    indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal);
                        // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Programa.cs", "GetProgramas()", sUsuario, lineaError,
                                             nombreProcedimiento, listaParametros, exError.Message);

                return null;
            }

        }

        public static e_Programa GetProgramaPorID(string sNombreBaseDatos, string sUsuario, int IdPrograma)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbTse.GetStoredProcCommand("GetProgramaInforXId");
                dbTse.AddInParameter(dbCommand, "@IdPrograma", DbType.Int16, IdPrograma);

                var ePrograma = new e_Programa();

                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                       // ePrograma.IdPrograma = int.Parse(reader["IdPrograma"].ToString());
                        ePrograma.NombreCorto = reader["NombreCorto"].ToString();
                        ePrograma.Descripcion = reader["Descripcion"].ToString();
                    }
                }

                return ePrograma;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "GetProgramaInforXId";
                const string listaParametros = "";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0)
                    indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal);
                        // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Programa.cs", "GetProgramaInforXId()", sUsuario, lineaError,
                                             nombreProcedimiento, listaParametros, exError.Message);

                return null;
            }

        }

        

        /// <summary>
        /// Berny
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static e_Programa CargarProgramasBusqueda(IDataReader reader)
        {
            var eProgramas = new e_Programa();

            eProgramas.IdPrograma = int.Parse(reader["IdPrograma"].ToString());
            eProgramas.NombreCorto = reader["NombreCorto"].ToString();
            eProgramas.Descripcion = reader["Descripcion"].ToString();

            return eProgramas;
        }

        public static List<e_Programa> GetProgramasPorCriterio(string sNombreBaseDatos, string sUsuario, string criterio)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbTse.GetStoredProcCommand("GetProgramasPorCriterio");//
                dbTse.AddInParameter(dbCommand, "@Criterio", DbType.String, criterio);

                var eProgramas = new List<e_Programa>();

                using (IDataReader reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        eProgramas.Add(CargarProgramasBusqueda(reader));
                    }
                }

                return eProgramas;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "GetProgramasPorCriterio";
                const string listaParametros = "";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0)
                    indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal);
                        // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Programa.cs", "GetProgramasPorCriterio()", sUsuario, lineaError,
                                             nombreProcedimiento, listaParametros, exError.Message);

                return null;
            }

        }

        public static e_Programa GetProgramasPorNombre(string sNombreBaseDatos, string sUsuario, string criterio)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbTse.GetStoredProcCommand("GetProgramasPorNombre");//
                dbTse.AddInParameter(dbCommand, "@Nombre", DbType.String, criterio);

                var eProgramas = new e_Programa();

                using (IDataReader reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        eProgramas = CargarProgramasBusqueda(reader);
                    }
                }

                return eProgramas;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "GetProgramasPorNombre";
                const string listaParametros = "";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0)
                    indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal);
                // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Programa.cs", "GetProgramasPorNombre()", sUsuario, lineaError,
                                             nombreProcedimiento, listaParametros, exError.Message);

                return null;
            }

        }

        #endregion

        #region "Update"

        public static bool EditarPrograma(string sNombreBaseDatos, string sUsuario, e_Programa ePrograma)
        {
            try
            {
                var dbMjp = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbMjp.GetStoredProcCommand("UpdPrograma");//

                dbMjp.AddInParameter(dbCommand, "@NombreCorto", DbType.String, ePrograma.NombreCorto);
                dbMjp.AddInParameter(dbCommand, "@Descripcion", DbType.String, ePrograma.Descripcion);
                dbMjp.AddInParameter(dbCommand, "@IdPrograma", DbType.Int16, ePrograma.IdPrograma);

                dbMjp.ExecuteNonQuery(dbCommand);
                return true;

            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "UpdPrograma";
                const string listaParametros = "";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0)
                    indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal);
                        // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Programa", "EditarPrograma()", sUsuario, lineaError,
                                             nombreProcedimiento, listaParametros, exError.Message);

                return false;
            }

        }

        #endregion

        #region "Delete"

        public static bool EliminarPrograma(string sNombreBaseDatos, string sUsuario, int IdPrograma)
        {
            try
            {
                var dbMjp = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbMjp.GetStoredProcCommand("EliminarPrograma");//
                dbMjp.AddInParameter(dbCommand, "@IdPrograma", DbType.Int16, IdPrograma);

                dbMjp.ExecuteNonQuery(dbCommand);
                return true;

            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "EliminarPrograma";
                const string listaParametros = "";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0)
                    indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal);
                        // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Programa", "EliminarPrograma()", sUsuario, lineaError,
                                             nombreProcedimiento, listaParametros, exError.Message);

                return false;
            }

        }

        #endregion

        #region "Verificar"
        public static string VerificarIdPrograma(string sNombreBaseDatos, string sUsuario, string programa)
        {
            var idProg = "";
            try
            {
                var dbMjp = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbMjp.GetStoredProcCommand("VerificarIdPrograma");//

                dbMjp.AddInParameter(dbCommand, "@NomCorto", DbType.String, programa);

                idProg = dbMjp.ExecuteScalar(dbCommand).ToString();
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "VerificarIdPrograma";
                var listaParametros = "nomCorto = " + programa;

                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0)
                    indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal);
                        // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Programa.cs", "VerificarIdPrograma", sUsuario, lineaError,nombreProcedimiento, listaParametros, exError.Message);

                idProg = "0";
            }
            return idProg;
        }

        #endregion
    }
}

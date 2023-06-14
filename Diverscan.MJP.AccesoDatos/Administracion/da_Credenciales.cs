using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.Administracion
{
    public class da_Credenciales
    {

        public static e_Usuario GetUsuarioLogin(string sNombreBaseDatos, string sUsuario)
        {
            bool Sinconexion = false;
            try
            {                
                Database dbTRACEID = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                DbCommand NombreStoreProcedure = dbTRACEID.GetStoredProcCommand("GetUsuarioLogin");//

                dbTRACEID.AddInParameter(NombreStoreProcedure, "@Usuario", DbType.String, sUsuario);

                e_Usuario eUsuarios = null;

                using (var reader = dbTRACEID.ExecuteReader(NombreStoreProcedure))
                {
                    while (reader.Read())
                    {
                        eUsuarios = CargarUsuarioLogin(reader, sNombreBaseDatos);
                    }
                }

                return eUsuarios;
            }
            catch (Exception exError)
            {
                if (exError.Message.Contains("error: 26"))
                    Sinconexion = true;
                
                var clLog = new clErrores();
                const string nombreProcedimiento = "GetUsuarioLogin";
                var listaParametros = sUsuario;
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_ModAdministracionLogin.cs", "GetUsuarioLogin()", sUsuario, lineaError, nombreProcedimiento, listaParametros, exError.Message);

                if (Sinconexion == true)
                {
                    var eUsuarios = new e_Usuario();
                    eUsuarios.IdUsuario = "SIN CONEXION";
                    eUsuarios.Usuario = "SIN CONEXION";
                    eUsuarios.Contrasenna = "SIN CONEXION";
                    eUsuarios.Bloqueado = false;
                    eUsuarios.Nombre = "SIN CONEXION";
                    eUsuarios.Apellido = "SIN CONEXION";
                    eUsuarios.idCompania = "SIN CONEXION";
                    eUsuarios.IdRoles = "0";
                    return eUsuarios;
                }
                else
                    return null;
            }
        }

        private static e_Usuario CargarUsuarioLogin(IDataReader reader , String sNombreBaseDatos)
        {
            try
            {
                var eUsuarios = new e_Usuario();
                eUsuarios.IdUsuario = reader["IDUSUARIO"].ToString();
                eUsuarios.Usuario = reader["USUARIO"].ToString();
                eUsuarios.Contrasenna = reader["CONTRASENNA"].ToString();
                eUsuarios.Bloqueado = Convert.ToBoolean(reader["ESTA_BLOQUEADO"]);
                eUsuarios.Nombre = reader["NOMBRE_PILA"].ToString();
                eUsuarios.Apellido = reader["APELLIDOS_PILA"].ToString();
                eUsuarios.idCompania = reader["IDCOMPANIA"].ToString();
                eUsuarios.IdRoles = reader["IDROL"].ToString();
                eUsuarios.IdBodega = Convert.ToInt32(reader["IdBodega"].ToString());
                bool trazableBodega = false;
                Boolean.TryParse(reader["trazable"].ToString(), out trazableBodega);
                eUsuarios.TrazableBodega = trazableBodega;
                return eUsuarios;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Credenciales.cs", "GetUsuarioLogin()", "", lineaError, nombreProcedimiento, "" , exError.Message);

                return null;
            }
        }

        protected static e_Programa CargarProgramaPorUsarioID(IDataReader reader)
        {
            var ePrograma = new e_Programa();

            ePrograma.NombreCorto = reader["NombreCorto"].ToString();
            ePrograma.Descripcion = reader["Descripcion"].ToString();
            ePrograma.IdPrograma = Convert.ToInt32(reader["IdPrograma"].ToString());
            return ePrograma;
        }

        public static string GetVersionSistemaHH()
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SPObtenerVersionActualSistemaHH");            
            dbTse.AddOutParameter(dbCommand, "@PVersionSistemaOUT", DbType.String, 200);            
            try
            {
                dbTse.ExecuteNonQuery(dbCommand);
                return dbTse.GetParameterValue(dbCommand, "@PVersionSistemaOUT").ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
                //throw ex;         
            }            
        }        
    }
}

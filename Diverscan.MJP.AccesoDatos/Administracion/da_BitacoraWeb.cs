using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.Administracion
{
    public class da_BitacoraWeb
    {
        public static bool InsertarBitacoraWeb(string sNombreBaseDatos, string sUsuario, e_BitacoraWeb eBitacora)
        {
            try
            {
                var dbMjp = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbMjp.GetStoredProcCommand("InsertarBitacoraWeb");//

                dbMjp.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, eBitacora.idUsuario);
                dbMjp.AddInParameter(dbCommand, "@Accion", DbType.String,eBitacora.Accion);

                dbMjp.ExecuteNonQuery(dbCommand);
                return true;

            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "InsertarBitacoraWeb";
                string listaParametros =
                    eBitacora.idUsuario + " | " + eBitacora.Accion;
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_BitacoraWeb", "InsertarBitacoraWeb()", sUsuario, lineaError, nombreProcedimiento, listaParametros, exError.Message);

                return false;
            }
        }
    }
}

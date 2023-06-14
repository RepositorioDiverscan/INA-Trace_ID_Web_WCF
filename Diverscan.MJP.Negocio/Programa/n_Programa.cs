using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.AccesoDatos.Programa;

namespace Diverscan.MJP.Negocio.Programa
{
    public class n_Programa
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
            return da_Programa.GetTodosProgramas(sNombreBaseDatos, sUsuario);
        }

        #endregion

        #region "Insertar"
        public static bool InsertarPrograma(string sNombreBaseDatos, string sUsuario, e_Programa ePrograma)
        {
            return da_Programa.InsertarPrograma(sNombreBaseDatos, sUsuario, ePrograma);
        }
        #endregion

        #region "Selects"
        public static List<e_Programa> GetProgramas(string sNombreBaseDatos, string sUsuario)
        {
            return da_Programa.GetProgramas(sNombreBaseDatos, sUsuario);
        }

        public static List<e_Programa> GetProgramasPorCriterio(string sNombreBasesDatos, string sUsuario, string criterio)
        {
            return da_Programa.GetProgramasPorCriterio(sNombreBasesDatos, sUsuario, criterio);
        }

        public static e_Programa GetProgramaPorID(string sNombreBasesDatos, string sUsuario, int IdPrograma)
        {
            return da_Programa.GetProgramaPorID(sNombreBasesDatos, sUsuario, IdPrograma);
        }

        public static e_Programa GetProgramasPorNombre(string sNombreBaseDatos, string sUsuario, string nombre)
        {
            return da_Programa.GetProgramasPorNombre(sNombreBaseDatos, sUsuario, nombre);
        }
        #endregion

        #region "Update"
        public static bool EditarPrograma(string sNombreBaseDatos, string sUsuario, e_Programa ePrograma)
        {
            return da_Programa.EditarPrograma(sNombreBaseDatos, sUsuario, ePrograma);
        }
        #endregion

        #region "Delete"
        public static bool EliminarPrograma(string sNombreBaseDatos, string sUsuario, int IdPrograma)
        {
            return da_Programa.EliminarPrograma(sNombreBaseDatos, sUsuario, IdPrograma);
        }
        #endregion

        #region "Verificar"
        public static string VerificarIdPrograma(string sNombreBaseDatos, string sUsuario, string programa)
        {
            return da_Programa.VerificarIdPrograma(sNombreBaseDatos, sUsuario, programa);
        }
        #endregion
    }
}

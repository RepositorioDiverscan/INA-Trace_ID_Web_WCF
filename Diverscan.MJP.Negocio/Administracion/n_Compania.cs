using System;
using System.Collections.Generic;
using Diverscan.MJP.AccesoDatos.Administracion;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;


namespace Diverscan.MJP.Negocio.Administracion
{
    public class n_Compania
    {
        #region Consultar

        public static List<e_Compania> GetCompanias(string sNombreBaseDatos, string sUsuario)
        {
            return da_Compania.GetCompanias(sNombreBaseDatos, sUsuario);
        }

        #endregion // Consultar

    }
}

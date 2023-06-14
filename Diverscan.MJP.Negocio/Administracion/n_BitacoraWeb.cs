using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.AccesoDatos.Administracion;

namespace Diverscan.MJP.Negocio.Administracion
{
    public class n_BitacoraWeb
    {
        public static bool InsertarBitacoraWeb(string sNombreBaseDatos, string sUsuario,e_BitacoraWeb eBitacora)
        {
            return da_BitacoraWeb.InsertarBitacoraWeb(sNombreBaseDatos, sUsuario, eBitacora);
        }
    }
}

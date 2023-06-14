using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.RolUsuarioHH
{
    public class NRolUsuarioHH
    {
        private FileExceptionWriter _fileExceptionWriter = new FileExceptionWriter();

        public List<ERolUsuarioHH> ObtenerRolesUsuariosHH(int IdRol)
        {
            try
            {
                DRolUsuarioHH dRolUsuarioHH = new DRolUsuarioHH();
                return dRolUsuarioHH.ObtenerUsuariosHH(IdRol);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.ROLUSERSFILEPATHEXCEPTION);
                return null;
            }
        }
    }
}

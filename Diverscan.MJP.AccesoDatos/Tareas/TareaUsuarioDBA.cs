using Diverscan.MJP.Entidades.Tareas;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Tareas
{
    public class TareaUsuarioDBA
    {

        public void AgregarTareaUsuario(TareaUsuarioRecord tareaUsuario)
        {
            var restulado = -7;
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_EnviaDatosTarea_VD");
                dbTse.AddInParameter(dbCommand, "@PidLineaDetalleSolicitud", DbType.Int64, tareaUsuario.IdDetalleSolicitud);
                dbTse.AddInParameter(dbCommand, "@PidBodega", DbType.Int64, tareaUsuario.IdBodega);
                dbTse.AddInParameter(dbCommand, "@PidUsuario", DbType.Int32, tareaUsuario.IdUsuario);

                dbCommand.CommandTimeout = 3600;
                restulado = dbTse.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
        }
        /*public void  AgregarTareaUsuario(TareaUsuarioRecord tareaUsuario)
        {
            var restulado = -7;
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_EnviaDatosTarea_VD");
                dbTse.AddInParameter(dbCommand, "@PidMaestroSolicitud", DbType.Int64, tareaUsuario.IdMaestroSolicitud);
                dbTse.AddInParameter(dbCommand, "@PidBodega", DbType.Int64, tareaUsuario.IdBodega);
                dbTse.AddInParameter(dbCommand, "@PidUsuario", DbType.Int32, tareaUsuario.IdUsuario);

                dbCommand.CommandTimeout = 3600;
                restulado = dbTse.ExecuteNonQuery(dbCommand);
            }
            catch(Exception ex)
            {
                var result = ex.Message;
            }
        }*/
    }
}

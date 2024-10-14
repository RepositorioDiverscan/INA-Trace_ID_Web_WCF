using Diverscan.MJP.Entidades.SyncActiceID;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SyncActiveID
{
    public class da_SyncActiveID
    {
        public eUbicacionActivo ObtenerUbicacionActual(string input)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString_ActiveID"); //Cadena de conexion de ActiveID

            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT Activos.shortDescription, Activos.companySysId, Edificio.name AS Edificio, Piso.name AS Piso, OFicina.name AS Oficina ");
            query.AppendLine("FROM assets AS Activos ");
            query.AppendLine("INNER JOIN tagRegistry AS Tag ON Activos.tagSysId = Tag.tagSysId ");
            query.AppendLine("INNER JOIN buildings AS Edificio ON Activos.buildingSysId = Edificio.buildingSysId ");
            query.AppendLine("INNER JOIN floors AS Piso ON Activos.floorSysId = Piso.floorSysId ");
            query.AppendLine("INNER JOIN officeses AS OFicina ON Activos.officeSysId = OFicina.officeSysId ");
            query.AppendLine("WHERE Activos.Placa = " + input);

            using (var dbCommand = dbTse.GetSqlStringCommand(query.ToString()))
            {
                dbCommand.CommandType = CommandType.Text;
                try
                {
                    using (var reader = dbTse.ExecuteReader(dbCommand))
                    {
                        if (reader.Read())
                        {
                            return new eUbicacionActivo(reader);
                        }
                        return null;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public string ObtenerUbicacionDefecto(string input)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString_ActiveID"); //Cadena de conexion de ActiveID

            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT T.defaultLocation FROM dbo.assets A ");
            query.AppendLine("INNER JOIN TraceID_defaultLocation T ");
            query.AppendLine("ON A.assetSysId = T.assetSysId ");
            query.AppendLine("WHERE A.Placa = " + input);

            using (var dbCommand = dbTse.GetSqlStringCommand(query.ToString()))
            {
                dbCommand.CommandType = CommandType.Text;
                try
                {
                    using (var reader = dbTse.ExecuteReader(dbCommand))
                    {
                        if (reader.Read())
                        {
                            return Convert.ToString(reader["defaultLocation"]);
                        }
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public bool ActualizaUbiDefecto(eUbicacionActivo ubicacion, string input)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("UPDATE assets ");
            query.AppendLine("SET buildingSysId = '" + ubicacion.BuildingSysId + "', ");
            query.AppendLine("floorSysId = '" + ubicacion.FloorSysId + "', ");
            query.AppendLine("officeSysId = '" + ubicacion.OfficeSysId + "' ");
            query.AppendLine("WHERE Placa = " + input);


            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString_ActiveID");

            using (var dbCommand = dbTse.GetSqlStringCommand(query.ToString()))
            {
                dbCommand.CommandType = CommandType.Text;
                try
                {
                    int affectedRows = dbTse.ExecuteNonQuery(dbCommand);
                    return affectedRows > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }
    }
}

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.Encargado
{
    public class DEncargado
    {
        public EEncargado ObtenerEncargados(int idBodega, string buscar)
        {
            EEncargado encargado = new EEncargado();

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("uspObtenerEncargado");
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);
            dbTse.AddInParameter(dbCommand, "@buscar", DbType.String, buscar);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    encargado = new EEncargado(reader);
                }
            }
            return encargado;
        }

        public List<EEncargado> ObtenerEncargadosXBodega(long idWarehouse)
        {
            List<EEncargado> encargados = new List<EEncargado>();

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_BuscarEncargadosXBodega");
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int64, idWarehouse);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    encargados.Add(new EEncargado(reader));
                }
            }
            return encargados;
        }

        public string IngresarEncargado(EEncargado encargado)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("IngresarEncargado");
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int64, encargado.IdBodega);
            dbTse.AddInParameter(dbCommand, "@nombre", DbType.String, encargado.Nombre);
            dbTse.AddInParameter(dbCommand, "@mail", DbType.String, encargado.Mail);
            dbTse.AddInParameter(dbCommand, "@comentarios", DbType.String, encargado.Comentarios);
            dbTse.AddInParameter(dbCommand, "@activo", DbType.Boolean, encargado.Activo);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    return reader["Resultado"].ToString();
                }
                else
                {
                    return  "El encargado no pudo ser insertado";
                }
            }
        }

        public string ActualizarEncargado(EEncargado encargado)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ActualizarEncargado");
            dbTse.AddInParameter(dbCommand, "@idEncargado", DbType.Int64, encargado.IdEncargado);
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int64, encargado.IdBodega);
            dbTse.AddInParameter(dbCommand, "@nombre", DbType.String, encargado.Nombre);
            dbTse.AddInParameter(dbCommand, "@mail", DbType.String, encargado.Mail);
            dbTse.AddInParameter(dbCommand, "@comentarios", DbType.String, encargado.Comentarios);
            dbTse.AddInParameter(dbCommand, "@activo", DbType.Boolean, encargado.Activo);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    return reader["Resultado"].ToString();
                }
                else
                {
                    return "El encargado no pudo ser actualizado";
                }
            }
        }
    }
}

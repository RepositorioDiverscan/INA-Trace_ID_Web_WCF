using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Transportista
{
    public class DTransportista
    {
        public string IngresarTransportista(ETransportista transportista) 
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("IngresarTransportista");
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int64, transportista.IdBodega);
            dbTse.AddInParameter(dbCommand, "@nombre", DbType.String, transportista.Nombre);
            dbTse.AddInParameter(dbCommand, "@mail", DbType.String, transportista.Mail);
            dbTse.AddInParameter(dbCommand, "@telefono", DbType.String, transportista.Telefono);
            dbTse.AddInParameter(dbCommand, "@comentarios", DbType.String, transportista.Comentarios);
            dbTse.AddInParameter(dbCommand, "@activo", DbType.Boolean, transportista.Activo);
            string result = "";

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    result = reader["Resultado"].ToString();
                }
            }
            return result;
        }

        public string ActualizarTransportista(ETransportista transportista)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ActualizarTransportista");
            dbTse.AddInParameter(dbCommand, "@idTransportista", DbType.Int64, transportista.IdTransportista);
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int64, transportista.IdBodega);
            dbTse.AddInParameter(dbCommand, "@nombre", DbType.String, transportista.Nombre);
            dbTse.AddInParameter(dbCommand, "@mail", DbType.String, transportista.Mail);
            dbTse.AddInParameter(dbCommand, "@telefono", DbType.String, transportista.Telefono);
            dbTse.AddInParameter(dbCommand, "@comentarios", DbType.String, transportista.Comentarios);
            dbTse.AddInParameter(dbCommand, "@activo", DbType.Boolean, transportista.Activo);
            string result = "";

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    result = reader["Resultado"].ToString();
                }
            }
            return result;
        }

        public List<ETransportista> BuscarTransportista(String buscar)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_BuscarTransportistas");
            dbTse.AddInParameter(dbCommand, "@idCompania", DbType.String,"AMCO");
            dbTse.AddInParameter(dbCommand, "@buscar", DbType.String, buscar);

            List<ETransportista> transportistas = new List<ETransportista>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    transportistas.Add( new ETransportista(reader));
                }
            }
            return transportistas;
        }

        public List<ETransportista> BuscarTransportistaXBodega(long idWarehouse)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_BuscarTransportistasXBodega");
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int64, idWarehouse);

            List<ETransportista> transportistas = new List<ETransportista>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    transportistas.Add(new ETransportista(reader));
                }
            }
            return transportistas;
        }
    }
}

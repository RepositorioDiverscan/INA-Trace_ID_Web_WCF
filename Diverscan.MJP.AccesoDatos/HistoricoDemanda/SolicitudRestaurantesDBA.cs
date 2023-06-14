using Diverscan.MJP.Entidades.HistoricoDemanda;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.HistoricoDemanda
{
    public class SolicitudRestaurantesDBA
    {
        //public List<SolicitudRestaurantesRecord> ObtenerSolicitudRestaurantes(DateTime fechaInicio, DateTime fechaFin ,long idArticulo)
        public List<SolicitudRestaurantesRecord> ObtenerSolicitudRestaurantes(DateTime fechaSeleccionada, string idInterno)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            //var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerSolicitudRestaurantes");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerSolicitudRestaurantesV2");
            dbTse.AddInParameter(dbCommand, "@FechaSeleccionada", DbType.DateTime, fechaSeleccionada);
            dbTse.AddInParameter(dbCommand, "@IdInterno", DbType.String, idInterno);

            var restaurantesList = new List<SolicitudRestaurantesRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    restaurantesList.Add(new SolicitudRestaurantesRecord(reader));
                }
            }

            return restaurantesList;
        }
    }
}

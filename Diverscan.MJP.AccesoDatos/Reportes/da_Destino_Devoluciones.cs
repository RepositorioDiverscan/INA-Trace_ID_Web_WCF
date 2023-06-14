using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.Reportes.Articulos;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    class da_Destino_Devoluciones
    {

        public List<e_Destinos_Dev> ObtenerDestino()
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_BUSCADESTINO_DEVOLUCIONES");

            List<e_Destino_Dev> articulosList = new List<e_Destino_Dev>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string nombre = reader["Nombre"].ToString();
              
                    articulosList.Add(new Entidades.e_Destino_Dev(nombre, GTIN));
                }
            }
            return articulosList;
        }
    }
}

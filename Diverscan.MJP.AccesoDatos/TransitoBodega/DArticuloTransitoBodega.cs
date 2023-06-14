using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.TransitoBodega
{
    public class DArticuloTransitoBodega
    {
        public List<EArticuloTransitoBodega> BuscarArticulosTransitoBodega(int idBodega)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ArticulosTransitoBodega");
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int64, idBodega);

            List<EArticuloTransitoBodega> listaArticulos = new List<EArticuloTransitoBodega>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {                    
                    listaArticulos.Add(new EArticuloTransitoBodega(reader));
                }
            }

            return listaArticulos;
        }
    }


    
}

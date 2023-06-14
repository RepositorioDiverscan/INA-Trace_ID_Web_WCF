using Diverscan.MJP.AccesoDatos.Reportes.TransitoMercaderia.Entidad;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.TransitoMercaderia
{
    class da_Transito
    {
        public List<EListObtenerTransitoMercaderia> ObtenerTransitoMercaderia(EObtenerTransitoMercaderia obtenerTransitoMercaderia)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerTransitoMercaderia");
            dbTse.AddInParameter(dbCommand, "@fechaInicio", DbType.Date, obtenerTransitoMercaderia.FechaInicio);
            dbTse.AddInParameter(dbCommand, "@fechaFin", DbType.Date, obtenerTransitoMercaderia.FechaFin);
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, obtenerTransitoMercaderia.IdBodega);
            dbTse.AddInParameter(dbCommand, "@idInterno", DbType.String, obtenerTransitoMercaderia.IdInterno);

            List<EListObtenerTransitoMercaderia> listObtenerTrasladoMercaderias = new List<EListObtenerTransitoMercaderia>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string Sku = reader["SKU"].ToString();
                    string Nombre = reader["NombreArticulo"].ToString();
                    int Unidades = Convert.ToInt32(reader["Unidad"].ToString());
                    int DiasUbicacion = Convert.ToInt32(reader["DiasUbicacion"].ToString());
                    string Ubicacion = reader["Ubicacion"].ToString();
                    string Zona = reader["Zona"].ToString();


                    listObtenerTrasladoMercaderias.Add(new EListObtenerTransitoMercaderia(Sku,Nombre,Unidades,DiasUbicacion,Ubicacion,Zona));
                }
            }
            return listObtenerTrasladoMercaderias;
        }
    }
}

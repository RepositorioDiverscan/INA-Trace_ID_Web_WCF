using Diverscan.MJP.AccesoDatos.Reportes.Traslado.Entidad;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Traslado
{
    class da_Traslado
    {
        public List<EListObtenerTrasladoMercaderia> ObtenerTrasladoMercaderia(EObtenerTrasladoMercaderia obtenerTrasladoMercaderia)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerTrasladoMercaderia");
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.Date, obtenerTrasladoMercaderia.FechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.Date, obtenerTrasladoMercaderia.FechaFin);
            dbTse.AddInParameter(dbCommand, "@Sku", DbType.String, obtenerTrasladoMercaderia.Sku);
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, obtenerTrasladoMercaderia.IdBodega);

            List<EListObtenerTrasladoMercaderia> listObtenerTrasladoMercaderias = new List<EListObtenerTrasladoMercaderia>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string Sku = reader["idInterno"].ToString();
                    string Nombre = reader["Nombre"].ToString();
                    string Usuario = reader["Usuario"].ToString();
                    int Unidades = Convert.ToInt32(reader["Unidades"].ToString());
                    
                    listObtenerTrasladoMercaderias.Add(new EListObtenerTrasladoMercaderia(Sku,Nombre,Usuario,Unidades));
                }
            }
            return listObtenerTrasladoMercaderias;
        }
    }
}

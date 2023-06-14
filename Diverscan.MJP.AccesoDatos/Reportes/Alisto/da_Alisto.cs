using Diverscan.MJP.AccesoDatos.Reportes.Alisto.Entidad;
using Diverscan.MJP.AccesoDatos.Reportes.Alisto.RecargaGuanacaste.Entidad;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Alisto
{
   public class da_Alisto
    {
        public List<EListObtenerAsignacionAlisto> ObtenerDiaVecimientoArticulo(DateTime f1, DateTime f2, string id)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerAsignacionAlisto");
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, f1);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, f2);
            dbTse.AddInParameter(dbCommand, "@Usuario", DbType.String, id);

            List<EListObtenerAsignacionAlisto> listaArticulos = new List<EListObtenerAsignacionAlisto>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        listaArticulos.Add(new EListObtenerAsignacionAlisto(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaArticulos;
        }

        public List<EListObtenerRecargaBodegaGuanacaste> ObtenerRecargaBodegaGuanacaste(EObtenerRecargaGuanacaste eObtenerRecargaGuanacaste)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerRecargaBodegaGuanacaste");
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, eObtenerRecargaGuanacaste.FechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, eObtenerRecargaGuanacaste.FechaFin);

            List<EListObtenerRecargaBodegaGuanacaste> listaOlas = new List<EListObtenerRecargaBodegaGuanacaste>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {

                    while (reader.Read())
                    {
                        long IdOla = long.Parse(reader["NumeroOla"].ToString());
                        string Fecha = reader["Fecha"].ToString();
                        string SKU = reader["SKU"].ToString();
                        string Nombre = reader["Nombre"].ToString();
                        int Unidades = Convert.ToInt32(reader["Unidades"].ToString());

                        listaOlas.Add(new EListObtenerRecargaBodegaGuanacaste
                        (
                            IdOla,
                            Fecha,
                            SKU,
                            Nombre,
                            Unidades
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaOlas;
        }
    }
}

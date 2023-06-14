
using Diverscan.MJP.Entidades.Reportes;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    public class da_BusquedaDestino
    {

        public static List<e_ObtenerDevoluciones> ObtenerDatosBusquedaDestino(int IdDestino, DateTime pfechaInicio, DateTime pFechaFin)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_CONSULTA_HISTORIAL_DEVOLUCIONES");
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.Date, pfechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFinal", DbType.Date, pFechaFin);
            dbTse.AddInParameter(dbCommand, "@IdDestino", DbType.Int32, IdDestino);
            dbCommand.CommandTimeout = 3600;
            List<e_ObtenerDevoluciones> listaRegistros = new List<e_ObtenerDevoluciones>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                       
                        string _nombre = reader["Nombre"].ToString();
                        decimal _cantidad = Convert.ToDecimal(reader["Cantidad"].ToString());
                        string _procedencia = reader["Procedencia"].ToString();
                        string _descripcion = reader["Descripcion"].ToString();
                        string _empaque = reader["Empaque"].ToString();
                        string _motivo = reader["Motivo_Devolucion"].ToString();
                        DateTime _fecha_Registro = Convert.ToDateTime(reader["FechaIngreso"].ToString());

                        listaRegistros.Add(new e_ObtenerDevoluciones
                        (
                         _nombre,
                         _cantidad ,
                         _procedencia ,
                         _descripcion,
                         _empaque,
                         _motivo,
                         _fecha_Registro
                        )); 
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaRegistros;
        }
    }
}

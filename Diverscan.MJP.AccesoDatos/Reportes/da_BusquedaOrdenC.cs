using Diverscan.MJP.Entidades.Reportes;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    public class da_BusquedaOrdenC
    {

        public static List<e_ObtenerOrdenesC> ObtenerBusquedaOC(int idMaestroOC, DateTime pfechaInicio, DateTime pFechaFin)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_CONSULTA_HISTORICO_RECEPCION");
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.Date, pfechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFinal", DbType.Date, pFechaFin);
            dbTse.AddInParameter(dbCommand, "@NOrdenCompra", DbType.Int32, idMaestroOC);
            dbCommand.CommandTimeout = 3600;
            List<e_ObtenerOrdenesC> listaRegistros = new List<e_ObtenerOrdenesC>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {

                        int _orden_compra = Convert.ToInt32(reader["Orden_Compra"].ToString());
                        string _producto = reader["Producto"].ToString();
                        decimal _cantidad_recibida = Convert.ToDecimal(reader["Cantidad_Recibida"].ToString());
                        decimal _cantidad_rechazada = Convert.ToDecimal(reader["Cantidad_Rechazada"].ToString());
                        decimal _articulos_oc = Convert.ToDecimal(reader["Articulos_OC"].ToString());
                        DateTime _fecharegistro = Convert.ToDateTime(reader["FechaRegistro"].ToString());
                        DateTime _fechacreacion = Convert.ToDateTime(reader["FechaCreacion"].ToString());

                        listaRegistros.Add(new e_ObtenerOrdenesC
                        (
                        _orden_compra ,
                        _producto ,
                        _cantidad_recibida ,
                        _cantidad_rechazada ,
                        _articulos_oc,
                        _fecharegistro,
                        _fechacreacion
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

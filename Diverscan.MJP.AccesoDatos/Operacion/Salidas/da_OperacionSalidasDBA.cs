using Diverscan.MJP.Entidades.Operacion.Salidas;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.Operacion.Salidas
{

    public class da_OperacionSalidasDBA
    {
        public List<e_Destino_Solicitud_SSCC_Asociado> Obtener_Destinos_Solicitud_Rango_Fecha_Con_SSCCAsociado(string parametroBusqueda, DateTime fechaInicio, DateTime fechaFin)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_Destinos_Solicitud_Rango_Fecha_Con_SSCCAsociado");
            dbTse.AddInParameter(dbCommand, "@PParametroBusqueda", DbType.String, parametroBusqueda);
            dbTse.AddInParameter(dbCommand, "@PFechaInicio", DbType.DateTime, fechaInicio);
            dbTse.AddInParameter(dbCommand, "@PFechaFin", DbType.DateTime, fechaFin);

            List<e_Destino_Solicitud_SSCC_Asociado> listaDatos = new List<e_Destino_Solicitud_SSCC_Asociado>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        long _idMaestroSolicitudTID = (long)Convert.ToDouble((reader["IdMaestroSolicitudTID"].ToString()));
                        string _idSolicitudSAP = reader["IdSolicitudSAP"].ToString();
                        DateTime _fechaCreacion = Convert.ToDateTime(reader["FechaCreacion"].ToString());
                        string _nombreDestino = reader["NombreDestino"].ToString();

                        listaDatos.Add(new e_Destino_Solicitud_SSCC_Asociado(
                            _idMaestroSolicitudTID,
                            _idSolicitudSAP,
                            _fechaCreacion,
                            _nombreDestino
                            ));
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }
            return listaDatos;
        }

        public List<e_SSCC_Zona_Transito_Por_Destino_Solicitud> Obtener_SSCC_Zona_Transito_Por_Destino_Solicitud(long numSolicitudTID)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_SSCC_Zona_Transito_Por_Destino_Solicitud");
            dbTse.AddInParameter(dbCommand, "@PNumSolicitudTID", DbType.String, numSolicitudTID);
            List<e_SSCC_Zona_Transito_Por_Destino_Solicitud> listaDatos = new List<e_SSCC_Zona_Transito_Por_Destino_Solicitud>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        long _idMaestroSolicitud = (long)Convert.ToDouble((reader["IdMaestroSolicitud"].ToString()));
                        long _idConsecutivoSSCC = (long)Convert.ToDouble((reader["IdConsecutivoSSCC"].ToString()));
                        string _SSCCGenerado = reader["SSCCGenerado"].ToString();
                        listaDatos.Add(new e_SSCC_Zona_Transito_Por_Destino_Solicitud(
                            _idConsecutivoSSCC,
                            _SSCCGenerado,
                           _idMaestroSolicitud
                            ));
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }
            return listaDatos;
        }

        public List<e_Articulos_SSCC_Procesado> Obtener_Articulos_SSCC_Procesado(long idConsecutivoSSCC)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_Articulos_SSCC_Procesado");
            dbTse.AddInParameter(dbCommand, "@PIdConsecutivoSSCC", DbType.String, idConsecutivoSSCC);
            List<e_Articulos_SSCC_Procesado> listaDatos = new List<e_Articulos_SSCC_Procesado>();

            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        string _nombreArticulo = reader["NombreArticulo"].ToString();
                        decimal _cantidadUI = Convert.ToDecimal(reader["CantidadUI"].ToString());
                        string _unidadMedidaUI = reader["UnidadMedida"].ToString();

                        listaDatos.Add(new e_Articulos_SSCC_Procesado(
                            _nombreArticulo,
                            _cantidadUI,
                            _unidadMedidaUI
                            ));
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }

            return listaDatos;
        }

    }
}

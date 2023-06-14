using Diverscan.MJP.Entidades.ProcesarSolicitud;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.ProcesarSolicitud
{
    public class da_DetalleSSCCArticuloSolicitud
    {
        public List<e_SSCCSolicitud> ObtenerSSCCNoDespachadosSolicitud(string SSCCGenerado)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_HH_Despacho_Obtener_SSCC_No_Despachados_Solicitud");
            dbTse.AddInParameter(dbCommand, "@PSSCCGenerado", DbType.String, SSCCGenerado);
            List<e_SSCCSolicitud> listaDatos = new List<e_SSCCSolicitud>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        string _consecutivoSSCC = reader["ConsecutivoSSCC"].ToString();
                        string _estadoTraslado = reader["EstadoTraslado"].ToString();
                        string _estadoDespacho = reader["EstadoDespacho"].ToString();

                        listaDatos.Add(new e_SSCCSolicitud(
                            _consecutivoSSCC,
                            _estadoTraslado,
                            _estadoDespacho
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaDatos;
        }
        
        public List<e_ArticulosPendientesSolicitud> ObtenerArticulosPendientesSolicitud(string SSCCGenerado)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_HH_Despacho_Obtener_Articulos_Pendientes_Solicitud_SSCC");
            dbTse.AddInParameter(dbCommand, "@PSSCCGenerado", DbType.String, SSCCGenerado);
            List<e_ArticulosPendientesSolicitud> listaDatos = new List<e_ArticulosPendientesSolicitud>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        string _idInternoArticulo = reader["IdInternoArticulo"].ToString();
                        string _nombreArticulo = reader["NombreArticulo"].ToString();
                        string _estadoArticulo = reader["EstadoArticulo"].ToString();

                        listaDatos.Add(new e_ArticulosPendientesSolicitud(
                            _idInternoArticulo,
                            _nombreArticulo,
                            _estadoArticulo
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaDatos;
        }

        public string ObtenerDestinoPorSSCCGenerado(string SSCCGenerado)
        {
            string nombreDestino = "";
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_HH_Despacho_Obtener_Destino_Por_SSCCGenerado");
            dbTse.AddInParameter(dbCommand, "@PSSCCGenerado", DbType.String, SSCCGenerado);
            dbTse.AddOutParameter(dbCommand, "@PNombreDestinoSolicitudOUT", DbType.String, 200);            
            try
            {
                dbTse.ExecuteNonQuery(dbCommand);
                nombreDestino = dbTse.GetParameterValue(dbCommand, "@PNombreDestinoSolicitudOUT").ToString();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return nombreDestino;
        }        
    }
}
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.SSCC;
using Diverscan.MJP.Entidades.OPESALMaestroSolicitud;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Certificación
{
    public class d_Certificacion
    {
        public List<E_CertificacionDetalle> ObtenerDetalleCertificacion(int idBodega, int idMaestroSolicitud)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_DetalleOlaCertificador");
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);
            dbTse.AddInParameter(dbCommand, "@idMaestroOrdenSalida", DbType.Int32, idMaestroSolicitud);

            List<E_CertificacionDetalle> ListCertificacionDetalle = new List<E_CertificacionDetalle>();

            var reader = dbTse.ExecuteReader(dbCommand);
            List<e_OPESALMaestroSolicitud> ordersToEnlist = new List<e_OPESALMaestroSolicitud>();
            
            while (reader.Read())
            {
                E_CertificacionDetalle detailCertificacion = new E_CertificacionDetalle(reader);
                ListCertificacionDetalle.Add(detailCertificacion);  
            }
            return ListCertificacionDetalle;
        }

        public string CertificarLineaSSCC(string consecutivoSSCC, EDetalleSSCCOla detalleSSCCOla)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("CertificarLineaSSCC");
            dbTse.AddInParameter(dbCommand, "@consecutivoSSCC", DbType.String, consecutivoSSCC);            
            dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int32, detalleSSCCOla.IdArticulo);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, detalleSSCCOla.Lote);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.Date, detalleSSCCOla.FechaAndroid);
            dbTse.AddInParameter(dbCommand, "@cantidadCertificar", DbType.Int32, detalleSSCCOla.Cantidad);
            dbTse.AddInParameter(dbCommand, "@cantidadCertificada", DbType.Int32, detalleSSCCOla.CantidadCertificada);
            dbTse.AddInParameter(dbCommand, "@diferenciaCertificacion", DbType.Int32, detalleSSCCOla.DiferenciaCertificacion);
            string result = "";
            
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    result = reader["Resultado"].ToString();
                }
            }           
            return result;
        }

        public string CertificarSSCC(string consecutivoSSCC, int idUsuario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("CertificarSSCC");
            dbTse.AddInParameter(dbCommand, "@consecutivoSSCC", DbType.String, consecutivoSSCC);
            dbTse.AddInParameter(dbCommand, "@idUsuario", DbType.Int64, idUsuario);
            string result = "";

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    result = reader["Resultado"].ToString();
                }
            }
            return result;
        }

        public List<e_OPESALMaestroSolicitud> GetOrdersToCertificated(
           int idInternoWarehouse, DateTime dateInit, DateTime dateEnd, string idInternoOrder)//
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_GetOrdersToCertificated");

            dbTse.AddInParameter(dbCommand, "@idInternoOrder", DbType.String, idInternoOrder);
            dbTse.AddInParameter(dbCommand, "@dateInit", DbType.DateTime, dateInit);
            dbTse.AddInParameter(dbCommand, "@dateEnd", DbType.DateTime, dateEnd);
            dbTse.AddInParameter(dbCommand, "@idWareHouse", DbType.Int32, idInternoWarehouse);
            dbCommand.CommandTimeout = 300;
            var reader = dbTse.ExecuteReader(dbCommand);
            List<e_OPESALMaestroSolicitud> ordersToEnlist = new List<e_OPESALMaestroSolicitud>();
            while (reader.Read())
            {
                e_OPESALMaestroSolicitud order = new e_OPESALMaestroSolicitud();

                order.IdMaestroSolicitud = long.Parse(reader["idMaestroSolicitud"].ToString());
                order.FechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
                order.Nombre = reader["Nombre"].ToString();
                order.Comentarios = reader["Comentarios"].ToString();
                order.IdDestino = Convert.ToInt32(reader["idDestino"].ToString());
                order.DestinoNombre = reader["DestinoNombre"].ToString();
                order.IdInterno = reader["idInterno"].ToString();
                order.IdBodega = Convert.ToInt32(reader["idBodega"].ToString());
                order.NombreBodega = reader["Bodega"].ToString();
                order.Prioridad = Convert.ToInt32(reader["Prioridad"].ToString());
                order.PrioridadString = reader["PrioridadDescripcion"].ToString();
                order.FechaEntrega = DateTime.Parse(reader["FechaEntrega"].ToString());
                order.PorcentajeAlisto = reader["PorcentajeAlistado"].ToString();
                order.PorcentajeAsignado = reader["PorcentajeAsignado"].ToString();             
                order.Certificado = Convert.ToBoolean(Convert.ToInt16(reader["certificado"].ToString()));
                order.CantidadSSCC = Convert.ToInt32(reader["totalSSCC"].ToString());
                order.CantidadSSCCUbciados = Convert.ToInt32(reader["SSCCUbicados"].ToString());

                ordersToEnlist.Add(order);
            }

            return ordersToEnlist;

        }

        public string CertificarOla(long idMaestroSolicitud, long idUsuario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("certificarOla_VP");
            dbTse.AddInParameter(dbCommand, "@idMaestroSolicitud", DbType.String, idMaestroSolicitud);
            dbTse.AddInParameter(dbCommand, "@idUsuario", DbType.Int64, idUsuario);
            string result = "";

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    result = reader["Resultado"].ToString();
                }
            }
            return result;
        }
    }
}

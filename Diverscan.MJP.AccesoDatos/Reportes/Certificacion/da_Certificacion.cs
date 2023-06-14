using Diverscan.MJP.AccesoDatos.Reportes.Certificacion.SSCCSINOFinalizado;
using Diverscan.MJP.AccesoDatos.Reportes.Cliente;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.AsignoCertificacion
{
    public class da_Certificacion
    {
        public List<EListAsignacionCertificacion> ObtenerListadoAsignacionCertificacion(EAsignacionCertificacion asignacionCertificacion)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_AsignacionCertificacion");
            dbTse.AddInParameter(dbCommand, "@fechaInicio", DbType.DateTime, asignacionCertificacion.FechaInicio);
            dbTse.AddInParameter(dbCommand, "@fechaFin", DbType.DateTime, asignacionCertificacion.FechaFin);
            dbTse.AddInParameter(dbCommand, "@idOla", DbType.Int32, asignacionCertificacion.IdOla);
            dbTse.AddInParameter(dbCommand, "@usuario", DbType.String, asignacionCertificacion.Usuario);

            List<EListAsignacionCertificacion> listAsignacionCertificacions = new List<EListAsignacionCertificacion>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string SSCC = reader["SSCC"].ToString();
                    string Nombre = reader["Nombre"].ToString();
                    string SKU = reader["SKU"].ToString();
                    int UnidadAsignada = Convert.ToInt32(reader["UnidadAsignadaCertificacion"].ToString());
                    int CantidadCertificada = Convert.ToInt32(reader["CantidadCertificada"].ToString());
                    string EstadoSSCC = reader["EstadoSSCC"].ToString();
                    listAsignacionCertificacions.Add(new EListAsignacionCertificacion(SSCC, Nombre, SKU, UnidadAsignada, CantidadCertificada, EstadoSSCC));
                }
            }
            return listAsignacionCertificacions;
        }

        public List<EListSSCCSINOFinalizado> ObtenerListadoSSCCSINOFinalizado(ESSCCSINOFinalizado eSSCCSINOFinalizado)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerSSCCPorcentajeAvance");
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, eSSCCSINOFinalizado.FehaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, eSSCCSINOFinalizado.FechaFin);
            dbTse.AddInParameter(dbCommand, "@idCliente", DbType.String, eSSCCSINOFinalizado.IdCliente);
            dbCommand.CommandTimeout = 300;

            List<EListSSCCSINOFinalizado> listSSCCSINOFinalizados = new List<EListSSCCSINOFinalizado>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string SSCC = reader["SSCC"].ToString();
                    string Fecha = reader["Fecha"].ToString();
                    string NombreCliente = reader["NombreCliente"].ToString();
                    string PorCertificado = reader["PorCertificado"].ToString();
                    int IdOla = Convert.ToInt32(reader["idOla"].ToString());
                    listSSCCSINOFinalizados.Add(new EListSSCCSINOFinalizado(SSCC, Fecha, NombreCliente, PorCertificado, IdOla));
                }
            }
            return listSSCCSINOFinalizados;
        }

        public List<EListObtenerCliente> ObtenerClientes(EObtenerCliente obtenerCliente)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_GetClientes");
            dbTse.AddInParameter(dbCommand, "@fechaInicio", DbType.DateTime, obtenerCliente.FechaInicio);
            dbTse.AddInParameter(dbCommand, "@fechaFin", DbType.DateTime, obtenerCliente.FechaFin);

            List<EListObtenerCliente> listAsignacionCertificacions = new List<EListObtenerCliente>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string IdCliente = reader["idCliente"].ToString();
                    string NombreCliente = reader["NombreCliente"].ToString();
                    listAsignacionCertificacions.Add(new EListObtenerCliente(IdCliente, NombreCliente));
                }
            }
            return listAsignacionCertificacions;
        }
    }
}

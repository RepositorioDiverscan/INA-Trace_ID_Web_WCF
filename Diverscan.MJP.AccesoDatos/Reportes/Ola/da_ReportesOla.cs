using Diverscan.MJP.AccesoDatos.Reportes.Ola.DespachoOla;
using Diverscan.MJP.AccesoDatos.Reportes.Ola.DisponibleFcturacion;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.ReportePedidoSinOla
{
    public class da_ReportesOla
    {
        public List<EListPedidoSinOla> ObtenerListadoPedidosSinOlas(EPedidosSinOla pedidosSinOla)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerPedidosSinOla");
            dbTse.AddInParameter(dbCommand, "@fechaInicial", DbType.DateTime, pedidosSinOla.FechaInicio);
            dbTse.AddInParameter(dbCommand, "@fechaFinal", DbType.DateTime, pedidosSinOla.FechaFinal);
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, pedidosSinOla.IdBodega);

            List<EListPedidoSinOla> ListaPreMaestroSolicitud = new List<EListPedidoSinOla>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string idMaestroSolicitud = reader["idInterno"].ToString();
                    string nombre = reader["Nombre"].ToString();
                    string comentarios = reader["Comentarios"].ToString();
                    DateTime fechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
                    string Ruta = reader["Ruta"].ToString();
                    string Direccion = reader["Direccion"].ToString();

                    ListaPreMaestroSolicitud.Add(new EListPedidoSinOla(idMaestroSolicitud, nombre,
                        comentarios, Ruta, Direccion,fechaCreacion));
                }
            }
            return ListaPreMaestroSolicitud;
        }

        public List<EListBodega> ObtenerBodegas()
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_GETBODEGA");

            List<EListBodega> ListaPreMaestroSolicitud = new List<EListBodega>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    int idBodega = Convert.ToInt32(reader["idBodega"].ToString());
                    string nombre = reader["nombre"].ToString();
                    string idInterno = reader["idInterno"].ToString();
                   
                    ListaPreMaestroSolicitud.Add(new EListBodega(idBodega,nombre,idInterno));
                }
            }
            return ListaPreMaestroSolicitud;
        }

        public List<EListObtenerOlasDisponiblesFacturacion> ObtenerOlasDisponiblesFacturacion(EObtenerOlasDisponiblesFacturacion disponiblesFacturacion)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerOlasDisponiblesFacturacion");
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, disponiblesFacturacion.FechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, disponiblesFacturacion.FechaFin);

            List<EListObtenerOlasDisponiblesFacturacion> listObtenerOlasDisponiblesFacturacions = new List<EListObtenerOlasDisponiblesFacturacion>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string Fecha = reader["Fecha"].ToString();
                    long idOla = long.Parse(reader["idOla"].ToString());
                    string NombreCliente = reader["NombreCliente"].ToString();
                    string DiasFinalizados = reader["DiasFinalizados"].ToString();
                    string Avance = reader["Avance"].ToString();

                    listObtenerOlasDisponiblesFacturacions.Add(new EListObtenerOlasDisponiblesFacturacion(Fecha,idOla,NombreCliente,DiasFinalizados,Avance));
                }
            }
            return listObtenerOlasDisponiblesFacturacions;
        }
        public List<EListObtenerDespachoMercaderia> ObtenerDespachoMercaderia(EObtenerDespachoMercaderia despachoMercaderia)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerDespachoMercaderia");
            dbTse.AddInParameter(dbCommand, "@FechaInico", DbType.DateTime, despachoMercaderia.FechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, despachoMercaderia.FechaFin);
            dbTse.AddInParameter(dbCommand, "@idOla", DbType.String, despachoMercaderia.IdOla);

            List<EListObtenerDespachoMercaderia> listdespacho  = new List<EListObtenerDespachoMercaderia>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    
                    long idOla = long.Parse(reader["idOla"].ToString());
                    string FechaDespacho = reader["FechaDespacho"].ToString();
                    string Nombre = reader["Nombre"].ToString();
                    string UnidadTransporte = reader["UnidadTransporte"].ToString();

                    listdespacho.Add(new EListObtenerDespachoMercaderia(idOla, UnidadTransporte, Nombre, FechaDespacho));
                }
            }
            return listdespacho;
        }
    }
}

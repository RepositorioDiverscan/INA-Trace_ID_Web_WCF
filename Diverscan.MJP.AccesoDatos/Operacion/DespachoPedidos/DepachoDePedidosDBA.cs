using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Diverscan.MJP.AccesoDatos.Operacion.DespachoPedidos.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.Operacion.DespachoPedidos
{
    public class DepachoDePedidosDBA : IDespachoDePedidos
    {
        public void FacturarOla(int Ola, long idTransportista)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_FacturarOla");
            dbTse.AddInParameter(dbCommand, "@idOla", DbType.Int32, Ola);
            dbTse.AddInParameter(dbCommand, "@idTransportista", DbType.Int64, idTransportista);
            dbTse.ExecuteNonQuery(dbCommand);
        }
 
        public List<E_ListadoDetalleOla> ObtenerDetalleOla(int idOla)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerDetallesOla");
            dbTse.AddInParameter(dbCommand, "@idOla", DbType.Int32, idOla);

            List<E_ListadoDetalleOla> ListaDetallesOla = new List<E_ListadoDetalleOla>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    int idRegistroOla = Convert.ToInt32(reader["idRegistroOla"].ToString());
                    int idMaestroSolicitud = Convert.ToInt32(reader["idMaestroSolicitud"].ToString());
                    string idInternoPanal = reader["idInternoPanal"].ToString();
                    string Nombre = reader["Nombre"].ToString();
                    decimal CantidadSolicitada = Convert.ToDecimal(reader["CantidadSolicitada"].ToString());
                    decimal CantidadAlistada = Convert.ToDecimal(reader["CantidadAlistada"].ToString());
                    decimal CantidadDisponible = Convert.ToDecimal(reader["CantidadDisponible"].ToString());
                    string NombreUsuario = reader["NombreUsuario"].ToString();


                    ListaDetallesOla.Add(new E_ListadoDetalleOla(idRegistroOla, idMaestroSolicitud, idInternoPanal, Nombre
                        , CantidadSolicitada, CantidadAlistada,CantidadDisponible, NombreUsuario));
                }
            }
            return ListaDetallesOla;

        }

        public List<E_ListadoOlasFactura> ObtenerListadoOlas(int idBodega, DateTime FechaInicio,
            DateTime FechaFinal, string Busqueda, int facturado)
        {
            try
            {
           
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenesListadoOlas_Facturar");
                dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);
                dbTse.AddInParameter(dbCommand, "@fechaInicio", DbType.DateTime, FechaInicio);
                dbTse.AddInParameter(dbCommand, "@fechaFinal", DbType.DateTime, FechaFinal);
                dbTse.AddInParameter(dbCommand, "@busqueda", DbType.String, Busqueda);
                dbTse.AddInParameter(dbCommand, "@facturado", DbType.Int32, facturado);

                List<E_ListadoOlasFactura> ListaOlas = new List<E_ListadoOlasFactura>();

                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        int idRegistroOla = Convert.ToInt32(reader["idRegistroOla"].ToString());
                        string FechaIngreso = reader["FechaIngreso"].ToString();
                        string Observacion = reader["Observacion"].ToString();
                        string Ruta = reader["Ruta"].ToString();
                        string PorcentajeAlistado = reader["PorcentajeAlistado"].ToString();
                        string Facturado = reader["Facturado"].ToString();

                        ListaOlas.Add(new E_ListadoOlasFactura(idRegistroOla, FechaIngreso, Observacion, Ruta, PorcentajeAlistado, Facturado));
                    }
                }
                return ListaOlas;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public List<EMaestroSolicitudFacturado> ObtenerPreMaestrosFacturados(int idBodega, DateTime FechaInicio,
           DateTime FechaFinal)
        {           
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("ObtenerMaestrosSolicitudFacturadosFecha");
                dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);
                dbTse.AddInParameter(dbCommand, "@fechaInicio", DbType.DateTime, FechaInicio);
                dbTse.AddInParameter(dbCommand, "@fechaFin", DbType.DateTime, FechaFinal);

                List<EMaestroSolicitudFacturado> maestrosFacturadosLista = new List<EMaestroSolicitudFacturado>();

                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        long idRegistroOla = Convert.ToInt32(reader["idRegistroOla"].ToString());
                        long idMaestroSolicitud = Convert.ToInt32(reader["idMaestroSolicitud"].ToString());
                        string numeroFactura = reader["numeroFactura"].ToString();
                        DateTime fechaCreacion = Convert.ToDateTime(reader["FechaCreacion"].ToString());
                        string nombre = reader["Nombre"].ToString();
                        string comentarios = reader["Comentarios"].ToString();
                        string idInternoMaestro = reader["idInternoMaestro"].ToString();
                        string idInternoCliente = reader["idInternoCliente"].ToString();
                        string nombreCliente = reader["nombreCliente"].ToString();

                        maestrosFacturadosLista.Add(
                            new EMaestroSolicitudFacturado(idRegistroOla, idMaestroSolicitud, numeroFactura, fechaCreacion,
                            nombre, comentarios, idInternoMaestro,idInternoCliente,nombreCliente));
                    }
                }
                return maestrosFacturadosLista;          
        }

        public List<EMaestroSolicitudFacturado> ObtenerPreMaestrosFacturadosXOla(int idWarehouse, long idOla)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerPreMaestrosXOla");
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idWarehouse);
            dbTse.AddInParameter(dbCommand, "@idOla", DbType.Int64, idOla);

            List<EMaestroSolicitudFacturado> maestrosFacturadosLista = new List<EMaestroSolicitudFacturado>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    long idRegistroOla = Convert.ToInt32(reader["idRegistroOla"].ToString());
                    long idMaestroSolicitud = Convert.ToInt32(reader["idMaestroSolicitud"].ToString());
                    string numeroFactura = reader["numeroFactura"].ToString();
                    DateTime fechaCreacion = Convert.ToDateTime(reader["FechaCreacion"].ToString());
                    string nombre = reader["Nombre"].ToString();
                    string comentarios = reader["Comentarios"].ToString();
                    string idInternoMaestro = reader["idInternoMaestro"].ToString();
                    string idInternoCliente = reader["idInternoCliente"].ToString();
                    string nombreCliente = reader["nombreCliente"].ToString();

                    maestrosFacturadosLista.Add(
                        new EMaestroSolicitudFacturado(idRegistroOla, idMaestroSolicitud, numeroFactura, fechaCreacion,
                        nombre, comentarios, idInternoMaestro, idInternoCliente, nombreCliente));
                }
            }
            return maestrosFacturadosLista;
        }

        public List<EMaestroFacturadoProducto> ObtenerPreMaestrosXArticulo(long idBodega, string lote,
           DateTime fechaExp, long idArticulo)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerFacturasXArticulo");
            dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@lote", DbType.String, lote);
            dbTse.AddInParameter(dbCommand, "@fechaExp", DbType.DateTime, fechaExp);
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int64, idBodega);

            List<EMaestroFacturadoProducto> maestrosFacturadosLista = new List<EMaestroFacturadoProducto>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string numeroFactura = reader["numeroFactura"].ToString();               
                    string nombre = reader["Nombre"].ToString();                    
                    string idInternoMaestro = reader["idInternoMaestro"].ToString();
                    string idInternoCliente = reader["idInternoCliente"].ToString();
                    string nombreCliente = reader["nombreCliente"].ToString();
                    decimal cantidad = Convert.ToDecimal(reader["Cantidad"].ToString());
                    maestrosFacturadosLista.Add(
                        new EMaestroFacturadoProducto(numeroFactura,nombre,idInternoMaestro,
                        idInternoCliente,nombreCliente,cantidad));
                }
            }
            return maestrosFacturadosLista;
        }
    }
}

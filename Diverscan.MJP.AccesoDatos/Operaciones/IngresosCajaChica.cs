using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Operaciones
{
    public class IngresosCajaChica
    {

        public List<eReporteICCh> IngresoCajaChica(int idBodega, DateTime f1, DateTime f2, string numero)
        {

            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_RECEPCION_ING_CAJA_CHICA");
            db.AddInParameter(dbCommand, "@ordenCompra", DbType.String, numero);
            db.AddInParameter(dbCommand, "@fechaInicioBusqueda", DbType.DateTime, f1);
            db.AddInParameter(dbCommand, "@fechaFinBusqueda", DbType.DateTime, f2);
            db.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);
            //guardar en una lista
            List<eReporteICCh> ListordenCompra = new List<eReporteICCh>();
            try
            {
                using (var reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ListordenCompra.Add(new eReporteICCh(reader));
                    }
                }
            }
            catch (Exception e)
            {
                var p = e.Message;
            }

            return ListordenCompra;
        }

        public List<EDetalleReporteOC> ObtenerDetalleIngCajaChica(int id)
        {

            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_GETDETAILICCh");
            db.AddInParameter(dbCommand, "@p_idMaestroIngresoCCh", DbType.Int32, id);
            // db.AddInParameter(dbCommand, "@p_idBodega", DbType.Int32, idBodega);

            //guardar en una lista
            List<EDetalleReporteOC> ListordenDetalleCompra = new List<EDetalleReporteOC>();
            try
            {
                using (var reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ListordenDetalleCompra.Add(new EDetalleReporteOC(reader));
                    }
                }
            }
            catch (Exception e)
            {
                var p = e.Message;
            }

            return ListordenDetalleCompra;
        }

        public string ProcesarFactura(int id, DateTime FechaProc)
        {
            string respuesta = "";
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_PROCESSICCh");
            db.AddInParameter(dbCommand, "@p_idMaestroIngresoCajaChica", DbType.Int32, id);
            db.AddInParameter(dbCommand, "@p_FechaProc", DbType.DateTime, FechaProc);

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    respuesta = reader["respuesta"].ToString();
                }
            }

            return respuesta;
        }

        public List<EDetalleOrdenC> ObtenerDetalleCC(int id, int idBodega)
        {

            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_GETDETAILICCh");
            db.AddInParameter(dbCommand, "@p_idMaestroIngresoCCh", DbType.Int32, id);
            //db.AddInParameter(dbCommand, "@p_idBodega", DbType.Int32, idBodega);

            List<EDetalleOrdenC> ListDetalleIngCCh = new List<EDetalleOrdenC>();
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    EDetalleOrdenC datos = new EDetalleOrdenC();
                    datos.Nombre = reader["Nombre"].ToString();
                    datos.IdArticulo = Convert.ToInt32(reader["idArticulo"].ToString());
                    datos.numFactura = reader["numFactura"].ToString();
                    datos.IdInterno = reader["idInterno"].ToString();
                    datos.GTIN = reader["GTIN"].ToString();
                    datos.DescripcionRechazo = reader["DescripcionRechazo"].ToString();
                    datos.CantidadBodega = Convert.ToInt32(reader["CantidadBodega"].ToString());

                    string CantidadaRechaz = reader["CantidadRechazados"].ToString();

                    if (CantidadaRechaz != "")
                    {
                        datos.CantidadRechazados = Convert.ToDouble(reader["CantidadRechazados"].ToString());
                    }
                    else
                    {
                        datos.CantidadRechazados = 0;
                    }

                    string CantidadaRecibir = reader["CantidadRecibidos"].ToString();
                    if (CantidadaRecibir != "")
                    {
                        datos.CantidadRecibidos = Convert.ToDouble(reader["CantidadRecibidos"].ToString());
                    }
                    else
                    {
                        datos.CantidadRecibidos = 0;
                    }
                    string cantidadTrans = reader["CantidadTransito"].ToString();
                    if (cantidadTrans != "")
                    {
                        datos.CantidadTransito = Convert.ToDouble(reader["CantidadTransito"].ToString());
                    }
                    else
                    {
                        datos.CantidadRechazados = 0;
                    }
                    datos.CantidadxRecibir = Convert.ToDouble(reader["CantidadxRecibir"].ToString());
                    datos.NumLinea = reader["numLinea"].ToString();
                    ListDetalleIngCCh.Add(datos);
                }
            }
            return ListDetalleIngCCh;
        }

        public List<eIngresoCajaChica> ingresoXCajaChica(DateTime? fechaInicio, DateTime? fechaFin, string ordenCompra, int idBodega)
        {

            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_BuscarIngresosCajaChicaARecibirBodega");
            db.AddInParameter(dbCommand, "@cajaChcica", DbType.String, ordenCompra);
            db.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);
            db.AddInParameter(dbCommand, "@fechaInicioBusqueda", DbType.DateTime, fechaInicio);
            db.AddInParameter(dbCommand, "@fechaFinBusqueda", DbType.DateTime, fechaFin);
            List<eIngresoCajaChica> ListordenCompra = new List<eIngresoCajaChica>();
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    eIngresoCajaChica datos = new eIngresoCajaChica();
                    datos.NumeroTransaccion = reader["NumeroTransaccion"].ToString();
                    datos.NumeroValeCajaChica = reader["NumeroValeCajaChica"].ToString();
                    datos.fechaCreacion = Convert.ToDateTime(reader["FechaCreacion"].ToString());
                    datos.NombreProveedor = reader["NombreProveedor"].ToString();
                    datos.Usuario = reader["Usuario"].ToString();
                    datos.NumeroCertificado = reader["NumeroCertificado"].ToString();
                    datos.Procesada = Convert.ToBoolean(reader["Procesada"].ToString());
                    datos.PorcentajeRecepcion = Convert.ToDouble(reader["PorcentajeRecepcion"].ToString());

                    var fechaProcesamientoTesto = reader["FechaProcesamiento"].ToString();
                    if (!string.IsNullOrEmpty(fechaProcesamientoTesto))
                        datos.FechaProcesamiento = Convert.ToDateTime(fechaProcesamientoTesto);
                    datos.IdMaestroIngresoCajaChica = Convert.ToInt32(reader["IdMaestroIngresoCajaChica"].ToString());
                    datos.BodegaDefecto = reader["idBodega"].ToString();
                    ListordenCompra.Add(datos);
                }
            }


            return ListordenCompra;
        }

    }
}

using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Diverscan.MJP.AccesoDatos.Operaciones
{
    public class DBIngresoTrasladoBodega
    {
        public List<EIngresoTrasladoBodega> ObtenerIngresoTrasladoBodega(DateTime? fechaInicio, DateTime? fechaFin, string numTransaccion, int idBodega)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_BuscarIngresoTrasladoARecibirBodega");
                db.AddInParameter(dbCommand, "@numTransaccion", DbType.String, numTransaccion);
                db.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);
                db.AddInParameter(dbCommand, "@fechaInicioBusqueda", DbType.DateTime, fechaInicio);
                db.AddInParameter(dbCommand, "@fechaFinBusqueda", DbType.DateTime, fechaFin);
                List<EIngresoTrasladoBodega> ListIngresoTraslado = new List<EIngresoTrasladoBodega>();
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        EIngresoTrasladoBodega datos = new EIngresoTrasladoBodega();
                        datos.NumeroTransaccion = reader["NumeroTransaccion"].ToString();
                        datos.Comentario = reader["Comentario"].ToString();
                        datos.FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"].ToString());
                        datos.Usuario = reader["Usuario"].ToString();
                        datos.Proveedor = reader["NombreProveedor"].ToString();

                        if (Convert.ToBoolean(reader["Estado"]))
                        {
                            datos.Estado = "Listo para Recepción";
                        }
                        else
                        {
                            datos.Estado = "Pendiente";
                        }

                        datos.PorcentajeRecepcion = Convert.ToDouble(reader["PorcentajeRecepcion"].ToString());

                        var fechaProcesamientoTesto = reader["FechaProcesamiento"].ToString();
                        if (!string.IsNullOrEmpty(fechaProcesamientoTesto))
                            datos.FechaProcesamiento = Convert.ToDateTime(fechaProcesamientoTesto);
                        datos.IdMaestroIngresoTraslado = Convert.ToInt32(reader["idMaestroIngresoTraslado"].ToString());
                        datos.IdBodega = Convert.ToInt32(reader["idBodega"]);
                        ListIngresoTraslado.Add(datos);
                    }
                }

                return ListIngresoTraslado;
            }


            catch (Exception ex)
            {

                throw ex;
            }

        }


        public List<EDetalleOrdenC> ObtenerDetalleIngresoTraslado(int id, int idBodega)
        {

            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_BuscarDetalleIngresoTrasladoARecibirBodega");
            db.AddInParameter(dbCommand, "@p_idMaestroIngresoTraslado", DbType.Int32, id);
            db.AddInParameter(dbCommand, "@p_idBodega", DbType.Int32, idBodega);

            List<EDetalleOrdenC> ListDetalleordenCompra = new List<EDetalleOrdenC>();
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    EDetalleOrdenC datos = new EDetalleOrdenC();
                    datos.Nombre = reader["Nombre"].ToString();
                    datos.IdArticulo = Convert.ToInt32(reader["idArticulo"].ToString());
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
                    ListDetalleordenCompra.Add(datos);
                }
            }
            return ListDetalleordenCompra;
        }


        public string CrearIngresoTrasladoBodegas(EIngresoTrasladoBodega eIngresoTraslado)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");

            var dbCommand = db.GetStoredProcCommand("SP_CrearIngresoTrasladoBodega");
            db.AddInParameter(dbCommand, "@numTransaccion", DbType.String, eIngresoTraslado.NumeroTransaccion);
            db.AddInParameter(dbCommand, "@idBodega", DbType.Int32, eIngresoTraslado.IdBodega);
            db.AddInParameter(dbCommand, "@IdBodegaTraslado", DbType.Int32, eIngresoTraslado.IdBodegaTraslado);
            db.AddInParameter(dbCommand, "@comentario", DbType.String, eIngresoTraslado.Comentario);
            db.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, eIngresoTraslado.IdUsuario);
            db.AddInParameter(dbCommand, "@idInterno", DbType.String, eIngresoTraslado.IdInterno);
            db.AddInParameter(dbCommand, "@cantidadSolicitada", DbType.Int32, eIngresoTraslado.CantidadSolicitada);

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    return reader["respuesta"].ToString();
                }
            }
            return "Error";
        }
    }
}

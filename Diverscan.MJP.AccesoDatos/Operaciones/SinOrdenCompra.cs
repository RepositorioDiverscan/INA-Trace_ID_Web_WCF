using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Operaciones
{
    public class SinOrdenCompra
    {

        public List<eSinOrdenCompra> SinOrdenComprasBodega(DateTime? fechaInicio, DateTime? fechaFin, string numTransaccion, int idBodega)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = db.GetStoredProcCommand("SP_BuscarSinOrdenCompraARecibirBodega");
                db.AddInParameter(dbCommand, "@numTransaccion", DbType.String, numTransaccion);
                db.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);
                db.AddInParameter(dbCommand, "@fechaInicioBusqueda", DbType.DateTime, fechaInicio);
                db.AddInParameter(dbCommand, "@fechaFinBusqueda", DbType.DateTime, fechaFin);
                List<eSinOrdenCompra> ListordenCompra = new List<eSinOrdenCompra>();
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        eSinOrdenCompra datos = new eSinOrdenCompra();
                        datos.NumeroTransaccion = reader["NumeroTransaccion"].ToString();
                        datos.DescCorta = reader["DescripcionCorta"].ToString();
                        datos.FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"].ToString());
                        datos.NumeroCertificado = reader["NumeroCertificado"].ToString();
                        datos.NombreUsuario = reader["Usuario"].ToString();
                        datos.NombreProveedor = reader["NombreProveedor"].ToString();
                        datos.Estado = reader["Estado"].ToString();
                        datos.PorcentajeRecepcion = Convert.ToDouble(reader["PorcentajeRecepcion"].ToString());

                        var fechaProcesamientoTesto = reader["FechaProcesamiento"].ToString();
                        if (!string.IsNullOrEmpty(fechaProcesamientoTesto))
                            datos.FechaProcesamiento = Convert.ToDateTime(fechaProcesamientoTesto);
                        datos.IdMaestroSinOrdenCompra = Convert.ToInt32(reader["idMaestroOrdenCompraSinDoc"].ToString());
                        datos.BodegaDefecto = reader["idBodega"].ToString();
                        ListordenCompra.Add(datos);
                    }
                }

                return ListordenCompra;
            }


            catch (Exception ex)
            {

                throw ex;
            }

        }


        public List<EDetalleOrdenC> ObtenerDetalleSinOrdenCompras(int id, int idBodega)
        {

            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_GETDETAILWITHOUTPURCHASEORDER");
            db.AddInParameter(dbCommand, "@p_idMaestroSinOrdenCompra", DbType.Int32, id);

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



    }
}

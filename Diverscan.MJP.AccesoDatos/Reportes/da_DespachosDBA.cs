using Diverscan.MJP.Entidades.Reportes.Despachos;
using Diverscan.MJP.Entidades.Reportes.Kardex;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    public class da_DespachosDBA
    {
        public List<e_Detalle_Despacho_Por_Numero_Solicitud> ObtenerDetalleDespachoPorNumeroSolicitud(long pNumeroSolicitud, ref DateTime fechaPedidoOUT, ref string destinoDescripcion, ref string idInternoSolicitud, ref string idSolicitudTID)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_Detalle_Despacho_Por_Numero_Solicitud");
            dbTse.AddInParameter(dbCommand, "@PNumeroSolicitud", DbType.String, pNumeroSolicitud);
            dbTse.AddOutParameter(dbCommand, "@POUTFechaSolicitud", DbType.String, 50);
            dbTse.AddOutParameter(dbCommand, "@POUTDestinoDescripcion", DbType.String, 200);
            dbTse.AddOutParameter(dbCommand, "@POUTIdInternoSolicitud", DbType.String, 1000);
            dbTse.AddOutParameter(dbCommand, "@POUTIdSolicitudTID", DbType.String, 1000);
            List<e_Detalle_Despacho_Por_Numero_Solicitud> listaDatos = new List<e_Detalle_Despacho_Por_Numero_Solicitud>();
            try
            {

                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {


                        string codigo = reader["Codigo"].ToString();
                        long sscc = Convert.ToInt64(reader["SSCC"].ToString());
                        string ssccEtiqueta = reader["SSCCEtiqueta"].ToString();
                        string lote = reader["Lote"].ToString();
                        string descripcion = reader["Descripcion"].ToString();
                        int bultos = Convert.ToInt32(reader["Bultos"].ToString());
                        string bultosUnidadMedida = reader["UnidadMedidaBulto"].ToString();
                        string ubicacion = reader["Ubicacion"].ToString();
                        decimal pedidoUI = Decimal.Round(Convert.ToDecimal(reader["PedidoUI"].ToString()), 2);
                        decimal alistadoUI = Decimal.Round(Convert.ToDecimal(reader["AlistadoUI"].ToString()), 2);
                        string unidadMedidaUI = reader["UnidadMedidaUI"].ToString();
                        decimal alistadoUF = Convert.ToDecimal(reader["AlistadoUF"].ToString());
                        string empaqueUF = reader["EmpaqueUF"].ToString();
                        string und = reader["Und"].ToString();
                        decimal costo = Decimal.Round(Convert.ToDecimal(reader["Costo"].ToString()), 2);
                        decimal total = Decimal.Round(Convert.ToDecimal(reader["Total"].ToString()), 2);
                        DateTime fechaPedido = Convert.ToDateTime(reader["FechaPedido"].ToString());
                        string placa = reader["Placa"].ToString();
                        string marcaVehiculo = reader["MarcaVehiculo"].ToString();
                        string modelo = reader["Modelo"].ToString();

                        listaDatos.Add(new e_Detalle_Despacho_Por_Numero_Solicitud
                        (
                            codigo,
                            sscc,
                            ssccEtiqueta,
                            lote,
                            descripcion,
                            bultos,
                            bultosUnidadMedida,
                            ubicacion,
                            pedidoUI,
                            alistadoUI,
                            unidadMedidaUI,
                            alistadoUF,
                            empaqueUF,
                            und,
                            costo,
                            total,
                            fechaPedido,
                            placa,
                            marcaVehiculo,
                            modelo
                        ));
                    }
                }
                //var result = dbTse.GetParameterValue(dbCommand, "@POUTFechaSolicitud").ToString();            
                fechaPedidoOUT = Convert.ToDateTime(dbTse.GetParameterValue(dbCommand, "@POUTFechaSolicitud").ToString());
                destinoDescripcion = dbTse.GetParameterValue(dbCommand, "@POUTDestinoDescripcion").ToString();
                idInternoSolicitud = dbTse.GetParameterValue(dbCommand, "@POUTIdInternoSolicitud").ToString();
                idSolicitudTID = dbTse.GetParameterValue(dbCommand, "@POUTIdSolicitudTID").ToString();
            }
            catch (Exception)
            {
                fechaPedidoOUT = DateTime.Now;
                destinoDescripcion = "";
                idInternoSolicitud = "";
                idSolicitudTID = "";
            }
            return listaDatos;
        }

        public List<e_Detalle_Despacho_Por_Numero_Solicitud_Reporte> ObtenerDetalleDespachoPorNumeroSolicitudReporte(long pNumeroSolicitud)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_Detalle_Despacho_Por_Numero_Solicitud_Reporte");
            dbTse.AddInParameter(dbCommand, "@PNumeroSolicitud", DbType.String, pNumeroSolicitud);
            List<e_Detalle_Despacho_Por_Numero_Solicitud_Reporte> listaDatos = new List<e_Detalle_Despacho_Por_Numero_Solicitud_Reporte>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        string codigo = reader["Codigo"].ToString();
                        long sscc = Convert.ToInt64(reader["SSCC"].ToString());
                        string ssccEtiqueta = reader["SSCCEtiqueta"].ToString();
                        string lote = reader["Lote"].ToString();
                        string descripcion = reader["Descripcion"].ToString();
                        int bultos = Convert.ToInt32(reader["Bultos"].ToString());
                        string bultosUnidadMedida = reader["UnidadMedidaBulto"].ToString();
                        string ubicacion = reader["Ubicacion"].ToString();
                        decimal pedidoUI = Decimal.Round(Convert.ToDecimal(reader["PedidoUI"].ToString()), 2);
                        decimal alistadoUI = Decimal.Round(Convert.ToDecimal(reader["AlistadoUI"].ToString()), 2);
                        string unidadMedidaUI = reader["UnidadMedidaUI"].ToString();
                        decimal alistadoUF = Convert.ToDecimal(reader["AlistadoUF"].ToString());
                        string empaqueUF = reader["EmpaqueUF"].ToString();
                        string und = reader["Und"].ToString();
                        decimal costo = Decimal.Round(Convert.ToDecimal(reader["Costo"].ToString()), 2);
                        decimal total = Decimal.Round(Convert.ToDecimal(reader["Total"].ToString()), 2);
                        DateTime fechaPedido = Convert.ToDateTime(reader["FechaPedido"].ToString());
                        string placa = reader["Placa"].ToString();
                        string marcaVehiculo = reader["MarcaVehiculo"].ToString();
                        string modelo = reader["Modelo"].ToString();
                        string fechaSolicitud = reader["FechaSolicitud"].ToString();
                        string nombreDestino = reader["NombreDestino"].ToString();
                        string idSolicitudTID = reader["IdSolicitudTID"].ToString();
                        string idInternoSolicitud = reader["IdInternoSolicitud"].ToString();
                        listaDatos.Add(new e_Detalle_Despacho_Por_Numero_Solicitud_Reporte
                        (
                            codigo,
                            sscc,
                            ssccEtiqueta,
                            lote,
                            descripcion,
                            bultos,
                            bultosUnidadMedida,
                            ubicacion,
                            pedidoUI,
                            alistadoUI,
                            unidadMedidaUI,
                            alistadoUF,
                            empaqueUF,
                            und,
                            costo,
                            total,
                            fechaPedido,
                            placa,
                            marcaVehiculo,
                            modelo,
                            fechaSolicitud,
                            nombreDestino,
                            idSolicitudTID,
                            idInternoSolicitud
                        ));
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }

            return listaDatos;
        }

        public List<e_Pedidos_Despacho> ObtenerPedidosDespachos(DateTime fechaInicio, DateTime fechaFin, bool filtroSoloDespachados)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_Solicitudes_Por_Tienda_Despachos");
            dbTse.AddInParameter(dbCommand, "@PFechaInicio", DbType.DateTime, fechaInicio);
            dbTse.AddInParameter(dbCommand, "@PFechaFin", DbType.DateTime, fechaFin);
            dbTse.AddInParameter(dbCommand, "@PFiltroSoloDespachados", DbType.Boolean, filtroSoloDespachados);
            List<e_Pedidos_Despacho> listaPedidosDespacho = new List<e_Pedidos_Despacho>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {

                        long _numeroSolicituTID = (long)Convert.ToDouble((reader["NumeroSolicituTID"].ToString()));
                        string _numeroSolicitudERP = reader["NumeroSolicitudERP"].ToString();
                        string _idInternoSolicitud = reader["IdInternoSolicitud"].ToString();
                        long _idDestinoPedido = (long)Convert.ToDouble((reader["IdDestinoPedido"].ToString()));
                        string _nombreDestino = reader["NombreDestino"].ToString();
                        string _estadoSolicitud = reader["EstadoSolicitud"].ToString();
                        DateTime _fechaPedido = Convert.ToDateTime(reader["FechaPedido"].ToString());

                        listaPedidosDespacho.Add(new e_Pedidos_Despacho(
                            _numeroSolicituTID,
                            _numeroSolicitudERP,
                            _idInternoSolicitud,
                            _idDestinoPedido,
                            _nombreDestino,
                            _estadoSolicitud,
                            _fechaPedido
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaPedidosDespacho;
        }
    }
}

using Diverscan.MJP.Entidades.Devolutions.SolicitudDevolucion;
using Diverscan.MJP.GestorImpresiones.Utilidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Devolutions
{
    public class da_SolicitudDevolucion
    {
        public List<e_Transportista> ObtenerTransportistas(string idBodega)
        {
            List<e_Transportista> _Transportistas = new List<e_Transportista>();

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerTransportistas");

            dbTse.AddInParameter(dbCommand, "@P_idBodega", DbType.Int32, Convert.ToInt32(idBodega));

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    e_Transportista _Transportista = CargarTransportistas(reader);

                    _Transportistas.Add(_Transportista);
                }
            }
            return _Transportistas;
        }

        private static e_Transportista CargarTransportistas(IDataReader reader)
        {
            try
            {
                string Nombre = reader["Nombre"].ToString();
                int idTransportista = Convert.ToInt32(reader["idTransportista"].ToString());

                var _Transportistas = new e_Transportista(Nombre, idTransportista);

                return _Transportistas;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "ObtenerTransportistas";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_SolicitudDevolucion.cs", "ObtenerTransportistas()", "", lineaError, nombreProcedimiento, "", exError.Message);

                return null;
            }
        }

        public string IngresarSolicitudDevolucion(e_EncabezadoSolicitudDevolucion solicitudDevolucion, DataTable tablaDetalle)
        {
            var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("IngresarSolicitudDevolucion");
            db.AddInParameter(dbCommand, "@P_IdUsuario", DbType.Int32, solicitudDevolucion.IdUsuario);
            db.AddInParameter(dbCommand, "@P_IdTransportista", DbType.Int32, solicitudDevolucion.IdTransportista);
            db.AddInParameter(dbCommand, "@P_IdBodega", DbType.Int32, solicitudDevolucion.IdBodega);

            //se agrega la lista de detalles como parámetro al sp
            SqlParameter tablaDetalles = new SqlParameter();
            tablaDetalles.ParameterName = "@P_DetalleSolicitud";
            tablaDetalles.SqlDbType = SqlDbType.Structured;
            tablaDetalles.Value = tablaDetalle;
            dbCommand.Parameters.Add(tablaDetalles);
            dbCommand.CommandTimeout = 3600;

            db.AddOutParameter(dbCommand, "@P_resultado", DbType.String, 200);
            db.ExecuteNonQuery(dbCommand);

            string resultado = dbCommand.Parameters["@P_resultado"].Value.ToString();

            return resultado;

        }

        public List<e_EncabezadoSolicitudDevolucion> ObtenerSolicitudesDevolucion(string idBodega)
        {
            List<e_EncabezadoSolicitudDevolucion> _solicitudes = new List<e_EncabezadoSolicitudDevolucion>();

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerSolicitudesDevolucion");

            dbTse.AddInParameter(dbCommand, "@P_idBodega", DbType.Int32, Convert.ToInt32(idBodega));

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    e_EncabezadoSolicitudDevolucion solicitud = CargarSolicitudes(reader);

                    _solicitudes.Add(solicitud);
                }
            }
            return _solicitudes;
        }

        private static e_EncabezadoSolicitudDevolucion CargarSolicitudes(IDataReader reader)
        {
            try
            {
                long idSolicitudDevolucion = long.Parse(reader["idSolicitudDevolucion"].ToString());
                string FechaCreacion = reader["FechaCreacion"].ToString();
                string NombreUsuario = reader["NombreUsuario"].ToString();
                
                var _solicitud = new e_EncabezadoSolicitudDevolucion(idSolicitudDevolucion, NombreUsuario, FechaCreacion);

                return _solicitud;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "ObtenerSolicitudesDevolucion";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_SolicitudDevolucion.cs", "ObtenerSolicitudesDevolucion()", "", lineaError, nombreProcedimiento, "", exError.Message);

                return null;
            }
        }

        public List<e_DetalleSolicitudDevolucion> ObtenerDetallesSolicitud(long idSolicitud)
        {
            List<e_DetalleSolicitudDevolucion> _solicitudes = new List<e_DetalleSolicitudDevolucion>();

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerDetallesSolicitud");

            dbTse.AddInParameter(dbCommand, "@P_idSolicitud", DbType.Int64, idSolicitud);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    e_DetalleSolicitudDevolucion solicitud = CargarDetallesSolicitud(reader);

                    _solicitudes.Add(solicitud);
                }
            }
            return _solicitudes;
        }

        private static e_DetalleSolicitudDevolucion CargarDetallesSolicitud(IDataReader reader)
        {
            try
            {
                long idArticulo = long.Parse(reader["idArticulo"].ToString());
                string Nombre = reader["Nombre"].ToString();
                int Cantidad = Convert.ToInt32(reader["Cantidad"].ToString());
                string MARCA = reader["MARCA"].ToString();
                string Placa = reader["Placa"].ToString();
                long idDetalleSolicitudDevolucion = long.Parse(reader["idDetalleSolicitudDevolucion"].ToString());
                string Lote = "NA";
                string FechaVencimiento = "1900-01-01";

                var detalle = new e_DetalleSolicitudDevolucion(idArticulo, Cantidad, Placa, Lote, FechaVencimiento, Nombre, MARCA, idDetalleSolicitudDevolucion);

                return detalle;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "ObtenerDetallesSolicitud";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_SolicitudDevolucion.cs", "ObtenerDetallesSolicitud()", "", lineaError, nombreProcedimiento, "", exError.Message);

                return null;
            }
        }



    }
}

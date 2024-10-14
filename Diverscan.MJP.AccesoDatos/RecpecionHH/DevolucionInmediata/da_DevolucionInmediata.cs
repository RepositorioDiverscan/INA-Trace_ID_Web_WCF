using Diverscan.MJP.Entidades.Recepcion;
using Diverscan.MJP.Utilidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.RecpecionHH.DevolucionInmediata
{
    public class da_DevolucionInmediata
    {
        public List<e_DevolucionInmediata> ObtenerDevolucionInmediata(string idbodega, string idCausa)
        {
            List<e_DevolucionInmediata> _Devoluciones = new List<e_DevolucionInmediata>();

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerDetallesDevolucionInmediata");

            dbTse.AddInParameter(dbCommand, "@P_idBodega", DbType.Int32, Convert.ToInt32(idbodega));
            dbTse.AddInParameter(dbCommand, "@P_idCausa", DbType.Int32, Convert.ToInt32(idCausa));

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    e_DevolucionInmediata e_Devolucion = CargarDevoluciones(reader);

                    _Devoluciones.Add(e_Devolucion);
                }
            }
            return _Devoluciones;
        }

        private static e_DevolucionInmediata CargarDevoluciones(IDataReader reader)
        {
            try
            {

                int idDetallePedidoOriginal = Convert.ToInt32(reader["idDetallePedidoOriginal"].ToString());
                int idPedidoOriginal = Convert.ToInt32(reader["idPedidoOriginal"].ToString());
                int idArticulo = Convert.ToInt32(reader["idArticulo"].ToString());
                int cantidadDevolucion = Convert.ToInt32(reader["CantidadDevolucion"].ToString());
                string nombreArticulo = reader["NombreHH"].ToString();
                bool esActivo = Convert.ToBoolean(reader["Activo"]);

                var e_Devolucion = new e_DevolucionInmediata(idDetallePedidoOriginal, idPedidoOriginal, idArticulo, cantidadDevolucion, nombreArticulo, esActivo);

                return e_Devolucion;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "ObtenerDetallesDevolucionInmediata";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_DevolucionInmediata.cs", "ObtenerPedidos()", "", lineaError, nombreProcedimiento, "", exError.Message);

                return null;
            }
        }

        public string RecibirDevolucionInmediata(e_RecepcionDevolucion e_Recepcion, string idEstadoDevolucion)
        {
            var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("RecibirDevolucionInmediata");
            db.AddInParameter(dbCommand, "@P_IdArticulo", DbType.Int64, e_Recepcion.IdArticulo);
            db.AddInParameter(dbCommand, "@P_Lote", DbType.String, e_Recepcion.Lote);
            db.AddInParameter(dbCommand, "@P_FechaVencimiento", DbType.DateTime, e_Recepcion.FechaVencimiento);
            db.AddInParameter(dbCommand, "@P_IdUbicacion", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "@P_Cantidad", DbType.Int32, e_Recepcion.Cantidad);
            db.AddInParameter(dbCommand, "@P_IdSecuencia", DbType.Int64, e_Recepcion.IdSecuancia);
            db.AddInParameter(dbCommand, "@P_IdEstado", DbType.Int32, e_Recepcion.IdEstado);
            db.AddInParameter(dbCommand, "@P_IdUsuario", DbType.Int32, e_Recepcion.IdUsuario);
            db.AddInParameter(dbCommand, "@P_IdMetodoAccion", DbType.Int32, e_Recepcion.IdMetodoAccion);
            db.AddInParameter(dbCommand, "@P_idEstadoDevolucion", DbType.Int32, Int32.Parse(idEstadoDevolucion));
            db.AddInParameter(dbCommand, "@P_idDetallePedidoOriginal", DbType.Int64, e_Recepcion.IdDetallePedidoOriginal);
            db.AddOutParameter(dbCommand, "@P_respuesta", DbType.String, 200);
            db.ExecuteNonQuery(dbCommand);

            string resultado = dbCommand.Parameters["@P_respuesta"].Value.ToString();

            return resultado;
        }


    }
}

using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SolicitudBODEGA
{
    public class SolicitudBodegaDBA : ISolicitudBodega
    {
        public void InsertarPreDetalle(string nombre, int idPreMaestroSolicitud, string idDestino, string IdInterno, 
            decimal Cantidad, string descripcion, string idCompania, int idUsuario, int numLinea, string gtin)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InsertarDetalleSolicitudBodega");
            dbTse.AddInParameter(dbCommand, "@Nombre", DbType.String, nombre);
            dbTse.AddInParameter(dbCommand, "@idMaestroSolicitud", DbType.Int32, idPreMaestroSolicitud);
            dbTse.AddInParameter(dbCommand, "@idDestino", DbType.String, idDestino);
            dbTse.AddInParameter(dbCommand, "@idInternoArticulo", DbType.String, IdInterno);
            dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Decimal, Cantidad);
            dbTse.AddInParameter(dbCommand, "@Descripcion", DbType.String, descripcion);
            dbTse.AddInParameter(dbCommand, "@IdCompania", DbType.String, idCompania);
            dbTse.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, idUsuario);
            dbTse.AddInParameter(dbCommand, "@numLinea", DbType.Int32, numLinea);
            dbTse.AddInParameter(dbCommand, "@GTIN", DbType.String, gtin);
            dbTse.ExecuteNonQuery(dbCommand);
        }

        public string InsertarPreMaestro(int idUsuario, string nombre, string comentario, string idCompania, 
            string idDestino, string idInterno, string idInternoSAP, DateTime fechaEntrega)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InsertarMaestroSolicitudBodega");
            dbTse.AddInParameter(dbCommand, "@idUsuario", DbType.Int32, idUsuario);
            dbTse.AddInParameter(dbCommand, "@Nombre", DbType.String, nombre);
            dbTse.AddInParameter(dbCommand, "@Comentarios", DbType.String, comentario);
            dbTse.AddInParameter(dbCommand, "@IdCompania", DbType.String, idCompania);
            dbTse.AddInParameter(dbCommand, "@idDestino", DbType.String, idDestino);
            dbTse.AddInParameter(dbCommand, "@idInterno", DbType.String, idInterno);
            dbTse.AddInParameter(dbCommand, "@idInternoSAP", DbType.String, idInternoSAP);
            dbTse.AddInParameter(dbCommand, "@FechaEntrega", DbType.DateTime, fechaEntrega);

            var idMaestroSolicitud = dbTse.ExecuteScalar(dbCommand);

            return Convert.ToString(idMaestroSolicitud);
        }
    }
}

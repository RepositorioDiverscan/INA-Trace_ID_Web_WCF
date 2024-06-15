using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.Devolutions
{
    public class da_RecibirSolicitudDevolucion
    {
        public string ObtenerSolicitudesDevolucion(EDetalleRecibirSolicitudDevolucion detalle, int usuario)
        {

            var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("RecibirSolicitudDevolucion");

            db.AddInParameter(dbCommand, "@P_IdArticulo", DbType.Int32, detalle._IdArticulo);
            db.AddInParameter(dbCommand, "@P_Lote", DbType.String, detalle._Lote);
            db.AddInParameter(dbCommand, "@P_FechaVencimiento", DbType.String, detalle._FechaVencimiento);
            db.AddInParameter(dbCommand, "@P_Cantidad", DbType.Int32, detalle._Cantidad);
            db.AddInParameter(dbCommand, "@P_IdUsuario", DbType.Int32, usuario);
            db.AddInParameter(dbCommand, "@P_idDetalleSolicitudDevolucion", DbType.Int32, detalle._IdDetalleSolicitudDevolucion);

            db.AddOutParameter(dbCommand, "@P_resultado", DbType.String, 200);
            db.ExecuteNonQuery(dbCommand);

            return dbCommand.Parameters["@P_resultado"].Value.ToString();
        }
    }
}

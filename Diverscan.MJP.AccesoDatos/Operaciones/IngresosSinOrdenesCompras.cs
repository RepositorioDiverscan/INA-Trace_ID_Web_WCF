using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Operaciones
{
    public class IngresosSinOrdenesCompras
    {
        public List<eReporteSOC> SinOrdenCompras(int idBodega, DateTime f1, DateTime f2, string numero)
        {

            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_RECEPCION_DOC_SOC");
            db.AddInParameter(dbCommand, "@ordenCompra", DbType.String, numero);
            db.AddInParameter(dbCommand, "@fechaInicioBusqueda", DbType.DateTime, f1);
            db.AddInParameter(dbCommand, "@fechaFinBusqueda", DbType.DateTime, f2);
            db.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);
            //guardar en una lista
            List<eReporteSOC> ListordenCompra = new List<eReporteSOC>();
            try
            {
                using (var reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ListordenCompra.Add(new eReporteSOC(reader));
                    }
                }
            }
            catch (Exception e)
            {
                var p = e.Message;
            }

            return ListordenCompra;
        }

        public List<EDetalleReporteOC> ObtenerDetalleSinOrdenCompras(int id)
        {

            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_GETDETAILWITHOUTPURCHASEORDER");
            db.AddInParameter(dbCommand, "@p_idMaestroSinOrdenCompra", DbType.Int32, id);
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
            var dbCommand = db.GetStoredProcCommand("SP_PROCESSWITHOUTPURCHASEORDER");
            db.AddInParameter(dbCommand, "@p_idMaestroSinOrdenCompra", DbType.Int32, id);
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

    }
}

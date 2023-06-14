using Diverscan.MJP.AccesoDatos.Operacion.DespachoPedidos.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Operaciones
{
    public class DetalleFacturado
    {
        public List<eDetalleFacturado> ObtenerDetalleFacturado(string IdSap)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_GETDETAILPURCHASEORDER_MAESTRO");
            db.AddInParameter(dbCommand, "@idInternoSAP", DbType.String, IdSap);

            List<eDetalleFacturado> ListDetalleFacturado = new List<eDetalleFacturado>();
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    eDetalleFacturado datos = new eDetalleFacturado();
                    datos.IdInternoArticulo = reader["idInternoArticulo"].ToString();
                    datos.numLinea = Convert.ToInt32(reader["numLinea"].ToString());
                    datos.Nombre = reader["Nombre"].ToString();
                    datos.CantidadSolicitada = Convert.ToDecimal(reader["CantidadSolicitada"].ToString());
                    datos.CantidadAlistada = Convert.ToDecimal(reader["CantidadAlistada"].ToString());
                    datos.DocEntry = reader["DocEntry"].ToString();
                    datos.DocNum = reader["DocNum"].ToString();
                    ListDetalleFacturado.Add(datos);
                }
            }
            return ListDetalleFacturado;
        }

        public List<EDetalleFacturadoTrazabilidad> ObtenerDetalleFacturadoTrazabilidad(long idMaestroSolicitud)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("ObtenerDetalleSolicitudFacturados");
            db.AddInParameter(dbCommand, "@idMaestroSolicitud", DbType.Int64, idMaestroSolicitud);

            List<EDetalleFacturadoTrazabilidad> detalleFacturadoTrazaLista = new List<EDetalleFacturadoTrazabilidad>();
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    EDetalleFacturadoTrazabilidad datos = new EDetalleFacturadoTrazabilidad();
                    datos.IdMaestroSolicitud = long.Parse(reader["idMaestroSolicitud"].ToString());
                    datos.IdInternoArticulo = reader["idInternoArticulo"].ToString();
                    datos.Nombre = reader["nombreArticulo"].ToString();
                    datos.Cantidad = Convert.ToDecimal(reader["cantidad"].ToString());
                    datos.Lote = reader["lote"].ToString();
                    datos.FechaVencimiento = DateTime.Parse(reader["fechaVencimiento"].ToString()).ToShortDateString();
                    detalleFacturadoTrazaLista.Add(datos);
                }
            }
            return detalleFacturadoTrazaLista;
        }

        
    }
}

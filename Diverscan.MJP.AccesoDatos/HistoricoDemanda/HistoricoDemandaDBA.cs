using Diverscan.MJP.Entidades.HistoricoDemanda;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.HistoricoDemanda
{
    public class HistoricoDemandaDBA
    {

        public List<HistoricoDemandaRecord> ObtenerHistoricoDemandaArticuloFechas(DateTime fechaIni, DateTime fechaFin, long idArticulo, long idProveedor, bool filtroPorArticulo)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            //var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerHistoricoDemandaArticuloFechas");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerHistoricoDemandaArticuloFechasV2");
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, fechaIni);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, fechaFin);
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@IdProveedor", DbType.Int64, idProveedor);
            dbTse.AddInParameter(dbCommand, "@FiltroPorLote", DbType.Boolean, filtroPorArticulo);

            var hdList = new List<HistoricoDemandaRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    hdList.Add(new HistoricoDemandaRecord(reader));
                }
            }
            return hdList;
        }

        public List<HistoricoDemandaProveedorRecord> ObtenerHistoricoDemandaProveedorFechas(DateTime fechaIni, DateTime fechaFin, long idProveedor)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            //var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerHistoricoDemandaProveedorFechas");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerHistoricoDemandaProveedorFechasV2");
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, fechaIni);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, fechaFin);
            dbTse.AddInParameter(dbCommand, "@IdProveedor", DbType.Int64, idProveedor);

            var hdList = new List<HistoricoDemandaProveedorRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    hdList.Add(new HistoricoDemandaProveedorRecord(reader));
                }
            }

            return hdList;
        }

        //Genera un historico de pedidos mediante el código interno del artículo 
        public List<HistoricoDemandaRecord> ObtenerHistoricoDemandaArticuloIdInternoFechas(string idInternoArticulo, DateTime fechaIni, DateTime fechaFin)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");            
            var dbCommand = dbTse.GetStoredProcCommand("SP_Historico_Demanda_Por_IdArticulo_ERP");
            dbTse.AddInParameter(dbCommand, "@PIdInternoArticulo", DbType.String, idInternoArticulo);
            dbTse.AddInParameter(dbCommand, "@PFechaInicio", DbType.DateTime, fechaIni);
            dbTse.AddInParameter(dbCommand, "@PFechaFin", DbType.DateTime, fechaFin);      

            var hdList = new List<HistoricoDemandaRecord>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        hdList.Add(new HistoricoDemandaRecord(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
            return hdList;
        }
    }
}

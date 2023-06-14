using Diverscan.MJP.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.OrdenCompa
{
    public class da_PreDetalleSolicitud
    {
        public List<e_PreDetalleSolicitud> GetPreDetalleSolicitud(long idMaestroSolicitud, int idBodega) 
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("sp_OPESALPreDetalleSolicitudPoridMaestroSolicitud");

            dbTse.AddInParameter(dbCommand, "@idMaestroSolicitud", DbType.Int64, idMaestroSolicitud);
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int64, idBodega);

            List<e_PreDetalleSolicitud> ListPreDetalleSolicitud = new List<e_PreDetalleSolicitud>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string idInternoArticulo = reader["idInternoArticulo"].ToString();
                    decimal Cantidad = Convert.ToDecimal(reader["Cantidad"].ToString());
                    long MaestroSolicitudId = Convert.ToInt64(reader["idMaestroSolicitud"].ToString());

                    ListPreDetalleSolicitud.Add(new e_PreDetalleSolicitud(idInternoArticulo, Cantidad, MaestroSolicitudId));
                }
            }
            return ListPreDetalleSolicitud;
        }

        public string ProcesarPreDetalleSolicitud(List<e_PreDetalleSolicitud> listPreDetalleSolicitud)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            DataSet DB = new DataSet();
            string result = "";

            try
            {

                foreach (e_PreDetalleSolicitud data in listPreDetalleSolicitud)
                {
                    var dbCommand = dbTse.GetStoredProcCommand("SP_ProcesarPreDetalleSolicitud");
                    dbTse.AddInParameter(dbCommand, "@idInterno", DbType.Int64, data.IdInternoArticulo);
                    dbTse.AddInParameter(dbCommand, "@CantKg", DbType.Decimal, data.Cantidad1);
                    dbTse.AddInParameter(dbCommand, "@idMaestroSolicitud", DbType.Decimal, data.MaestroSolicitudId);

                    dbCommand.CommandTimeout = 3600;

                    DB = dbTse.ExecuteDataSet(dbCommand);
                    if (DB.Tables[0].Rows.Count > 0)
                        result += DB.Tables[0].Rows[0][0].ToString() + " ";
                   
                }
            }

            catch (Exception ex)
            {
                return result += ex.Message;
            }

            return result;
        }

        public List<e_OPESALDetalleSolicitud> GetDetalleSolicitudParaAporbarSalida(long idMaestroSolicitud, int idBodega) 
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("sp_GetDetalleSolicitudParaAporbarSalida");

            dbTse.AddInParameter(dbCommand, "@idMaestroSolicitud", DbType.Int64, idMaestroSolicitud);
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);

            List<e_OPESALDetalleSolicitud> ListOPESALDetalleSolicitud = new List<e_OPESALDetalleSolicitud>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    long idLineaDetalleSolicitud = Convert.ToInt64(reader["idLineaDetalleSolicitud"].ToString());
                    Single Cantidad = Convert.ToSingle(reader["Cantidad"].ToString());

                    ListOPESALDetalleSolicitud.Add(new e_OPESALDetalleSolicitud(idLineaDetalleSolicitud, Cantidad));
                }
            }
            return ListOPESALDetalleSolicitud;
        }
    }
}

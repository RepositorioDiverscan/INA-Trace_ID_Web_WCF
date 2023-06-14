using Diverscan.MJP.AccesoDatos.Invoiced;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Emo
{
    public class DEmo
    {
        public List<EEmo> BuscarEmo(long idWarehouse, long idTransportista, DateTime dateInit, DateTime dateEnd, string idInternoSAP)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("BuscarEmo");
            dbTse.AddInParameter(dbCommand, "@idWarehouse", DbType.Int64, idWarehouse);
            dbTse.AddInParameter(dbCommand, "@idTransportista", DbType.Int64, idTransportista);
            dbTse.AddInParameter(dbCommand, "@dateInit", DbType.DateTime, dateInit);
            dbTse.AddInParameter(dbCommand, "@dateEnd", DbType.DateTime, dateEnd);
            dbTse.AddInParameter(dbCommand, "@idEmo", DbType.String, idInternoSAP);

            List<EEmo> emoList = new List<EEmo>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    emoList.Add(new EEmo(reader));
                }
            }

            return emoList;
        }

        public List<EEmo> BuscarEmo(int idRecord, int top)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("BuscarEmoSincronizador");
            dbTse.AddInParameter(dbCommand, "@idRecord", DbType.Int64, idRecord);
            dbTse.AddInParameter(dbCommand, "@top", DbType.Int64, top);
           

            List<EEmo> emoList = new List<EEmo>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    EEmo eEmo = new EEmo();
                    eEmo.IdEmo = long.Parse(reader["idEmo"].ToString());
                    eEmo.IdTransportista = long.Parse(reader["idTransportista"].ToString());
                    eEmo.NombreTransportista = reader["Nombre"].ToString();
                    eEmo.IdBodega = long.Parse(reader["idBodega"].ToString());
                    eEmo.NombreUsuario = reader["usuario"].ToString();
                    eEmo.IdInterno = reader["idInterno"].ToString();
                    eEmo.RecordDate = Convert.ToDateTime(reader["fechaCreacion"].ToString());
                    eEmo.TotalPeso = decimal.Parse(reader["totalPeso"].ToString());
                    eEmo.TotalMonto = decimal.Parse(reader["totalMonto"].ToString());
                    bool resultParse = false;
                    eEmo.State = Boolean.TryParse(reader["estado"].ToString(), out resultParse);
                    eEmo.DetailEmo = new List<EInvoiced>();

                    emoList.Add(eEmo);
                }
            }

            return emoList;
        }

        public long CreateEmo(long idWarehouse, long idTransportista, int idUser)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("CreateEmo");
            dbTse.AddInParameter(dbCommand, "@idWarehouse", DbType.Int64, idWarehouse);
            dbTse.AddInParameter(dbCommand, "@idTransportista", DbType.Int64, idTransportista);
            dbTse.AddInParameter(dbCommand, "@idUser", DbType.Int32, idUser);

            long idEmo = -1;

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    idEmo = long.Parse(reader["idEmo"].ToString());
                }
            }

            return idEmo;
        }

        public string InsertInvoicesByEmo(long idEmo, DataTable facturas)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("InsertInvoicesByEmo");
            dbTse.AddInParameter(dbCommand, "@idEmo", DbType.Int64, idEmo);
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@facturas";
            parameter.SqlDbType = System.Data.SqlDbType.Structured;
            parameter.Value = facturas;
            dbCommand.Parameters.Add(parameter);
            string result = "";
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    result = reader["result"].ToString() ;
                }
            }

            return result;
        }

        public List<EInvoiced> GetInvoicesByEmo(long idEmo)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("BuscarInvoicesXEmo");
            dbTse.AddInParameter(dbCommand, "@idEmo", DbType.Int64, idEmo);
            SqlParameter parameter = new SqlParameter();

            List<EInvoiced> result = new List<EInvoiced>(); 
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    EInvoiced temp = new EInvoiced();
                    temp.IdRecord = long.Parse(reader["idInvoiced"].ToString());
                    temp.IdSap = reader["IdSap"].ToString();
                    temp.BillNumber = reader["BillNumber"].ToString();
                    temp.BillPrice = Convert.ToDecimal(reader["BillPrice"].ToString());
                    temp.Weight = Convert.ToDecimal(reader["weight"].ToString());
                    temp.Volume = Convert.ToDecimal(reader["volume"].ToString());
                    temp.RecordDate = Convert.ToDateTime(reader["RecordDate"].ToString());
                    temp.IdOla = long.Parse(reader["idRegistroOla"].ToString());
                    temp.NameClient = reader["Nombre"].ToString();
                    temp.NumberPage = Convert.ToInt32(reader["NumberPage"].ToString());

                    result.Add(temp);
                }
            }

            return result;
        }

        public List<EInvoiced> GetInvoicesByEmoSynchronizer(long idEmo)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("BuscarInvoicesXEmoSynchronizer");
            dbTse.AddInParameter(dbCommand, "@idEmo", DbType.Int64, idEmo);
            SqlParameter parameter = new SqlParameter();

            List<EInvoiced> result = new List<EInvoiced>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    EInvoiced temp = new EInvoiced();
                    temp.IdRecord = long.Parse(reader["idInvoiced"].ToString());
                    temp.IdSap = reader["IdSap"].ToString();
                    temp.BillNumber = reader["BillNumber"].ToString();
                    temp.BillPrice = Convert.ToDecimal(reader["BillPrice"].ToString());
                    temp.TypeBill = reader["TypeBill"].ToString();
                    temp.Weight = Convert.ToDecimal(reader["weight"].ToString());
                    temp.Volume = Convert.ToDecimal(reader["volume"].ToString());
                    temp.RecordDate = Convert.ToDateTime(reader["RecordDate"].ToString());
                    temp.IdOla = long.Parse(reader["idRegistroOla"].ToString());
                    temp.NameClient = reader["Nombre"].ToString();
                    temp.CodeClient = reader["CodeClient"].ToString();

                    result.Add(temp);
                }
            }

            return result;
        }
    }
}

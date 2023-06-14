using Diverscan.MJP.Entidades.OrdenCompra;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.OrdenCompra
{
    public class GetPurchaseOrderDBA
    {
        public List<EGetPucharseOrder> GetPurchaseOrder(string fecha, string idBodega)
        {
         
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_GetPurchaseOrder_Mobile");

                dbTse.AddInParameter(dbCommand, "@Fecha", DbType.DateTime, fecha);
                dbTse.AddInParameter(dbCommand, "@p_IdBodega", DbType.Int32, Convert.ToInt32(idBodega));

                List<EGetPucharseOrder> mAIList = new List<EGetPucharseOrder>();
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        mAIList.Add(new EGetPucharseOrder(reader));
                    }
                }
                return mAIList;                      
        }
        
          public List<EDetailPurchaseOrder> GetDetailPurchaseOrder(string IdMOC, string TipoIngreso)
        {

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_GetPurchaseOrderDetailsMobileV2");

            dbTse.AddInParameter(dbCommand, "@idMOC", DbType.Int64, IdMOC);
            dbTse.AddInParameter(dbCommand, "@TipoIngreso", DbType.Int16, TipoIngreso);

            List<EDetailPurchaseOrder> mAIList = new List<EDetailPurchaseOrder>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    mAIList.Add(new EDetailPurchaseOrder(reader));
                }
            }
            return mAIList;

        }

        public string UpdateCertificateOC(int idMOC, string certificate)
        {
            string respuesta = "";
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_UpdateCertificateOC");

            dbTse.AddInParameter(dbCommand, "@idMOC", DbType.Int32, idMOC);
            dbTse.AddInParameter(dbCommand, "@numeroCertificado", DbType.String, certificate);

            using (IDataReader reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    respuesta = reader["Resultado"].ToString();
                }
            }

            return respuesta;
        }

        public List<EGetPucharseOrder> GetOnePurchaseOrder( string IdInterno, string idBodega)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_GETPUCHARSEONEORDER");
            db.AddInParameter(dbCommand, "@p_IdInterno", DbType.String, IdInterno);
            db.AddInParameter(dbCommand, "@p_IdBodega", DbType.Int32, Convert.ToInt32(idBodega));

            List<EGetPucharseOrder> mAIList = new List<EGetPucharseOrder>();
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    mAIList.Add(new EGetPucharseOrder(reader));
                }
            }

            return mAIList;
        }

        public string UpdateBillOC(int idMOC, string numberBill)
        {
            string respuesta = "";
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_UpdateBillOC");

            dbTse.AddInParameter(dbCommand, "@idMOC", DbType.Int32, idMOC);
            dbTse.AddInParameter(dbCommand, "@numeroBill", DbType.String, numberBill);

            using (IDataReader reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    respuesta = reader["Resultado"].ToString();
                }
            }

            return respuesta;
        }
    }
}

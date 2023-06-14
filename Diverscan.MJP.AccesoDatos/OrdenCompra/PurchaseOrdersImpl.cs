using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades.OrdenCompra;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.OrdenCompra
{
    public class PurchaseOrdersImpl : IPurchaseOrders
    {
        public List<e_OrdenCompra> GetPurchaseOrderDetails(string PurchaseOrderId)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("GetPurchaseOrderDetails");

                dbTse.AddInParameter(dbCommand, "@idOrdenCompra", DbType.String, PurchaseOrderId);

                List<e_OrdenCompra> mAIList = new List<e_OrdenCompra>();
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        mAIList.Add(
                            new e_OrdenCompra(reader));
                    }
                }
                return mAIList;
            }
            catch(Exception)
            {
                return new List<e_OrdenCompra>();
            }
        }
    }
}

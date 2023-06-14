using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Invoiced
{
    public class DInvoiced
    {
        public List<EInvoiced> BuscarInvoiced(long idWarehouse, long idTransportista, DateTime dateInit, DateTime dateEnd, string idInternoSAP)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("BuscarInvoiced");
            dbTse.AddInParameter(dbCommand, "@idWarehouse", DbType.Int64, idWarehouse);
            dbTse.AddInParameter(dbCommand, "@idTransportista", DbType.Int64, idTransportista);
            dbTse.AddInParameter(dbCommand, "@dateInit", DbType.DateTime, dateInit);
            dbTse.AddInParameter(dbCommand, "@dateEnd", DbType.DateTime, dateEnd);
            dbTse.AddInParameter(dbCommand, "@idInternoSAP", DbType.String, idInternoSAP);

            List<EInvoiced> invoicedList = new List<EInvoiced>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    invoicedList.Add(new EInvoiced(reader));
                }
            }
            return invoicedList;
        }
    }
}

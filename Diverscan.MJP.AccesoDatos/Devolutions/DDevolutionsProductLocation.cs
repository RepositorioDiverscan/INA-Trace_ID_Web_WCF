using Diverscan.MJP.Entidades.Devolutions.DevolutionProductLocation;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Devolutions
{
    public class DDevolutionsProductLocation
    {
        public void insertProductLocation(EDevolutionProduct eDevolutionProduct)
        {
            //SP_InsertProductLocation

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InsertProductLocation");

            foreach (EProductLocation temp in eDevolutionProduct.ProductLocations)
            {
                dbTse.AddInParameter(dbCommand, "@id_devolution_header", DbType.Int64, eDevolutionProduct.IdOrderDevolution);
                dbTse.AddInParameter(dbCommand, "@id_product", DbType.Int64, eDevolutionProduct.IdProduct);
                dbTse.AddInParameter(dbCommand, "@location", DbType.Int64, temp.NameLocation);
                dbTse.AddInParameter(dbCommand, "@lote", DbType.String, eDevolutionProduct.Lote);
                dbTse.AddInParameter(dbCommand, "@date_expery", DbType.String, eDevolutionProduct.DateExpery);
                dbTse.AddInParameter(dbCommand, "@quantity", DbType.Int64, temp.QuantityToLocate);                                             
                dbTse.AddInParameter(dbCommand, "@bar_code", DbType.String, eDevolutionProduct.BarCode);

                dbTse.ExecuteReader(dbCommand);
            }
        }
    }
}

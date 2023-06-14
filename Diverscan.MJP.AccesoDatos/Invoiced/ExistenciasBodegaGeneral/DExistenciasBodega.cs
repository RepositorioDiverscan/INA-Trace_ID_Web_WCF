using Diverscan.MJP.AccesoDatos.Invoiced.ExtenciasBodegaGeneral;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Invoiced.ExistenciasBodegaGeneral
{
    public class DExistenciasBodega
    {
        public List<EExistenciasBodega> ObtenerExistenciasBodegaGeneral(long idWarehouse)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerExistenciasBodegaGeneral");
            dbTse.AddInParameter(dbCommand, "@idWarehouse", DbType.Int64, idWarehouse);


            List<EExistenciasBodega>  eExistenciaslist = new List<EExistenciasBodega>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    eExistenciaslist.Add(new EExistenciasBodega(reader));
                }
            }
            return eExistenciaslist;
        }
    }
}

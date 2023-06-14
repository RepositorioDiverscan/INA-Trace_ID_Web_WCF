using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Recaudo
{
    public class DRecaudoDetalle
    {
        public List<ERecaudoDetalle> GetRecaudoDetalle(long idRecaudo)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("Sp_Bus_DetallePorRecaudo_VP");
            db.AddInParameter(dbCommand, "@IdRecaudo", DbType.Int64, idRecaudo);
            List<ERecaudoDetalle> recaudoDetalleList = new List<ERecaudoDetalle>();
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ERecaudoDetalle recaudoDetalle = new ERecaudoDetalle(reader);
                    recaudoDetalleList.Add(recaudoDetalle);
                }
            }

            return recaudoDetalleList;
        }
    }
}

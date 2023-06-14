using Diverscan.MJP.Entidades.Calidad;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Calidad
{
    public class RecepcionDBA
    {
        public List<RecepcionRecord> ObtenerCalidadRecepciónOC(long idArticulo, string lote)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerCalidadRecepciónOC");

            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, lote);

            List<RecepcionRecord> TRAList = new List<RecepcionRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    TRAList.Add(new RecepcionRecord(reader));
                }
            }
            return TRAList;
        }
    }
}

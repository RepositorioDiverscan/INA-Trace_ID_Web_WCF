using Diverscan.MJP.Entidades.PICKING;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.PICKING
{
    public class ConsultaArticulosPicking
    {
        public List<PickingRecord> GetMotivoAjusteInvertario()
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerTodosArticulosPicking");
          
            List<PickingRecord> mAIList = new List<PickingRecord>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    mAIList.Add(new PickingRecord(reader));
                }
            }
            return mAIList;
        }
    }
}



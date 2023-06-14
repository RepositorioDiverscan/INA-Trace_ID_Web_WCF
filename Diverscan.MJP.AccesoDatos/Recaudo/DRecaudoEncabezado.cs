using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Recaudo
{
    public class DRecaudoEncabezado
    {
        public List<ERecaudoEncabezado> GetRecaudoEncabezado(long idJornada)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("Sp_Bus_RecaudoPorJornada_VP");
            db.AddInParameter(dbCommand, "@IdJornada", DbType.Int64, idJornada);    
            List<ERecaudoEncabezado> recaudoEncabezadoList = new List<ERecaudoEncabezado>();
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ERecaudoEncabezado jornadaRecaudo = new ERecaudoEncabezado(reader);
                    recaudoEncabezadoList.Add(jornadaRecaudo);
                }
            }

            return recaudoEncabezadoList;
        }
    }
}

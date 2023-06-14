using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.MotivoAjusteInventario
{
    public class ConsultaMotivoAjusteInvertario
    {
        public List<MotivoAjusteInventarioRecord> GetMotivoAjusteInvertario(bool tipoAjuste)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("Obtener_MotivoAjusteInventario");

            dbTse.AddInParameter(dbCommand, "@TipoAjuste", DbType.Boolean, tipoAjuste);        

            List<MotivoAjusteInventarioRecord> mAIList = new List<MotivoAjusteInventarioRecord>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    mAIList.Add(new MotivoAjusteInventarioRecord(reader));
                }
            }
            return mAIList;
        }

        public List<MotivoAjusteInventarioRecord> GetAllMotivoAjusteInvertario(bool tipoAjuste)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerTodos_MotivoAjusteInventario");

            dbTse.AddInParameter(dbCommand, "@TipoAjuste", DbType.Boolean, tipoAjuste);

            List<MotivoAjusteInventarioRecord> mAIList = new List<MotivoAjusteInventarioRecord>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    mAIList.Add(new MotivoAjusteInventarioRecord(reader));
                }
            }
            return mAIList;
        }        
    }
}

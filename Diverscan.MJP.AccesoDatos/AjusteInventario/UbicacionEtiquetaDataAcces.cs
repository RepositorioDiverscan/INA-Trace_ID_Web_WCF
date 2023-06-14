using Diverscan.MJP.Entidades.CustomException;
using Diverscan.MJP.Entidades.Invertario;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.AjusteInventario
{
    public class UbicacionEtiquetaDataAcces : IUbicacionEtiquetaDataAcces
    {
        public long GetIdUbicacion(string etiqueta)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("Obtener_IdUbicacion_Etiqueta");

            dbTse.AddInParameter(dbCommand, "@Etiqueta", DbType.String, etiqueta);

            List<long> idList = new List<long>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    idList.Add(long.Parse(reader["IdRegistro"].ToString()));
                }
            }
            if (idList.Count > 1)
                throw new MoreThanOneRecordException("Existe mas de un requistro para la misma etiqueta");
          if (idList.Count==1)
            return idList[0];
          throw new RecordNotFoundException("No existe un requistro para la etiqueta");
        }
               
    }
}

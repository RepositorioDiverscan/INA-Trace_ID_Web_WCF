using Diverscan.MJP.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.AjusteInventario
{
    public class AjusteDeInventarioDataAcces
    {
        public void InsertLogAjusteDeInventario(LogAjusteDeInventario logAjusteDeInventario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("IngresarLogAjusteDeInventario");

            dbTse.AddInParameter(dbCommand, "@IdTRAIngresoSalidaArticulos", DbType.Int64, logAjusteDeInventario.IdTRAIngresoSalidaArticulos);
            dbTse.AddInParameter(dbCommand, "@IdAjusteInventario", DbType.Int64, logAjusteDeInventario.IdAjusteInventario);
            dbTse.AddInParameter(dbCommand, "@FechaRegistro", DbType.DateTime, logAjusteDeInventario.FechaRegistro);
            dbTse.AddInParameter(dbCommand, "@Estado", DbType.Int32, logAjusteDeInventario.Estado);
            var result = dbTse.ExecuteNonQuery(dbCommand);
            
        }
    }
}

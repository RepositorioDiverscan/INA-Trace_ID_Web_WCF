using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades.Reportes.Kardex;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    public class da_kardexSKU
    {
        public List<e_kardexSKU> ObtenerKardex(int idBodega, string Sku, string Lote, bool Transitos, DateTime f1, DateTime f2)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerKardexSKUDetalleTra");
            dbTse.AddInParameter(dbCommand, "@BodegaId", DbType.Int32, idBodega);
            dbTse.AddInParameter(dbCommand, "@Sku", DbType.String, Sku);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, Lote);
            dbTse.AddInParameter(dbCommand, "@Transitos", DbType.Boolean, Transitos);
            dbTse.AddInParameter(dbCommand, "@Inicio", DbType.DateTime, f1);
            dbTse.AddInParameter(dbCommand, "@Final", DbType.DateTime, f2);

            //Guardar en una lista
            List<e_kardexSKU> listaKardex = new List<e_kardexSKU>();

            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        listaKardex.Add(new e_kardexSKU(reader));
                    }
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            return listaKardex;
        }
    }
}

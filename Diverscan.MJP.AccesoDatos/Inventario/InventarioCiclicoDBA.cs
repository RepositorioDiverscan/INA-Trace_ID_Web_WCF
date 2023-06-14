using Diverscan.MJP.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Inventario
{
    public class InventarioCiclicoDBA
    {
        public void InsertInventarioCiclico(e_InventarioCiclicoRecord e_inventarioCiclicoRecord)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_IngresarInventarioCiclico");

            dbTse.AddInParameter(dbCommand, "@IdCategoriaArticulo", DbType.Int32, e_inventarioCiclicoRecord.IdCategoriaArticulo);
            dbTse.AddInParameter(dbCommand, "@FechaPorAplicar", DbType.DateTime, e_inventarioCiclicoRecord.FechaPorAplicar);
            var result = dbTse.ExecuteNonQuery(dbCommand);
        }

        public void InsertInventarioCiclico(List<e_InventarioCiclicoRecord> e_inventarioCiclicoData)
        {
            foreach (var e_inventarioCiclicoRecord in e_inventarioCiclicoData)
            {
                InsertInventarioCiclico(e_inventarioCiclicoRecord);
            }
        }

        public List<e_InventarioCiclicoRecord> GetInventariosCiclicos(DateTime fechaInicio, DateTime fechaFin)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_InventarioCiclico");

            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, fechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, fechaFin);      

            List<e_InventarioCiclicoRecord> inventarioList = new List<e_InventarioCiclicoRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    long idInventario = long.Parse(reader["IdInventario"].ToString());
                    int idCategoriaArticulo = int.Parse(reader["IdCategoriaArticulo"].ToString());
                    string nombreCategoria = reader["NombreCategoria"].ToString();
                    DateTime fechaPorAplicar = DateTime.Parse(reader["FechaPorAplicar"].ToString());
                    
                    inventarioList.Add(new e_InventarioCiclicoRecord(idInventario, idCategoriaArticulo, nombreCategoria, fechaPorAplicar));
                }
            }
            return inventarioList;
        
        }
    }
}

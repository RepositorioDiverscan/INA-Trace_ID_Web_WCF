using Diverscan.MJP.Entidades.Invertario;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Inventario
{
    public class UbicacionesInventarioCiclicoDBA
    {
        public List<UbicacionesInventarioCiclicoRecord> ObtenerUbicacionesInventarioCiclico(long idInventario, int estado)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerUbicacionesInventarioCiclico");


            dbTse.AddInParameter(dbCommand, "@IdInventario", DbType.Int64, idInventario);
            dbTse.AddInParameter(dbCommand, "@Estado", DbType.Int32, estado);

            List<UbicacionesInventarioCiclicoRecord> ubicaciones = new List<UbicacionesInventarioCiclicoRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ubicaciones.Add(new UbicacionesInventarioCiclicoRecord(reader));
                }
            }
            return ubicaciones;
        }

        public void Update_UbicacionesRealizarInventarioCiclico(long idUbicacionesInventario, int estado)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Update_UbicacionesRealizarInventarioCiclico");

            dbTse.AddInParameter(dbCommand, "@IdUbicacionesInventario", DbType.Int64, idUbicacionesInventario);
            dbTse.AddInParameter(dbCommand, "@Estado", DbType.Int32, estado);
            var result = dbTse.ExecuteNonQuery(dbCommand);
        }

        public void ActualizarArticulosInventarioCiclicoRealizar(long idArticulosInventarioCiclico, int estado)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_UpdateArticulosInventarioCiclicoRealizar");

            dbTse.AddInParameter(dbCommand, "@IdArticulosInventarioCiclico", DbType.Int64, idArticulosInventarioCiclico);
            dbTse.AddInParameter(dbCommand, "@Estado", DbType.Int32, estado);
            var result = dbTse.ExecuteNonQuery(dbCommand);
        }
    }
}

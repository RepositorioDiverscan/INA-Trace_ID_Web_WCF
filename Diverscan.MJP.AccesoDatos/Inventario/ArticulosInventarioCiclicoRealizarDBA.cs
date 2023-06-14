using Diverscan.MJP.Entidades.Invertario;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Inventario
{
    public class ArticulosInventarioCiclicoRealizarDBA
    {
        public List<ArticuloCiclicoRealizarRecord> ObtenerArticulosInventarioCiclicoRealizar(long idInventario, int estado)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerArticulosInventarioCiclicoRealizar");

             dbTse.AddInParameter(dbCommand, "@IdInventario", DbType.Int64, idInventario);
            dbTse.AddInParameter(dbCommand, "@Estado", DbType.Int32, estado);

            List<ArticuloCiclicoRealizarRecord> articulosList = new List<ArticuloCiclicoRealizarRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    long idArticulosInventarioCiclico = long.Parse(reader["IdArticulosInventarioCiclico"].ToString());
                    long idArticulo = long.Parse(reader["IdArticulo"].ToString());
                    string nombre = reader["Nombre"].ToString();

                    articulosList.Add(new ArticuloCiclicoRealizarRecord(idArticulo, nombre, idArticulosInventarioCiclico));
                }
            }
            return articulosList;
        }
    }
}

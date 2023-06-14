using Diverscan.MJP.Entidades.Invertario;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Inventario
{
    public class CopiaSistemaArticuloCiclicoDBA
    {
        public List<ArticulosDisponibles> ObtenerArticulosCopiaSistema(long idInventario, long idArticulo)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerArticulosCopiaSistemaDetalleCiclico");
            dbTse.AddInParameter(dbCommand, "@IdInventarioCiclico", DbType.Int64, idInventario);
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);

            List<ArticulosDisponibles> cantidadList = new List<ArticulosDisponibles>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    cantidadList.Add(new ArticulosDisponibles(reader));
                }
            }
            return cantidadList;
        }

        public List<ArticulosDisponibles> ObtenerTodosArticulosCopiaSistema(long idInventario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerTodosArticulosCopiaSistemaDetalleCiclico");
            dbTse.AddInParameter(dbCommand, "@IdInventarioCiclico", DbType.Int64, idInventario);            

            List<ArticulosDisponibles> cantidadList = new List<ArticulosDisponibles>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    cantidadList.Add(new ArticulosDisponibles(reader));
                }
            }
            return cantidadList;
        }
    }
}

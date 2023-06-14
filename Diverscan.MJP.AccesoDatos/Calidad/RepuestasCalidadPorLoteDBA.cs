using Diverscan.MJP.Entidades.Calidad;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Calidad
{
    public class RepuestasCalidadPorLoteDBA
    {
        public List<RespuestasCalidad> ObtenerRepuestasCalidadPorLote(long idArticulo, string lote)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerRepuestasCalidadPorLote");

            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, lote);

            List<RespuestasCalidad> TRAList = new List<RespuestasCalidad>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    TRAList.Add(new RespuestasCalidad(reader));
                }
            }
            return TRAList;
        }



        public List<RespuestasCalidad> ObtenerRepuestasCalidadPorFechaVencimiento(long idArticulo, DateTime dateTime)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerRepuestasCalidadPorFechaVencimiento");

            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.DateTime, dateTime);

            List<RespuestasCalidad> TRAList = new List<RespuestasCalidad>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    TRAList.Add(new RespuestasCalidad(reader));
                }
            }
            return TRAList;
        }
    }
}


using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.SectorWarehouse
{
    public class DSectorWarehouse
    {
        public List<ESectorWarehouse> GetSectorsWarehouse(int idWarehouse)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_GetSetorsWareHouse");

            dbTse.AddInParameter(dbCommand, "@idWarehouse", DbType.String, idWarehouse);

            List<ESectorWarehouse> sectorsWarehouseList = new List<ESectorWarehouse>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    sectorsWarehouseList.Add(new ESectorWarehouse(reader));
                }
            }
            return sectorsWarehouseList;
        }

        public string InsertSectorWarehouse(ESectorWarehouse sectorWarehouse)
        {

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InsertSectorWareHouse");

            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, sectorWarehouse.IdBodega);
            dbTse.AddInParameter(dbCommand, "@nombre", DbType.String, sectorWarehouse.Name);
            dbTse.AddInParameter(dbCommand, "@descripcion", DbType.String, sectorWarehouse.Description);
            dbTse.AddInParameter(dbCommand, "@activo", DbType.Boolean, sectorWarehouse.Active);

            string result = "";
            using (IDataReader reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    result = reader["Result"].ToString();
                }
            }

            return result;
        }

        public string UpDateSectorWarehouse(ESectorWarehouse sectorWarehouse)
        {

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_UpdateSectorWarehouse");

            dbTse.AddInParameter(dbCommand, "@idSectorWareHouse", DbType.Int32, sectorWarehouse.IdSectorWarehouse);
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, sectorWarehouse.IdBodega);
            dbTse.AddInParameter(dbCommand, "@nombre", DbType.String, sectorWarehouse.Name);
            dbTse.AddInParameter(dbCommand, "@descripcion", DbType.String, sectorWarehouse.Description);
            dbTse.AddInParameter(dbCommand, "@activo", DbType.Boolean, sectorWarehouse.Active);

            string result = "";
            using (IDataReader reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    result = reader["Result"].ToString();
                }
            }

            return result;
        }

        public List<EEStanteWarehouse> GetEstanteWarehouse(int idWarehouse)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerEstantesPorBodega");

            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idWarehouse);

            List<EEStanteWarehouse> estanteWarehouseList = new List<EEStanteWarehouse>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    estanteWarehouseList.Add(new EEStanteWarehouse(reader));
                }
            }
            return estanteWarehouseList;
        }


        public List<EUbicacionesXEstante> GetEstanteXPasilloWarehouse(string pasillo, int idBodega)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerUbicacionesPorEstante");

            dbTse.AddInParameter(dbCommand, "@Estante", DbType.String, pasillo);
            dbTse.AddInParameter(dbCommand, "@IdBodega", DbType.Int32, idBodega);

            List<EUbicacionesXEstante> estanteWarehouseList = new List<EUbicacionesXEstante>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    estanteWarehouseList.Add(new EUbicacionesXEstante(reader));
                }
            }
            return estanteWarehouseList;
        }

        public List<EUbicacionXSector> GetUbicacionXSectorWarehouse(int sector)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerUbicacionesPorSectorBodega");

            dbTse.AddInParameter(dbCommand, "@idSectorBodega", DbType.Int32, sector);

            List<EUbicacionXSector> sectorsWarehouseList = new List<EUbicacionXSector>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    sectorsWarehouseList.Add(new EUbicacionXSector(reader));
                }
            }
            return sectorsWarehouseList;
        }


        public string InsertUbicacionXSectorWarehouse(int sector,int idUbicacion)
        {
            string Resultado = "";
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("InsertUbicacionPorSectorBodega");

            dbTse.AddInParameter(dbCommand, "@idUbicacion", DbType.Int32, idUbicacion);
            dbTse.AddInParameter(dbCommand, "@idSectorBodega", DbType.Int32, sector);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    Resultado= reader["Result"].ToString();
                }
            }
            return Resultado;
        }

        public string DeleteUbicacionXSectorWarehouse(int sector, int idUbicacion)
        {
            string Resultado = "";
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("DeleteUbicacionPorSectorBodega");

            dbTse.AddInParameter(dbCommand, "@idUbicacion", DbType.Int32, idUbicacion);
            dbTse.AddInParameter(dbCommand, "@idSectorBodega", DbType.Int32, sector);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    Resultado = reader["estante"].ToString();
                }
            }
            return Resultado;
        }

    }
}

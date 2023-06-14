using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.SectorWarehouse
{
    public class NSectorWareHouse
    {
        private IFileExceptionWriter _fileExceptionWriter;
        private DSectorWarehouse _dSectorWarehouse;

        public NSectorWareHouse(IFileExceptionWriter fileExceptionWriter)
        {
            _fileExceptionWriter = fileExceptionWriter;
            _dSectorWarehouse = new DSectorWarehouse();
        }

        public List<ESectorWarehouse> GetSectorsWarehouse(int idWarehouse)
        {
            try
            {
                return _dSectorWarehouse.GetSectorsWarehouse(idWarehouse);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SECTORWAREHOUSEFILEPATHEXCEPTION);
                return null;
            }
        }

        public string InsertSectorWarehouse(ESectorWarehouse sectorWarehouse)
        {
            try
            {
                return _dSectorWarehouse.InsertSectorWarehouse(sectorWarehouse);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SECTORWAREHOUSEFILEPATHEXCEPTION);
                return "fail";
            }
        }

        public string UpDateSectorWarehouse(ESectorWarehouse sectorWarehouse)
        {
            try
            {
                return _dSectorWarehouse.UpDateSectorWarehouse(sectorWarehouse);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SECTORWAREHOUSEFILEPATHEXCEPTION);
                return "fail";
            }

        }

        public List<EEStanteWarehouse> GetEstanteWarehouse(int idWarehouse)
        {
            try
            {
                return _dSectorWarehouse.GetEstanteWarehouse(idWarehouse);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SECTORWAREHOUSEFILEPATHEXCEPTION);
                return null;
            }
        }

        public List<EUbicacionesXEstante> GetEstanteXPasilloWarehouse(string pasillo, int idBodega)
        {
            try
            {
                 return _dSectorWarehouse.GetEstanteXPasilloWarehouse(pasillo, idBodega);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SECTORWAREHOUSEFILEPATHEXCEPTION);
                return null;
            }
        }

        public List<EUbicacionXSector> GetUbicacionXSectorWarehouse(int sector)
        {
            try
            {
                return _dSectorWarehouse.GetUbicacionXSectorWarehouse(sector);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SECTORWAREHOUSEFILEPATHEXCEPTION);
                return null;
            }
        }

        public string InsertUbicacionXSectorWarehouse(int sector, int idUbicacion)
        {
            try
            {
                return _dSectorWarehouse.InsertUbicacionXSectorWarehouse(sector,idUbicacion);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SECTORWAREHOUSEFILEPATHEXCEPTION);
                return null;
            }
        }

        public string DeleteUbicacionXSectorWarehouse(int sector, int idUbicacion)
        {
            try
            {
                return _dSectorWarehouse.DeleteUbicacionXSectorWarehouse(sector, idUbicacion);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SECTORWAREHOUSEFILEPATHEXCEPTION);
                return null;
            }
        }

    }
}

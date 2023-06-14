using Diverscan.MJP.AccesoDatos.Invoiced.ExtenciasBodegaGeneral;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Invoiced.ExistenciasBodegaGeneral
{
    public class NExistenciasBodega
    {
        private DExistenciasBodega _dexistencias;
        private IFileExceptionWriter _fileExceptionWriter;

        public NExistenciasBodega(IFileExceptionWriter fileExceptionWriter)
        {
            _fileExceptionWriter = fileExceptionWriter;
            _dexistencias = new DExistenciasBodega();
        }
        public List<EExistenciasBodega> ObtenerExistenciasBodegaGeneral(long idWarehouse)
        {
            List<EExistenciasBodega> eExistenciaslist ;
            try
            {
                eExistenciaslist = _dexistencias.ObtenerExistenciasBodegaGeneral(idWarehouse);
            }
            catch (Exception ex)
            {
                eExistenciaslist = new List<EExistenciasBodega>();
                _fileExceptionWriter.WriteException(ex, PathFileConfig.INVOICEDFILEPATHEXCEPTION);
            }
            return eExistenciaslist;
        }
    }
}

using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Invoiced
{
    public class NInvoiced
    {
        private DInvoiced _dInvoiced;
        private IFileExceptionWriter _fileExceptionWriter;

        public NInvoiced(IFileExceptionWriter fileExceptionWriter)
        {
            _fileExceptionWriter = fileExceptionWriter;
            _dInvoiced = new DInvoiced();
        }
        public List<EInvoiced> BuscarInvoiced(long idWarehouse, long idTransportista, DateTime dateInit, DateTime dateEnd, string idInternoSAP)
        {
            List<EInvoiced> invoicedList;
            try
            {
                invoicedList = _dInvoiced.BuscarInvoiced(idWarehouse, idTransportista, dateInit, dateEnd, idInternoSAP);
            }
            catch (Exception ex)
            {
                invoicedList = new List<EInvoiced>();
                _fileExceptionWriter.WriteException(ex, PathFileConfig.INVOICEDFILEPATHEXCEPTION);               
            }
            return invoicedList;
        }
    }
}

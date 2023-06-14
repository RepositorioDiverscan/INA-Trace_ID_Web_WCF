using Diverscan.MJP.AccesoDatos.Invoiced;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Emo
{
    public class NEmo
    {
        private DEmo _dEmo;
        private IFileExceptionWriter _fileExceptionWriter;

        public NEmo(IFileExceptionWriter fileExceptionWriter)
        {
            _fileExceptionWriter = fileExceptionWriter;
            _dEmo = new DEmo();
        }

        public List<EEmo> BuscarEmo(long idWarehouse, long idTransportista, DateTime dateInit, DateTime dateEnd, string idInternoSAP)
        {
            List<EEmo> emoList;
            try
            {
                emoList = _dEmo.BuscarEmo(idWarehouse, idTransportista, dateInit, dateEnd, idInternoSAP);
            }
            catch (Exception ex)
            {
                emoList = new List<EEmo>();
                _fileExceptionWriter.WriteException(ex, PathFileConfig.INVOICEDFILEPATHEXCEPTION);
            }
            return emoList;
        }

        public long CreateEmo(long idWarehouse, long idTransportista, int idUser)
        {
            long idEmo;
            try
            {
                idEmo = _dEmo.CreateEmo(idWarehouse, idTransportista, idUser);
            }
            catch (Exception ex)
            {
                idEmo = -1;
                _fileExceptionWriter.WriteException(ex, PathFileConfig.EMOFILEPATHEXCEPTION);
            }
            return idEmo;
        }

        public string InsertInvoicesByEmo(long idEmo, List<long> facturas)
        {
                            
            string response = "";
            try
            {
                DataTable inputInvoices = new DataTable();
                inputInvoices.Columns.Add("idFactura", typeof(long));
                foreach(long temp in facturas)
                {
                    inputInvoices.Rows.Add(temp);
                }
                response = _dEmo.InsertInvoicesByEmo(idEmo, inputInvoices);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                _fileExceptionWriter.WriteException(ex, PathFileConfig.EMOFILEPATHEXCEPTION);
            }
            return response;
        }

        public List<EInvoiced> GetInvoicesByEmo(long idEmo)
        {
            List<EInvoiced> invoicesList;
            try
            {
                invoicesList = _dEmo.GetInvoicesByEmo(idEmo);
            }
            catch (Exception ex)
            {
                invoicesList = new List<EInvoiced>();
                _fileExceptionWriter.WriteException(ex, PathFileConfig.INVOICEDFILEPATHEXCEPTION);
            }
            return invoicesList;
        }
    }
}

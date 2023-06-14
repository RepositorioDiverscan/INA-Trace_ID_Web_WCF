using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Transportista
{
    public class NTransportista
    {
        private DTransportista _dTransportista;
        private IFileExceptionWriter _fileExceptionWriter;

        public NTransportista(IFileExceptionWriter fileExceptionWriter)
        {
            _fileExceptionWriter = fileExceptionWriter;
            _dTransportista = new DTransportista();
        }

        public List<ETransportista> BuscarTransportistaXBodega(long idWarehouse)
        {
            List<ETransportista> transportistasList;
            try
            {
                transportistasList = _dTransportista.BuscarTransportistaXBodega(idWarehouse);
            }
            catch (Exception ex)
            {
                transportistasList = new List<ETransportista>();
                _fileExceptionWriter.WriteException(ex, PathFileConfig.INVOICEDFILEPATHEXCEPTION);
            }
            return transportistasList;
        }
    }
}

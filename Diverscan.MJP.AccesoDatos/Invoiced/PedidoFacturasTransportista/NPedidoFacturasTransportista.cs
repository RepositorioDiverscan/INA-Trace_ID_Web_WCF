using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Invoiced.PedidoFacturasTransportista
{
    public class NPedidoFacturasTransportista
    {

        private DPedidoFacturasTransportista _dPedidoFacturasTransportista;
        private IFileExceptionWriter _fileExceptionWriter;

        public NPedidoFacturasTransportista(IFileExceptionWriter fileExceptionWriter)
        {
            _fileExceptionWriter = fileExceptionWriter;
            _dPedidoFacturasTransportista = new DPedidoFacturasTransportista();
        }
        public List<EPedidoFacturasTransportista> BuscarPedidoFacturasTransportista(long idWarehouse, long idTransportista, DateTime dateInit, DateTime dateEnd, string idInternoSAP)
        {
            List<EPedidoFacturasTransportista> pedidosFacturasTransportista;
            try
            {
                pedidosFacturasTransportista = _dPedidoFacturasTransportista.BuscarPedidoFacturasTransportista(idWarehouse, idTransportista, dateInit, dateEnd, idInternoSAP);
            }
            catch (Exception ex)
            {
                pedidosFacturasTransportista = new List<EPedidoFacturasTransportista>();
                _fileExceptionWriter.WriteException(ex, PathFileConfig.INVOICEDFILEPATHEXCEPTION);
            }
            return pedidosFacturasTransportista;
        }
    }
}

using Diverscan.MJP.AccesoDatos.OrdenCompra;
using Diverscan.MJP.Entidades.OrdenCompra;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.OrdenCompra
{
    public class n_GetPurchaseOrder
    {
        GetPurchaseOrderDBA getPurchaseOrder;
        private IFileExceptionWriter _fileException;
       
        public n_GetPurchaseOrder(IFileExceptionWriter fileException)
        {
            this._fileException = fileException;
            getPurchaseOrder = new GetPurchaseOrderDBA();
        }

        public List<EGetPucharseOrder> GetPurchaseOrder(string fecha, string idBodega)
        {          
            return getPurchaseOrder.GetPurchaseOrder(fecha,idBodega);
        }

        public ResultGetDetailPurchaseOrder GetDetailPurchaseOrder(string IdMOC, string TipoIngreso)
        {
            ResultGetDetailPurchaseOrder resultDetail = new ResultGetDetailPurchaseOrder();

            try
            {
                resultDetail.DetailList = getPurchaseOrder.GetDetailPurchaseOrder(IdMOC, TipoIngreso);
                resultDetail.state = true;
                resultDetail.Description = "Successfull";
                
            }
            catch (Exception ex){
                resultDetail.state = true;
                resultDetail.Description = ex.Message;
                _fileException.WriteException(ex, PathFileConfig.DETAILPURCHASEFILEPATHEXCEPTION);               
            }
            return resultDetail;
        }

        //public string InsertReceptionDetailOrder(EDetailPurchaseOrder detailPurchaseOrder)
        //{
        //    try
        //    {
        //        return getPurchaseOrder.InsertReceptionDetailOrder(detailPurchaseOrder);
        //    }
        //    catch (Exception ex)
        //    {
        //        _fileException.WriteException(ex, PathFileConfig.DETAILPURCHASEFILEPATHEXCEPTION);
        //        return ex.Message;
        //    }
           
        //}

        public string UpdateCertificateOC(int idMOC, string certificate)
        {
            String result = "";
            try
            {
                result = getPurchaseOrder.UpdateCertificateOC(idMOC,certificate);
               

            }
            catch (Exception ex)
            {
                result = "Fail "+ ex.Message;
                _fileException.WriteException(ex, PathFileConfig.DETAILPURCHASEFILEPATHEXCEPTION);
            }
            return result;
        }


        public List<EGetPucharseOrder> GetOnePurchaseOrder(string IdInterno, string idBodega)
        {
            return getPurchaseOrder.GetOnePurchaseOrder(IdInterno, idBodega);
        }

        public string UpdateBillOC(int idMOC, string numberBill)
        {
            String result = "";
            try
            {
                result = getPurchaseOrder.UpdateBillOC(idMOC, numberBill);


            }
            catch (Exception ex)
            {
                result = "Fail " + ex.Message;
                _fileException.WriteException(ex, PathFileConfig.PRODUCTSTORAGEFILEPATHEXCEPTION);
            }
            return result;
        }
    }
}

using Diverscan.MJP.AccesoDatos.Devolutions;
using Diverscan.MJP.Entidades.Devolutions.DevolucionHeader;
using Diverscan.MJP.Entidades.Devolutions.DevolutionsDetail;
using Diverscan.MJP.Entidades.Devolutions.DevolutionProductLocation;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace Diverscan.MJP.Negocio.Devolutions
{
    public class NDevolutions
    {

        private IFileExceptionWriter _fileException;

        public NDevolutions(IFileExceptionWriter fileException)
        {
            this._fileException = fileException;
        }

        public ResultGetDevolutionHeader GetDevolutionsOrders( string userEmail)
        {
            ResultGetDevolutionHeader resultGetDevolutionHeader = new ResultGetDevolutionHeader();
            try
            {
                DDevolutionsHeader dDevolutionsHeader = new DDevolutionsHeader();
                resultGetDevolutionHeader.EListHeader = dDevolutionsHeader.GetDevolutionHeader(userEmail, "Pendiente");
                if (resultGetDevolutionHeader.EListHeader.Count>0) {
                    resultGetDevolutionHeader.state = true;
                    resultGetDevolutionHeader.Description = "Successful";
                } else {                   
                    resultGetDevolutionHeader.state = false;
                    resultGetDevolutionHeader.Description = "There are no records with the email adrres";
                }
                return resultGetDevolutionHeader;
            }
            catch (Exception ex)
            {
                resultGetDevolutionHeader.state = false;
                resultGetDevolutionHeader.Description = ex.Message;
                resultGetDevolutionHeader.EListHeader = null;
                _fileException.WriteException(ex, PathFileConfig.DEVOLUTIONSFILEPATHEXCEPTION);
                return resultGetDevolutionHeader;
            }           
        }

        public ResultGetDevolutionDetail GetDevolutionDetailOrder(string idDevolutionHeader)
        {           
            ResultGetDevolutionDetail resultGetDevolutionDetail = new ResultGetDevolutionDetail();          
            try
            {
                DDevolutionsDetail dDevolutionsDetail = new DDevolutionsDetail();
                resultGetDevolutionDetail.EListDetails = dDevolutionsDetail.GetDevolutionDetail(idDevolutionHeader);
                resultGetDevolutionDetail.state = true;
                resultGetDevolutionDetail.Description = "Successful";            
                return resultGetDevolutionDetail;
            }
            catch (Exception ex)
            {
                resultGetDevolutionDetail.state = false;
                resultGetDevolutionDetail.Description = ex.Message;
                resultGetDevolutionDetail.EListDetails = null;
                _fileException.WriteException(ex, PathFileConfig.DEVOLUTIONSFILEPATHEXCEPTION);
                return resultGetDevolutionDetail;
            }            
        }

        public string InsertProductLocation( EDevolutionProduct productLocation)
        {

            string result = "";
            try
            {
                DDevolutionsProductLocation dDevolutionsProductLocation = new DDevolutionsProductLocation();
                dDevolutionsProductLocation.insertProductLocation(productLocation);
                result = "Successful";
                return result;
            }
            catch (Exception ex)
            {
               
                _fileException.WriteException(ex, PathFileConfig.DEVOLUTIONSFILEPATHEXCEPTION);
                result = "Fail";
                return result;
            }
        }
    }
}
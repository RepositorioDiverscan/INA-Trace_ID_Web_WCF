using Diverscan.MJP.AccesoDatos.GTIN14VariableLogistic;
using Diverscan.MJP.Entidades.GTIN14VariableLogistic;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.GTIN14VariableLogistic
{
    public class NGTIN14VariableLogistic
    {
        private IFileExceptionWriter _fileExceptionWriter;
        private DGTIN14VariableLogistic dGTIN14Variable;

        public NGTIN14VariableLogistic(IFileExceptionWriter fileExceptionWriter)
        {
            _fileExceptionWriter = fileExceptionWriter;
            this.dGTIN14Variable =  new DGTIN14VariableLogistic();
        }

        public ResultGetGTIN14 GetProductDetailGTIN14(string gtin14)
        {
            ResultGetGTIN14 resultGetGTIN14 = new ResultGetGTIN14();
            try
            {
                resultGetGTIN14.Gtin14Variable = dGTIN14Variable.GetProductDetailGTIN14(gtin14);
                resultGetGTIN14.state = true;
                resultGetGTIN14.Description = "Successful";
                return resultGetGTIN14;
            }
            catch (Exception ex)
            {
                resultGetGTIN14.Description = ex.ToString();
                resultGetGTIN14.state = false;
                resultGetGTIN14.Gtin14Variable = null;

                _fileExceptionWriter.WriteException(ex, PathFileConfig.GTIN14LOGISTICFILEPATHEXCEPTION);
                return resultGetGTIN14;
            }
        }

        public string GetProductBaseByGTIN14(string gtin14)
        {
            return dGTIN14Variable.GetProductBaseByGTIN14(gtin14);
        }
    }
}

using Diverscan.MJP.AccesoDatos.Alistos;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SSCC
{
    public class NSSCC
    {
        private ConsultarSSCC _consultarSSCC;
        private IFileExceptionWriter _fileExceptionWriter;

        public NSSCC(IFileExceptionWriter fileExceptionWriter)
        {
            _fileExceptionWriter = fileExceptionWriter;
            _consultarSSCC = new ConsultarSSCC();
        }

        public string UbicarSSCC(int idUbicacion, string consecutivoSSCC)
        {
            try
            {
                return _consultarSSCC.UbicarSSCC(idUbicacion, consecutivoSSCC);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SSCCFILEPATHEXCEPTION);
                return null;
            }
        }
        public ResultGetSSCCProducts GetSSCCProducts(string consecutivoSSCC)
        {
            ResultGetSSCCProducts resultGetSSCCProducts = new ResultGetSSCCProducts();
            try
            {
                resultGetSSCCProducts.OlaSSCC = _consultarSSCC.GetSSCCProducts(consecutivoSSCC);
                resultGetSSCCProducts.State = true;
                resultGetSSCCProducts.Message = "Succesfull";
                return resultGetSSCCProducts;
            }
            catch (Exception ex)
            {
                resultGetSSCCProducts.OlaSSCC = null;
                resultGetSSCCProducts.State = false;
                resultGetSSCCProducts.Message = ex.Message;
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SSCCFILEPATHEXCEPTION);
                return resultGetSSCCProducts;
            }
        }

        public List<ESSCC> ObtenerSSCCXIdSolicitud(long idMaestroSolicitud)
        {
            try
            {
                return _consultarSSCC.ObtenerSSCCXIdSolicitud(idMaestroSolicitud);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SSCCFILEPATHEXCEPTION);
                return null;
            }
        }

        public List<EDetalleSSCCCertificado> GetSSCCProducts(int idSSCC)
        {
            try
            {
                return _consultarSSCC.GetSSCCProducts(idSSCC);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SSCCFILEPATHEXCEPTION);
                return null;
            }
        }

        public ResultGetSSCC ObtenerSSCC(string ssccGenerado)
        {
            ResultGetSSCC result = new ResultGetSSCC();
            try
            {
                ESSCC eSSCC = _consultarSSCC.ObtenerSSCC(ssccGenerado);
                if (eSSCC.DescripcionSSCC.Contains("SSCC no existe"))
                {
                    result.SSCC = null;
                    result.Description = eSSCC.DescripcionSSCC;
                }
                else {
                    result.SSCC = eSSCC;
                    result.Description = "Successful";
                }
                result.state = true;
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SSCCFILEPATHEXCEPTION);
                result.state = false;
                result.Description = "Fail";
            }
            return result;
        }

        public ResultGetSSCCDespatch ObtenerSSCCDespacho(string ssccGenerado)
        {
            ResultGetSSCCDespatch result = new ResultGetSSCCDespatch();
            try
            {
                ESSCC eSSCC = _consultarSSCC.ObtenerSSCC(ssccGenerado);
                if (eSSCC.DescripcionSSCC.Contains("SSCC no existe"))
                {
                    result.SSCC = null;
                    result.Description = eSSCC.DescripcionSSCC;
                }
                else
                {
                    result.SSCC = eSSCC;
                    result.SSCCOLA = _consultarSSCC.ObtenerSSCCXDespacho(eSSCC.IdMaestroSolicitud, eSSCC.IdSSCC);
                    result.Description = "Successful";
                }
                result.state = true;
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SSCCFILEPATHEXCEPTION);
                result.state = false;
                result.Description = "Fail";
            }
            return result;
        }

        public string RevertirArticuloSSCCCertificado(ERevertirArticuloSSCC articuloSSCC)
        {
            try
            {
                return _consultarSSCC.RevertirArticuloSSCCCertificado(articuloSSCC);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SSCCFILEPATHEXCEPTION);
                return ex.Message;
            }
        }

        public ResultWS TransferSSCC(int idUbicacion, int idSSCC)
        {
            ResultWS result = new ResultWS();
            try
            {              
                result.Description =_consultarSSCC.TransferSSCC(idUbicacion, idSSCC);
                result.state = true;
                return result;
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SSCCFILEPATHEXCEPTION);
                result.Description = ex.Message;
                result.state = false;
                return result;
            }
        }

        public ResultGetSSCCS CantidadSSCCActivosUsuario(long idUsuario)
        {
            ResultGetSSCCS result = new ResultGetSSCCS();
            try
            {
                result.SSCCS = _consultarSSCC.CantidadSSCCActivosUsuario(idUsuario);
                result.Description = "Succesful";
                result.state = true;
                return result;
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SSCCFILEPATHEXCEPTION);
                result.Description = ex.Message;
                result.state = false;
                return result;
            }
        }

        public List<ESSCC> GenerarSSCC(int cantidad)
        {
            List<ESSCC> listaSSCC = new List<ESSCC>();
            try
            {
                listaSSCC = _consultarSSCC.GenerarSSCC(cantidad);
                return listaSSCC;
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.SSCCFILEPATHEXCEPTION);
                return listaSSCC;
            }
        }
    }
}

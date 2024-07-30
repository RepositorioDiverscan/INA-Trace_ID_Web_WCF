using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;

namespace Diverscan.MJP.AccesoDatos.Encargado
{
    public class NEncargado
    {
        private DEncargado _dEncargado;
        private IFileExceptionWriter _fileExceptionWriter;
        EEncargado encargado = new EEncargado();

        public NEncargado(IFileExceptionWriter fileExceptionWriter)
        {
            _fileExceptionWriter = fileExceptionWriter;
            _dEncargado = new DEncargado();
        }

        public EEncargado ObtenerEncargadoXBodega(int idBodega, string buscar)
        {
            try
            {
                encargado = _dEncargado.ObtenerEncargados(idBodega, buscar);
            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.INVOICEDFILEPATHEXCEPTION);
            }
            return encargado;
        }
    }
}

using CodeUtilities;
using Diverscan.MJP.GestorImpresiones.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Diverscan.MJP.AccesoDatos.Articulos;
using Diverscan.MJP.Utilidades;

namespace Diverscan.MJP.AccesoDatos.Articulos
{
    public class NArticulos
    {
        public EArticuloId ObtenerIdArticulos(string idArticulo)
        {
            try
            {
                DAArticulos dAArticuloss = new DAArticulos();
                return dAArticuloss.ObtenerIdInterno_Articulo(idArticulo);
            }
            catch (Exception ex)
            {
                var cl = new GestorImpresiones.Utilidades.clErrores();
                FileExceptionWriter _fileExceptionWriter = new FileExceptionWriter();
                _fileExceptionWriter.WriteException(ex, PathFileConfig.PRODUCTSTORAGEFILEPATHEXCEPTION);

                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();

                return null;
            }
        }

        public EArticuloId ObtenerNombreArticulos(string idArticulo)
        {
            try
            {
                DAArticulos dAArticuloss = new DAArticulos();
                return dAArticuloss.ObtenerNombreArticulo(idArticulo);
            }
            catch (Exception ex)
            {
                var cl = new GestorImpresiones.Utilidades.clErrores();
                FileExceptionWriter _fileExceptionWriter = new FileExceptionWriter();
                _fileExceptionWriter.WriteException(ex, PathFileConfig.PRODUCTSTORAGEFILEPATHEXCEPTION);

                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();

                return null;
            }
        }
    }
}

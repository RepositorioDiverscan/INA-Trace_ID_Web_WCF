using CodeUtilities;
using Diverscan.MJP.GestorImpresiones.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Diverscan.MJP.AccesoDatos.Bodega;
using Diverscan.MJP.AccesoDatos.Articulos;
using Diverscan.MJP.Utilidades;

namespace Diverscan.MJP.AccesoDatos.ModuloConsultas
{
   public  class NConsultas
    {
        private DAConsultas _dAConsultas;

        public NConsultas()
        {
            _dAConsultas = new DAConsultas();
        }

        public List<EProveedor> ObtenerProveedores()
        {
           
            return _dAConsultas.ObtenerProveedores();
        }

        public List<EArticulo> ObtenerArticulos(int idProvedor)
        {
          

            return _dAConsultas.ObtenerArticulos(idProvedor);
        }

        public List<EZona> ObtenerZonas(int idArticulo)
        {
          

            return _dAConsultas.ObtenerZonas(idArticulo);
        }

        public List<EArticulo> ObtenerArticulosDisponiblesXzonas(int idArticulo, List<EZona> zonas,int idBodega)
        {
            try
            {
                DataTable inputZonas = zonas.ToDataTable();
                inputZonas.Columns.Remove("Nombre");
                                       
                return _dAConsultas.ObtenerArticulosXZonas(idArticulo, inputZonas, idBodega);

            }catch(Exception ex){
                var cl = new GestorImpresiones.Utilidades.clErrores();
                FileExceptionWriter _fileExceptionWriter = new FileExceptionWriter();
                _fileExceptionWriter.WriteException(ex, PathFileConfig.PRODUCTSTORAGEFILEPATHEXCEPTION);

                cl.escribirError(ex.Message, ex.StackTrace);
                ex.ToString();
               
                return null;
            }
        }

        public List<EArticulo> ObtenerArticulosDisponiblesXzonasAndroid(int idArticulo,
            List<EZonaAndroid> zonas, int idBodega)
        {
            try
            {
                DataTable inputZonas = zonas.ToDataTable();
                inputZonas.Columns.Remove("Nombre");

                return _dAConsultas.ObtenerArticulosXZonas(idArticulo, inputZonas, idBodega);
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

        //Método para obtener todas las Bodegas del Sistema
        public List<EBodega> GETBODEGAS()
        {
            return _dAConsultas.GETBODEGAS();
        }

        public string ObtenerInventarioBodegaTradeBook(string idInternoProducto, string idInternoBodega)
        {
            return _dAConsultas.ObtenerInventarioBodegaTradeBook(idInternoProducto, idInternoBodega);
        }
    }
}

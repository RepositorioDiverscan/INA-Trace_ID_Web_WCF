using Diverscan.MJP.AccesoDatos.MaestroArticulo;
using Diverscan.MJP.Entidades.MaestroArticulo;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.MaestroArticulo
{
    public class n_MaestroArticulo
    {
        private FileExceptionWriter _fileExceptionWriter;
        public static List<e_MaestroArticulo> GetListMaestroArticulo(string prefix, string IdCompania)
        {
            da_MaestroArticulo da_maestroArticulo = new da_MaestroArticulo();
            return da_maestroArticulo.GetListMaestroArticulo(prefix, IdCompania);
        }

        public static List<e_MaestroArticulo> GetMaestroArticulo(string IdCompania)
        {
            da_MaestroArticulo da_maestroArticulo = new da_MaestroArticulo();
            return da_maestroArticulo.GetMaestroArticulo(IdCompania);
        }
        public List<e_ArticuloSinUnidadAlistoDefecto> GetArticuloSinUnidadAlistoDefecto()
        {
            da_MaestroArticulo da_maestroArticulo = new da_MaestroArticulo();
            return da_maestroArticulo.GetArticuloSinUnidadAlistoDefecto();
        }


        #region Notificación
        public string GetNotificacionArticulosUnidadAlisto()
        {
            da_MaestroArticulo da_maestroArticulo = new da_MaestroArticulo();
            List<e_ArticuloSinUnidadAlistoDefecto> lista = da_maestroArticulo.GetArticuloSinUnidadAlistoDefecto();
            int cantidadArticulosSinUnidadAlisto = lista.Count;
            if (cantidadArticulosSinUnidadAlisto > 0)
            {
                if (cantidadArticulosSinUnidadAlisto == 1)
                {
                    return "Existe" + cantidadArticulosSinUnidadAlisto + " artículo sin unidad alisto por defecto";
                }
                else
                {
                    return "Existen " + lista.Count + " artículos sin unidad alisto por defecto";
                }
            }
            else
            {
                return "false";//Indica que no hay artículos sin unidad alisto
            }
        }

        public string GeneraGTIN()
        {
            da_MaestroArticulo da_maestroArticulo = new da_MaestroArticulo();
            return da_maestroArticulo.GeneraGTIN();
        }

        public string GeneraGTIN14(string GTIN13)
        {
            da_MaestroArticulo da_maestroArticulo = new da_MaestroArticulo();
            return da_maestroArticulo.GeneraGTIN14(GTIN13);
        }
        #endregion        

        public List<EMinPicking> GetMinPicking(int IdBodega)
        {
            da_MaestroArticulo da_maestroArticulo = new da_MaestroArticulo();
            return da_maestroArticulo.GetMinPicking(IdBodega);
        }

        //public List<EProductStorage> GetSuggestionStorage(int idProduct, int idBodega) {
        //    da_MaestroArticulo da_maestroArticulo = new da_MaestroArticulo();

        //    List<EProductStorage> productStorages = new List<EProductStorage>();
        //    try {
        //        productStorages = da_maestroArticulo.GetSuggestionStorage(idProduct,idBodega);
        //    }
        //    catch (Exception ex)
        //    {
        //        _fileExceptionWriter = new FileExceptionWriter();
        //        _fileExceptionWriter.WriteException(ex, PathFileConfig.PRODUCTSTORAGEFILEPATHEXCEPTION);

        //    }
        //    return productStorages;
        //}
    }
}


using Diverscan.MJP.AccesoDatos.AjusteInventario;
using Diverscan.MJP.Entidades.Common;
using Diverscan.MJP.Entidades.CustomEvent;
using Diverscan.MJP.Entidades.CustomException;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.AjusteInventario
{
    public class UbicacionEtiquetaLoader
    {
        readonly IUbicacionEtiquetaViewer _ubicacionEtiquetaViewer;
        readonly IUbicacionEtiquetaDataAcces _ubicacionEtiquetaDataAcces;
        public UbicacionEtiquetaLoader(IUbicacionEtiquetaViewer ubicacionEtiquetaViewer, IUbicacionEtiquetaDataAcces ubicacionEtiquetaDataAcces)
        {
            if (ubicacionEtiquetaViewer == null)
                throw new ArgumentNullException("ubicacionEtiquetaViewer","ubicacionEtiquetaViewer is null");
            _ubicacionEtiquetaViewer = ubicacionEtiquetaViewer;
            _ubicacionEtiquetaDataAcces = ubicacionEtiquetaDataAcces;
            _ubicacionEtiquetaViewer.UbicacionEtiqueta += getUbicacionEtiqueta;
        }

        private void getUbicacionEtiqueta(object sender, EventArgs eventArgs)
        {
            var data = eventArgs as UbicacionEtiquetaEvent;
            if (data != null)
            {
                var etiqueta = data.Etiqueta;
                ubicarArticulo(etiqueta);
            }
        }

        private long ubicarArticulo(string etiqueta)
        {           
            long idUbicacion = 0;
            try
            {
                idUbicacion = _ubicacionEtiquetaDataAcces.GetIdUbicacion(etiqueta);
                _ubicacionEtiquetaViewer.SetIdUbicacion(idUbicacion);
            }
            catch (MoreThanOneRecordException ex)
            {
                _ubicacionEtiquetaViewer.ShowMessage(ex.Message);
            }
            catch (RecordNotFoundException ex)
            {
                _ubicacionEtiquetaViewer.ShowMessage(ex.Message);
            }
            return idUbicacion;
        }

        public static long OtenerIdUbicacion(string etiqueta)
        {
            var ubicacionEtiquetaDataAcces = new UbicacionEtiquetaDataAcces();
            return ubicacionEtiquetaDataAcces.GetIdUbicacion(etiqueta);
        }
    }
}

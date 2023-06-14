using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.MotivoAjusteInventario
{
    [Serializable]
    public class ArticuloXSolicitudAjusteDetalle : ArticuloXSolicitudAjusteRecord
    {
        string _etiquetaActual;
        decimal _precioUnidad;

        public ArticuloXSolicitudAjusteDetalle()
        {
        }
        public ArticuloXSolicitudAjusteDetalle(long idSolicitudAjusteInventario, long idArticulo, string codigoInterno, string nombreArticulo, string unidadMedida, bool esGranel,
            string lote, DateTime fechaVencimiento, long idUbicacionActual, long idUbicacionMover, int cantidad, decimal cantidadUI, string etiquetaActual, decimal precioUnidad)
            : base(idArticulo, codigoInterno, nombreArticulo, unidadMedida, esGranel, lote, fechaVencimiento, idUbicacionActual, idUbicacionMover, cantidad, cantidadUI)
        {
            _idSolicitudAjusteInventario = idSolicitudAjusteInventario;
            _etiquetaActual = etiquetaActual;
            _precioUnidad = precioUnidad;
        }

        public string EtiquetaActual
        {
            get { return _etiquetaActual; }
            set { _etiquetaActual = value; }
        }

        public decimal PrecioUnidad
        {
            get { return _precioUnidad; }
            set { _precioUnidad = value; }
        }

        public decimal PrecioXCantidad
        {
            get { return _precioUnidad * CantidadToShow; }            
        }
    }
}

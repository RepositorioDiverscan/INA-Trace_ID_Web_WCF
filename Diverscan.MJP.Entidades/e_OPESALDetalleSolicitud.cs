using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    public class e_OPESALDetalleSolicitud
    {
        private readonly long _idLineaDetalleSolicitud;
        private readonly Single _cantidad;

        public long IdLineaDetalleSolicitud
        {
            get { return _idLineaDetalleSolicitud; }
        }

        public Single Cantidad
        {
            get { return _cantidad; }
        }

        public e_OPESALDetalleSolicitud(long idLineaDetalleSolicitud, Single cantidad) 
        {
            _idLineaDetalleSolicitud = idLineaDetalleSolicitud;
            _cantidad = cantidad;
        }
    }
}

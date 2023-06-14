using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    public class e_PreDetalleSolicitud
    {
        private readonly string _idInternoArticulo;
        private readonly decimal _Cantidad;
        private readonly long _MaestroSolicitudId;

        public e_PreDetalleSolicitud(string idInternoArticulo, decimal Cantidad, long MaestroSolicitudId) 
        {
            _idInternoArticulo = idInternoArticulo;
            _Cantidad = Cantidad;
            _MaestroSolicitudId = MaestroSolicitudId;
        }

        public string IdInternoArticulo
        {
            get { return _idInternoArticulo; }
        }

        public decimal Cantidad1
        {
            get { return _Cantidad; }
        }

        public long MaestroSolicitudId
        {
            get { return _MaestroSolicitudId; }
        } 
    }
}

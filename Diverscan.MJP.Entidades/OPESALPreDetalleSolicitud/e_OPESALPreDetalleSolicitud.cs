using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.OPESALPreDetalleSolicitud
{
    [Serializable]
    public class e_OPESALPreDetalleSolicitud
    {
        long _idPreLineaDetalleSolicitud;
        string _Nombre;
        string _MaestroSolicitudNombre;
        string _InternoArticuloNombre;
        decimal _Cantidad;
        string _Descripcion;
        string _IdCompania;
        long _IdMaestroSolicitud;

        public e_OPESALPreDetalleSolicitud(long idPreLineaDetalleSolicitud, string Nombre, string MaestroSolicitudNombre, string InternoArticuloNombre, decimal Cantidad, string Descripcion, string IdCompania, long IdMaestroSolicitud) 
        {
            _idPreLineaDetalleSolicitud = idPreLineaDetalleSolicitud;
            _Nombre = Nombre;
            _MaestroSolicitudNombre = MaestroSolicitudNombre;
            _InternoArticuloNombre = InternoArticuloNombre;
            _Cantidad = Cantidad;
            _Descripcion = Descripcion;
            _IdCompania = IdCompania;
            _IdMaestroSolicitud = IdMaestroSolicitud;
        }

        public long IdPreLineaDetalleSolicitud
        {
            get { return _idPreLineaDetalleSolicitud; }
            set { _idPreLineaDetalleSolicitud = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string MaestroSolicitudNombre
        {
            get { return _MaestroSolicitudNombre; }
            set { _MaestroSolicitudNombre = value; }
        }

        public string InternoArticuloNombre
        {
            get { return _InternoArticuloNombre; }
            set { _InternoArticuloNombre = value; }
        }

        public decimal Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public string IdCompania
        {
            get { return _IdCompania; }
            set { _IdCompania = value; }
        }

        public long IdMaestroSolicitud
        {
            get { return _IdMaestroSolicitud; }
            set { _IdMaestroSolicitud = value; }
        }
    }
}

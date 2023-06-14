using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    public class e_ProcesarSolicitud
    {
        Int64 _idMaestroSolicitud;
        Int64 _idarticulo;
        Int64 _cantidad;
        int  _idUsuario;
        string _idCompania;
        Int64 _idDestino;
        string _zonaAlmacen;
        string _zonaPicking;
        Int64 _idMetodoAccion;
        Int64 _idlineadetallesolicitud;

        public Int64 IdMaestroSolicitud
        {
            get { return _idMaestroSolicitud; }
            set { _idMaestroSolicitud = value; }
        }

        public Int64 Idarticulo
        {
            get { return _idarticulo; }
            set { _idarticulo = value; }
        }

        public Int64 Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public  int  IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }

        public string IdCompania
        {
            get { return _idCompania; }
            set { _idCompania = value; }
        }

        public Int64 IdDestino
        {
            get {  return _idDestino; }
            set { _idDestino = value; }
        }

        public string ZonaAlmacen
        {
            get { return _zonaAlmacen; }
            set { _zonaAlmacen = value;}
        }

        public string ZonaPicking
        {
           get { return _zonaPicking; }
           set { _zonaPicking = value;}
        }

        public  Int64 IdMetodoAccion
        {
           get { return _idMetodoAccion; }
           set { _idMetodoAccion = value;}
        }

        public Int64 idLineaDetalleSolicitud
        {
           get { return _idlineadetallesolicitud; }
           set { _idlineadetallesolicitud = value;}
        }

        public e_ProcesarSolicitud(Int64 caNtidad, int idUsuario, string idCompania, string zonaAlmacen, string zonaPicking, Int64 idMetodoAccion, Int64 idLineaDetalleSolicitud) 
        {
            _cantidad = caNtidad;
            _idUsuario = idUsuario;
            _idCompania = idCompania;
            _zonaAlmacen = zonaAlmacen;
            _zonaPicking = zonaPicking;
            _idMetodoAccion = idMetodoAccion;
            _idlineadetallesolicitud = idLineaDetalleSolicitud;
        }
    }
}

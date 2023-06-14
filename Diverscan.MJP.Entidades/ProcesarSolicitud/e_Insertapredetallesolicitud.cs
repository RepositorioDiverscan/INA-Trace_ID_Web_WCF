using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    public class e_Insertapredetallesolicitud
    {
        Int64 _idMaestroSolicitud;
        Int64 _idDestino;
        Int64 _idarticulo;
        Int64 _cantidad;
        int  _idUsuario;
        string _idCompania;

        public Int64 IdMaestroSolicitud
        {
            get { return _idMaestroSolicitud; }
            set { _idMaestroSolicitud = value; }
        }

        public Int64 IdDestino
        {
            get {  return _idDestino; }
            set { _idDestino = value; }
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

        public e_Insertapredetallesolicitud(Int64 idMaestroSolicitud, Int64 idArticulo, Int64 caNtidad, int idUsuario, string idCompania)
        {
            _idMaestroSolicitud = idMaestroSolicitud;
            //_idDestino = idDestino;
            _idarticulo = idArticulo;
            _cantidad = caNtidad;
            _idUsuario = idUsuario;
            _idCompania = idCompania;
        }
    }
}

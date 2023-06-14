using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    public class e_ProcesarOrdenCompra
    {
        long _IdMaestroOrdenCompra;
        long _IdArticulo;
        long _CantidadxRecibir;
        int _IdUsuario;
        string _IdCompania;

        public e_ProcesarOrdenCompra(long idMaestroOrdenCompra, long idArticulo, long cantidadxRecibir, int idUsuario, string IdCompania)
        {
            _IdMaestroOrdenCompra = idMaestroOrdenCompra;
            _IdArticulo = idArticulo;
            _CantidadxRecibir = cantidadxRecibir;
            _IdUsuario = idUsuario;
            _IdCompania = IdCompania;
        }


        public long IdMaestroOrdenCompra
        {
            get { return _IdMaestroOrdenCompra; }
            set { _IdMaestroOrdenCompra = value; }
        }

        public long IdArticulo
        {
            get { return _IdArticulo; }
            set { _IdArticulo = value; }
        }

        public long CantidadxRecibir
        {
            get { return _CantidadxRecibir; }
            set { _CantidadxRecibir = value; }
        }


        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public string IdCompania
        {
            get { return _IdCompania; }
            set { _IdCompania = value; }
        }
    }
}

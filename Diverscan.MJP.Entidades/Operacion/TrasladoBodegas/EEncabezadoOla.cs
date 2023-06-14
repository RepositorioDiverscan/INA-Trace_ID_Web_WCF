using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Operacion.TrasladoBodegas
{
    public class EEncabezadoOla
    {
        //Atributos de la clase
        private int _idSolicitudTrasladoBodega;
        private int _idUsuarioProcesador;
        private int _idBodegaProcesador;
        List<EListaArticulos> _listaArticulos = new List<EListaArticulos>();

        public EEncabezadoOla(int idSolicitudTrasladoBodega, int idUsuarioProcesador, int idBodegaProcesador, List<EListaArticulos> listaArticulos)
        {
            _idSolicitudTrasladoBodega = idSolicitudTrasladoBodega;
            _idUsuarioProcesador = idUsuarioProcesador;
            _idBodegaProcesador = idBodegaProcesador;
            _listaArticulos = listaArticulos;
        }


        //Métodos Getters and Setters
        public int IdSolicitudTrasladoBodega { get => _idSolicitudTrasladoBodega; set => _idSolicitudTrasladoBodega = value; }
        public int IdUsuarioProcesador { get => _idUsuarioProcesador; set => _idUsuarioProcesador = value; }
        public int IdBodegaProcesador { get => _idBodegaProcesador; set => _idBodegaProcesador = value; }
        public List<EListaArticulos> ListaArticulos { get => _listaArticulos; set => _listaArticulos = value; }
    }
}

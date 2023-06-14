using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Invertario
{
    [Serializable]
    public class BodegaFisica_SistemaRecord
    {
        long _idUbicacion;
        string _etiqueta;
        int _cantidadBodega;
        int _cantidadSistema;
        string _unidadMedida;
        bool _esGranel;
        string _nombreArticulo;
        string _lote;
        string _fVToShow;
        long _idArticulo;
        string _idInterno;

        public double UnidadInventario { get; set; }

        public BodegaFisica_SistemaRecord(long idUbicacion, string etiqueta, int cantidadBodega, int cantidadSistema, string unidadMedida,
            bool esGranel, string nombreArticulo, double unidadInventario)
        {
            _idUbicacion = idUbicacion;
            _etiqueta = etiqueta;
            _cantidadBodega = cantidadBodega;
            _cantidadSistema = cantidadSistema;
            _unidadMedida = unidadMedida;
            _esGranel = esGranel;
            _nombreArticulo = nombreArticulo;
            UnidadInventario = unidadInventario;
        }

        public BodegaFisica_SistemaRecord(long idUbicacion, string etiqueta, int cantidadBodega, int cantidadSistema, string unidadMedida,
           bool esGranel, string nombreArticulo, double unidadInventario, string lote, DateTime fv, long idArticulo)
        {
            _idUbicacion = idUbicacion;
            _etiqueta = etiqueta;
            _cantidadBodega = cantidadBodega;
            _cantidadSistema = cantidadSistema;
            _unidadMedida = unidadMedida;
            _esGranel = esGranel;
            _nombreArticulo = nombreArticulo;
            UnidadInventario = unidadInventario;
            _lote = lote;
            _fVToShow = fv.ToShortDateString().ToString();
            _idArticulo = idArticulo;
        }

     //   public BodegaFisica_SistemaRecord(long idUbicacion, string etiqueta, int cantidadBodega, int cantidadSistema, string unidadMedida,
     //bool esGranel, string nombreArticulo, double unidadInventario, string lote, DateTime fv, long idArticulo,string idInterno)
     //   {
     //       _idUbicacion = idUbicacion;
     //       _etiqueta = etiqueta;
     //       _cantidadBodega = cantidadBodega;
     //       _cantidadSistema = cantidadSistema;
     //       _unidadMedida = unidadMedida;
     //       _esGranel = esGranel;
     //       _nombreArticulo = nombreArticulo;
     //       UnidadInventario = unidadInventario;
     //       _lote = lote;
     //       _fVToShow = fv.ToShortDateString().ToString();
     //       _idArticulo = idArticulo;
     //       _idInterno = idInterno;
     //   }

        public long IdUbicacion
        {
            get { return _idUbicacion; }
            set { _idUbicacion = value; ;}
        }
        public string Etiqueta
        {
            get { return _etiqueta; }
            set { _etiqueta = value; }
        }
        public int CantidadBodega
        {
            get { return _cantidadBodega; }
            set { _cantidadBodega = value; }
        }

        public decimal CantidadBodegaParaMostrar
        {
            get 
            {
                if (_esGranel)
                {
                    return (decimal)(_cantidadBodega / 1000d);
                }
                return _cantidadBodega; 
            }
        }

        public int CantidadSistema
        {
            get { return _cantidadSistema; }
            set { _cantidadSistema = value; }
        }

        public decimal CantidadSistemaParaMostrar
        {
            get
            {
                if (_esGranel)
                {
                    return (decimal)(_cantidadSistema / 1000d);
                }
                return _cantidadSistema;
            }
        }

        //public decimal DifenrenciaCantidad
        //{
        //    get 
        //    {
        //        var diff = _cantidadSistema - _cantidadBodega;
        //        if (_esGranel)
        //        {
        //            return (decimal)(diff / 1000d);
        //        }
        //        return diff; 
        //    }
        //}

        public decimal DifenrenciaCantidad
        {
            get
            {
                var diff = _cantidadBodega -_cantidadSistema;
                if (_esGranel)
                {
                    return (decimal)(diff / 1000d);
                }
                return diff;
            }
        }

        public string UnidadMedida
        {
            get { return _unidadMedida; }
            set { _unidadMedida = value; }
        }

        public bool EsGranel
        {
            get { return _esGranel; }
            set { _esGranel = value; }
        }

        public string NombreArticulo
        {
            get { return _nombreArticulo; }
            set { _nombreArticulo = value; }
        }

        public double UICantidadBodegaParaMostrar
        {
            get { return (double)CantidadBodegaParaMostrar * UnidadInventario; }
           
        }

        public double UICantidadSistemaParaMostrar
        {
            get { return (double)CantidadSistemaParaMostrar * UnidadInventario; }

        }

        public double UIDifenrenciaCantidad
        {
            get { return (double)DifenrenciaCantidad * UnidadInventario; }

        }

        public string Lote
        {
            get { return _lote; }
            set { _lote = value; }
        }

        public string FVToShow
        {
            get { return _fVToShow; }
            set { _fVToShow = value; }
        }

        public long IdArticulo
        {
            get { return _idArticulo; }
            set { _idArticulo = value; }
        }

        public string IdInterno
        {
            get { return _idInterno; }
            set { _idInterno = value; }
        }
    }
}

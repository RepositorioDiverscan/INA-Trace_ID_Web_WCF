using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Invertario
{
    [Serializable]
    public class ArticuloInventario : IArticuloInventario
    {
         long _idArticulo;        
        DateTime _fechaVencimiento;
        string _lote;
        long _idUbicacion; 
        string _etiqueta;
        int _cantidad;
        string _unidadMedida;
        bool _esGranel;
        string _nombreArticulo;
        string _idInterno;

        public double UnidadInventario { get; set; }

        public ArticuloInventario()
        { 
        
        }

        public ArticuloInventario(long idUbicacion, string etiqueta, long idArticulo, int cantidad,DateTime fechaVencimiento,
            string lote, string unidadMedida, bool esGranel, string nombreArticulo, double unidadInventario)
        {
            _idUbicacion = idUbicacion;
            _etiqueta = etiqueta;
            _idArticulo = idArticulo;
            _cantidad = cantidad;
              _fechaVencimiento = fechaVencimiento;
            _lote = lote;
            _unidadMedida = unidadMedida;
            _esGranel = esGranel;
            _nombreArticulo = nombreArticulo;
            UnidadInventario = unidadInventario;
        }
        public long IdUbicacion
        {
            set { _idUbicacion = value; }
            get { return _idUbicacion; }
        }

        public string Etiqueta
        {
            set { _etiqueta = value; }
            get { return _etiqueta; }
        }

        public long IdArticulo
        {
            set { _idArticulo = value; }
            get { return _idArticulo; }
        }
        public int Cantidad
        {
            set { _cantidad = value; }
            get { return _cantidad; }            
        }

        public DateTime FechaVencimiento
        {
            set { _fechaVencimiento = value; }
            get { return _fechaVencimiento; }
        }
        public string Lote
        {
            set { _lote = value; }
            get { return _lote; }
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

        public string IdInterno {
            get { return _idInterno; }
            set { _idInterno = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diverscan.MJP.UI.Administracion.Inventario
{
    public class ArticulosAgrupados
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
        public List<long> IdsTRA = new List<long>();

        public ArticulosAgrupados(long idArticulo, DateTime fechaVencimiento,
            string lote, long idUbicacion, string etiqueta, int cantidad, string unidadMedida, bool esGranel, string nombreArticulo)
        {
            _idArticulo = idArticulo;
            _fechaVencimiento = fechaVencimiento;
            _lote = lote;
            _idUbicacion = idUbicacion;
            _etiqueta = etiqueta;
            _cantidad = cantidad;

            _unidadMedida = unidadMedida;
            _esGranel = esGranel;

            _nombreArticulo = nombreArticulo;
        }
        public long IdArticulo
        {
            set { _idArticulo = value; }
            get { return _idArticulo; }
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
        public int Cantidad
        {
            set { _cantidad = value; }
            get { return _cantidad; }
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
    }
}
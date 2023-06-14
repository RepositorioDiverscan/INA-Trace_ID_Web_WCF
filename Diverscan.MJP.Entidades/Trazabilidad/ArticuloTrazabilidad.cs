using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Trazabilidad
{
    public class ArticuloTrazabilidad
    {
        int _idEstado;
        string _lote;
        int _cantidadPorLote;
        long _idUbicacion;
        DateTime _fechaMovimiento;
        string _etiqueta;
        bool _esGranel;
        string _unidadMedida;

        public ArticuloTrazabilidad( int idEstado,string lote, int cantidadPorLote, 
            long idUbicacion, DateTime fechaMovimiento,string etiqueta, bool esGranel,string unidadMedida)
        {
            _idEstado = idEstado;
            _lote = lote;
            _cantidadPorLote = cantidadPorLote;
            _idUbicacion = idUbicacion;
            _fechaMovimiento = fechaMovimiento;
            _etiqueta = etiqueta;
            _esGranel = esGranel;
            _unidadMedida = unidadMedida;
        }     

        public int IdEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }
        public string Lote
        {
            get { return _lote; }
            set { _lote = value; }
        }
        public int CantidadPorLote
        {
            get { return _cantidadPorLote; }
            set { _cantidadPorLote = value; }
        }
        public long IdUbicacion
        {
            get { return _idUbicacion; }
            set { _idUbicacion = value; }
        }        
        public DateTime FechaMovimiento
        {
            get { return _fechaMovimiento; }
            set { _fechaMovimiento = value; }
        }        
        public string Etiqueta
        {
            get { return _etiqueta; }
            set { _etiqueta = value; }
        }        
        public bool EsGranel
        {
            get { return _esGranel; }
            set { _esGranel = value; }
        }
        public string UnidadMedida
        {
            get { return _unidadMedida; }
            set { _unidadMedida = value; }
        }        
    }
}

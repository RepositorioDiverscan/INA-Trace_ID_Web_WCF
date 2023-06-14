using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.MaestroArticulo
{
    [Serializable]
    public class e_MaestroArticulo
    {
        long _idArticulo;
        string _idCompania;
        string _Nombre;
        string _NombreHH;
        string _GTIN;
        string _UnidadesMedidaNombre;
        string _TiposEmpaqueNombre;
        string _BodegaNombre;
        string _FamiliaNombre;
        bool _Granel;
        decimal _TemperaturaMaxima;
        decimal _TemperaturaMinima;
        int _DiasMinimosVencimiento;
        string _idInterno;
        decimal _Contenido;
        string _Unidad_Medida;
        int _DiasMinimosVencimientoRestaurantes;
        decimal _minPicking;
        bool trazable;


        public e_MaestroArticulo(long idArticulo,string idCompania,string Nombre,string NombreHH,string GTIN,string UnidadesMedidaNombre,string TiposEmpaqueNombre, string BodegaNombre,string FamiliaNombre,bool Granel,decimal TemperaturaMaxima,decimal TemperaturaMinima,int DiasMinimosVencimiento,string idInterno,decimal Contenido,string Unidad_Medida,int DiasMinimosVencimientoRestaurantes) 
        {
            _idArticulo = idArticulo;
            _idCompania = idCompania;
            _Nombre = Nombre;
            _NombreHH = NombreHH;
            _GTIN = GTIN;
            _UnidadesMedidaNombre = UnidadesMedidaNombre;
            _TiposEmpaqueNombre = TiposEmpaqueNombre;
            _BodegaNombre = BodegaNombre;
            _FamiliaNombre = FamiliaNombre;
            _Granel = Granel;
            _TemperaturaMaxima = TemperaturaMaxima;
            _TemperaturaMinima = TemperaturaMinima;
            _DiasMinimosVencimiento = DiasMinimosVencimiento;
            _idInterno = idInterno;
            _Contenido = Contenido;
            _Unidad_Medida = Unidad_Medida;
            _DiasMinimosVencimientoRestaurantes = DiasMinimosVencimientoRestaurantes;
        }

        //Constructor utilizado para obtener detalles del artículo en predetalle solicitud
        public e_MaestroArticulo(string idInterno, string NombreArticulo, string UnidadMedida, decimal Contenido, string Empaque, bool Granel, int DiasMinimosVencimiento)
        {
            _idInterno              = idInterno;
            _Nombre                 = NombreArticulo;
            _Unidad_Medida          = UnidadMedida;
            _Contenido              = Contenido;
            _TiposEmpaqueNombre     = Empaque;
            _Granel                 = Granel;                       
            _DiasMinimosVencimiento = DiasMinimosVencimiento;
                                
        }

        public e_MaestroArticulo()
        {

        }
        public long IdArticulo
        {
            get { return _idArticulo; }
            set { _idArticulo = value; }
        }

        public string IdCompania
        {
            get { return _idCompania; }
            set { _idCompania = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string NombreHH
        {
            get { return _NombreHH; }
            set { _NombreHH = value; }
        }

        public string GTIN
        {
            get { return _GTIN; }
            set { _GTIN = value; }
        }

        public string UnidadesMedidaNombre
        {
            get { return _UnidadesMedidaNombre; }
            set { _UnidadesMedidaNombre = value; }
        }

        public string TiposEmpaqueNombre
        {
            get { return _TiposEmpaqueNombre; }
            set { _TiposEmpaqueNombre = value; }
        }

        public string BodegaNombre
        {
            get { return _BodegaNombre; }
            set { _BodegaNombre = value; }
        }

        public string FamiliaNombre
        {
            get { return _FamiliaNombre; }
            set { _FamiliaNombre = value; }
        }

        public bool Granel
        {
            get { return _Granel; }
            set { _Granel = value; }
        }

        public decimal TemperaturaMaxima
        {
            get { return _TemperaturaMaxima; }
            set { _TemperaturaMaxima = value; }
        }

        public decimal TemperaturaMinima
        {
            get { return _TemperaturaMinima; }
            set { _TemperaturaMinima = value; }
        }

        public int DiasMinimosVencimiento
        {
            get { return _DiasMinimosVencimiento; }
            set { _DiasMinimosVencimiento = value; }
        }

        public string IdInterno
        {
            get { return _idInterno; }
            set { _idInterno = value; }
        }

        public decimal Contenido
        {
            get { return _Contenido; }
            set { _Contenido = value; }
        }

        public string Unidad_Medida
        {
            get { return _Unidad_Medida; }
            set { _Unidad_Medida = value; }
        }

        public int DiasMinimosVencimientoRestaurantes
        {
            get { return _DiasMinimosVencimientoRestaurantes; }
            set { _DiasMinimosVencimientoRestaurantes = value; }
        }

        public decimal MinPicking { get => _minPicking; set => _minPicking = value; }
        public bool Trazable { get => trazable; set => trazable = value; }
    }
}

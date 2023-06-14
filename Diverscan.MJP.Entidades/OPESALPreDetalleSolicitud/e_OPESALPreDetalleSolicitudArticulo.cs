using Diverscan.MJP.Entidades.MaestroArticulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.OPESALPreDetalleSolicitud
{
    public class e_OPESALPreDetalleSolicitudArticulo : e_MaestroArticulo
    {
        private decimal _unidadesAlisto;
        private decimal _unidadesAlistoInventario;
        private string  _unidadesAlistoDetalle;
        private decimal _cantidadUnidadMedida;
        private decimal _cantidadgtin;
        private string _gtin;
        //Permite obtener los detalles de un artículo para insertarlo en el Predetalle de la solicitud
        public e_OPESALPreDetalleSolicitudArticulo(string idInterno, string NombreArticulo, string UnidadMedida, decimal Contenido, string Empaque, bool Granel, int DiasMinimosVencimiento, decimal UnidadesAlisto, decimal UnidadesAlistoInventario, string UnidadesAlistoDetalle, decimal CantidadUnidadMedida, 
            decimal cantidadgtin, string gtin_leido) 
        : base(idInterno, NombreArticulo, UnidadMedida, Contenido, Empaque, Granel, DiasMinimosVencimiento)
        {
            _unidadesAlisto             = UnidadesAlisto;
            _unidadesAlistoInventario   = UnidadesAlistoInventario;
            _unidadesAlistoDetalle      = UnidadesAlistoDetalle;
            _cantidadUnidadMedida       = CantidadUnidadMedida;
            _cantidadgtin = cantidadgtin;
            _gtin = gtin_leido;

        }
        public e_OPESALPreDetalleSolicitudArticulo()
        {}

        public decimal UnidadesAlisto
        {
            get { return _unidadesAlisto; }
            set { _unidadesAlisto = value; }
        }
        public decimal UnidadesAlistoInventario
        {
            get { return _unidadesAlistoInventario; }
            set { _unidadesAlistoInventario = value; }
        }
        public string UnidadesAlistoDetalle
        {
            get { return _unidadesAlistoDetalle; }
            set { _unidadesAlistoDetalle = value; }
        }
        public decimal CantidadUnidadMedida
        {
            get { return _cantidadUnidadMedida; }
            set { _cantidadUnidadMedida = value; }
        }

        public decimal CantidadGtin
        {
            get { return _cantidadgtin; }
            set { _cantidadgtin = value; }
        }

        public string Gtin
        {
            get { return _gtin; }
            set { _gtin = value; }
        }
    }
}

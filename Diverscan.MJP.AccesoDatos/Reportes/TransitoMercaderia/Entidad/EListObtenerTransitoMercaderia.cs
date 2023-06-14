using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.TransitoMercaderia.Entidad
{
    [Serializable]
    public class EListObtenerTransitoMercaderia
    {
        string _sku,
            _nombreArticulo;
        int _unidad,
            _diasUbicacion;
        string _ubicacion,
            _zona;

        public EListObtenerTransitoMercaderia(string sku, string nombreArticulo, int unidad, int diasUbicacion, string ubicacion, string zona)
        {
            _sku = sku;
            _nombreArticulo = nombreArticulo;
            _unidad = unidad;
            _diasUbicacion = diasUbicacion;
            _ubicacion = ubicacion;
            _zona = zona;
        }

        public string Sku { get => _sku; set => _sku = value; }
        public string NombreArticulo { get => _nombreArticulo; set => _nombreArticulo = value; }
        public int Unidad { get => _unidad; set => _unidad = value; }
        public int DiasUbicacion { get => _diasUbicacion; set => _diasUbicacion = value; }
        public string Ubicacion { get => _ubicacion; set => _ubicacion = value; }
        public string Zona { get => _zona; set => _zona = value; }
    }
}

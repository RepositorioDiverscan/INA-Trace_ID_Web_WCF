using System;

namespace Diverscan.MJP.Entidades.Operacion.Salidas
{
    [Serializable]
    public class e_Articulos_SSCC_Procesado
    {
        private string _nombreArticulo;
        private decimal _cantidadUI;
        private string _unidadMedidaUI;

        public e_Articulos_SSCC_Procesado() { }
        public e_Articulos_SSCC_Procesado(string nombreArticulo, decimal cantidadUI, string unidadMedidaUI)
        {
            _nombreArticulo = nombreArticulo;
            _cantidadUI = cantidadUI;
            _unidadMedidaUI = unidadMedidaUI;
        }

        public string NombreArticulo { get { return _nombreArticulo; } set { _nombreArticulo = value; } }
        public decimal CantidadUI { get { return _cantidadUI; } set { _cantidadUI = value; } }
        public string UnidadMedidaUI { get { return _unidadMedidaUI; } set { _unidadMedidaUI = value; } }
    }
}

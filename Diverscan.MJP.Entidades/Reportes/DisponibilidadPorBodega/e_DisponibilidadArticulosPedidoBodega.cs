using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes.DisponibilidadPorBodega
{
    [Serializable]
    public class e_DisponibilidadArticulosPedidoBodega
    {
        private string _idInternoArticulo;
        private string _nombreArticulo;
        private string _unidadMedida;
        private decimal? _cantidadEnPedido;
        private decimal? _cantidadEnBodega;
        private decimal? _diferenciaBodegaPedido;
        private string _tipoArticulo;

        public e_DisponibilidadArticulosPedidoBodega(string idInternoArticulo, string nombreArticulo, string unidadMedida, decimal? cantidadEnPedido, decimal? cantidadEnBodega, decimal? diferenciaBodegaPedido, string tipoArticulo)
        {
            //_idInternoArticulo = idInternoArticulo ?? throw new ArgumentNullException(nameof(idInternoArticulo));
            //_nombreArticulo = nombreArticulo ?? throw new ArgumentNullException(nameof(nombreArticulo));
            //_unidadMedida = unidadMedida ?? throw new ArgumentNullException(nameof(unidadMedida));
            //_cantidadEnPedido = cantidadEnPedido ?? throw new ArgumentNullException(nameof(cantidadEnPedido));
            //_cantidadEnBodega = cantidadEnBodega ?? throw new ArgumentNullException(nameof(cantidadEnBodega));
            //_diferenciaBodegaPedido = diferenciaBodegaPedido ?? throw new ArgumentNullException(nameof(diferenciaBodegaPedido));
            //_tipoArticulo = tipoArticulo ?? throw new ArgumentNullException(nameof(tipoArticulo));

            _idInternoArticulo = idInternoArticulo;
            _nombreArticulo = nombreArticulo;
            _unidadMedida = unidadMedida;
            _cantidadEnPedido = cantidadEnPedido;
            _cantidadEnBodega = cantidadEnBodega;
            _diferenciaBodegaPedido = diferenciaBodegaPedido;
            _tipoArticulo = tipoArticulo;

        }

        public string IdInternoArticulo 
        {
            get { return _idInternoArticulo; }
            set { _idInternoArticulo = value; }
        }
        public string NombreArticulo
        {
            get { return _nombreArticulo; }
            set { _nombreArticulo = value; }
        }
        public string UnidadMedida
        {
            get { return _unidadMedida; }
            set { _unidadMedida = value; }
        }
        public decimal? CantidadEnPedido
        {
            get { return _cantidadEnPedido; }
            set { _cantidadEnPedido = value; }
        }
        public decimal? CantidadEnBodega
        {
            get { return _cantidadEnBodega; }
            set { _cantidadEnBodega = value; }
        }
        public decimal? DiferenciaBodegaPedido
        {
            get { return _diferenciaBodegaPedido; }
            set { _diferenciaBodegaPedido = value; }
        }
        public string TipoArticulo
        {
            get { return _tipoArticulo; }
            set { _tipoArticulo = value; }
        }
    }
}

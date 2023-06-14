using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes.Kardex
{
    [Serializable]
    public class e_Detalle_Despacho_Por_Numero_Solicitud
    {
        private string _codigo { get; set; }
        private long _sscc{ get; set; }
        private string _ssccEtiqueta{ get; set; }
        private string _lote{ get; set; }
        private string _descripcion{ get; set; }
        private int _bultos{ get; set; }
        private string _bultosUnidadMedida { get; set; }
        private string _bultosUnidadMedidaConcatenado { get; set; }
        private string _ubicacion{ get; set; }
        private decimal _pedidoUI{ get; set; } //Undades Inventario
        private decimal _alistadoUI{ get; set; } //Undades Inventario
        private string _unidadMedidaUI{ get; set; }
        private decimal _alistadoUF{ get; set; } //Unidades físicas
        private string _empaqueUF{ get; set; }
        private string _und{ get; set; }
        private decimal _costo{ get; set; }
        private decimal _total{ get; set; }
        private DateTime _fechaPedido{ get; set; }
        private string _placa{ get; set; }
        private string _marcaVehiculo{ get; set; }
        private string _modelo{ get; set; }
        private string _marcaModeloVehiculo;
        private string _fechaPedidoExport;        

        public e_Detalle_Despacho_Por_Numero_Solicitud() { }

        public e_Detalle_Despacho_Por_Numero_Solicitud(
            string codigo,
            long sscc,
            string ssccEtiqueta,
            string lote,
            string descripcion,
            int bultos,
            string bultosUnidadMedida,
            string ubicacion,
            decimal pedidoUI, //Undades Inventario
            decimal alistadoUI, //Undades Inventario
            string unidadMedidaUI,
            decimal alistadoUF, //Unidades físicas
            string empaqueUF,
            string und,
            decimal costo,
            decimal total,
            DateTime fechaPedido,
            string placa,
            string marcaVehiculo,
            string modelo)
        {
            _codigo = codigo;
            _sscc = sscc;
            _ssccEtiqueta = ssccEtiqueta;
            _lote = lote;
            _descripcion = descripcion;
            _bultos = bultos;
            _bultosUnidadMedida = bultosUnidadMedida;
            _ubicacion = ubicacion;
            _pedidoUI = pedidoUI;
            _alistadoUI = alistadoUI;
            _unidadMedidaUI = unidadMedidaUI;
            _alistadoUF = alistadoUF;
            _empaqueUF = empaqueUF;
            _und = und;
            _costo = costo;
            _total = total;
            _fechaPedido = fechaPedido;
            _placa = placa;
            _marcaVehiculo = marcaVehiculo;
            _modelo = modelo;
        }

        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        public long SSCC
        {
            get { return _sscc; }
            set { _sscc = value; }
        }
        public string SSCCEtiqueta
        {
            get { return _ssccEtiqueta; }
            set { _ssccEtiqueta = value; }
        }
        public string Lote
        {
            get { return _lote; }
            set { _lote = value; }
        }
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        public int Bultos
        {
            get { return _bultos; }
            set { _bultos = value; }
        }    
        public string BultosUnidadMedida
        {
            get { return _bultosUnidadMedida; }
            set { _bultosUnidadMedida = value; }
        }
        public string BultosUnidadMedidaConcatenado
        {
            get { return _bultos + " " + _bultosUnidadMedida; }
        }
        public string Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
        }
        public decimal PedidoUI
        {
            get { return _pedidoUI; }
            set { _pedidoUI = value; }
        }
        public decimal AlistadoUI
        {
            get { return _alistadoUI; }
            set { _alistadoUI = value; }
        }
        public string UnidadMedidaUI
        {
            get { return _unidadMedidaUI; }
            set { _unidadMedidaUI = value; }
        }
        public decimal AlistadoUF
        {
            get { return _alistadoUF; }
            set { _alistadoUF = value; }
        }
        public string EmpaqueUF
        {
            get { return _empaqueUF; }
            set { _empaqueUF = value; }
        }
        public string Und
        {
            get { return _und; }
            set { _und = value; }
        }
        public decimal Costo
        {
            get { return _costo; }
            set { _costo = value; }
        }
        public decimal Total
        {
            get { return _total; }
            set { _total = value; }
        }
        public DateTime FechaPedido
        {
            get { return _fechaPedido; }
            set { _fechaPedido = value; }
        }
        public string FechaPedidoExport
        {
            get { return _fechaPedido.ToShortDateString(); }
        }
        public string Placa
        {
            get { return _placa; }
            set { _placa = value; }
        }
        public string MarcaVehiculo
        {
            get { return _marcaVehiculo; }
            set { _marcaVehiculo = value; }
        }
        public string Modelo
        {
            get { return _modelo; }
            set { _modelo = value; }
        }
        public string MarcaModeloVehiculo
        {
            get { return _marcaVehiculo + " " + _modelo; }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Reportes
{
    public class e_OrdenCE
    {

        public int _Orden_Compra { get; set; }
        public string _Producto{ get; set; }
        public decimal _Cantidad_Recibida { get; set; }
        public decimal _Cantidad_Rechazada { get; set; }
        public decimal _Articulos_OC { get; set; }
        public DateTime _FechaRegistro { get; set; }
        public DateTime _FechaCreacion { get; set; }



        public e_OrdenCE(int Orden_Compra, string Producto, decimal Cantidad_Recibida, decimal Cantidad_Rechazada, decimal Articulos_OC,
            DateTime FechaRegistro, DateTime FechaCreacio)
        {
            _Orden_Compra = Orden_Compra;
            _Producto = Producto;
            _Cantidad_Recibida = Cantidad_Recibida;
            _Cantidad_Rechazada = Cantidad_Rechazada;
            _Articulos_OC = Articulos_OC;
            _FechaRegistro = FechaRegistro;
            _FechaCreacion = FechaCreacio; 

    }

        public int Orden_Compra
        {
            get { return _Orden_Compra; }
            set { _Orden_Compra = value; }
        }
        public string Producto
        {
            get { return _Producto; }
            set { _Producto = value; }
        }
        public decimal Cantidad_Recibida
        {
            get { return _Cantidad_Recibida; }
            set { _Cantidad_Recibida = value; }
        }
        public decimal Cantidad_Rechazada
        {
            get { return _Cantidad_Rechazada; }
            set { _Cantidad_Rechazada = value; }
        }
        public decimal Articulos_OC
        {
            get { return _Articulos_OC; }
            set { _Articulos_OC = value; }
        }

        public DateTime FechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        public DateTime FechaCreacion
        {
            get { return _FechaCreacion; }
            set { _FechaCreacion = value; }
        }


    }
}

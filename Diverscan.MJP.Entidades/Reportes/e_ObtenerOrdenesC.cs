using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes
{
   
    public class e_ObtenerOrdenesC 
    {

        private int _orden_compra;
        private string  _producto;
        private decimal _cantidad_recibida;
        private decimal _cantidad_rechazada;
        private decimal _articulos_oc;
       private DateTime _fecharegistro;
        private DateTime _fechacreacion;

        public e_ObtenerOrdenesC() { }

        public e_ObtenerOrdenesC(
         int orden_compra,
         string producto,
         decimal cantidad_recibida,
         decimal cantidad_rechazada,
         decimal articulos_oc,
        DateTime fecharegistro,
        DateTime fechacreacion)
        {
            _orden_compra = orden_compra;
            _producto = producto;
            _cantidad_recibida = cantidad_recibida;
            _cantidad_rechazada = cantidad_rechazada;
            _articulos_oc = articulos_oc;
            _fecharegistro = fecharegistro;
            _fechacreacion = fechacreacion;
        }

        public int Orden_Compra
        {
            get { return _orden_compra; }
            set { _orden_compra = value; }
        }
        public string Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        public decimal Cantidad_Recibida
        {
            get { return _cantidad_recibida; }
            set { _cantidad_recibida  = value; }
        }
        public decimal Cantidad_Rechazada
        {
            get { return _cantidad_rechazada; }
            set { _cantidad_rechazada = value; }
        }
        public decimal Articulos_OC
        {
            get { return _articulos_oc; }
            set { _articulos_oc = value; }
        }

        public DateTime FechaRegistro
        {
            get { return _fecharegistro; }
            set { _fecharegistro = value; }
        }

        public DateTime FechaCreacion
        {
            get { return _fechacreacion; }
            set { _fechacreacion = value; }
        }
    }
}

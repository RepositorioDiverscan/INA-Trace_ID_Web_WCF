using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes
{


    public class e_ObtenerDevoluciones
    {
      
        private string _nombre;
        private decimal _cantidad;
        private string _procedencia;
        private string _descripcion;
        private string _empaque;
        private string _motivo_devolucion;
        private DateTime _fechaIngreso;

        public e_ObtenerDevoluciones() { }

        public e_ObtenerDevoluciones(
        string nombre,
        decimal cantidad,
        string procedencia,
        string descripcion,
        string empaque,
        string Motivo_Devolucion,
        DateTime fechaIngreso)
        {
            _nombre = nombre;
            _cantidad = cantidad;
            _procedencia = procedencia;
            _descripcion = descripcion;
            _empaque = empaque;
            _motivo_devolucion = Motivo_Devolucion;
            _fechaIngreso = fechaIngreso; 

    }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public decimal Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        public string Procedencia
        {
            get { return _procedencia; }
            set { _procedencia = value; }
        }
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        public string Empaque
        {
            get { return _empaque; }
            set { _empaque = value; }
        }
        public string Motivo_Devolucion
        {
            get { return _motivo_devolucion; }
            set { _motivo_devolucion = value; }
        }
        public DateTime FechaIngreso
        {
            get { return _fechaIngreso; }
            set { _fechaIngreso = value; }
        }
       
    }
}

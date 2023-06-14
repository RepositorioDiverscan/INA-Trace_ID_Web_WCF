using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Trazabilidad.Entidades
{
    [Serializable]
    public class EListadoTrazabilidad
    {
        private int _idTrazabilidad;
        private decimal _cantidad;
        private int _idEstado;
        private string _operacion;
        private DateTime _fechaRegistro;
        private string _nombre;
        private decimal _saldo;

        public EListadoTrazabilidad(int IdTrazabilidad, decimal Cantidad, int IdEstado, string Operacion, DateTime FechaRegistro, string Nombre, decimal Saldo)
        {
            _idTrazabilidad = IdTrazabilidad;
            _cantidad = Cantidad;
            _idEstado = IdEstado;
            _operacion = Operacion;
            _fechaRegistro = FechaRegistro;
            _nombre = Nombre;
            _saldo = Saldo;

        }

        public int IdTrazabilidad
        {
            get { return _idTrazabilidad; }
        }

        public decimal Cantidad
        {
            get { return _cantidad; }
        }

        public int IdEstado
        {
            get { return _idEstado; }
        }

        public string Operacion
        {
            get { return _operacion; }
        }

        public DateTime FechaRegistro
        {
            get { return _fechaRegistro; }
        }

        public string Nombre
        {
            get { return _nombre; }
        }

        public decimal Saldo
        {
            get { return _saldo; }
        }

    }
}

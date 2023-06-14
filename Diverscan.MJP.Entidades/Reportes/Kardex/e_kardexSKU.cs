using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes.Kardex
{
   
    public class e_kardexSKU
    {
        private long _idTrazabilidad;
        private long _idUbicacion;
        private string _ubicacion;
        private int _idMetodoAccion;
        private string _metodo;
        private string _metodoDescripcion;
        private int _cantidad;
        private int _saldo;
        private string _fechaRegistro;
        private string _lote;
        private string _fechaVencimiento;
        private string _idInterno;
        private string _nombre;
        public e_kardexSKU(IDataReader reader)
        {
            _idTrazabilidad = Convert.ToInt32(reader["IdTrazabilidad"]);
            _idUbicacion = Convert.ToInt32(reader["idUbicacion"]);
            _ubicacion = Convert.ToString(reader["Ubicacion"]);
            _idMetodoAccion = Convert.ToInt32(reader["IdMetodoAccion"]);
            _metodo = Convert.ToString(reader["Metodo"]);
            _metodoDescripcion = Convert.ToString(reader["MetodoDescripcion"]);
            _cantidad = Convert.ToInt32(reader["Cantidad"]);
            _saldo = Convert.ToInt32(reader["Saldo"]);
            _fechaRegistro = Convert.ToString(reader["FechaRegistro"]);
            _lote = Convert.ToString(reader["Lote"]);
            _fechaVencimiento = Convert.ToString(reader["FechaVencimiento"]);
            _idInterno = Convert.ToString(reader["idInterno"]);
            _nombre = Convert.ToString(reader["Nombre"]);
            
        }
        public long IdTrazabilidad { get => _idTrazabilidad; set => _idTrazabilidad = value; }
        public long IdUbicacion { get => _idUbicacion; set => _idUbicacion = value; }
        public string Ubicacion { get => _ubicacion; set => _ubicacion = value; }
        public int IdMetodoAccion { get => _idMetodoAccion; set => _idMetodoAccion = value; }
        public string Metodo { get => _metodo; set => _metodo = value; }
        public string MetodoDescripcion { get => _metodoDescripcion; set => _metodoDescripcion = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
        public int Saldo { get => _saldo; set => _saldo = value; }
        public string FechaRegistro { get => _fechaRegistro; set => _fechaRegistro = value; }
        public string Lote { get => _lote; set => _lote = value; }
        public string FechaVencimiento { get => _fechaVencimiento; set => _fechaVencimiento = value; }
        public string IdInterno { get => _idInterno; set => _idInterno = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
    }
}

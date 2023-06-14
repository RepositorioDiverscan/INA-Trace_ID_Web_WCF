using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.Operaciones
{
    [Serializable]
    public class eReporteOC
    {
        private int _idMaestroOrdenCompra;
        private string _numTransaccion;
        private string _comentario;
        private DateTime _fechaCreacion;
        private string _numCertificdo;
        private int _procesada;
        private DateTime _fechaProcesamiento;
        private string _nombreProveedor;
        private string _usuario;
        private double _porcentajeRecepcion;

        public eReporteOC(IDataReader reader)
        {
            _idMaestroOrdenCompra = Convert.ToInt32(reader["IdMaestroOrdenCompra"]);
            _numTransaccion = Convert.ToString(reader["NumeroTransaccion"]);
            _comentario = Convert.ToString(reader["Comentario"]);
            _fechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]);
            _numCertificdo = Convert.ToString(reader["NumeroCertificado"]);
            _procesada = Convert.ToInt32(reader["Procesada"]);
            _fechaProcesamiento = Convert.ToDateTime(reader["FechaProcesamiento"]);
            _nombreProveedor = Convert.ToString(reader["NombreProveedor"]);
            _usuario = Convert.ToString(reader["Usuario"]);
            _porcentajeRecepcion = Convert.ToDouble(reader["PorcentajeRecepcion"]);
        }
        public int IdMaestroOrdenCompra { get => _idMaestroOrdenCompra; set => _idMaestroOrdenCompra = value; }
        public string NumTransaccion { get => _numTransaccion; set => _numTransaccion = value; }
        public string Comentario { get => _comentario; set => _comentario = value; }
        public DateTime FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public int Procesada { get => _procesada; set => _procesada = value; }
        public DateTime FechaProcesamiento { get => _fechaProcesamiento; set => _fechaProcesamiento = value; }
        public string NombreProveedor { get => _nombreProveedor; set => _nombreProveedor = value; }
        public double PorcentajeRecepcion { get => _porcentajeRecepcion; set => _porcentajeRecepcion = value; }
        public string Usuario { get => _usuario; set => _usuario = value; }
        public string NumCertificdo { get => _numCertificdo; set => _numCertificdo = value; }
    }
}

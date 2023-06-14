using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Vehiculo
{
    [Serializable]
    public class EVehiculo 
    {
        private long _idVehiculo;
        private long _idTransportista;
        private string _nombreTransportista;
        private int _idTipoVehiculo;
        private string _tipoVehiculo;
        private int _idMarca;
        private string _marca;
        private string _placa;
        private string _nombreVehiculo;
        private int _idColor;
        private string _color;
        private string _modelo;
        private string _comentario;
        private decimal _capacidadVolumen;
        private int _capacidadPeso;
        private decimal _capacidadVolumenUsado;
        private int _capacidadPesoUsado;
        private int _idBodega;
        private string _idInternoBodega;
        private bool _activo;
        private DateTime _fechaRegistro;
        private string _fechaRegistroAndroid;
        private int _idUsuarioRegistro;

        public EVehiculo()
        {

        }

        public EVehiculo(IDataReader reader)
        {
            _idVehiculo = long.Parse(reader["IdVehiculo"].ToString());
            _nombreTransportista = reader["Transportista"].ToString();
            TipoVehiculo = reader["TipoVehiculo"].ToString();
            _marca = reader["Marca"].ToString();
            _placa = reader["Placa"].ToString();
            _nombreVehiculo = reader["Nombre"].ToString();
            _color = reader["Color"].ToString();
            _modelo = reader["Modelo"].ToString();
            _comentario = reader["Comentario"].ToString();
            _capacidadPeso = int.Parse(reader["CapacidadPeso"].ToString());
            _capacidadVolumen = long.Parse(reader["CapacidadVolumen"].ToString());
            _capacidadPesoUsado = int.Parse(reader["CapacidadPesoUsado"].ToString());
            _capacidadVolumenUsado = long.Parse(reader["CapacidadVolumenUsado"].ToString());
            _fechaRegistroAndroid = reader["FechaRegistro"].ToString();
            _fechaRegistro = DateTime.Parse(_fechaRegistroAndroid);
        }

        public EVehiculo(int idTransportista, int idTipo, int idMarca, int idColor, string numeroPlaca, string modelo, decimal volumen, int peso, string comentario, int idBodega, bool activo, int idUsuarioRegistro)
        {
            _idTransportista = idTransportista;
            _idTipoVehiculo = idTipo;
            _idMarca = idMarca;
            _placa = numeroPlaca;
            _idColor = idColor;
            _modelo = modelo;
            _comentario = comentario;
            _capacidadVolumen = volumen;
            _capacidadPeso = peso;
            _idBodega = idBodega;
            _activo = activo;
            _idUsuarioRegistro = idUsuarioRegistro;
        }

        public long IdVehiculo { get => _idVehiculo; set => _idVehiculo = value; }
        public string NombreTransportista { get => _nombreTransportista; set => _nombreTransportista = value; }
        public string Marca { get => _marca; set => _marca = value; }
        public string Placa { get => _placa; set => _placa = value; }
        public string NombreVehiculo { get => _nombreVehiculo; set => _nombreVehiculo = value; }
        public string Color { get => _color; set => _color = value; }
        public string Modelo { get => _modelo; set => _modelo = value; }
        public string Comentario { get => _comentario; set => _comentario = value; }
        public decimal CapacidadVolumen { get => _capacidadVolumen; set => _capacidadVolumen = value; }
        public int CapacidadPeso { get => _capacidadPeso; set => _capacidadPeso = value; }
        public DateTime FechaRegistro { get => _fechaRegistro; set => _fechaRegistro = value; }
        public string FechaRegistroAndroid
        {
            get
            {
                _fechaRegistroAndroid = "";
                if (_fechaRegistro != null)
                {
                    _fechaRegistroAndroid = _fechaRegistro.ToShortDateString();
                }
                return _fechaRegistroAndroid;
            }
            set
            {
                _fechaRegistroAndroid = value;
            }
        }
        public string TipoVehiculo { get => _tipoVehiculo; set => _tipoVehiculo = value; }
        public decimal CapacidadVolumenUsado { get => _capacidadVolumenUsado; set => _capacidadVolumenUsado = value; }
        public int CapacidadPesoUsado { get => _capacidadPesoUsado; set => _capacidadPesoUsado = value; }
        public int IdBodega { get => _idBodega; set => _idBodega = value; }
        public bool Activo { get => _activo; set => _activo = value; }
        public long IdTransportista { get => _idTransportista; set => _idTransportista = value; }
        public int IdTipoVehiculo { get => _idTipoVehiculo; set => _idTipoVehiculo = value; }
        public int IdColor { get => _idColor; set => _idColor = value; }
        public int IdMarca { get => _idMarca; set => _idMarca = value; }
        public int IdUsuarioRegistro { get => _idUsuarioRegistro; set => _idUsuarioRegistro = value; }
        public string IdInternoBodega { get => _idInternoBodega; set => _idInternoBodega = value; }
    }
}

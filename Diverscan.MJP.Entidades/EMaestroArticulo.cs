using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    public class EMaestroArticulo
    {
        private int _idArticulo;
        private string _idInterno;
        private string _nombre;
        private string _gtin;
        private string _nombreFamilia;
        private decimal _contenido;
        private string _unidad_Medida;
        private bool _activo;
        private string _fechaRegistro;

        public EMaestroArticulo(IDataReader reader)
        {
            _idArticulo = Convert.ToInt32(reader["idArticulo"]);
            _idInterno = Convert.ToString(reader["idInterno"]);
            _nombre = Convert.ToString(reader["Nombre"]);
            _gtin = Convert.ToString(reader["GTIN"]);
            _nombreFamilia = Convert.ToString(reader["NombreFamilia"]);
            _contenido = Convert.ToDecimal(reader["Contenido"]);
            _unidad_Medida = Convert.ToString(reader["Unidad_Medida"]);
            _activo = Convert.ToBoolean(reader["Activo"]);
            _fechaRegistro = Convert.ToString(reader["FechaRegistro"]);

        }
        public int IdArticulo { get => _idArticulo; set => _idArticulo = value; }
        public string IdInterno { get => _idInterno; set => _idInterno = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string NombreFamilia { get => _nombreFamilia; set => _nombreFamilia = value; }
        public decimal Contenido { get => _contenido; set => _contenido = value; }
        public string Unidad_Medida { get => _unidad_Medida; set => _unidad_Medida = value; }
        public bool Activo { get => _activo; set => _activo = value; }
        public string FechaRegistro { get => _fechaRegistro; set => _fechaRegistro = value; }
        public string Gtin { get => _gtin; set => _gtin = value; }
    }
}
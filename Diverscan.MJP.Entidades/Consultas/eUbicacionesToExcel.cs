using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Consultas
{
    public class eUbicacionesToExcel
    {
        private int _idUbi, _idBod, _secuencia;
        private string _nombreBodega, _idZona, _nombreZona, _estante, _nivel, _columna, _pos, _cara, _desc;
        private double _areaAncho, _largo, _alto, _profundidad, _capPesoKilos, _capVolumen;


        public int IdUbi { get => _idUbi; set => _idUbi = value; }
        public int IdBod { get => _idBod; set => _idBod = value; }
        public int Secuencia { get => _secuencia; set => _secuencia = value; }
        public string NombreBodega { get => _nombreBodega; set => _nombreBodega = value; }
        public string IdZona { get => _idZona; set => _idZona = value; }
        public string NombreZona { get => _nombreZona; set => _nombreZona = value; }
        public string Estante { get => _estante; set => _estante = value; }
        public string Nivel { get => _nivel; set => _nivel = value; }
        public string Columna { get => _columna; set => _columna = value; }
        public string Pos { get => _pos; set => _pos = value; }
        public string Cara { get => _cara; set => _cara = value; }
        public string Desc { get => _desc; set => _desc = value; }
        public double AreaAncho { get => _areaAncho; set => _areaAncho = value; }
        public double Largo { get => _largo; set => _largo = value; }
        public double Alto { get => _alto; set => _alto = value; }
        public double Profundidad { get => _profundidad; set => _profundidad = value; }
        public double CapPesoKilos { get => _capPesoKilos; set => _capPesoKilos = value; }
        public double CapVolumen { get => _capVolumen; set => _capVolumen = value; }
    }
}

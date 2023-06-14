using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Trazabilidad.Entidades
{
    public class EConsultaArticulos
    {
        private readonly int _idArticulo;
        private readonly string _nombre;
        private readonly string _GTIN;
        public EConsultaArticulos(int idArticulo, string nombre, string gTIN)
        {
            _idArticulo = idArticulo;
            _nombre = nombre;
            _GTIN = gTIN;
        }

        public int IdArticulo
        {
            get { return _idArticulo; }
        }

        public string Nombre
        {
            get { return _nombre; }
        }
        public string GTIN
        {
            get { return _GTIN; }
        }
        public string Nombre_GTIN
        {
            get { return _nombre + " -- " + _GTIN; }
        }
    }
}

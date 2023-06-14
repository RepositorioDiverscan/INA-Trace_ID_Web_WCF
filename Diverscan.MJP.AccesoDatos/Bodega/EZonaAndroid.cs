using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Bodega
{
    public class EZonaAndroid
    {
        private int _idZona;
        private string _nombre;
        
        public string Nombre { get => _nombre; set => _nombre = value; }
        public int IdZona { get => _idZona; set => _idZona = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.TomasFisicas
{
    [Serializable]
    public class eTomaFisicaDe
    {
        public string IdInterno { get; set; }
        public string NombreProducto { get; set; }
        public string CantidadBodega { get; set; }
        public string CantidadSistema { get; set; }
        public string CantidadSoap { get; set; }
    }
}

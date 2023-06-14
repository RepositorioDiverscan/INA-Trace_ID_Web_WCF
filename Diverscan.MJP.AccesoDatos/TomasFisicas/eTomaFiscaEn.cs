using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.TomasFisicas
{
    [Serializable]
    public class eTomaFiscaEn
    {
        public string NombreTomaFisica { get; set; }
        public string FechaTomaFisica { get; set; }
        public string Bodega { get; set; }
    }
}

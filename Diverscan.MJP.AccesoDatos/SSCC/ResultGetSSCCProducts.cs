using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SSCC
{
    public class ResultGetSSCCProducts
    {
        private EEncabezadoOlaSSCC olaSSCC = new EEncabezadoOlaSSCC();
        private bool state;
        private string message;

        public EEncabezadoOlaSSCC OlaSSCC { get => olaSSCC; set => olaSSCC = value; }
        public bool State { get => state; set => state = value; }
        public string Message { get => message; set => message = value; }
    }
}

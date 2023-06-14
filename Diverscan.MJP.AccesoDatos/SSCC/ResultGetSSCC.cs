using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SSCC
{
    public class ResultGetSSCC : ResultWS
    {
        private ESSCC _SSCC = new ESSCC();

        public ESSCC SSCC { get => _SSCC; set => _SSCC = value; }
    }
}

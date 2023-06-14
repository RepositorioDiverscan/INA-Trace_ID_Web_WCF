using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SSCC
{
     public class ResultGetSSCCDespatch : ResultWS
    {
        private ESSCC _SSCC = new ESSCC();
        private List<ESSCC> _SSCCOLA = new List<ESSCC>();

        public ESSCC SSCC { get => _SSCC; set => _SSCC = value; }
        public List<ESSCC> SSCCOLA { get => _SSCCOLA; set => _SSCCOLA = value; }
    }
}

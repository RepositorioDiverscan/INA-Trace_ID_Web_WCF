using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SSCC
{
    public class ResultGetSSCCS : ResultWS
    {
        private List<ESSCC> _SSCCS = new List<ESSCC>();

        public List<ESSCC> SSCCS { get => _SSCCS; set => _SSCCS = value; }
    }
}

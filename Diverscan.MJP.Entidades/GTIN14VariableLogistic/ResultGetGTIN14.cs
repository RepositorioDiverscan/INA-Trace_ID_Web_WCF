using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.GTIN14VariableLogistic
{
    public class ResultGetGTIN14 : ResultWS
    {
        private EGTIN14VariableLogistic gtin14Variable;

        public EGTIN14VariableLogistic Gtin14Variable { get => gtin14Variable; set => gtin14Variable = value; }
    }
}

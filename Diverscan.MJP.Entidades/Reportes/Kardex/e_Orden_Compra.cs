using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes.Kardex
{
    public class e_Orden_Compra
    {
        private string numeroOC;


        public e_Orden_Compra(string pNumeroOC)
        {
            numeroOC = pNumeroOC;
        }

        public string NumeroOC
        {
            get { return numeroOC; }
            set { numeroOC = value; }
        }
    }
}

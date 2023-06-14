using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes.Destino
{
    class e_Destino_Dev

    {
     
        private readonly string _destino;
        public e_Destino_Dev(string  destino)
        {
        
            _destino = destino; 
        }

        public string destino
        {
            get { return _destino; }
        }

 
    }
}

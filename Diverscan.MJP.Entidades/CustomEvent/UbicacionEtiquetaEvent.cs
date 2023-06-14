using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.CustomEvent
{
    public class UbicacionEtiquetaEvent : EventArgs
    {
        private readonly string _etiqueta;
        public UbicacionEtiquetaEvent(string etiqueta)
        {
            _etiqueta = etiqueta;
        }
        public string Etiqueta
        {
            get { return _etiqueta; }
        }
    }
}

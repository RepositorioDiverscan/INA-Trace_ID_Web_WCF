using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.DisponibleBodegaWebService
{
    [Serializable]
    public class EDisponibleBodega
    {
        public EDisponibleBodega()
        {
            itemCode = "";
            whsCode = "";
            onHand = "";
        }
        public string itemCode { get; set; }
        public string whsCode { get; set; }
        public string onHand { get; set; }
    }
}

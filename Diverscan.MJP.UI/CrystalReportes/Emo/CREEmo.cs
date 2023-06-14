
using Diverscan.MJP.AccesoDatos.Emo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.UI.CrystalReportes.Emo
{   
    public class CREEmo : EEmo
    {

        private int _quantityInvoices;
        public int QuantityInvoices { get => _quantityInvoices; set => _quantityInvoices = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Diverscan.MJP.Entidades.Devolutions.DevolutionProductLocation
{
    public class EProductLocation
    {
        private string _nameLocation;
        private string _quantityToLocate;

        public EProductLocation()
        {

        }

        public EProductLocation(string nameLocation, string quantityToLocate)
        {
            this.NameLocation = nameLocation;
            this.QuantityToLocate = quantityToLocate;
        }

        public string QuantityToLocate { get => _quantityToLocate; set => _quantityToLocate = value; }
        public string NameLocation { get => _nameLocation; set => _nameLocation = value; }
    }
}
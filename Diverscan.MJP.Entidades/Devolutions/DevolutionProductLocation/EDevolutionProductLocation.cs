using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Devolutions.DevolutionProductLocation
{
    public class EDevolutionProductLocation
    {
        private string _idOrderDevolution;
        private string _idProduct;
        private string _location;
        private string _quantity;
        private string _barCode;

        public EDevolutionProductLocation()
        {

        }

        public EDevolutionProductLocation( string idOrderDevolution,string idProduct,string location,
            string quantity,string barCode)
        {
            this._idOrderDevolution = idOrderDevolution;
            this._idProduct = idProduct;
            this._location = location;
            this._quantity = quantity;
            this._barCode = barCode;
        }

        public string IdOrderDevolution { get => _idOrderDevolution; set => _idOrderDevolution = value; }
        public string IdProduct { get => _idProduct; set => _idProduct = value; }
        public string Location { get => _location; set => _location = value; }
        public string Quantity { get => _quantity; set => _quantity = value; }
        public string BarCode { get => _barCode; set => _barCode = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Devolutions.DevolutionProductLocation
{
    public class EDevolutionProduct
    {
        private string _idOrderDevolution;
        private string _idProduct;
        private string _productCode;
        private string _productName;
        private string _quantity;
        private string _barCode;
        private string _lote;
        private string _dateExpery;
        private LinkedList<EProductLocation> _productLocations;

        public EDevolutionProduct()
        {
            this._productLocations = new LinkedList<EProductLocation>();
        }

        public EDevolutionProduct( string idOrderDevolution,string idProduct,
            string quantity,string barCode, string productCode, string productName)
        {
            this._idOrderDevolution = idOrderDevolution;
            this._idProduct = idProduct;
            this._productCode = productCode;
            this._productName = productName;
            this._quantity = quantity;
            this._barCode = barCode;
        }

        public EDevolutionProduct(string idOrderDevolution, string idProduct,
            string quantity, string barCode, string productCode, string productName,
            LinkedList<EProductLocation> productLocations)
        {
            this._idOrderDevolution = idOrderDevolution;
            this._idProduct = idProduct;
            this._productCode = productCode;
            this._productName = productName;
            this._quantity = quantity;
            this._barCode = barCode;
            this._productLocations = productLocations;
        }

        public string IdOrderDevolution { get => _idOrderDevolution; set => _idOrderDevolution = value; }
        public string IdProduct { get => _idProduct; set => _idProduct = value; }        
        public string Quantity { get => _quantity; set => _quantity = value; }
        public string BarCode { get => _barCode; set => _barCode = value; }
        public string ProductCode { get => _productCode; set => _productCode = value; }
        public string ProductName { get => _productName; set => _productName = value; }
        public string Lote { get => _lote; set => _lote = value; }
        public string DateExpery { get => _dateExpery; set => _dateExpery = value; }
        public LinkedList<EProductLocation> ProductLocations { get => _productLocations; set => _productLocations = value; }


    }
}

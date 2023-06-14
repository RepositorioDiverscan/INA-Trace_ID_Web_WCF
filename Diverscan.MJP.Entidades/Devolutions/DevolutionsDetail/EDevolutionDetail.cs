using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Diverscan.MJP.Entidades.Devolutions.DevolutionsDetail
{
    //[DataContract]
    public class EDevolutionDetail
    {
        private string _idDevolutionHeader;
        private string _idProduct;
        private string _productCode;
        private string _productName;
        private string _quantity;
        private int _apply;
        private int _noApply;

        public EDevolutionDetail()
        {

        }

        public EDevolutionDetail(IDataReader reader)
        {
            this._idDevolutionHeader = reader["id_devolution_header"].ToString();
            this._idProduct = reader["id_product"].ToString();
            this._productCode = reader["product_code"].ToString();
            this._productName = reader["product_name"].ToString();
            this._quantity = reader["quantity"].ToString();
            this._apply = Convert.ToInt32(reader["apply"].ToString());
            this._noApply = Convert.ToInt32(reader["no_apply"].ToString());
        }

        public EDevolutionDetail(string idDevolutionHeader, string productCode, string productName, string quantity)
        {
            this._idDevolutionHeader = idDevolutionHeader;
            this._productCode = productCode;
            this._productName = productName;
            this._quantity = quantity;
        }
       
      // [DataMember]
        public string IdDevolutionHeader { get => _idDevolutionHeader; set => _idDevolutionHeader = value;  }

      // [DataMember]
        public string ProductCode { get => _productCode; set => _productCode = value; }

      // [DataMember]
        public string Quantity { get => _quantity; set => _quantity = value; }

      // [DataMember]
        public string ProductName { get => _productName; set => _productName = value; }

      // [DataMember]
        public int Apply { get => _apply; set => _apply = value; }

      // [DataMember]
        public int NoApply { get => _noApply; set => _noApply = value; }

        public string IdProduct { get => _idProduct; set => _idProduct = value; }
    }
}
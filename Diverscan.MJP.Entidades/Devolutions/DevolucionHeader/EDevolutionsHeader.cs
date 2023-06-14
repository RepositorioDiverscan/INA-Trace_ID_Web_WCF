using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Diverscan.MJP.Entidades.Devolutions.DevolucionHeader
{
   
    public class EDevolutionsHeader
    {
        private  int _idDevolutionHeader;
        private string _userName;
        private string _userEmail;
        private string _dateRegister;
        private string _reason;
        private string _descriptionDevolution;
        private string _client;
        private string _pointSale;
        private string _orderStatus;

        public EDevolutionsHeader()
        {

        }
        public EDevolutionsHeader(IDataReader reader)
        {
            _idDevolutionHeader = Convert.ToInt32(reader["id_devolution_header"].ToString());
            _userName = reader["user_name"].ToString();
            _userEmail = reader["user_email"].ToString();
            _dateRegister = reader["date_register"].ToString();
            _reason = reader["reason"].ToString();
            _descriptionDevolution = reader["description_devolution"].ToString();
            _client = reader["client"].ToString();
            _pointSale = reader["point_sale"].ToString();
        }
      
        public string UserName { get => _userName; set => _userName = value; }
       
        public string DateRegister { get => _dateRegister; set => _dateRegister = value; }
       
        public string Reason { get => _reason; set => _reason = value; }
       
        public string DescriptionDevolution { get => _descriptionDevolution; set => _descriptionDevolution = value; }
       
        public string Client { get => _client; set => _client = value; }
       
        public string UserEmail { get => _userEmail; set => _userEmail = value; }
       
        public string OrderStatus { get => _orderStatus; set => _orderStatus = value; }

        public int IdDevolutionHeader { get => _idDevolutionHeader; set => _idDevolutionHeader = value; }

        public string PointSale { get => _pointSale; set => _pointSale = value; }
    }
}
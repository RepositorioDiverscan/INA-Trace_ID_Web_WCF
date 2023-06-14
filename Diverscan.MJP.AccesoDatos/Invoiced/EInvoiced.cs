using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Invoiced
{
    [Serializable]
    public class EInvoiced
    {
        private long _idRecord;
        private string _idSap;
        private DateTime _recordDate;
        private string _billNumber;
        private decimal _billPrice;
        private string _typeBill;
        private bool _isEmo;
        private decimal _weight;
        private decimal _volume;
        private long _idOla;
        private string _nameClient;
        private string _codeClient;
        private int _numberPage;
        public EInvoiced()
        {
        }

        public EInvoiced(IDataReader reader)
        {
            _idRecord = long.Parse(reader["IdRecord"].ToString());
            _idSap = reader["IdSap"].ToString();
            _recordDate = Convert.ToDateTime(reader["RecordDate"].ToString());
            _billNumber = reader["BillNumber"].ToString();
            _billPrice = Convert.ToDecimal(reader["BillPrice"].ToString());
            bool resultParse = false;
            if (Boolean.TryParse(reader["IsEmo"].ToString(), out resultParse))
                _isEmo = resultParse;
        }

        public EInvoiced(long idRecord, string idSap, DateTime recordDate, string billNumber, decimal billPrice, bool isEmo)
        {
            _idRecord = idRecord;
            _idSap = idSap;
            _recordDate = recordDate;
            _billNumber = billNumber;
            _billPrice = billPrice;
            _isEmo = isEmo;
        }

        public long IdRecord { get => _idRecord; set => _idRecord = value; }
        public string IdSap { get => _idSap; set => _idSap = value; }
        public DateTime RecordDate { get => _recordDate; set => _recordDate = value; }
        public string BillNumber { get => _billNumber; set => _billNumber = value; }
        public decimal BillPrice { get => _billPrice; set => _billPrice = value; }
        public bool IsEmo { get => _isEmo; set => _isEmo = value; }
        public decimal Weight { get => _weight; set => _weight = value; }
        public decimal Volume { get => _volume; set => _volume = value; }
        public long IdOla { get => _idOla; set => _idOla = value; }
        public string NameClient { get => _nameClient; set => _nameClient = value; }
        public string CodeClient { get => _codeClient; set => _codeClient = value; }
        public string TypeBill { get => _typeBill; set => _typeBill = value; }
        public int NumberPage { get => _numberPage; set => _numberPage = value; }
    }
}

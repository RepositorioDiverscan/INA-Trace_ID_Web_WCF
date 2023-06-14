using Diverscan.MJP.AccesoDatos.Invoiced;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Emo
{
    [Serializable]
    [DataContract]
    public class EEmo
    {
        private long _idEmo;
        private long _idTransportista;
        private string _nombreTransportista;
        private long _idBodega;
        private string _nombreUsuario;
        private string _idInterno;
        private DateTime _recordDate;
        private decimal _totalPeso;
        private decimal _totalMonto;
        private bool _state;
        private List<EInvoiced> _detailEmo;

        public EEmo()
        {                
        }

        public EEmo(IDataReader reader)
        {
            IdEmo = long.Parse(reader["idEmo"].ToString());
            _idTransportista = long.Parse(reader["idTransportista"].ToString());
            _nombreTransportista = reader["Nombre"].ToString();
            _idBodega = long.Parse(reader["idBodega"].ToString());           
            _recordDate = Convert.ToDateTime(reader["fechaCreacion"].ToString());
            _totalPeso = decimal.Parse(reader["totalPeso"].ToString());
            _totalMonto = decimal.Parse(reader["totalMonto"].ToString());
            _idInterno = reader["idInterno"].ToString();
            bool resultParse = false;
            _state = Boolean.TryParse(reader["estado"].ToString(), out resultParse);
            _detailEmo = new List<EInvoiced>();
        }
        public EEmo(long idEmo, long idTransportista, string nombreTransportista, long idBodega, DateTime recordDate, decimal totalPeso, decimal totalMonto, bool state)
        {
            IdEmo = idEmo;
            _idTransportista = idTransportista;
            _nombreTransportista = nombreTransportista;
            _idBodega = idBodega;
            _recordDate = recordDate;
            _totalPeso = totalPeso;
            _totalMonto = totalMonto;
            _state = state;
        }

        [DataMember]
        public long IdTransportista { get => _idTransportista; set => _idTransportista = value; }
        [DataMember]
        public long IdBodega { get => _idBodega; set => _idBodega = value; }
        [DataMember]
        public DateTime RecordDate { get => _recordDate; set => _recordDate = value; }
        [DataMember]
        public bool State { get => _state; set => _state = value; }
        [DataMember]
        public string NombreTransportista { get => _nombreTransportista; set => _nombreTransportista = value; }
        [DataMember]
        public decimal TotalPeso { get => _totalPeso; set => _totalPeso = value; }
        [DataMember]
        public decimal TotalMonto { get => _totalMonto; set => _totalMonto = value; }
        [DataMember]
        public long IdEmo { get => _idEmo; set => _idEmo = value; }
        [DataMember]
        public List<EInvoiced> DetailEmo { get => _detailEmo; set => _detailEmo = value; }
        [DataMember]
        public string IdInterno { get => _idInterno; set => _idInterno = value; }
        [DataMember]
        public string NombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
    }
}

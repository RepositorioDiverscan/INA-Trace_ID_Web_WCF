using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.GTIN14VariableLogistic
{
    [DataContract]
    public class EGTIN14VariableLogistic
    {
        private int _idGTIN14;        
        private string _idBaseProduct;
        private string _idInterno;
        private string _GTIN14;
        private string _description;
        private double _quantity;
        private string _name;
        private double _contents;
        private bool _active;      

        public EGTIN14VariableLogistic()
        {
        }
        public EGTIN14VariableLogistic(IDataReader reader)
        {
            _idGTIN14 = Convert.ToInt32(reader["idGTIN14VariableLogistica"].ToString());
            _GTIN14 = reader["ConsecutivoGTIN14"].ToString();
            _description = reader["Descripcion"].ToString();
            _quantity = Double.Parse(reader["Cantidad"].ToString());
            _name = reader["Nombre"].ToString();           
            _contents = Convert.ToDouble(reader["Contenido"].ToString());
            _idInterno = reader["idInterno"].ToString();
            bool resultParse = false;
            _active = Boolean.TryParse(reader["Activo"].ToString(), out resultParse);
            _idBaseProduct = reader["idBaseArticulo"].ToString();                     
        }

        [DataMember]
        public int IdGTIN14 { get => _idGTIN14; set => _idGTIN14 = value; }

        [DataMember]
        public string IdBaseProduct { get => _idBaseProduct; set => _idBaseProduct = value; }

        [DataMember]
        public string IdInterno { get => _idInterno; set => _idInterno = value; }

        [DataMember]
        public string GTIN14 { get => _GTIN14; set => _GTIN14 = value; }

        [DataMember]
        public string Description { get => _description; set => _description = value; }

        [DataMember]
        public double Quantity { get => _quantity; set => _quantity = value; }

        [DataMember]
        public string Name { get => _name; set => _name = value; }

        [DataMember]
        public double Contents { get => _contents; set => _contents = value; }

        [DataMember]
        public bool Active { get => _active; set => _active = value; }       
    }
}

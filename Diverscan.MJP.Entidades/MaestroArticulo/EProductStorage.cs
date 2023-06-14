using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.MaestroArticulo
{
    [DataContract]
    public class EProductStorage
    {
        private int _idProduct;
        private string _nameProduct;
        private string _lot;
        private string _dateExp;
        private string _description;
        private int _quantity;

        public EProductStorage(IDataReader reader)
        {
            this._idProduct = Convert.ToInt32(reader["IdArticulo"].ToString());
            this._nameProduct = reader["Nombre"].ToString();
            this._quantity = Convert.ToInt32(reader["CantidadDisponible"].ToString());
            this._lot = reader["Lote"].ToString();
            this._dateExp = reader["FechaVencimiento"].ToString();
            this._description = reader["Descripcion"].ToString();
        }

        [DataMember]
        public int IdProduct { get => _idProduct; set => _idProduct = value; }

        [DataMember]
        public string NameProduct { get => _nameProduct; set => _nameProduct = value; }

        [DataMember]
        public string Lot { get => _lot; set => _lot = value; }

        [DataMember]
        public string DateExp { get => _dateExp; set => _dateExp = value; }

        [DataMember]
        public string Description { get => _description; set => _description = value; }

        [DataMember]
        public int Quantity { get => _quantity; set => _quantity = value; }
    }
}

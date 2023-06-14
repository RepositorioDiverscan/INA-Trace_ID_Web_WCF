using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.MaestroArticulo
{
    [Serializable]
    [DataContract]

    public class EMinPicking
    {
        private string _NombreArticulo { get; set; }
        private int _IdArticulo { get; set; }
        private int _CantidMinPicking { get; set; }
        private int _CantidadDisponible { get; set; }
        private double _Cosiente { get; set; }



        public EMinPicking(IDataReader reader)
        {
            this._IdArticulo = Convert.ToInt32(reader["IdArticulo"].ToString());
            this._NombreArticulo = reader["NombreArticulo"].ToString();
            this._CantidMinPicking = Convert.ToInt32(reader["MinPicking"].ToString());
            this._CantidadDisponible = Convert.ToInt32(reader["CantidadDisponible"].ToString());
            this._Cosiente = Convert.ToDouble(reader["Cosiente"].ToString());

        }

        [DataMember]
        public int IdArticulo { get => _IdArticulo; set => _IdArticulo = value; }

        [DataMember]
        public string NombreArticulo { get => _NombreArticulo; set => _NombreArticulo = value; }

        [DataMember]
        public int CantidMinPicking { get => _CantidMinPicking; set => _CantidMinPicking = value; }

        [DataMember]
        public int CantidadDisponible { get => _CantidadDisponible; set => _CantidadDisponible = value; }

        [DataMember]
        public double Cosiente { get => _Cosiente; set => _Cosiente = value; }

    }
}

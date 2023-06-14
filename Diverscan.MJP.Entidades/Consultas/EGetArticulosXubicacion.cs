using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.Consultas
{
    [DataContract]
    public class EGetArticulosXubicacion
    {
        string __nombre, _bodega, _ubicacion, _fechaRegistro;
        int _idArticulo, _cantidad;



        public EGetArticulosXubicacion(string nombre, string bodega, string ubicacion, string fechaRegistro, int idArticulo, int cantidad)
        {
            __nombre = nombre;
            _bodega = bodega;
            _ubicacion = ubicacion;
            _fechaRegistro = fechaRegistro;
            _idArticulo = idArticulo;
            _cantidad = cantidad;
        }

        public EGetArticulosXubicacion(IDataReader reader)
        {
            __nombre = reader["Nombre"].ToString();
            _bodega = reader["Bodega"].ToString();
            _ubicacion = reader["Ubicacion"].ToString();
            _fechaRegistro = reader["FechaRegistro"].ToString();
            _idArticulo = Convert.ToInt32((reader["idArticulo"].ToString()));
            _cantidad = Convert.ToInt32((reader["Cantidad"].ToString()));
        }


        [DataMember]
        public string Nombre { get => __nombre; set => __nombre = value; }

        [DataMember]
        public string Bodega { get => _bodega; set => _bodega = value; }

        [DataMember]
        public string Ubicacion { get => _ubicacion; set => _ubicacion = value; }

        [DataMember]
        public string FechaRegistro { get => _fechaRegistro; set => _fechaRegistro = value; }

        [DataMember]
        public int IdArticulo { get => _idArticulo; set => _idArticulo = value; }

        [DataMember]
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
    }
}

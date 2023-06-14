using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.OrdenCompra
{
    [DataContract]
    public class EGetPucharseOrder
    {
        int _idMOC;
        string _idInterno;
        int _idProveedor;
        string _nombreProveedor;
        string _fechaProcesamiento;
        private string _numeroCertificado;
        private short _tipoIngreso;

        public EGetPucharseOrder(int idMOC, string idInterno, int idProveedor, string fechaProcesamiento, short tipoIngreso)
        {
            _idMOC = idMOC;
            _idInterno = idInterno;
            _idProveedor = idProveedor;
            _fechaProcesamiento = fechaProcesamiento;
            _tipoIngreso = tipoIngreso;
        }
        public EGetPucharseOrder(IDataReader reader)
        {
            _idMOC = Convert.ToInt32((reader["idMOC"].ToString()));
            _idInterno = reader["idInterno"].ToString();
            _idProveedor = Convert.ToInt32(reader["idProveedor"].ToString());
            _nombreProveedor = reader["NombreProveedor"].ToString();
            _fechaProcesamiento = reader["FechaCreacion"].ToString();
            _numeroCertificado = reader["NumeroCertificado"].ToString();
            _tipoIngreso = Convert.ToInt16(reader["TipoIngreso"].ToString());
        }

        [DataMember]
        public int IdMOC
        {
            get { return _idMOC; }
            set { _idMOC = value; }
        }

        [DataMember]
        public string IdInterno
        {
            get { return _idInterno; }
            set { _idInterno = value; }
        }

        [DataMember]
        public int IdProveedor
        {
            get { return _idProveedor; }
            set { _idProveedor = value; }
        }

        [DataMember]
        public string FechaProcesamiento
        {
            get { return _fechaProcesamiento; }
            set { _fechaProcesamiento = value; }
        }

        [DataMember]
        public string NombreProveedor
        {
            get { return _nombreProveedor; }
            set { _nombreProveedor = value; }
        }

        [DataMember]
        public string NumeroCertificado { get => _numeroCertificado; set => _numeroCertificado = value; }

        [DataMember]
        public short TipoIngreso { get => _tipoIngreso; set => _tipoIngreso = value; }
    }
}

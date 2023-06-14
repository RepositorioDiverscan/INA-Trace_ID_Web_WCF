using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.PICKING
{
    [DataContract]
    public class PickingRecord
    {
        string _nombreArticulo;
        string _cantidad;
        string _etiquetaUbicacion;

        public PickingRecord(string nombreArticulo,string cantidad, string etiquetaUbicacion)
        {
            _nombreArticulo = nombreArticulo;
            _cantidad = cantidad;
            _etiquetaUbicacion = etiquetaUbicacion;
        }

        public PickingRecord(IDataReader reader)
        {
            _nombreArticulo = reader["Nombre"].ToString();
            //_cantidad = long.Parse(reader["Cantidad"].ToString());
            _cantidad = reader["Cantidad"].ToString();
            _etiquetaUbicacion = reader["EtiquetaUbicacion"].ToString();
        }
        [DataMember]
        public string NombreArticulo
        {
            get { return _nombreArticulo; }
            set { _nombreArticulo = value; }
        }
        [DataMember]
        public string Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        [DataMember]
        public string EtiquetaUbicacion
        {
            get { return _etiquetaUbicacion; }
            set { _etiquetaUbicacion = value; }
        }
    }
}

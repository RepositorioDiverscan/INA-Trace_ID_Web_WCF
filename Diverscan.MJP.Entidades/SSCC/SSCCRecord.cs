using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.SSCC
{
    [DataContract]
    public class SSCCRecord
    {
        int _idMaestroSolicitud;
        string _SSCCGenerado;
        string _Descripcion;
        double _Cantidad;
        string _Nombre;
       
        public SSCCRecord(int idMaestroSolicitud, string SSCCGenerado,  string Descripcion, double Cantidad, string Nombre)
        {
             _idMaestroSolicitud = idMaestroSolicitud;
             _SSCCGenerado = SSCCGenerado;
             _Descripcion = Descripcion;
             _Cantidad = Cantidad;
             _Nombre = Nombre;
        }

         public SSCCRecord(IDataReader reader)
        {

            _idMaestroSolicitud = int.Parse(reader["idMaestroSolicitud"].ToString());
            _SSCCGenerado = reader["SSCCGenerado"].ToString();
            _Descripcion = reader["Descripcion"].ToString();
            _Cantidad = long.Parse(reader["Cantidad"].ToString());
            _Nombre = reader["Nombre"].ToString();
        }

         [DataMember]
         public int idMaestroSolicitud
         {
             get { return _idMaestroSolicitud; }
             set { _idMaestroSolicitud = value; }
         }

         [DataMember]
         public string SSCCGenerado
         {
             get { return _SSCCGenerado; }
             set { _SSCCGenerado = value; }
         }

         [DataMember]
         public string Descripcion
         {
             get { return _Descripcion; }
             set { _Descripcion = value; }
         }

         [DataMember]
         public double Cantidad
         {
             get { return _Cantidad; }
             set { _Cantidad = value; }
         }
         [DataMember]
         public string NombreArticulo
         {
             get { return _Nombre; }
             set { _Nombre = value; }
         }
    }
}

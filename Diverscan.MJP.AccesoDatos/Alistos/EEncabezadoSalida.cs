using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Alistos
{
    [Serializable]
    public class EEncabezadoSalida
    {
        string _nombre;
        string _destino;
        string _fechaEntregaAndroid;
        int _idMaestroSolicitud;
        int _prioridad;

        public EEncabezadoSalida()
        {
        }
        public EEncabezadoSalida(string nombre,string destino, string fechaEntregaAndroid,int idMaestroSolicitud)
        {
            _nombre = nombre;
            _destino = destino;
            _fechaEntregaAndroid = fechaEntregaAndroid;
            _idMaestroSolicitud = idMaestroSolicitud;
        }

        public EEncabezadoSalida(IDataReader reader)
        {
            _nombre= reader["Nombre"].ToString();
            _destino= reader["Descripcion"].ToString();
            _fechaEntregaAndroid= reader["FechaEntrega"].ToString();
            _idMaestroSolicitud = Convert.ToInt32(reader["idMaestroSolicitud"].ToString());
        }

        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Destino { get => _destino; set => _destino = value; }
        public string FechaEntregaAndroid { get => _fechaEntregaAndroid; set => _fechaEntregaAndroid = value; }
        public int IdMaestroSolicitud { get => _idMaestroSolicitud; set => _idMaestroSolicitud = value; }
        public int Prioridad { get => _prioridad; set => _prioridad = value; }
    }
}

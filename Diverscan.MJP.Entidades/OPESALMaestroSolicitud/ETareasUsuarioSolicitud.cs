using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.OPESALMaestroSolicitud
{
    [Serializable]
    public class ETareasUsuarioSolicitud
    {
        int _idusuario;
        string _nombreUsuario;
        string _nombreArticulo;
        double _cantidad;
        DateTime _fechaAsignacion;
        int _idTareaUsuario;
        int _idTareaSolicitud;
        int _idLineaDetalle;
        string _idInternoSAP;
        string _nombreDestino;
        string _descripcionPrioridad;

        public ETareasUsuarioSolicitud(int idUsuario, string nombreUsuario, string nombreArticulo, double cantidad, DateTime fechaAsignacion, 
                                       int idTareaUsuario, int idTareaSolicitud, int idLineaDetalle,string idInternoSAP, string nombreDestino, string descripcionPrioridad )
        {
             _idusuario= idUsuario;
             _nombreUsuario= nombreUsuario;
             _nombreArticulo= nombreArticulo;
             _cantidad= cantidad;
             _fechaAsignacion= fechaAsignacion;
             _idTareaUsuario= idTareaUsuario;
             _idTareaSolicitud= idTareaSolicitud;
             _idLineaDetalle= idLineaDetalle;
             _idInternoSAP= idInternoSAP;
             _nombreDestino= nombreDestino;
            _descripcionPrioridad = descripcionPrioridad;
        }

        public ETareasUsuarioSolicitud(IDataReader reader)
        {
            _idusuario = Convert.ToInt32(reader["IDUSUARIO"].ToString());
            _nombreUsuario = reader["NombreUsuario"].ToString();
            _nombreArticulo = reader["NombreArticulo"].ToString();
            _cantidad = Convert.ToDouble(reader["Cantidad"].ToString());
            _fechaAsignacion = Convert.ToDateTime(reader["FechaAsignacion"].ToString());
            _idTareaUsuario = Convert.ToInt32(reader["IdTareasUsuario"].ToString());
          // _idTareaSolicitud = Convert.ToInt32(reader["IdTareasUsuario"].ToString());
            _idLineaDetalle = Convert.ToInt32(reader["IdLineaDetalleSolicitud"].ToString());
            _idInternoSAP = reader["idInterno"].ToString();
            _nombreDestino = reader["Nombre"].ToString();
            _descripcionPrioridad = reader["DescripcionPrioridad"].ToString();
        }

        public int Idusuario { get => _idusuario; set => _idusuario = value; }
        public string NombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
        public string NombreArticulo { get => _nombreArticulo; set => _nombreArticulo = value; }
        public double Cantidad { get => _cantidad; set => _cantidad = value; }
        public DateTime FechaAsignacion { get => _fechaAsignacion; set => _fechaAsignacion = value; }
        public int IdTareaUsuario { get => _idTareaUsuario; set => _idTareaUsuario = value; }
        public int IdTareaSolicitud { get => _idTareaSolicitud; set => _idTareaSolicitud = value; }
        public int IdLineaDetalle { get => _idLineaDetalle; set => _idLineaDetalle = value; }
        public string IdInternoSAP { get => _idInternoSAP; set => _idInternoSAP = value; }
        public string NombreDestino { get => _nombreDestino; set => _nombreDestino = value; }
        public string DescripcionPrioridad { get => _descripcionPrioridad; set => _descripcionPrioridad = value; }
    }
}

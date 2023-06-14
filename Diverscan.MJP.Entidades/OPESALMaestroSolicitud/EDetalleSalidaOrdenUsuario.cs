using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.OPESALMaestroSolicitud
{
    [Serializable]
    public class EDetalleSalidaOrdenUsuario
    {
        int _idLineaDetalleSolicitud;
        int _idMaestroSolicitud;
        double _cantidad;
        string _nombre;
        string _descripcion;
        int _pendiente;
        int _idProducto;
        int _diasMaxVencimiento;

        public EDetalleSalidaOrdenUsuario(IDataReader reader)
        {
            _idLineaDetalleSolicitud = Convert.ToInt32(reader["idLineaDetalleSolicitud"]);
            _idMaestroSolicitud = Convert.ToInt32(reader["idMaestroSolicitud"].ToString());
            Cantidad = Convert.ToDouble(reader["Cantidad"].ToString());
            _nombre = reader["Nombre"].ToString();
            _descripcion = reader["Descripcion"].ToString();
            double pendiente = Convert.ToDouble(reader["Pendiente"].ToString());
            _pendiente = Convert.ToInt32(pendiente);
            _idProducto = Convert.ToInt32(reader["idArticulo"].ToString());

        }

        public EDetalleSalidaOrdenUsuario(int idLineaDetalleSolicitud, int idMaestroSolicitud, double cantidad, string nombre, string descripcion, int pendiente, int idProducto, int diasMaxVencimiento)
        {
            _idLineaDetalleSolicitud = idLineaDetalleSolicitud;
            _idMaestroSolicitud = idMaestroSolicitud;
            _cantidad = cantidad;
            _nombre = nombre;
            _descripcion = descripcion;
            _pendiente = pendiente;
            _idProducto = idProducto;
            _diasMaxVencimiento = diasMaxVencimiento;
        }

        public int IdLineaDetalleSolicitud { get => _idLineaDetalleSolicitud; set => _idLineaDetalleSolicitud = value; }
        public int IdMaestroSolicitud { get => _idMaestroSolicitud; set => _idMaestroSolicitud = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public double Cantidad { get => _cantidad; set => _cantidad = value; }
        public int Pendiente { get => _pendiente; set => _pendiente = value; }
        public int IdProducto { get => _idProducto; set => _idProducto = value; }
        public int DiasMaxVencimiento { get => _diasMaxVencimiento; set => _diasMaxVencimiento = value; }
    }
}

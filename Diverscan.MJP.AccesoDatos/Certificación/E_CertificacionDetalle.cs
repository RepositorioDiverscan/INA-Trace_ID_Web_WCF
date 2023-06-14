using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Certificación
{
    [Serializable]
    public class E_CertificacionDetalle
    {
        string _nombreSector;
        string _nombreArticulo;
        int _idArticulo;
        decimal _cantidad;
        int _estadoDetalle;
        string _detalleAlistado;
        int _idSector;
        int _idLineaDetalleSolicitud;
        string _nombreUsuario;
        int _cantidadAlistada;
        int _cantidadDisponible;
        string _idInternoArticulo;

        public E_CertificacionDetalle(int idLineaDetalleSolicitud, string nombreSector, string nombreArticulo, int idArticulo, decimal cantidad, int estadoDetalle,
            int idSector, string NombreUsuario, int CantidadAlistada, int CantidadDisponible, string idInternoArticulo)
        {            
            _nombreSector = nombreSector;
            _nombreArticulo = nombreArticulo;
            _idArticulo = idArticulo;
            _cantidad = cantidad;
            _estadoDetalle = estadoDetalle;
            _idSector = idSector;
            _idLineaDetalleSolicitud = idLineaDetalleSolicitud;
            _nombreUsuario = NombreUsuario;
            _cantidadAlistada = CantidadAlistada;
            _cantidadDisponible = CantidadDisponible;
            _idInternoArticulo = idInternoArticulo;
        }

        public E_CertificacionDetalle(IDataReader reader)
        {
            _nombreSector = reader["NombreSector"].ToString();
            _nombreArticulo = reader["Nombre"].ToString();
            _idArticulo = Convert.ToInt32(reader["idArticulo"].ToString());
            _cantidad = Convert.ToDecimal(reader["Cantidad"].ToString());
            _estadoDetalle = Convert.ToInt32(reader["EstadoDetalle"].ToString());
            _detalleAlistado = ConvertirIdEstado(_estadoDetalle);
            _idSector = Convert.ToInt32(reader["IdSector"].ToString());
            _idLineaDetalleSolicitud = Convert.ToInt32(reader["idLineaDetalleSolicitud"].ToString());
            _nombreUsuario = reader["NombreUsuario"].ToString();
            _cantidadAlistada = Convert.ToInt32(reader["CantidadAlistada"].ToString());
            _cantidadDisponible = Convert.ToInt32(reader["CantidadDisponible"].ToString());
            _idInternoArticulo = reader["idInterno"].ToString();
        }

        public string NombreSector { get => _nombreSector; set => _nombreSector = value; }
        public string NombreArticulo { get => _nombreArticulo; set => _nombreArticulo = value; }
        public int IdArticulo { get => _idArticulo; set => _idArticulo = value; }
        public decimal Cantidad { get => _cantidad; set => _cantidad = value; }
        public int EstadoDetalle { get => _estadoDetalle; set => _estadoDetalle = value; }
        public int IdSector { get => _idSector; set => _idSector = value; }
        public int IdLineaDetalleSolicitud { get => _idLineaDetalleSolicitud; set => _idLineaDetalleSolicitud = value; }
        public string DetalleAlistado { get => _detalleAlistado; set => _detalleAlistado = value; }
        public string NombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
        public int CantidadAlistada { get => _cantidadAlistada; set => _cantidadAlistada = value; }
        public int CantidadDisponible { get => _cantidadDisponible; set => _cantidadDisponible = value; }

        public string IdInternoArticulo { get => _idInternoArticulo; set => _idInternoArticulo = value; }

        private string ConvertirIdEstado(int DetalleId)
        {
            string Resultado = "";
            if (DetalleId == 0)
            {
                Resultado = "Sin asignar";
            }
            else if (DetalleId == 1)
            {
                Resultado = "Pendiente";
            }
            else if (DetalleId == 2)
            {
                Resultado = "Finalizada";
            }

            return Resultado;
        }
    }
}

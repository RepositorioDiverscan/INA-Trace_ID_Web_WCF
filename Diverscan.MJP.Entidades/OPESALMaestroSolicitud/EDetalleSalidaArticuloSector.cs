using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.OPESALMaestroSolicitud
{
    [Serializable]
    public class EDetalleSalidaArticuloSector
    {
        string _nombreSector;
        string _nombreArticulo;
        int _idArticulo;
        double _cantidad;
        int _estadoDetalle;
        string _detalleAlistado;
        int _idSector;
        int _idLineaDetalleSolicitud;
        string _nombreUsuario;

        public EDetalleSalidaArticuloSector(int idLineaDetalleSolicitud, string nombreSector, string nombreArticulo, int idArticulo, double cantidad, int estadoDetalle, int idSector, string NombreUsuario)
        {
            _nombreSector = nombreSector;
            _nombreArticulo = nombreArticulo;
            _idArticulo = idArticulo;
            _cantidad = cantidad;
            _estadoDetalle = estadoDetalle;
            _idSector = idSector;
            _idLineaDetalleSolicitud = idLineaDetalleSolicitud;
            _nombreUsuario = NombreUsuario;


        }

        public EDetalleSalidaArticuloSector(IDataReader reader)
        {
            _nombreSector = reader["NombreSector"].ToString();
            _nombreArticulo = reader["Nombre"].ToString();
            _idArticulo = Convert.ToInt32(reader["idArticulo"].ToString());
            _cantidad = Convert.ToDouble(reader["Cantidad"].ToString());
            _estadoDetalle = Convert.ToInt32(reader["EstadoDetalle"].ToString());
            _detalleAlistado = ConvertirIdEstado(_estadoDetalle);
            _idSector = Convert.ToInt32(reader["IdSector"].ToString());
            _idLineaDetalleSolicitud = Convert.ToInt32(reader["idLineaDetalleSolicitud"].ToString());
            _nombreUsuario = reader["NombreUsuario"].ToString();
        }

        public string NombreSector { get => _nombreSector; set => _nombreSector = value; }
        public string NombreArticulo { get => _nombreArticulo; set => _nombreArticulo = value; }
        public int IdArticulo { get => _idArticulo; set => _idArticulo = value; }
        public double Cantidad { get => _cantidad; set => _cantidad = value; }
        public int EstadoDetalle { get => _estadoDetalle; set => _estadoDetalle = value; }
        public int IdSector { get => _idSector; set => _idSector = value; }
        public int IdLineaDetalleSolicitud { get => _idLineaDetalleSolicitud; set => _idLineaDetalleSolicitud = value; }
        public string DetalleAlistado { get => _detalleAlistado; set => _detalleAlistado = value; }
        public string NombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
   

        private string ConvertirIdEstado(int DetalleId)
        {
            string Resultado = "";
            if(DetalleId==0)
            {
              Resultado = "Sin asignar";    
            } else if (DetalleId==1)
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

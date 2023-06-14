using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes
{
    public class e_ReporteDespachadoVsRecibido
    {
        private long _IdPedidoOriginal;
        private string _FechaEntrega;
        private int _CantidadDespachada;
        private int _CantidadRecibida;
        private string _IdInterno;
        private string _NombreArticulo;
        private string _ProfesorSolicita;

        public e_ReporteDespachadoVsRecibido(IDataReader reader)
        {
            _IdPedidoOriginal = Convert.ToInt64(reader["idPedidoOriginal"].ToString());
            _FechaEntrega = reader["FechaEntrega"].ToString();
            _CantidadDespachada = Convert.ToInt32(reader["CantidadDespachada"].ToString());
            _CantidadRecibida = Convert.ToInt32(reader["cantidadRecibida"].ToString());
            _IdInterno = reader["idInterno"].ToString();
            _NombreArticulo = reader["Nombre"].ToString();
            _ProfesorSolicita = reader["ProfesorSolicita"].ToString();
        }

        public long IdPedidoOriginal { get => _IdPedidoOriginal; set => _IdPedidoOriginal = value; }
        public string FechaEntrega { get => _FechaEntrega; set => _FechaEntrega = value; }
        public int CantidadDespachada { get => _CantidadDespachada; set => _CantidadDespachada = value; }
        public int CantidadRecibida { get => _CantidadRecibida; set => _CantidadRecibida = value; }
        public string IdInterno { get => _IdInterno; set => _IdInterno = value; }
        public string NombreArticulo { get => _NombreArticulo; set => _NombreArticulo = value; }
        public string ProfesorSolicita { get => _ProfesorSolicita; set => _ProfesorSolicita = value; }
    }
}

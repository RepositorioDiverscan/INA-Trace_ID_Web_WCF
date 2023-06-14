using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.ReportePedidoSinOla
{
    [Serializable]
    public class EListPedidoSinOla
    {
        string _idInterno;
        string _Nombre,
            _Comentarios,
            _Ruta,
            _Direccion;
        DateTime _FechaCreacion;

        public EListPedidoSinOla(string idInterno, string nombre, string comentarios, string ruta, string direccion, DateTime fechaCreacion)
        {
            _idInterno = idInterno;
            _Nombre = nombre;
            _Comentarios = comentarios;
            _Ruta = ruta;
            _Direccion = direccion;
            _FechaCreacion = fechaCreacion;
        }

        public string IdInterno { get => _idInterno; set => _idInterno = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Comentarios { get => _Comentarios; set => _Comentarios = value; }
        public string Ruta { get => _Ruta; set => _Ruta = value; }
        public string Direccion { get => _Direccion; set => _Direccion = value; }
        public DateTime FechaCreacion { get => _FechaCreacion; set => _FechaCreacion = value; }
    }
}

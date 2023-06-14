using Diverscan.MJP.Entidades.OPESALMaestroSolicitud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Operacion.DespachoPedidos.Entidades
{
    [Serializable]
    public class EMaestroSolicitudFacturado
    {
        private long _idRegistroOla;
        private long _idMaestroSolicitud;
        private string _numeroFactura;
        private DateTime _fechaCreacion;
        private string _nombre;
        private string _comentarios;
        private string _idInterno;
        private string _idInternoCliente;
        private string _nombreCliente;

        public EMaestroSolicitudFacturado()
        {
        }

        public EMaestroSolicitudFacturado(long idRegistroOla, long idMaestroSolicitud, string numeroFactura,
            DateTime fechaCreacion,  string nombre, string comentarios, string idInternoMaestro, 
            string idInternoCliente, string nombreCliente)            
        {                       
            _idRegistroOla = idRegistroOla;
            _idMaestroSolicitud = idMaestroSolicitud;
            _numeroFactura = numeroFactura;
            _fechaCreacion = fechaCreacion;
            _nombre = nombre;
            _comentarios = comentarios;
            _idInterno = idInternoMaestro;
            _idInternoCliente = idInternoCliente;
            _nombreCliente = nombreCliente;                                                         
        }

        public long IdRegistroOla { get => _idRegistroOla; set => _idRegistroOla = value; }
        public long IdMaestroSolicitud { get => _idMaestroSolicitud; set => _idMaestroSolicitud = value; }
        public DateTime FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Comentarios { get => _comentarios; set => _comentarios = value; }
        public string IdInterno { get => _idInterno; set => _idInterno = value; }
        public string IdInternoCliente { get => _idInternoCliente; set => _idInternoCliente = value; }
        public string NombreCliente { get => _nombreCliente; set => _nombreCliente = value; }
        public string NumeroFactura { get => _numeroFactura; set => _numeroFactura = value; }
    }
}

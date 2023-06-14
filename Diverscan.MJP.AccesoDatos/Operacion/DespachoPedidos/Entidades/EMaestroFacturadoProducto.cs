using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Operacion.DespachoPedidos.Entidades
{
    [Serializable]
    public class EMaestroFacturadoProducto : EMaestroSolicitudFacturado
    {
        private decimal _cantidad;

        public EMaestroFacturadoProducto()
        {
        }

        public EMaestroFacturadoProducto(string numeroFactura, string nombre, string idInternoMaestro,
            string idInternoCliente, string nombreCliente, decimal cantidad)
        {
            _cantidad = cantidad;
            NumeroFactura = numeroFactura;
            Nombre = nombre;
            IdInterno = idInternoMaestro;
            IdInternoCliente = idInternoMaestro;
            NombreCliente = nombreCliente;
        }

        public decimal Cantidad { get => _cantidad; set => _cantidad = value; }
    }
}

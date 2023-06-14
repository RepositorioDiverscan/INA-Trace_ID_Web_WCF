using Diverscan.MJP.AccesoDatos.Operacion.DespachoPedidos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Operacion.DespachoPedidos
{
    public interface IDespachoDePedidos
    {
        List<E_ListadoOlasFactura> ObtenerListadoOlas(int idBodega, DateTime FechaInicio, DateTime FechaFinal, string Busqueda, int facturado);

        List<E_ListadoDetalleOla> ObtenerDetalleOla(int idOla);

        void FacturarOla(int Ola, long idTransportista);

        List<EMaestroSolicitudFacturado> ObtenerPreMaestrosFacturados(int idBodega, DateTime FechaInicio,
           DateTime FechaFinal);

        List<EMaestroFacturadoProducto> ObtenerPreMaestrosXArticulo(long idBodega, string lote,
           DateTime fechaExp, long idArticulo);
        List<EMaestroSolicitudFacturado> ObtenerPreMaestrosFacturadosXOla(int idWarehouse, long idOla);
    }
}

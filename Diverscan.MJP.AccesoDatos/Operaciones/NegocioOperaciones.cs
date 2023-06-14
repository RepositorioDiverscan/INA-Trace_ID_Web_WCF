using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Operaciones
{
    public class NegocioOperaciones
    {

        public List<eOrdenCompra> ObtenerOrdenComprasBodega(DateTime? fechaInicio, DateTime? fechaFin, string ordenCompra, int idBodega)
        {
            OrdenesCompras ordenesCompras = new OrdenesCompras();
            return ordenesCompras.OrdenComprasBodega(fechaInicio, fechaFin, ordenCompra, idBodega);

        }

        public List<eSinOrdenCompra> ObtenerSinOrdenCompraBodega(DateTime? fechaInicio, DateTime? fechaFin, string ordenCompra, int idBodega)
        {
            SinOrdenCompra sinOrdenesCompras = new SinOrdenCompra();
            return sinOrdenesCompras.SinOrdenComprasBodega(fechaInicio, fechaFin, ordenCompra, idBodega);

        }

        public List<eIngresoCajaChica> ObtenerCCBodega(DateTime? fechaInicio, DateTime? fechaFin, string ordenCompra, int idBodega)
        {
            IngresosCajaChica ingresoCC = new IngresosCajaChica();
            return ingresoCC.ingresoXCajaChica(fechaInicio, fechaFin, ordenCompra, idBodega);

        }

        public List<EDetalleOrdenC> ObtenerDetalleOrdenCompras(int id, int idBodega)
        {
            OrdenesCompras ordenesCompras = new OrdenesCompras();

            return ordenesCompras.ObtenerDetalleOrdenCompras(id, idBodega);
        }

        public List<EDetalleOrdenC> ObtenerDetalleSinOrdenCompra(int id, int idBodega)
        {
            SinOrdenCompra SinOrdenesCompra = new SinOrdenCompra();

            return SinOrdenesCompra.ObtenerDetalleSinOrdenCompras(id, idBodega);
        }

        public List<EDetalleOrdenC> ObtenerDetalleCC(int id, int idBodega)
        {
            IngresosCajaChica ingresoCC = new IngresosCajaChica();

            return ingresoCC.ObtenerDetalleCC(id, idBodega);
        }

        public string ProcesarFactura(int id, DateTime FechaProc)
        {
            OrdenesCompras ordenesCompras = new OrdenesCompras();
            return ordenesCompras.ProcesarFactura(id, FechaProc);
        }

        public List<eDetalleFacturado> ObtenerDetalleOrdenComprasXMaestro(string IdSap)
        {
            DetalleFacturado detalleFacturado = new DetalleFacturado();
            return detalleFacturado.ObtenerDetalleFacturado(IdSap);
        }

    }
}

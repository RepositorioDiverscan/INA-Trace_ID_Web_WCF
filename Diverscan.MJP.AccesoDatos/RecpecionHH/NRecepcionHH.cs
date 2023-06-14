using Diverscan.MJP.AccesoDatos.RecpecionHH.DetalleRecepcion;
using Diverscan.MJP.AccesoDatos.RecpecionHH.DevolucionProducto;
using Diverscan.MJP.AccesoDatos.RecpecionHH.FinalizarProducto;
using Diverscan.MJP.AccesoDatos.RecpecionHH.ProductoRecibido;
using Diverscan.MJP.AccesoDatos.RecpecionHH.RechazoProducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.RecpecionHH
{
    public class NRecepcionHH
    {
        RecepcionHH_DBA _recepcionHH_DBA;

        public NRecepcionHH()
        {
            _recepcionHH_DBA = new RecepcionHH_DBA();
        }

        public ResultadoInsertarProductoRecibido InsertarArticuloRecibidoOC(EProductoRecibido productoRecibido)
        {
            ResultadoInsertarProductoRecibido resultadoInsertarProductoRecibido = new ResultadoInsertarProductoRecibido();

            try
            {
                var insertar = _recepcionHH_DBA.InsertarArticuloRecibidoOC(productoRecibido);
                resultadoInsertarProductoRecibido.Estado = 1;
                resultadoInsertarProductoRecibido.Mensaje = insertar;
            }
            catch(Exception)
            {
                resultadoInsertarProductoRecibido.Estado = 0;
                resultadoInsertarProductoRecibido.Mensaje = "Lo sentimos ha ocurrido un error, intente de nuevo o contacte al administrador";
            }

            return resultadoInsertarProductoRecibido;
        }

        public ResultadoDevolucionProducto ActualizarCantidadProductoRecibidoOC(EDevolcionProducto devolcionProducto)
        {
            ResultadoDevolucionProducto resultadoDevolucionProducto = new ResultadoDevolucionProducto();

            try
            {
                var devolucion = _recepcionHH_DBA.ActualizarCantidadProductoRecibidoOC(devolcionProducto);
                resultadoDevolucionProducto.Estado = 1;
                resultadoDevolucionProducto.Mensaje = devolucion;
            }
            catch(Exception ex)
            {
                resultadoDevolucionProducto.Estado = 0;
                resultadoDevolucionProducto.Mensaje = ex.Message;
            }

            return resultadoDevolucionProducto;
        }

        public ResultadoDetalleRecepcion ObtenerDetalleRecepcionOC(long IdDetalleOrdenCompra)
        {
            ResultadoDetalleRecepcion resultadoDetalleRecepcion = new ResultadoDetalleRecepcion();

            try
            {
                var lista = _recepcionHH_DBA.ObtenerDetalleRecepcionOC(IdDetalleOrdenCompra);
                resultadoDetalleRecepcion.Estado = 1;
                resultadoDetalleRecepcion.Mensaje = "Exitoso";
                resultadoDetalleRecepcion.listDetalleRecepcion = lista;
            }
            catch (Exception)
            {
                resultadoDetalleRecepcion.Estado = 0;
                resultadoDetalleRecepcion.Mensaje = "Lo sentimos ha ocurrido un error, intente de nuevo o contacte al administrador";
            }

            return resultadoDetalleRecepcion;
        }

        public ResultadoRechazoProducto InsertarArticuloRechazadoOC(ERechazoProducto rechazoProducto)
        {
            ResultadoRechazoProducto resultadoRechazoProducto = new ResultadoRechazoProducto();

            try
            {
                var insertResultado = _recepcionHH_DBA.InsertarArticuloRechazadoOC(rechazoProducto);
                resultadoRechazoProducto.Mensaje = insertResultado;
                resultadoRechazoProducto.Estado = 1;
            }
            catch (Exception)
            {
                resultadoRechazoProducto.Estado = 0;
                resultadoRechazoProducto.Mensaje = "Lo sentimos ha ocurrido un error, intente de nuevo o contacte al administrador";
            }

            return resultadoRechazoProducto;
        }

        public ResultadoFinalizarRecepcionProducto InsertarRecepcionProductoFinalizada(EFinalizarRecepcionProducto finalizar)
        {
            ResultadoFinalizarRecepcionProducto resultado = new ResultadoFinalizarRecepcionProducto();

            try
            {
                var result = _recepcionHH_DBA.InsertarRecepcionProductoFinalizada(finalizar);
                resultado.Mensaje = result;
                resultado.Estado = 1;
            }
            catch(Exception ex)
            {
                resultado.Estado = 0;
                //resultado.Mensaje = "Lo sentimos ha ocurrido un error, intente de nuevo o contacte al administrador";
                resultado.Mensaje = ex.ToString();
            }

            return resultado;
        }

    }
}

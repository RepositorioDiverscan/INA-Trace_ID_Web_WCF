using System;
using Newtonsoft.Json;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.AccesoDatos.DepuracionPedido;

namespace Diverscan.MJP.UI.Operaciones.Pedidos
{
    public partial class DepurarPedidoAjaz : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DB_DepuracionPedido dB_DepuracionPedido = new DB_DepuracionPedido();

            //Obtener la informacion del Usuario
            e_Usuario _eUsuario = (e_Usuario)Session["USUARIO"];
            int IdUsuario = Convert.ToInt32(_eUsuario.IdUsuario);

            var opcion = Request.Form["Opcion"];

            switch (opcion)
            {
                case "ConsultaPedidoOriginalEncabezado":
                    var pedidosOriginales = dB_DepuracionPedido.ObtenerPedidoOriginalEncabezado(IdUsuario);
                    Response.Write(JsonConvert.SerializeObject(pedidosOriginales, Formatting.Indented));
                    break;


                case "ObtenerPedidoOriginalDetalle":
                    var idPedidoOriginal = Convert.ToInt64(Request.Form["IdPedidoOriginal"]);
                    var detallesOriginal = dB_DepuracionPedido.ObtenerPedidoOriginalDetalle(idPedidoOriginal);
                    Response.Write(JsonConvert.SerializeObject(detallesOriginal, Formatting.Indented));
                    break;


                case "ModificarCantidad":
                    var idPedidoOriginalMod = Convert.ToInt32(Request.Form["IdPedidoOriginal"]);
                    var idArticulo = Convert.ToInt32(Request.Form["IdArticulo"]);
                    var cantidadMod = Convert.ToInt32(Request.Form["CantidadModificar"]);
                    var accion = Convert.ToString(Request.Form["Accion"]);

                    var resultadoModificarCantidad = dB_DepuracionPedido.ModificarCantidad(idPedidoOriginalMod, idArticulo, cantidadMod, accion);
                    Response.Write(resultadoModificarCantidad);
                    break;
            }

        }
    }
}
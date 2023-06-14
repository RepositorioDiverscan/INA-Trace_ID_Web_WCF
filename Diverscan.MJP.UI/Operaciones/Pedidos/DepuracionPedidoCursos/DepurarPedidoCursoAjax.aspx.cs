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

            if (_eUsuario == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }

            int IdUsuario = Convert.ToInt32(_eUsuario.IdUsuario);
            int IdBodega = Convert.ToInt32(_eUsuario.IdBodega);

            var opcion = Request.Form["Opcion"];

            switch (opcion)
            {
                case "ConsultaPedidoOriginalEncabezado":
                    var idUsuario = Convert.ToInt32(IdUsuario);
                    var listaEncabezados = dB_DepuracionPedido.ObtenerPedidosCursosEnDepuracionEncabezados(IdBodega, idUsuario);
                    Response.Write(JsonConvert.SerializeObject(listaEncabezados, Formatting.Indented));
                    break;


                case "ConsultaPedidoOriginalDetalle":
                    var idPedidoDetalle = Convert.ToInt32(Request.Form["IdPedido"]);
                    var listaDetalle = dB_DepuracionPedido.ObtenerPedidosCursosEnDepuracionDetalle(idPedidoDetalle);
                    Response.Write(JsonConvert.SerializeObject(listaDetalle, Formatting.Indented));
                    break;


                case "ModificarCantidadDetalle":
                    var idPedidoOriginal = Convert.ToInt32(Request.Form["IdPedido"]);
                    var idArticulo = Convert.ToInt32(Request.Form["IdArticulo"]);
                    var cantidadModificar = Convert.ToInt32(Request.Form["CantidadModificar"]);
                    var accion = Convert.ToString(Request.Form["Accion"]);

                    var respuesta = dB_DepuracionPedido.ModificarCantidadPedido(idPedidoOriginal, idArticulo, cantidadModificar, accion);
                    Response.Write(respuesta);
                    break;
            }

        }
    }
}
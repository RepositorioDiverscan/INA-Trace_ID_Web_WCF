using Diverscan.MJP.AccesoDatos.Pedidos.Admin;
using Diverscan.MJP.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Operaciones.Pedidos.PedidosAdministrativos
{
    public partial class pedidoAdminAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Instanciar la BD
            DBPedidosAdmin dBPedidosAdmin = new DBPedidosAdmin();

            //Obtener la informacion del Usuario
            e_Usuario _eUsuario = (e_Usuario)Session["USUARIO"];

            if (_eUsuario == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }

            int IdUsuario = Convert.ToInt32(_eUsuario.IdUsuario);
            var IdBodega = _eUsuario.IdBodega;

            var opcion = Request.Form["Opcion"];

            switch (opcion)
            {
                //Caso para Obtener los Encabezados de los Pedidos
                case "ObtenerEncabezadosPedidosAdministrativo":
                    var ListaPedidosCursosEncabezado = dBPedidosAdmin.ObtenerPedidosAdminEncabezados(IdBodega);
                    Response.Write(JsonConvert.SerializeObject(ListaPedidosCursosEncabezado, Formatting.Indented));
                    break;


                //Caso para Obtener los Detalles de los Pedidos
                case "ObtenerDetallePedidos":
                    var IdPedidoOriginal = Convert.ToInt32(Request.Form["IdPedidoOriginal"]);
                    var ListaPedidosCursosDetalle = dBPedidosAdmin.ObtenerPedidosCursosDetalle(IdPedidoOriginal);
                    Response.Write(JsonConvert.SerializeObject(ListaPedidosCursosDetalle, Formatting.Indented));
                    break;


                //Caso para Anular Pedidos Cursos
                case "AnularPedidoCurso":
                    var IdPedidoOriginalAnular = Convert.ToInt32(Request.Form["IdPedidoOriginal"]);
                    var respuestaAnular = dBPedidosAdmin.anularPedidoCurso(IdPedidoOriginalAnular);
                    Response.Write(respuestaAnular);
                    break;


                //Caso para Aceptar los Pedidos de un Curso 
                case "AceptarPedidoAdmin":
                    var IdPedidoOriginalAceptar = Convert.ToInt32(Request.Form["IdPedidoOriginal"]);
                    var respuestaAceptar = dBPedidosAdmin.AprobarPedidoAdmin(IdPedidoOriginalAceptar);
                    Response.Write(respuestaAceptar);
                    break;
            }
        }
    }
}
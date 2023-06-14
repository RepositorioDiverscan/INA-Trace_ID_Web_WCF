using Diverscan.MJP.AccesoDatos.Pedidos;
using Diverscan.MJP.Entidades;
using Newtonsoft.Json;
using System;

namespace Diverscan.MJP.UI.Operaciones.Pedidos.PedidoCursos
{
    public partial class pedidoCursosAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Instanciar la BD
            DBPedidosCursos dBPedidosCursos = new DBPedidosCursos();

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
                case "ObtenerEncabezadosPedidos":
                    var ListaPedidosCursosEncabezado = dBPedidosCursos.ObtenerPedidosCursosEncabezados(IdBodega);
                    Response.Write(JsonConvert.SerializeObject(ListaPedidosCursosEncabezado, Formatting.Indented));
                    break;


                //Caso para Obtener los Detalles de los Pedidos
                case "ObtenerDetallePedidos":
                    var IdPedidoOriginal = Convert.ToInt32(Request.Form["IdPedidoOriginal"]);
                    var ListaPedidosCursosDetalle = dBPedidosCursos.ObtenerPedidosCursosDetalle(IdPedidoOriginal);
                    Response.Write(JsonConvert.SerializeObject(ListaPedidosCursosDetalle, Formatting.Indented));
                    break;


                //Caso para Anular Pedidos Cursos
                case "AnularPedidoCurso":
                    var IdPedidoOriginalAnular = Convert.ToInt32(Request.Form["IdPedidoOriginal"]);
                    var respuestaAnular = dBPedidosCursos.anularPedidoCurso(IdPedidoOriginalAnular);
                    Response.Write(respuestaAnular);
                    break;


                //Caso para Enviar al Profesor a Depurar un Pedido Curso
                case "EnviarDepuracionPedidoCurso":
                    var IdPedidoOriginalDepuracion = Convert.ToInt32(Request.Form["IdPedidoOriginal"]);
                    var respuestaDepuracion = dBPedidosCursos.EnviarDepuracionPedidoCurso(IdPedidoOriginalDepuracion);
                    Response.Write(respuestaDepuracion);
                    break;


                //Caso para Aceptar los Pedidos de un Curso 
                case "AceptarPedidoCurso":
                    var IdPedidoOriginalAceptar = Convert.ToInt32(Request.Form["IdPedidoOriginal"]);
                    var respuestaAceptar = dBPedidosCursos.AprobarPedidoCurso(IdPedidoOriginalAceptar);
                    Response.Write(respuestaAceptar);
                    break;
            }
        }
    }
}
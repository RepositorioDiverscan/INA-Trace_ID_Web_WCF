using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.MaestroArticulo;
using System;
using System.Web.Services;

namespace Diverscan.MJP.UI
{
    public partial class Notificaciones : System.Web.UI.Page
    {
        static n_MaestroArticulo n_MaestroArticulo = new n_MaestroArticulo();
        static int cantidadNotificacionesAjax = 1;
        int cantidadNotificacionesOnLoad = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            var _eUsuario = (e_Usuario)Session["USUARIO"];

            if (_eUsuario == null)
            {
                Response.Redirect("~/Administracion/wf_Credenciales.aspx");
            }
        }

        [WebMethod] //Método para ejecucíón mendiante ajax
        public static string GetNotifications()
        {          
            return concatenarNotificaciones();
        }
        
        //Utilizado para mostrar las notificaciones dentro del sistema 
        public static string concatenarNotificaciones()
        {
            //Se agregan las notificaciones al vector para enviarlas al método que busca notificaciones desde ajax en el site.masterpage 
            //Divisor de notificaciones | para hacer el split en JAVASCRIPT

            /*
             *                      [NOTAS PARA AGREGAR NOTIFICACIONES]
             * 1.Cree su método en la capa negocio que ejecute y retorne el mensaje que desea 
             * 2.En caso que el método no deba  retornar una notificación el mismo deberá retornar el string: false
             * 3.Para agregar otras notificaciones simplemente agrege el número de índices al vector: notificacionesObtenidas y asigne 
             *   en la posición u orden que desea se visualize las notificaciones que usted requiera 
             */
            string[] notificacionesObtenidas = new string[cantidadNotificacionesAjax];
            notificacionesObtenidas[0] = n_MaestroArticulo.GetNotificacionArticulosUnidadAlisto();

            string notificacionesConcatenadas = ""; //Utilizada para retornar el string divido con las notificaciones que existan
            for (int i = 0; i < notificacionesObtenidas.Length; i++)
            {
                if (i == (notificacionesObtenidas.Length - 1)) //Se evalua para evitar poner el separador al último valor 
                {
                    notificacionesConcatenadas += notificacionesObtenidas[i]; //En caso que sea el último valor o solo exista 1 solo valor en el vector
                }
                else
                {
                    notificacionesConcatenadas += notificacionesObtenidas[i] + "|";
                }                
            }
            return notificacionesConcatenadas;
        }


        //Utilizado para luego de loggearse mostrar las notificaciones iniciales
        public string [] GetNotificacionesOnLoad()
        {                        
            /*
             *                      [NOTAS PARA AGREGAR NOTIFICACIONES]
             * 1.Cree su método en la capa negocio que ejecute y retorne el mensaje que desea 
             * 2.En caso que el método no deba  retornar una notificación el mismo deberá retornar el string: false
             * 3.Para agregar otras notificaciones simplemente agrege el número de índices al vector: notificacionesObtenidas y asigne 
             *   en la posición u orden que desea se visualize las notificaciones que usted requiera 
             */
            string[] notificacionesObtenidas = new string[cantidadNotificacionesOnLoad];
            notificacionesObtenidas[0] = n_MaestroArticulo.GetNotificacionArticulosUnidadAlisto();            
            return notificacionesObtenidas;
        }
    }
}
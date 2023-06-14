using Diverscan.MJP.Entidades.Reportes.OlasFinalizadas;
using Diverscan.MJP.Negocio.Reportes.OlasFinalizadas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Reportes.OlaDisponibleFacturacion
{
    public partial class OlaFinalizadaAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Recibe la opcion para ejecutar la sentencia
            var opcion = Request.Form["opcion"];

            //case para validar la acción segun la opcion
            switch (opcion)
            {
                case "ObtenerOlas":
                    //Invocxar la capa de Negocio
                    NOlaFinalizada ola = new NOlaFinalizada();
                    var f1 = Convert.ToDateTime(Request.Form["F1"]);
                    var f2 = Convert.ToDateTime(Request.Form["F2"]);

                    //Almacenar en una lista

                    
                     //Enviar el JSON con la informacion de la BD
                     Response.Write(JsonConvert.SerializeObject(ola.ConsultaOla(f1, f2),Formatting.Indented));
                    break;

            }
        }
    }
}
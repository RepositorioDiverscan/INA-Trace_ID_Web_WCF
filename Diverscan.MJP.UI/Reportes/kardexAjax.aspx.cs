using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades.Reportes.Kardex;
using Diverscan.MJP.Negocio.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diverscan.MJP.UI.Reportes
{
    public partial class kardexAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //recibe la opcion para ejecutar la setencia
            var opcion = Request.Form["opcion"];
            try
            {
                //case para validar la accion segun la opcion 
                switch (opcion)
                {
                    case "ObtenerKardex":

                        if (da_kardexSKU.ValidaIdInterno(Convert.ToString(Request.Form["sku"])))
                        {
                            //invocamos a la clase de Negocio
                            n_kardexSKU nKardex = new n_kardexSKU();
                            int idBodega = Convert.ToInt32(Request.Form["idBodega"]);
                            string sku = Convert.ToString(Request.Form["sku"]);
                            string lote = Convert.ToString(Request.Form["lote"]);
                            bool transitos = Convert.ToBoolean(Request.Form["transito"]);
                            DateTime f1 = Convert.ToDateTime(Request.Form["f1"]);
                            DateTime f2 = Convert.ToDateTime(Request.Form["f2"]);

                            //alamcenamos en una lista
                            List<e_kardexSKU> listaKardex = nKardex.ObtenerKardex(idBodega, sku, lote, transitos, f1, f2);

                            //convertir la informacion en un json
                            var jsonStringKardex = new JavaScriptSerializer();
                            jsonStringKardex.MaxJsonLength = 2147483644;
                            var jsonStringResultDevolucion = jsonStringKardex.Serialize(listaKardex);

                            //Enviar el JSON con la informacion de la BD 
                            Response.Write(jsonStringResultDevolucion);
                        }
                        else
                        {
                            Response.Write("El Id Interno no existe");
                        }

                        break;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
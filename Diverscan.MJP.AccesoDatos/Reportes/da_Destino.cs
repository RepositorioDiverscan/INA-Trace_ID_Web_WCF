using Diverscan.MJP.Entidades.Reportes.Despachos;
using Diverscan.MJP.Entidades.Reportes.Kardex;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    public class da_Destino
    {
        SqlDataAdapter sqlAdapter = null;
        DataSet dsConsulta = new DataSet();


        public string stGetConexion()
        {
            return ConfigurationManager.ConnectionStrings["MJPConnectionString"].ToString();
        }
        public DataSet dsConsultarProducto(int inCodigo)
        {
            try
            {
              
                string stSentencia = "EXEC SP_BUSCADESTINO_DEVOLUCIONES";

                sqlAdapter = new SqlDataAdapter(stSentencia, stGetConexion());
                sqlAdapter.Fill(dsConsulta);

                return dsConsulta;
            }
            catch (Exception)

            {
                throw;

            }
        }

       
        public void cargarControl(ref DropDownList ddlControl,DataSet  dzConsulta)
        {
            try
            {
                ddlControl.DataSource = dzConsulta;
                ddlControl.DataTextField = "Nombre";
               // ddlControl.DataValueField = "idDestino";
                ddlControl.DataBind();

                ddlControl.Items.Insert(0, new ListItem("--Seleccionar--", "-1"));
            }
            catch (Exception) {
                throw;

            }
        }


    }
}

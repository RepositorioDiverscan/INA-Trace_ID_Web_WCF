using System;
using System.Configuration;
using System.Web;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Utilidades;

namespace Diverscan.MJP.UI
{
    public partial class wf_Control : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var  _eUsuario = (e_Usuario)Session["USUARIO"];

            if (_eUsuario == null)
            {
                Response.Redirect("Administracion/wf_Credenciales.aspx");
            }
           

            ControlInstitucion();

            if(!IsPostBack)
            {
                Ver();
            }
        }

        private void ControlInstitucion()
        {
            try
            {
                var ruta = ConfigurationSettings.AppSettings["Institucion"];

                if (!string.IsNullOrEmpty(ruta))
                {
                    
                }
            }
            catch (Exception ex)
            {
                clErrores cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
            }
        }

        private void Ver()
        {

            var dato = Request.QueryString["EsVer"];

            if (!IsPostBack)
            {
                Cargar_ddlProgramas();
            }

            if (dato == "ver")
            {
                abrirModal();
                return;
            }

            if(dato == "pass")
            {
                abrirModalPass();
                return;
            }

            pnPermisos.Visible = true;
        }

        #region "Seleccion de Programa"

        private void abrirModal()
        {
            modalPass.Show();
            pnModal.Visible = true;
            cambioClave.Visible = false;
            cambioPrograma.Visible = true;
        }

        protected void Cargar_ddlProgramas()
        {
            var _eUsuario = (e_Usuario)Session["USUARIO"];

            ddlListaProgramas.DataTextField = "Descripcion";
            ddlListaProgramas.DataValueField = "IdPrograma";
            ddlListaProgramas.DataBind();
        }

        protected void btnCerrarModal_OnClick(object sender, EventArgs e)
        {
            var _eUsuario = (e_Usuario)Session["USUARIO"];

         
            HttpContext.Current.Session["USUARIO"] = _eUsuario;
            Response.Redirect("Default.aspx");
        }

        #endregion

        #region "Change Pass"

        private void abrirModalPass()
        {
            var eUsuario = (e_Usuario)Session["USUARIO"];
            lbBienvenida.Text = "Bienvenido <br>" + eUsuario.Nombre + " " + eUsuario.Apellido +
                                " <br> realice el cambio de clave para continuar.";
            modalPass.Show();
            pnModal.Visible = true;
            cambioPrograma.Visible = false;
            cambioClave.Visible = true;
        }
       
        protected void btnCambiarPass_OnClick(object sender, EventArgs e)
        {
            try
            {
                lbEstado.Text = "";

                var MD5 = System.Security.Cryptography.MD5.Create();

                var eUsuario = (e_Usuario)Session["USUARIO"];

                if (txtPass1.Text.Length >= 8)
                {
                    if (txtPass1.Text != txtPass2.Text)
                    {
                        lbEstado.Text = "Las contraseñas digitadas no son iguales.";

                    }
                    else
                    {
                        if (txtPass1.Text != "123456789")
                        {
                            var pass = clHash.GetMd5Hash(MD5, txtPass1.Text);

                         
                        }
                        else
                        {
                            lbEstado.Text = "Esta clave no esta permitida por el sistema.";
                        }
                    }
                }
                else
                {
                    lbEstado.Text = "La clave debe de contener mas de 7 digitos.";
                }
            }
            catch (Exception exception)
            {
                clErrores cl = new clErrores();
                cl.escribirError(exception.Message, exception.StackTrace);
            }
        }

        #endregion

    }
}
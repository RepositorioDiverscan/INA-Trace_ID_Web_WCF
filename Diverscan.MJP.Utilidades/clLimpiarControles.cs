using System.Web.UI;
using System.Web.UI.WebControls;
namespace Diverscan.MJP.Utilidades
{
    public class clLimpiarControles
    {
        //Limpia los controles que se encuentren en un panel
        public void LimpiarControles(Panel panel)
        {
            foreach (Control c in panel.Controls)
            {
                switch (c.GetType().ToString())
                {
                    case "System.Web.UI.WebControls.TextBox":

                        ((TextBox) c).Text = "";

                        break;

                    case "System.Web.UI.WebControls.CheckBox":

                        ((CheckBox) c).Checked = false;

                        break;

                    case "System.Web.UI.WebControls.RadioButton":

                        ((RadioButton) c).Checked = false;

                        break;

                    case "System.Web.UI.WebControls.DropDownList":

                        ((DropDownList) c).SelectedIndex = 0;

                        break;

                    case "ystem.Web.UI.WebControls.Label":
                        ((Label) c).Text = "";
                        break;
                }

            }
        }

        public void ProtegerEscritura(Panel panel)
        {
            foreach (Control c in panel.Controls)
            {
                switch (c.GetType().ToString())
                {
                    case "System.Web.UI.WebControls.TextBox":

                        ((TextBox)c).Enabled = false;

                        break;

                    case "System.Web.UI.WebControls.CheckBox":

                        ((CheckBox)c).Enabled = false;

                        break;

                    case "System.Web.UI.WebControls.RadioButton":

                        ((RadioButton)c).Enabled = false;

                        break;

                    case "System.Web.UI.WebControls.DropDownList":

                        ((DropDownList)c).Enabled = false;

                        break;
                }

            }
        }

        public void HabilitarEscritura(Panel panel)
        {
            foreach (Control c in panel.Controls)
            {
                switch (c.GetType().ToString())
                {
                    case "System.Web.UI.WebControls.TextBox":

                        ((TextBox)c).Enabled = true;

                        break;

                    case "System.Web.UI.WebControls.CheckBox":

                        ((CheckBox)c).Enabled = true;

                        break;

                    case "System.Web.UI.WebControls.RadioButton":

                        ((RadioButton)c).Enabled = true;

                        break;

                    case "System.Web.UI.WebControls.DropDownList":

                        ((DropDownList)c).Enabled = true;

                        break;
                }

            }
        }

    }
}

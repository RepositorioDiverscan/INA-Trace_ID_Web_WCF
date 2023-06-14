using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


namespace Diverscan.MJP.Utilidades.general
{
    public class Controls
    {
        public void ClearFormControls(Control ctrls)
        {

            foreach (Control control in ctrls.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = " ";
                    //txtbox.Text = string.Empty;
                    //txtbox.DataBind();
                }
                else if (control is CheckBox)
                {
                    CheckBox chkbox = (CheckBox)control;
                    chkbox.Checked = false;
                    chkbox.DataBind();
                }
                else if (control is RadioButton)
                {
                    RadioButton rdbtn = (RadioButton)control;
                    rdbtn.Checked = false;
                    rdbtn.DataBind();
                }
                else if (control is RadDatePicker)
                {
                    RadDatePicker dtp = (RadDatePicker)control;
                    dtp.SelectedDate = DateTime.Now;
                    dtp.DataBind();
                }
                else if (control is DropDownList) 
                {
                    ((DropDownList)control).ClearSelection();

                }
            }
        }
    }
}

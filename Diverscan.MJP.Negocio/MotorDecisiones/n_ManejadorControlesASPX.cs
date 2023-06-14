using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.Adapters;
using System.Web.UI.WebControls;
using System.Web.Compilation;
using System.Xml;
using Diverscan.MJP.AccesoDatos.MotorDecision;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Negocio.Programa;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using Telerik.Web.UI;
using Telerik.Web.UI.Diagram;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;
using System.Reflection.Emit;

namespace Diverscan.MJP.Negocio.MotorDecisiones
{
    public class n_ManejadorControlesASPX
    {
        /// <summary>
        /// Para que el metodo funcione se debe cumplir la gerarqui de objetos del modelo.
        /// </summary>
        /// <param name="Contenedor" Panel1 = Tab1, Paneln = Tabn></param>
        /// <param name="NombreControl"></param>
        /// <returns></returns>
        /// 

        public static string ObtenerValorTextBox(Control Contenedor, string NombreTXB)
        {
            string Respuesta = "No encontrado";
            try
            {
                foreach (Control c in Contenedor.Controls)
                {
                    //Type type = typeof(clasepagina);
                    if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                    {
                        foreach (Control cc in c.Controls)
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                if (ccc is TextBox)
                                {
                                    TextBox TXB = (TextBox)ccc;
                                    if (TXB.ID == NombreTXB)
                                    {
                                        Respuesta = TXB.Text;
                                    }
                                }
                            }
                        }
                    }
                }
                return Respuesta;
            }
            catch (Exception ex)
            {
                Respuesta = "error,Ops! Ha ocurrido un Error, Codigo: TID-NE-MDS-000001" + ex.Message;
                return Respuesta;
            }
        }



        public static string EditableTextBox(Control Contenedor, string NombreTXB , bool Value) 
        {
            string Respuesta = "No encontrado";
            try
            {
                foreach (Control c in Contenedor.Controls)
                {
                    //Type type = typeof(clasepagina);
                    if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                    {
                        foreach (Control cc in c.Controls)
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                if (ccc is TextBox)
                                {
                                    TextBox TXB = (TextBox)ccc;
                                    if (TXB.ID == NombreTXB)
                                    {
                                        TXB.Visible = Value;
                                        Respuesta = "Visibilidad exitosa";
                                    }
                                }
                            }
                        }
                    }
                }

                return Respuesta;
            }
            catch (Exception ex)
            {
                Respuesta = "error,Ops! Ha ocurrido un Error, Codigo: TID-NE-MDS-000002" + ex.Message;
                return Respuesta;
            }
        
        }


        public static string VisibilidadTextBox(Control Contenedor, string NombreTXB , bool Value)
        {
            string Respuesta = "No encontrado";
            try
            {
                foreach (Control c in Contenedor.Controls)
                {
                    //Type type = typeof(clasepagina);
                    if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                    {
                        foreach (Control cc in c.Controls)
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                if (ccc is TextBox)
                                {
                                    TextBox TXB = (TextBox)ccc;
                                    if (TXB.ID == NombreTXB)
                                    {
                                        TXB.Enabled = Value;
                                        Respuesta = "Habilitado exitosamente";
                                    }
                                }
                            }
                        }
                    }
                }

                return Respuesta;
            }
            catch (Exception ex)
            {
                Respuesta = "error,Ops! Ha ocurrido un Error, Codigo: TID-NE-MDS-000002" + ex.Message;
                return Respuesta;
            }

        }

        public static string CargarTextBox(Control Contenedor, string NombreTXB, string ValorTextBox)
        {
            string Respuesta = "No encontrado";
            try
            {
                foreach (Control c in Contenedor.Controls)
                {
                    //Type type = typeof(clasepagina);
                    if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                    {
                        foreach (Control cc in c.Controls)
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                if (ccc is TextBox)
                                {
                                    TextBox TXB = (TextBox)ccc;
                                    if (TXB.ID == NombreTXB)
                                    {
                                        TXB.Text = ValorTextBox;
                                        Respuesta = "Carga exitosa";
                                    }
                                }
                            }
                        }
                    }
                }

                return Respuesta;
            }
            catch (Exception ex)
            {
                Respuesta = "error,Ops! Ha ocurrido un Error, Codigo: TID-NE-MDS-000002" + ex.Message;
                return Respuesta;
            }
        }

        public static string RetonarIdValorDDL(Control Contenedor, string NombreDDL)
        {
            string idValor = "";
            try
            {
                foreach (Control c in Contenedor.Controls)
                {
                //Type type = typeof(clasepagina);
                 if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                    {
                        foreach (Control cc in c.Controls)
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                if (ccc is DropDownList)
                                {
                                    DropDownList DDL = (DropDownList)ccc;
                                    if (DDL.ID == NombreDDL)
                                    {
                                        idValor = DDL.Text;
                                    }
                                }
                            }
                        }
                    }
                }
                return idValor;
            }
            catch (Exception)
            {
                return idValor;
            }

        }

        /// <summary>
        /// Para que el metodo funcione se debe cumplir la gerarqui de objetos del modelo.
        /// </summary>
        /// <param name="Contenedor" Panel1 = Tab1, Paneln = Tabn></param>
        /// <param name="NombreControl"></param>
        /// <returns></returns>
        public static Control RetornaControlContendor(Control Contenedor, string NombreControl)
        {
            Control ControlDevuelto = null;
            try
            {
                //Type type = typeof(clasepagina);
                 foreach (Control c in Contenedor.Controls)
                {
                    if (c.GetType().ToString().Equals("System.Web.UI.WebControls.Panel"))
                    {
                        foreach (Control cc in c.Controls)
                        {
                            if (cc.ID == NombreControl)
                            {
                                ControlDevuelto = cc;
                            }
                        }
                    }
                }


                 return ControlDevuelto;
            }
            catch (Exception)
            {
                return ControlDevuelto;
            }
 
        }

        public static string ObtenerIDsRadListBox(Control RLA)
        {
            string IDs = "Objeto vacio o incorrecto";
            if (RLA is RadListBox)
            {
                IDs = "";
                RadListBox RLB = (RadListBox)RLA;
                foreach (RadListBoxItem item in RLB.Items)
                {
                    IDs += item.Value.ToString() + ";";
                }
                if (IDs != "")
                {
                    IDs = IDs.Substring(0, IDs.Length - 1);
                }
            }
            
            return IDs;
        }

        public static string[] ObtenerID_Valor_RadListBox(Control Panel, string RadListBoxName, string TextBoxNumericName)
        {
            string Valor1 = n_ManejadorControlesASPX.ObtenerCantidadesRadListBox(n_ManejadorControlesASPX.RetornaControlContendor(Panel, RadListBoxName), TextBoxNumericName);
            string Valor2 = n_ManejadorControlesASPX.ObtenerIDsRadListBox(n_ManejadorControlesASPX.RetornaControlContendor(Panel, RadListBoxName));
            string[] Valores = { Valor1, Valor2 };
            return Valores;

        }



        public static string ObtenerCantidadesRadListBox(Control RLA, string RadNumericTXB)
        {
            string Cant = "Objeto vacio o incorrecto";
            if (RLA is RadListBox)
            {
                Cant = "";
                RadListBox RLB = (RadListBox)RLA;
                foreach (RadListBoxItem item in RLB.Items)
                {
                    //"Qtxt12"
                    RadNumericTextBox RN = (RadNumericTextBox)item.FindControl(RadNumericTXB);
                    Cant += RN.Value.ToString() + ";";
                }
                if (Cant != "")
                {
                    Cant = Cant.Substring(0, Cant.Length - 1);
                }
            }
            return Cant;
        }

        public static bool CargarRadListBox(RadListBox RLB, string SQLQuery, string usuario, string ControlNumerico, string DataTextField, string DataValueField)
        {
            try
            {

                DataSet DS = new DataSet();
                int Fila = 0;
                RLB.Items.Clear();
                DS = n_ConsultaDummy.GetDataSet(SQLQuery, usuario);
                if (DS.Tables[0].Rows.Count == 0)
                {
                    DS.Clear();
                }
                else
                {
                    RLB.DataSource = DS.Tables[0];
                    RLB.DataTextField = DataTextField;
                    RLB.DataValueField = DataValueField;
                    RLB.DataBind();
                    double ValorTxt = 0;
                    foreach (RadListBoxItem item in RLB.Items)
                    {
                        //cuando pasa del 1 al 2
                        RadNumericTextBox RN = (RadNumericTextBox)item.FindControl(ControlNumerico);
                        RN.ReadOnly = false;
                        ValorTxt = double.Parse(DS.Tables[0].Rows[Fila][2].ToString());
                        RN.Value = ValorTxt;
                        RN.Text = ValorTxt.ToString();
                        RN.ReadOnly = true;
                        item.DataBind();
                        Fila++;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

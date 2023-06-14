using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Negocio.MotorDecisiones;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Negocio.Programa;
using Diverscan.MJP.Utilidades;
using Diverscan.Visitas.Utilidades;
using Telerik.Web.UI;
using Telerik.Web.UI.PersistenceFramework;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Meta.Numerics.Matrices; //https://metanumerics.codeplex.com/
using Meta.Numerics.Statistics;
using System.Diagnostics;


namespace Diverscan.MJP.Negocio.UsoGeneral
{
    public class n_SmartMaintenance
    {
        public static string[] EditarDatos(Control Contenedor, string TblMantenimiento, string idUsuario)
        {
             string[] Mensaje = {"error",""};
            try
            {
                string TblTab1Mantenimiento = TblMantenimiento;
                List<String> NombreCampo = new List<String>();
                List<String> ValoresCampo = new List<String>();
                int CantidadCambios = 0;
                int CantidadCampos = 0;
                String SQL = "";
                string KeyColumn = "";
                string KeyValue = "";
                UpdatePanel Up = new UpdatePanel();
               

                foreach (Control c in Contenedor.Controls)
                {
                    if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                    {
                        foreach (Control cc in c.Controls)
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                if (ccc is TextBox)
                                {
                                    TextBox TxtB = (TextBox)ccc;
                                    if (TxtB.CssClass != "TextBoxBusqueda")
                                    {
                                        //carga dato para insertar
                                        if ((TxtB.ID.Substring(0, 3) == "txt" || TxtB.ID.Substring(0, 3) == "txm") && TxtB.ID != "txtRepitacontraseña" && !TxtB.ID.StartsWith("txtSearch"))
                                        {
                                            if (KeyValue == "")
                                            {
                                                return Mensaje;
                                            }
                                            CantidadCampos++;
                                            SQL = "select 1 from " + TblTab1Mantenimiento + " where " + TxtB.ID.Replace("0", string.Empty).Substring(3) + " = '" + TxtB.Text + "' and " + KeyColumn + " = '" + KeyValue + "'";
                                            try
                                            {
                                                if (n_ConsultaDummy.GetDataSet(SQL, idUsuario).Tables[0].Rows.Count == 0)
                                                {
                                                    SQL = "update " + TblTab1Mantenimiento + " set " + TxtB.ID.Replace("0", string.Empty).Substring(3) + " = '" + TxtB.Text + "' where " + KeyColumn + " = '" + KeyValue + "'";
                                                    if (n_ConsultaDummy.PushData(SQL, idUsuario)) CantidadCambios++;
                                                }
                                            }
                                            catch (Exception)
                                            {
                                                //Hay que manejar formatos para fechas
                                            }
                                        }
                                    }
                                    else
                                    {
                                        KeyColumn = TxtB.ID.Replace("0", string.Empty).Substring(3);
                                        KeyValue = TxtB.Text;
                                    }
                                }
                                else
                                {
                                    if (ccc is CheckBox)
                                    {
                                        CheckBox ChkB = (CheckBox)ccc;
                                        //Carga dato para insertar
                                        CantidadCampos++;
                                        if (TblTab1Mantenimiento == "OPEINGCalendarioAnden")
                                          SQL = "select 1 from " + TblTab1Mantenimiento + " where " + ChkB.ID.Substring(3) + " = '" + ChkB.Checked.ToString() + "' and " + KeyColumn + " = '" + KeyValue + "'";
                                         else
                                        SQL = "select 1 from " + TblTab1Mantenimiento + " where " + ChkB.ID.Replace("0", string.Empty).Substring(3) + " = '" + ChkB.Checked.ToString() + "' and " + KeyColumn + " = '" + KeyValue + "'";

                                        if (n_ConsultaDummy.GetDataSet(SQL, idUsuario).Tables[0].Rows.Count == 0)
                                        {
                                            if (TblTab1Mantenimiento == "OPEINGCalendarioAnden")
                                              SQL = "update " + TblTab1Mantenimiento + " set " + ChkB.ID.Substring(3) + " = '" + ChkB.Checked.ToString() + "' where " + KeyColumn + " = '" + KeyValue + "'";
                                             else 
                                            SQL = "update " + TblTab1Mantenimiento + " set " + ChkB.ID.Replace("0", string.Empty).Substring(3) + " = '" + ChkB.Checked.ToString() + "' where " + KeyColumn + " = '" + KeyValue + "'";
                                            if (n_ConsultaDummy.PushData(SQL, idUsuario)) CantidadCambios++;
                                        }
                                    }
                                    else
                                    {
                                        if (ccc is DropDownList)
                                        {
                                            DropDownList DdlB = (DropDownList)ccc;
                                            //Carga dato para insertar
                                            if (DdlB.ID.StartsWith("ddl"))
                                            {
                                                CantidadCampos++;
                                                SQL = "select 1 from " + TblTab1Mantenimiento + " where " + DdlB.ID.Replace("0", string.Empty).Substring(3) + " = '" + DdlB.Text + "' and " + KeyColumn + " = '" + KeyValue + "'";
                                                if (n_ConsultaDummy.GetDataSet(SQL, idUsuario).Tables[0].Rows.Count == 0)
                                                {
                                                    SQL = "update " + TblTab1Mantenimiento + " set " + DdlB.ID.Replace("0", string.Empty).Substring(3) + " = '" + DdlB.Text + "' where " + KeyColumn + " = '" + KeyValue + "'";
                                                    if (n_ConsultaDummy.PushData(SQL, idUsuario)) CantidadCambios++;
                                                }

                                            }
                                        }
                                        else 
                                        {
                                            if (ccc is RadDatePicker)
                                            {
                                                RadDatePicker radDatePicker = (RadDatePicker)ccc;

                                                if (radDatePicker.ID.StartsWith("txt") || radDatePicker.ID.StartsWith("dtp")) 
                                                {
                                                    CantidadCambios++;
                                                    SQL = "select 1 from " + TblTab1Mantenimiento + " where CONVERT(VARCHAR," + radDatePicker.ID.Replace("0", string.Empty).Substring(3) + ", 103) = CONVERT(VARCHAR,'" + radDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd") + "', 103) and " + KeyColumn + " = '" + KeyValue + "'";
                                                    if (n_ConsultaDummy.GetDataSet(SQL, idUsuario).Tables[0].Rows.Count == 0)
                                                    {
                                                        SQL = "update " + TblTab1Mantenimiento + " set " + radDatePicker.ID.Replace("0", string.Empty).Substring(3) + " = '" + radDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd") + "' where " + KeyColumn + " = '" + KeyValue + "'";
                                                        if (n_ConsultaDummy.PushData(SQL, idUsuario)) CantidadCambios++;
                                                    } 
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }

                }

                decimal PorcCoincidencia = (decimal.Parse(CantidadCambios.ToString()) / decimal.Parse(CantidadCampos.ToString()) * 100);
                if (PorcCoincidencia > 0)
                {
                    Mensaje[0] = "ok";
                    Mensaje[1] = "Dato actualizado exitosamente. El registro cambio en un " + Math.Round(PorcCoincidencia, 2) + "%";
                }
                else
                {
                    Mensaje[0] = "info";
                    Mensaje[1] = "Al parecer no tenemos nada que actualizar, todo esta como antes!";
                }

                return Mensaje;
            }
            catch (Exception ex)
            {
               Mensaje[0] = "error";
               Mensaje[1] = "Ops! Ha ocurrido un Error, Codigo:TID-NE-SMM-000001" + ex.ToString();
               return Mensaje; 
            }
        }
       
        public static string[] AgregarDatos(Control Contenedor, string TblMantenimiento, int ToleranciaAgregar, string idUsuario)
        {

            
            string[] Mensaje = { "error", "" };
            try
            {
                List<String> NombreCampo = new List<String>();
                List<String> ValoresCampo = new List<String>();
                int CantConcidencias = 0;
                String SQL = "";

                foreach (Control c in Contenedor.Controls)
                {
                    if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                    {
                        foreach (Control cc in c.Controls)
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                if (ccc is TextBox)
                                {

                                  
                                    TextBox TxtB = (TextBox)ccc;
                                    if (TxtB.CssClass != "TextBoxBusqueda")
                                    {
                                        if ((TxtB.ID.Substring(0, 3) == "txt" || TxtB.ID.Substring(0, 3) == "txm") && TxtB.ID != "txtRepitacontraseña" && !TxtB.ID.StartsWith("txtSearch"))
                                        {
                                            //carga dato para insertar
                                            NombreCampo.Add(TxtB.ID.Replace("0", string.Empty).Substring(3));
                                            ValoresCampo.Add(TxtB.Text.Trim());
                                            SQL = "select 1 from " + TblMantenimiento + " where " + TxtB.ID.Replace("0", string.Empty).Substring(3) + " = '" + TxtB.Text.Trim() + "'";
                                            try
                                            {
                                                if (n_ConsultaDummy.GetDataSet(SQL, idUsuario).Tables[0].Rows.Count > 0)
                                                {
                                                    CantConcidencias++;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Mensaje[0] = "error";
                                                Mensaje[1] = "Ops! Ha ocurrido un Error, Codigo:TID-NE-SMM-000002" + ex.ToString();
                                                return Mensaje; 
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (ccc is CheckBox)
                                    {
                                        CheckBox ChkB = (CheckBox)ccc;
                                        //Carga dato para insertar
                                        NombreCampo.Add(ChkB.ID.Replace("0", string.Empty).Substring(3));
                                        ValoresCampo.Add(ChkB.Checked.ToString());
                                        SQL = "select 1 from " + TblMantenimiento + " where " + ChkB.ID.Replace("0", string.Empty).Substring(3) + " = '" + ChkB.Checked.ToString() + "'";
                                        if (n_ConsultaDummy.GetDataSet(SQL, idUsuario).Tables[0].Rows.Count > 0)
                                        {
                                            CantConcidencias++;
                                        }
                                    }
                                    else
                                    {
                                        if (ccc is DropDownList)
                                        {
                                            DropDownList DdlB = (DropDownList)ccc;
                                            //Carga dato para insertar
                                            if (DdlB.ID.Substring(0, 3) == "ddl")
                                            {
                                                NombreCampo.Add(DdlB.ID.Replace("0", string.Empty).Substring(3));
                                                ValoresCampo.Add(DdlB.SelectedItem.Value);
                                                SQL = "select 1 from " + TblMantenimiento + " where " + DdlB.ID.Replace("0", string.Empty).Substring(3) + " = '" + DdlB.Text + "'";
                                                if (n_ConsultaDummy.GetDataSet(SQL, idUsuario).Tables[0].Rows.Count > 0)
                                                {
                                                    CantConcidencias++;                                                            
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (ccc is RadDatePicker)  // control que muestra hora a elejir.
                                            {
                                                RadDatePicker DdlB = (RadDatePicker)ccc;
                                                //Carga dato para insertar
                                                if (DdlB.ID.Substring(0, 3) == "txt" || DdlB.ID.Substring(0, 3) == "dtp")
                                                {
                                                    NombreCampo.Add(DdlB.ID.Replace("0", string.Empty).Substring(3));
                                                    ValoresCampo.Add(DdlB.SelectedDate.Value.ToString("yyyy-MM-dd"));
                                                    SQL = "select 1 from " + TblMantenimiento + " where CONVERT(VARCHAR," + DdlB.ID.Replace("0", string.Empty).Substring(3) + ",103) =  CONVERT(VARCHAR,'" + DdlB.SelectedDate.Value.ToString() + "',103)";
                                                    if (n_ConsultaDummy.GetDataSet(SQL, idUsuario).Tables[0].Rows.Count > 0)
                                                    {
                                                        CantConcidencias++;
                                                    }

                                                }
                                             }
                                         }
                                    }
                                 }
                            } 
                        }
                    }

                }
                decimal PorcCoincidencia = (decimal.Parse(CantConcidencias.ToString()) / decimal.Parse(NombreCampo.Count.ToString()) * 100);


                //if ()  // if (PorcCoincidencia > ToleranciaAgregar)
                if (PorcCoincidencia > ToleranciaAgregar)
                {
                    Mensaje[0] = "info";
                    Mensaje[1] = "Lamentablemente no tenemos autorizado agregar este registro, pues coincide con otro registro al " + Math.Round(PorcCoincidencia, 2).ToString() + "%, siendo mayor a la toleracia que es: " + ToleranciaAgregar.ToString() + "%";
                }
                else
                {
                    string CampoColumnas = "(";
                    string CamposValues = "('";
                    foreach (string NombreCampo_ in NombreCampo)
                    {
                        CampoColumnas += NombreCampo_ + ",";
                    }

                    // se quita el ultimo caracter
                    CampoColumnas = CampoColumnas.Substring(0, CampoColumnas.Length - 1) + ")";

                    foreach (string ValoresCampo_ in ValoresCampo)
                    {
                        CamposValues += ValoresCampo_ + "','";
                    }

                    //Se quitan los ultimos dos caracteres: ,'
                    CamposValues = CamposValues.Substring(0, CamposValues.Length - 2) + ")";

                    string ConsultaSQL = "insert into " + TblMantenimiento + CampoColumnas + " values " + CamposValues;
                    if (n_ConsultaDummy.PushData(ConsultaSQL, idUsuario))
                    {
                        if  (Contenedor.NamingContainer.ToString () == "xx")
                            SQL ="yy";

                        Mensaje[0] = "ok";
                        Mensaje[1] = "Dato ingresado exitosamente";
                        
                    }
                    else
                    {
                        Mensaje[0] = "ok";
                        Mensaje[1] = "El dato no fue ingresado, revise los valores o contacte al administrador"; 
                    }    
                }
                return Mensaje;
            }
            catch (Exception ex)
            {
                Mensaje[0] = "error";
                Mensaje[1] = "Ops! Ha ocurrido un Error, Codigo:TID-NE-SMM-000003" + ex.ToString();
                return Mensaje;
            }

            
        }

        public static string[] CargarGrid(Control Contenedor, string idUsuario)
        {
            string[] Mensaje = { "error", "" };
            try
            {
                string VistaDetalle = "";
                DataSet DSDetalle = new DataSet();
                TextBox KEY = new TextBox();
                RadGrid RG = new RadGrid();

                foreach (Control c in Contenedor.Controls)
                {
                    if (c.GetType().ToString().Equals("System.Web.UI.WebControls.Panel"))
                    {
                        Panel Panel = (Panel)c;
                        if ((Panel.CssClass == "TituloPanelVistaDetalle") ||  (Panel.CssClass == "TituloParaDespacho"))
                            VistaDetalle = Panel.ID.Replace("0", string.Empty).Replace("0", string.Empty);
                    }
                    if (c.GetType().ToString().Equals("System.Web.UI.WebControls.TextBox"))
                    {
                        TextBox TxtB = (TextBox)c;

                        if (TxtB.CssClass == "TextBoxBusqueda")
                        {
                            KEY = TxtB;
                        }
                    }

                    if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                    {
                        foreach (Control cc in c.Controls)
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                if (ccc is TextBox)
                                {
                                    TextBox TxtB = (TextBox)ccc;
                                    if (TxtB.CssClass == "TextBoxBusqueda")
                                    {
                                        KEY = TxtB;
                                    }
                                }
                                else
                                {
                                    if (ccc is DropDownList)
                                    {
                                        DropDownList DdlB = (DropDownList)ccc;
                                        if (VistaDetalle == "Vista_Calendarioanden" && DdlB.ID == "ddlIdAnden")
                                        {
                                            //KEY.Text = DdlB.SelectedItem.Text;
                                            //KEY.ID = DdlB.DataValueField.Replace("Id", string.Empty);
                                        }

                                        else
                                        {
                                            try
                                            {
                                                DdlB.Text = "--Seleccionar--";
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (ccc is RadDatePicker)
                                        {
                                            RadDatePicker DdlB = (RadDatePicker)ccc;
                                            if (VistaDetalle == "Vista_Calendarioanden" && DdlB.ID == "RadDatePicker1")
                                            {
                                                //KEY.Text += DdlB.SelectedDate.ToString().Substring (0,10);
                                                //KEY.ID += "+convert(nvarchar(10),Fecha,103)";
                                            }
                                        }

                            }
                        }
                    }
                        }
                    }
                    if (c.GetType().ToString().Equals("Telerik.Web.UI.RadGrid"))
                    {
                        RadGrid RGn = (RadGrid)c;
                        RG = RGn;
                    }
                }

                if (VistaDetalle != "")
                {
                    DSDetalle = n_ConsultaDummy.GetDataSet(VistaDetalle, KEY, idUsuario);
                    if (DSDetalle.Tables[0].Rows.Count > 0)
                    {
                        RG.DataSource = DSDetalle;
                        RG.Rebind();
                    }
                }
                //KEY.Text = string.Empty;

                return Mensaje;
            }
            catch (Exception ex)
            {
                Mensaje[1] = "Ops! Ha ocurrido un Error, Codigo:TID-NE-SMM-000004" + ex.ToString();
                return Mensaje;
            }
        }

        public static string[] CargarDatos(Control Contenedor, string idUsuario)
        {
            string[] Mensaje = { "error", "" };
            try
            {
                string Vista = "";
                string VistaDetalle = "";
                TextBox KEY = new TextBox();
                KEY.Text = "";
                DataSet DS = new DataSet();
                DataSet DSDetalle = new DataSet();
                RadGrid RG = new RadGrid();

                foreach (Control c in Contenedor.Controls)
                {
                    if (c.GetType().ToString().Equals("System.Web.UI.WebControls.Panel"))
                    {
                        Panel Panel = (Panel)c;
                        if (Panel.CssClass == "TituloPanelVista")
                        {
                            Vista = Panel.ID.Replace("0", string.Empty);
                        }
                        else
                        {
                            if (Panel.CssClass == "TituloPanelVistaDetalle")
                                VistaDetalle = Panel.ID.Replace("0", string.Empty).Replace("0", string.Empty);
                        }
                    }

                    if (c.GetType().ToString().Equals("System.Web.UI.WebControls.TextBox"))
                    {
                        TextBox TxtB = (TextBox)c;

                        if (TxtB.CssClass == "TextBoxBusqueda")
                        {
                            KEY = TxtB;
                        }
                        else
                        {
                            TxtB.Text = "";
                        }
                    }

                    //if (OnlyGrid == false)
                    //{
                    if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                    {
                        foreach (Control cc in c.Controls)
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                if (ccc is TextBox)
                                {
                                    TextBox TxtB = (TextBox)ccc;
                                    //if (TxtB.CssClass == "txtSearch")                                 
                                    if (TxtB.CssClass == "TextBoxBusqueda")
                                    {
                                        KEY = TxtB;
                                    }
                                    else
                                    {
                                        TxtB.Text = "";
                                    }
                                }
                                else
                                {
                                    if (ccc is CheckBox)
                                    {
                                        CheckBox ChkB = (CheckBox)ccc;
                                        ChkB.Checked = false;
                                    }
                                    else
                                    {
                                        if (ccc is DropDownList)
                                        {
                                            DropDownList DdlB = (DropDownList)ccc;
                                            try
                                            {
                                                DdlB.Text = "--Seleccionar--";
                                            }
                                            catch (Exception ex)
                                            {
                                                Mensaje[0] = "error";
                                                Mensaje[1] = "Ops! Ha ocurrido un Error, Codigo:TID-NE-SMM-000005" + ex.ToString();
                                                return Mensaje;
                                            }
                                        }
                                    }
                                }
                                if (ccc is RadDatePicker)
                                {
                                    RadDatePicker Ddlb = (RadDatePicker)ccc;
                                    try
                                    {
                                        Ddlb.SelectedDate = (null);
                                        //Ddlb.Nulltext = "";
                                        //Value.ToShortDateString();//or selectedDate.Value.ToString("d");
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                            }
                        }
                    }

                    if (c.GetType().ToString().Equals("System.Web.UI.WebControls.CheckBox"))
                    {
                        CheckBox ChkB = (CheckBox)c;
                        ChkB.Checked = false;
                    }

                    if (c.GetType().ToString().Equals("System.Web.UI.WebControls.DropDownList"))
                    {
                        DropDownList DdlB = (DropDownList)c;
                        DdlB.Text = "--Seleccionar--";
                        //KEY.ID = DdlB.DataTextField;
                        //KEY.Text = DdlB.DataValueField;
                    }

                    if (c.GetType().ToString().Equals("Telerik.Web.UI.RadGrid"))
                    {
                        RadGrid RGn = (RadGrid)c;
                        RG = RGn;
                    }

                }

                DS = n_ConsultaDummy.GetDataSet(Vista, KEY, idUsuario);
                double cantregistros = 0;
                try
                {
                    cantregistros = DS.Tables[0].Rows.Count;
                }
                catch (Exception)
                {
                    cantregistros = 0;
                }

                if (KEY.Text != "")
                {
                    if (cantregistros == 0)
                    {
                        Mensaje[0] = "info";
                        Mensaje[1] = "No hay resultados para la busqueda";
                    }
                    else
                    {
                        if (cantregistros == 1)
                        {
                            Mensaje[0] = "info";
                            Mensaje[1] = "Encontramos " + cantregistros.ToString() + " registro para su busqueda.";
                        }
                        else
                        {
                            Mensaje[0] = "info";
                            Mensaje[1] = "Encontramos " + cantregistros.ToString() + " registros para su busqueda.";
                        }
                    }
                }

                CargarGrid(Contenedor, idUsuario);

                if (Vista != "" && KEY.Text != "")
                {
                    foreach (Control c in Contenedor.Controls)
                    {
                        if (c.GetType().ToString().Equals("System.Web.UI.WebControls.TextBox"))
                        {
                            TextBox TxtB = (TextBox)c;
                            if (TxtB.CssClass != "TextBoxBusqueda")
                            {
                                if (DS.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataColumn column in DS.Tables[0].Columns)
                                    {
                                        if (column.ColumnName == TxtB.ID.Replace("0", string.Empty))
                                        {
                                            TxtB.Text = DS.Tables[0].Rows[0][column].ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    TxtB.Text = "";
                                }
                            }
                        }

                        if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                        {
                            foreach (Control cc in c.Controls)
                            {
                                foreach (Control ccc in cc.Controls)
                                {
                                    if (ccc is TextBox)
                                    {
                                        TextBox TxtB = (TextBox)ccc;
                                        if (TxtB.CssClass != "TextBoxBusqueda")
                                        {
                                            if (DS.Tables[0].Rows.Count > 0)
                                            {
                                                foreach (DataColumn column in DS.Tables[0].Columns)
                                                {
                                                    if (column.ColumnName == TxtB.ID.Replace("0", string.Empty))
                                                    {
                                                        TxtB.Text = DS.Tables[0].Rows[0][column].ToString();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                TxtB.Text = "";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (ccc is CheckBox)
                                        {
                                            CheckBox ChkB = (CheckBox)ccc;
                                            if (DS.Tables[0].Rows.Count > 0)
                                            {
                                                foreach (DataColumn column in DS.Tables[0].Columns)
                                                {
                                                    if (VistaDetalle == "Vista_Calendarioanden")
                                                    {
                                                        if (column.ColumnName == ChkB.ID)
                                                            ChkB.Checked = Convert.ToBoolean(DS.Tables[0].Rows[0][column].ToString());
                                                    }
                                                    else
                                                    {
                                                        if (column.ColumnName == ChkB.ID.Replace("0", string.Empty))
                                                        {
                                                            ChkB.Checked = Convert.ToBoolean(DS.Tables[0].Rows[0][column].ToString());
                                                        }
                                                    }
                                                }
                                            }

                                        }
                                        else
                                        {
                                            if (ccc is DropDownList)
                                            {
                                                DropDownList DdlB = (DropDownList)ccc;
                                                if (DS.Tables[0].Rows.Count > 0)
                                                {
                                                    foreach (DataColumn column in DS.Tables[0].Columns)
                                                    {
                                                        if (column.ColumnName == DdlB.ID.Replace("0", string.Empty))
                                                        {
                                                            string Texto = DS.Tables[0].Rows[0][column].ToString();
                                                            DdlB.SelectedIndex = DdlB.Items.IndexOf(DdlB.Items.FindByText(Texto));
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (ccc is RadDatePicker)  // control que muestra hora a elejir.
                                                {
                                                    RadDatePicker DdlB = (RadDatePicker)ccc;
                                                    if (DS.Tables[0].Rows.Count > 0)
                                                    {
                                                        foreach (DataColumn column in DS.Tables[0].Columns)
                                                        {
                                                            if (column.ColumnName == DdlB.ID)
                                                            {
                                                                string Texto = DS.Tables[0].Rows[0][column].ToString();
                                                                DdlB.SelectedDate = DateTime.Parse(DS.Tables[0].Rows[0][column].ToString());
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }


                                    }
                                }
                            }
                        }

                        if (c.GetType().ToString().Equals("System.Web.UI.WebControls.CheckBox"))
                        {
                            CheckBox ChkB = (CheckBox)c;
                            if (DS.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataColumn column in DS.Tables[0].Columns)
                                {
                                    if (column.ColumnName == ChkB.ID.Replace("0", string.Empty))
                                    {
                                        ChkB.Checked = Convert.ToBoolean(DS.Tables[0].Rows[0][column].ToString());
                                    }
                                }
                            }
                        }



                        if (c.GetType().ToString().Equals("System.Web.UI.WebControls.DropDownList"))
                        {
                            DropDownList DdlB = (DropDownList)c;
                            if (DS.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataColumn column in DS.Tables[0].Columns)
                                {
                                    if (column.ColumnName == DdlB.ID.Replace("0", string.Empty))
                                    {
                                        string Texto = DS.Tables[0].Rows[0][column].ToString();
                                        DdlB.SelectedIndex = DdlB.Items.IndexOf(DdlB.Items.FindByText(Texto));
                                    }
                                }
                            }
                        }
                    }
                }



                return Mensaje;
            }
            catch (Exception ex)
            {
                Mensaje[1] = "Ops! Ha ocurrido un Error, Codigo:TID-NE-SMM-000006" + ex.ToString();
                return Mensaje;
            }
        }

        public static string[] CargarDDL(DropDownList DD, string TBL, string IdUsuario, bool Compania)
        {
            string[] Mensaje = { "error", "" };
            try
            {
                string idCompania = n_MotorDecisiones.Metodos.ObtenerCompaniaXUsuario(IdUsuario);

                string SQL = "select ";
                string ID = DD.ID.Substring(3);
                ID = ID.Replace("0", string.Empty);
                SQL += ID;
                SQL += ", Nombre from " + TBL;

                if (Compania)
                {
                    SQL += " WHERE idCompania = '" + idCompania + "' ";
                }


                if (ID.ToUpper() != "NOMBRE")
                {
                    SQL += " order by Nombre";
                }
                DD.DataTextField = "Nombre";
                DD.DataSource = n_ConsultaDummy.GetDataSet(SQL, IdUsuario);
                // le quita el ddlid
                DD.DataValueField = ID;
                DD.DataBind();
                DD.Items.Insert(0, new ListItem("--Seleccionar--"));
                ///Aqui se debe seleccionar la compania por defecto que tenga el usuario asignada, y si es ID=BASE,
                return Mensaje;
            }
            catch (Exception ex)
            {
                Mensaje[1] = "error, Ops! Ha ocurrido un Error, Codigo: TID-NE-SMM-000007" + ex.Message;
                return Mensaje;
            }
        }
        public static string[] CargarDDLHoras(DropDownList DD)
        {
            string[] Mensaje = { "error", "" };
            try
            {
                string SQL = "select ";
                SQL += "idHoraDia, Nombre from " + e_TablasBaseDatos.TblHorasDia();
                DD.DataSource = n_ConsultaDummy.GetDataSet(SQL, "0");
                DD.DataTextField = "Nombre";
                DD.DataValueField = "idHoraDia";
                DD.DataBind();
                DD.Items.Insert(0, new ListItem("--Seleccionar--"));
                ///Aqui se debe seleccionar la compania por defecto que tenga el usuario asignada, y si es ID=BASE,
                return Mensaje;
            }
            catch (Exception ex)
            {
                Mensaje[1] = "error, Ops! Ha ocurrido un Error, Codigo: TID-NE-SMM-000008" + ex.Message;
                return Mensaje;
            }
        }

        public static void CargarDDLsHoras(Control Contenedor)
        {
            string[] Mensaje = { "error", "" };
            try
            {
                DataSet DS = new DataSet();

                string SQL = "select ";
                SQL += "idHoraDia, Nombre from " + e_TablasBaseDatos.TblHorasDia();
                DS = n_ConsultaDummy.GetDataSet(SQL, "0");
                foreach (Control c in Contenedor.Controls)
                {
                    if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                    {
                        foreach (Control cc in c.Controls)
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                if (ccc is DropDownList)
                                {
                                    DropDownList DDL = (DropDownList)ccc;
                                    if (DDL.ID.Length > 9)                                    
                                    {
                                        if (DDL.ID.Substring(0, 9) == "ddlidHora")
                                        {
                                            DDL.DataSource = DS.Tables[0];
                                            DDL.DataTextField = "Nombre";
                                            DDL.DataValueField = "idHoraDia";
                                            DDL.DataBind();
                                            DDL.Items.Insert(0, new ListItem("--Seleccionar--"));

                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje[1] = "error, Ops! Ha ocurrido un Error, Codigo: TID-NE-SMM-000009" + ex.Message;               
            }
        }

        public static string CargarEjecutarAccion(string Pagina, Control Contendor, string idUsuario, string NombreObjeto)
        {
            string resultado="";
            try
            {
                n_MotorDecisiones.Metodos Ejecutar = new n_MotorDecisiones.Metodos();
                n_MotorDecisiones.Metodos MD = new n_MotorDecisiones.Metodos();
                //List<e_AccionFlujo> Acciones_pre = MD.ObtenerAccionFlujo(Page.AppRelativeVirtualPath.ToString(), BotonAccion);
                List<e_AccionFlujo> Acciones = new List<e_AccionFlujo>();
                e_AccionFlujo Accion = new e_AccionFlujo();
                Acciones = MD.ObtenerAccionObjeto(Pagina, NombreObjeto);
                
                Accion = Acciones.Find(x => x.ObjetoFuente == NombreObjeto);
                List<int> iteraciones = new List<int>();
                int iteracion = 0;
                bool ContinuarEjecucion = true;

                foreach (e_Metodo Metodo in Accion.Metodos)
                {
                    if (ContinuarEjecucion)
                    {
                        iteracion = Metodo.Parametros.Max(x => x.ValorM.Count);
                        resultado = "";
                        string resultadoTemp = "";
                        decimal NumeroTemp = 0;
                        bool EsDecimal = false;
                        for (int i = 0; i < iteracion; i++)
                        {
                            foreach (e_ParametrosWF PM in Metodo.Parametros)
                            {
                                try
                                {
                                    PM.Valor = PM.ValorM[i].ValorMulti;
                                }
                                catch (Exception)
                                {
                                    PM.Valor = PM.Valor; // esto no hace nada pero explica que intenta asignar un valor
                                }
                            }
                            if (Metodo.IdParametroAccion != 0)
                            {
                                if (Metodo.AcumulaSalida == true)
                                {
                                    resultadoTemp = MD.Accionar(Metodo, Contendor, idUsuario, Accion.IdAccion.ToString(), Metodo.idMetodoAccion.ToString());
                                    if (Metodo.idTipoMetodo == 2)//es dos porque es decsion, 1 para Accion normal
                                    {
                                        if (resultadoTemp == "False")
                                            ContinuarEjecucion = false;
                                        break;
                                    }
                                    EsDecimal = Decimal.TryParse(resultadoTemp, out NumeroTemp);
                                    if (EsDecimal)
                                    {
                                        if (resultado == "") resultado = "0";
                                        NumeroTemp = decimal.Parse(resultado) + NumeroTemp;
                                        resultado = NumeroTemp.ToString();
                                    }
                                    else
                                    {
                                        resultado += resultadoTemp;
                                    }
                                }
                                else
                                {
                                    resultado = MD.Accionar(Metodo, Contendor, idUsuario, Accion.IdAccion.ToString(), Metodo.idMetodoAccion.ToString());
                                }
                                MD.ActualizarValorParametro(Metodo.IdParametroAccion.ToString(), resultado, idUsuario);
                                e_Metodo M2 = new e_Metodo();
                                var a = Accion.Metodos.FirstOrDefault(x => x.Parametros.Any(y => y.IdParametroAccion == Metodo.IdParametroAccion));
                                var b = a.Parametros.FirstOrDefault(x => x.IdParametroAccion == Metodo.IdParametroAccion);
                                b.Valor = resultado;
                                List<e_ValorMultipleValor> VMS = new List<e_ValorMultipleValor>();
                                e_ValorMultipleValor VM = new e_ValorMultipleValor();
                                string[] valores = b.Valor.Split(';');
                                foreach (string str in valores)
                                {
                                    VM = new e_ValorMultipleValor();
                                    VM.ValorMulti = str;
                                    VM.TipoParametro = b.TipoParametro;
                                    VMS.Add(VM);
                                }
                                b.ValorM = VMS;
                            }
                            else
                            {
                                if (Metodo.AcumulaSalida == true)
                                {
                                    resultadoTemp = MD.Accionar(Metodo, Contendor, idUsuario, Accion.IdAccion.ToString(), Metodo.idMetodoAccion.ToString());
                                    if (Metodo.idTipoMetodo == 2)//es dos porque es decsion, 1 para Accion normal
                                    {
                                        if (resultadoTemp == "False")
                                            ContinuarEjecucion = false;
                                        break;
                                    }
                                    EsDecimal = Decimal.TryParse(resultadoTemp, out NumeroTemp);
                                    if (EsDecimal)
                                    {
                                        if (resultado == "") resultado = "0";
                                        NumeroTemp = decimal.Parse(resultado) + NumeroTemp;
                                        resultado = NumeroTemp.ToString();
                                    }
                                    else
                                    {
                                        resultado += resultadoTemp;
                                    }
                                }
                                else
                                {
                                    resultado = MD.Accionar(Metodo, Contendor, idUsuario, Accion.IdAccion.ToString(), Metodo.idMetodoAccion.ToString());
                                    if (Metodo.idTipoMetodo == 2)//es dos porque es decsion, 1 para Accion normal
                                    {
                                        if (resultado == "False")
                                            ContinuarEjecucion = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }// if
                    else
                    {
                        resultado = "Resultado false, se termina proceso";
                    }
                    
                }
                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }
        }

        public static string CargarEjecutarAccion(string Pagina, string CodLeido, string idUsuario, string NombreObjeto)
        {
            string resultado = "";
            try
            {
                n_MotorDecisiones.Metodos Ejecutar = new n_MotorDecisiones.Metodos();
                n_MotorDecisiones.Metodos MD = new n_MotorDecisiones.Metodos();
                //List<e_AccionFlujo> Acciones_pre = MD.ObtenerAccionFlujo(Page.AppRelativeVirtualPath.ToString(), BotonAccion);
                List<e_AccionFlujo> Acciones = new List<e_AccionFlujo>();
                e_AccionFlujo Accion = new e_AccionFlujo();
                Acciones = MD.ObtenerAccionObjeto(Pagina, NombreObjeto);

                Accion = Acciones.Find(x => x.ObjetoFuente == NombreObjeto);
                List<int> iteraciones = new List<int>();
                int iteracion = 0;
                bool ContinuarEjecucion = true;

                foreach (e_Metodo Metodo in Accion.Metodos)
                {
                    if (ContinuarEjecucion)
                    {
                        iteracion = Metodo.Parametros.Max(x => x.ValorM.Count);
                        resultado = "";
                        string resultadoTemp = "";
                        decimal NumeroTemp = 0;
                        bool EsDecimal = false;
                        for (int i = 0; i < iteracion; i++)
                        {
                            foreach (e_ParametrosWF PM in Metodo.Parametros)
                            {
                                try
                                {
                                    PM.Valor = PM.ValorM[i].ValorMulti;
                                }
                                catch (Exception)
                                {
                                    PM.Valor = PM.Valor; // esto no hace nada pero explica que intenta asignar un valor
                                }
                            }
                            if (Metodo.IdParametroAccion != 0)
                            {
                                if (Metodo.AcumulaSalida == true)
                                {
                                    resultadoTemp = MD.Accionar(Metodo, CodLeido, idUsuario, Accion.IdAccion.ToString(), Metodo.idMetodoAccion.ToString());
                                    if (Metodo.idTipoMetodo == 2)//es dos porque es decsion, 1 para Accion normal
                                    {
                                        if (resultadoTemp == "False")
                                            ContinuarEjecucion = false;
                                        break;
                                    }
                                    EsDecimal = Decimal.TryParse(resultadoTemp, out NumeroTemp);
                                    if (EsDecimal)
                                    {
                                        if (resultado == "") resultado = "0";
                                        NumeroTemp = decimal.Parse(resultado) + NumeroTemp;
                                        resultado = NumeroTemp.ToString();
                                    }
                                    else
                                    {
                                        resultado += resultadoTemp;
                                    }
                                }
                                else
                                {
                                    resultado = MD.Accionar(Metodo, CodLeido, idUsuario, Accion.IdAccion.ToString(), Metodo.idMetodoAccion.ToString());
                                }
                                MD.ActualizarValorParametro(Metodo.IdParametroAccion.ToString(), resultado, idUsuario);
                                e_Metodo M2 = new e_Metodo();
                                var a = Accion.Metodos.FirstOrDefault(x => x.Parametros.Any(y => y.IdParametroAccion == Metodo.IdParametroAccion));
                                var b = a.Parametros.FirstOrDefault(x => x.IdParametroAccion == Metodo.IdParametroAccion);
                                b.Valor = resultado;
                                List<e_ValorMultipleValor> VMS = new List<e_ValorMultipleValor>();
                                e_ValorMultipleValor VM = new e_ValorMultipleValor();
                                string[] valores = b.Valor.Split(';');
                                foreach (string str in valores)
                                {
                                    VM = new e_ValorMultipleValor();
                                    VM.ValorMulti = str;
                                    VM.TipoParametro = b.TipoParametro;
                                    VMS.Add(VM);
                                }
                                b.ValorM = VMS;
                            }
                            else
                            {
                                if (Metodo.AcumulaSalida == true)
                                {
                                    resultadoTemp = MD.Accionar(Metodo, CodLeido, idUsuario, Accion.IdAccion.ToString(), Metodo.idMetodoAccion.ToString());
                                    if (Metodo.idTipoMetodo == 2)//es dos porque es decsion, 1 para Accion normal
                                    {
                                        if (resultadoTemp == "False")
                                            ContinuarEjecucion = false;
                                        break;
                                    }
                                    EsDecimal = Decimal.TryParse(resultadoTemp, out NumeroTemp);
                                    if (EsDecimal)
                                    {
                                        if (resultado == "") resultado = "0";
                                        NumeroTemp = decimal.Parse(resultado) + NumeroTemp;
                                        resultado = NumeroTemp.ToString();
                                    }
                                    else
                                    {
                                        resultado += resultadoTemp;
                                    }
                                }
                                else
                                {
                                    resultado = MD.Accionar(Metodo, CodLeido, idUsuario, Accion.IdAccion.ToString(), Metodo.idMetodoAccion.ToString());
                                    if (Metodo.idTipoMetodo == 2)//es dos porque es decsion, 1 para Accion normal
                                    {
                                        if (resultado == "False")
                                            ContinuarEjecucion = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }// if
                    else
                    {
                        resultado = "Resultado false, se termina proceso";
                    }

                }
                return resultado;
            }
            catch (Exception ex )
            {
                Debug.WriteLine(ex);
                return resultado;
            }
        }

        public static string CargarEjecutarAccionMD(string BaseDatos, string Tabla, string idUsuario, string Evento)
        {
            string resultado = "";
            try
            {
                n_MotorDecisiones.Metodos Ejecutar = new n_MotorDecisiones.Metodos();
                n_MotorDecisiones.Metodos MD = new n_MotorDecisiones.Metodos();
                //List<e_AccionFlujo> Acciones_pre = MD.ObtenerAccionFlujo(Page.AppRelativeVirtualPath.ToString(), BotonAccion);
                List<e_AccionFlujo> Acciones_ = new List<e_AccionFlujo>();
                e_AccionFlujo Accion = new e_AccionFlujo();
                Acciones_ = MD.ObtenerAccionObjetoMD(BaseDatos, Tabla);

                Accion = Acciones_.Find(x => x.ObjetoFuente == Tabla && x.Envento.Nombre == Evento);
                List<int> iteraciones = new List<int>();
                int iteracion = 0;

                foreach (e_Metodo Metodo in Accion.Metodos)
                {

                    iteracion = Metodo.Parametros.Max(x => x.ValorM.Count);
                    resultado = "";
                    string resultadoTemp = "";
                    decimal NumeroTemp = 0;
                    bool EsDecimal = false;
                    for (int i = 0; i < iteracion; i++)
                    {
                        foreach (e_ParametrosWF PM in Metodo.Parametros)
                        {
                            try
                            {
                                PM.Valor = PM.ValorM[i].ValorMulti;
                            }
                            catch (Exception)
                            {
                                PM.Valor = PM.Valor; // esto no hace nada pero explica que intenta asignar un valor
                            }
                        }
                        if (Metodo.IdParametroAccion != 0)
                        {
                            if (Metodo.AcumulaSalida == true)
                            {
                                resultadoTemp = MD.Accionar(Metodo, idUsuario);
                                EsDecimal = Decimal.TryParse(resultadoTemp, out NumeroTemp);
                                if (EsDecimal)
                                {
                                    if (resultado == "") resultado = "0";
                                    NumeroTemp = decimal.Parse(resultado) + NumeroTemp;
                                    resultado = NumeroTemp.ToString();
                                }
                                else
                                {
                                    resultado += resultadoTemp;
                                }
                            }
                            else
                            {
                                resultado = MD.Accionar(Metodo, idUsuario);
                            }
                            MD.ActualizarValorParametro(Metodo.IdParametroAccion.ToString(), resultado, idUsuario);
                            e_Metodo M2 = new e_Metodo();
                            var a = Accion.Metodos.FirstOrDefault(x => x.Parametros.Any(y => y.IdParametroAccion == Metodo.IdParametroAccion));
                            var b = a.Parametros.FirstOrDefault(x => x.IdParametroAccion == Metodo.IdParametroAccion);
                            b.Valor = resultado;
                            List<e_ValorMultipleValor> VMS = new List<e_ValorMultipleValor>();
                            e_ValorMultipleValor VM = new e_ValorMultipleValor();
                            string[] valores = b.Valor.Split(';');
                            foreach (string str in valores)
                            {
                                VM = new e_ValorMultipleValor();
                                VM.ValorMulti = str;
                                VM.TipoParametro = b.TipoParametro;
                                VMS.Add(VM);
                            }
                            b.ValorM = VMS;
                        }
                        else
                        {
                            if (Metodo.AcumulaSalida == true)
                            {
                                resultadoTemp = MD.Accionar(Metodo, idUsuario);
                                EsDecimal = Decimal.TryParse(resultadoTemp, out NumeroTemp);
                                if (EsDecimal)
                                {
                                    if (resultado == "") resultado = "0";
                                    NumeroTemp = decimal.Parse(resultado) + NumeroTemp;
                                    resultado = NumeroTemp.ToString();
                                }
                                else
                                {
                                    resultado += resultadoTemp;
                                }
                            }
                            else
                            {
                                resultado = MD.Accionar(Metodo, idUsuario);
                            }
                        }
                    }
                }
                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }
        }

        public static string[] CargarDDLPreguntas(DropDownList DD, string TBL, string IdUsuario)
        {
            string[] Mensaje = { "error", "" };
            try
            {
                string SQL = "select ";
                string ID = DD.ID.Substring(3);
                ID = ID.Replace("0", string.Empty);
                SQL += ID;
                SQL += ", Pregunta from " + TBL;
                if (ID.ToUpper() != "NOMBRE")
                {
                    SQL += " order by idPregunta";
        }
                DD.DataTextField = "Pregunta";
                DD.DataSource = n_ConsultaDummy.GetDataSet(SQL, IdUsuario);
                // le quita el ddlid
                DD.DataValueField = ID;
                DD.DataBind();
                DD.Items.Insert(0, new ListItem("--Seleccionar--"));
                ///Aqui se debe seleccionar la compania por defecto que tenga el usuario asignada, y si es ID=BASE,
                return Mensaje;
            }
            catch (Exception ex)
            {
                Mensaje[1] = "error, Ops! Ha ocurrido un Error, Codigo: TID-NE-SMM-000010" + ex.Message;
                return Mensaje;
            }
        }

        public static string[] CargarDDLRespuestas(DropDownList DD, string TBL, string IdUsuario)
        {
            string[] Mensaje = { "error", "" };
            try
            {
                string SQL = "select ";
                string ID = DD.ID.Substring(3);
                ID = ID.Replace("0", string.Empty);
                SQL += ID;
                SQL += ", Respuesta from " + TBL;
                if (ID.ToUpper() != "NOMBRE")
                {
                    SQL += " order by idRespuesta";
                }
                DD.DataTextField = "Respuesta";
                DD.DataSource = n_ConsultaDummy.GetDataSet(SQL, IdUsuario);
                // le quita el ddlid
                DD.DataValueField = ID;
                DD.DataBind();
                DD.Items.Insert(0, new ListItem("--Seleccionar--"));
                ///Aqui se debe seleccionar la compania por defecto que tenga el usuario asignada, y si es ID=BASE,
                return Mensaje;
            }
            catch (Exception ex)
            {
                Mensaje[1] = "error, Ops! Ha ocurrido un Error, Codigo: TID-NE-SMM-000010" + ex.Message;
                return Mensaje;
            }
        }

        public static void LimpiarForm(Control Contenedor)
        {
            try
            {
                foreach (Control c in Contenedor.Controls)
                {
                    if (c.GetType().ToString().Equals("System.Web.UI.UpdatePanel"))
                    {
                        foreach (Control cc in c.Controls)
                        {
                            foreach (Control ccc in cc.Controls)
                            {
                                if (ccc is TextBox)
                                {
                                    TextBox TxtB = (TextBox)ccc;
                                    TxtB.Text = "";
                                }

                                if (ccc is DropDownList)
                                {
                                    DropDownList DdlB = (DropDownList)ccc;
                                    DdlB.SelectedIndex = 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                               
            }
        }

      }

   
}

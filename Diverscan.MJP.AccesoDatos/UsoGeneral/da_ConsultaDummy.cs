using System;
using Diverscan.MJP.Entidades;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Diverscan.MJP.AccesoDatos.UsoGeneral
{
    public class da_ConsultaDummy
    {
        //TraceID.(2016). UsoGeneral/da_ConsultaDummy.En Trace ID Codigos documentados(25).Costa Rica:Grupo Diverscan. 

        //static string ConnStringEncriptado = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;
        //static clEncriptar clE = new clEncriptar();
        //static string ConnString = clE.Desencriptar(ConnStringEncriptado);

        public static void CargarDropDown(DropDownList DDL , string SQL , string NombreValorDB )
        {
                DDL.Items.Clear();
                DataSet DSBaseDatos = new DataSet();
                DSBaseDatos = GetDataSet(SQL, "0");
                if (DSBaseDatos.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dsRowEvento in DSBaseDatos.Tables[0].Rows)
                    {
                        string name = dsRowEvento[NombreValorDB].ToString();
                        DDL.Items.Add(name);
                    }

                    DDL.Items.Insert(0, new ListItem("--Seleccionar--"));
                }
        }

        public static DataSet GetDataSet(string Vista, TextBox KEY, string idUsuario)
        {
            try
            {
                //SqlConnection conn = new SqlConnection(ConnString);
                String ConnString = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(ConnString);

                string SQL = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'idCompania' AND TABLE_NAME = '" + Vista + "'";
                string ExisteCompania = GetUniqueValue(SQL, idUsuario);

                string query = "SELECT * FROM " + Vista + " WHERE 1=1 ";

                if (!string.IsNullOrEmpty(ExisteCompania))
                {
                    SQL = "SELECT " + e_TblUsuarios.IdCompania() + " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblUsuarios() + " WHERE " + e_TblUsuarios.IdUsario() + " = '" + idUsuario + "'";
                    string idCompania = GetUniqueValue(SQL, idUsuario);

                    query += " AND idCompania = '" + idCompania + "'";
                }

                if (KEY.Text != "") 
                {
                    //query += " where " + KEY.ID + " = '" + KEY.Text + "'";
                    query += " AND " + KEY.ID + " = '" + KEY.Text + "'";
                }

                if (Vista == "Vista_Calendarioanden")
                    query += " order by txtIdCalendarioanden desc";
                
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(query, conn);

                DataSet myDataSet = new DataSet();

                conn.Open();
                try
                {
                    adapter.Fill(myDataSet);
                }
                finally
                {
                    conn.Close();
                }

                return myDataSet;
            }
            catch (Exception)
            {
                return null;
               
            }
           
        }

        public static DataSet GetDataSet(string Query, string idUsuario)
        {
            String ConnString = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnString);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(Query, conn);

                adapter.SelectCommand.CommandTimeout = 2000;

                DataSet myDataSet = new DataSet();

                conn.Open();
                try
                {
                    adapter.Fill(myDataSet);
                }
                finally
                {
                    conn.Close();
                }

                return myDataSet;
            }
            catch (Exception)
            {
                //TraceID.(2016). UsoGeneral/da_ConsultaDummy.En Trace ID Codigos documentados(26).Costa Rica:Grupo Diverscan. 

                return null;

            }

        }

        public static DataSet GetDataSet2(string Query, string idUsuario)
        {
            String ConnString = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnString);
            try
            {
                string query2 = Query;

                SqlDataAdapter adapter2 = new SqlDataAdapter();
                adapter2.SelectCommand = new SqlCommand(query2, conn);

                DataSet myDataSet2 = new DataSet();
                adapter2.SelectCommand.CommandTimeout = 2000;

                conn.Open();
                try
                {
                    adapter2.Fill(myDataSet2);
                }
                finally
                {
                    conn.Close();
                }

                return myDataSet2;
            }
            catch (Exception)
            {
                //TraceID.(2016). UsoGeneral/da_ConsultaDummy.En Trace ID Codigos documentados(27).Costa Rica:Grupo Diverscan. 

                return null;

            }

        }

        public static DataSet GetDataSet3(string Query, string idUsuario)
        {
            String ConnString = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnString);
            try
            {
                string query3 = Query;

                SqlDataAdapter adapter3 = new SqlDataAdapter();
                adapter3.SelectCommand = new SqlCommand(query3, conn);

                DataSet myDataSet3 = new DataSet();

                conn.Open();
                try
                {
                    adapter3.Fill(myDataSet3);
                }
                finally
                {
                    conn.Close();
                }

                return myDataSet3;
            }
            catch (Exception)
            {
                //TraceID.(2016). UsoGeneral/da_ConsultaDummy.En Trace ID Codigos documentados(28).Costa Rica:Grupo Diverscan. 

                return null;

            }

        }

        public static string GetUniqueValue(string Query, string idUsuario)
        {
            String ConnString = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnString);
            try
            {
                string query = Query;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(query, conn);
                adapter.SelectCommand.CommandTimeout = 2000;

                DataSet myDataSet = new DataSet();

                conn.Open();
                try
                {
                    adapter.Fill(myDataSet);
                }

                finally
                {
                    conn.Close();
                }

                return myDataSet.Tables[0].Rows[0][0].ToString();
                }

                catch (Exception)
                {
                    return "";

                }

        }


        public static bool PushData(string SQL, string idUsuario)
        {
            string ConnString = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL, conn);
                cmd.CommandTimeout = 2000;
                cmd.ExecuteNonQuery();
                return true;
            }

            catch (System.Data.SqlClient.SqlException)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }

        }
    
    }
}

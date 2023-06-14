using Diverscan.MJP.Entidades.OPESALPreDetalleSolicitud;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.OPESALPreDetalleSolicitud
{
    public class da_OPESALPreDetalleSolicitud
    {
        public e_OPESALPreDetalleSolicitudArticulo GetDetallesArticuloPorIdInterno(string IdCompania, string IdInternoArticulo, decimal CantidadProducto, string gtin)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                try
                {
                    e_OPESALPreDetalleSolicitudArticulo detalleArticulo = new e_OPESALPreDetalleSolicitudArticulo();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SPObtenerDetallesArticuloPorIdInterno";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PIdCompania", IdCompania);
                        cmd.Parameters.AddWithValue("@PIdInternoArticulo", IdInternoArticulo);
                        cmd.Parameters.AddWithValue("@PCantidadProducto", CantidadProducto);
                        cmd.Parameters.AddWithValue("@PGtin", gtin);
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string IdInterno                    = reader["IdInterno"].ToString();
                                string NombreArticulo               = reader["NombreArticulo"].ToString();
                                string UnidadMedida                 = reader["UnidadMedida"].ToString();
                                decimal Contenido                   = decimal.Parse(reader["Contenido"].ToString());
                                string Empaque                      = reader["Empaque"].ToString();
                                bool EsGranel                       = bool.Parse(reader["EsGranel"].ToString());
                                int DiasMinimosVencimiento          = int.Parse(reader["DiasMinimosVencimiento"].ToString());
                                decimal UnidadesAlisto              = decimal.Parse(reader["UnidadesAlisto"].ToString());
                                decimal UnidadesAlistoInventario    = decimal.Parse(reader["UnidadesAlistoInventario"].ToString());
                                string UnidadesAlistoDetalle        = reader["UnidadAlistoDetalle"].ToString(); 
                                decimal CantidadUnidadMedida        = decimal.Parse(reader["CantidadUnidadMedida"].ToString());
                                decimal CantidadGtin = decimal.Parse(reader["CantidadGTIN"].ToString());
                                string Gtin = reader["GTIN"].ToString();
                                detalleArticulo = new e_OPESALPreDetalleSolicitudArticulo(IdInterno, NombreArticulo, UnidadMedida, Contenido, Empaque, EsGranel, DiasMinimosVencimiento, UnidadesAlisto, UnidadesAlistoInventario, UnidadesAlistoDetalle, CantidadUnidadMedida, CantidadGtin, Gtin);
                            }
                        }
                    }
                    return detalleArticulo;
                }
                catch 
                {
                    e_OPESALPreDetalleSolicitudArticulo detalleArticulo = new e_OPESALPreDetalleSolicitudArticulo();
                    return detalleArticulo;
                }
                finally
                { conn.Close(); }
            }
        }
    }
}

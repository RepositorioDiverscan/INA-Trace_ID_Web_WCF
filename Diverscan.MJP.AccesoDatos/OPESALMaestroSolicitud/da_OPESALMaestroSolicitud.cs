using Diverscan.MJP.Entidades.OPESALMaestroSolicitud;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.OPESALMaestroSolicitud
{
    public class da_OPESALMaestroSolicitud
    {
        public List<e_OPESALMaestroSolicitud> GetListOPESALMaestroSolicitud(string prefix, string IdCompania)
        {
            List<e_OPESALMaestroSolicitud> ListMaestroSolicitud = new List<e_OPESALMaestroSolicitud>();
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select * from View_listMaestroSolicitud where IdCompania = @IdCompania AND Nombre like + '%' + @SearchText + '%' or idInterno like + '%' + @SearchText + '%' or idInternoSAP like + '%' + @SearchText + '%' or idMaestroSolicitud like + '%' + @SearchText + '%'  or DestinoNombre like + '%' + @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@IdCompania", IdCompania);
                        cmd.Parameters.AddWithValue("@SearchText", prefix);
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                long idMaestroSolicitud = long.Parse(reader["idMaestroSolicitud"].ToString());
                                string UsuarioNombre = reader["UsuarioNombre"].ToString();
                                DateTime FechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
                                string Nombre = reader["Nombre"].ToString();
                                string Comentarios = reader["Comentarios"].ToString();
                                string idCompania = reader["IdCompania"].ToString();
                                string DestinoNombre = reader["DestinoNombre"].ToString();
                                string idInterno = reader["idInterno"].ToString();
                                string idInternoSAP = reader["idInternoSAP"].ToString();
                                int prioridad = Convert.ToInt32(reader["Prioridad"].ToString());
                                DateTime fechaEntrega = DateTime.Parse(reader["FechaEntrega"].ToString());
                                string porcetajeAlisto = reader["PorcentajeAlistado"].ToString();

                                ListMaestroSolicitud.Add(new e_OPESALMaestroSolicitud(idMaestroSolicitud, UsuarioNombre, FechaCreacion, Nombre, Comentarios, idCompania, DestinoNombre, idInterno, idInternoSAP, prioridad, fechaEntrega, porcetajeAlisto));
                            }
                        }
                        conn.Close();
                    }
                }
                return ListMaestroSolicitud;
            }
            catch (Exception)
            {
                return ListMaestroSolicitud;
            }
        }

        public List<e_OPESALMaestroSolicitud> GetMaestroArticulos(string IdCompania)
        {
            List<e_OPESALMaestroSolicitud> ListMaestroSolicitud = new List<e_OPESALMaestroSolicitud>();
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["MJPConnectionString"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select * from View_listMaestroSolicitud where IdCompania = @IdCompania";
                        cmd.Parameters.AddWithValue("@IdCompania", IdCompania);
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                long idMaestroSolicitud = long.Parse(reader["idMaestroSolicitud"].ToString());
                                string UsuarioNombre = reader["UsuarioNombre"].ToString();
                                DateTime FechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
                                string Nombre = reader["Nombre"].ToString();
                                string Comentarios = reader["Comentarios"].ToString();
                                string idCompania = reader["IdCompania"].ToString();
                                string DestinoNombre = reader["DestinoNombre"].ToString();
                                string idInterno = reader["idInterno"].ToString();
                                string idInternoSAP = reader["idInternoSAP"].ToString();
                                int prioridad = Convert.ToInt32(reader["Prioridad"].ToString());
                                DateTime fechaEntrega = DateTime.Parse(reader["FechaEntrega"].ToString());
                                string porcentajeAlisto = reader["PorcentajeAlistado"].ToString();

                                ListMaestroSolicitud.Add(new e_OPESALMaestroSolicitud(idMaestroSolicitud, UsuarioNombre, FechaCreacion, Nombre, Comentarios, idCompania, DestinoNombre, idInterno, idInternoSAP, prioridad, fechaEntrega, porcentajeAlisto));
                            }
                        }
                        conn.Close();
                    }
                }
                return ListMaestroSolicitud;
            }
            catch (Exception)
            {
                return ListMaestroSolicitud;
            }
        }

        public List<e_OPESALMaestroSolicitud> GetOrdersToEnlist(
            int idInternoWarehouse, DateTime dateInit, DateTime dateEnd, string idInternoOrder)//
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_GetOrdersToEnlist");

            //dbTse.AddInParameter(dbCommand, "@idInternoOrder", DbType.String, idInternoOrder);
            dbTse.AddInParameter(dbCommand, "@dateInit", DbType.DateTime, dateInit);
            dbTse.AddInParameter(dbCommand, "@dateEnd", DbType.DateTime, dateEnd);
            dbTse.AddInParameter(dbCommand, "@idWareHouse", DbType.Int32, idInternoWarehouse);
            dbCommand.CommandTimeout = 300;
            var reader = dbTse.ExecuteReader(dbCommand);
            List<e_OPESALMaestroSolicitud> ordersToEnlist = new List<e_OPESALMaestroSolicitud>();
            while (reader.Read())
            {
                e_OPESALMaestroSolicitud order = new e_OPESALMaestroSolicitud(reader);

                ordersToEnlist.Add(order);
            }

            return ordersToEnlist;

        }

        public List<EDetalleSalidaArticuloSector> GetDetalleSalidaArticulosSector(int idBodega, int idMaestroSolicitud)
        {

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenieneSectorPorNumeroOrdenPrueba");
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);
            dbTse.AddInParameter(dbCommand, "@idMaestroOrdenSalida", DbType.Int32, idMaestroSolicitud);

            List<EDetalleSalidaArticuloSector> ListDetalleMaestroSolicitud = new List<EDetalleSalidaArticuloSector>();

            var reader = dbTse.ExecuteReader(dbCommand);
            List<e_OPESALMaestroSolicitud> ordersToEnlist = new List<e_OPESALMaestroSolicitud>();
            while (reader.Read())
            {             
                EDetalleSalidaArticuloSector detailOrder = new EDetalleSalidaArticuloSector(reader);
                ListDetalleMaestroSolicitud.Add(detailOrder);
            }
            return ListDetalleMaestroSolicitud;
        }

        public string InsertarTareaAlistador(int idLineaDetalleSolicitud, int idUsuario, int idPrioridad)
        {
            string Resultado = "";

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("IngesarTareaAlistador");
            dbTse.AddInParameter(dbCommand, "@IdUsuario", DbType.Int32, idUsuario);
            dbTse.AddInParameter(dbCommand, "@IdLineaDetalleSolicitud", DbType.Int32, idLineaDetalleSolicitud);
            dbTse.AddInParameter(dbCommand, "@IdPrioridad", DbType.Int32, idPrioridad);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    Resultado = reader["Resultado"].ToString();
                }
            }

            return Resultado;
        }
        public string InsertarTareasAlistador(List<long> listaIdLineaDetalleSolicitud, int idUsuario, int idPrioridad)
        {
            var dataTable = listaIdLineaDetalleSolicitud.ToDataTablePrimitive("IdlineaDetallesolicitud");
            string Resultado = "";

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("IngesarTareasAlistador");
            dbTse.AddInParameter(dbCommand, "@IdUsuario", DbType.Int32, idUsuario);        
            dbTse.AddInParameter(dbCommand, "@IdPrioridad", DbType.Int32, idPrioridad);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ListaIdlineaDetallesolicitud";
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.Value = dataTable;
            dbCommand.Parameters.Add(parameter);


            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    Resultado = reader["Resultado"].ToString();
                }
            }

            return Resultado;
        }


        public string ActualizarTareaAlistador(int idLineaDetalleSolicitud, int idTareasUsuario)
        {
            string Resultado = "";

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ActualizarTareaAlistador");
            dbTse.AddInParameter(dbCommand, "@IdTareasUsuario", DbType.Int32, idTareasUsuario);
            dbTse.AddInParameter(dbCommand, "@IdLineaDetalleSolicitud", DbType.Int32, idLineaDetalleSolicitud);

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    Resultado = reader["Resultado"].ToString();
                }
            }

            return Resultado;
        }

        public List<ETareasUsuarioSolicitud> GetTareasPendientesPorUsuario(int idUsuario)
        {

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerTareasPendientesPorUsuario");
            dbTse.AddInParameter(dbCommand, "@IDUSUARIO", DbType.Int32, idUsuario);

            List<ETareasUsuarioSolicitud> ListTareasUsuarioSolicitud = new List<ETareasUsuarioSolicitud>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ETareasUsuarioSolicitud TareasUsuario = new ETareasUsuarioSolicitud(reader);
                    ListTareasUsuarioSolicitud.Add(TareasUsuario);
                }
            }
            return ListTareasUsuarioSolicitud;
        }

        public List<EPrioridadOrden> GetPrioridadOrden()
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_GetorderPriority");

            List<EPrioridadOrden> ListPrioridadOrden = new List<EPrioridadOrden>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    EPrioridadOrden ePrioridadOrden = new EPrioridadOrden(reader);
                    ListPrioridadOrden.Add(ePrioridadOrden);
                }

            }
            return ListPrioridadOrden;
        }

        public List<EDetalleSalidaOrdenUsuario> GetDetalleSalidaOrdenUsuario(int idUsuario, int idMaestroSalida)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("GetDetalleOrdenesSalidasXUsuario");
            dbTse.AddInParameter(dbCommand, "@p_idusuario", DbType.Int32, idUsuario);
            dbTse.AddInParameter(dbCommand, "@p_idMaestroSalida", DbType.Int32, idMaestroSalida);

            dbCommand.CommandTimeout = 300;
            List<EDetalleSalidaOrdenUsuario> ListDetalle = new List<EDetalleSalidaOrdenUsuario>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    EDetalleSalidaOrdenUsuario eDetalleSalidaOrdenUsuario = new EDetalleSalidaOrdenUsuario(reader);
                    ListDetalle.Add(eDetalleSalidaOrdenUsuario);
                }
            }
            return ListDetalle;
        }

        public List<e_OPESALMaestroSolicitud> GetOrders(
          int idInternoWarehouse, DateTime dateInit, DateTime dateEnd, string idInternoOrder)//
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_GetOrders");

            dbTse.AddInParameter(dbCommand, "@idInternoOrder", DbType.String, idInternoOrder);
            dbTse.AddInParameter(dbCommand, "@dateInit", DbType.DateTime, dateInit);
            dbTse.AddInParameter(dbCommand, "@dateEnd", DbType.DateTime, dateEnd);
            dbTse.AddInParameter(dbCommand, "@idWareHouse", DbType.Int32, idInternoWarehouse);
            dbCommand.CommandTimeout = 300;
            var reader = dbTse.ExecuteReader(dbCommand);
            List<e_OPESALMaestroSolicitud> ordersToEnlist = new List<e_OPESALMaestroSolicitud>();
            while (reader.Read())
            {
                e_OPESALMaestroSolicitud order = new e_OPESALMaestroSolicitud();

                order.IdMaestroSolicitud = long.Parse(reader["idMaestroSolicitud"].ToString());
                 order.FechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
                 order.Nombre = reader["Nombre"].ToString();
                 order.Comentarios = reader["Comentarios"].ToString();
                 order.IdDestino = Convert.ToInt32(reader["idDestino"].ToString());
                 order.DestinoNombre = reader["DestinoNombre"].ToString();
                 order.IdInterno = reader["idInterno"].ToString();
                 order.IdBodega = Convert.ToInt32(reader["idBodega"].ToString());
                 order.NombreBodega = reader["Bodega"].ToString();
                 order.PrioridadString = reader["PrioridadDescripcion"].ToString();
                 order.FechaEntrega = DateTime.Parse(reader["FechaEntrega"].ToString());
   
                ordersToEnlist.Add(order);
            }

            return ordersToEnlist;

        }

    }

}

using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SolicitudDeOla
{
    public class SolicituDeOlaDBA : ISolicitudDeOla
    {
        public void AprobarOla(List<int> ListadoOlasAprobadas)
        {
            foreach (var item in ListadoOlasAprobadas)
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_ActivarOlas");
                dbTse.AddInParameter(dbCommand, "@NumeroOla", DbType.Int32, item);
                dbTse.ExecuteNonQuery(dbCommand);
            }
        }
        
        public void InsertarOla(List<int> ListadoSolicitudes, string Observacion, int prioridad, int idBodega)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtieneIdRegistroOla");
            var resultado = dbTse.ExecuteScalar(dbCommand);
            int valorOla = Convert.ToInt32(resultado) + 1;
            IngresarMaestroOla(ListadoSolicitudes, Convert.ToInt32(valorOla), Observacion, prioridad, idBodega);
        }
        
        public void IngresarMaestroOla(List<int> ListadoSolicitudes, int registroOla, string Observacion, int prioridad, int idBodega)
        {
            foreach (var item in ListadoSolicitudes)
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_InsercionOlas");
                dbTse.AddInParameter(dbCommand, "@idRegistro", DbType.Int32, registroOla);
                dbTse.AddInParameter(dbCommand, "@idPreMaestroSolicitud", DbType.Int32, item);
                dbTse.AddInParameter(dbCommand, "@Observacion", DbType.String, Observacion);
                dbTse.AddInParameter(dbCommand, "@prioridad", DbType.Int32, prioridad);
                dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);
                dbTse.ExecuteNonQuery(dbCommand);
            }            
        }

        public void InsertarOlaCompleta(List<int> ListadoSolicitudes, string Observacion, int prioridad, int idBodega)
        {
            var dataTable = ListadoSolicitudes.ToDataTablePrimitive("IdPreMaestroSolicitud");

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InsercionOlasAgrupadas");
            dbTse.AddInParameter(dbCommand, "@Observacion", DbType.String, Observacion);
            dbTse.AddInParameter(dbCommand, "@prioridad", DbType.Int32, prioridad);
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ListaIdPreMaestroSolicitud";
            parameter.SqlDbType = System.Data.SqlDbType.Structured;
            parameter.Value = dataTable;
            dbCommand.Parameters.Add(parameter);

            dbTse.ExecuteNonQuery(dbCommand);
        }

        public void EliminarListadoPreMaestro(List<int> ListadoSolicitudes, int idBodega)
        {
            foreach (var item in ListadoSolicitudes)
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_EliminarPreMaestro_VP");
                dbTse.AddInParameter(dbCommand, "@idPreMaestroSolicitud", DbType.Int64, item);                
                dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);
                dbTse.ExecuteNonQuery(dbCommand);
            }
        }
        
        public List<E_ListadoPreMaestro> ObtenerListadoPreMaestro(int idBodega)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerListadoPreDetalle");
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);

            List<E_ListadoPreMaestro> ListaPreMaestroSolicitud = new List<E_ListadoPreMaestro>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    int idMaestroSolicitud = Convert.ToInt32(reader["idMaestroSolicitud"].ToString());
                    string nombre = reader["Nombre"].ToString();
                    string comentarios = reader["Comentarios"].ToString();
                    DateTime fechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
                    string Ruta = reader["Ruta"].ToString();
                    string Direccion = reader["Direccion"].ToString();
                    string nombreCliente = reader["NombreCliente"].ToString();

                    ListaPreMaestroSolicitud.Add(new E_ListadoPreMaestro(idMaestroSolicitud, nombre,
                        nombreCliente, comentarios, fechaCreacion, Ruta, Direccion));
                }
            }
            return ListaPreMaestroSolicitud;
        }
        
        public List<E_ListadoPreMaestro> ObtenerListadoPreMaestroFechas(DateTime fechaInicio, DateTime fechaFinal, int idBodega)  //  string ruta,
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerListadoPreDetalleBusqueda");
            dbTse.AddInParameter(dbCommand, "@fechaInicial", DbType.DateTime, fechaInicio);
            dbTse.AddInParameter(dbCommand, "@fechaFinal", DbType.DateTime, fechaFinal);
            //dbTse.AddInParameter(dbCommand, "@ruta", DbType.String, ruta);
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);

            List<E_ListadoPreMaestro> ListaPreMaestroSolicitud = new List<E_ListadoPreMaestro>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    int idMaestroSolicitud = Convert.ToInt32(reader["idMaestroSolicitud"].ToString());
                    string nombre = reader["Nombre"].ToString();
                    string comentarios = reader["Comentarios"].ToString();
                    DateTime fechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
                    string Ruta = reader["Ruta"].ToString();
                    string Direccion = reader["Direccion"].ToString();
                    string nombreCliente = reader["NombreCliente"].ToString();

                    ListaPreMaestroSolicitud.Add(new E_ListadoPreMaestro(idMaestroSolicitud, nombre,
                        nombreCliente, comentarios, fechaCreacion, Ruta, Direccion));
                }
            }
            return ListaPreMaestroSolicitud;
        }

        public List<E_ListadoOlasCreadas> ObtenerListadoOlas(int Estado, int idBodega)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerListadoDeCreadas");
            dbTse.AddInParameter(dbCommand, "@Estado", DbType.Int32, Estado);
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);


            List<E_ListadoOlasCreadas> ListaOlas = new List<E_ListadoOlasCreadas>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                 
                    int idRegistroOla = Convert.ToInt32(reader["idRegistroOla"].ToString());
                    string idPreMaestroSolicitud = reader["idPreMaestroSolicitud"].ToString();
                    string FechaIngreso =reader["FechaIngreso"].ToString();
                    string estado = reader["Estado"].ToString();
                    string Observaciones = reader["Observacion"].ToString();

                    ListaOlas.Add(new E_ListadoOlasCreadas(idRegistroOla, idPreMaestroSolicitud,
                        FechaIngreso, estado, Observaciones));
                }
            }
            return ListaOlas;
        }
        
        public List<E_ListadoOlasCreadas> ObtenerListadoOlasPendientesSeleccionadas(int idRegistro, int idBodega)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerListadoOlaPendienteEditar");
            dbTse.AddInParameter(dbCommand, "@idRegistroOla", DbType.Int32, idRegistro);
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);


            List<E_ListadoOlasCreadas> ListaOlas = new List<E_ListadoOlasCreadas>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {

                    int idRegistroOla = Convert.ToInt32(reader["idRegistroOla"].ToString());
                    string idPreMaestroSolicitud = reader["idPreMaestroSolicitud"].ToString();
                    string FechaIngreso = reader["FechaIngreso"].ToString();
                    string estado = reader["Estado"].ToString();
                    string Observacion =  reader["Observacion"].ToString();
                    ListaOlas.Add(new E_ListadoOlasCreadas(idRegistroOla, idPreMaestroSolicitud,
                        FechaIngreso, estado, Observacion));
                }
            }
            return ListaOlas;
        }
        
        public void EditarOla(List<int> ListadoOlas, int RegistroOla)
        {
            foreach (var item in ListadoOlas)
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_InsertarNuevaOla");
                dbTse.AddInParameter(dbCommand, "@idMaestroSolicitud", DbType.Int32, item);
                dbTse.AddInParameter(dbCommand, "@idRegistroOla", DbType.Int32, RegistroOla);
                dbTse.ExecuteNonQuery(dbCommand);
            }
        }
        
        public List<E_ListadoSolicitudesEliminarOla> ObtenerListadoSolicitudesEliminar(int idRegistroOla)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerListaEliminarOla");
            dbTse.AddInParameter(dbCommand, "@idRegistroOla", DbType.Int32, idRegistroOla);


            List<E_ListadoSolicitudesEliminarOla> ListaEliminarSoOlas = new List<E_ListadoSolicitudesEliminarOla>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {

                    int IdOla = Convert.ToInt32(reader["IdOla"].ToString());
                    int idPreMaestroSolicitud = Convert.ToInt32(reader["idPreMaestroSolicitud"].ToString());
                    string Nombre  = reader["Nombre"].ToString();
                    DateTime FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"].ToString());


                    ListaEliminarSoOlas.Add(new E_ListadoSolicitudesEliminarOla(IdOla, idPreMaestroSolicitud, 
                        Nombre, FechaIngreso));
                }
            }
            return ListaEliminarSoOlas;
        }
        
        public void EliminarSolicitudOla(List<int> ListadoSolicitudesEliminar, int Ola)
        {
            foreach (var item in ListadoSolicitudesEliminar)
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_EliminaSolicitdDeOla");
                dbTse.AddInParameter(dbCommand, "@idSolicitud", DbType.Int32, item);
                dbTse.AddInParameter(dbCommand, "@idOla", DbType.Int32, Ola);
                dbTse.ExecuteNonQuery(dbCommand);
            }
        }
        
        public List<E_ListadoRutas> ObtenerListadoRutas( int idBodega)
        {
            //SP_ObtenerListadoMaestroRutas
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerListadoMaestroRutas");
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);

            List<E_ListadoRutas> ListaPreMaestroSolicitud = new List<E_ListadoRutas>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                   
                    string Ruta = reader["Ruta"].ToString();



                    ListaPreMaestroSolicitud.Add(new E_ListadoRutas( Ruta));
                }
            }
            return ListaPreMaestroSolicitud;
        }
        
        public List<E_ListadoPreMaestro> ObtenerListadoPreMaestroRuta(string ruta, int idBodega)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerListadoPreDetalleBusquedaRuta");
            dbTse.AddInParameter(dbCommand, "@ruta", DbType.String, ruta);
            dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);

            List<E_ListadoPreMaestro> ListaPreMaestroSolicitud = new List<E_ListadoPreMaestro>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    int idMaestroSolicitud = Convert.ToInt32(reader["idMaestroSolicitud"].ToString());
                    string nombre = reader["Nombre"].ToString();                  
                    string comentarios = reader["Comentarios"].ToString();
                    DateTime fechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
                    string Ruta = reader["Ruta"].ToString();
                    string Direccion = reader["Direccion"].ToString();
                    string nombreCliente = reader["NombreCliente"].ToString();

                    ListaPreMaestroSolicitud.Add(new E_ListadoPreMaestro(idMaestroSolicitud, nombre,
                        nombreCliente,comentarios, fechaCreacion, Ruta, Direccion));
                }
            }
            return ListaPreMaestroSolicitud;
        }
        
        public List<E_ListadoDetallesMaestro> ObtenerListadoDetalleMaestro(int idMaestro)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ConsultaDetallePedido");
            dbTse.AddInParameter(dbCommand, "@idMaestro", DbType.Int32, idMaestro);

            List<E_ListadoDetallesMaestro> ListaDetalle = new List<E_ListadoDetallesMaestro>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    int idPreLineaDetalleSolicitud = Convert.ToInt32(reader["idPreLineaDetalleSolicitud"].ToString());
                    string nombre = reader["Nombre"].ToString();
                    string idInterno = reader["idInterno"].ToString();
                    decimal Cantidad =  Convert.ToDecimal(reader["Cantidad"].ToString());


                    ListaDetalle.Add(new E_ListadoDetallesMaestro(idPreLineaDetalleSolicitud,
                        nombre, idInterno, Cantidad));
                }
            }
            return ListaDetalle;
        }
        
        public int RevertirOla(int idMaestro)
        {
            int response = -1;
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("RevertirOla");
            dbTse.AddInParameter(dbCommand, "@idOla", DbType.Int32, idMaestro);
            dbTse.AddOutParameter(dbCommand, "@Response", DbType.Int32, 200);
          
            dbTse.ExecuteNonQuery(dbCommand);
            response = Convert.ToInt32(dbTse.GetParameterValue(dbCommand, "@Response").ToString());
            
            return response;
        }

    }
}

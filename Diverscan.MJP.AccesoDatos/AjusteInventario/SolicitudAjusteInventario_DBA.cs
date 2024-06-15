using Diverscan.MJP.AccesoDatos.TRAIngresoSalidaArticulos;
using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.AjusteInventario
{
    public class SolicitudAjusteInventario_DBA
    {
        public long InsertarSolicitudAjusteInventarioRecordYObtenerId(SolicitudAjusteInventarioRecord solicitudAjusteInventarioRecord)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InsertarSolicitudAjusteInventario");

            dbTse.AddInParameter(dbCommand, "@IdUsuario", DbType.Int32, solicitudAjusteInventarioRecord.IdUsuario);            
            dbTse.AddInParameter(dbCommand, "@IdAjusteInventario", DbType.Int64, solicitudAjusteInventarioRecord.IdAjusteInventario);            
            dbTse.AddInParameter(dbCommand, "@Estado", DbType.Int32, solicitudAjusteInventarioRecord.Estado);
            dbTse.AddInParameter(dbCommand, "@IdSolicitudAjusteInventarioRef", DbType.Int32, solicitudAjusteInventarioRecord.IdSolicitudAjusteInventarioRef);
            dbTse.AddOutParameter(dbCommand, "@Identity", DbType.Int64, 0);

            var result = dbTse.ExecuteNonQuery(dbCommand);
            long identity = Convert.ToInt64(dbCommand.Parameters["@Identity"].Value);

            return identity;
        }

        public int TieneCantidadDisponibleParaSalida(ArticuloXSolicitudAjusteRecord articuloXSolicitudAjusteRecord, long idAjusteInventario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("TieneCantidadDisponibleParaSalida_VP");

            //dbTse.AddInParameter(dbCommand, "@IdAjusteInventario", DbType.Int64, idAjusteInventario);
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, articuloXSolicitudAjusteRecord.IdArticulo);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, articuloXSolicitudAjusteRecord.Lote);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.DateTime, articuloXSolicitudAjusteRecord.FechaVencimientoAndroid);
            dbTse.AddInParameter(dbCommand, "@IdUbicacion", DbType.Int64, articuloXSolicitudAjusteRecord.IdUbicacionActual);        
            dbTse.AddOutParameter(dbCommand, "@Cantidad", DbType.Int32, articuloXSolicitudAjusteRecord.Cantidad);
            //dbTse.AddOutParameter(dbCommand, "@Resultado", DbType.String, 100);
            dbTse.ExecuteNonQuery(dbCommand);

            if (dbCommand.Parameters["@Cantidad"].Value == DBNull.Value)
            {
                return 0;
            }
            else
            {
                int result = Convert.ToInt32(dbCommand.Parameters["@Cantidad"].Value);
                return result;
            }
        }

        public int InsertarArticuloAjusteInventarioRecord(ArticuloXSolicitudAjusteRecord articuloXSolicitudAjusteRecord, long idSolicitudAjusteInventario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_InsertarRELArticuloXSolicitudAjuste");

            dbTse.AddInParameter(dbCommand, "@IdSolicitudAjusteInventario", DbType.Int64, idSolicitudAjusteInventario); 
            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, articuloXSolicitudAjusteRecord.IdArticulo);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, articuloXSolicitudAjusteRecord.Lote);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.DateTime, articuloXSolicitudAjusteRecord.FechaVencimiento);
            dbTse.AddInParameter(dbCommand, "@IdUbicacionActual", DbType.Int64, articuloXSolicitudAjusteRecord.IdUbicacionActual);
            dbTse.AddInParameter(dbCommand, "@IdUbicacionMover", DbType.Int64, articuloXSolicitudAjusteRecord.IdUbicacionMover);
            dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Int32, articuloXSolicitudAjusteRecord.Cantidad);

            var result = dbTse.ExecuteNonQuery(dbCommand);
            return result;
        }

        public int ActualizarCantidadArticuloSalidaAjusteInventarioRecord(ArticuloXSolicitudAjusteRecord articuloXSolicitudAjusteRecord, int IdUsuario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_SalidaTrazabilidad");

            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, articuloXSolicitudAjusteRecord.IdArticulo);
            dbTse.AddInParameter(dbCommand, "@Lote", DbType.String, articuloXSolicitudAjusteRecord.Lote);
            dbTse.AddInParameter(dbCommand, "@FechaVencimiento", DbType.DateTime, articuloXSolicitudAjusteRecord.FechaVencimiento);
            dbTse.AddInParameter(dbCommand, "@IdUbicacion", DbType.Int64, articuloXSolicitudAjusteRecord.IdUbicacionActual);
            dbTse.AddInParameter(dbCommand, "@Cantidad", DbType.Int32, articuloXSolicitudAjusteRecord.Cantidad);
            dbTse.AddInParameter(dbCommand, "@IdUsuario", DbType.Int32, IdUsuario);
            dbTse.AddInParameter(dbCommand, "@IdMetodoAccion", DbType.Int32, 8);

            int result = dbTse.ExecuteNonQuery(dbCommand);
            return result;
        }

        public int AplicarSolicitudAjusteAutomatico(long IdSolicitud)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_AplicarSolicitudAjusteAutomatico");

            dbTse.AddInParameter(dbCommand, "@IdSolicitudAjusteInventario", DbType.Int64, IdSolicitud);

            var result = dbTse.ExecuteNonQuery(dbCommand);
            return result;
        }


        public long InsertarSolicitudAjusteInventarioYObtenerIdSolicitud(SolicitudAjusteInventarioRecord solicitudAjusteInventarioRecord, List<ArticuloXSolicitudAjusteRecord> articuloXSolicitudAjusteRecord)
        {            
            var id = InsertarSolicitudAjusteInventarioRecordYObtenerId(solicitudAjusteInventarioRecord);
            if (id > 0)
            {
                foreach (var articulo in articuloXSolicitudAjusteRecord)
                {
                    InsertarArticuloAjusteInventarioRecord(articulo, id);
                }

                AplicarSolicitudAjusteAutomatico(id);

                return id;
            }
            return 0;
        }

        public string InsertarSolicitudAjusteInventarioYObtenerIdSolicitudHH(SolicitudAjusteInventarioRecord solicitudAjusteInventarioRecord, List<ArticuloXSolicitudAjusteRecord> articuloXSolicitudAjusteRecord)
        {
            bool tieneDisponible = true;
            var mensaje = "";
            foreach (var articulo in articuloXSolicitudAjusteRecord)
            {
                if (TieneCantidadDisponibleParaSalida(articulo, solicitudAjusteInventarioRecord.IdAjusteInventario) == 0)
                {
                    mensaje += articulo.NombreArticulo + " Cantidad no Disponible ----- ";
                    tieneDisponible = false;
                }
            }
            if (!tieneDisponible)
            {
                return mensaje;
            }

            var id = InsertarSolicitudAjusteInventarioRecordYObtenerId(solicitudAjusteInventarioRecord);
            if (id > 0)
            {
                foreach (var articulo in articuloXSolicitudAjusteRecord)
                {
                    InsertarArticuloAjusteInventarioRecord(articulo, id);
                }

                AplicarSolicitudAjusteAutomatico(id);

                foreach (var articulo in articuloXSolicitudAjusteRecord)
                {
                    ActualizarCantidadArticuloSalidaAjusteInventarioRecord(articulo, solicitudAjusteInventarioRecord.IdUsuario);
                }

                return "Registro Exitoso";
            }
            return "Problemas al registrar la solicitud";
        }

        public List<AjusteSolicitudRecord> GetSolicitudAjusteInventario( 
            DateTime fechaInicio, DateTime fechaFin, int estado, int idBodega)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerSolicitudAjusteInventario");

            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, fechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, fechaFin);
            dbTse.AddInParameter(dbCommand, "@Estado", DbType.Int32, estado);
            dbTse.AddInParameter(dbCommand, "@IdBodega", DbType.Int32, idBodega);

            List<AjusteSolicitudRecord> logList = new List<AjusteSolicitudRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    long idSolicitudAjusteInventario = long.Parse(reader["IdSolicitudAjusteInventario"].ToString());
                    DateTime fechaSolicitud = DateTime.Parse(reader["FechaSolicitud"].ToString());
                    string nombre = reader["Nombre"].ToString();
                    string apellidos = reader["Apellidos"].ToString();
                    string motivoAjuste = reader["MotivoInventario"].ToString();
                    bool tipoAjuste = bool.Parse(reader["TipoAjuste"].ToString());
                    string centroCosto = reader["CentroCosto"].ToString();
                    
                    logList.Add(new AjusteSolicitudRecord(idSolicitudAjusteInventario, fechaSolicitud, nombre, apellidos,
                    motivoAjuste, tipoAjuste, centroCosto));
                }
            }
            return logList;
        }

        public List<AjusteSolicitudRecord> GetSolicitudAjusteInventarioPorID(long idSolicitudAjusteInventario, int idBodega)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerSolicitudAjusteInventarioPorID");
            dbTse.AddInParameter(dbCommand, "@IdSolicitudAjusteInventario", DbType.Int64, idSolicitudAjusteInventario);
            dbTse.AddInParameter(dbCommand, "@IdBodega", DbType.Int32, idBodega);
            List<AjusteSolicitudRecord> logList = new List<AjusteSolicitudRecord>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    long IdSolicitudAjusteInventario = long.Parse(reader["IdSolicitudAjusteInventario"].ToString());
                    DateTime fechaSolicitud = DateTime.Parse(reader["FechaSolicitud"].ToString());
                    string nombre = reader["Nombre"].ToString();
                    string apellidos = reader["Apellidos"].ToString();
                    string motivoAjuste = reader["MotivoInventario"].ToString();
                    bool tipoAjuste = bool.Parse(reader["TipoAjuste"].ToString());
                    string centroCosto = reader["CentroCosto"].ToString();

                    logList.Add(new AjusteSolicitudRecord(idSolicitudAjusteInventario, fechaSolicitud, nombre, apellidos,
                        motivoAjuste, tipoAjuste, centroCosto));
                }
            }
            return logList;
        }

        public void UpdateSolicitudAjusteInventario(
            long idSolicitudAjusteInventario, int estado, long idCentroCosto, long idUsuario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_Update_SolicitudAjusteInventario");

            dbTse.AddInParameter(dbCommand, "@IdSolicitudAjusteInventario", DbType.Int64, idSolicitudAjusteInventario);
            dbTse.AddInParameter(dbCommand, "@Estado", DbType.Int32, estado);
            dbTse.AddInParameter(dbCommand, "@IdDestino", DbType.Int64, idCentroCosto);
            dbTse.AddInParameter(dbCommand, "@IdUsuario", DbType.Int64, idUsuario);
            var result = dbTse.ExecuteNonQuery(dbCommand);
        }

        public int GetIdSolicitudAjusteRefencia(int idSolicitudAjusteInventario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerIdSolicitudAjusteReferencia");
            dbTse.AddInParameter(dbCommand, "@IdSolicitudAjusteRefencia", DbType.Int64, idSolicitudAjusteInventario);
            // AjusteSolicitudRecord ajuste = new AjusteSolicitudRecord();
            int IdSolicitudAjusteInventario = -1;
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    IdSolicitudAjusteInventario = Int32.Parse(reader["IdSolicitudAjusteInventario"].ToString());
                }
            }            
            return IdSolicitudAjusteInventario;
        }

        public int GetLocationIdDevolutionState(bool state, int warehouse)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_GETLOCATION_DevolutionState");
            db.AddInParameter(dbCommand, "@p_state", DbType.Byte, state);
            db.AddInParameter(dbCommand, "@p_warehouse", DbType.Int32, warehouse);
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    return Convert.ToInt32(reader["RESULTADO"]);
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}

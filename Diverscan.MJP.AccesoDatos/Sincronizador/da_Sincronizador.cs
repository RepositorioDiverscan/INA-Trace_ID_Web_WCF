using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using Diverscan.MJP.Utilidades;
using Diverscan.MJP.Entidades.Rol;
using Diverscan.MJP.Entidades.Pedidos;

namespace Diverscan.MJP.AccesoDatos.Sincronizador
{
    public class da_Sincronizador
    {
        public List<e_Usuario> ObtenerUsuarios(string idbodega)
        {
            List<e_Usuario> _Usuarios = new List<e_Usuario>();

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerUsuariosSincronizador");

            dbTse.AddInParameter(dbCommand, "@p_IdBodega", DbType.Int32, Convert.ToInt32(idbodega));

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    e_Usuario usuario = CargarUsuarioLogin(reader);

                    _Usuarios.Add(usuario);
                }
            }
            return _Usuarios;
        }

        private static e_Usuario CargarUsuarioLogin(IDataReader reader)
        {
            try
            {
                var eUsuarios = new e_Usuario();
                eUsuarios.IdUsuario = reader["IDUSUARIO"].ToString();
                eUsuarios.Usuario = reader["USUARIO"].ToString();
                eUsuarios.Contrasenna = reader["CONTRASENNA"].ToString();
                eUsuarios.Bloqueado = Convert.ToBoolean(reader["ESTA_BLOQUEADO"]);
                eUsuarios.Nombre = reader["NOMBRE_PILA"].ToString();
                eUsuarios.Apellido = reader["APELLIDOS_PILA"].ToString();
                eUsuarios.idCompania = reader["IDCOMPANIA"].ToString();
                eUsuarios.IdRoles = reader["IDROL"].ToString();
                eUsuarios.IdBodega = Convert.ToInt32(reader["IdBodega"].ToString());
                bool trazableBodega = false;
                Boolean.TryParse(reader["trazable"].ToString(), out trazableBodega);
                eUsuarios.TrazableBodega = trazableBodega;
                return eUsuarios;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "ObtenerUsuariosSincronizador";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Sincronizador.cs", "ObtenerUsuarios()", "", lineaError, nombreProcedimiento, "", exError.Message);

                return null;
            }
        }

        public List<e_RolHH> ObtenerRoles()
        {
            List<e_RolHH> _RolHHs = new List<e_RolHH>();

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerRolesSincronizador");

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    e_RolHH rol = CargarRol(reader);

                    _RolHHs.Add(rol);
                }
            }
            return _RolHHs;
        }

        private static e_RolHH CargarRol(IDataReader reader)
        {
            try
            {
                
                int idRol = Convert.ToInt32(reader["idRol"].ToString());
                string FormHH = reader["FormHH"].ToString();

                var e_rol = new e_RolHH(idRol,FormHH);

                return e_rol;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "ObtenerRolesSincronizador";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Sincronizador.cs", "ObtenerRoles()", "", lineaError, nombreProcedimiento, "", exError.Message);

                return null;
            }
        }

        public List<e_PedidoSincronizador> ObtenerPedidos(string idbodega)
        {
            List<e_PedidoSincronizador> _PedidosSincronizador = new List<e_PedidoSincronizador>();

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerPedidosSincronizador");

            dbTse.AddInParameter(dbCommand, "@p_IdBodega", DbType.Int32, Convert.ToInt32(idbodega));

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    e_PedidoSincronizador pedido = CargarPedido(reader);

                    _PedidosSincronizador.Add(pedido);
                }
            }
            return _PedidosSincronizador;
        }

        private static e_PedidoSincronizador CargarPedido(IDataReader reader)
        {
            try
            {

                int idDespachoPedido = Convert.ToInt32(reader["IdDespachoPedido"].ToString());
                int idPedidoOriginal = Convert.ToInt32(reader["IdPedidoOriginal"].ToString());
                string destino = reader["Destino"].ToString();
                DateTime fechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
                DateTime fechaEntrega = DateTime.Parse(reader["FechaEntrega"].ToString());
                bool tipo = Convert.ToBoolean(reader["Tipo"].ToString());
                string nombreProfesor = reader["NombreProfesor"].ToString();
                int idTransportista = Convert.ToInt32(reader["IdEncargado"].ToString());
                string nombreTransportista = reader["NombreEncargado"].ToString();

                var e_pedido = new e_PedidoSincronizador(idDespachoPedido, idPedidoOriginal, destino, fechaCreacion, fechaEntrega, tipo, nombreProfesor,
                    idTransportista, nombreTransportista);

                return e_pedido;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "ObtenerPedidosSincronizador";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Sincronizador.cs", "ObtenerPedidos()", "", lineaError, nombreProcedimiento, "", exError.Message);

                return null;
            }
        }

        public List<e_DetallePedidoSincronizador> ObtenerDetallesPedidos(string idbodega)
        {
            List<e_DetallePedidoSincronizador> _DetallesPedidosSincronizador = new List<e_DetallePedidoSincronizador>();

            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("ObtenerDetallePedidosSincronizador");

            dbTse.AddInParameter(dbCommand, "@p_IdBodega", DbType.Int32, Convert.ToInt32(idbodega));

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    e_DetallePedidoSincronizador detallepedido = CargarDetalle(reader);

                    _DetallesPedidosSincronizador.Add(detallepedido);
                }
            }
            return _DetallesPedidosSincronizador;
        }

        private static e_DetallePedidoSincronizador CargarDetalle(IDataReader reader)
        {
            try
            {

                int idDetallePedidoOriginal = Convert.ToInt32(reader["idDetallePedidoOriginal"].ToString());
                int idPedidoOriginal = Convert.ToInt32(reader["idPedidoOriginal"].ToString());
                int idArticulo = Convert.ToInt32(reader["idArticulo"].ToString());
                string idArticuloInterno = reader["idArticuloInterno"].ToString();
                int CantidadOriginal = Convert.ToInt32(reader["CantidadOriginal"].ToString());
                int CantidadAlistada = Convert.ToInt32(reader["CantidadAlistada"].ToString());
                string ArticuloNombre = reader["ArticuloNombre"].ToString();
                string GTIN = reader["GTIN"].ToString();
                bool ConTrazabilidad = Convert.ToBoolean(reader["ConTrazabilidad"].ToString());

                var e_detallePedido = new e_DetallePedidoSincronizador(idDetallePedidoOriginal, idPedidoOriginal, idArticulo, idArticuloInterno,
                    CantidadOriginal, CantidadAlistada, ArticuloNombre, GTIN, ConTrazabilidad);

                return e_detallePedido;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "ObtenerPedidosSincronizador";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Sincronizador.cs", "ObtenerPedidos()", "", lineaError, nombreProcedimiento, "", exError.Message);

                return null;
            }
        }

        public string IngresarPedidosRecibidos(e_PedidoRecibido e_Pedido)
        {
            var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("InsertPedidoRecibido");
            db.AddInParameter(dbCommand, "@P_idPedido", DbType.Int32, e_Pedido.IdPedido);
            db.AddInParameter(dbCommand, "@P_fechaTerminado", DbType.DateTime, e_Pedido.FechaTerminado);
            db.AddInParameter(dbCommand, "@P_usuarioEntrega", DbType.Int32, e_Pedido.UsuarioEntrega);
            db.AddOutParameter(dbCommand, "@P_respuesta", DbType.String, 200);
            db.ExecuteNonQuery(dbCommand);

            string resultado = dbCommand.Parameters["@P_respuesta"].Value.ToString();
            return resultado;
        }

        public string IngresarDetallesRecibidos(e_DetalleRecibido e_Detalle, int idPedido)
        {
            var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("InsertDetallesRecibidos");
            db.AddInParameter(dbCommand, "@P_idPedido", DbType.Int32, idPedido);
            db.AddInParameter(dbCommand, "@P_idDetallePedidoOriginal", DbType.Int32, e_Detalle.IdDetallePedidoOriginal);
            db.AddInParameter(dbCommand, "@P_cantidadRecibida", DbType.Int32, e_Detalle.CantidadRecibida);
            db.AddInParameter(dbCommand, "@P_causaDevolucion", DbType.Int32, e_Detalle.CausaDevolucion);
            db.AddOutParameter(dbCommand, "@P_respuesta", DbType.String, 200);
            db.ExecuteNonQuery(dbCommand);

            string resultado = dbCommand.Parameters["@P_respuesta"].Value.ToString();
            return resultado;
        }

        public string RevertirPedidoRecibido(int idPedido)
        {
            var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("revertirPedidoRecibido");
            db.AddInParameter(dbCommand, "@P_idPedido", DbType.Int32, idPedido);
            db.AddOutParameter(dbCommand, "@P_respuesta", DbType.String, 200);
            db.ExecuteNonQuery(dbCommand);

            string resultado = dbCommand.Parameters["@P_respuesta"].Value.ToString();
            return resultado;
        }

    }

}

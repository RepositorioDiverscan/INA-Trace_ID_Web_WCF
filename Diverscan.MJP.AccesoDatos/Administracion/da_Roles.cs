using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.Administracion
{
    public class da_Roles
    {
        #region "Insertar Roles"

        public bool InsRoles(string sNombreBaseDatos, string sUsuario, e_Roles eRoles)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbTse.GetStoredProcCommand("insSegRoles");
                dbTse.AddInParameter(dbCommand, "@NombreRol", DbType.String, eRoles.NombreRol);
                dbTse.AddInParameter(dbCommand, "@wf_Usuario", DbType.Boolean, eRoles.wf_Usuario);
                dbTse.AddInParameter(dbCommand, "@wf_Default", DbType.Boolean, eRoles.wf_Default);
                dbTse.AddInParameter(dbCommand, "@wf_Requisicion", DbType.Boolean, eRoles.wf_Requisicion);
                dbTse.AddInParameter(dbCommand, "@wf_Alistos", DbType.Boolean, eRoles.wf_Alistos);
                dbTse.AddInParameter(dbCommand, "@wf_CargarEmbarque", DbType.Boolean, eRoles.wf_CargarEmbarque);
                dbTse.AddInParameter(dbCommand, "@wf_ConsultaAlistos", DbType.Boolean, eRoles.wf_ConsultaAlistos);
                dbTse.AddInParameter(dbCommand, "@wf_ConsultaRequisicion", DbType.Boolean, eRoles.wf_ConsultaRequisicion);
                dbTse.AddInParameter(dbCommand, "@wf_Credenciales", DbType.Boolean, eRoles.wf_Credenciales);
                dbTse.AddInParameter(dbCommand, "@wf_Custodia", DbType.Boolean, eRoles.wf_Custodia);
                dbTse.AddInParameter(dbCommand, "@wf_Despacho", DbType.Boolean, eRoles.wf_Despacho);
                dbTse.AddInParameter(dbCommand, "@wf_DetalleCompra", DbType.Boolean, eRoles.wf_DetalleCompra);
                dbTse.AddInParameter(dbCommand, "@wf_Devolucion", DbType.Boolean, eRoles.wf_Devolucion);
                dbTse.AddInParameter(dbCommand, "@wf_FamiliarYRequerimientos", DbType.Boolean, eRoles.wf_FamiliarYRequerimientos);
                dbTse.AddInParameter(dbCommand, "@wf_FormularioEmbarque", DbType.Boolean, eRoles.wf_FormularioEmbarque);
                dbTse.AddInParameter(dbCommand, "@wf_Inventario", DbType.Boolean, eRoles.wf_Inventario);
                dbTse.AddInParameter(dbCommand, "@wf_ListadoEmbarque", DbType.Boolean, eRoles.wf_ListadoEmbarque);
                dbTse.AddInParameter(dbCommand, "@wf_ListadoOrdenes", DbType.Boolean, eRoles.wf_ListadoOrdenes);
                dbTse.AddInParameter(dbCommand, "@wf_ListadoRequerimientos", DbType.Boolean, eRoles.wf_ListadoRequerimientos);
                dbTse.AddInParameter(dbCommand, "@wf_Localizacion", DbType.Boolean, eRoles.wf_Localizacion);
                dbTse.AddInParameter(dbCommand, "@wf_MantenimientoGeneral", DbType.Boolean, eRoles.wf_MantenimientoGeneral);
                dbTse.AddInParameter(dbCommand, "@wf_MenuAdministracion", DbType.Boolean, eRoles.wf_MenuAdministracion);
                dbTse.AddInParameter(dbCommand, "@wf_MenuConfiguracion", DbType.Boolean, eRoles.wf_MenuConfiguracion);
                dbTse.AddInParameter(dbCommand, "@wf_MenuIngresos", DbType.Boolean, eRoles.wf_MenuIngresos);
                dbTse.AddInParameter(dbCommand, "@wf_MenuSalidas", DbType.Boolean, eRoles.wf_MenuSalidas);
                dbTse.AddInParameter(dbCommand, "@wf_MenuUbicaciones", DbType.Boolean, eRoles.wf_MenuUbicaciones);
                dbTse.AddInParameter(dbCommand, "@wf_OrdenCompra", DbType.Boolean, eRoles.wf_OrdenCompra);
                dbTse.AddInParameter(dbCommand, "@wf_Programa", DbType.Boolean, eRoles.wf_Programa);
                dbTse.AddInParameter(dbCommand, "@wf_Reubicacion", DbType.Boolean, eRoles.wf_Reubicacion);
                dbTse.AddInParameter(dbCommand, "@wf_Usuarios", DbType.Boolean, eRoles.wf_Usuarios);
                dbTse.AddInParameter(dbCommand, "@wf_ValidaRequerimientos", DbType.Boolean, eRoles.wf_ValidaRequerimientos);


                dbTse.AddInParameter(dbCommand, "@wf_BandejaMaestroArticulo", DbType.Boolean, eRoles.wf_BandejaMaestroArticulo);
                dbTse.AddInParameter(dbCommand, "@wf_Empaque", DbType.Boolean, eRoles.wf_Empaque);
                dbTse.AddInParameter(dbCommand, "@wf_ImporatarExcel", DbType.Boolean, eRoles.wf_ImporatarExcel);
                dbTse.AddInParameter(dbCommand, "@wf_MaestroArticulo", DbType.Boolean, eRoles.wf_MaestroArticulo);
                dbTse.AddInParameter(dbCommand, "@wf_ReporteUbicacion", DbType.Boolean, eRoles.wf_ReporteUbicacion);

                dbTse.AddInParameter(dbCommand, "@wf_CentrolesPenales", DbType.Boolean, eRoles.wf_CentrolesPenales);
                dbTse.AddInParameter(dbCommand, "@wf_DetalleRequisicion", DbType.Boolean, eRoles.wf_DetalleRequisicion);
                dbTse.AddInParameter(dbCommand, "@wf_TrasladoProgramas", DbType.Boolean, eRoles.wf_TrasladoProgramas);

                dbTse.AddInParameter(dbCommand, "@rl_ManejoHH", DbType.Boolean, eRoles.rl_ManejoHH);
                
                

                dbTse.ExecuteNonQuery(dbCommand);

                return true;

            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "insSegRoles";
                const string listaParametros = "";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_ModAdministracionRoles.cs", "InsRoles()", sUsuario, lineaError, nombreProcedimiento, listaParametros, exError.Message);

                return false;
            }
        }

        #endregion

        #region "Editar Roles"

        public static bool UdpRoles(string sNombreBaseDatos, string sUsuario, e_Roles eRoles, Int32 idRol)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbTse.GetStoredProcCommand("updSegRoles");

                dbTse.AddInParameter(dbCommand, "@idRol", DbType.Int32, idRol);
                //dbTse.AddInParameter(dbCommand, "@NombreRol", DbType.String, eRoles.NombreRol);
                //dbTse.AddInParameter(dbCommand, "@RegistroManual", DbType.Boolean, eRoles.wf_Usuario);
                dbTse.AddInParameter(dbCommand, "@NombreRol", DbType.String, eRoles.NombreRol);
                dbTse.AddInParameter(dbCommand, "@wf_Usuario", DbType.Boolean, eRoles.wf_Usuario);
                dbTse.AddInParameter(dbCommand, "@wf_Default", DbType.Boolean, eRoles.wf_Default);
                dbTse.AddInParameter(dbCommand, "@wf_Requisicion", DbType.Boolean, eRoles.wf_Requisicion);
                dbTse.AddInParameter(dbCommand, "@wf_Alistos", DbType.Boolean, eRoles.wf_Alistos);
                dbTse.AddInParameter(dbCommand, "@wf_CargarEmbarque", DbType.Boolean, eRoles.wf_CargarEmbarque);
                dbTse.AddInParameter(dbCommand, "@wf_ConsultaAlistos", DbType.Boolean, eRoles.wf_ConsultaAlistos);
                dbTse.AddInParameter(dbCommand, "@wf_ConsultaRequisicion", DbType.Boolean, eRoles.wf_ConsultaRequisicion);
                dbTse.AddInParameter(dbCommand, "@wf_Credenciales", DbType.Boolean, eRoles.wf_Credenciales);
                dbTse.AddInParameter(dbCommand, "@wf_Custodia", DbType.Boolean, eRoles.wf_Custodia);
                dbTse.AddInParameter(dbCommand, "@wf_Despacho", DbType.Boolean, eRoles.wf_Despacho);
                dbTse.AddInParameter(dbCommand, "@wf_DetalleCompra", DbType.Boolean, eRoles.wf_DetalleCompra);
                dbTse.AddInParameter(dbCommand, "@wf_Devolucion", DbType.Boolean, eRoles.wf_Devolucion);
                dbTse.AddInParameter(dbCommand, "@wf_FamiliarYRequerimientos", DbType.Boolean, eRoles.wf_FamiliarYRequerimientos);
                dbTse.AddInParameter(dbCommand, "@wf_FormularioEmbarque", DbType.Boolean, eRoles.wf_FormularioEmbarque);
                dbTse.AddInParameter(dbCommand, "@wf_Inventario", DbType.Boolean, eRoles.wf_Inventario);
                dbTse.AddInParameter(dbCommand, "@wf_ListadoEmbarque", DbType.Boolean, eRoles.wf_ListadoEmbarque);
                dbTse.AddInParameter(dbCommand, "@wf_ListadoOrdenes", DbType.Boolean, eRoles.wf_ListadoOrdenes);
                dbTse.AddInParameter(dbCommand, "@wf_ListadoRequerimientos", DbType.Boolean, eRoles.wf_ListadoRequerimientos);
                dbTse.AddInParameter(dbCommand, "@wf_Localizacion", DbType.Boolean, eRoles.wf_Localizacion);
                dbTse.AddInParameter(dbCommand, "@wf_MantenimientoGeneral", DbType.Boolean, eRoles.wf_MantenimientoGeneral);
                dbTse.AddInParameter(dbCommand, "@wf_MenuAdministracion", DbType.Boolean, eRoles.wf_MenuAdministracion);
                dbTse.AddInParameter(dbCommand, "@wf_MenuConfiguracion", DbType.Boolean, eRoles.wf_MenuConfiguracion);
                dbTse.AddInParameter(dbCommand, "@wf_MenuIngresos", DbType.Boolean, eRoles.wf_MenuIngresos);
                dbTse.AddInParameter(dbCommand, "@wf_MenuSalidas", DbType.Boolean, eRoles.wf_MenuSalidas);
                dbTse.AddInParameter(dbCommand, "@wf_MenuUbicaciones", DbType.Boolean, eRoles.wf_MenuUbicaciones);
                dbTse.AddInParameter(dbCommand, "@wf_OrdenCompra", DbType.Boolean, eRoles.wf_OrdenCompra);
                dbTse.AddInParameter(dbCommand, "@wf_Programa", DbType.Boolean, eRoles.wf_Programa);
                dbTse.AddInParameter(dbCommand, "@wf_Reubicacion", DbType.Boolean, eRoles.wf_Reubicacion);
                dbTse.AddInParameter(dbCommand, "@wf_Usuarios", DbType.Boolean, eRoles.wf_Usuarios);
                dbTse.AddInParameter(dbCommand, "@wf_ValidaRequerimientos", DbType.Boolean, eRoles.wf_ValidaRequerimientos);
                dbTse.AddInParameter(dbCommand, "@wf_BandejaMaestroArticulo", DbType.Boolean, eRoles.wf_BandejaMaestroArticulo);
                dbTse.AddInParameter(dbCommand, "@wf_Empaque", DbType.Boolean, eRoles.wf_Empaque);
                dbTse.AddInParameter(dbCommand, "@wf_ImporatarExcel", DbType.Boolean, eRoles.wf_ImporatarExcel);
                dbTse.AddInParameter(dbCommand, "@wf_MaestroArticulo", DbType.Boolean, eRoles.wf_MaestroArticulo);
                dbTse.AddInParameter(dbCommand, "@wf_ReporteUbicacion", DbType.Boolean, eRoles.wf_ReporteUbicacion);
                dbTse.AddInParameter(dbCommand, "@wf_CentrolesPenales", DbType.Boolean, eRoles.wf_CentrolesPenales);
                dbTse.AddInParameter(dbCommand, "@wf_DetalleRequisicion", DbType.Boolean, eRoles.wf_DetalleRequisicion);
                dbTse.AddInParameter(dbCommand, "@wf_TrasladoProgramas", DbType.Boolean, eRoles.wf_TrasladoProgramas);
                dbTse.AddInParameter(dbCommand, "@rl_ManejoHH", DbType.Boolean, eRoles.rl_ManejoHH);
                dbTse.ExecuteNonQuery(dbCommand);

                return true;

            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "updSegRoles";
                const string listaParametros = "";

                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_ModAdministracionRoles.cs", "UdpRoles()", sUsuario, lineaError, nombreProcedimiento, listaParametros, exError.Message);

                return false;
            }
        }

        #endregion

        #region GetRoles

        public static List<e_Roles> GetRolesPorCriterio(string sNombreBaseDatos, string sUsuario, string criterio)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbTse.GetStoredProcCommand("GetRolesPorCriterio");


                dbTse.AddInParameter(dbCommand, "@Criterio", DbType.String, criterio + "%");
                var eRoles = new List<e_Roles>();

                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        eRoles.Add(CargaRoles(reader));
                    }
                }

                return eRoles;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "GetRolesPorCriterio";
                var listaParametros = sUsuario + " | " + criterio;
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Roles.cs", "GetRolesPorCriterio()", sUsuario, lineaError, nombreProcedimiento, listaParametros, exError.Message);

                return null;
            }

        }

        public static List<e_Roles> GetRolesPorNombre(string sNombreBaseDatos, string sUsuario, string nombre)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbTse.GetStoredProcCommand("GetRolesPorNombre");


                dbTse.AddInParameter(dbCommand, "@NombreRol", DbType.String, nombre);
                var eRoles = new List<e_Roles>();

                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        eRoles.Add(CargaRoles(reader));
                    }
                }

                return eRoles;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "GetRolesPorNombre";
                var listaParametros = sUsuario + " | " + nombre;
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Roles.cs", "GetRolesPorNombre()", sUsuario, lineaError, nombreProcedimiento, listaParametros, exError.Message);

                return null;
            }

        }

        public static string GetNombreRolPorUsuario(string sNombreBaseDatos, string sUsuario, string usuario)
        {
            try
            {
                var dbMJP = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbMJP.GetStoredProcCommand("GetNombreRolPorUsuario");//
                dbMJP.AddInParameter(dbCommand, "@IdUsuario", DbType.Int32, int.Parse(usuario));

                return (string)dbMJP.ExecuteScalar(dbCommand);
                
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "GetNombreRolPorUsuario";
                var listaParametros = sUsuario + " | " + usuario;
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_ModAdministracionRoles.cs", "GetNombreRolPorUsuario()", sUsuario, lineaError, nombreProcedimiento, listaParametros, exError.Message);

                return "";
            }
        }

        public static List<e_Roles> GetRoles(string sNombreBaseDatos, string sUsuario, e_Roles eRolesBsqueda)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbTse.GetStoredProcCommand("GetRoles");//

                var eRoles = new List<e_Roles>();

                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        eRoles.Add(CargaRoles(reader));
                    }
                }

                return eRoles;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "GetRoles";
                var listaParametros = eRolesBsqueda.NombreRol;
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_ModAdministracionRoles.cs", "GetRoles()", sUsuario, lineaError, nombreProcedimiento, listaParametros, exError.Message);

                return null;
            }

        }

        public static e_Roles GetRolesXid(string sNombreBaseDatos, string sUsuario, String idRol)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbTse.GetStoredProcCommand("GetRolesXId");

                dbTse.AddInParameter(dbCommand, "@IdRol", DbType.String, idRol);

                e_Roles eRoles = null;

                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        eRoles = CargaRoles(reader);
                    }
                }

                return eRoles;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "GetRolesXId";
                var listaParametros = idRol;
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_ModAdministracionRoles.cs", "GetRolesXid()", sUsuario, lineaError, nombreProcedimiento, listaParametros.ToString(), exError.Message);

                return null;
            }

        }

        /// <summary>
        /// Sobre carga del Metodo GetRolesXid para que acepte Int
        /// </summary>
        /// <param name="sNombreBaseDatos"></param>
        /// <param name="sUsuario"></param>
        /// <param name="idRol"></param>
        /// <returns></returns>
        public static e_Roles GetRolesXid(string sNombreBaseDatos, string sUsuario, Int32 idRol)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbTse.GetStoredProcCommand("GetRolesXId");

                dbTse.AddInParameter(dbCommand, "@IdRol", DbType.String, idRol);

                e_Roles eRoles = null;

                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        eRoles = CargaRoles(reader);
                    }
                }

                return eRoles;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "GetRolesXId";
                var listaParametros = idRol;
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_ModAdministracionRoles.cs", "GetRolesXid()", sUsuario, lineaError, nombreProcedimiento, listaParametros.ToString(), exError.Message);

                return null;
            }

        }


        #endregion

        #region "Eliminar Roles"

        public bool DelRol(string sNombreBaseDatos, string sUsuario, Int32 idRol)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase(sNombreBaseDatos);
                var dbCommand = dbTse.GetStoredProcCommand("delRol");

                dbTse.AddInParameter(dbCommand, "@idRol", DbType.Int32, idRol);

                dbTse.ExecuteNonQuery(dbCommand);

                return true;

            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "delRol";
                var listaParametros = idRol;

                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0) indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal); // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_ModAdministracionRoles.cs", "DelRol()", sUsuario, lineaError, nombreProcedimiento, listaParametros.ToString(), exError.Message);

                return false;
            }
        }

        #endregion

        #region "Utilidades"

        private static e_Roles CargaRoles(IDataReader reader)
        {
            try
            {
                var roles = new e_Roles
                                {
                                    idRol = reader["idRol"].ToString(),
                                    NombreRol = Convert.ToString(reader["NombreRol"]),
                                    wf_Usuario = Convert.ToBoolean(reader["wf_Usuario"]),
                                    wf_Default = Convert.ToBoolean(reader["wf_Default"]),

                                    wf_Custodia = Convert.ToBoolean(reader["wf_Custodia"]),
                                    wf_Devolucion = Convert.ToBoolean(reader["wf_Devolucion"]),
                                    wf_MenuIngresos = Convert.ToBoolean(reader["wf_MenuIngresos"]),
                                    wf_DetalleCompra = Convert.ToBoolean(reader["wf_DetalleCompra"]),
                                    wf_ListadoOrdenes = Convert.ToBoolean(reader["wf_ListadoOrdenes"]),
                                    wf_OrdenCompra = Convert.ToBoolean(reader["wf_OrdenCompra"]),
                                    wf_MenuAdministracion = Convert.ToBoolean(reader["wf_MenuAdministracion"]),
                                    wf_Usuarios = Convert.ToBoolean(reader["wf_Usuarios"]),
                                    wf_Credenciales = Convert.ToBoolean(reader["wf_Credenciales"]),
                                    wf_FamiliarYRequerimientos = Convert.ToBoolean(reader["wf_FamiliarYRequerimientos"]),
                                    wf_Programa = Convert.ToBoolean(reader["wf_Programa"]),
                                    wf_MenuSalidas = Convert.ToBoolean(reader["wf_MenuSalidas"]),
                                    wf_Alistos = Convert.ToBoolean(reader["wf_Alistos"]),
                                    wf_ConsultaAlistos = Convert.ToBoolean(reader["wf_ConsultaAlistos"]),
                                    wf_Despacho = Convert.ToBoolean(reader["wf_Despacho"]),
                                    wf_ConsultaRequisicion = Convert.ToBoolean(reader["wf_ConsultaRequisicion"]),
                                    wf_Requisicion = Convert.ToBoolean(reader["wf_Requisicion"]),
                                    wf_MenuConfiguracion = Convert.ToBoolean(reader["wf_MenuConfiguracion"]),
                                    wf_Inventario = Convert.ToBoolean(reader["wf_Inventario"]),
                                    wf_Localizacion = Convert.ToBoolean(reader["wf_Localizacion"]),
                                    wf_MantenimientoGeneral = Convert.ToBoolean(reader["wf_MantenimientoGeneral"]),
                                    wf_MenuUbicaciones = Convert.ToBoolean(reader["wf_MenuUbicaciones"]),
                                    wf_Reubicacion = Convert.ToBoolean(reader["wf_Reubicacion"]),
                                    wf_ListadoRequerimientos = Convert.ToBoolean(reader["wf_ListadoRequerimientos"]),
                                    wf_ValidaRequerimientos = Convert.ToBoolean(reader["wf_ValidaRequerimientos"]),
                                    wf_CargarEmbarque = Convert.ToBoolean(reader["wf_CargarEmbarque"]),
                                    wf_FormularioEmbarque = Convert.ToBoolean(reader["wf_FormularioEmbarque"]),
                                    wf_ListadoEmbarque = Convert.ToBoolean(reader["wf_ListadoEmbarque"]),

                                    wf_BandejaMaestroArticulo = Convert.ToBoolean(reader["wf_BandejaMaestroArticulo"]),
                                    wf_Empaque = Convert.ToBoolean(reader["wf_Empaque"]),
                                    wf_ImporatarExcel = Convert.ToBoolean(reader["wf_ImportaExcel"]),
                                    wf_MaestroArticulo = Convert.ToBoolean(reader["wf_MaestroArticulo"]),
                                    wf_ReporteUbicacion = Convert.ToBoolean(reader["wf_ReporteUbicacion"]),

                                    wf_CentrolesPenales = Convert.ToBoolean(reader["wf_CentrolesPenales"]),
                                    wf_DetalleRequisicion = Convert.ToBoolean(reader["wf_DetalleRequisicion"]),
                                    wf_TrasladoProgramas = Convert.ToBoolean(reader["wf_TrasladoProgramas"]),

                                    rl_ManejoHH = Convert.ToBoolean(reader["rl_ManejoHH"])
                                };
                return roles;
            }
            catch (Exception e)
            {
                var cl = new clErrores();
                cl.escribirError(e.Message, e.StackTrace);
                return null;
            }
        }

        #endregion

    }
}

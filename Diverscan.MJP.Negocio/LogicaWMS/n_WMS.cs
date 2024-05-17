using System;
using System.Globalization;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.AccesoDatos.UsoGeneral;
using Diverscan.MJP.AccesoDatos.MotorDecision;
using Diverscan.MJP.Negocio.MotorDecisiones;
using Diverscan.MJP.Negocio.Administracion;
using Diverscan.MJP.Negocio.UsoGeneral;
using Diverscan.MJP.Negocio.GS1;
using Diverscan.MJP.Negocio.OrdenCompa;
using Diverscan.MJP.Negocio.ProcesarSolicitud;
using Diverscan.MJP.Negocio.ProcesarRecepcionUbicacion;
using Diverscan.MJP.Negocio.Traslados;
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
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text.RegularExpressions;

namespace Diverscan.MJP.Negocio.LogicaWMS
{

    public class n_WMS
    {
        //e_Usuario UsrLogged = new e_Usuario();
        private static string idUsuario = ConfigurationManager.AppSettings["idUsuario"];
       
        #region ObtenerCompania

        public static string ObtenerCompaniaXUsuario(string idUsuario)
        {
            string SQL = "";
            string respuesta = "";
            try
            {
                SQL = "SELECT " + e_TblUsuarios.IdCompania() + " FROM [" + e_BaseDatos.NombreBD() + "].[" + e_BaseDatos.Esquema() + "].[" + e_TablasBaseDatos.TblUsuarios() + "] WHERE " + e_TblUsuarios.IdUsario() + " = '" + idUsuario + "'";
                respuesta = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;
            }
        }

        public static string ObtenerCompaniaXArticulo(string idArticulo, string idUsuario)
        {
            string SQL = "";
            string respuesta = "";
            try
            {
                SQL = "SELECT " + e_TblMaestroArticulosFields.idCompania() + " FROM " + e_TablasBaseDatos.TblMaestroArticulos() + " WHERE " + e_TblMaestroArticulosFields.idArticulo() + " = '" + idArticulo + "'";
                respuesta = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;
            }
        }

        public static string ObteneridDestinoBexim(string idArticulo, string idUsuario)
        {
            string SQL = "";
            string respuesta = "";
            try
            {
                SQL = "SELECT " + e_TblMaestroArticulosFields.idCompania() + " FROM " + e_TablasBaseDatos.TblMaestroArticulos() + " WHERE " + e_TblMaestroArticulosFields.idArticulo() + " = '" + idArticulo + "'";
                respuesta = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;
            }
        }

        public string getIdCompania(string idUsuario) 
        {
            return ObtenerCompaniaXUsuario(idUsuario);
        }

        #endregion ObtenerCompania

        #region ImprimirCodigo

        #region SolicitudAlmuerzoEvento

        //public static string SolicitudAlmuerzoEvento(string CodLeido) 
        //{
        //    DataSet DatosRegistro = new DataSet();
        //    CodLeido = CodLeido.Replace("-", "").Trim();
        
        //    string SqlUsuarioSol = " SELECT A.IdUsuario FROM [PROD_CONCAPAN].[dbo].[tbl_Usuario] A WHERE A.Identificacion = ";
        //    string respuesta = "Solicitud Denegada";
            
        //    try
        //    {




        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        
        //}
        #endregion SolicitudAlmuerzoEvento


        public void ProcesarSalidas(string CodLeido, string idCompania) 
        {
            string respuesta = "No Exitoso-";
            int Cant = 0;
            string[] Articulo = ObtenerIdArticuloNombreCodigoLeido_GS1128(CodLeido, idUsuario).Split(';');
            string idArticulo = Articulo[0];
            string NombreArticulo = Articulo[1];
            string FechaVencimiento = CargarEntidadesGS1.GS1128_DevolverFechaVencimiento(CodLeido);
            string Lote = CargarEntidadesGS1.GS1128_DevolveLote(CodLeido);
            string idZona = "";
            string Cantidad = CargarEntidadesGS1.GS1128_DevolverCantidad(CodLeido);  // código 37
            string Peso = CargarEntidadesGS1.GS1128_DevolverPeso(CodLeido);  // código 310
            string idUbicacion = CargarEntidadesGS1.GS1128_DevolveridUbicacion(CodLeido);
            //bool EsNum = int.TryParse(Cantidad, out Cant);
            int idBodega = ObtenerBodegaXarticulo(idArticulo, idUsuario);
            string SQL;
            string TablaOrigen = "";
            string IdCampo = "";
            string IdEstado = "";
            string SumUno_RestaCero = "";
            string ValorIdCampo = "";
            
            string idMetodoAccion = "28";

            string Zona_Recepcion = ConfigurationManager.AppSettings["Zona_Recepcion"].ToString();
            string Zona_Almacenamiento = ConfigurationManager.AppSettings["Zona_Almacenamiento"].ToString();
            string Zona_Picking = ConfigurationManager.AppSettings["Zona_Picking"].ToString();
            string Zona_Despacho = ConfigurationManager.AppSettings["Zona_Despacho"].ToString();

            SQL = "SELECT " + e_TblEstadoTransaccionalFields.BaseDatos() + "+ '.' +" + e_TblEstadoTransaccionalFields.idTabla() + " AS Tabla, ";
            SQL += e_TblEstadoTransaccionalFields.IdCampo() + ", " + e_TblEstadoTransaccionalFields.IdEstado() + ", " + e_TblEstadoTransaccionalFields.SumUno_RestaCero() + " FROM ";
            SQL += e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblEstadoTransaccional() + " WHERE " + e_TblEstadoTransaccionalFields.idMetodoAccion() + " = '" + idMetodoAccion + "'";
            DataSet DS1 = new DataSet();
            DS1 = n_ConsultaDummy.GetDataSet(SQL, idUsuario);

            if (DS1.Tables[0].Rows.Count > 0)
            {
                TablaOrigen = DS1.Tables[0].Rows[0]["Tabla"].ToString();
                IdCampo = DS1.Tables[0].Rows[0][e_TblEstadoTransaccionalFields.IdCampo()].ToString();

                IdEstado = DS1.Tables[0].Rows[0][e_TblEstadoTransaccionalFields.IdEstado()].ToString();
                SumUno_RestaCero = DS1.Tables[0].Rows[0][e_TblEstadoTransaccionalFields.SumUno_RestaCero()].ToString();

                SQL = "SELECT a." + e_TblDetalleTrasladoFields.idZona() + " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleTraslado() + " a ";
                SQL += " INNER JOIN " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTipoTraslado() + " b";
                SQL += " ON a." + e_TblDetalleTrasladoFields.idTipoTraslado() + " = b." + e_TblTipoTrasladoFields.idTipoTraslado() + " ";
                SQL += " WHERE b." + e_TblTipoTrasladoFields.idMetodoAccion() + " = '" + idMetodoAccion + "'";
                DataSet DS2 = new DataSet();
                DS2 = n_ConsultaDummy.GetDataSet(SQL, idUsuario);


                foreach (DataRow DR in DS2.Tables[0].Rows)
                {
                    idZona = DR[e_TblDetalleTrasladoFields.idZona()].ToString();

                    SQL = "SELECT " + e_TblZonasFields.Abreviatura() + " FROM " + e_TablasBaseDatos.TblZonas() + " WHERE " + e_TblZonasFields.idZona() + " = '" + idZona + "'";
                    string Abreviatura = n_ConsultaDummy.GetUniqueValue(SQL, idUbicacion);

                    if (string.IsNullOrEmpty(idUbicacion.Trim()))
                    {
                        idUbicacion = n_WMS.ObtenerUbicacionSugerida(Articulo[0], Cant, idUsuario, idZona);
                    }

                    ValorIdCampo = "";

                    //if (idZona != "1")
                    if (Abreviatura != Zona_Recepcion)
                    {
                        SQL = "SELECT " + e_TblTransaccionFields.idRegistro() +
                              "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + " ";
                        SQL += " WHERE " + e_TblTransaccionFields.FechaVencimiento() + " = '" + FechaVencimiento + "' ";
                        SQL += "       AND " + e_TblTransaccionFields.Lote() + " = '" + Lote + "' ";
                        SQL += "       AND " + e_TblTransaccionFields.idArticulo() + " = '" + idArticulo + "' ";
                        SQL += "       AND " + e_TblTransaccionFields.Cantidad() + " = '" + Cantidad + "' ";
                        SQL += "       AND " + e_TblTransaccionFields.idUbicacion() + " = '" + idUbicacion + "'";
                        SQL += "       AND " + e_TblTransaccionFields.idEstado() + " = '" + IdEstado + "'";
                        SQL += " ORDER BY " + e_TblTransaccionFields.idRegistro() + " DESC";
                        DataSet DS = new DataSet();
                        DS = n_ConsultaDummy.GetDataSet(SQL, idUsuario);
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            ValorIdCampo = "";
                        }
                        else
                        {
                            ValorIdCampo = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                        }
                    }
                }



            }

            bool Articuloenpicking = false;
            if (FechaVencimiento == "" && Lote == "")
            {
                SQL = "SELECT " + e_TblDetalleSolicitudFields.idLineaDetalleSolicitud() +
                        "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleSolicitud() +
                        "  WHERE " + e_TblDetalleSolicitudFields.idArticulo() + " = " + idArticulo +
                        "        AND " + e_TblDetalleSolicitudFields.Cantidad() + " = " + Cantidad +
                        "        AND " + e_TblDetalleSolicitudFields.IdCompania() + " = '" + idCompania + "'" +
                        "        AND CAST(" + e_TblDetalleSolicitudFields.idLineaDetalleSolicitud() + " AS NVARCHAR(10)) NOT IN (SELECT SUBSTRING(" + e_TblTransaccionFields.NumDocumentoAccion() + ",0,CHARINDEX('-'," + e_TblTransaccionFields.NumDocumentoAccion() + "))" +
                                                                                                                            "      FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                                                                                                            "      WHERE " + e_TblTransaccionFields.idTablaCampoDocumentoAccion() + " = '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleSolicitud() + "'" +
                                                                                                                            "            AND " + e_TblTransaccionFields.idCampoDocumentoAccion() + " =  '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleSolicitud() + ".idlineadetallesolicitud')";

                TablaOrigen = e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleSolicitud();
                ValorIdCampo = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);  // obtengo el idLineaDetalleSolicitud del artículo en el detalle de la solicitud.
            }
            else
            {
                ValorIdCampo = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
            }

            // intento insertar en la tabla TRA si hay disponibilidad del articulo en Picking.
            SQL = "SELECT " + e_TblZonasFields.idZona() +
                    "  FROM " + e_TablasBaseDatos.TblZonas() +
                    "  WHERE " + e_TblZonasFields.Abreviatura() + " = '" + Zona_Picking + "'";
            idZona = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

            List<e_Ubicacion> Ubicaciones = new List<e_Ubicacion>();
            Ubicaciones = ObtenerDisponibilidadArticulo(idArticulo, Cantidad, idUsuario, idZona);
            foreach (e_Ubicacion Ubic in Ubicaciones)
            {
                foreach (e_Articulo _Articulo in Ubic.Articulos)
                {
                    string FechaSQL = _Articulo.FechaVencimiento.Year.ToString() + "-" + _Articulo.FechaVencimiento.Month.ToString() + '-' + _Articulo.FechaVencimiento.Day.ToString();
                    if (Single.Parse(_Articulo.CantidadDisponible) > 0)
                    {
                        SQL = "";
                        SQL = "SELECT " + e_TblTransaccionFields.idRegistro() +
                                "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                "  WHERE " + e_TblTransaccionFields.idArticulo() + " = " + idArticulo +
                                "        AND " + e_TblTransaccionFields.Lote() + " = '" + _Articulo.Lote + "'" +
                                "        AND " + e_TblTransaccionFields.FechaVencimiento() + " = '" + FechaSQL + "'" +
                                "        AND " + e_TblTransaccionFields.Cantidad() + " = " + _Articulo.CantidadDisponible +
                                "        AND " + e_TblTransaccionFields.idMetodoAccion() + " = 59" +
                                "        AND " + e_TblTransaccionFields.idRegistro() + " NOT IN (SELECT SUBSTRING(" + e_TblTransaccionFields.NumDocumentoAccion() + ",CHARINDEX('-'," + e_TblTransaccionFields.NumDocumentoAccion() + ")+1,100)" +
                                                                                                    "      FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                                                                                    "      WHERE " + e_TblTransaccionFields.idArticulo() + " = " + idArticulo +
                                                                                                    "            AND " + e_TblTransaccionFields.Lote() + " = '" + _Articulo.Lote + "'" +
                                                                                                    "            AND " + e_TblTransaccionFields.FechaVencimiento() + " = '" + FechaSQL + "'" +
                                                                                                    "            AND " + e_TblTransaccionFields.Cantidad() + " = " + _Articulo.CantidadDisponible +
                                                                                                    "            AND " + e_TblTransaccionFields.idMetodoAccion() + " = 28)";
                        string ValorIdCampo2 = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);  //obtengo el idregistro del artículo que esta ubicado en 
                        //almacen o picking
                        if (string.IsNullOrEmpty(ValorIdCampo) || string.IsNullOrEmpty(ValorIdCampo2))
                        {
                            respuesta = "Registro de Solicitud o de ubicación no encontrado...";
                            Articuloenpicking = true;
                            break;
                        }

                        respuesta = TransaccionMD(idArticulo, _Articulo.CantidadDisponible, TablaOrigen, IdCampo, ValorIdCampo + "-" + ValorIdCampo2, "TABLA.INSERT", IdEstado, SumUno_RestaCero, idUsuario, idMetodoAccion, FechaSQL, _Articulo.Lote, Ubic.idUbicacion.ToString());
                        Articuloenpicking = true;
                        break;
                    }
                }

            }

            if (!Articuloenpicking)
            {
                // intento insertar en la tabla TRA si hay disponibilidad del articulo en almacén. 
                SQL = "SELECT " + e_TblZonasFields.idZona() +
                        "  FROM " + e_TablasBaseDatos.TblZonas() +
                        "  WHERE " + e_TblZonasFields.Abreviatura() + " = '" + Zona_Almacenamiento + "'";
                idZona = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                Ubicaciones.Clear();
                Ubicaciones = ObtenerDisponibilidadArticulo(idArticulo, Cantidad, idUsuario, idZona);
                foreach (e_Ubicacion Ubic in Ubicaciones)
                {
                    foreach (e_Articulo _Articulo in Ubic.Articulos)
                    {
                        string FechaSQL = _Articulo.FechaVencimiento.Year.ToString() + "-" + _Articulo.FechaVencimiento.Month.ToString() + '-' + _Articulo.FechaVencimiento.Day.ToString();

                        if (Single.Parse(_Articulo.CantidadDisponible) > 0)
                        {
                            SQL = "";
                            SQL = "SELECT " + e_TblTransaccionFields.idRegistro() +
                                    "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                    "  WHERE " + e_TblTransaccionFields.idArticulo() + " = " + idArticulo +
                                    "        AND " + e_TblTransaccionFields.Lote() + " = " + _Articulo.Lote +
                                    "        AND " + e_TblTransaccionFields.FechaVencimiento() + " = " + FechaSQL +
                                    "        AND " + e_TblTransaccionFields.Cantidad() + " = " + _Articulo.CantidadDisponible +
                                    "        AND " + e_TblTransaccionFields.idMetodoAccion() + " = 59" +
                                    "        AND " + e_TblTransaccionFields.idRegistro() + " NOT IN (SELECT SUBSTRING(" + e_TblTransaccionFields.NumDocumentoAccion() + ",CHARINDEX('-'," + e_TblTransaccionFields.NumDocumentoAccion() + ")+1,100)" +
                                                                                                        "      FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                                                                                        "      WHERE " + e_TblTransaccionFields.idArticulo() + " = " + idArticulo +
                                                                                                        "            AND " + e_TblTransaccionFields.Lote() + " = '" + _Articulo.Lote + "'" +
                                                                                                        "            AND " + e_TblTransaccionFields.FechaVencimiento() + " = '" + FechaSQL + "'" +
                                                                                                        "            AND " + e_TblTransaccionFields.Cantidad() + " = " + _Articulo.CantidadDisponible +
                                                                                                        "            AND " + e_TblTransaccionFields.idMetodoAccion() + " = 28)";
                            string ValorIdCampo2 = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);  //obtengo el idregistro del artículo que esta ubicado en 
                            //almacen o picking
                            if (string.IsNullOrEmpty(ValorIdCampo) || string.IsNullOrEmpty(ValorIdCampo2))
                            {
                                respuesta = "Registro de Solicitud o de ubicación no encontrado...";
                                break;
                            }
                            respuesta = TransaccionMD(idArticulo, _Articulo.CantidadDisponible, TablaOrigen, IdCampo, ValorIdCampo + "-" + ValorIdCampo2, "TABLA.INSERT", IdEstado, SumUno_RestaCero, idUsuario, idMetodoAccion, FechaSQL, _Articulo.Lote, Ubic.idUbicacion.ToString());
                            break;
                        }
                    }

                }
            }
        }


        public static string CrearSSCC(string CodLeido, string idUsuario)
        {
            string SQL = "";
            string Cantidad = "1";
            string CantidadSSCC = CodLeido;
            int Cant = 1;

            if (string.IsNullOrEmpty(CantidadSSCC.Trim()))
            {
                CantidadSSCC = "1";
            }

            bool EsNum = int.TryParse(CantidadSSCC, out Cant);

            string Respuesta = "";
            try
            {
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);

                for (int i = 0; i < Cant; i++)
                {
                    string sscc = "";
                    int VecesRepetir = 0;
                    string Espacios = "";
                    string CodigoCompania = "";
                    string Descripcion = "";
                    string TipoCodigo = "Código de rastreo interno";
                    string TipoEtiqueta = "Etiqueta SSCC";
                    string Estado = "0";
                    string CodigoEtiqueta = "";
                    string idCataLogo = "844";

                    SQL = "SELECT TOP 1 " + e_TblConsecutivosSSCCFields.idConsecutivoSSCC() + " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblConsecutivosSSCC() + " WHERE " + e_TblConsecutivosSSCCFields.idCompania() + " = '" + idCompania + "' ORDER BY " + e_TblConsecutivosSSCCFields.idConsecutivoSSCC() + " DESC";
                    string ConsecutivoSSCC = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                    Int64 Consecutivo = Convert.ToInt64(ConsecutivoSSCC) + 1;

                    ConsecutivoSSCC = Consecutivo.ToString();
                    Descripcion = "SSCC " + ConsecutivoSSCC;

                    SQL = "SELECT " + e_TblCompaniaFields.Codigo() + " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblCompania() + " WHERE " + e_TblCompaniaFields.IdCompania() + " = '" + idCompania + "'";
                    CodigoCompania = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                    VecesRepetir = 9 - ConsecutivoSSCC.Length;
                    Espacios = "";
                    Espacios = string.Concat(Enumerable.Repeat(0, VecesRepetir));
                    sscc = CodigoCompania + Espacios + ConsecutivoSSCC;

                    int resultado = CargarEntidadesGS1.GS1128_DigitoVerificadorSSCC(sscc);

                    sscc = sscc + resultado.ToString();

                    SQL = "INSERT INTO " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblConsecutivosSSCC() + " (" + e_TblConsecutivosSSCCFields.Descripcion();
                    SQL += ", " + e_TblConsecutivosSSCCFields.ConsecutivoSSCC() + ", " + e_TblConsecutivosSSCCFields.TipoCodigo() + ", " + e_TblConsecutivosSSCCFields.SSCCGenerado() + ", " + e_TblConsecutivosSSCCFields.idCompania();
                    SQL += ") VALUES ('" + Descripcion + "', '" + ConsecutivoSSCC + "', '" + TipoCodigo + "', '" + sscc + "', '" + idCompania + "')";
                    n_ConsultaDummy.PushData(SQL, idUsuario);

                    Respuesta = sscc;

                    if (!string.IsNullOrEmpty(Respuesta.Trim()))
                    {
                        CodigoEtiqueta = "(00)" + sscc;

                        SQL = "INSERT INTO " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblGestorImpresion() + " (" + e_TblGestorImpresionFields.CodigoInterno();
                        SQL += ", " + e_TblGestorImpresionFields.Estado() + ", " + e_TblGestorImpresionFields.Descripcion() + ", " + e_TblGestorImpresionFields.TipoEtiqueta();
                        SQL += ", " + e_TblGestorImpresionFields.Cantidad() + ", " + e_TblGestorImpresionFields.idUsuario() + ", " + e_TblGestorImpresionFields.CodigoEtiqueta();
                        SQL += ", " + e_TblGestorImpresionFields.idCataLogo() + ", " + e_TblGestorImpresionFields.idCompania();
                        SQL += ") VALUES ('" + sscc + "', '" + Estado + "', '" + Descripcion + "', '" + TipoEtiqueta + "', '" + Cantidad + "', '" + idUsuario + "', '" + CodigoEtiqueta + "', '" + idCataLogo + "', '" + idCompania + "')";
                        n_ConsultaDummy.PushData(SQL, idUsuario);
                    }//
                    else
                    {
                        Respuesta = "No Generado";
                    }
                }//for (int i = 0; i < Cant; i++)

                return Respuesta;
            }
            catch (Exception)
            {
                return Respuesta;
            }
        }

        private static string ImprimirCodigoGTIN14(string CodLeido, string idUsuario)
        {
            string SQL = "";

            string Respuesta = String.Empty;
            try
            {
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);

                string TipoEtiqueta = String.Empty;
                string idCataLogo = String.Empty;
                string Estado = "0";
                int Cantidad = (int)double.Parse(CargarEntidadesGS1.GS1128_DevolverCantidad(CodLeido));
                CodLeido = CodLeido.Substring(0, CodLeido.Length - 10);
                string CodigoEtiqueta = CodLeido.Substring(CodLeido.Length - 2);
                CodLeido = CodLeido.Substring(0, CodLeido.Length - 2);
                if (CodigoEtiqueta == "01")
                {
                    TipoEtiqueta = "Unidades de Distribución(Grande(4x3))";
                    idCataLogo = "842";
                }
                if (CodigoEtiqueta == "02")
                {
                    TipoEtiqueta = "Unidades de Distribución(Mediana(3x2))";
                    idCataLogo = "843";
                }
                if (CodigoEtiqueta == "03")
                {
                    TipoEtiqueta = "Unidades de Distribución(Pequeña(2x1))";
                    idCataLogo = "880";
                }
                SQL = "INSERT INTO " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblGestorImpresion() + " (" + e_TblGestorImpresionFields.CodigoInterno();
                SQL += ", " + e_TblGestorImpresionFields.Estado() + ", " + e_TblGestorImpresionFields.Descripcion() + ", " + e_TblGestorImpresionFields.TipoEtiqueta();
                SQL += ", " + e_TblGestorImpresionFields.Cantidad() + ", " + e_TblGestorImpresionFields.idUsuario() + ", " + e_TblGestorImpresionFields.CodigoEtiqueta() + ", " + e_TblGestorImpresionFields.idCataLogo();
                SQL += ", " + e_TblGestorImpresionFields.FechaVencimiento() + "," + e_TblGestorImpresionFields.Lote() + ", " + e_TblGestorImpresionFields.idCompania();
                SQL += ") VALUES ('" + CodLeido + "', '" + Estado + "', '" + CodLeido + "', '" + TipoEtiqueta + "', '" + Cantidad + "', '";
                SQL += idUsuario + "', '" + CodLeido + "', '" + idCataLogo + "', '" + "N/A" + "','" + "N/A" + "', '" + idCompania + "')";
                n_ConsultaDummy.PushData(SQL, idUsuario);
                Respuesta = "Se envio a imprimir el codigo " + CodLeido;
                return Respuesta;
            }
            catch (Exception)
            {
                return Respuesta;
            }
        }

        private static string ImprimirCodigoUbicacion(string CodLeido, string idUsuario, int Cantidad)
        {
            string SQL = "";
            string Respuesta = String.Empty;
            try
            {
                //TraceID.(2016). LogicaWMS/N_WMS.En Trace ID Codigos documentados(21).Costa Rica:Grupo Diverscan. 
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);

                string TipoEtiqueta = String.Empty;
                string idCataLogo = String.Empty;
                string Estado = "0";
                CodLeido = CodLeido.Substring(2, CodLeido.Length - 2);
                    TipoEtiqueta = "Unidades de Distribución(Grande(4x3))";
                    idCataLogo = "842";
                CodLeido = "(91)" + CodLeido;
                SQL = "INSERT INTO " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblGestorImpresion() + " (" + e_TblGestorImpresionFields.CodigoInterno();
                SQL += ", " + e_TblGestorImpresionFields.Estado() + ", " + e_TblGestorImpresionFields.Descripcion() + ", " + e_TblGestorImpresionFields.TipoEtiqueta();
                SQL += ", " + e_TblGestorImpresionFields.Cantidad() + ", " + e_TblGestorImpresionFields.idUsuario() + ", " + e_TblGestorImpresionFields.CodigoEtiqueta() + ", " + e_TblGestorImpresionFields.idCataLogo();
                SQL += ", " + e_TblGestorImpresionFields.FechaVencimiento() + "," + e_TblGestorImpresionFields.Lote() + ", " + e_TblGestorImpresionFields.idCompania();
                SQL += ") VALUES ('" + CodLeido + "', '" + Estado + "', '" + CodLeido + "', '" + TipoEtiqueta + "', '" + Cantidad + "', '";
                SQL += idUsuario + "', '" + CodLeido + "', '" + idCataLogo + "', '" + "N/A" + "','" + "N/A" + "', '" + idCompania + "')";
                n_ConsultaDummy.PushData(SQL, idUsuario);
                Respuesta = "Se envio a imprimir el codigo " + CodLeido;
                return Respuesta;
            }
            catch (Exception)
            {
                return Respuesta;
            }
        }

        private static string ImprimirCodigoConsumo(string CodLeido, string idUsuario, int Cantidad)
        {
            string SQL = "";
            string Respuesta = String.Empty;
            try
            {
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);

                EntidadesGS1.e_GTIN GTIN = new EntidadesGS1.e_GTIN();
                bool EsGTIN = CargarEntidadesGS1.GS1128_ObtenerGTIN(CodLeido, out GTIN);
                string[] Articulo = ObtenerIdArticuloNombreCodigoLeido_GS1128(CodLeido, idUsuario).Split(';');
                string idArticulo = Articulo[0];
                string NombreArticulo = Articulo[1];
                string FechaVencimiento = GS1.CargarEntidadesGS1.GS1128_DevolverFechaVencimiento(CodLeido);
                FechaVencimiento = FechaVencimiento.Replace("-", string.Empty).Substring(2);
                string Lote = GS1.CargarEntidadesGS1.GS1128_DevolveLote(CodLeido);
                string TipoEtiqueta = "Unidades de Consumo(Linea Doble)";
                string Estado = "0";
                string CodigoEtiqueta = "(01)" + "0" + GTIN.ValorLeido + "(17)" + FechaVencimiento + "(10)" + Lote;
                string idCataLogo = "841";
                SQL = "INSERT INTO " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblGestorImpresion() + " (" + e_TblGestorImpresionFields.CodigoInterno();
                SQL += ", " + e_TblGestorImpresionFields.Estado() + ", " + e_TblGestorImpresionFields.Descripcion() + ", " + e_TblGestorImpresionFields.TipoEtiqueta();
                SQL += ", " + e_TblGestorImpresionFields.Cantidad() + ", " + e_TblGestorImpresionFields.idUsuario() + ", " + e_TblGestorImpresionFields.CodigoEtiqueta() + ", " + e_TblGestorImpresionFields.idCataLogo();
                SQL += ", " + e_TblGestorImpresionFields.FechaVencimiento() + "," + e_TblGestorImpresionFields.Lote() + ", " + e_TblGestorImpresionFields.idCompania();
                SQL += ") VALUES ('" + CodLeido + "', '" + Estado + "', '" + NombreArticulo + "', '" + TipoEtiqueta + "', '" + ((int)Cantidad).ToString() + "', '";
                SQL += idUsuario + "', '" + CodigoEtiqueta + "', '" + idCataLogo + "', '" + FechaVencimiento + "','" + Lote + "', '" + idCompania + "')";
                n_ConsultaDummy.PushData(SQL, idUsuario);
                Respuesta = "Se envio a imprimir el codigo " + CodLeido;
                return Respuesta;
            }
            catch (Exception)
            {
                return Respuesta;
            }
        }

        public static string ImprimirCodigo(string CodLeido, string idUsuario)
        {
            string respuesta = string.Empty;
            try
            {
                if (CodLeido.Length > 10)
                {
                    int Cantidad = int.Parse(CargarEntidadesGS1.GS1128_DevolverCantidad(CodLeido));
                    CodLeido = CodLeido.Substring(0, CodLeido.Length - 10);
                    string Caso = String.Empty;
                    if (CodLeido.Length > 2)
                    {
                        Caso = CodLeido.Substring(0, 2);
                    }
                    else
                    {
                        Caso = "00";
                    }
                    switch (Caso)
                    {
                        case "01": respuesta = ImprimirCodigoConsumo(CodLeido, idUsuario, Cantidad);
                            break;
                        case "91": respuesta = ImprimirCodigoUbicacion(CodLeido, idUsuario, Cantidad);
                            break;
                        case "00": respuesta = CrearSSCC(Cantidad.ToString(), idUsuario);
                            break;
                        case "02": respuesta = ImprimirCodigoGTIN14(CodLeido, idUsuario);
                            break;
                    }
                }
                else
                {
                    respuesta = "La cantidad debe enviarse 36n...n en formato GS1";
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                return respuesta + " Ha ocurrido un Error, Codigo: TraceID-UI-ADM-000001" + ex.Message;
            }
        }

        #endregion ImprimirCodigo

        #region ProcesarDespacho

        public static string AsociarSSCC(string CodLeido, string idUsuario)
        {
            string SQL = "";
            string Respuesta = "No Procesado;-1";
         
            try
            {
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);
               //TRAMA = SSCC;CODGS1;UBICACIÓN_PISTOLEADA;UBICACIÓN_TAREA;Idmaestrosolicitud;idtarea
                string[] spl = CodLeido.Split(';');

               // obtengo información del artículo a alistar
                if (spl[1].Substring(0, 2) != "01")  // en caso de leer un GTIN13-GTIN14 se le pone adelante el 01 para poder procesar la recepción-ubicación-alisto.
                {
                    if (spl[1].Length == 13)
                        spl[1] = "010" + spl[1];
                    else if (spl[1].Length == 14)
                        spl[1] = "01" + spl[1];
                }

               // valido que todo lo que venga en la trama este correcto
                string[] Articulo = ObtenerIdArticuloNombreCodigoLeido_GS1128(spl[1].Trim(), idUsuario).Split(';');
                string SSCCLeido = CargarEntidadesGS1.GS1128_DevolverSSCCGenerado(spl[0].Trim());
                string idUbicacion = DevolverIdUbicacion(spl[2].Trim(), idUsuario);  // se obtiene el IdUbicacion a partir de la etiqueta pistoleada.
                string ubicavalida = DevolverIdUbicacion(spl[3].Trim(), idUsuario);  // se obtiene el IdUbicacion a partir de la etiqueta de la tarea.
                string idMaestrosolicitud = spl[4].Trim(); 

                if (string.IsNullOrEmpty(SSCCLeido))  // si el SSCC es vacío o nulo se aborta el proceso.
                {
                    Respuesta = "SSCC no válido...;-1";
                    return Respuesta;
                }

                if (string.IsNullOrEmpty(idUbicacion))  // si la ubicación es vacío o nulo se aborta el proceso.
                {
                    Respuesta = "Ubicación no válida...;-1";
                    return Respuesta;
                }
                
                if (string.IsNullOrEmpty(Articulo[0]) || Articulo[0] == "0")  // si el artículo no es válido se aborta el proceso.
                {
                    Respuesta = "Artículo no válido...;-1";
                     return Respuesta;
                }

                if (idUbicacion != ubicavalida)
                {
                    Respuesta = "Ubicación no corresponde...;-1";
                    return Respuesta;
                }

                if (string.IsNullOrEmpty(idMaestrosolicitud))
                {
                    Respuesta = "Solicitud no válida...;-1";
                    return Respuesta;
                }

               // si todo está correcto, se extrae la información del código GS1
                EntidadesGS1.e_GTIN GTIN = new EntidadesGS1.e_GTIN();
                string idArticulo = Articulo[0];
                string NombreArticulo = Articulo[1];
                string FV = CargarEntidadesGS1.GS1128_DevolverFechaVencimiento(spl[1].Trim());
                string Lote = CargarEntidadesGS1.GS1128_DevolveLote(spl[1].Trim());
                string cantidad = CargarEntidadesGS1.GS1128_DevolverCantidad(spl[1].Trim());
                string Peso = CargarEntidadesGS1.GS1128_DevolverPeso(spl[1].Trim());
                cantidad = Single.Parse(cantidad).ToString();
                Single CantidadLineas = 0;
                bool EsGTIN = CargarEntidadesGS1.GS1128_ObtenerGTIN(spl[1].Trim(), out GTIN);  // verifico que es un GTIN valido
                DateTime EvaluaFecha = DateTime.Today;

               // antes de evaluar la mínima vida útil para restaurantes, se debe definir si el producto tiene días mínimos de vencimiento para restaurantes. 
                SQL = "";
                SQL = "SELECT " + e_TblMaestroArticulosFields.DiasMinimosVencimientoRestaurantes() +
                      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroArticulos() +
                      "  WHERE " + e_TblMaestroArticulosFields.idArticulo() + " = " + idArticulo +
                      "        AND " + e_TblMaestroArticulosFields.Granel() + " = 0";
                string diasminimos = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario); // me trae días mínimos de vencimiento restaurantes si el artículo no es granel.

                if (!string.IsNullOrEmpty(diasminimos))
                {
                    int diasminimosrest = Diasminimosvencimientoresturantes(idArticulo, idUsuario);
                    switch (diasminimosrest)  // se evalúan los días mínimos de vencimiento restaurantes.
                    {
                        case 2:
                            Respuesta = "Error al evaluar días minimos de vencimiento para restaurantes en este producto;-1";
                            break;

                        default:
                            EvaluaFecha = DateTime.Parse(FV).AddDays(diasminimosrest);
                            break;
                    }

                    if (!(EvaluaFecha > DateTime.Today))  // si no cumplen con los días mínimo de vencimiento para restaurantes.
                    {
                        Respuesta = "Producto no cumple con los días mínimos de vencimiento para restaurantes...( " + diasminimos.ToString() + ");-1";
                        //return Respuesta;
                    }
                }

                if (EsGTIN)   // es un GTIN valido
                {
                    if (Peso == "0")
                    {
                        if (int.Parse(cantidad) > GTIN.VLs[0].Cantidad)
                            CantidadLineas = int.Parse(cantidad);
                        else
                            CantidadLineas = GTIN.VLs[0].Cantidad;  //  si es un GTIN14, las lineas son la cantidad del GTIN13 que representa.
                    }
                    else
                    {
                        CantidadLineas = Single.Parse(Peso, System.Globalization.CultureInfo.InvariantCulture) * 1000;
                    }
                }

                if (idUbicacion == ubicavalida)
                {
                    n_ProcesarSolicitud Alistararticulo = new n_ProcesarSolicitud();
                    Respuesta = Alistararticulo.AlistarArticulo(Int64.Parse(idArticulo),
                                                                idCompania,
                                                                SSCCLeido,
                                                                28,
                                                                Lote,
                                                                FV,
                                                                Int64.Parse (idMaestrosolicitud),
                                                                Int64.Parse(CantidadLineas.ToString()),
                                                                Int64.Parse(idUbicacion),
                                                                spl[5]);
                }

                return Respuesta;
            }
            catch (Exception ex)
            {
                return ex.Message + "-" + Respuesta;
            }
        }

        public static string ProcesarDespacho(string CodLeido, string idUsuario, string idMetodoAccion)
        {
            string Respuesta = "No Procesado-";
            string SSCCLeido = CargarEntidadesGS1.GS1128_DevolverSSCCGenerado(CodLeido);

            try
            {
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);
               // verifico que el SSCC sea válido.
                if (string.IsNullOrEmpty(SSCCLeido))
                {
                    Respuesta = "SSCC no válido";
                    throw new Exception(Respuesta);
                }

                n_ProcesarSolicitud ProcesarDespacho = new n_ProcesarSolicitud();
                Respuesta = ProcesarDespacho.ProcesarDespacho(SSCCLeido, idCompania, int.Parse(idUsuario));
                if (Respuesta.Contains("TRANSACCIÓN EXITOSA"))
                {
                    //string[] exito = Respuesta.Split(';');
                    //string cierrasol = CierraSolicitud(exito[1], idUsuario); 
                    //string cierraSSCC = CierraSSCC(CodLeido, idUsuario);
                    //return exito[0] + "*-*" + cierrasol + "*-*" + cierraSSCC;
                    string[] exito = Respuesta.Split(';');
                    return exito[0] + "*-*" + exito[1] + "*-*" + exito[2];
                }
                else
                    return Respuesta;
            }
            catch (Exception ex)
            {
                return Respuesta += ex.Message;
            }

        }

        #endregion ProcesarDespacho

        #region ProcesarOrdenCompra

        public static bool ActualizarDetalleOC(string RecibidoOK, string idDetalleOrdenCompra, string CntRecibida, string idUsuario)
        {
            try
            {
                string SQL = "";
                SQL = " update " + e_TablasBaseDatos.TblDetalleOrdenesCompra();
                SQL += " set " + e_TblDetalleOrdenesCompraFields.CantidadxRecibir() + " = '" + CntRecibida + "' ";
                SQL += " where " + e_TblDetalleOrdenesCompraFields.idDetalleOrdenCompra() + " = '" + idDetalleOrdenCompra + "';";
                SQL += " update " + e_TablasBaseDatos.TblDetalleOrdenesCompra();
                SQL += " set " + e_TblDetalleOrdenesCompraFields.RecibidoOK() + " = '" + RecibidoOK + "' ";
                SQL += " where " + e_TblDetalleOrdenesCompraFields.idDetalleOrdenCompra() + " = '" + idDetalleOrdenCompra + "';";
                SQL += " update " + e_TablasBaseDatos.TblDetalleOrdenesCompra();
                SQL += " set " + e_TblDetalleOrdenesCompraFields.idUsuario() + " = '" + idUsuario + "' ";
                SQL += " where " + e_TblDetalleOrdenesCompraFields.idDetalleOrdenCompra() + " = '" + idDetalleOrdenCompra + "';";
                SQL += " update " + e_TablasBaseDatos.TblDetalleOrdenesCompra();
                SQL += " set " + e_TblDetalleOrdenesCompraFields.Procesado() + " = '" + "1" + "' ";
                SQL += " where " + e_TblDetalleOrdenesCompraFields.idDetalleOrdenCompra() + " = '" + idDetalleOrdenCompra + "';";
                n_ConsultaDummy.PushData(SQL, idUsuario);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string ProcesarDetalleOC(string CantidadRecibida, string idDetalleOrdenCompra, string idUsuario)
        {
            DataSet DS = new DataSet();
            string SQL = "";
            string Mensaje = "";
            try
            {
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);
                //
                SQL = "SELECT " + e_TblDetalleOrdenesCompraFields.idArticulo() + "," + e_TblDetalleOrdenesCompraFields.CantidadxRecibir() + 
                      "  FROM "  + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleOrdenesCompra() +
                      "  WHERE " + e_TblDetalleOrdenesCompraFields.idDetalleOrdenCompra() + " = " + idDetalleOrdenCompra + 
                      "        AND " + e_TblDetalleOrdenesCompraFields.idCompania() + " = '" + idCompania + "'";
                DS = n_ConsultaDummy.GetDataSet(SQL, idUsuario);
                if (DS.Tables[0].Rows.Count == 1)
                {
                    string idArticulo = DS.Tables[0].Rows[0][0].ToString();
                    string CantidadxRecibir = DS.Tables[0].Rows[0][1].ToString();
                    double CntRecibida = 0;
                    double CntxRecibir = 0;
                    bool EsNumero1 = Double.TryParse(CantidadRecibida, out CntRecibida);
                    bool EsNumero2 = Double.TryParse(CantidadxRecibir, out CntxRecibir);
                    if (EsNumero1)
                    {
                        if (EsNumero2)
                        {
                            //Las cantidades son numeros
                            //esto valida si la linea ya fue procesada
                            if ((n_ConsultaDummy.GetUniqueValue("SELECT " + e_TblDetalleOrdenesCompraFields.idDetalleOrdenCompra() + " FROM " + e_TablasBaseDatos.TblDetalleOrdenesCompra() + " WHERE " + e_TblDetalleOrdenesCompraFields.idDetalleOrdenCompra() + " = '" + idDetalleOrdenCompra + "' AND " + e_TblDetalleOrdenesCompraFields.Procesado() + " = 0 AND " + e_TblDetalleOrdenesCompraFields.idCompania() + " = '" + idCompania + "'", idUsuario)) == "")
                            {
                                Mensaje = "error;";
                            }
                            else
                            {
                                if (ActualizarDetalleOC("1", idDetalleOrdenCompra, CantidadRecibida, idUsuario))
                                {
                                    if (CntRecibida == CntxRecibir)
                                    {
                                        Mensaje = "ok; Se proceso el detalle de la orden # " + idDetalleOrdenCompra + " exitosamente.";
                                    }
                                    else
                                    {
                                        Mensaje = "Info; Se proceso el detalle de la orden # " + idDetalleOrdenCompra + " exitosamente, sin embargo la cantidad recibida es menor a la esperada";
                                    }

                                }
                                else
                                {
                                    Mensaje = "error; Por alguna razon no logramos actualizar el registro, consulte a su administrador de base de datos, Codigo: TID-NE-LWM-000002.";
                                }
                            }
                        }
                        else // (EsNumero2)
                        {
                            Mensaje = "error; OPs! La Cantidad Solicitada no representa un numero, por favor corrija en base de datos, algo esta realmente mal,  Codigo: TID-NE-LWM-000003.";
                        }
                    }
                    else // (EsNumero1)
                    {
                        Mensaje = "error;OPs! La Cantidad Recibida no representa un numero, por favor corrija. Codigo: TID-NE-LWM-000004.";
                    }
                }
                else // (DS.Tables[0].Rows.Count == 1)
                {
                    if (DS.Tables[0].Rows.Count > 1)
                    {
                        Mensaje = "error; OPs! Por alguna razon existen mas de 1 una linea de detalle con ese numero. Lo siento pero no podemos trabajar asi. Codigo: TID-NE-LWM-000005.";
                    }
                    else
                    {
                        Mensaje = "error;OPs! Por alguna razon no encontramos la linea detalle #: " + idDetalleOrdenCompra.ToString() + "Codigo: TID-NE-LWM-000006";
                    }
                }
                return Mensaje;
            }
            catch (Exception)
            {
                Mensaje = "error;Ops! Ha ocurrido un Error, Codigo: TID-NE-LWM-000007";
                return Mensaje;
            }
        }

        #endregion ProcesarOrdenCompra

        #region ManejoTransacciones

        public static string TransaccionMD(string idArticulo, string cantidad, string Tabla, string IdCampo, string ValorCampo, string NombreEvento, string IdEstado, string SumUno_RestaCero, string idUsuario, string idMetodoAccion, string FechaVencimiento, string Lote, string idUbicacion)
        {
            //TraceID.(2016). LogicaWMS/n_WMS.En Trace ID Codigos documentados(22).Costa Rica:Grupo Diverscan. 
            string Trama = "Transaccion no procesada-";
            try
            {
                if (ValorCampo != "")
                {
                    string bdName = String.Empty;
                    string tablaName = String.Empty;

                    try
                    {
                        string[] bd = Tabla.Split('.');
                        bdName = bd[0];
                        tablaName = bd[1] + "." + bd[2];
                    }
                    catch (Exception)
                    {
                        bdName = "TRACEID";
                        tablaName = e_TablasBaseDatos.TblTransaccion();
                    }
                   
                    string SinonimoPalabraIndependiente = "";
                    List<Diccionario> Diccionarios = new List<Diccionario>();
                    string NombreCampo = e_TblTransaccionFields.NumDocumentoAccion();
                   
                    /// Esto evita que la transaccion se duplique.
                    string SQL = "SELECT " + e_TblTransaccionFields.idRegistro() + 
                                 "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + " ";
                    SQL += " WHERE " + e_TblTransaccionFields.idTablaCampoDocumentoAccion() + " = '" + Tabla + "'";
                    SQL += " AND " + e_TblTransaccionFields.idCampoDocumentoAccion() + " = '" + Tabla + "." + IdCampo + "'";
                    SQL += " AND " + NombreCampo + " = '" + ValorCampo + "'";
                    SQL += " AND " + e_TblTransaccionFields.idEstado() + " = '" + IdEstado + "'";
                    DataSet DSQuery = da_ConsultaDummy.GetDataSet(SQL, idUsuario);
                    if (DSQuery.Tables[0].Rows.Count == 0)
                    {
                        Trama = "";
                        string Procesado = "0";
                        if (idUbicacion != "")
                        {
                            //TRAMA DE RESPUESTA = SumUno_RestaCero, idArticulo, FechaVencimiento, Lote, idUsuario, idAccion, idTablaCampoDocumentoAccion, idCampoDocumentoAccion, NumDocumentoAccion, idUbicacion, Procesado, idEstado, Cantidad
                            Trama += SumUno_RestaCero + ";" + idArticulo + ";" + FechaVencimiento + ";" + Lote + ";" + idUsuario + ";" + idMetodoAccion + ";" + Tabla + ";" + Tabla + "." + IdCampo + ";" + ValorCampo + ";" + idUbicacion + ";" + Procesado + ";" + IdEstado + ";" + cantidad;
                            da_MotorDecision.AgregarTRAIngresoSalidaArticulos(Trama);
                            Trama = "Transaccion exitosa";
                        }
                    }
                    else
                    {
                        Trama = "Transaccion duplicada";
                    }
                }

                return Trama;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string InsertDetalleOrdenCompra(string CodLeido1, string idUsuario, string idMetodoAccion)
        {

            e_ProcesarOrdenCompra e_procesarOrdenCompra;
            n_DetalleOrdenCompra n_detalleOrdenCompra = new n_DetalleOrdenCompra();

            string respuesta = "Código de barras nulo o no corresponde a la empresa asociada.";
            string mensaje = String.Empty;
            try
            {
                string[] CodLeido = CodLeido1.Split(';');
                string CantidadporRecibir = CodLeido[1].ToString();
                string MaestroOrdencompra = CodLeido[2].ToString();

                string idCompania = ObtenerCompaniaXUsuario(idUsuario);

                string[] Articulo = ObtenerIdArticuloNombreCodigoLeido_GS1128(CodLeido[0].ToString(), idUsuario).Split(';');
                string idArticulo = Articulo[0];
                string NombreArticulo = Articulo[1];
                
                e_procesarOrdenCompra = new e_ProcesarOrdenCompra(Convert.ToInt64(MaestroOrdencompra),
                                                                  Convert.ToInt64(idArticulo),
                                                                  Convert.ToInt64(CantidadporRecibir), 
                                                                  Convert.ToInt32(idUsuario), 
                                                                  idCompania);
                n_detalleOrdenCompra.InsertarDetalleOrdenCompra(e_procesarOrdenCompra);
                
               return respuesta;
            }
            catch (Exception ex)
            {
                respuesta = "Código de barras nulo o en blanco";
                return respuesta + " - " + ex.Message;
            }
        }

        public static string InsertDetalleSolicitud(string idLineaDetalleSolicitud, string datos, string idMetodoAccion)
        {
            string[] data = datos.Split(';');
            string fechavenc = data[1];
            string idusuario = data [0];
            string cant = data[2];
            string respuesta = String.Empty;
          
            n_ProcesarSolicitud InsertaDetalleSolicitudTRA = new n_ProcesarSolicitud();
            e_ProcesarSolicitud DetalleSolicitudTRA;
           
            try
            {
                string idCompania = ObtenerCompaniaXUsuario(idusuario);
            
                DetalleSolicitudTRA = new e_ProcesarSolicitud(Int64.Parse(cant),
                                                              int.Parse (idusuario),
                                                              idCompania,
                                                              ConfigurationManager.AppSettings["Zona_Almacenamiento"].ToString(),
                                                              ConfigurationManager.AppSettings["Zona_Picking"].ToString(),
                                                              Int64.Parse(idMetodoAccion),
                                                              Int64.Parse(idLineaDetalleSolicitud));

                respuesta = InsertaDetalleSolicitudTRA.InsertarDetalleSolicitud(DetalleSolicitudTRA);
               
            }
            catch (Exception ex)
            {
                return respuesta + "-" + ex.Message;
            }

            return respuesta;

        }

        public static string ObtenerIdEstadoTransaccion(string idMetodoAccion, string idUsuario)
        {
            string Respuesta = "";
            string SQL = "";
            try
            {
                SQL = "SELECT " + e_TblEstadoTransaccionalFields.IdEstado() + " FROM " + e_TablasBaseDatos.TblEstadoTransaccional() + " WHERE " + e_TblEstadoTransaccionalFields.idMetodoAccion() + " = '" + idMetodoAccion + "'";
                Respuesta = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                return Respuesta;
            }
            catch (Exception)
            {
                return Respuesta;
            }
        }

        public static string ObtenerSumUno_RestaCeroTransaccion(string idMetodoAccion, string idUsuario)
        {
            string Respuesta = "";
            string SQL = "";
            try
            {
                SQL = "SELECT " + e_TblEstadoTransaccionalFields.SumUno_RestaCero() + " FROM " + e_TablasBaseDatos.TblEstadoTransaccional() + " WHERE " + e_TblEstadoTransaccionalFields.idMetodoAccion() + " = '" + idMetodoAccion + "'";
                Respuesta = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                return Respuesta;
            }
            catch (Exception)
            {
                return Respuesta;
            }
        }

        public static string ObteneridUbicacionXCodUbicacion(string CodUbicacion, string idUsuario)
        {
            string Respuesta = "";
            string SQL = "";
            try
            {
                SQL = "SELECT " + e_TblUsuarios.IdCompania() + " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblUsuarios() + " WHERE " + e_TblUsuarios.IdUsario() + " = '" + idUsuario + "'";
                string idCompania = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                SQL = "SELECT " + e_VistaCodigosUbicacionFields.idUbicacion() + " FROM " + e_TablasBaseDatos.VistaCodigosUbicacion() + " WHERE " + e_VistaCodigosUbicacionFields.CODUBI() + " = '" + CodUbicacion + "' AND " + e_VistaCodigosUbicacionFields.idCompania() + " = '" + idCompania + "'";
                Respuesta = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);


                return Respuesta;
            }
            catch (Exception)
            {
                return Respuesta;
            }
        }
        
        public static string ObtenerValorIdCampo(string idMetodoAccionAnterior, string FechaVencimiento, string Lote, string idArticulo, string Cantidad, string idUsuario)
        {
            string SQL = "SELECT " + e_TblTransaccionFields.idRegistro() + 
                         "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + 
                         "  WHERE " + e_TblTransaccionFields.FechaVencimiento() + " = '" + FechaVencimiento + 
                         "'       AND " + e_TblTransaccionFields.Lote() + " = '" + Lote + 
                         "'       AND " + e_TblTransaccionFields.idArticulo() + " = '" + idArticulo + 
                         "'       AND " + e_TblTransaccionFields.Cantidad() + " = '" + Cantidad + 
                         "'       AND " + e_TblTransaccionFields.idMetodoAccion() + " = '" + idMetodoAccionAnterior + 
                         "'       AND " + e_TblTransaccionFields.idRegistro() + 
                         " NOT IN (SELECT " + e_TblTransaccionFields.NumDocumentoAccion() + 
                         " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + 
                         " WHERE " + e_TblTransaccionFields.idTablaCampoDocumentoAccion() + " = '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + "TRAIngresoSalidaArticulos'" + 
                         " AND " + e_TblTransaccionFields.idCampoDocumentoAccion() + " = '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + "TRAIngresoSalidaArticulos.idRegistro')";
            string ValorIdCampo = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
            return ValorIdCampo;
        }


        #endregion Manejo Transacciones

        #region Ubicaciones
        //Falta idUsuario, agregar a Flujo
        public static string ObtenerDisponibilidadCamion(string idVehiculo, decimal SolicitudPesoKilos, decimal SolicitudDimensionm3)
        {
            string resp = "";
            string SQL = "";
            try
            {  
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);

                decimal TotalPesoKilos = 0;
                decimal TotalDimensionm3 = 0;
                decimal PesoVehiculo = 0;
                decimal VolumenVehiculo = 0;
               
                DataSet DatosActualesCarga = new DataSet();
                DataSet InfoVehiculo = new DataSet();
                decimal[] TotalLibre = new decimal[2];
                string FechaActual = "declare @ano as int,@mes as int,@dia as int,@fechaini as date set @fechaini = getdate() set @ano = year(@fechaini)set @mes = month(@fechaini) set @dia = day(@fechaini) select cast(@ano as nvarchar(4)) + '-' + cast(@mes as nvarchar(2)) + '-' + cast(@dia as nvarchar(2))";
                string fecha = n_ConsultaDummy.GetUniqueValue(FechaActual, "0");
                //Continuar Comparación de Fechas

                //string AuxDatos = " SELECT A.PesoKilos , A.DimensionM3 FROM TraVehiculoTrasladoSSSCC A WHERE A.Fecha = '" + fecha + "'";
                // en este query se tiene que tomar en cuenta el camion actual.
                string AuxDatos = "SELECT A.PesoKilos , A.DimensionM3 " +
                                  "  FROM TraVehiculoTrasladoSSSCC AS A " +
                                  "    INNER JOIN ADMVehiculo      AS B ON (A.idVehiculo = B.IdVehiculo) " + 
                                  "  WHERE A.Fecha = '" + fecha + "'" +
                                  "        AND B.idCompania = '" + idCompania + "'" +
                                  "        AND B.IdVehiculo = " + idVehiculo;
                DatosActualesCarga = n_ConsultaDummy.GetDataSet2(AuxDatos, "0");

                //string SqlCapacidadVehiculo = "SELECT A.CapacidadVolumen , A.CapacidadPeso FROM ADMVehiculo A WHERE A.idVehiculo = " + idVehiculo;
                string SqlCapacidadVehiculo = "SELECT A.CapacidadVolumen , A.CapacidadPeso " +
                                              "  FROM ADMVehiculo AS A " + 
                                              "  WHERE A.idVehiculo = '" + idVehiculo + "'" +
                                              "        AND A.idCompania = '" + idCompania + "'";
                InfoVehiculo = n_ConsultaDummy.GetDataSet2(SqlCapacidadVehiculo, "0");

                if (InfoVehiculo.Tables[0].Rows.Count > 0)
                {
                    if (DatosActualesCarga.Tables[0].Rows.Count > 0)
                    {

                        VolumenVehiculo = Convert.ToDecimal(InfoVehiculo.Tables[0].Rows[0][0].ToString());
                        PesoVehiculo = Convert.ToDecimal(InfoVehiculo.Tables[0].Rows[0][1].ToString());


                        foreach (DataRow Row in DatosActualesCarga.Tables[0].Rows)
                        {
                            TotalPesoKilos += Convert.ToDecimal(Row["PesoKilos"].ToString());
                            TotalDimensionm3 += Convert.ToDecimal(Row["Dimensionm3"].ToString());
                        }

                        //Espacio libre

                        TotalLibre[0] = VolumenVehiculo - TotalDimensionm3;
                        TotalLibre[1] = PesoVehiculo - TotalPesoKilos;

                        if (TotalLibre[0] >= SolicitudDimensionm3 && TotalLibre[1] >= SolicitudPesoKilos)                       
                            resp = "Solicitud Procesada";
                        else                      
                            resp = "Solicitud Denegada";
                    }
                    else
                   resp = "Solicitud Procesada";
          
                }
                else 
                    resp = "Error al obtener datos";  
              
                return resp;
            }
            catch (Exception)
            {
                return resp;
            }
        
        }

        public static string ObtenerDisponibilidadArticulo(string idArticulo, string idUsuario)
        {
            string Respuesta = "";
            string SQL = "";
            string GTIN = "";
            double PesokilosIng = 0.00;
            double PesokilosRes = 0.00;
            double PesokilosTra = 0.00;
            double PesokilosIngArt = 0.00;
            double PesokilosResArt = 0.00;
            double PesokilosTraArt = 0.00;
            string Granel = "";
            string Um = "-";
            DataSet DB = new DataSet();
            string[] idarticulo = idArticulo.Split('|');
            if (idarticulo.Count() == 1)
            {
                SQL = "SELECT " + e_TblMaestroArticulosFields.GTIN() +
                      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroArticulos() +
                      "  WHERE " + e_TblMaestroArticulosFields.idArticulo() + " = " + idarticulo[0];
                GTIN = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                GTIN = "010" + GTIN;
            }

            string[] articulo;

            try
            {
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);
                string idCompaniaArticulo = ObtenerCompaniaXArticulo(idarticulo[0], idUsuario);

                if (idCompaniaArticulo.Equals(idCompania))
                {
                    int CntUbicacionesArticulo = 0;
                    string CodUbicacion;
                    double TotalIngresados = 0;
                    //double TotalReservados = 0;
                    //double TotalTraslados = 0;
                    double CantidadTrasladadaUbi = 0;
                    double CantidadIngresadaUbi = 0;
                    double CantidadReservadadaUbi = 0;
                    string idubicacion = "";
                    string Zona_Almacenamiento = ConfigurationManager.AppSettings["Zona_Almacenamiento"].ToString();
                    string Zona_Picking = ConfigurationManager.AppSettings["Zona_Picking"].ToString();
                    SQL = "";
                    SQL = "";
                    SQL = "SELECT *" +
                          "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaDisponibilidadArticulos() +
                          "  WHERE " + e_VistaUbicacionesDetalle.idArticulo() + " = " + idarticulo[0] + 
                          "  ORDER BY FV, etiqueta, lote";


                    DB = n_ConsultaDummy.GetDataSet(SQL, idUsuario);
                    if (DB.Tables[0].Rows.Count > 0)
                    {
                        Respuesta += "---* INFORMACIÓN ARTÍCULO *---\n";
                        EntidadesGS1.e_GTIN GT = new EntidadesGS1.e_GTIN();
                        if (idarticulo.Count() > 1)
                            GTIN = idarticulo[1];

                        bool gtintrue = CargarEntidadesGS1.GS1128_ObtenerGTIN(GTIN, out GT);
                        articulo = ObtenerIdArticuloNombreCodigoLeido_GS1128(GTIN, idUsuario).Split(';');
                        Respuesta += "Artículo........:" + articulo[3] + "-" + articulo[1] + "\n";
                        Respuesta += "Fecha Venc.:" + CargarEntidadesGS1.GS1128_DevolverFechaVencimiento(GTIN) + "\n";
                        Respuesta += "Lote...........:" + CargarEntidadesGS1.GS1128_DevolveLote(GTIN) + "\n";
                        Respuesta += CargarEntidadesGS1.GS1128_DevolverUnidadInventario_Alisto(GT.VLs[0].Codigo.ToString()) + "-" + 
                                     CargarEntidadesGS1.GS1128_DevolverCantidad(GTIN) + "\n";
                        Respuesta += "Tipo GTIN....:" + GT.Tipo + "\n";
                        Respuesta += "Peso.........:" + CargarEntidadesGS1.GS1128_DevolverPeso(GTIN) + "\n";
                        Respuesta += "Artículo Id.:" + articulo[0] + "\n";
                        Respuesta += "-Cantidad-|-Unidad-|-Lote-|--FecVenc--|\n";
                        Respuesta += "--Ubicación--\n";

                        foreach (DataRow fila in DB.Tables[0].Rows)
                        {
                            CntUbicacionesArticulo++;
                            if (fila["RegUbic"].ToString() == "1" && (CantidadIngresadaUbi > 0 || CantidadReservadadaUbi > 0 || CantidadTrasladadaUbi > 0))
                            {
                                if (fila["Granel"].ToString() == "False")
                                {
                                    Respuesta += "Disponible:" + CantidadIngresadaUbi + "   " + fila["Unidad_Medida"] + "\n";
                                    //Respuesta += "Total reservado.:" + CantidadReservadadaUbi + "   " + PesokilosResArt.ToString() + " Kg" + "\n";
                                    //Respuesta += "Total trasladado:" + CantidadTrasladadaUbi + "   " + PesokilosTraArt.ToString() + " Kg" + "\n";
                                }
                                else
                                {
                                    Respuesta += "Disponible:" + CantidadIngresadaUbi + "   " + fila["Unidad_Medida"] + "\n";
                                    //Respuesta += "Total reservado.:" + CantidadReservadadaUbi + " Kg" + "\n";
                                    //Respuesta += "Total trasladado:" + CantidadTrasladadaUbi + " Kg" + "\n";
                                }
                                CantidadIngresadaUbi = 0;
                                CantidadReservadadaUbi = 0;
                                CantidadTrasladadaUbi = 0;
                                PesokilosIngArt = 0.00;
                                PesokilosResArt = 0.00;
                                PesokilosTraArt = 0.00;
                            }

                            if (idubicacion != fila["idUbicacion"].ToString())
                            {
                                //CodUbicacion = fila["Etiqueta"].ToString();
                                //Respuesta += "Ubicación........: [" + CodUbicacion + "]" + "\n";  // fila["idUbicacion"].ToString() + "]
                                //Respuesta += "Articulo..........: " + fila["idInterno"].ToString() + "-" + fila["NombreArticulo"].ToString() + "\n";
                                //Respuesta += "Fecha Vencim.: " + fila["FechaVencimiento"].ToString().Substring(0, 10) + "\n";
                                //Respuesta += "Lote.............: " + fila["Lote"].ToString() + "\n";
                                //Respuesta += "idArticulo.......: " + fila["idArticulo"].ToString() + "\n";
                            }

                            //if (fila["NombreEstado"].ToString() == "Ingresado")
                            //{

                                if (fila["Granel"].ToString() == "False")
                                {
                                if (fila["idZona"].ToString() == "1" || fila["idZona"].ToString() == "3" || fila["idZona"].ToString() == "4")
                                  {
                                    TotalIngresados += Math.Round((Single.Parse(fila["SUMACantidadEstado"].ToString())), 6);
                                    //PesokilosIng += int.Parse(fila["SUMACantidadEstado"].ToString()) * double.Parse(fila["PesoKilos"].ToString());
                                    //PesokilosIngArt += int.Parse(fila["SUMACantidadEstado"].ToString()) * double.Parse(fila["PesoKilos"].ToString());
                                  }
                                CantidadIngresadaUbi += Math.Round((Single.Parse(fila["SUMACantidadEstado"].ToString())), 6);
                            }
                                else
                                {
                                if (fila["idZona"].ToString() == "1" || fila["idZona"].ToString() == "3" || fila["idZona"].ToString() == "4")
                                  {
                                    TotalIngresados += Math.Round((Single.Parse(fila["SUMACantidadEstado"].ToString()) / 1000), 6);
                                    CantidadIngresadaUbi += Math.Round((Single.Parse(fila["SUMACantidadEstado"].ToString()) / 1000), 6);
                                  }
                                CantidadIngresadaUbi += Math.Round((Single.Parse(fila["SUMACantidadEstado"].ToString()) / 1000), 6);
                            }
                          
                                if (fila["Granel"].ToString() == "False")
                                {
                                  DateTime fv = DateTime.Parse(fila["FV"].ToString());

                                  string year = fv.Year.ToString();
                                  string month = fv.Month.ToString();
                                  string day = fv.Day.ToString();

                                  if (month.Length == 1)
                                    month = "0" + month;

                                  if (day.Length == 1)
                                    day = "0" + day;

                                string FechaVencimiento = day + "/" + month + "/" +  year;

                                string UM = fila["Unidad_Medida"].ToString();
                                UM = UM.Replace(' ', '_'); //  si hay espacios en blanco lo sustituye por '_'

                                if (UM.Length > 12)
                                    UM = UM.Substring(0, 11);

                                Respuesta += Math.Round(CantidadIngresadaUbi, 6) + "|[" + UM + "]|" + fila["Lote"] + "|" + FechaVencimiento + "|\n";
                                Respuesta += fila["Etiqueta"].ToString() + "\n";
                            }
                                else
                                {
                                  DateTime fv = DateTime.Parse(fila["FV"].ToString());

                                  string year = fv.Year.ToString();
                                  string month = fv.Month.ToString();
                                  string day = fv.Day.ToString();

                                  if (month.Length == 1)
                                    month = "0" + month;

                                  if (day.Length == 1)
                                    day = "0" + day;

                                string FechaVencimiento = day + "/" + month + "/" + year;

                                string UM = fila["Unidad_Medida"].ToString();
                                UM = UM.Replace(' ', '_'); //  si hay espacios en blanco lo sustituye por '_'
                                if (UM.Length > 12)
                                    UM = UM.Substring(0, 11);

                                Respuesta += Math.Round(CantidadIngresadaUbi, 6) + "|[" + UM + "]|" + fila["Lote"] + "|" + FechaVencimiento + "|\n";
                                Respuesta += fila["Etiqueta"].ToString() + "\n";
                                //Respuesta += "Total reservado.:" + CantidadReservadadaUbi + " Kg" + "\n";
                                //Respuesta += "Total trasladado:" + CantidadTrasladadaUbi + " Kg" + "\n";
                            }
                                CantidadIngresadaUbi = 0;
                                CantidadReservadadaUbi = 0;
                                CantidadTrasladadaUbi = 0;
                                PesokilosIngArt = 0.00;
                                PesokilosResArt = 0.00;
                                PesokilosTraArt = 0.00;
                            //}

                            idubicacion = fila["idUbicacion"].ToString();
                            Granel = fila["Granel"].ToString();
                            Um = fila["Unidad_Medida"].ToString();

                        }

                        if (Granel == "False")
                        {
                            Respuesta += "-* DISPONIBILIDAD UNIDADES INVENTARIO *-\n";
                            Respuesta += "Total Disponible.......:" + TotalIngresados.ToString() + "  [" + Um + "]\n";
                            //Respuesta += "Total Reservados.........:" + TotalReservados.ToString() + "     " + PesokilosRes.ToString() + " Kg" + "\n";
                            //Respuesta += "Total Trasladados........:" + TotalTraslados.ToString() + "      " + PesokilosTra.ToString() + " Kg" + "\n";
                            //Respuesta += "Cantidad Disponible......:" + (TotalIngresados - TotalReservados - TotalTraslados).ToString() + " " + (PesokilosIng - PesokilosRes - PesokilosTra).ToString() + " Kg" + "\n";
                        }
                        else
                        {
                            Respuesta += "-* DISPONIBILIDAD UNIDADES GRANEL INVENTARIO *-\n";
                            Respuesta += "Total Disponible.......:" + TotalIngresados.ToString() + "  [" + Um + "]\n";
                            //Respuesta += "Total Reservados.........:" + TotalReservados.ToString() + " Kg" + "\n";
                            //Respuesta += "Total Trasladados........:" + TotalTraslados.ToString() + " Kg" + "\n";
                            //Respuesta += "Cantidad Disponible......:" + (TotalIngresados - TotalReservados - TotalTraslados).ToString() + " Kg" + "\n";
                        }

                    }
                    else
                    {
                        Respuesta += "---* INFORMACIÓN ARTÍCULO *---\n";
                        EntidadesGS1.e_GTIN GT = new EntidadesGS1.e_GTIN();
                        if (idarticulo.Count() > 1)
                            GTIN = idarticulo[1];

                        bool gtintrue = CargarEntidadesGS1.GS1128_ObtenerGTIN(GTIN, out GT);
                        articulo = ObtenerIdArticuloNombreCodigoLeido_GS1128(GTIN, idUsuario).Split(';');
                        Respuesta += "Artículo........:" + articulo[3] + "-" + articulo[1] + "\n";
                        Respuesta += "Fecha Venc.:" + CargarEntidadesGS1.GS1128_DevolverFechaVencimiento(GTIN) + "\n";
                        Respuesta += "Lote...........:" + CargarEntidadesGS1.GS1128_DevolveLote(GTIN) + "\n";
                        Respuesta += CargarEntidadesGS1.GS1128_DevolverUnidadInventario_Alisto(GT.VLs[0].Codigo.ToString()) + "-" + 
                                     CargarEntidadesGS1.GS1128_DevolverCantidad(GTIN) + "\n";
                        Respuesta += "Tipo GTIN....:" + GT.Tipo + "\n";
                        Respuesta += "Peso.........:" + CargarEntidadesGS1.GS1128_DevolverPeso(GTIN) + "\n";
                        Respuesta += "Artículo Id.:" + articulo[0] + "\n";
                        Respuesta += "*ESTE ARTÍCULO NO TIENE TRANSACCIONES REGISTRADAS* \n";
                    }

                    return Respuesta;
                }
                else
                {
                    if (idArticulo == "" || idArticulo == "--Seleccionar--")
                        Respuesta = "Elija un artículo para ver disponibilidad.";
                    else
                        Respuesta = "El artículo consultado no corresponde a la empresa asociada";

                    return Respuesta;
                }
            }
            catch (Exception ex)
            {
                return Respuesta + "-" + ex.Message;
            }
        }

        public static List<e_Ubicacion> ObtenerDisponibilidadArticulo(string idArticulo, string cantidad, string idUsuario, string idZona)
        {
            List<e_Ubicacion> Respuesta = new List<e_Ubicacion>();
            try
            {
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);
                string idCompaniaArticulo = ObtenerCompaniaXArticulo(idArticulo, idUsuario);

                if (idCompaniaArticulo.Equals(idCompania))
                {
                int CntUbicacionesArticulo = 0;
                string CodUbicacion;
                double TotalIngresados = 0;
                double TotalReservados = 0;
                double TotalTraslados = 0;
                double CantidadTrasladadaUbi = 0;
                double CantidadIngresadaUbi = 0;
                double CantidadReservadadaUbi = 0;
                List<e_Ubicacion> Ubicaciones = new List<e_Ubicacion>();
                n_MotorDecisiones.Metodos MD = new n_MotorDecisiones.Metodos();
                Ubicaciones = MD.ObtenerUbicaciones(idUsuario, idArticulo);
                //double DiasFechaVencimiento = Convert.ToDouble(ConfigurationManager.AppSettings["DiasFechaVencimiento"].ToString());
                //DateTime FechaProceso = DateTime.Today;

                foreach (e_Ubicacion Ubicacion in Ubicaciones)
                {
                    foreach (e_Articulo Articulo in Ubicacion.Articulos)
                    {
                     //DateTime EvaluaFecha = Articulo.FechaVencimiento.AddDays(DiasFechaVencimiento);  // DiasFechaVencimiento = -5
                     // Solamente estan disponibles los Articulos en Zona de Picking y Almacenamiento
                     // se evalua, además, que la fecha de proceso tenga mas de 5 días respecto a la fecha de vencimiento.
                      if (Articulo.IdArticulo.ToString() == idArticulo && (Ubicacion.idZona == idZona)) //&& (EvaluaFecha > FechaProceso))
                        {
                            CntUbicacionesArticulo++;
                            CodUbicacion = Ubicacion.AbreviaturaBodega + "-" + Ubicacion.AbreviaturaZona + "-";
                            CodUbicacion += Ubicacion.estante + "-" + Ubicacion.nivel + "-" + Ubicacion.columna + "-";
                            CodUbicacion += Ubicacion.pos + "[" + Ubicacion.idUbicacion + "]";
                            foreach (e_CantidadeEstado CantEstado in Articulo.CantidadesEstado)
                            {
                                if (CantEstado.Nombre == "Ingresado")
                                {
                                    TotalIngresados += CantEstado.Cantidad;
                                    CantidadIngresadaUbi += CantEstado.Cantidad;

                                }

                                //if (CantEstado.Nombre == "Reservado")
                                //{
                                //    TotalReservados += CantEstado.Cantidad;
                                //    CantidadReservadadaUbi += CantEstado.Cantidad;
                                //}

                                if (CantEstado.Nombre == "Traslado")
                                {
                                    TotalTraslados += CantEstado.Cantidad;
                                    CantidadTrasladadaUbi += CantEstado.Cantidad;
                                }

                            }
                            if (TotalIngresados > 0)
                            {
                            double TotalDisponible = TotalIngresados - TotalReservados - TotalTraslados;
                            if (TotalDisponible >= Convert.ToDouble(cantidad))
                            {
                                Articulo.CantidadDisponible = cantidad.ToString();
                                Respuesta.Add(Ubicacion);
                            }
                            else 
                            {
                                Articulo.CantidadDisponible = TotalDisponible.ToString();
                                Respuesta.Add(Ubicacion);
                            }
                            }//if (TotalIngresados > 0)
                            CantidadIngresadaUbi = 0;
                            CantidadReservadadaUbi = 0;
                        }
                    }
                }
                }
                return Respuesta;
            }

            catch (Exception)
            {
                return Respuesta;
            }

        }

        public static string LeerCodigoParaUbicar(Control Contenedor, string CodLeido, string txtidArticulo, string txtNombre,
        string txtFechaVencimiento, string txtInfoCod, string txtLote, string txtUbicacionSugerida, string txtCantidad, string idUsuario, string TxtidarticuloERP)  //  = "TxtidarticuloERP"
        {  // TxtidarticuloERP -> parametro opcional.
            string respuesta = "";
            //string Cantidad = "";
            //string UbicSugerida = "";
            n_ProcesarSolicitud InfoArticulo = new n_ProcesarSolicitud();

            try
            {
                int Cant = 0;
                n_MotorDecisiones.Metodos MD = new n_MotorDecisiones.Metodos();
               
                if (CodLeido.Substring(0, 2) != "01")  // en caso de leer un GTIN13-GTIN14 se le pone adelante el 01 para poder procesar la recepción-ubicación-alisto.
                {
                    if (CodLeido.Length == 13)
                        CodLeido = "010" + CodLeido;
                    else if (CodLeido.Length == 14)
                        CodLeido = "01" + CodLeido;
                }

                string idCompania = ObtenerCompaniaXUsuario(idUsuario);

                respuesta = InfoArticulo.DevuelveInfoArticulo(CodLeido, idCompania);
                respuesta += ";" + CargarEntidadesGS1.GS1128_DevolverFechaVencimiento(CodLeido) + ";" + CargarEntidadesGS1.GS1128_DevolveLote(CodLeido);

            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(CodLeido))
                    respuesta = "No Procesado-Código en blanco...;0";
                else
                    respuesta = "No Procesado-" + ex.Message + ";0";
            }
              
            return respuesta;
        }

        public static string ObtenerGTIN14deGTIN(string GTIN)
        {
            string respuesta = String.Empty;
            try
            {
                string SQL = "select GTIN";
                return respuesta;
                    }
            catch (Exception)
            {
                return respuesta;
            }
        }

        public static string ObtenerEtiquetaIdUsuario(string idUbicacion, string idUsuario)
        {
            string SQL = "";
            string respuesta = "";
            try
            {
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);

                SQL = "SELECT " + e_VistaCodigosUbicacionFields.ETIQUETA() + " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaCodigosUbicacion() + " WHERE " + e_VistaCodigosUbicacionFields.idUbicacion() + " = '" + idUbicacion + "' AND " + e_VistaCodigosUbicacionFields.idCompania() + " = '" + idCompania + "'";
                respuesta = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                return respuesta;
            }
            catch (Exception)
            {
                respuesta = "No Procesado";
                return respuesta;
            }
        }

        public static string LeerCodigoParaUbicarHH(string CodLeido, string idUsuario, string idMetodoAccion)
        {
            string respuesta = "No Exitoso-";
            try
            {
            CodLeido = CodLeido.Trim();
            int Cant = 0;
            string idUbicacionCodLeido = "";
            string SQL = "";
            string idUbicacionCodLeidoMover = "";
            string validazonas = "";
            string OC = "";
            string SP = "";
            bool borraidUbicacionCodLeido = false;

            string idCompania = ObtenerCompaniaXUsuario(idUsuario);

            if (CodLeido.Contains(";"))
            {
                string[] spl = CodLeido.Split(';');
                CodLeido = spl[0].Trim();
                idUbicacionCodLeido = spl[1].Trim();   // esto se usa para el módulo de traslados entre ubicaciones de la bodega.

                if (string.IsNullOrEmpty(idUbicacionCodLeido))
                {
                    return "Ubicación actual nula o en blanco...";
                }

                if (idUbicacionCodLeido.Length >= 2)
                {
                    if (idUbicacionCodLeido.Substring(0, 2) == "OC")  // este se usa para el módulo de recepción para saber si el artículo pertenece o no a la orden de compra.
                    {
                        OC = idUbicacionCodLeido.Substring(2);
                        borraidUbicacionCodLeido = true;
                    }

                    if (idUbicacionCodLeido.Substring(0, 2) == "SP")  // este se usa para el módulo de pedidos para saber si el artículo pertenece o no a la solicitud de pedido.
                    {
                        SP = idUbicacionCodLeido.Substring(2);
                        borraidUbicacionCodLeido = true;
                    }
                }
             
             
                if (spl.Count() > 2)
                {
                    idUbicacionCodLeidoMover = spl[2].Trim();
                    if (Array.Exists(spl, element => (element == "1" || element == "0")))  // esta validación es para ver si el módulo de traslados verifica que las
                        validazonas = spl[3];                                              // ubicaciones involucradas son estrictamente de ALM y PIC o no.
              }
            }

            //CodLeido = CodLeido.Replace("-", "");

            if (borraidUbicacionCodLeido)
              idUbicacionCodLeido = "";


           //Se obtiene informacion del Codigo Leido
            n_MotorDecisiones.Metodos MD = new n_MotorDecisiones.Metodos();
            if (CodLeido.Substring(0, 2) != "01")  // en caso de leer un GTIN13-GTIN14 se le pone adelante el 01 para poder procesar la recepción-ubicación-alisto.
            {
                if (CodLeido.Length == 13)
                    CodLeido = "010" + CodLeido;
                else if (CodLeido.Length == 14)
                    CodLeido = "01" + CodLeido;
            }

            string[] Articulo = ObtenerIdArticuloNombreCodigoLeido_GS1128(CodLeido, idUsuario).Split(';');
            string idArticulo = Articulo[0];
            string NombreArticulo = Articulo[1];
            string FechaVencimiento = CargarEntidadesGS1.GS1128_DevolverFechaVencimiento(CodLeido);
            string Lote = CargarEntidadesGS1.GS1128_DevolveLote(CodLeido);
            string idZona = "";
            string Cantidad = CargarEntidadesGS1.GS1128_DevolverCantidad(CodLeido);
            string Peso = CargarEntidadesGS1.GS1128_DevolverPeso(CodLeido);
            string idUbicacion = CargarEntidadesGS1.GS1128_DevolveridUbicacion(CodLeido);
            if (string.IsNullOrEmpty(idUbicacion))
                idUbicacion = "0";

            bool EsNum = int.TryParse(Cantidad, out Cant);
            int idBodega = ObtenerBodegaXarticulo(idArticulo, idUsuario);

            string Zona_Recepcion = ConfigurationManager.AppSettings["Zona_Recepcion"].ToString();
            string Zona_Almacenamiento = ConfigurationManager.AppSettings["Zona_Almacenamiento"].ToString();
            string Zona_Picking = ConfigurationManager.AppSettings["Zona_Picking"].ToString();
            string Zona_Despacho = ConfigurationManager.AppSettings["Zona_Despacho"].ToString();
            string Zona_Quimicos = ConfigurationManager.AppSettings["Zona_Quimicos"].ToString();
            string Zona_No_convencionales = ConfigurationManager.AppSettings["Zona_No_convencionales"].ToString();
            string Zona_No_conformes = ConfigurationManager.AppSettings["Zona_No_conformes"].ToString();
            string Zona_Devoluciones = ConfigurationManager.AppSettings["Zona_Devoluciones"].ToString();
            string Zona_Cuarentena = ConfigurationManager.AppSettings["Zona_Cuarentena"].ToString();
            string Zona_Virtual = ConfigurationManager.AppSettings["Zona_Virtual"].ToString();
            string Banco_de_Alimento = ConfigurationManager.AppSettings["Banco_de_Alimentos"].ToString();
            string Centro_de_atencion_a_restaurantes = ConfigurationManager.AppSettings["Centro_de_atencion_a_restaurantes"].ToString();

           // Se quita esta porcion del codigo el 22-12-2016 por jgondres: e_TblEstadoTransaccionalFields.BaseDatos() + "." +
            SQL = "SELECT " + e_TblEstadoTransaccionalFields.BaseDatos() + "+ '.' +" + e_TblEstadoTransaccionalFields.idTabla() + " AS Tabla, ";
            SQL += e_TblEstadoTransaccionalFields.IdCampo() + ", " + e_TblEstadoTransaccionalFields.IdEstado() + ", " + e_TblEstadoTransaccionalFields.SumUno_RestaCero() + " FROM ";
            SQL += e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblEstadoTransaccional() + " WHERE " + e_TblEstadoTransaccionalFields.idMetodoAccion() + " = '" + idMetodoAccion + "'";
            DataSet DS1 = new DataSet();
            DS1 = n_ConsultaDummy.GetDataSet(SQL, idUsuario);

            if (DS1.Tables[0].Rows.Count > 0)
            {
                string TablaOrigen = DS1.Tables[0].Rows[0]["Tabla"].ToString();
                string IdCampo = DS1.Tables[0].Rows[0][e_TblEstadoTransaccionalFields.IdCampo()].ToString();

                string IdEstado = DS1.Tables[0].Rows[0][e_TblEstadoTransaccionalFields.IdEstado()].ToString();
                string SumUno_RestaCero = DS1.Tables[0].Rows[0][e_TblEstadoTransaccionalFields.SumUno_RestaCero()].ToString();

                SQL = "SELECT a." + e_TblDetalleTrasladoFields.idZona() + " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleTraslado() + " a ";
                SQL += " INNER JOIN " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTipoTraslado() + " b";
                SQL += " ON a." + e_TblDetalleTrasladoFields.idTipoTraslado() + " = b." + e_TblTipoTrasladoFields.idTipoTraslado() + " ";
                SQL += " WHERE b." + e_TblTipoTrasladoFields.idMetodoAccion() + " = '" + idMetodoAccion + "'";
                DataSet DS2 = new DataSet();
                DS2 = n_ConsultaDummy.GetDataSet(SQL, idUsuario);

                foreach (DataRow DR in DS2.Tables[0].Rows)
                {
                    idZona = DR[e_TblDetalleTrasladoFields.idZona()].ToString();

                    SQL = "SELECT " + e_TblZonasFields.Abreviatura() + " FROM " + e_TablasBaseDatos.TblZonas() + " WHERE " + e_TblZonasFields.idZona() + " = '" + idZona + "'";
                    string Abreviatura = n_ConsultaDummy.GetUniqueValue(SQL, idUbicacion);

                    if (string.IsNullOrEmpty(idUbicacion.Trim()))
                    {
                        idUbicacion = n_WMS.ObtenerUbicacionSugerida(Articulo[0], Cant, idUsuario, idZona);
                    }

                    string ValorIdCampo = "";
                    Single CantiPeso = 0.0F;

                    //if (idZona != "1")
                    if (Abreviatura != Zona_Recepcion)
                    {
                        SQL = "SELECT " + e_TblTransaccionFields.idRegistro() +
                              "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + " ";
                        SQL += " WHERE " + e_TblTransaccionFields.FechaVencimiento() + " = '" + FechaVencimiento + "' ";
                        SQL += "       AND " + e_TblTransaccionFields.Lote() + " = '" + Lote + "' ";
                        SQL += "       AND " + e_TblTransaccionFields.idArticulo() + " = " + idArticulo ;
                        //if (Peso == "0")
                        //    SQL += "       AND " + e_TblTransaccionFields.Cantidad() + " = " + Cantidad.Replace(",", ".");
                        //else
                        //{
                        //    CantiPeso = Single.Parse(Peso.Replace(",", "."));
                        //    Peso = (CantiPeso * 1000).ToString();
                        //    SQL += "       AND " + e_TblTransaccionFields.Cantidad() + " = " + Peso.Replace(",", ".");
                        //}
                        SQL += "       AND " + e_TblTransaccionFields.idUbicacion() + " = " + idUbicacion;
                        SQL += "       AND " + e_TblTransaccionFields.idEstado() + " = " + IdEstado;
                        SQL += " ORDER BY " + e_TblTransaccionFields.idRegistro() + " DESC";
                        DataSet DS = new DataSet();
                        DS = n_ConsultaDummy.GetDataSet(SQL, idUsuario);
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            ValorIdCampo = "";
                        }
                        else
                        {
                            ValorIdCampo = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                            if (string.IsNullOrEmpty(ValorIdCampo))
                            {
                                string Consulta = "SELECT TOP 1 " + e_TblTransaccionFields.idUbicacion() + 
                                                  "  FROM " + e_TablasBaseDatos.TblTransaccion() +
                                                  "  WHERE " + e_TblTransaccionFields.idArticulo() + " = '" + idArticulo + 
                                                  "' ORDER BY " + e_TblTransaccionFields.idRegistro() + " DESC";
                                string IdUbicacion2 = n_ConsultaDummy.GetUniqueValue(Consulta, idUsuario);

                                SQL = "SELECT " + e_TblTransaccionFields.idRegistro() + 
                                      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + " ";
                                SQL += " WHERE " + e_TblTransaccionFields.FechaVencimiento() + " = '" + FechaVencimiento + "' ";
                                SQL += " AND " + e_TblTransaccionFields.Lote() + " = '" + Lote + "' ";
                                SQL += " AND " + e_TblTransaccionFields.idArticulo() + " = '" + idArticulo + "' ";
                                SQL += " AND " + e_TblTransaccionFields.Cantidad() + " = '" + Cantidad + "' ";
                                SQL += " AND " + e_TblTransaccionFields.idUbicacion() + " = '" + IdUbicacion2 + "'";
                                SQL += " ORDER BY " + e_TblTransaccionFields.idRegistro() + " DESC";
                                ValorIdCampo = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                            }
                        }
                    }  

                    #region Agrega Transaccion detalle solicitud (Se Automatizó, ya no se usa)
                    /*
                     * ~/Operaciones/Salidas/wf_CrearSolicitud.aspx
                     * btnAprobar
                     */
                    if (idMetodoAccion == "28")
                    {
                        bool Articuloenpicking = false;
                        if (FechaVencimiento == "" && Lote == "")
                        {
                            SQL = "SELECT " + e_TblDetalleSolicitudFields.idLineaDetalleSolicitud() +
                                  "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleSolicitud() +
                                  "  WHERE " + e_TblDetalleSolicitudFields.idArticulo() + " = " + idArticulo +
                                  "        AND " + e_TblDetalleSolicitudFields.Cantidad() + " = " + Cantidad +
                                  "        AND " + e_TblDetalleSolicitudFields.IdCompania() + " = '" + idCompania + "'" +
                                  "        AND " + e_TblDetalleSolicitudFields.Procesado() + " = 0" +
                                  "        AND " + e_TblDetalleSolicitudFields.idMaestroSolicitud() + " = " + SP +
                                  "        AND CAST(" + e_TblDetalleSolicitudFields.idLineaDetalleSolicitud() + " AS NVARCHAR(10)) NOT IN (SELECT SUBSTRING(" + e_TblTransaccionFields.NumDocumentoAccion() + ",0,CHARINDEX('-'," + e_TblTransaccionFields.NumDocumentoAccion() + "))" +
                                                                                                                                      "      FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                                                                                                                      "      WHERE " + e_TblTransaccionFields.idTablaCampoDocumentoAccion() + " = '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleSolicitud() + "'" +
                                                                                                                                      "            AND " + e_TblTransaccionFields.idCampoDocumentoAccion() + " =  '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleSolicitud() + ".idlineadetallesolicitud')";
                            
                            TablaOrigen = e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleSolicitud();
                            ValorIdCampo = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);  // obtengo el idLineaDetalleSolicitud del artículo en el detalle de la solicitud.
                        }
                        else
                        {
                            ValorIdCampo = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                        }

                        // intento insertar en la tabla TRA si hay disponibilidad del articulo en Picking.
                        SQL = "SELECT " + e_TblZonasFields.idZona() +
                              "  FROM " + e_TablasBaseDatos.TblZonas() +
                              "  WHERE " + e_TblZonasFields.Abreviatura() + " = '" + Zona_Picking + "'";
                        idZona = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                        List<e_Ubicacion> Ubicaciones = new List<e_Ubicacion>(); 
                        Ubicaciones = ObtenerDisponibilidadArticulo(idArticulo, Cantidad, idUsuario, idZona);
                        foreach (e_Ubicacion Ubic in Ubicaciones)
                        {
                            foreach (e_Articulo _Articulo in Ubic.Articulos)
                            {
                                string FechaSQL = "";
                                if (Single.Parse(_Articulo.CantidadDisponible) > 0)
                                {
                                    SQL = "";
                                    SQL = "SELECT TOP 1 " + e_TblTransaccionFields.idRegistro() + "," + e_TblTransaccionFields.Lote() + "," + e_TblTransaccionFields.FechaVencimiento() +
                                          "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                          "  WHERE " + e_TblTransaccionFields.idArticulo() + " = " + idArticulo +
                                          "        AND " + e_TblTransaccionFields.Cantidad() + " = " + Cantidad +
                                          "        AND " + e_TblTransaccionFields.idEstado() + " = 12" +
                                          "        AND " + e_TblTransaccionFields.idUbicacion() + " IN (SELECT " + e_TblTransaccionFields.idUbicacion() +
                                                                                                       "      FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroUbicaciones() +
                                                                                                       "      WHERE " + e_TblMaestroUbicacion.idZona() + " = " + idZona + ")" +
                                          "        AND  CAST(" + e_TblTransaccionFields.idRegistro() + " AS NVARCHAR(10)) NOT IN (SELECT " + e_TblTransaccionFields.NumDocumentoAccion() +      //SUBSTRING(",CHARINDEX('-'," + e_TblTransaccionFields.NumDocumentoAccion() + ")+1,100)" +
                                                                                                             "      FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                                                                                             "      WHERE " + e_TblTransaccionFields.idArticulo() + " = " + idArticulo +
                                                                                                                             "            AND " + e_TblTransaccionFields.Cantidad() + " = " + Cantidad +
                                                                                                                             "            AND " + e_TblTransaccionFields.idEstado() + " = 14" +
                                                                                                                             "            AND " + e_TblTransaccionFields.idUbicacion() + " IN (SELECT " + e_TblTransaccionFields.idUbicacion() +
                                                                                                                                                                                         "       FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroUbicaciones() +
                                                                                                                                                                                         "       WHERE " + e_TblMaestroUbicacion.idZona() + " = " + idZona + ")" +
                                                                                                                             "    UNION " +
                                                                                                                             "    SELECT SUBSTRING(" + e_TblTransaccionFields.NumDocumentoAccion() + ",CHARINDEX('-'," + e_TblTransaccionFields.NumDocumentoAccion() + ")+1,100)" +
                                                                                                                             "      FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                                                                                                             "      WHERE " + e_TblTransaccionFields.idArticulo() + " = " + idArticulo +
                                                                                                                             "            AND " + e_TblTransaccionFields.Cantidad() + " = " + Cantidad +
                                                                                                                             "            AND " + e_TblTransaccionFields.idEstado() + " = 13" +
                                                                                                                             "            AND " + e_TblTransaccionFields.idUbicacion() + " IN (SELECT " + e_TblTransaccionFields.idUbicacion() +
                                                                                                                                                                                         "       FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroUbicaciones() +
                                                                                                                                                                                         "       WHERE " + e_TblMaestroUbicacion.idZona() + " = " + idZona + "))" +
                                          "   ORDER BY " + e_TblTransaccionFields.FechaVencimiento() + "," + e_TblTransaccionFields.Fecharegistro();

                                    DataSet ValorIdCampo2 = da_ConsultaDummy.GetDataSet(SQL, idUsuario);  //obtengo el idregistro,lote y fecha de vencimiento del artículo 
                                    // que esta ubicado en picking.
                                    if (string.IsNullOrEmpty(ValorIdCampo) || ValorIdCampo2.Tables[0].Rows.Count <= 0)
                                    {
                                        //respuesta = "Registro de Solicitud o de ubicación no encontrado...";
                                        //Articuloenpicking = true;
                                    break;
                                }

                                    Lote = ValorIdCampo2.Tables[0].Rows[0][1].ToString();
                                    string vic2 = ValorIdCampo2.Tables[0].Rows[0][0].ToString();
                                    DateTime fv = Convert.ToDateTime(ValorIdCampo2.Tables[0].Rows[0][2].ToString());
                                    string ano = fv.Year.ToString();
                                    string mes = fv.Month.ToString();
                                    string dia = fv.Day.ToString();
                                    FechaSQL = ano + "-" + mes + "-" + dia;

                                    respuesta = TransaccionMD(idArticulo, Cantidad, TablaOrigen, IdCampo, ValorIdCampo + "-" + vic2, "TABLA.INSERT", IdEstado, SumUno_RestaCero, idUsuario, idMetodoAccion, FechaSQL, Lote, Ubic.idUbicacion.ToString());
                                    Articuloenpicking = true;
                                    break;
                            }
                                else
                                    respuesta = "No hay cantidad disponible en Picking.";
                            }

                        }

                        if (!Articuloenpicking)
                        {
                        // intento insertar en la tabla TRA si hay disponibilidad del articulo en almacén. 
                         SQL = "SELECT " + e_TblZonasFields.idZona() +
                               "  FROM " + e_TablasBaseDatos.TblZonas() + 
                               "  WHERE " + e_TblZonasFields.Abreviatura() + " = '" + Zona_Almacenamiento + "'";
                         idZona = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                         Ubicaciones.Clear();
                         Ubicaciones = ObtenerDisponibilidadArticulo(idArticulo, Cantidad, idUsuario, idZona);
                         foreach (e_Ubicacion Ubic in Ubicaciones)
                         {
                            foreach (e_Articulo _Articulo in Ubic.Articulos)
                            {
                                    string FechaSQL = "";

                                if (Single.Parse(_Articulo.CantidadDisponible) > 0)
                                {
                                        SQL = "";
                                        SQL = "SELECT TOP 1 " + e_TblTransaccionFields.idRegistro() + "," + e_TblTransaccionFields.Lote() + "," + e_TblTransaccionFields.FechaVencimiento() +
                                              "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                              "  WHERE " + e_TblTransaccionFields.idArticulo() + " = " + idArticulo +
                                              "        AND " + e_TblTransaccionFields.Cantidad() + " = " + Cantidad +
                                              "        AND " + e_TblTransaccionFields.idEstado() + " = 12" +
                                              "        AND " + e_TblTransaccionFields.idUbicacion() + " IN (SELECT " + e_TblTransaccionFields.idUbicacion() +
                                                                                                           "      FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroUbicaciones() +
                                                                                                           "      WHERE " + e_TblMaestroUbicacion.idZona() + " = " + idZona + ")" +
                                              "        AND  CAST(" + e_TblTransaccionFields.idRegistro() + " AS NVARCHAR(10)) NOT IN (SELECT " + e_TblTransaccionFields.NumDocumentoAccion() +      //SUBSTRING(",CHARINDEX('-'," + e_TblTransaccionFields.NumDocumentoAccion() + ")+1,100)" +
                                                                                                                 "      FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                                                                                                 "      WHERE " + e_TblTransaccionFields.idArticulo() + " = " + idArticulo +
                                                                                                                                 "            AND " + e_TblTransaccionFields.Cantidad() + " = " + Cantidad +
                                                                                                                                 "            AND " + e_TblTransaccionFields.idEstado() + " = 14" +
                                                                                                                                 "            AND " + e_TblTransaccionFields.idUbicacion() + " IN (SELECT " + e_TblTransaccionFields.idUbicacion() +
                                                                                                                                                                                             "       FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroUbicaciones() +
                                                                                                                                                                                             "       WHERE " + e_TblMaestroUbicacion.idZona() + " = " + idZona + ")" +
                                                                                                                                 "    UNION " +
                                                                                                                                 "    SELECT SUBSTRING(" + e_TblTransaccionFields.NumDocumentoAccion() + ",CHARINDEX('-'," + e_TblTransaccionFields.NumDocumentoAccion() + ")+1,100)" +
                                                                                                                                 "      FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                                                                                                                 "      WHERE " + e_TblTransaccionFields.idArticulo() + " = " + idArticulo +
                                                                                                                                 "            AND " + e_TblTransaccionFields.Cantidad() + " = " + Cantidad +
                                                                                                                                 "            AND " + e_TblTransaccionFields.idEstado() + " = 13" +
                                                                                                                                 "            AND " + e_TblTransaccionFields.idUbicacion() + " IN (SELECT " + e_TblTransaccionFields.idUbicacion() +
                                                                                                                                                                                             "       FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroUbicaciones() +
                                                                                                                                                                                             "       WHERE " + e_TblMaestroUbicacion.idZona() + " = " + idZona + "))" +
                                        "   ORDER BY " + e_TblTransaccionFields.FechaVencimiento() + "," + e_TblTransaccionFields.Fecharegistro();

                                        DataSet ValorIdCampo2 = da_ConsultaDummy.GetDataSet(SQL, idUsuario);  //obtengo el idregistro,lote y fecha de vencimiento del artículo 
                                        // que esta ubicado en almacén.
                                        if (string.IsNullOrEmpty(ValorIdCampo) || ValorIdCampo2.Tables[0].Rows.Count <= 0)
                                        {
                                            respuesta = "Registro de Solicitud o de ubicación no encontrado...";
                                    break;
                                }

                                        Lote = ValorIdCampo2.Tables[0].Rows[0][1].ToString();
                                        string vic2 = ValorIdCampo2.Tables[0].Rows[0][0].ToString();
                                        DateTime fv = Convert.ToDateTime(ValorIdCampo2.Tables[0].Rows[0][2].ToString());
                                        string ano = fv.Year.ToString();
                                        string mes = fv.Month.ToString();
                                        string dia = fv.Day.ToString();
                                        FechaSQL = ano + "-" + mes + "-" + dia;
                                        respuesta = TransaccionMD(idArticulo, Cantidad, TablaOrigen, IdCampo, ValorIdCampo + "-" + vic2, "TABLA.INSERT", IdEstado, SumUno_RestaCero, idUsuario, idMetodoAccion, FechaSQL, Lote, Ubic.idUbicacion.ToString());
                                        break;
                            }
                                    else
                                        respuesta += "-No hay cantidad disponible en Almacén.";
                                }
                         }
                    }
                    }

                    #endregion Agrega Transaccion detalle solicitud (Se Automatizó, ya no se usa)

                    #region Recibir Producto

                    /*
                     * ~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx
                     * btnAccion32--- Recepción de artículos.
                     */
                        if (idMetodoAccion == "55")
                        {
                            string respuesta2 = "";
                            string Sufijo = "";
                            string Mensajevidautil = "";
                            Single saldo = 0.0F;
                            Single Sumrecibidos = 0.0F;
                            Single CantidadOCoriginal = 0.0F;
                            EntidadesGS1.e_GTIN GTIN = new EntidadesGS1.e_GTIN();
                            DataSet DS = new DataSet();
                            bool EsGTIN = CargarEntidadesGS1.GS1128_ObtenerGTIN(CodLeido, out GTIN);

                           //Descomentar esto cuando se realiza la sincronizacion 
                            //if (!ValidarArticuloProveerdor(idArticulo, OC))
                            //{
                            //    return respuesta = "Este proveedor no tiene autorización de despachar este articulo";
                            //}  

                           // obtengo los días de mínima vida útil.
                            SQL = "";
                            SQL = "SELECT " + e_TblMaestroArticulosFields.DiasMinimosVencimiento() +
                                  "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroArticulos() +
                                  "  WHERE " + e_TblMaestroArticulosFields.idArticulo() + " = " + idArticulo; // +
                                  //"        AND " + e_TblMaestroArticulosFields.Granel() + " = 0";
                            string diasminimos = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario); // me trae días mínimos de vencimiento si el artículo no es granel.

                            if (int.Parse(diasminimos) > 0)
                            {
                                // evalua mínima vida útil.
                                if (string.IsNullOrEmpty(FechaVencimiento))
                                {
                                    respuesta2 = "Valor de fecha de vencimiento nula o vacía.... intente de nuevo";
                                }
                                else
                                {
                                    string Mensajeminimavidautil = validaDiasminimosvencimientoArticulo(idArticulo, FechaVencimiento, idUsuario);
                                    string[] Evaluavidautil = Mensajeminimavidautil.Split(';');
                                    if (Evaluavidautil[1] == "False")
                                    {
                                        //    // estas lineas es para determinar si se rechazar o no el producto, por la cantidad.
                                        //    n_ProcesarRecepcionUbicacion rechazo = new n_ProcesarRecepcionUbicacion();
                                        //    string[] resulrechaza = rechazo.TotalArticuloyPendienteOC(Int64.Parse(idArticulo), Int64.Parse(OC)).Split(';');
                                        //    int porrechazar = int.Parse(resulrechaza[1]);
                                        //    if (porrechazar > 0 && porrechazar > GTIN.VLs[0].Cantidad)
                                        //    {
                                        //        string queryInsertRechasados = "INSERT INTO " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TBLOPEArticulosRechazadosOC() +
                                        //                                                                " (" + e_TBLOPEArticulosRechazadosOCField.idCompania() + "," +
                                        //                                                                       e_TBLOPEArticulosRechazadosOCField.idUsuario() + "," +
                                        //                                                                       e_TBLOPEArticulosRechazadosOCField.idMaestroOrdenCompra() + "," +
                                        //                                                                       e_TBLOPEArticulosRechazadosOCField.idArticulo() + "," +
                                        //                                                                       e_TBLOPEArticulosRechazadosOCField.Cantidad() + "," +
                                        //                                                                       e_TBLOPEArticulosRechazadosOCField.Lote() + "," +
                                        //                                                                       e_TBLOPEArticulosRechazadosOCField.FechaVencimiento() + ") " +
                                        //                                                      "VALUES('" + idCompania + "'," +
                                        //                                                                   Convert.ToInt32(idUsuario) + "," +
                                        //                                                                   Convert.ToInt32(OC) + "," +
                                        //                                                                   idArticulo + "," +
                                        //                                                                   GTIN.VLs[0].Cantidad.ToString() + ",'" + Lote + "'," +
                                        //                                                                   "CONVERT(NVARCHAR(10),CAST('" + FechaVencimiento + "' AS DATE),112))";

                                        //        respuesta2 = Evaluavidautil[0] + ";" + Evaluavidautil[1] + ";" + Evaluavidautil[2];
                                        //        if (GTIN.VLs[0].Cantidad > 1)
                                        //        {
                                        //            string codigoLeido = OC + ";" + idArticulo + ";" + "9" + ";" + "2" + ";" + "GTIN 14(" + GTIN.VLs[0].Cantidad.ToString() + ")" + respuesta2.Replace(";", "-") + ";" + "---" + ";" + "0" + ";" + "---" + ";" + "False" + ";" + CodLeido + ";" + "-";
                                        //            AgregarFormularioCalidad(codigoLeido, idUsuario);
                                        //            da_ConsultaDummy.PushData(queryInsertRechasados, idUsuario);
                                        //        }
                                        //        else
                                        //        {
                                        //            string codigoLeido = OC + ";" + idArticulo + ";" + "9" + ";" + "2" + ";" + respuesta2.Replace(";", "-") + ";" + "---" + ";" + "0" + ";" + "---" + ";" + "False" + ";" + CodLeido + ";" + "-";
                                        //            AgregarFormularioCalidad(codigoLeido, idUsuario);
                                        //            da_ConsultaDummy.PushData(queryInsertRechasados, idUsuario);
                                        //        }
                                        //    }

                                        Mensajevidautil = Evaluavidautil[0] +"-" + Evaluavidautil[2];
                                        //n_ProcesarRecepcionUbicacion Contador = new n_ProcesarRecepcionUbicacion();
                                        //string[] resultado2 = Contador.TotalArticuloyPendienteOC(Int64.Parse(idArticulo), Int64.Parse(OC)).Split(';');
                                        //respuesta += "|" + resultado2[0] + "|" + resultado2[1] + "|" + resultado2[2];
                                        //break;
                                    }
                                }
                            }

                            // se cambia de int a single por el cambio de int a single en e_ValorLogistico
                            Single CantidadLineas = 0;
                            Single Cantisingle = Single.Parse(Cantidad);
                            Cantidad = Cantisingle.ToString();
                            bool esGranel = CargarEntidadesGS1.GS1128_EsArticuloGranel(CodLeido);
                            if (esGranel)
                            {  
                                // en caso de que sea un artículo a granel, se ingresa el peso ingresado por el operario, en Kilos y se guarsa en gramos.
                                int equivKggr = 1000;  // int.Parse(ConfigurationManager.AppSettings["EquivalenciaKg-gr"].ToString()); // equivalencia Kg-gr
                                Cantisingle = Single.Parse(Peso, System.Globalization.CultureInfo.InvariantCulture);
                                Cantidad = (Cantisingle*equivKggr).ToString();  // obtenemos el peso en gramos.
                                CantidadLineas = 1;
                                saldo = 0;  // Single.Parse(Cantidad); // en caso de que haya decimales con esta variable se registran correctamente los decimales.
                            }

                            n_ProcesarRecepcionUbicacion recepcion = new n_ProcesarRecepcionUbicacion();
                            string [] resultado = recepcion.ProcesaRecepcion(Int64.Parse(OC), 
                                                                             Int64.Parse(idArticulo),
                                                                             Int64.Parse(Cantidad),
                                                                             int.Parse(idUsuario),
                                                                             idCompania,
                                                                             Zona_Recepcion, 
                                                                             idBodega,
                                                                             long.Parse(idMetodoAccion),
                                                                             FechaVencimiento,
                                                                             Lote).Split(';');

                            if (resultado[0].Contains("TRANSACCIÓN EXITOSA"))
                            {
                                string ETIQUETA = ObtenerEtiquetaIdUsuario(resultado[3], idUsuario);
                                respuesta = resultado[0] + " \n" + ETIQUETA + "|" + resultado[1] + "|" + resultado[2] + "|" + resultado[4] + "|" + Mensajevidautil;
                            }
                            else
                                respuesta += resultado[0];
                        }

                    #endregion Recibir Producto

                    #region Ubicar Producto WEB

                    /*
                     * ~/Operaciones/Ingresos/wf_UbicarArticulo.aspx--ya no se usa.
                     * bntAccion22
                     */
                        if (idMetodoAccion == "43")
                        {
                            string respuesta2 = "";
                            EntidadesGS1.e_GTIN GTIN = new EntidadesGS1.e_GTIN();
                            bool EsGTIN = CargarEntidadesGS1.GS1128_ObtenerGTIN(CodLeido, out GTIN);
                            if (EsGTIN)   // es un GTIN valido
                            {
                                // se cambia de int a single por el cambio de int a single en e_ValorLogistico
                                Single CantidadLineas = 0;
                                CantidadLineas = GTIN.VLs[0].Cantidad;  //  si es un GTIN14, las lineas son la cantidad del GTIN13 que representa.
                                Cantidad = GTIN.VL.Cantidad.ToString(); // se cambia la cantidad a insertar por la equivalencia registrada en el maestro de artículos. 
                                for (int Linea = 0; Linea < CantidadLineas; Linea++)
                                {
                                    ValorIdCampo = ObtenerValorIdCampo("55", FechaVencimiento, Lote, idArticulo, Cantidad, idUsuario);
                                    respuesta = TransaccionMD(idArticulo, Cantidad, TablaOrigen, IdCampo, ValorIdCampo, "TABLA.INSERT", IdEstado, SumUno_RestaCero, idUsuario, idMetodoAccion, FechaVencimiento, Lote, idUbicacion.ToString());
                                }
                            }
                            else
                                respuesta = "GTIN no es valido...";
                        }

                        if (idMetodoAccion == "57")  // ya no se usa
                        {
                            string respuesta2 = "";
                            ValorIdCampo = ObtenerValorIdCampo("43", FechaVencimiento, Lote, idArticulo, Cantidad, idUsuario);
                            if (ValorIdCampo == "") // si no devuelve un valor valido, valida si la transacción 43 ya está registrada y determinar si el artículo
                            {                        //  ya fue ubicado y/o recibido.
                                string SQL1, SQL2, VIC1, VIC2;
                                SQL1 = "SELECT " + e_TblTransaccionFields.idRegistro() +
                                       " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                       " WHERE " + e_TblTransaccionFields.FechaVencimiento() + " = '" + FechaVencimiento +
                                       "' AND " + e_TblTransaccionFields.Lote() + " = '" + Lote +
                                       "' AND " + e_TblTransaccionFields.idArticulo() + " = '" + idArticulo +
                                       "' AND " + e_TblTransaccionFields.Cantidad() + " = '" + Cantidad +
                                       "' AND " + e_TblTransaccionFields.idMetodoAccion() + " = '43'";
                                VIC1 = da_ConsultaDummy.GetUniqueValue(SQL1, idUsuario);
                                 
                                SQL2 = "SELECT 1 FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                      " WHERE " + e_TblTransaccionFields.NumDocumentoAccion() + " = '" + VIC1 + "' and " + e_TblTransaccionFields.idTablaCampoDocumentoAccion() + " = '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + "TRAIngresoSalidaArticulos'" +
                                      "         AND " + e_TblTransaccionFields.idCampoDocumentoAccion() + " = '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + "TRAIngresoSalidaArticulos.idRegistro'";
                                VIC2 = da_ConsultaDummy.GetUniqueValue(SQL2, idUsuario);

                                if (VIC1 != "" && VIC2 == "1")
                                    respuesta2 = "Artículo ya fue ubicado.";

                                if (VIC1 == "" && VIC2 == "")
                                    respuesta2 = "Artículo no ha sido recibido.";
                            }

                            respuesta = TransaccionMD(idArticulo, Cantidad, TablaOrigen, IdCampo, ValorIdCampo, "TABLA.INSERT", IdEstado, SumUno_RestaCero, idUsuario, idMetodoAccion, FechaVencimiento, Lote, idUbicacion.ToString());
                            respuesta += respuesta2;
                        }

                    #endregion Ubicar Producto WEB

                    #region Ubicar Producto HH

                    /*
                     * ~/HH/Operaciones/Ingresos/wf_UbicarArticulo.aspx
                     * bntAccion22
                     */
                        if (idMetodoAccion == "58")
                        {  
                              
                        }

                        if (idMetodoAccion == "59")
                        {
                            EntidadesGS1.e_GTIN GTIN = new EntidadesGS1.e_GTIN();
                            bool EsGTIN = CargarEntidadesGS1.GS1128_ObtenerGTIN(CodLeido, out GTIN);
                            decimal Cantisingle = decimal.Parse(Cantidad);
                            Cantidad = Cantisingle.ToString("G", System.Globalization.CultureInfo.InvariantCulture);
                            bool esgranel = CargarEntidadesGS1.GS1128_EsArticuloGranel(CodLeido);
                            if (esgranel)  // si es un artículo a granel se insertan las lineas según la cantidad que tenga el código GS1.
                            {
                                // en caso de que sea un artículo a granel, se ingresa el peso ingresado por el operario, en Kilos y se guarsa en gramos.
                                int equivKggr = 1000;   //int.Parse(ConfigurationManager.AppSettings["EquivalenciaKg-gr"].ToString()); // equivalencia Kg-gr
                                Cantisingle = decimal.Parse(Peso, System.Globalization.CultureInfo.InvariantCulture) * equivKggr;
                                Cantidad = Cantisingle.ToString("G", System.Globalization.CultureInfo.InvariantCulture).Replace(".00","");   //.Replace(",00","");  // obtenemos el peso en gramos.
                                if (Cantisingle == 0)
                                {
                                    respuesta = "Proceso no exitoso-Debe ser etiqueta con peso." + "|0|0|0";
                                    return respuesta;
                                }
                            }
                            // esto se hace para quitarle decimales innecesarios a la variable Cantidad... y no brinque un error.
                            Cantidad = Cantisingle.ToString("G", System.Globalization.CultureInfo.InvariantCulture);
                            int raya = Cantidad.IndexOf(".");
                            if (raya > 0)
                            {
                                Cantidad = Cantidad.Substring(0, raya).Trim();
                            }
                            n_ProcesarRecepcionUbicacion ubicacion = new n_ProcesarRecepcionUbicacion();
                            string[] resultado = ubicacion.ProcesaUbicacion(Int64.Parse(idArticulo),
                                                                             Int64.Parse(Cantidad),
                                                                             int.Parse(idUsuario),
                                                                             idCompania,
                                                                             Zona_Picking,
                                                                             Zona_Almacenamiento,
                                                                             58,
                                                                             FechaVencimiento,
                                                                             Lote,
                                                                             idUbicacionCodLeido).Split(';');
                            if (resultado[0].Contains("TRANSACCIÓN EXITOSA"))
                            {
                                respuesta = resultado[0] + " \n" + "|" + resultado[1] + "|" + resultado[2] + "|" + resultado[3];
                            }
                            else
                                respuesta += resultado[0];
                        }

                    #endregion Ubicar Producto HH

                    #region Alistar Producto
                    /*
                     * ~/HH/Operaciones/Salidas/wf_CrearAlisto.aspx
                     * btnAccion11
                     */
                        if (idMetodoAccion == "62")  // asociar SSCC con ubicación de despacho.(saca de picking)
                        {
                            string idRegistroSSCC = idUbicacionCodLeidoMover;  //  se hace este "cambio" de variables para buenas practicas de programación.
                            string UbicaTran = "";
                            string x = "";
                            SQL = "";
                            SQL = "SELECT " + e_TblTransaccionFields.idUbicacion() +
                                             "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                             "  WHERE " + e_TblTransaccionFields.idRegistro() + " = '" + idRegistroSSCC + "'";
                            idUbicacion = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                            if (!string.IsNullOrEmpty(idUbicacion))
                            {
                                 if (CantiPeso > 0)
                                     respuesta = TransaccionMD(idArticulo, (CantiPeso*1000).ToString().Replace(",","."), TablaOrigen, IdCampo, idRegistroSSCC, "TABLA.INSERT", IdEstado, SumUno_RestaCero, idUsuario, idMetodoAccion, FechaVencimiento, Lote, idUbicacion);
                                 else
                                     respuesta = TransaccionMD(idArticulo, Cantidad, TablaOrigen, IdCampo, idRegistroSSCC, "TABLA.INSERT", IdEstado, SumUno_RestaCero, idUsuario, idMetodoAccion, FechaVencimiento, Lote, idUbicacion);
                                 

                                    if (respuesta.Equals("Transaccion exitosa"))
                                    {
                                        SQL = "SELECT " + e_VistaCodigosUbicacionFields.idUbicacion() +
                                                  "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaCodigosUbicacion() +
                                                  "  WHERE " + e_VistaCodigosUbicacionFields.idCompania() + " = '" + idCompania + "'" +
                                                  "        AND replace((replace(" + e_VistaCodigosUbicacionFields.ETIQUETA() + ",'(','')),')','') = '" + idUbicacionCodLeido + "'";

                                        UbicaTran = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);  // se obtiene el ID ubicación a partir de la etiqueta pistoleada.
                                        string idUbicacionTem = UbicaTran;

                                        CodLeido = CodLeido + ";" + UbicaTran;

                                        LeerCodigoParaUbicarHH(CodLeido, idUsuario, "63");

                                        string ETIQUETA = ObtenerEtiquetaIdUsuario(idUbicacionTem, idUsuario);
                                        respuesta = respuesta + " \n" + ETIQUETA;
                                    }
                                }
                                else
                                {
                                    respuesta = "Ubicación, PIC-ALM, no válida...";
                                }
                        }


                        if (idMetodoAccion == "63")   // los deja con estatus de alistados.
                        {
                            if (ValorIdCampo == "")
                            {
                                if (CantiPeso > 0)
                                    Cantidad = (CantiPeso * 1000).ToString().Replace(",", ".");

                                SQL = "SELECT " + e_TblTransaccionFields.idRegistro() + " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + " WHERE FechaVencimiento = '" + FechaVencimiento + "' AND Lote = '" + Lote + "' AND idArticulo = '" + idArticulo + "'  AND Cantidad = '" + Cantidad + "' AND idMetodoAccion = '62' AND idRegistro NOT IN (SELECT NumDocumentoAccion  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + " WHERE idTablaCampoDocumentoAccion = '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + "' AND idCampoDocumentoAccion = '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + ".idRegistro')";

                                ValorIdCampo = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                            }

                            idUbicacion = idUbicacionCodLeido;
                            respuesta = TransaccionMD(idArticulo, Cantidad, TablaOrigen, IdCampo, ValorIdCampo, "TABLA.INSERT", IdEstado, SumUno_RestaCero, idUsuario, idMetodoAccion, FechaVencimiento, Lote, idUbicacion.ToString());

                        }
                    #endregion Alistar Producto

                    #region Procesar Despacho
                    
                     //   ~/HH/Operaciones/Salidas/wf_ProcesarDespacho.aspx
                     //   btnAccion31
                     
                        if (idMetodoAccion == "66")  // aprueba despacho
                        {
                            string Consulta = "SELECT TOP 1 " + e_TblTransaccionFields.idUbicacion() + 
                                              "  FROM " + e_TablasBaseDatos.TblTransaccion() +
                                              "  WHERE " + e_TblTransaccionFields.FechaVencimiento() + " = '" + FechaVencimiento + "'" +
                                              "        AND " + e_TblTransaccionFields.Lote() + " = '" + Lote + "'" +
                                              "        AND " + e_TblTransaccionFields.idArticulo() + " = '" + idArticulo + "'" +
                                              "        AND " + e_TblTransaccionFields.Cantidad() + " = " + Cantidad +
                                              "        AND " + e_TblTransaccionFields.idMetodoAccion() + " = 63" +
                                              "  ORDER BY  " + e_TblTransaccionFields.idRegistro() + " DESC";
                            string idubica = n_ConsultaDummy.GetUniqueValue(Consulta, idUsuario);

                            SQL = "";
                            SQL = "SELECT " + e_TblTransaccionFields.idRegistro() +
                                  "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                  "  WHERE " + e_TblTransaccionFields.FechaVencimiento() + " = '" + FechaVencimiento + "'" +
                                  "        AND " + e_TblTransaccionFields.Lote() + " = '" + Lote + "'" +
                                  "        AND " + e_TblTransaccionFields.idArticulo() + " = '" + idArticulo + "'" +
                                  "        AND " + e_TblTransaccionFields.Cantidad() + " = " + Cantidad +
                                  "        AND " + e_TblTransaccionFields.idUbicacion() + " = '" + idubica + "'" +
                                  "        AND " + e_TblTransaccionFields.idMetodoAccion() + " = 63" +
                                  "        AND " + e_TblTransaccionFields.idRegistro() + " NOT IN (SELECT " + e_TblTransaccionFields.NumDocumentoAccion() +
                                  "                                                                  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                  "                                                                  WHERE " + e_TblTransaccionFields.idTablaCampoDocumentoAccion() + " = '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + "'" +
                                  "                                                                        AND " + e_TblTransaccionFields.idCampoDocumentoAccion() + " = '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + ".idregistro')";

                            ValorIdCampo = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                            idUbicacion = idubica;
                            respuesta = TransaccionMD(idArticulo, Cantidad, TablaOrigen, IdCampo, ValorIdCampo, "TABLA.INSERT", IdEstado, SumUno_RestaCero, idUsuario, idMetodoAccion, FechaVencimiento, Lote, idUbicacion.ToString());
                        }

                        if (idMetodoAccion == "67")  // despacho efectivo.??
                        {
                            //if (idZona == "6" && string.IsNullOrEmpty(idUbicacion))
                            SQL = "";
                            SQL = "SELECT " + e_VistaCodigosUbicacionFields.idUbicacion() + 
                                  "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaCodigosUbicacion() +
                                  "  WHERE " + e_VistaCodigosUbicacionFields.CODUBI() + " LIKE '%DES%'"; 
                            idUbicacion = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario); 

                            if (string.IsNullOrEmpty(idUbicacion))
                              idUbicacion = "1618";

                            SQL = "";
                            SQL = "SELECT " + e_TblTransaccionFields.idRegistro() +
                                  "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + 
                                  "  WHERE " + e_TblTransaccionFields.FechaVencimiento() + " = '" + FechaVencimiento + "'" +
                                  "        AND " + e_TblTransaccionFields.Lote() + " = '" + Lote + "'" +
                                  "        AND " + e_TblTransaccionFields.idArticulo() + " = '" + idArticulo + "'" +
                                  "        AND " + e_TblTransaccionFields.idMetodoAccion() + " = 66" +
                                  "  ORDER BY " + e_TblTransaccionFields.idRegistro() + " DESC";
                            ValorIdCampo = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);  // se obtiene el idregistro del traslado anterior.

                            respuesta = TransaccionMD(idArticulo, Cantidad, TablaOrigen, IdCampo, ValorIdCampo, "TABLA.INSERT", IdEstado, SumUno_RestaCero, idUsuario, idMetodoAccion, FechaVencimiento, Lote, idUbicacion.ToString());
                        }

                        if (idMetodoAccion == "75")  // Metodo acción que hace los traslados, este es de salida.
                        {
                           // en caso de que el traslado se deba a una devolución de artículo del SSCC, se hace esta validación para obterner el idregistro correcto.
                            n_Traslados PrTraslados = new n_Traslados();
                            string Mensaje2 = "";
                            if (validazonas == "1")
                            {
                             //este bloque es para determinar si las ubicaciones estan en zona 3 o 4,  si no es así sale del proceso.
                              SQL = "SELECT Idzona from dbo.Vista_CodigosMaestroUbicacion WHERE idCompania = '" + idCompania + "' AND replace((replace(etiqueta,'(','')),')','') = '" + idUbicacionCodLeidoMover + "'";
                              string idzonaM = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);   // se obtiene el IdZona a partir de la etiqueta pistoleada de la ubicación.

                              SQL = "SELECT " + e_TblZonasFields.Abreviatura() + " FROM " + e_TablasBaseDatos.TblZonas() + " WHERE " + e_TblZonasFields.idZona() + " = '" + idzonaM + "'";
                              string AbreviaturaM = n_ConsultaDummy.GetUniqueValue(SQL, idUbicacion);

                              SQL = "select Idzona from dbo.Vista_CodigosMaestroUbicacion where idCompania = '" + idCompania + "' AND replace((replace(etiqueta,'(','')),')','') = '" + idUbicacionCodLeido + "'";
                              string idzonaA = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);   // se obtiene el IdZona a partir de la etiqueta pistoleada de la ubicación.

                              SQL = "SELECT " + e_TblZonasFields.Abreviatura() + " FROM " + e_TablasBaseDatos.TblZonas() + " WHERE " + e_TblZonasFields.idZona() + " = '" + idzonaA + "'";
                              string AbreviaturaA = n_ConsultaDummy.GetUniqueValue(SQL, idUbicacion);

                              if ((AbreviaturaM != Zona_Almacenamiento && 
                                    AbreviaturaM != Zona_Picking && 
                                    AbreviaturaM != Zona_Quimicos && 
                                    AbreviaturaM != Zona_No_convencionales && 
                                    AbreviaturaM != Zona_No_conformes &&
                                    AbreviaturaM != Zona_Devoluciones &&
                                    AbreviaturaM != Zona_Cuarentena &&
                                    AbreviaturaM != Zona_Virtual &&
                                    AbreviaturaM != Banco_de_Alimento &&
                                    AbreviaturaM != Centro_de_atencion_a_restaurantes) && (idMetodoAccion == "75" || idMetodoAccion == "76"))
                              {
                                respuesta = "La Ubicación a mover no es permitida en este proceso...";
                                return respuesta;
                              }

                              if ((AbreviaturaA != Zona_Almacenamiento &&
                                    AbreviaturaA != Zona_Picking &&
                                    AbreviaturaA != Zona_Quimicos &&
                                    AbreviaturaA != Zona_No_convencionales &&
                                    AbreviaturaA != Zona_No_conformes &&
                                    AbreviaturaA != Zona_Devoluciones &&
                                    AbreviaturaA != Zona_Cuarentena &&
                                    AbreviaturaA != Zona_Virtual && 
                                    AbreviaturaA != Banco_de_Alimento &&
                                    AbreviaturaA != Centro_de_atencion_a_restaurantes) && (idMetodoAccion == "75" || idMetodoAccion == "76"))
                              {
                                respuesta = "La Ubicación actual no es permitida en este proceso...";
                                return respuesta;
                              }

                            }
                           //termina bloque de verificación de zonas.

                            SQL = "SELECT " + e_VistaCodigosUbicacionFields.idUbicacion() +
                                  "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaCodigosUbicacion() +
                                  "  WHERE " + e_VistaCodigosUbicacionFields.idCompania() + " = '" + idCompania + "'" +
                                  "        AND replace((replace(" + e_VistaCodigosUbicacionFields.ETIQUETA() + ",'(','')),')','') = '" + idUbicacionCodLeido + "'";
                            string idUbicacion75 = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);   // se obtiene el IdUbicacion a partir de la etiqueta pistoleada de la ubicación.

                            if (Lote == "")  // si el valor para Lote viene en blanco se debe verificar si realmente es: NULL, "NULL" o vacío.
                            {
                                SQL = "SELECT " + e_TblTransaccionFields.Lote() +
                                      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                      "  WHERE " + e_TblTransaccionFields.FechaVencimiento() + " = '" + FechaVencimiento + "'" +
                                      "        AND " + e_TblTransaccionFields.idArticulo() + " = '" + idArticulo + "'" +
                                      "        AND " + e_TblTransaccionFields.idUbicacion() + " = '" + idUbicacion75 + "'" +
                                      "        AND " + e_TblTransaccionFields.idEstado() + " = '12' ";
                                Lote = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                            }

                             SQL = "SELECT IdUbicacion FROM Vista_CodigosMaestroUbicacion" +
                                   "  WHERE idCompania = '" + idCompania + "'" +
                                   "        AND replace((replace(etiqueta,'(','')),')','') = '" + idUbicacionCodLeidoMover + "'";
                             string idUbicacion76 = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario); // se obtiene el IdUbicacion a partir de la etiqueta pistoleada de la ubicación.

                             EntidadesGS1.e_GTIN GTIN = new EntidadesGS1.e_GTIN();
                             bool EsGTIN = CargarEntidadesGS1.GS1128_ObtenerGTIN(CodLeido, out GTIN);
                             decimal Cantisingle = decimal.Parse(Cantidad);
                             Cantidad = Cantisingle.ToString("G", System.Globalization.CultureInfo.InvariantCulture);
                             bool esgranel = CargarEntidadesGS1.GS1128_EsArticuloGranel(CodLeido);
                             if (esgranel)  // si es un artículo a granel se insertan las lineas según la cantidad que tenga el código GS1.
                             {
                                 // en caso de que sea un artículo a granel, se ingresa el peso ingresado por el operario, en Kilos y se guarsa en gramos.
                                 int equivKggr = 1000;   //int.Parse(ConfigurationManager.AppSettings["EquivalenciaKg-gr"].ToString()); // equivalencia Kg-gr
                                 Cantisingle = decimal.Parse(Peso, System.Globalization.CultureInfo.InvariantCulture) * equivKggr;
                                 Cantidad = Cantisingle.ToString("G", System.Globalization.CultureInfo.InvariantCulture).Replace(".00", "");   //.Replace(",00","");  // obtenemos el peso en gramos.
                                 if (Cantisingle == 0)
                                 {
                                     respuesta = "Proceso no exitoso-Debe ser etiqueta con peso." + "|0|0|0";
                                     return respuesta;
                                 }
                             }
                             // esto se hace para quitarle decimales innecesarios a la variable Cantidad... y no brinque un error.
                             Cantidad = Cantisingle.ToString("G", System.Globalization.CultureInfo.InvariantCulture);
                             int raya = Cantidad.IndexOf(".");
                             if (raya > 0)
                             {
                                 Cantidad = Cantidad.Substring(0, raya).Trim();
                             }

                            string mensaje = PrTraslados.ProcesarTraslados(Int64.Parse(idArticulo)
														                  ,Lote
														                  ,FechaVencimiento
														                  ,Int64.Parse(idUbicacion75)
														                  ,Int64.Parse(idUbicacion76)
														                  ,Int64.Parse(Cantidad)
														                  ,int.Parse(idUsuario)
                                                                          ,int.Parse(idMetodoAccion)
                                                                          ,76
                                                                          );
                            respuesta = mensaje;

                        }

                        if (idMetodoAccion == "76")   // Metodo acción que hace los traslados, este es de entrada.   && (IdMetodoaccionanterior == "75"))
                        {
                            
                            SQL = "SELECT IdUbicacion FROM dbo.Vista_CodigosMaestroUbicacion WHERE idCompania = '" + idCompania + "' AND replace((replace(etiqueta,'(','')),')','') = '" + idUbicacionCodLeido + "'";
                            string idUbicacion76 = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario); // se obtiene el IdUbicacion a partir de la etiqueta pistoleada de la ubicación.

                            if (Lote == "")  // si el valor para Lote viene en blanco se debe verificar si realmente es: NULL, "NULL" o vacío.
                            {
                                SQL = "SELECT lote FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + " WHERE FechaVencimiento = '" + FechaVencimiento + "'  AND idArticulo = '" + idArticulo + "' AND idMetodoAccion = '75' AND idEstado = '14' ";
                                Lote = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                            }

                            SQL = "SELECT idRegistro FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + " WHERE FechaVencimiento = '" + FechaVencimiento + "'  AND Lote = '" + Lote + "' AND idArticulo = '" + idArticulo + "' AND idmetodoaccion = '75' order by idRegistro desc";
                            ValorIdCampo = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);  // se obtiene el idregistro del traslado anterior.

                            EntidadesGS1.e_GTIN GTIN = new EntidadesGS1.e_GTIN();
                            bool EsGTIN = CargarEntidadesGS1.GS1128_ObtenerGTIN(CodLeido, out GTIN);
                            Cantidad = GTIN.VL.Cantidad.ToString(); // se cambia la cantidad a insertar por la equivalencia registrada en el maestro de artículos. 

                            respuesta = TransaccionMD(idArticulo, Cantidad, TablaOrigen, IdCampo, ValorIdCampo, "TABLA.INSERT", IdEstado, SumUno_RestaCero, idUsuario, idMetodoAccion, FechaVencimiento, Lote, idUbicacion76);
                        }

                    #endregion Procesar Despacho
                }
              }
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(true);
                StackFrame sf = st.GetFrame(1);
                if (CodLeido == "")
                    respuesta = "Ponga un código valido.";
                else
                    respuesta = "El artículo/ubicación consultado no es valido o no corresponde a la empresa asociada-" + ex.StackTrace + "-" + sf.GetFileLineNumber(); 
            }

            return respuesta;
        }
        

        private static string GetTotalArticulosOC(string OC, string idUsuario)
        {
            try
            {
                DataSet dataset = new DataSet();
                string cantidad = String.Empty;
                string query = "SELECT count(idArticulo) as TotalArticulosOC FROM Vista_DetalleOrdenCompraCEDI WHERE OC_Traceid = "+ OC +"";

                dataset = da_ConsultaDummy.GetDataSet(query, idUsuario);
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    cantidad = dataset.Tables[0].Rows[0][0].ToString();
                }
                else 
                {
                    cantidad = "0";
                }
                return cantidad;
            }
            catch (Exception e) 
            {
                return " ";
            }
        }

        public static bool ValidarArticuloProveerdor(string idArticulo,string  OC)
        {
            string getIdProvedor, SQL, SQL1;
            DataSet dataset;
            try
            {
                SQL = "select idProveedor from [TRACEID].[dbo].[OPEINGMaestroOrdenCompra] where idMaestroOrdenCompra = " + OC + "";
                getIdProvedor = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                SQL1 = "select * from [TRACEID].[dbo].[RELArticulosProveedor] where idArticulo = " + idArticulo + " and IdProveedor = " + getIdProvedor + "";

                dataset = da_ConsultaDummy.GetDataSet(SQL1, idUsuario);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }          
        }

        public static string CodigoParaSolicitarHH(string idArticulo, string cantidad, string idUsuario)
        {
            string SQL, Espacios;
            try
            {
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);
               // obtengo el GTIN del maestro de artículos.
                string GTIN;
                SQL = "SELECT " + e_TblMaestroArticulosFields.GTIN();
                SQL += " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroArticulos();
                SQL += " WHERE " + e_TblMaestroArticulosFields.idArticulo() + " = '" + idArticulo + "'";
                SQL += " AND " + e_TblMaestroArticulosFields.idCompania() + " = '" + idCompania + "'";
                GTIN = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

               // se le agregan ceros al GTIN
                int VecesRepetir = 13 - GTIN.Length;
                Espacios = "";
                Espacios = string.Concat(Enumerable.Repeat(0, VecesRepetir));
                GTIN = Espacios + GTIN;

               // se arma el código de barras.
                string Codigo = "010" + GTIN + "37" + cantidad; 
                return Codigo;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string CrearCodigoGS1(string idArticulo, string cantidad, string FechaVencimiento, string Lote, string idUsuario)
        {
            string SQL;
            string Codigo = "";
            int VecesRepetir = 0;
            string Espacios = "";
            Single Cantisingle = 0F;
            try
            {
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);
                string GTIN = "";
                string granel = ""; 
                DataSet DS = new DataSet();
                SQL = "SELECT " + e_TblMaestroArticulosFields.GTIN() + "," + e_TblMaestroArticulosFields.Granel();
                SQL += " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroArticulos();
                SQL += " WHERE " + e_TblMaestroArticulosFields.idArticulo() + " = '" + idArticulo + "'";
                SQL += "       AND " + e_TblMaestroArticulosFields.idCompania() + " = '" + idCompania + "'";
                DS = da_ConsultaDummy.GetDataSet(SQL, idUsuario);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    GTIN = DS.Tables[0].Rows[0][0].ToString();
                    granel = DS.Tables[0].Rows[0][1].ToString();
                    VecesRepetir = 14 - GTIN.Length;
                    Espacios = "";
                    Espacios = string.Concat(Enumerable.Repeat(0, VecesRepetir));
                    GTIN = Espacios + GTIN;

                    Codigo = "01" + GTIN;
                    // = "01" + GTIN + "17" + FechaVencimiento + "37" + cantidad + "10" + Lote;

                    if (!string.IsNullOrEmpty(FechaVencimiento))
                    {
                        Codigo = Codigo + "17" + FechaVencimiento;
                    }

                    if (!string.IsNullOrEmpty(cantidad))
                    {
                        if (granel == "False")
                        {
                            VecesRepetir = 8 - cantidad.Length;
                            Espacios = "";
                            Espacios = string.Concat(Enumerable.Repeat(0, VecesRepetir));
                            cantidad = Espacios + cantidad;
                            Codigo = Codigo + "37" + cantidad;
                        }
                        else
                        {
                            int decimales = 0;
                            cantidad = cantidad.Replace(",", ".");
                            cantidad = cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture);
                            Cantisingle = Single.Parse(cantidad, System.Globalization.CultureInfo.InvariantCulture);
                            cantidad = Cantisingle.ToString(System.Globalization.CultureInfo.InvariantCulture);
                            int lugarpunto = cantidad.IndexOf(".") + 1;
                            if (lugarpunto > 0)
                               decimales = cantidad.Substring(lugarpunto).Length;
                            
                            cantidad = cantidad.Replace(".", "");
                            VecesRepetir = 6 - cantidad.Length;
                            Espacios = "";
                            Espacios = string.Concat(Enumerable.Repeat(0, VecesRepetir));
                            cantidad = Espacios + cantidad;
                            Codigo = Codigo + "310" + decimales.ToString() + cantidad;
                        }
                    }

                    if (!string.IsNullOrEmpty(Lote))
                    {
                        Codigo = Codigo + "10" + Lote;
                    }
                }
                else
                    Codigo = "00";

                return Codigo;
            }
            catch (Exception)
            {
                return Codigo;
            }

        }

        public static string ObtenerUbicacionXcodigoLeido(string CodLeido, string idUsuario, string idMetodoAccion)
        {
            string Ubicacion = "";
            try
            {
                if (CodLeido.Substring(0, 2) != "01")  // en caso de leer un GTIN13-GTIN14 se le pone adelante el 01 para poder procesar la recepción-ubicación-alisto.
                {
                    if (CodLeido.Length == 13)
                        CodLeido = "010" + CodLeido;
                    else if (CodLeido.Length == 14)
                        CodLeido = "01" + CodLeido;
                }
                int Cant = 0;
                n_MotorDecisiones.Metodos MD = new n_MotorDecisiones.Metodos();
                EntidadesGS1.e_GTIN GTIN = new EntidadesGS1.e_GTIN();
                string[] Articulo = ObtenerIdArticuloNombreCodigoLeido_GS1128(CodLeido, idUsuario).Split(';');
                string idArticulo = Articulo[0];
                string Cantidad = CargarEntidadesGS1.GS1128_DevolverCantidad(CodLeido);
                string FechaVencimiento = CargarEntidadesGS1.GS1128_DevolverFechaVencimiento(CodLeido);
                string Lote = CargarEntidadesGS1.GS1128_DevolveLote(CodLeido);
                bool EsNum = int.TryParse(Cantidad, out Cant);
                Ubicacion = n_WMS.ObtenerUbicacionSugeridaAlmacenar(Articulo[0], Cant, idUsuario, false);
                bool EsGTIN = CargarEntidadesGS1.GS1128_ObtenerGTIN(CodLeido, out GTIN);
                bool esGranel = CargarEntidadesGS1.GS1128_EsArticuloGranel(CodLeido);
                return Ubicacion + ";" + Articulo[1] + ";" + GTIN.VLs[0].Cantidad.ToString() + ";" + Articulo[0] + ";" + ObtenerCompaniaXUsuario(idUsuario) + ";" + Lote + ";" + FechaVencimiento + ";" + esGranel.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        private static string ObtenerUbicacionDisponible(e_Ubicacion Ubicacion, int CantidadUndsInventario, bool idUbicacion)
        {
            string CodUbicacion = "";
            double CantidadIngresadaUbi = 0;
            double CantidadReservadadaUbi = 0;
            double CantidadTrasladadaUbi = 0;
            decimal CantidadActualUbi = 0;
            decimal CapacidadPesoUbi = 0;
            decimal CapacidadDimeUbi = 0;
            decimal PesoAIngresarArt = 0;
            decimal EspaAIngresarArt = 0;
            decimal DisponibilPesUbi = 0;
            decimal DisponibilDimUbi = 0;
            bool PermiteIngCantUbi = false;
            int UltimaUbicacion = 0;
            List<e_Ubicacion> UbicacionesAnalizar = new List<e_Ubicacion>();
            foreach (e_Articulo Articulo in Ubicacion.Articulos)
            {
                e_Ubicacion UbicacionAnalizar = new e_Ubicacion();
                CantidadActualUbi = 0;
                // primero busca ubicaciones donde el articulo haya estado o este actualmente
                foreach (e_CantidadeEstado CantEstado in Articulo.CantidadesEstado)
                {
                    if (CantEstado.Nombre == "Ingresado")
                    {
                        CantidadIngresadaUbi += CantEstado.Cantidad;
                    }
                    if (CantEstado.Nombre == "Reservado")
                    {
                        CantidadReservadadaUbi += CantEstado.Cantidad;
                    }
                    if (CantEstado.Nombre == "Traslado")
                    {
                        CantidadTrasladadaUbi += CantEstado.Cantidad;
                    }
                }
                CantidadActualUbi = decimal.Parse((CantidadIngresadaUbi - CantidadReservadadaUbi - CantidadTrasladadaUbi).ToString());
                CapacidadPesoUbi = Ubicacion.CapacidadPesoKilos;
                CapacidadDimeUbi = Ubicacion.CapacidadVolumenM3;
                PesoAIngresarArt = (CantidadActualUbi + CantidadUndsInventario) * Articulo.PesoKilos;
                EspaAIngresarArt = (CantidadActualUbi + CantidadUndsInventario) * Articulo.DimensionUnidadM3;
                DisponibilPesUbi = CapacidadPesoUbi - PesoAIngresarArt;
                DisponibilDimUbi = CapacidadDimeUbi - EspaAIngresarArt;
                UbicacionAnalizar = Ubicacion;
                UbicacionAnalizar.DisponibilidadVolumenM3 = 0;
                UbicacionAnalizar.DisponibilidadPesoKilos = 0;
                UbicacionAnalizar.RelacionPesoVolLibre = 0;
                PermiteIngCantUbi = DisponibilDimUbi > 0 && DisponibilPesUbi > 0;
                if (PermiteIngCantUbi)
                {
                    UbicacionAnalizar.DisponibilidadVolumenM3 = DisponibilDimUbi;
                    UbicacionAnalizar.DisponibilidadPesoKilos = DisponibilPesUbi;
                    UbicacionAnalizar.RelacionPesoVolLibre = DisponibilPesUbi / DisponibilDimUbi;
                }
                UbicacionAnalizar.PermiteIngreso = PermiteIngCantUbi;
                UbicacionesAnalizar.Add(UbicacionAnalizar);
                UltimaUbicacion = Ubicacion.idUbicacion;
            }

            //si es no hay se debe buscar la ubicacion mas cercana hasta obtener algo, por lo que esto es un algoritmo 
            //recursivo donde tiene que haver un bit para excluir el id articulo como filtro en la busqueda.
            // Hay que mejorarlo para que pueda descomponer las cantidades en unidades de almacenamiento o distribucion.
            UbicacionesAnalizar = UbicacionesAnalizar.FindAll(x => x.PermiteIngreso == true);
            if (UbicacionesAnalizar.Count > 0)
            {
                decimal MenorValorPeso = UbicacionesAnalizar.Min(x => x.DisponibilidadPesoKilos);
                if (idUbicacion)
                {
                    CodUbicacion = UbicacionesAnalizar.Find(x => x.DisponibilidadPesoKilos == MenorValorPeso).idUbicacion.ToString();
                }
                else
                {
                    //CodUbicacion = UbicacionesAnalizar.Find(x => x.DisponibilidadPesoKilos == MenorValorPeso).CodUbicacion.ToString();
                    CodUbicacion = UbicacionesAnalizar.Find(x => x.DisponibilidadPesoKilos == MenorValorPeso).Etiqueta.ToString();
                }
            }
            return CodUbicacion;
        }

        private static int ObtenerBodegaXarticulo(string idArticulo, string idUsuario)
        {
            string SQL = "";
            try
            {
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);
                SQL = "SELECT " + e_TblMaestroArticulosFields.idBodega();
                SQL += " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroArticulos();
                SQL += " WHERE " + e_TblMaestroArticulosFields.idArticulo() + " = '" + idArticulo + "'";
                SQL += " AND " + e_TblMaestroArticulosFields.idCompania() + " = '" + idCompania + "'";
                string idBodega = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                return Convert.ToInt32(idBodega);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // Hay que agregar la opcion de dividir en cantidades de almacenamiento
        // hay que agregar la opcion cuando el articulo solo puede ser almacenado en cierta bodega.
        public static string ObtenerUbicacionSugeridaAlmacenar(string idArticulo, int CantidadUndsInventario, string idUsuario, bool idUbicacion)
        {
            string Respuesta = "error";
            try
            {
                int idBodega;
                int contadorBucle = 0;
                int idUbiInicial = 0;
                bool ingreso = false;
                List<e_Ubicacion> Ubicaciones = new List<e_Ubicacion>();
                List<e_Ubicacion> Ubicacionestmp = new List<e_Ubicacion>();
                string Zona_Almacenamiento = ConfigurationManager.AppSettings["Zona_Almacenamiento"].ToString();

                n_MotorDecisiones.Metodos MD = new n_MotorDecisiones.Metodos();

                //Se agregó el idBodega para filtrar solamente las ubicaciones de la bodega correspondiente del articulo
                idBodega = ObtenerBodegaXarticulo(idArticulo, idUsuario);

                Respuesta = "";
                Ubicaciones = MD.ObtenerUbicaciones(idUsuario, idArticulo);
                Ubicaciones = Ubicaciones.FindAll(x => x.AbreviaturaZona == Zona_Almacenamiento);
                Ubicaciones = Ubicaciones.FindAll(x => x.idBodegaUbicacion == idBodega);
                //
                Ubicacionestmp = MD.ObtenerUbicaciones(idUsuario, "");
                Ubicacionestmp = Ubicacionestmp.FindAll(x => x.AbreviaturaZona == Zona_Almacenamiento);
                Ubicacionestmp = Ubicacionestmp.FindAll(x => x.idBodegaUbicacion == idBodega);
                if (Ubicaciones.Count > 0)
                {
                    idUbiInicial = Ubicaciones.Find(x => x.idUbicacion > 0).idUbicacion;
                }
                else
                {
                    idUbiInicial = Ubicacionestmp.Find(x => x.idUbicacion > 0).idUbicacion;
                    Ubicaciones = Ubicacionestmp;
                }
                while (Respuesta == "" && contadorBucle < Ubicacionestmp.Count)
                {
                    foreach (e_Ubicacion Ubicacion in Ubicaciones) // primero agota las ubicaciones donde el articulo ha estado
                    {
                        Respuesta = ObtenerUbicacionDisponible(Ubicacion, CantidadUndsInventario, idUbicacion);
                        if (Respuesta == "")
                        {
                            Ubicaciones = Ubicacionestmp.FindAll(x => x.idUbicacion > idUbiInicial);
                            foreach (e_Ubicacion Ubicacion2 in Ubicaciones) // agota las ubicaciones siguientes
                            {
                                Respuesta = ObtenerUbicacionDisponible(Ubicacion2, CantidadUndsInventario, idUbicacion);
                                if (Respuesta != "")
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (Respuesta == "")
                    {
                        if (ingreso == false)
                        {
                            Ubicaciones = Ubicacionestmp; // carga las ubicaciones iniciales de nuevo.
                            ingreso = true;
                        }
                        else
                        {
                            Respuesta = "No hay espacio";
                            break;
                        }
                    }
                    contadorBucle++;
                }

                //Resumen. 

                return Respuesta;
            }
            catch (Exception)
            {
                return Respuesta;
            }
        }

        // Hay que agregar la opcion de dividir en cantidades de almacenamiento
        // hay que agregar la opcion cuando el articulo solo puede ser almacenado en cierta bodega.
        public static string ObtenerUbicacionSugerida(string idArticulo, int CantidadUndsInventario, string idUsuario, string idZona)
        {
            string Respuesta = "error";
            try
            {
                int idBodega;
                int contadorBucle = 0;
                int idUbiInicial = 0;
                bool ingreso = false;
                List<e_Ubicacion> Ubicaciones = new List<e_Ubicacion>();
                List<e_Ubicacion> Ubicacionestmp = new List<e_Ubicacion>();

                n_MotorDecisiones.Metodos MD = new n_MotorDecisiones.Metodos();

                //Se agregó el idBodega para filtrar solamente las ubicaciones de la bodega correspondiente del articulo
                idBodega = ObtenerBodegaXarticulo(idArticulo, idUsuario);

                Respuesta = "";
                Ubicaciones = MD.ObtenerUbicaciones(idUsuario, idArticulo);
                Ubicaciones = Ubicaciones.FindAll(x => x.idZona == idZona);
                Ubicaciones = Ubicaciones.FindAll(x => x.idBodegaUbicacion == idBodega);
                Ubicacionestmp = MD.ObtenerUbicaciones(idUsuario, "");
                Ubicacionestmp = Ubicacionestmp.FindAll(x => x.idZona == idZona);
                Ubicacionestmp = Ubicacionestmp.FindAll(x => x.idBodegaUbicacion == idBodega);
                if (Ubicaciones.Count > 0)
                {
                    idUbiInicial = Ubicaciones.Find(x => x.idUbicacion > 0).idUbicacion;
                    Respuesta = idUbiInicial.ToString();
                }
                else
                {
                    idUbiInicial = Ubicacionestmp.Find(x => x.idUbicacion > 0).idUbicacion;
                    Ubicaciones = Ubicacionestmp;
                    Respuesta = idUbiInicial.ToString();
                }
                while (Respuesta == "" && contadorBucle < Ubicacionestmp.Count)
                {
                    foreach (e_Ubicacion Ubicacion in Ubicaciones) // primero agosta las ubicaciones donde el articulo ha estado
                    {
                        Respuesta = ObtenerUbicacionDisponible(Ubicacion, CantidadUndsInventario, false);
                        if (Respuesta == "")
                        {
                            Ubicaciones = Ubicacionestmp.FindAll(x => x.idUbicacion > idUbiInicial);
                            foreach (e_Ubicacion Ubicacion2 in Ubicaciones) // agota las ubicaciones siguientes
                            {
                                Respuesta = ObtenerUbicacionDisponible(Ubicacion2, CantidadUndsInventario, false);
                                if (Respuesta != "")
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (Respuesta == "")
                    {
                        if (ingreso == false)
                        {
                            Ubicaciones = Ubicacionestmp; // carga las ubicaciones iniciales de nuevo.
                            ingreso = true;
                        }
                        else
                        {
                            Respuesta = "No hay espacio";
                            break;
                        }
                    }
                    contadorBucle++;
                }

                //Resumen. 

                return Respuesta;
            }
            catch (Exception)
            {
                return Respuesta;
            }
        }

        public static DataSet ObtenerDetalleUbicaciones(string Where)
        {
            try
            {
                DataSet DS = new DataSet();
                List<string> Campos = new List<string>();
                Type type = typeof(e_VistaUbicacionesDetalle);
                Campos = da_MotorDecision.VariablesDeLaClass(type);
                string SQL = "select ";
                foreach (string str in Campos)
                {
                    SQL += str + ",";
                }

                SQL = SQL.Substring(0, SQL.Length - 1);
                SQL += " from " + e_TablasBaseDatos.VistaDetalleUbicaciones();
                if (!string.IsNullOrEmpty(Where.Trim()))
                {
                    SQL += " " + Where;
                }
                SQL += " order by " + e_VistaUbicacionesDetalle.idUbicacion() + " , " + e_VistaUbicacionesDetalle.idArticulo() + "," + e_VistaUbicacionesDetalle.FechaVencimiento() + "," + e_VistaUbicacionesDetalle.Lote();
                return da_ConsultaDummy.GetDataSet(SQL, "MD");

            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DataSet ObtenerDetalleUbicacionesNuevos()
        {

            try
            {
                DataSet DS = new DataSet();
                List<string> Campos = new List<string>();
                Type type = typeof(e_VistaUbicacionesDetalle);
                Campos = da_MotorDecision.VariablesDeLaClass(type);
                string SQL = "SELECT * FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + " Vista_CodigosMaestroUbicacion";
                return da_ConsultaDummy.GetDataSet(SQL, "MD");

            }
            catch (Exception)
            {

                return null;
            }
        }

        private static e_CantidadeEstado ObtenerCantEstado(DataRow DR)
        {
            bool EsNumero = false;
            double numero = 0;
            int numeroint = 1;

            e_CantidadeEstado CantEstado = new e_CantidadeEstado();

            EsNumero = int.TryParse(DR[e_VistaUbicacionesDetalle.idEstado()].ToString(), out numeroint);
            if (EsNumero) CantEstado.idEstado = numeroint; else { CantEstado.idEstado = 1; }

            CantEstado.Nombre = DR[e_VistaUbicacionesDetalle.NombreEstado()].ToString();

            EsNumero = double.TryParse(DR[e_VistaUbicacionesDetalle.CantidadEstado()].ToString(), out numero);
            if (EsNumero) CantEstado.Cantidad = numero; else { CantEstado.Cantidad = 1; }

            return CantEstado;

        }

        private static e_Articulo ObtenerArticulo(DataRow DR)
        {
            bool EsNumero = false;
            //double numero = 0;
            decimal numerodecimal = 0;
            int numeroint = 1;
            DateTime Fecha;

            e_Articulo Articulo = new e_Articulo();

            EsNumero = int.TryParse(DR[e_VistaUbicacionesDetalle.idArticulo()].ToString(), out numeroint);
            if (EsNumero) Articulo.IdArticulo = numeroint; else { Articulo.IdArticulo = 1; }

            EsNumero = int.TryParse(DR[e_VistaUbicacionesDetalle.idInterno()].ToString(), out numeroint);
            if (EsNumero) Articulo.IdInterno = numeroint; else { Articulo.IdInterno = 1; }

            Articulo.IdCompania = DR[e_VistaUbicacionesDetalle.idCompania()].ToString();
            Articulo.Nombre = DR[e_VistaUbicacionesDetalle.NombreArticulo()].ToString();
            Articulo.NombreHH = DR[e_VistaUbicacionesDetalle.NombreHH()].ToString();
            Articulo.GTIN = DR[e_VistaUbicacionesDetalle.GTIN()].ToString();
            EsNumero = DateTime.TryParse(DR[e_VistaUbicacionesDetalle.FechaVencimiento()].ToString(), out Fecha);
            if (EsNumero) Articulo.FechaVencimiento = Fecha; else Articulo.FechaVencimiento = Fecha;
            Articulo.Lote = DR[e_VistaUbicacionesDetalle.Lote()].ToString();


            EsNumero = int.TryParse(DR[e_VistaUbicacionesDetalle.idUnidadMedida()].ToString(), out numeroint);
            if (EsNumero) Articulo.idUnidadMedida = numeroint; else { Articulo.idUnidadMedida = 1; }

            EsNumero = int.TryParse(DR[e_VistaUbicacionesDetalle.idTipoEmpaque()].ToString(), out numeroint);
            if (EsNumero) Articulo.idTipoEmpaque = numeroint; else { Articulo.idTipoEmpaque = 1; }

            EsNumero = int.TryParse(DR[e_VistaUbicacionesDetalle.idEtiqueta()].ToString(), out numeroint);
            if (EsNumero) Articulo.idEtiqueta = numeroint; else { Articulo.idEtiqueta = 1; }

            EsNumero = decimal.TryParse(DR[e_VistaUbicacionesDetalle.DuracionHoraAlisto()].ToString(), out numerodecimal);
            if (EsNumero) Articulo.DuracionHoraAlisto = numerodecimal; else { Articulo.DuracionHoraAlisto = 1; }

            EsNumero = int.TryParse(DR[e_VistaUbicacionesDetalle.idBodega()].ToString(), out numeroint);
            if (EsNumero) Articulo.idBodega = numeroint; else { Articulo.idBodega = 0; }

            EsNumero = decimal.TryParse(DR[e_VistaUbicacionesDetalle.PesoKilos()].ToString(), out numerodecimal);
            if (EsNumero) Articulo.PesoKilos = numerodecimal; else { Articulo.PesoKilos = 1; }

            EsNumero = decimal.TryParse(DR[e_VistaUbicacionesDetalle.DimensionUnidadM3()].ToString(), out numerodecimal);
            if (EsNumero) Articulo.DimensionUnidadM3 = numerodecimal; else { Articulo.DimensionUnidadM3 = 0.01m; }

            return Articulo;

        }

        private static e_Ubicacion ObtenerUbicacion(DataRow DR)
        {
            bool EsNumero = false;
            decimal numeroDecimal = 0;
            int numeroint = 0;

            e_Ubicacion Ubicacion = new e_Ubicacion();
            EsNumero = int.TryParse(DR[e_VistaUbicacionesDetalle.idAlmacen()].ToString(), out numeroint);
            if (EsNumero) Ubicacion.idAlmacen = numeroint; else Ubicacion.idAlmacen = 0;
            EsNumero = int.TryParse(DR[e_VistaUbicacionesDetalle.idBodega()].ToString(), out numeroint);
            if (EsNumero) Ubicacion.idBodega = numeroint; else Ubicacion.idBodega = 0;

            EsNumero = int.TryParse(DR[e_VistaUbicacionesDetalle.SecuenciaBodega()].ToString(), out numeroint);
            if (EsNumero) Ubicacion.SecuenciaBodega = numeroint; else Ubicacion.SecuenciaBodega = 1;

            Ubicacion.CodUbicacion = DR[e_VistaUbicacionesDetalle.CodUbicacion()].ToString();
            Ubicacion.Etiqueta = DR[e_VistaUbicacionesDetalle.Etiqueta()].ToString();

            Ubicacion.idZona = DR[e_VistaUbicacionesDetalle.idZona()].ToString();
            EsNumero = int.TryParse(DR[e_VistaUbicacionesDetalle.idUbicacion()].ToString(), out numeroint);
            if (EsNumero) Ubicacion.idUbicacion = numeroint; else Ubicacion.idUbicacion = 0;

            EsNumero = int.TryParse(DR[e_VistaUbicacionesDetalle.SecuenciaUbicacion()].ToString(), out numeroint);
            if (EsNumero) Ubicacion.Secuencia = numeroint; else Ubicacion.Secuencia = 1;

            Ubicacion.AbreviaturaAlmacen = DR[e_VistaUbicacionesDetalle.AbreviaturaAlmacen()].ToString();

            Ubicacion.AbreviaturaBodega = DR[e_VistaUbicacionesDetalle.AbreviaturaBodega()].ToString();

            Ubicacion.AbreviaturaZona = DR[e_VistaUbicacionesDetalle.AbreviaturaZona()].ToString();
            Ubicacion.estante = DR[e_VistaUbicacionesDetalle.estante()].ToString();
            Ubicacion.columna = DR[e_VistaUbicacionesDetalle.columna()].ToString();
            Ubicacion.nivel = DR[e_VistaUbicacionesDetalle.nivel()].ToString();
            Ubicacion.pos = DR[e_VistaUbicacionesDetalle.pos()].ToString();

            EsNumero = decimal.TryParse(DR[e_VistaUbicacionesDetalle.largo()].ToString(), out numeroDecimal);
            if (EsNumero) Ubicacion.largo = numeroDecimal; else Ubicacion.largo = 0;

            EsNumero = decimal.TryParse(DR[e_VistaUbicacionesDetalle.areaAncho()].ToString(), out numeroDecimal);
            if (EsNumero) Ubicacion.areaAncho = numeroDecimal; else Ubicacion.areaAncho = 0;

            EsNumero = decimal.TryParse(DR[e_VistaUbicacionesDetalle.alto()].ToString(), out numeroDecimal);
            if (EsNumero) Ubicacion.alto = numeroDecimal; else Ubicacion.alto = 0;

            Ubicacion.cara = DR[e_VistaUbicacionesDetalle.cara()].ToString();
            Ubicacion.profundidad = DR[e_VistaUbicacionesDetalle.profundidad()].ToString();

            EsNumero = decimal.TryParse(DR[e_VistaUbicacionesDetalle.CapacidadPesoKilos()].ToString(), out numeroDecimal);
            if (EsNumero) Ubicacion.CapacidadPesoKilos = numeroDecimal; else Ubicacion.CapacidadPesoKilos = 0;

            EsNumero = decimal.TryParse(DR[e_VistaUbicacionesDetalle.CapacidadVolumenM3()].ToString(), out numeroDecimal);
            if (EsNumero) Ubicacion.CapacidadVolumenM3 = numeroDecimal; else Ubicacion.CapacidadVolumenM3 = 0;

            EsNumero = int.TryParse(DR[e_VistaUbicacionesDetalle.idBodegaUbicacion()].ToString(), out numeroint);
            if (EsNumero) Ubicacion.idBodegaUbicacion = numeroint; else Ubicacion.idBodegaUbicacion = 0;

            return Ubicacion;
        }

        /// <summary>
        /// If the page string is empty or null it will returned all the Database's Acciones.
        /// </summary>
        /// <param name="Pagina"></param>
        /// <returns></returns>
        public static List<e_Ubicacion> ObtenerUbicaciones(string idUsuario, string stridArticulo)
        {
            return ObtenerUbicacionesFiltro(idUsuario, stridArticulo);
        }

        private static List<e_Ubicacion> ObtenerUbicacionesFiltro(string idUsuario, string stridArticulo)
        {
            List<e_Ubicacion> Ubicaciones = new List<e_Ubicacion>();
            string UbicacionAnterior = "";
            try
            {
                DataSet DS = new DataSet();
                string where = "";
                e_Ubicacion Ubicacion = new e_Ubicacion();                              // 1er nivel
                List<e_CantidadeEstado> CantEstados = new List<e_CantidadeEstado>();    // 3er nivel
                e_Articulo Articulo = new e_Articulo();                                 // 2do nivel
                List<e_Articulo> Articulos = new List<e_Articulo>();

                if (stridArticulo.Trim() != "")
                {
                    where = " where " + e_VistaUbicacionesDetalle.idArticulo() + " = '" + stridArticulo + "' AND CantidadEstado > 0";
                }
                DS = ObtenerDetalleUbicaciones(where);
                ///Carga los parametros 
                ///
                int idUbicacion = 0;
                int idArticulo = 0;
                int regs = 0;

                if (DS.Tables[0].Rows.Count > 0)
                {
                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    if (DR.Table.Rows.IndexOf(DR) == 0)
                    {
                        ///si se ingresa aqui o cambio de metodo o es la primea vez y se tiene que cargar todo... Accion, Metodo y Parametro
                        Ubicacion = ObtenerUbicacion(DR);
                        idUbicacion = Ubicacion.idUbicacion;
                        Articulo = ObtenerArticulo(DR);
                        idArticulo = Articulo.IdArticulo; //esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                        CantEstados.Add(ObtenerCantEstado(DR));
                        if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1)
                        {
                           //CantEstados.Add(ObtenerCantEstado(DR));
                            Articulo.CantidadesEstado = CantEstados;
                            Articulos.Add(Articulo);
                            Ubicacion.Articulos = Articulos;
                            Ubicaciones.Add(Ubicacion);
                        }
                    }
                    else// si no es la primera
                    {
                        if (DR[e_VistaUbicacionesDetalle.idUbicacion()].ToString() == idUbicacion.ToString())
                        {
                            if (DR[e_VistaUbicacionesDetalle.idArticulo()].ToString() == idArticulo.ToString())//si el metodo a cargar es el mismo se vuelve a cargar parametro
                            {
                                if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1) //Ultima vez que se ejecuta el ciclo
                                {
                                    CantEstados.Add(ObtenerCantEstado(DR));
                                    Articulo.CantidadesEstado = CantEstados;
                                    Articulos.Add(Articulo); //se crea nuevo articulo
                                    Ubicacion.Articulos = Articulos;
                                    if (UbicacionAnterior != Ubicacion.AbreviaturaZona)  // este if se pone aquí para que las ubicaciones no se dupliquen
                                    {
                                        Ubicaciones.Add(Ubicacion);
                                        UbicacionAnterior = Ubicacion.AbreviaturaZona;
                                    }
                                }
                                else
                                {
                                    CantEstados.Add(ObtenerCantEstado(DR));
                                }

                            }
                            else //si el articulo a cargar NO es el mismo se asignan la cantEstado al articulo anterior y se cargan nuevos CantEstado
                            {
                                Articulo.CantidadesEstado = CantEstados; //Se cargan las CantEstado al articulo finalizado
                                Articulos.Add(Articulo); //se crea nuevo metodo

                                Articulo = new e_Articulo();
                                CantEstados = new List<e_CantidadeEstado>();
                                Articulo = ObtenerArticulo(DR);
                                idArticulo = Articulo.IdArticulo;  //esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                                CantEstados.Add(ObtenerCantEstado(DR));
                            }
                        }
                        else // Cambio la ubicacion.
                        {
                            Articulo.CantidadesEstado = CantEstados;  //Se cargan los parametros al metodo finalizado
                            Articulos.Add(Articulo); //se crea nuevo metodo
                            Ubicacion.Articulos = Articulos;
                            if (UbicacionAnterior != Ubicacion.AbreviaturaZona)  // este if se pone aquí para que las ubicaciones no se dupliquen
                            {
                                Ubicaciones.Add(Ubicacion);
                                UbicacionAnterior = Ubicacion.AbreviaturaZona;
                            }
 
                            Articulos = new List<e_Articulo>();
                            Ubicacion = new e_Ubicacion();
                            Articulo = new e_Articulo();
                            CantEstados = new List<e_CantidadeEstado>();
                            Ubicacion = ObtenerUbicacion(DR);
                            idUbicacion = Ubicacion.idUbicacion;
                            Articulo = ObtenerArticulo(DR);
                            idArticulo = Articulo.IdArticulo; //esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                            CantEstados.Add(ObtenerCantEstado(DR)); // cuando cambia la ubicación, se pierde un elemento, con esta línea se evita esa perdida.
                            if (UbicacionAnterior != Ubicacion.AbreviaturaZona) // este if se pone aquí para que las ubicaciones no se dupliquen
                               Ubicaciones.Add(Ubicacion);

                            UbicacionAnterior = Ubicacion.AbreviaturaZona;

                            // esto pasa cuando cambia de ubicacion en el utlimo registro
                            if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1)
                            {
                                //CantEstados.Add(ObtenerCantEstado(DR));
                                Articulo.CantidadesEstado = CantEstados;
                                Articulos.Add(Articulo);
                                Ubicacion.Articulos = Articulos;
                                if (UbicacionAnterior != Ubicacion.AbreviaturaZona)  // este if se pone aquí para que las ubicaciones no se dupliquen
                                   Ubicaciones.Add(Ubicacion);
                            }
                         }
                      }
                   }
                }
                else
                {

                }
                return Ubicaciones;
            }
            catch (Exception)
            {
                return Ubicaciones;
            }
        }

        #endregion Ubicaciones

        #region Rutas

        public string ObtenerProximaFechaDisponible
             (string DiasDiponibles, string HoraMin, string HoraMax,
             string DuracionProceso, string FechayHoraInicio, string HorasDesplazamiento)
        {
            string Respuesta = "";
            DateTime FechaInicio = new DateTime();
            List<string> DD = DiasDiponibles.Split(';').ToList();
            List<string> HMin = HoraMin.Split(';').ToList();
            List<string> HMax = HoraMax.Split(';').ToList();
            List<int> Dias = new List<int>();
            DateTime Hoy = DateTime.Now;
            int DiaInt = 0;
            int HMinInt = 0;
            int HMaxInt = 0;
            List<int> DiasInt = new List<int>();
            List<int> HsMinInt = new List<int>();
            List<int> HsMaxInt = new List<int>();
            int HorasDesplazamientInt = 0;
            bool EsNumHoresDesplaz = int.TryParse(HorasDesplazamiento, out HorasDesplazamientInt);
            if (EsNumHoresDesplaz)
            {
                bool HoraInicioCorrecta = DateTime.TryParse(FechayHoraInicio, out FechaInicio);
                if (HoraInicioCorrecta)
                {
                    if (DD.Count == HMin.Count && DD.Count == HMax.Count)
                    {
                        foreach (string Dia in DD)
                        {
                            bool EsNum1 = int.TryParse(Dia, out DiaInt);
                            bool EsNum2 = int.TryParse(HMin[DD.IndexOf(Dia)], out  HMinInt);
                            bool EsNum3 = int.TryParse(HMin[DD.IndexOf(Dia)], out  HMaxInt);
                            if (EsNum1 && EsNum2 && EsNum3)
                            {
                                DiasInt.Add(DiaInt);
                                HsMaxInt.Add(HMaxInt);
                                HsMinInt.Add(HMinInt);
                            }
                            else //EsNum1 && EsNum2 && EsNum3)
                            {
                                Respuesta = "Alguna variable no es de tipo entera";
                                break;
                            }
                        }
                        Respuesta = "2016-09-08 07:00:00";
                    }
                    else // (DD.Count == HMin.Count && DD.Count == HMax.Count)
                    {
                        Respuesta = "La cantidad de dias y horas no coinciden";
                    }

                }
                else // if (HoraInicioCorrecta)
                {
                    Respuesta = "La hora de inicio de tiene el formato correcto. YYYY-MM-DD HH:mm:ss";
                }

            }
            else //if (EsNumHoresDesplaz)
            {
                Respuesta = "Las horas desplazamiento no es un numero entero";
            }

            return Respuesta;

        }

        public static DataSet ObtenerDetalleRuta(string Where)
        {
            try
            {
                DataSet DS = new DataSet();
                List<string> Campos = new List<string>();
                Type type = typeof(e_VistaRutaDetalles);
                Campos = da_MotorDecision.VariablesDeLaClass(type);
                string SQL = "select ";
                foreach (string str in Campos)
                {
                    SQL += str + ",";
                }

                SQL = SQL.Substring(0, SQL.Length - 1);
                SQL += " from " + e_TablasBaseDatos.VistaRutaDetalles();
                if (!string.IsNullOrEmpty(Where.Trim()))
                {
                    SQL += " " + Where;
                }
                SQL += " order by " + e_VistaRutaDetalles.idRuta() + " , " + e_VistaRutaDetalles.IdVehiculo() + "," + e_VistaRutaDetalles.idDestino();
                return da_ConsultaDummy.GetDataSet(SQL, "MD");

            }
            catch (Exception)
            {
                return null;
            }
        }

        private static e_Rutas ObtenerRutas(DataRow DR)
        {
            bool EsNumero = false;
            //double numero = 0;
            int numeroint = 1;

            e_Rutas Rutas = new e_Rutas();

            EsNumero = int.TryParse(DR[e_VistaRutaDetalles.idRuta()].ToString(), out numeroint);
            if (EsNumero) Rutas.idRuta = numeroint; else { Rutas.idRuta = 1; }

            Rutas.Nombre = DR[e_VistaRutaDetalles.NombreRuta()].ToString();

            Rutas.Descripcion = DR[e_VistaRutaDetalles.DescripcionRuta()].ToString();

            Rutas.Comentarios = DR[e_VistaRutaDetalles.ComentariosRuta()].ToString();

            return Rutas;

        }

        private static e_Vehiculo ObtenerVehiculo(DataRow DR)
        {

            bool EsNumero = false;
            //double numero = 0;
            int numeroint = 1;
            decimal numeroDecimal = 0;

            e_Vehiculo vehiculos = new e_Vehiculo();

            EsNumero = int.TryParse(DR[e_VistaRutaDetalles.IdVehiculo()].ToString(), out numeroint);
            if (EsNumero) vehiculos.IdVehiculo = numeroint; else { vehiculos.IdVehiculo = 1; }

            vehiculos.TipoVehiculo = DR[e_VistaRutaDetalles.TipoVehiculo()].ToString();

            vehiculos.Nombre = DR[e_VistaRutaDetalles.NombreVehiculo()].ToString();

            vehiculos.Modelo = DR[e_VistaRutaDetalles.Modelo()].ToString();

            vehiculos.Placa = DR[e_VistaRutaDetalles.Placa()].ToString();

            vehiculos.Comentario = DR[e_VistaRutaDetalles.Comentario()].ToString();

            EsNumero = decimal.TryParse(DR[e_VistaRutaDetalles.CapacidadPeso()].ToString(), out numeroDecimal);
            if (EsNumero) vehiculos.CapacidadPeso = numeroDecimal; else { vehiculos.CapacidadPeso = 1; }

            EsNumero = decimal.TryParse(DR[e_VistaRutaDetalles.CapacidadVolumen()].ToString(), out numeroDecimal);
            if (EsNumero) vehiculos.CapacidadVolumen = numeroDecimal; else { vehiculos.CapacidadVolumen = 1; }

            vehiculos.Color = DR[e_VistaRutaDetalles.Color()].ToString();

            vehiculos.MarcaVehiculo = DR[e_VistaRutaDetalles.MarcaCarro()].ToString();

            EsNumero = int.TryParse(DR[e_VistaRutaDetalles.idTransportista()].ToString(), out numeroint);
            if (EsNumero) vehiculos.idTransportista = numeroint; else { vehiculos.idTransportista = 1; }

            vehiculos.NombreTransportista = DR[e_VistaRutaDetalles.NombreTransportista()].ToString();

            vehiculos.Telefono = DR[e_VistaRutaDetalles.Telefono()].ToString();

            vehiculos.Correo = DR[e_VistaRutaDetalles.Correo()].ToString();

            vehiculos.ComentariosTransportista = DR[e_VistaRutaDetalles.ComentariosTransportista()].ToString();

            vehiculos.NombreCompañia = DR[e_VistaRutaDetalles.NombreCompañia()].ToString();

            return vehiculos;


        }

        private static e_Destinos_Dev ObtenerDestinos(DataRow DR)
        {
            bool EsNumero = false;
            //double numero = 0;
            int numeroint = 1;

            e_Destinos_Dev Destino = new e_Destinos_Dev();

            EsNumero = int.TryParse(DR[e_VistaRutaDetalles.idDestino()].ToString(), out numeroint);
            if (EsNumero) Destino.idDestino = numeroint; else { Destino.idDestino = 1; }

            Destino.NombreDestino = DR[e_VistaRutaDetalles.NombreDestino()].ToString();

            Destino.Direccion = DR[e_VistaRutaDetalles.Direccion()].ToString();

            Destino.DescripcionDestino = DR[e_VistaRutaDetalles.DescripcionDestino()].ToString();

            return Destino;

        }

        public static List<e_Rutas> ObtenerRutas(string idUsuario, string stridRuta)
        {
            return ObtenerRutasFiltro(idUsuario, stridRuta);
        }

        public static List<e_Rutas> ObtenerRutasFiltro(string idUsuario, string stridRuta)
        {
            List<e_Rutas> Rutas = new List<e_Rutas>();
            try
            {
                DataSet DS = new DataSet();
                string where = "";
                e_Rutas Ruta = new e_Rutas();                            // 1er nivel
                List<e_Destinos_Dev> Destino = new List<e_Destinos_Dev>();       // 3er nivel
                e_Vehiculo Vehiculo = new e_Vehiculo();                  // 2do nivel
                List<e_Vehiculo> Vehiculos = new List<e_Vehiculo>();

                if (stridRuta.Trim() != "")
                {
                    where = " where " + e_VistaRutaDetalles.idRuta() + " = '" + stridRuta + "'";
                }
                DS = ObtenerDetalleRuta(where);
                ///Carga los parametros 
                ///
                //int idDestino = 0;
                int idRuta = int.Parse(stridRuta);
                int IdVehiculo = 0;

                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    if (DR.Table.Rows.IndexOf(DR) == 0)
                    {
                        ///si se ingresa aqui o cambio de metodo o es la primea vez y se tiene que cargar todo... Accion, Metodo y Parametro
                        Ruta = ObtenerRutas(DR);
                        idRuta = Ruta.idRuta;
                        Vehiculo = ObtenerVehiculo(DR);
                        IdVehiculo = Vehiculo.IdVehiculo; //esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                        Destino.Add(ObtenerDestinos(DR));
                        if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1)
                        {
                            Destino.Add(ObtenerDestinos(DR));
                            Vehiculo.Destinos = Destino;
                            Vehiculos.Add(Vehiculo);
                            Ruta.idRuta = idRuta;
                            Rutas.Add(Ruta);
                        }
                    }
                    else// si no es la primera
                    {
                        if (DR[e_VistaRutaDetalles.idRuta()].ToString() == idRuta.ToString())
                        {
                            if (DR[e_VistaRutaDetalles.idRuta()].ToString() == idRuta.ToString())//si el metodo a cargar es el mismo se vuelve a cargar parametro
                            {
                                if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1) //Ultima vez que se ejecuta el ciclo
                                {
                                    Destino.Add(ObtenerDestinos(DR));
                                    Vehiculo.Destinos = Destino;
                                    Vehiculos.Add(Vehiculo); //se crea nuevo articulo
                                    Ruta.Vehiculos = Vehiculos;
                                    Rutas.Add(Ruta);
                                }
                                else
                                {
                                    Destino.Add(ObtenerDestinos(DR));
                                }

                            }
                            else //si el articulo a cargar NO es el mismo se asignan la cantEstado al articulo anterior y se cargan nuevos CantEstado
                            {
                                Vehiculo.Destinos = Destino; //Se cargan las CantEstado al articulo finalizado
                                Vehiculos.Add(Vehiculo); //se crea nuevo metodo

                                Vehiculo = new e_Vehiculo();
                                Destino = new List<e_Destinos_Dev>();
                                Vehiculo = ObtenerVehiculo(DR);
                                IdVehiculo = Vehiculo.IdVehiculo;  //esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta
                                Destino.Add(ObtenerDestinos(DR));
                            }
                        }
                        else // Cambio la ubicacion.
                        {
                            Vehiculo.Destinos = Destino;  //Se cargan los parametros al metodo finalizado
                            Vehiculos.Add(Vehiculo); //se crea nuevo metodo
                            Ruta.Vehiculos = Vehiculos;
                            Rutas.Add(Ruta);
                            Vehiculos = new List<e_Vehiculo>();
                            Ruta = new e_Rutas();
                            Vehiculo = new e_Vehiculo();
                            Destino = new List<e_Destinos_Dev>();
                            Ruta = ObtenerRutas(DR);
                            idRuta = Ruta.idRuta;
                            Vehiculo = ObtenerVehiculo(DR);
                            IdVehiculo = Vehiculo.IdVehiculo; //esto sirve solo para identidicar el metodo cuando se valide en cual metodo se esta

                            // esto pasa cuando cambia de ubicacion en el utlimo registro
                            if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1)
                            {
                                Destino.Add(ObtenerDestinos(DR));
                                Vehiculo.Destinos = Destino;
                                Vehiculos.Add(Vehiculo);
                                Ruta.Vehiculos = Vehiculos;
                                Rutas.Add(Ruta);
                            }
                        }
                    }
                }

                return Rutas;
            }
            catch (Exception)
            {
                return Rutas;
            }
        }

        #endregion Rutas

        #region AccionesCodigosGS1

        public static string ObtenerIdArticuloNombreCodigoLeido_GS1128(string CodigoLeido, string idUsuario)
        {
            string respuesta = "0;0;0;0";
            string SQL = "";
            string GTIN_OBTENIDO = "";

            try
            {
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);
                if (CodigoLeido.Trim().Length > 0)
                {
                    if (CodigoLeido.Substring(0, 2) != "01")  // en caso de leer un GTIN13-GTIN14 se le pone adelante el 01 para poder procesar la recepción-ubicación-alisto.
                    {
                        if (CodigoLeido.Length == 13)
                            CodigoLeido = "010" + CodigoLeido;
                        else if (CodigoLeido.Length == 14)
                            CodigoLeido = "01" + CodigoLeido;
                    }

                    EntidadesGS1.e_GTIN GTIN = new EntidadesGS1.e_GTIN();
                    bool EsGTIN = CargarEntidadesGS1.GS1128_ObtenerGTIN(CodigoLeido, out GTIN);
                    if (GTIN != null)
                    {
                        GTIN_OBTENIDO = Obtener_GTIN_Decodificador(CodigoLeido);

                        if (string.IsNullOrEmpty(GTIN.ValorLeido))
                           return respuesta;

                        //SQL = "SELECT CAST(" + e_TblMaestroArticulosFields.idArticulo()   + " AS NVARCHAR(10)) + ';' + " + 
                        //                       e_TblMaestroArticulosFields.Nombre()       + " + ';' + CAST(" +
                        //                       e_TblMaestroArticulosFields.Equivalencia() + " AS NVARCHAR(10)) + ';' + CAST(" + 
                        //                       e_TblMaestroArticulosFields.idInterno ()   + " AS NVARCHAR(10))" +
                        //      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroArticulos() +
                        //      "  WHERE " + e_TblMaestroArticulosFields.GTIN() + " = '" + GTIN.ValorLeido + "'" +
                        //      "        AND " + e_TblMaestroArticulosFields.idCompania() + " = '" + idCompania + "'";

                        SQL = "EXEC SP_ObtenerValores_Alisto '" + GTIN_OBTENIDO + "','" + idCompania + "'";

                        //SQL = "SELECT CAST(" + e_TblMaestroArticulosFields.idArticulo() + " AS NVARCHAR(10)) + ';' + " +
                        //                          e_TblMaestroArticulosFields.Nombre() + " + ';' + CAST(" +
                        //                          e_TblMaestroArticulosFields.Equivalencia() + " AS NVARCHAR(10)) + ';' + CAST(" +
                        //                          e_TblMaestroArticulosFields.idInterno() + " AS NVARCHAR(10))" +
                        //         "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroArticulos() +
                        //         "  WHERE " + e_TblMaestroArticulosFields.GTIN() + " = '" + GTIN_OBTENIDO + "'" +
                        //         "        AND " + e_TblMaestroArticulosFields.idCompania() + " = '" + idCompania + "'";
                        respuesta = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                        if (string.IsNullOrEmpty(respuesta))
                        {
                            SQL = "";
                            //SQL = "SELECT CAST(" + e_VistaRelacionGTIN13GTIN14.idArticulo() + " AS NVARCHAR(10)) + ';' + " + 
                            //                       e_VistaRelacionGTIN13GTIN14.Nombre() + " + ';' + CAST(" +
                            //                       e_VistaRelacionGTIN13GTIN14.Equivalencia() + " AS NVARCHAR(10)) + ';' + CAST(" +
                            //                       e_VistaRelacionGTIN13GTIN14.idinterno () + " AS NVARCHAR(10))" +
                            //      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaRelacionGTIN13GTIN14() +
                            //      "  WHERE (" + e_VistaRelacionGTIN13GTIN14.GTIN13() + " = '" + GTIN.ValorLeido + "'" +
                            //      "        OR " + e_VistaRelacionGTIN13GTIN14.GTIN14() + " = '" + GTIN.ValorLeido + "')" +
                            //      "        AND " + e_VistaRelacionGTIN13GTIN14.idCompania() + " = '" + idCompania + "'";
                            //respuesta = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                            //SQL = "SELECT CAST(" + e_VistaRelacionGTIN13GTIN14.idArticulo() + " AS NVARCHAR(10)) + ';' + " +
                            //                       e_VistaRelacionGTIN13GTIN14.Nombre() + " + ';' + CAST(" +
                            //                       e_VistaRelacionGTIN13GTIN14.Equivalencia() + " AS NVARCHAR(10)) + ';' + CAST(" +
                            //                       e_VistaRelacionGTIN13GTIN14.idinterno() + " AS NVARCHAR(10))" +
                            //      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaRelacionGTIN13GTIN14() +
                            //      "  WHERE (" + e_VistaRelacionGTIN13GTIN14.GTIN13() + " = '" + GTIN_OBTENIDO + "'" +
                            //      "        OR " + e_VistaRelacionGTIN13GTIN14.GTIN14() + " = '" + GTIN_OBTENIDO + "')" +
                            //      "        AND " + e_VistaRelacionGTIN13GTIN14.idCompania() + " = '" + idCompania + "'";

                            SQL = "EXEC SP_ObtenerValores_Alisto '" + GTIN_OBTENIDO + "','" + idCompania + "'";
                            respuesta = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                        }
                    }
                }

                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;
            }
        }

        public static string ObtenerIdArticuloCodigoLeido_GS1128(string CodLeido, string idUsuario)
        {
            string respuesta = "";
            string GTIN_OBTENIDO = "";
            try
            {
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);
                if (CodLeido.Trim().Length > 0)
                {
                    if (CodLeido.Substring(0, 2) != "01")  // en caso de leer un GTIN13-GTIN14 se le pone adelante el 01 para poder procesar la recepción-ubicación-alisto.
                    {
                        if (CodLeido.Length == 13)
                            CodLeido = "010" + CodLeido;
                        else if (CodLeido.Length == 14)
                            CodLeido = "01" + CodLeido;
                    }

                    EntidadesGS1.e_GTIN GTIN = new EntidadesGS1.e_GTIN();
                    bool EsGTIN = CargarEntidadesGS1.GS1128_ObtenerGTIN(CodLeido, out GTIN);
                    GTIN_OBTENIDO = Obtener_GTIN_Decodificador(CodLeido);
                    if (GTIN != null)
                    {
                        string SQL = "";
                        if (GTIN_OBTENIDO.Length == 13)
                        { 
                        SQL = "SELECT " + e_TblMaestroArticulosFields.idArticulo() + 
                                     "  FROM " + e_TablasBaseDatos.TblMaestroArticulos()  + 
                                     "  WHERE " + e_TblMaestroArticulosFields.GTIN() + " = '" + GTIN_OBTENIDO + "'" +
                                     "        AND " + e_TblMaestroArticulosFields.idCompania() + " = '" + idCompania + "'";
                        }
                        else if (GTIN_OBTENIDO.Length == 14)
                        {
                            SQL = "SELECT MA.idArticulo " +
                                  "FROM ADMMaestroArticulo as MA "+
                                  "INNER JOIN ADMGTIN14VariableLogistica as GT "+
                                  "ON GT.idInterno = MA.idInterno "+
                                  "WHERE GT.ConsecutivoGTIN14= " +" '" + GTIN_OBTENIDO + "'" +
                                  " AND MA." + e_TblMaestroArticulosFields.idCompania() + " = '" + idCompania + "'";
                        }

                        //string SQL = "SELECT " + e_TblMaestroArticulosFields.idArticulo() +
                        //            "  FROM " + e_TablasBaseDatos.TblMaestroArticulos() +
                        //            "  WHERE " + e_TblMaestroArticulosFields.GTIN() + " = '" + GTIN.ValorLeido + "'" +
                        //            "        AND " + e_TblMaestroArticulosFields.idCompania() + " = '" + idCompania + "'";
                        respuesta = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                        
                    }
                }
                return respuesta + "|" +CodLeido;
            }
            catch (Exception)
            {
                return respuesta;
            }
        }

        //HECHO POR DANEY
        public static string Obtener_GTIN_Decodificador(string datamatrix)
        {
            string resultado_gtin;
            try
            {
                string GTIN14 = "", GTIN14_37 = "", GTIN13 = "";

                Regex _TagParser = new Regex(@"02(\d{15})17");
                foreach (Match CurrentMatch in _TagParser.Matches(datamatrix))
                {
                    string P_GTIN14 = CurrentMatch.Groups[1].Value;

                    string cadena = P_GTIN14;
                    if (cadena.StartsWith("0"))
                        cadena = cadena.Substring(1);

                    GTIN14 = cadena;
                    //G14 = GTIN14.Length;
                }

                if (GTIN14 != "")
                {
                    resultado_gtin = GTIN14;
                }
                else
                {
                    Regex _TagParser1 = new Regex(@"01(\d{14})17");
                    foreach (Match CurrentMatch1 in _TagParser1.Matches(datamatrix))
                    {
                        string P_GTIN13 = CurrentMatch1.Groups[1].Value;
                        string cadena = P_GTIN13;
                        if (cadena.StartsWith("0"))
                            cadena = cadena.Substring(1);
                        GTIN13 = cadena;
                    }
                    if (GTIN13 != "")
                    {
                        resultado_gtin = GTIN13;
                    }
                    else
                    {

                        Regex _TagParser4 = new Regex(@"02(\d{14})37");
                        foreach (Match CurrentMatch4 in _TagParser4.Matches(datamatrix))
                        {
                            string P_GTIN14_37 = CurrentMatch4.Groups[1].Value;

                            string cadena = P_GTIN14_37;
                            if (cadena.StartsWith("0"))
                                cadena = cadena.Substring(1);

                            GTIN14_37 = cadena;
                            //G14 = GTIN14.Length;
                        }

                        if (GTIN14_37 != "")
                        {

                            resultado_gtin = GTIN14_37;
                        }
                        else
                        {
                            resultado_gtin = "0";
                        }
                    }




                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado_gtin;
        }

        ///TERMINA PRUEBA DANEY

        public static string DevolverIdUbicacion(string CodLeido, string idUsuario)
        {
            string respuesta = "";
            try
            {
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);
                List<EntidadesGS1.e_IdentificadorAplicacion> AIs = new List<EntidadesGS1.e_IdentificadorAplicacion>();
                AIs = CargarEntidadesGS1.GS1128_DevolverAIs(CodLeido);
                if (AIs != null)
                {
                    EntidadesGS1.e_IdentificadorAplicacion AI = new EntidadesGS1.e_IdentificadorAplicacion();
                    AI = AIs.Find(x => x.idIdentificadorAplicacion == "91");
                    {
                        string SQL = "select " + e_VistaCodigosUbicacionFields.idUbicacion() + " from " + e_TablasBaseDatos.VistaCodigosUbicacion();
                        SQL += " where " + e_VistaCodigosUbicacionFields.CODUBI() + " = '" + CodLeido.Replace("-","") + "'";
                        SQL += " AND " + e_VistaCodigosUbicacionFields.idCompania() + " = '" + idCompania + "'";
                        respuesta = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                    }
                }

                return respuesta;
            }
            catch (Exception)
            {
                return respuesta;

            }
        }

        #endregion AccionesCodigosGS1

        #region CalculoHorasHombreTareas

        private static e_UsuarioProductivo ObtenerUsuarioProductivo(DataRow DR)
        {
            e_UsuarioProductivo UP = new e_UsuarioProductivo();
            UP.idUsuario = DR[e_VistaTareaUsuarioFields.IDUSUARIO()].ToString().ToUpper();
            UP.HHDisponiblesParaTarea = decimal.Parse(DR[e_VistaTareaUsuarioFields.HHDisponiblesParaTarea()].ToString().ToUpper());
            return UP;
        }

        private static e_TareaUsuario ObtenerTareaUsuario(DataRow DR)
        {
            e_TareaUsuario TU = new e_TareaUsuario();
            TU.idTarea = DR[e_VistaTareaUsuarioFields.IdTarea()].ToString().ToUpper();
            TU.idMetodoAccion = DR[e_VistaTareaUsuarioFields.idMetodoAccion()].ToString().ToUpper();
            TU.idRegistro = DR[e_VistaTareaUsuarioFields.idRegistro()].ToString().ToUpper();
            TU.TiempoEstimado = decimal.Parse(DR[e_VistaTareaUsuarioFields.TiempoEstimado()].ToString().ToUpper());
            return TU;
        }

        public static DataSet ObtenerDataSetTareas(string And)
        {
            try
            {
                DataSet DS = new DataSet();
                List<string> Campos = new List<string>();
                Type type = typeof(e_VistaTareaUsuarioFields);
                Campos = da_MotorDecision.VariablesDeLaClass(type);
                string SQL = "select ";
                foreach (string str in Campos)
                {
                    SQL += str.ToUpper() + ",";
                }
                SQL = SQL.Substring(0, SQL.Length - 1);
                SQL += " from " + e_TablasBaseDatos.VistaTareasUsuario();

                if (!string.IsNullOrEmpty(And.Trim()))
                {
                    SQL += " " + And;
                }
                SQL += " order by " + e_VistaTareaUsuarioFields.TiempoEstimado() + " desc";
                SQL += "," + e_VistaTareaUsuarioFields.idRegistro() + ", ";
                SQL += e_VistaTareaUsuarioFields.IDUSUARIO();
                DS = da_ConsultaDummy.GetDataSet(SQL, "MD");
                return DS;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static List<e_TareaUsuario> ObtenerTareasUsuarios(string idUsuario, string idMetodoAccion)
        {
            List<e_TareaUsuario> TareasUsuarios = new List<e_TareaUsuario>();
            double Interacion = 0;
            DataSet DS = new DataSet();
            string and = String.Empty;
            try
            {
                DS = ObtenerDataSetTareas(and);  // trae las tareas de Vista_TareasUsuario order by IdTarea,IDUSUARIO
                e_TareaUsuario TareaUsuario = new e_TareaUsuario();
                e_UsuarioProductivo UsuarioProductivo = new e_UsuarioProductivo();
                List<e_UsuarioProductivo> UsuariosProductivos = new List<e_UsuarioProductivo>();  // lista de UsuarioProductivo.
               
                // List<Independiente> Independientes = new List<Independiente>();
                /// Carga los parametros 
                /// Segundo Nivel = Dependiente   //TareaUsuario
                /// Tercer  Nivel = Independiente //UsuarioProductivo
                string NombreTareaUsuario = "";
                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    Interacion++;
                    if (DR.Table.Rows.IndexOf(DR) == 0)
                    {
                        TareaUsuario = ObtenerTareaUsuario(DR);  //  idTarea, idMetodoAccion, idRegistro, TiempoEstimado.
                        NombreTareaUsuario = TareaUsuario.idTarea;
                        UsuariosProductivos.Add(ObtenerUsuarioProductivo(DR));  // es el usuario que viene en la vista.(donde se lo asigno?)
                        if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1)
                        {
                            TareaUsuario.UsuariosProductivos = UsuariosProductivos;
                            TareasUsuarios.Add(TareaUsuario);
                        }
                    }
                    else
                    {
                        if (DR[e_VistaTareaUsuarioFields.IdTarea()].ToString().ToUpper() == NombreTareaUsuario.ToUpper())//si el metodo a cargar es el mismo se vuelve a cargar parametro
                        {
                            if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1) //Ultima vez que se ejecuta el ciclo
                            {
                                TareaUsuario = ObtenerTareaUsuario(DR);
                                UsuariosProductivos.Add(ObtenerUsuarioProductivo(DR));
                                TareaUsuario.UsuariosProductivos = UsuariosProductivos;
                                TareasUsuarios.Add(TareaUsuario);
                            }
                            else
                            {
                                UsuariosProductivos.Add(ObtenerUsuarioProductivo(DR));
                            }
                        }
                        else 
                        {
                            TareaUsuario.UsuariosProductivos = UsuariosProductivos;
                            TareasUsuarios.Add(TareaUsuario);
                            TareaUsuario = new e_TareaUsuario();
                            UsuariosProductivos = new List<e_UsuarioProductivo>();
                            TareaUsuario = ObtenerTareaUsuario(DR);
                            NombreTareaUsuario = TareaUsuario.idTarea;
                            UsuariosProductivos.Add(ObtenerUsuarioProductivo(DR));
                            if (DR.Table.Rows.IndexOf(DR) == DR.Table.Rows.Count - 1)
                            {
                                TareaUsuario.UsuariosProductivos = UsuariosProductivos;
                                TareasUsuarios.Add(TareaUsuario);
                            }
                        }
                    }
                }

                return TareasUsuarios;
            }
            catch (Exception)
            {
                return TareasUsuarios;
            }
        }

        public static string SiguienteTarea(string CodLeido)
        {
            string resp = "No hay tareas registradas para este alisto o ya está cerrado...";

            try
            {
                string[] datos = CodLeido.Split(';');
                string identificadorUsuario = datos[0];
                string idMaestrosolicitud = datos[1];
                string Actualiza = datos[2];
                string idbodega = datos[3];
                //string SSCC = CargarEntidadesGS1.GS1128_DevolverSSCCGenerado(datos[4].Trim());
                string idcompania = ObtenerCompaniaXUsuario(identificadorUsuario);



                n_ProcesarSolicitud Generatarea = new n_ProcesarSolicitud();
                string resultado = Generatarea.GeneraTarea(int.Parse(identificadorUsuario), Int64.Parse(idMaestrosolicitud), Actualiza, int.Parse(idbodega));  //, SSCC);

                string[] tarea = resultado.Split(';');

                if (!string.IsNullOrEmpty(tarea[0].ToString()))
                {
                    return resultado;
                }

            }
            catch (Exception ex)
            {

                return resp + "-" + ex.Message;
            }

            return resp;

        }

        //Uso Interno --- Continuar
        public static string InfoSSCC(string SSCC, out string destino, out decimal PesoKilos, out decimal DimensionSSCCM3, out decimal Equivalencia)
        {
            try
            {
                if (!String.IsNullOrEmpty(SSCC))
                {
                    destino = "";
                    PesoKilos = 0;
                    DimensionSSCCM3 = 0;
                    Equivalencia = 0;
                    string resp = "";
                    DataSet InfoSSCC = new DataSet();
                    bool Salir = false;
                    string idTablaCampoDocumentoAccion = "";
                    DataSet ListaAlistosSSCC = new DataSet();
                    DataSet Aux = new DataSet();
                    DataSet Aux2 = new DataSet();
                    string SQLConsecutivo = "SELECT " + e_TblConsecutivosSSCCFields.idConsecutivoSSCC() + 
                                            "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblConsecutivosSSCC() +
                                            "  WHERE " + e_TblConsecutivosSSCCFields.SSCCGenerado() + " = '" + SSCC + "'";

                    int idConsecutivoSSCC = Convert.ToInt32(n_ConsultaDummy.GetUniqueValue(SQLConsecutivo, idUsuario));
                    string SQLAlistosSSCC = "SELECT idRegistro FROM RELSSCCTRA where idConsecutivoSSCC = " + idConsecutivoSSCC;
                    ListaAlistosSSCC = n_ConsultaDummy.GetDataSet2(SQLAlistosSSCC, idUsuario);

                    if (ListaAlistosSSCC.Tables[0].Rows.Count > 0)
                    {

                        for (int contador = 0; contador < ListaAlistosSSCC.Tables[0].Rows.Count; contador++)
                        {
                            Salir = false;
                            int NumDocAccion = Convert.ToInt32(ListaAlistosSSCC.Tables[0].Rows[contador][0]);
                            int cont = 0;
                            while (Salir == false)
                            {

                                string SQLNumDocAccion = "SELECT idRegistro , idTablaCampoDocumentoAccion , NumDocumentoAccion From TRAIngresoSalidaArticulos where idRegistro = " + NumDocAccion;
                                Aux = n_ConsultaDummy.GetDataSet2(SQLNumDocAccion, idUsuario);
                                NumDocAccion = Convert.ToInt32(Aux.Tables[0].Rows[0][2]);
                                idTablaCampoDocumentoAccion = Aux.Tables[0].Rows[0][1].ToString();
                                string BanderaAlistoAprobado = "SELECT * FROM ADMAlistosAprobados Where idLineaDetalleSolicitud = " + NumDocAccion;
                                Aux2 = n_ConsultaDummy.GetDataSet2(BanderaAlistoAprobado, "0");

                                if (idTablaCampoDocumentoAccion.Equals("TRACEID.dbo.OPESALDetalleSolicitud") && Aux2.Tables[0].Rows.Count > 0)
                                    {
                                        string SQLInfo = "Select A.idArticulo , B.Nombre , A.Descripcion , A.Cantidad , A.idDestino , C.Nombre , A.idMaestroSolicitud , A.idUsuario , B.Equivalencia , B.PesoKilos , B.DimensionUnidadM3  From OPESALDetalleSolicitud A , "
                                        + "ADMMaestroArticulo B , ADMDestino C Where A.idArticulo = B.idArticulo and A.idDestino = C.idDestino and  A.idLineaDetalleSolicitud = " + NumDocAccion;

                                        InfoSSCC = n_ConsultaDummy.GetDataSet2(SQLInfo, idUsuario);
                                         
                                        resp += "\n\n Alisto: \n\n\n idArticulo: " + InfoSSCC.Tables[0].Rows[0][0].ToString() + "\n B.Nombre: " + InfoSSCC.Tables[0].Rows[0][1].ToString() + "\n Descripcion: " + InfoSSCC.Tables[0].Rows[0][2].ToString() +
                                        "\n Cantidad: " + InfoSSCC.Tables[0].Rows[0][3].ToString() + "\n NombreDestino: " + InfoSSCC.Tables[0].Rows[0][5].ToString() + "\n MaestroSolicitud: "
                                       + InfoSSCC.Tables[0].Rows[0][6].ToString() + "\n Equivalencia: " + InfoSSCC.Tables[0].Rows[0][8].ToString();
                                        destino = InfoSSCC.Tables[0].Rows[0][4].ToString();
                                        PesoKilos += Convert.ToDecimal(InfoSSCC.Tables[0].Rows[0][9].ToString());
                                        DimensionSSCCM3 += Convert.ToDecimal(InfoSSCC.Tables[0].Rows[0][10].ToString());
                                        Equivalencia += Convert.ToDecimal(InfoSSCC.Tables[0].Rows[0][8].ToString());
                                    }
                                    if (cont == 1)
                                    {
                                        Salir = true;
                                    }
                                    cont++;                                
                            }
                        }
                    }
                    else
                    {

                        resp = "No existen alistos asociados al SSCC";
                    }


                    if (String.IsNullOrEmpty(resp))
                        resp = "No existen alistos aprobados en el SSCC";

                    return resp;
                }
                else
                {
                    DimensionSSCCM3 = 0;
                    PesoKilos = 0;
                    destino = "";
                    Equivalencia = 0;
                    return null;
                }
            }
            catch (Exception)
            {
                DimensionSSCCM3 = 0;
                PesoKilos = 0;
                destino = "";
                Equivalencia = 0;
                return null;

            }
        }

        public static string SSCCRuta(string destino) 
        {   
            try
            {
                string TramaRutas = "";
                string[] Rutas;
                DataSet RutasDestino = new DataSet();
                string SQLRelDestinoRuta = "SELECT A.idRuta From RELRutaDestinos A WHERE A.idDestino = " + destino;
                RutasDestino = n_ConsultaDummy.GetDataSet2(SQLRelDestinoRuta, idUsuario);
                
                foreach (DataRow Row in RutasDestino.Tables[0].Rows)
                {
                    string dtidRuta = Row["idRuta"].ToString();
                    TramaRutas += dtidRuta + ";";                
                }
              
                Rutas = TramaRutas.Split(';');

                for (int contador = 0; contador < Rutas.Length; contador++)
                {
                      

                }
               return "";
            }

            catch (Exception)
            {

                return null;
            }


        }

        ////public static string Clasificacion(string idArticulo) 
        ////{
        ////    try
        ////    {
        ////        string GTIN = "SELECT A.GTIN FROM ADMMaestroArticulo A Where idArticulo = " + idArticulo;
        ////        string Equivalencia = n_ConsultaDummy.GetUniqueValue(GTIN, idUsuario);

        //                    if (RegistrosTRA.Tables[0].Rows.Count == 0) //  si ho hay filas se llego al ultimo registro de la trazabilidad
        //                  break;
        //            }
        //            }

        //           //cuando la Transacción permite la devolución, se extrae la etiqueta de la ubicación actual y la ubicación anterior; para poder ejecutar el metodo
        //           //LeerCodigoParaUbicarHH con ambas ubicaciones y hacer la devolución.
        //            if (Permitedevolucion == 1)
        //            {
        //                SQL = "select  replace((replace(" + e_VistaCodigosUbicacionFields.ETIQUETA() + ",'(','')),')','') as etiqueta from " + 
        //                             e_TablasBaseDatos.VistaCodigosUbicacion() + " where " + e_VistaCodigosUbicacionFields.idUbicacion() + " = '" + Ubicacionactual + "'";
        //            Ubicacionactual = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                
        //                 SQL = "select  replace((replace(" + e_VistaCodigosUbicacionFields.ETIQUETA() + ",'(','')),')','') as etiqueta from " + 
        //                             e_TablasBaseDatos.VistaCodigosUbicacion() + " where " + e_VistaCodigosUbicacionFields.idUbicacion() + " = '" + Ubicacionmover + "'";
        //            Ubicacionmover = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

        //                string Codigo = spl[1].Trim() + ";" + Ubicacionactual + ";" + Ubicacionmover + ";0";

        //           Respuesta = LeerCodigoParaUbicarHH(Codigo, idUsuario, "75");

        //                if (Respuesta == "Transaccion exitosa")  // si fue positivo el traslado, se actualiza a devuelto en la tabla RELSSCCTRA.
        //                {
        //                    SQL = "UPDATE " + e_TablasBaseDatos.TblSSCCTRA() + " SET " + e_TblSSCCTRAFields.artDevuelto() +
        //                                  " = 1 WHERE " + e_TblSSCCTRAFields.idRegistro() + " = " + idRegistroriginal;
        //                    bool devuelto = da_ConsultaDummy.PushData(SQL, idUsuario);

        //                    if (!devuelto)
        //                        Respuesta += "NO pudo actualizarse SSCC"; 
        //                }

        //            }
        //            else
        //                Respuesta += "Artículo no puede ser devuelto ";
        //        }
        //        else
        //            Respuesta += "SSCC o Artículo a Devolver no corresponde ";

        //    }
        //    catch (Exception ex)
        //    {
        //        Respuesta += "Problemas con el proceso de devolución... " + ex.Message;
        //    }

        //    return Respuesta;
        //}

        /*
        public static string Clasificacion(string idArticulo) 
        {
            try
            {
                string GTIN = "SELECT A.GTIN FROM ADMMaestroArticulo A Where idArticulo = " + idArticulo;
                string Equivalencia = n_ConsultaDummy.GetUniqueValue(GTIN, idUsuario);


                return 
            }
            catch (Exception)
            {

                throw;
            }
                
        }



        public static string ConfirmarEquivalencia(int Cantidad , int idArticulo)
        {    
        try 
        {
            string SqlEquivalenciaArticulo = "SELECT A.Equivalencia FROM ADMMaestroArticulo A Where idArticulo = " +  idArticulo;

            float Equivalencia = Convert.ToInt64( n_ConsultaDummy.GetUniqueValue(SqlEquivalenciaArticulo, idUsuario));



            
        }catch(Exception e)
        {
 
            
            
        }
       

        }
         SQL = "declare @idarticulo bigint," +
                                 "@fechavenc date," +
                                 "@lote varchar(200)," +
                                 "@cantidad as decimal(18,0) " +

                        "SELECT  @idarticulo = " + ArticuloDevolver + "," +
                                "@fechavenc = fechavencimiento," +
                                "@lote = lote " +
                          "FROM traceid.dbo.TRAIngresoSalidaArticulos " +
                          "where  idregistro = " + idRegistro + " " +

        #endregion CalculoHorasHombreTareasin

          #region  Despacho


        public static string AsociarSSCCVehiculo(string SSCC , string UbicacionParqueo)
        {
             try 
	        {	  
                 string destino = "";
                 decimal PesoKilos ;
                 decimal DimensionSSCCM3 ;
                 string respuesta = n_WMS.InfoSSCC(SSCC, out destino, out PesoKilos, out DimensionSSCCM3);

                 return respuesta;
		
	        }
	        catch (Exception)
	        {
		
		        throw;
	        }
        
        } 

         #endregion Despacho

                        "SELECT  a.idUbicacion" +
                           "  FROM  traceid.dbo.TRAIngresoSalidaArticulos " +
                           "  Where   idarticulo = @idarticulo" +
                           "          and  fechavencimiento = @fechavenc" +
                           "          and  lote = @lote" +
                           "          and  idregistro >= " + idRegistro;
       */
        #endregion CalculoHorasHombreTareas
   
        #region Formulario Calidad
        
        public static string AgregarFormularioCalidad(string CodLeido, string idUsuario)
        {
           //Inserta los datos del formulario de calidad en la bd
            string[] sp = CodLeido.Split(';');
            string OrdenCompra = sp[0].ToString().Trim();
            string Articulo = sp[1].ToString().Trim();
            string Pregunta = sp[2].ToString().Trim();
            string Resp = sp[3].ToString().Trim();
            string Comentario = sp[4].ToString().Trim();
            string AdminPass = sp[5].ToString().Trim();
            string Temperatura = sp[6].ToString().Trim();
            string vencimiento = sp[7].ToString().Trim();
            string evalua = sp[8].ToString().Trim(); // esta se usa para que no se evalue la temperatura 2 veces.
            string codigoBarras = sp[9].ToString().Trim();
            string login = sp[10].ToString().Trim();
            string agregadopositivo = "";          
            string SQL = "";
            string Cantidad = CargarEntidadesGS1.GS1128_DevolverCantidad(codigoBarras);
            string FechaVencimiento = CargarEntidadesGS1.GS1128_DevolverFechaVencimiento(codigoBarras);
            string Lote = CargarEntidadesGS1.GS1128_DevolveLote(codigoBarras);

            try
            {
                if (evalua == "True")
                {
                    // primero validamos si la pregunta es para evaluar la temperatura o la mínima vida útil.
                    switch (EvaluaPregunta(Pregunta))
                    {
                        case 1:
                            // evalua temperatura
                            if (string.IsNullOrEmpty(Temperatura))
                            {
                                return "Valor de Temperatura nula o vacía.... intente de nuevo;False;0";
                            }

                            string MensajeTemperatura = validaTemperaturaArticulo(Articulo, Temperatura, idUsuario);
                            string[] EvaluaTemperatura = MensajeTemperatura.Split(';');
                            if (EvaluaTemperatura[1] == "False")
                                return EvaluaTemperatura[0] + ";" + EvaluaTemperatura[1] + ";" + EvaluaTemperatura[2];
                            else
                                Comentario += "Temperatura: " + Temperatura;

                            break;
                    }
                }

                string idCompania = ObtenerCompaniaXUsuario(idUsuario);

                if (Resp == "2")  // en caso de que se haya contestado negativamente, se procede a validar el password de usuario ingresado.
                {
                    if (!string.IsNullOrEmpty(AdminPass) && !AdminPass.Equals("---"))  // si el password no viene, entonces se ingresa el registro para que se pueda sacar un reporte de 
                    {
                        string ValidaPassword = ValidaAdminPass(AdminPass, idUsuario, login);
                        string[] mensaje = ValidaPassword.Split(';');
                        if (mensaje[1] == "False")  // no se pudo validar el password del administrador.
                        {

                            return mensaje[0]+ ";" + mensaje[1] + ";0";
                        }

                        agregadopositivo = mensaje[0] + "-"; // se adjunta este mensaje al que manda el metodo para informar al usuario.
                        AdminPass = mensaje[0].Substring(12, mensaje[0].Length - 12);  // se pone el usuario del password encontrado, para que se registre en la tabla 
                    }
                    else
                    {

                        // insertar el tabla de rechazado
                        string queryRechazado = "insert into OPEArticulosRechazadosOC(idCompania,idUsuario,idMaestroOrdenCompra,idArticulo,Cantidad,Lote,FechaVencimiento)" +
                        "values('" + idCompania + "'," + Convert.ToInt32(idUsuario) + "," + Convert.ToInt32(OrdenCompra) + "," + Articulo + "," + Cantidad + ",'" + Lote + "','" + FechaVencimiento.Replace("-","") + "')";

                        da_ConsultaDummy.PushData(queryRechazado, idUsuario);
                    }
                }


                SQL = "INSERT INTO " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblADMRespuestasFormulario() + " ("
                    + e_TblADMRespuestasFormulario.OrdenCompra() + ", "
                    + e_TblADMRespuestasFormulario.Articulo() + ", " + e_TblADMRespuestasFormulario.idPregunta() + ", "
                    + e_TblADMRespuestasFormulario.idRespuesta() + ", " + e_TblADMRespuestasFormulario.Comentarios() + ", "
                    + e_TblADMRespuestasFormulario.Usuario() + ", " + e_TblADMRespuestasFormulario.UsuarioAutoriza() + ", "
                    + e_TblADMRespuestasFormulario.idCompania()
                    + ") VALUES('" + OrdenCompra + "', '"
                    + Articulo + "', '"
                    + Pregunta + "', '"
                    + Resp + "', '"
                    + Comentario + "', '"
                    + idUsuario + "', '"
                    + AdminPass + "', '"
                    + idCompania + "')";
                if (da_ConsultaDummy.PushData(SQL, idUsuario))
                {
                    return agregadopositivo + "Formulario Ingresado Exitosamente";
                }
                else
                {
                    return "Ops! Ha ocurrido un Error, Codigo:TID-AD-MDS-000008;False;0";
                }
            }
            catch (Exception ex)
            {
                return ("Ops! Ha ocurrido un Error, Codigo:TID-UI-OPE-ING-000006-" + ex.Message + "-" + ex.StackTrace + "-" + ex.InnerException + ";False;0");
            }
        }

        #endregion Formulario Calidad

        #region CONCAPAN

        public static string IngresoEvento(string CodLeido)
        {
            DataSet DatosRegistro = new DataSet();
            string respuesta = "";
            string idUsuario = "";
            string sqlUsuario = "";
            string[] spl = CodLeido.Split(';');
            string opcion = spl[1].ToString();
            string TipoComida = spl[2].ToString();

            try
            {
                switch (opcion)
                {
                    case "0":

                        //respuesta = "Error al completar";
                        //sqlUsuario = "Select A.IdUsuario  FROM [TEST_DIV_ACCESO_DEBUG].[dbo].[Vistatbl_NumeroRegistro] A Where A.IdPerfilUsuario = '" + spl[0] + "'";
                        //idUsuario = n_ConsultaDummy.GetUniqueValue(sqlUsuario, "0");
                        //string SqlCodBarras = "SELECT B.IdPerfilUsuario , B.IdPerfilUsuario FROM [TEST_DIV_ACCESO_DEBUG].[dbo].[Vistatbl_NumeroRegistro] B Where B.IdPerfilUsuario = '" + idUsuario + "'";
                        //DatosRegistro = n_ConsultaDummy.GetDataSet2(SqlCodBarras, "0");


                        //if (DatosRegistro.Tables[0].Rows.Count > 0)
                        //{
                        //    string sqlInsert = "INSERT INTO [TEST_DIV_ACCESO_DEBUG].[dbo].[tbl_Ingreso_Eventos] (IdPerfilUsuario, Identificacion , CodBarras) values (" + DatosRegistro.Tables[0].Rows[0][0].ToString() + "," + spl[0].ToString() + "," + DatosRegistro.Tables[0].Rows[0][1].ToString() + " ) ";

                        //    n_ConsultaDummy.PushData(sqlInsert, "0");

                        //    respuesta = "Ingreso Correcto";
                        //}

                        break;

                    case "1":

                        switch (TipoComida)
                        {

                            case "0":


                                respuesta = "Solicitud de almuerzo denegada";
                                string SqlUsuarioSol = "SELECT A.IdPerfilUsuario , B.IdPerfilUsuario FROM [PROD_DIV_ACCESO].[dbo].[tblPerfilUsuario] A , [PROD_DIV_ACCESO].[dbo].[VistaAlmuerzosDisponibles] B WHERE A.Identificacion = '" + spl[0] + "'" + "and B.IdPerfilUsuario = A.IdPerfilUsuario";
                                idUsuario = n_ConsultaDummy.GetUniqueValue(SqlUsuarioSol, "0");

                                if (!String.IsNullOrEmpty(idUsuario))
                                {

                                    string Sqlbandera = "SELECT A.IdPerfilUsuario FROM [PROD_DIV_ACCESO].[dbo].[tbl_Almuerzo_Evento] A Where A.FechaRegistro = (CONVERT([varchar](10),getdate(),(103))) and A.IdPerfilUsuario = " + idUsuario;
                                    string BanderaAlmuerzo = n_ConsultaDummy.GetUniqueValue(Sqlbandera, "138");

                                    if (!String.IsNullOrEmpty(BanderaAlmuerzo))
                                    {
                                        respuesta = "Ya retiro el almuerzo del día de hoy";
                                    }
                                    else
                                    {

                                        string SQLProcesarAlmuerzo = "INSERT INTO [PROD_DIV_ACCESO].[dbo].[tbl_Almuerzo_Evento] (IdPerfilUsuario, Consumido ) values (" + idUsuario + "," + "'1'" + ") ";

                                        n_ConsultaDummy.PushData(SQLProcesarAlmuerzo, "138");

                                        respuesta = "Procesando Almuerzo";
                                    }
                                }
                                else
                                {

                                    respuesta = "Usuario no Registrado en el evento";

                                }
                                break;
                            /////

                            case "1":
                                respuesta = "Solicitud de cena denegada";

                                //string SqlUsuario = "SELECT A.IdPerfiUsuario  FROM [TEST_DIV_ACCESO_DEBUG].[dbo].[VistaAlmuerzosDisponibles] A WHERE A.Identificacion = '" + spl[0] + "'";
                                //idUsuario = n_ConsultaDummy.GetUniqueValue(SqlUsuario, "0");

                                if (!String.IsNullOrEmpty(idUsuario))
                                {

                                //    string Sqlbandera = "SELECT A.IdPerfilUsuario FROM [TEST_DIV_ACCESO_DEBUG].[dbo].[tbl_Cena] A Where A.FechaRegistro = (CONVERT([varchar](10),getdate(),(103))) and A.idUsuario = " + idUsuario;
                                //    string BanderaAlmuerzo = n_ConsultaDummy.GetUniqueValue(Sqlbandera, "138");

                                //    if (!String.IsNullOrEmpty(BanderaAlmuerzo))
                                //    {
                                //        respuesta = "Ya retiro el cena del día de hoy";
                                //    }
                                //    else
                                //    {

                                //        string SQLProcesarAlmuerzo = "INSERT INTO [PROD_CONCAPAN].[dbo].[tbl_Cena] (idUsuario, Consumido) values (" + idUsuario + "," + "'1'" + ") ";

                                //        n_ConsultaDummy.PushData(SQLProcesarAlmuerzo, "138");

                                //        respuesta = "Procesando Cena";
                                //    }
                                    }
                                else
                                {

                                    respuesta = "Usuario no Registrado en el evento";

                                }

                                break;

                        }

                        break;

                }
                return respuesta;
            }
            catch (Exception)
            {

                return respuesta;
            }

        }

        #endregion CONCAPAN

        #region Devolucion Articulo del SSCC

        public static string ValidaDevolucionArticuloSSCC(string CodLeido, string idUsuario)
        {
            string Respuesta = "Devolución de artículo no es posible-";
            string SQL = "";
           
            try
            {
                //TRAMA = SSCC;ARTíCULO;UBICACIÓN A MOVER.
                string[] spl = CodLeido.Split(';');
                string SSCCLeido = CargarEntidadesGS1.GS1128_DevolverSSCCGenerado(spl[0].Trim());  // obtengo el SSCC generado de la tabla Traceid.dbo.ADMConsecutivosSSCC
                string[] Articulo = ObtenerIdArticuloNombreCodigoLeido_GS1128(spl[1].Trim(), idUsuario).Split(';');
                string ArticuloDevolver = Articulo[0]; // obtengo el idArticulo
                string Ubicacionmover = spl[2];
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);

               // Este query verifica que la ubicación a mover sea válida y que pertenezca a la compañia actual.
                SQL = "select " + e_VistaCodigosUbicacionFields.idUbicacion() +
                      "  from " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaCodigosUbicacion() + 
                      "  where replace((replace(" + e_VistaCodigosUbicacionFields.ETIQUETA() + ",'(','')),')','') = '" + Ubicacionmover + "'" +
                      "        AND " + e_VistaCodigosUbicacionFields.idCompania() + " = '" + idCompania + "'";
                string Ubicacionmovervalida = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

               // validación de los datos ingresados.
                if (string.IsNullOrEmpty(SSCCLeido))
                    return Respuesta += "SSCC no válido...";

                if (string.IsNullOrEmpty(ArticuloDevolver))
                    return Respuesta += "Artículo no válido...";

                if (string.IsNullOrEmpty(Ubicacionmovervalida))
                    return Respuesta += "Ubicación a mover no es válida";

                 EntidadesGS1.e_GTIN GTIN = new EntidadesGS1.e_GTIN();
                 bool EsGTIN = CargarEntidadesGS1.GS1128_ObtenerGTIN(spl[1].Trim(), out GTIN);
                 Int64 Cantidad = Int64.Parse(GTIN.VLs[0].Cantidad.ToString());

                string idCompaniaArticulo = ObtenerCompaniaXArticulo(ArticuloDevolver, idUsuario);

                if (idCompaniaArticulo.Equals(idCompania))  // se válida que estemos en la compañia correcta.
                {
                    n_ProcesarSolicitud DevolverArtículo = new n_ProcesarSolicitud();
                    Respuesta = DevolverArtículo.DevolverArticuloSSCC(idCompania,
                                                                      SSCCLeido,
                                                                      Cantidad,
                                                                      Ubicacionmover,
                                                                      Int64.Parse(ArticuloDevolver));
                }
                else
                    Respuesta += "Compañia del artículo no corresponde a la del usuario";

            }
            catch (Exception ex)
            {
                Respuesta += "-Problemas con el proceso de devolución... " + ex.Message;
            }

            return Respuesta;
        }

        #endregion Devolucion Articulo del SSCC

        #region AjusteInventario

        #region CargarDrop

        public static void CargarDrop(DropDownList ddl, string Sql, string idUsuario)
        {
           /* try
            {
                ddl.Items.Clear();
                DataSet DSBaseDatos = new DataSet();
                DSBaseDatos = n_ConsultaDummy.GetDataSet(Sql, "0");
                if (DSBaseDatos != null)
                {
                    if (DSBaseDatos.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dsRowEvento in DSBaseDatos.Tables[0].Rows)
                        {

                            //if (dsRowEvento["Nombre"].ToString().Equals("Nombre"))
                            //{
                                string name = dsRowEvento["Nombre"].ToString();
                                ddl.Items.Add(name);
                            //}

                            //if (dsRowEvento["ddlidUnidadMedida"].ToString().Equals("ddlidUnidadMedida"))
                            //{
                            //    string name = dsRowEvento["ddlidUnidadMedida"].ToString();
                            //    ddl.Items.Add(name);
                            //}

                            //if (dsRowEvento["ddlidTipoEmpaque"].ToString().Equals("ddlidTipoEmpaque"))
                            //{
                            //    string name = dsRowEvento["ddlidTipoEmpaque"].ToString();
                            //    ddl.Items.Add(name);
                            //}
                        }

                        ddl.DataBind();
                        ddl.Items.Insert(0, new ListItem("--Seleccionar--"));
                    }

                }
            }
            catch (Exception ex)
            {
               

            }*/

        }

        public static bool CargarDropGood(DropDownList ddl, string Sql, string idUsuario)
        {
            try
            {
                ddl.Items.Clear();

                string ID = ddl.ID.Substring(3);
                ID = ID.Replace("0", string.Empty);
              

                DataSet DSBaseDatos = new DataSet();
                DSBaseDatos = n_ConsultaDummy.GetDataSet(Sql, "0");
                if (DSBaseDatos.Tables[0].Rows.Count > 0)
                {
                    ddl.DataSource = DSBaseDatos;
                    ddl.DataTextField = "Nombre";
                    ddl.DataValueField = ID;
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("--Seleccionar--"));
                }
          
            }

            catch (Exception)
            {
                return false;
            }

                return true;
        }

        public static string CargarDropStr(string CodLeido, string idUsuario)
        {
            string elemento = "";

            try
            {
                DataSet DSBaseDatos = new DataSet();
                DSBaseDatos = n_ConsultaDummy.GetDataSet(CodLeido, idUsuario);

                if (DSBaseDatos != null)
                {
                    if (DSBaseDatos.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dsRowEvento in DSBaseDatos.Tables[0].Rows)
                        {
                            elemento += dsRowEvento["Nombre"].ToString().Trim() + ";";
                        }

                        elemento = elemento.Substring(0,elemento.Length - 1);  // esto le quita el ; del final.

                    }

                }
            }

            catch (Exception ex)
            {
                elemento = ex.Message;
            }

            return elemento;
        }
        
        #endregion

        public static string HHRetornarInfoSSCC(string CodLeido, string idUsuario)
        {

            try
            {
                string respuesta = "";
                string SSCC = "";
                string destino = "";
                decimal PesoKilos = 0;
                decimal DimensionSSCCM3 = 0;
                decimal Equivalencia = 0;

                string[] Trama = CodLeido.Split('-');

                respuesta = InfoSSCC(SSCC, out destino, out PesoKilos, out DimensionSSCCM3, out Equivalencia);

                respuesta += destino;
                return respuesta;
            }
            catch (Exception)
            {
                return null;
            }

        }

        #endregion

#region AsociarVehiculo

        public static string AsociarVehiculoSSCC(string CodLeido, string idUsuario)
        {
            string respuesta = "";
            try
            {
                string[] spl = CodLeido.Split(';');
                string UbicacionParqueo = spl[0];
                string SSCCVehiculo = spl[1];
                string DimsensionSSCCM3 = spl[2];
                string Equivalencia = spl[3];
                string PesoKilos = spl[4];
                string idVehiculo = spl[5];
                string Idruta = spl[6];
                string SQL = "";

               // se válida que el SSCC no haya sido procesado anteriormente.
                SSCCVehiculo = CargarEntidadesGS1.GS1128_DevolverSSCCGenerado(SSCCVehiculo);  // obtengo el SSCC generado de la tabla Traceid.dbo.ADMConsecutivosSSCC
               
                // Este query verifica que la asociación no se haya hecho.
                SQL = "SELECT 1 FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TraVehiculoTrasladoSSSCC() + " AS A" +
                      "           INNER JOIN " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblConsecutivosSSCC() + " AS B ON (A." + e_TraVehiculoTrasladoSSSCC.ConsecutivoSSCC() + " = B." + e_TblConsecutivosSSCCFields.ConsecutivoSSCC() + ")" +
                      "          WHERE B." + e_TblConsecutivosSSCCFields.SSCCGenerado() + " = '" + SSCCVehiculo + "'";
                string validaAsociacion = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                if (validaAsociacion == "1")
                {
                    respuesta += "Asociacón con vehículo ya fue realizada...";
                    return respuesta;
                }

                if (string.IsNullOrEmpty(SSCCVehiculo))
                {
                    respuesta = "SSCC no válido.";
                    throw new Exception(respuesta);
                }

                //if (string.IsNullOrEmpty(PesoKilos) || Single.Parse(PesoKilos) <= 0)
                //{
                //    respuesta = "Peso no válido";
                //    throw new Exception(respuesta);
                //}

                //if (string.IsNullOrEmpty(DimsensionSSCCM3) || Single.Parse(DimsensionSSCCM3) <= 0)
                //{
                //    respuesta = "Dimensión no válida";
                //    throw new Exception(respuesta);
                //}

                if (string.IsNullOrEmpty(idVehiculo) || idVehiculo == "--Seleccionar--")
                {
                    respuesta = "Vehículo no válido";
                    throw new Exception(respuesta);
                }

                if (string.IsNullOrEmpty(Idruta) || Idruta == "--Seleccionar--")
                {
                    respuesta = "Ruta no válida";
                    throw new Exception(respuesta);
                }

                //DataSet RutaVehiculo = new DataSet();
                string SqlidUbicacion = "Select " + e_VistaCodigosUbicacionFields.idUbicacion() + 
                                        "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaCodigosUbicacion() + " as a " +
                                        "    INNER JOIN " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblZonas() + " AS b on (a." + e_VistaCodigosUbicacionFields.idZona() + " = b.idZona) " +
                                        "  WHERE replace((replace(etiqueta,'(','')),')','') = '" + UbicacionParqueo + "'";
                string idUbicacion = n_ConsultaDummy.GetUniqueValue(SqlidUbicacion, "0");  // se obtiene el idUbicación del parqueo.
                if (!String.IsNullOrEmpty(idUbicacion))
                {
                   // se pone la instrucción "SET DATEFIRST 1" para que el resultado del query "SqlDiaSemana" coincida con el orden de la tabla "ADMDiaSemana"
                    string SqlDiaSemana = "SET DATEFIRST 1   SELECT DatePart(WeekDay, GetDate()) As Dia , CONVERT(VARCHAR, getdate(), 108) As Hora";
                    DataSet DiaHora = n_ConsultaDummy.GetDataSet2(SqlDiaSemana, idUsuario);
                    string[] idHora = DiaHora.Tables[0].Rows[0][1].ToString().Split(':');
                    if (idHora[0] != "10" && idHora[0] != "20")  // si la hora es 10 de la mañana u ocho de la noche, no aplica el replace.
                        idHora[0] = idHora[0].ToString().Replace("0", "").Trim();

                    if (!string.IsNullOrEmpty(Idruta))
                    {
                        string idCompania = ObtenerCompaniaXUsuario(idUsuario);
                       //OBTIENE EL idConsecutivoSSCC DEL SSCC LEIDO
                        SQL = "SELECT " + e_TblConsecutivosSSCCFields.idConsecutivoSSCC() +
                              "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblConsecutivosSSCC() +
                              "  WHERE " + e_TblConsecutivosSSCCFields.idCompania() + " = '" + idCompania + "'" +
                              "        AND " + e_TblConsecutivosSSCCFields.SSCCGenerado() + " = '" + SSCCVehiculo + "'" +
                              "        AND " + e_TblConsecutivosSSCCFields.SSCCGenerado() + " IS NOT NULL "; 
                        string idConsecutivoSSCC = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                        string InsertTraVehiculoSSCC = "INSERT INTO " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TraVehiculoTrasladoSSSCC() +
                                                                                 "(" + e_TraVehiculoTrasladoSSSCC.ConsecutivoSSCC() + "," +
                                                                                       e_TraVehiculoTrasladoSSSCC.idVehiculo() + "," +
                                                                                       e_TraVehiculoTrasladoSSSCC.PesoKilos() + "," + 
                                                                                       e_TraVehiculoTrasladoSSSCC.DimensionM3() + "," +
                                                                                       e_TraVehiculoTrasladoSSSCC.Equivalencia() + "," +
                                                                                       e_TraVehiculoTrasladoSSSCC.idDia() + "," +
                                                                                       e_TraVehiculoTrasladoSSSCC.idHoraDia() + "," +
                                                                                       e_TraVehiculoTrasladoSSSCC.IdUbicacionParqueo() + ") " +
                                                       "   Values(" + idConsecutivoSSCC + "," +
                                                                      idVehiculo + "," + 
                                                                      "(" + PesoKilos.Replace(",", ".").Trim() + "),(" + 
                                                                      DimsensionSSCCM3.Replace(",", ".").Trim() + "),(" + 
                                                                      Equivalencia.Replace(",", ".").Trim() + ")," +
                                                                      DiaHora.Tables[0].Rows[0][0].ToString() + "," +
                                                                      idHora[0].ToString() + "," +
                                                                      idUbicacion + ")";

                        //switch (n_WMS.ObtenerDisponibilidadCamion(idVehiculo, Convert.ToDecimal(PesoKilos), Convert.ToDecimal(DimsensionSSCCM3)))
                        //{

                        //    case "Solicitud Procesada":
                                if (n_ConsultaDummy.PushData(InsertTraVehiculoSSCC, "0"))
                                    respuesta = "SSCC Asociado con exito";
                                else
                                    respuesta = "Error al asociar SSCC...";
                        //        break;

                        //    default:
                        //        respuesta = "Error al asociar SSCC";
                        //        break;

                        //}
                    }
                    else
                        respuesta = "Ruta para este día y hora no establecida";
                }
                else 
                {
                   respuesta = "Error... ingrese la ubicación correcta";
                   
                }

              
            }
            catch (Exception ex)
            {

                respuesta = ex.Message;
            }

            return respuesta;
        
        }

#endregion

        #region InfoSSCC
        public static string VerInfoSSCC(string CodLeido, string idUsuario)   
        {
            string resp = "";
            string destino = "";
            Single PesoKilos = 0;
            Single DimensionSSCCM3 = 0;
            string Equivalencia = "0";

            try
            {
                if (!String.IsNullOrEmpty(CodLeido))
                {
                    DataSet InfoSSCC = new DataSet();
                    n_ProcesarSolicitud n_InfoSSCC = new n_ProcesarSolicitud();
                    string idCompania = ObtenerCompaniaXUsuario(idUsuario);
                    string SSCCLeido = CargarEntidadesGS1.GS1128_DevolverSSCCGenerado(CodLeido);  // obtengo el SSCC generado de la tabla ADMConsecutivosSSCC
                    if (!string.IsNullOrEmpty(SSCCLeido) && !string.IsNullOrEmpty(idCompania))
                    {
                        InfoSSCC = n_InfoSSCC.DevuelveInfoSSCC(SSCCLeido, idCompania);

                        if (InfoSSCC.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow DR in InfoSSCC.Tables[0].Rows)
                            {
                                resp += "\n Indice SSCC: " + DR["idConsecutivoSSCC"].ToString() +
                                        "\n idArticulo......: " + DR["idArticulo"].ToString() +
                                        "\n idArticulo ERP .: " + DR["IdERP"].ToString() +
                                        "\n Nombre..........: " + DR["Nombre"].ToString() +
                                        "\n GTIN............: " + DR["GTIN"].ToString() +
                                        "\n Cantidad........: " + DR["Cantidad"].ToString() +
                                        "\n PesoKilos.......: " + DR["Kg"].ToString() +
                                        "\n DimenciónM3.....: " + DR["M3"].ToString() +
                                        "\n MaestroSolicitud: " + DR["idMaestroSolicitud"].ToString();

                                PesoKilos += Single.Parse(DR["Kg"].ToString());
                                DimensionSSCCM3 += Single.Parse(DR["M3"].ToString());
                                destino = DR["NombreDestino"].ToString();
                            }
                        }
                        else
                        {
                            resp = "Inconsistencia de proceso... solicitud no asociado al SSCC o no hay registro de solicitud;0;0;0;0";
                            return resp;
                        }
                    }
                    else
                    {
                        resp = "SSCC o compañia no válidos;0;0;0;0";
                    }

                }
                else
                {
                    resp = "SSCC en blanco o no válido;0;0;0;0";
                }
                
            }
            catch (Exception ex)
            {
                resp = ex.Message + ";0;0;0;0";
            }
                   

             return resp + ";" + destino + ";" + PesoKilos.ToString() + ";" + DimensionSSCCM3.ToString() + ";" + Equivalencia;
               
                //            }
                //            else
                //            {
                //                resp = "Inconsistencia de proceso... solicitud no asociado al SSCC o no hay registro de solicitud;0;0;0;0";
                //                return resp;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        resp = "No existen alistos asociados al SSCC;0;0;0;0";
                //    }


                //    if (String.IsNullOrEmpty(resp))
                //        resp = "No existen alistos aprobados en el SSCC;0;0;0;0";

                //    return resp + ";" + destino + ";" + PesoKilos.ToString() + ";" + DimensionSSCCM3.ToString() + ";" + Equivalencia; ;
                //}
               
        }

        #region ObtenerarticuloGranel
        public static string ObtenerArticuloGranel(string CodLeido, string idUsuario)
        {
            string Granel = "";
            string cantidad = "0";
            EntidadesGS1.e_GTIN GTIN = new EntidadesGS1.e_GTIN();
            try
            {
                if (CodLeido.Substring(0, 2) != "01")  // en caso de leer un GTIN13-GTIN14 se le pone adelante el 01 para poder procesar la recepción-ubicación-alisto.
                {
                    if (CodLeido.Length == 13)
                        CodLeido = "010" + CodLeido;
                    else if (CodLeido.Length == 14)
                        CodLeido = "01" + CodLeido;
                }

               string IdArticulo = ObtenerIdArticuloCodigoLeido_GS1128(CodLeido, idUsuario);
                if (string.IsNullOrEmpty(IdArticulo))
                    Granel = "Artículo no valido o no corresponde a la empresa asociada;False";
                else
                {
                    string[] Articulo = ObtenerIdArticuloNombreCodigoLeido_GS1128(CodLeido, idUsuario).Split(';');
                    string FV = CargarEntidadesGS1.GS1128_DevolverFechaVencimiento(CodLeido);
                    string Lote = CargarEntidadesGS1.GS1128_DevolveLote(CodLeido);
                    string peso = CargarEntidadesGS1.GS1128_DevolverPeso(CodLeido);
                    bool EsGTIN = CargarEntidadesGS1.GS1128_ObtenerGTIN(CodLeido, out GTIN);
                    if (peso == "0")
                    {
                        //cantidad = GTIN.VLs[0].Cantidad.ToString();
                        cantidad = Articulo[2].ToString();
                    }
                    else
                    { 
                        cantidad = peso;
                    }

                   Granel = "Ok;" + CargarEntidadesGS1.GS1128_EsArticuloGranel(CodLeido).ToString() + ";" + Articulo[1] + ";" + Articulo[0] + ";" + 
                            FV + ";" + Lote + ";" + cantidad;
               }
              
            }
            catch (Exception)
            {
                return "Error en lectura;False";
            }

            return Granel;
        }
        #endregion

        public static string ProcesarCodigoGS1ArticuloGranel(string CodLeido, string idUsuario)
        {
           // este metodo reconstruye el código GS1, solo para el caso de artículos a granel.
            try
            {
                string ano, mes, dia, codigoGS1, SQL, VI;
                string[] cl = CodLeido.Split(';');  // trama que trae el código GS1 actual y la cantidad puesta por el usuario.
                string[] idArticulo = ObtenerIdArticuloCodigoLeido_GS1128(cl[0], idUsuario).Split('|');
                string fechaven = "";
 
               // con este query determinamos si el artículo, con la nueva cantidad, ya esta registrado en la tabla detalle de OC.
               // y si es así, determinar si ya esta regristrado en la tabla TRA.
                SQL = "SELECT " + e_TblDetalleOrdenesCompraFields.idDetalleOrdenCompra() +
                      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleOrdenesCompra() +
                      "  WHERE " + e_TblDetalleOrdenesCompraFields.idArticulo() + " = '" + idArticulo[0] + "'" +
                      "          AND " + e_TblDetalleOrdenesCompraFields.CantidadxRecibir() + " = '" + cl[1] + "'" +
                      "          AND " + e_TblDetalleOrdenesCompraFields.idMaestroOrdenCompra() + " = " + cl[2];
                string ValorIdCampo = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                VI = "";

                if (!string.IsNullOrEmpty(ValorIdCampo))  // si devolvio un valor del idDetalleOrdenCompra, se procede a determinar 
                {                                         // si ya existe una transaccion con ese idDetalleOrdenCompra.
                    SQL = "";
                    SQL = "SELECT " + e_TblTransaccionFields.idRegistro() +
                          "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                          "  WHERE " + e_TblTransaccionFields.NumDocumentoAccion() + " = '" + ValorIdCampo + "'" +
                          "    AND " + e_TblTransaccionFields.Cantidad() + " = '" + cl[1] + "'" +
                          "    AND " + e_TblTransaccionFields.idArticulo() + " = " + idArticulo[0] +
                          "    AND " + e_TblTransaccionFields.idTablaCampoDocumentoAccion() + " = '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleOrdenesCompra() + "'" +
                          "    AND " + e_TblTransaccionFields.idCampoDocumentoAccion() + " = '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleOrdenesCompra() + "." + e_TblDetalleOrdenesCompraFields.idDetalleOrdenCompra() + "'";
                    VI = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                    if (!string.IsNullOrEmpty(VI))
                        return "0;Artículo ya fue recibido";
                }

                string cantidadarticuloOC = CargarEntidadesGS1.GS1128_DevolverCantidad(cl[0]);  // trae la cantidad original de la OC.
                string fv = CargarEntidadesGS1.GS1128_DevolverFechaVencimiento(cl[0]);
                string lote = CargarEntidadesGS1.GS1128_DevolveLote(cl[0]);
                if (!string.IsNullOrEmpty(fv))
                {
                    ano = fv.Substring(2, 2);
                    mes = fv.Substring(5, 2);
                    dia = fv.Substring(8, 2);
                    fechaven = ano + mes + dia;
                }

                codigoGS1 = n_WMS.CrearCodigoGS1(idArticulo[0], cl[1], fechaven, lote, idUsuario);  // reconstruimos el código GS1.


               // si devolvio un valor del idDetalleOrdenCompra, y no hay registro en la tabla TRA significa que el artículo no ha sido
               // recibido y por tanto no hace falta crear un registro en el detalle de OC.
                if (string.IsNullOrEmpty(ValorIdCampo) && (string.IsNullOrEmpty(VI)))
                {
                    // con este query obtenemos el  idDetalleOrdenCompra de registro original.
                    SQL = "";
                    SQL = "SELECT " + e_TblDetalleOrdenesCompraFields.idDetalleOrdenCompra() +
                          "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleOrdenesCompra() +
                          "  WHERE " + e_TblDetalleOrdenesCompraFields.idArticulo() + " = '" + idArticulo +
                          "'         AND " + e_TblDetalleOrdenesCompraFields.CantidadxRecibir() + " = '" + cantidadarticuloOC +
                          "'         AND " + e_TblDetalleOrdenesCompraFields.idMaestroOrdenCompra() + " = " + cl[2];
                    //string VIdCampo = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                    bool inserta = true;  // InsertaDetalleOC(VIdCampo + ";" + cl[1], idUsuario);  // se inserta registro de detalle de OC, según la nueva cantidad.
                    if (inserta)
                        return codigoGS1 + ";0";
                    else
                        return "0;No se pudo completar el proceso, vuelva a intentarlo...";
                }
                else
                    return codigoGS1 + ";0";
            }
            catch (Exception ex)
            {
                return "0;" + ex.Message;
            }
        }

        public static bool InsertaDetalleOC(string CodLeido, string idUsuario)
        {
           // este metodo se usa para insertar un registro en la tabla "TRACEID.dbo.OPEINGDetalleOrdenCompra", a partir de un registro existente en la misma tabla.
           // se usa para cuando se recibe un artículo al granel.
            string[] Param = CodLeido.Split(';');
            try
            {
                string query = "insert into " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleOrdenesCompra();
                query += " (" + e_TblDetalleOrdenesCompraFields.idMaestroOrdenCompra() + ",";
                query += e_TblDetalleOrdenesCompraFields.Nombre() + ",";
                query += e_TblDetalleOrdenesCompraFields.idArticulo() + ",";
                query += e_TblDetalleOrdenesCompraFields.CantidadxRecibir() + ",";
                query += e_TblDetalleOrdenesCompraFields.Comentario() + ",";
                query += e_TblDetalleOrdenesCompraFields.idUsuario() + ",";
                query += e_TblDetalleOrdenesCompraFields.Procesado() + ",";
                query += e_TblDetalleOrdenesCompraFields.idCompania() + ",";
                query += e_TblDetalleOrdenesCompraFields.RecibidoOK();
                query += ")";
                query += " select " + e_TblDetalleOrdenesCompraFields.idMaestroOrdenCompra() + ",";
                query += "        " + e_TblDetalleOrdenesCompraFields.Nombre() + ",";
                query += "        " + e_TblDetalleOrdenesCompraFields.idArticulo() + ",";
                query += "        " + Param[1] + ","; ;
                query += "        " + e_TblDetalleOrdenesCompraFields.Comentario() + ",";
                query += "        " + e_TblDetalleOrdenesCompraFields.idUsuario() + ",";
                query += "        " + e_TblDetalleOrdenesCompraFields.Procesado() + ",";
                query += "        " + e_TblDetalleOrdenesCompraFields.idCompania() + ",";
                query += "        " + e_TblDetalleOrdenesCompraFields.RecibidoOK();
                query += "   from " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleOrdenesCompra();
                query += "   where " + e_TblDetalleOrdenesCompraFields.idDetalleOrdenCompra() + " = " + Param[0];
                da_ConsultaDummy.PushData(query, idUsuario);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string ValidaAdminPass(string AdminPass, string idUsuario, string AdminUs)
        {
           // este metodo verifica que el password que se ingresó en el form de control de calidad, cuando se da una respuesta negativa, sea de un administrador.
            try
            {
                string SQL;
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);  // obtengo la compañia actual.

                // obtengo el Idrol del usuario adminsis general del sistema, para la empresa actual, para mas adelante comparar el Idrol de el con el del usuario que 
                // corresponde el password.
                //SQL = "SELECT " + e_TblUsuarios.IdRol() +
                //      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblUsuarios() +
                //      "  WHERE " + e_TblUsuarios.Usuario() + " = 'adminsis'" +
                //      "         AND " + e_TblUsuarios.IdCompania() + " = '" + idCompania + "'";
                string IdrolAdmin = "1";  // da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                // obtengo el usuario al que corresponde el password.
                var MD5 = System.Security.Cryptography.MD5.Create();
                string AdminPassEncriptado = clHash.GetMd5Hash(MD5, AdminPass);   // encripta el password ingresado, para poder compararlo despues.
                SQL = "";
                SQL = "SELECT " + e_TblUsuarios.Esta_Bloqueado() + "," + e_TblUsuarios.Usuario() +
                      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblUsuarios() +
                      "  WHERE " + e_TblUsuarios.Contrasenna() + " = '" + AdminPassEncriptado + "'" +
                      "        AND " + e_TblUsuarios.Usuario() + " = '" + AdminUs + "'" +
                      "        AND " + e_TblUsuarios.IdRol() + " = " + IdrolAdmin +
                      "        AND " + e_TblUsuarios.IdCompania() + " = '" + idCompania + "'";
                DataSet DS = da_ConsultaDummy.GetDataSet(SQL, idUsuario);

                switch (DS.Tables[0].Rows.Count)
                {
                    case 0:
                        return "Password no corresponde a ningún Administrador;False";

                    case 1:
                        if (bool.Parse(DS.Tables[0].Rows[0]["ESTA_BLOQUEADO"].ToString()))
                            return "Usuario bloqueado;False";
                        else
                            return "Verificado: " + DS.Tables[0].Rows[0]["Usuario"].ToString().ToUpper() + ";True";

                    default:
                        return "Password registrado " + DS.Tables[0].Rows.Count.ToString() + " veces;False";
                }
            }
            catch (Exception ex)
            {
                return "No se pudo completar el proceso, vuelva a intentarlo..." + ex.Message + ";False";
            }
                
        }

        public static string validaTemperaturaArticulo(string Idarticulo, string Temperatura, string Idusuario)
        {
           // valida la temperatura del articulo a recibir en el formulario de calidad.
            try
            {
                string SQL = "";
                SQL = "select " + e_TblMaestroArticulosFields.TemperaturaMaxima() + "," + e_TblMaestroArticulosFields.TemperaturaMinima() +
                      "  from " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroArticulos() +
                      "  where " + e_TblMaestroArticulosFields.idArticulo() + " = " + Idarticulo;
                DataSet DB = da_ConsultaDummy.GetDataSet(SQL, idUsuario);  // este query devuelve el rango de temperaturas permitidas para este artículo.

                Single Tmax;
                Single Tmin;
                Single Temp;
                Single.TryParse(DB.Tables[0].Rows[0]["TemperaturaMaxima"].ToString(), out Tmax);
                Single.TryParse(DB.Tables[0].Rows[0]["TemperaturaMinima"].ToString(), out Tmin);
                Single.TryParse(Temperatura, out Temp);  // transforma a número los valores string.

                if (Temp >= Tmin && Temp <= Tmax)  // evalua si el artículo está dentro del rango de temperatura aceptable.
                    return "Temperatura del producto dentro de los parámetros aceptables.;True;" + Temperatura;
                else
                    return "Temperatura del producto fuera de los parámetros aceptables.;False;" + Temperatura;
            }

            catch
            {
                return "error al evaluar temperatura... intente de nuevo.;False";
            }
        }

        public static int EvaluaPregunta(string IdPregunta)
        {
            try
            {
                string SQL = "";
                SQL = "SELECT evalua FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + ".ADMPreguntasCalidad where idpregunta = " + IdPregunta;
                string evalua = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                return int.Parse(evalua);

            }

            catch 
            {
                return 0;
            }

        }
        #endregion

        public static string ObtenerArticulosSegunUbicacion(string CodLeido, string idUsuario)
        {
            string Respuesta = "Ningún artículo encontrado en la ubicación... o no es válida.";
            string SQL = "";
            DataSet DS = new DataSet();
            int pos = 0;
            try
            {
                SQL = "SELECT *" +
                       "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaDisponibilidadArticulos() +
                       "  WHERE " + e_VistaArticulosSegunUbicacion.ETIQUETA() + " = '" + CodLeido.Substring(2, CodLeido.Length - 2) + "'";

                      
                DS = da_ConsultaDummy.GetDataSet(SQL, idUsuario);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    Respuesta = "";
                    Respuesta += " ARTÍCULOS ENCONTRADOS EN LA UBICACIÓN\n";

                    foreach (DataRow Fila in DS.Tables[0].Rows)
                    {
                        if (pos == 0)
                            Respuesta += "Ubicación........:" + Fila["idubicacion"].ToString() + " - " + Fila["bodega"].ToString() + " - " + Fila["zona"].ToString() + "\n";

                        Respuesta += "Artículo..:" + Fila["idInterno"].ToString() + "-" + Fila["NombreArticulo"].ToString() + "\n";
                        Respuesta += "Fecha Vencimiento:" + Fila["FV"].ToString().Substring(0, 10).Trim() + "\n";
                        Respuesta += "Lote.............:" + Fila["lote"].ToString() + "\n";
                        if (Fila["Granel"].ToString() == "True")
                            Respuesta += "Cantidad.........:" + Math.Round((Single.Parse(Fila["SUMAcantidadestado"].ToString()) / 1000), 6) + " " + Fila["Unidad_Medida"].ToString() + " \n\n";
                        else
                            Respuesta += "Cantidad.........:" + Fila["SUMAcantidadestado"].ToString() + " " + Fila["Unidad_Medida"].ToString() + " \n\n";

                        pos++;
                    }

                }
                else
                {
                    SQL = "SELECT idubicacion, Bodega, Zona" +
                          "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaCodigosUbicacion() +
                          "  WHERE REPLACE((REPLACE(etiqueta,'(','')),')','') = '" + CodLeido + "'";

                    DS = da_ConsultaDummy.GetDataSet(SQL, idUsuario);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        Respuesta = "";
                        Respuesta += " ARTÍCULOS NO ENCONTRADOS EN LA UBICACIÓN\n";

                        foreach (DataRow Fila in DS.Tables[0].Rows)
                        {
                            Respuesta += "Ubicación........:" + Fila["idubicacion"].ToString() + " - " + Fila["Bodega"].ToString() + " - " + Fila["Zona"].ToString() + "\n";
                        }
                    }

                }

                DS.Clear();
                
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return Respuesta;

        }

        public static string ValidaAccesosHH(string idUsuario)
        {
            string Opciones = "";
            DataSet DS = new DataSet();
            string SQL = "";
           
            SQL = "SELECT ddlformhh FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + "Vista_AccesosporUsuario WHERE idusuario = " + idUsuario + " AND WEB = 0";
            DS = n_ConsultaDummy.GetDataSet(SQL, idUsuario);

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow filas in DS.Tables[0].Rows)
                {
                    Opciones += filas["ddlformhh"].ToString() + ";";
                }
            }
            else
                Opciones = "0";

            return Opciones;
        }

        #region Eventos

        public static string EventoObtenerOpcionesXCedula(string CodLeido, string idUsuario)
        {
            string respuesta = "";
            string SQL = "";

            try
            {
                string idEmpresaEvento = ConfigurationManager.AppSettings["idEmpresaEvento"];

                SQL = "SELECT * FROM Vista_EventoOpcionesXCedula WHERE Identificacion = '" + CodLeido.Trim() + "' AND idEmpresa = '" + idEmpresaEvento + "'";
                DataSet DS = new DataSet();
                DS = n_ConsultaDummy.GetDataSet(SQL, idUsuario);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow DSRow in DS.Tables[0].Rows)
                    {
                        string IdEventoOpcion = DSRow["IdEventoOpcion"].ToString();

                        if (string.IsNullOrEmpty(respuesta))
                        {
                            respuesta = IdEventoOpcion;
                        }
                        else
                        {
                            respuesta = respuesta + ";" + IdEventoOpcion;
                        }

                    }
                }
            }
            catch
            {
                return respuesta;
            }

            return respuesta;
        }

        public static string EventoObtenerRegaliaXCedula(string CodLeido, string idUsuario)
        {
            string respuesta = "";
            string SQL = "";

            try
            {
                string idEmpresaEvento = ConfigurationManager.AppSettings["idEmpresaEvento"];

                SQL = "SELECT * FROM Vista_EventoRegaliaXCedula WHERE Identificacion = '" + CodLeido.Trim() + "' AND idEmpresa = '" + idEmpresaEvento + "'";
                DataSet DS = new DataSet();
                DS = n_ConsultaDummy.GetDataSet(SQL, idUsuario);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow DSRow in DS.Tables[0].Rows)
                    {
                        string Descripcion = DSRow["Descripcion"].ToString();
                        string RegaliaCanjeada = DSRow["RegaliaCangeada"].ToString();

                        respuesta = Descripcion + ";" + RegaliaCanjeada;
                    }
                }
            }
            catch
            {
                return respuesta;
            }

            return respuesta;
        }

        public static bool EventoEnviarOpcionesDemo(string CodLeido, string idUsuario)
        {
            bool respuesta = false;

            try
            {
                string idEmpresaEvento = ConfigurationManager.AppSettings["idEmpresaEvento"];

                string SQL = "";
                string IdPerfilUsuario = "";
                //string Identificacion = "";

                string[] Opciones = CodLeido.Split(';');

                for (int i = 0; i < Opciones.Count(); i++)
                {
                    if (i == 0)
                    {
                        string Identificacion = Opciones[0].ToString().Trim();
                        SQL = "SELECT IdPerfilUsuario FROM PROD_DIV_ACCESO.dbo.tblPerfilUsuario WHERE Identificacion = '" + Identificacion + "'";
                        IdPerfilUsuario = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                    }
                    else
                    {
                        string opcion = Opciones[i].ToString().Trim();
                        SQL = "SELECT IdDatosDemoEvento FROM PROD_DIV_ACCESO.dbo.tblDatosDemoEvento WHERE IdPerfilUsuario = '" + IdPerfilUsuario + "' AND IdEventoOpcion = '" + opcion + "' AND idEmpresa = '" + idEmpresaEvento + "'";
                        string IdDatosDemoEvento = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                        if (string.IsNullOrEmpty(IdDatosDemoEvento))
                        {
                            SQL = "INSERT INTO PROD_DIV_ACCESO.dbo.tblDatosDemoEvento (IdPerfilUsuario, IdEventoOpcion, idEmpresa) VALUES ('" + IdPerfilUsuario + "', '" + opcion + "', '" + idEmpresaEvento + "')";
                            n_ConsultaDummy.PushData(SQL, idUsuario);
                        }
                    }

                }

                if (!string.IsNullOrEmpty(IdPerfilUsuario))
                {
                    SQL = "UPDATE PROD_DIV_ACCESO.dbo.tblEventoRegaliaXUsuario SET RegaliaCangeada = '1' WHERE IdPerfilUsuario = '" + IdPerfilUsuario + "'";
                    respuesta = n_ConsultaDummy.PushData(SQL, idUsuario);
                }
            }
            catch
            {
                return false;
            }

            return respuesta;
        }

        public static bool ProcesaCantidadGrandeArticulo(string idArticulo, string cantidad, string idUsuario, string Idmetodoaccion)
        {
            string SQL = "";
            string Equivalencia = "0";
            string Regtran = "";
            string Saldo = "0";
            string TablaOrigen = "TRACEID.dbo.TRAIngresoSalidaArticulos";
            string IdCampo = "idRegistro";
            string Idestado = "0";
            string IdestadoIng = "0";
            string fv = "";
            string lt = "";
            string idubic = "";
            DataSet Idreg = new DataSet();
            decimal Canti = 0.00M;
            int blanco = 0;
            DateTime Feve;


           // Obtengo la equivalencia del artículo a procesar.
            SQL = "SELECT " + e_TblMaestroArticulosFields.Equivalencia() +
                  "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroArticulos() +
                  "  WHERE " + e_TblMaestroArticulosFields.idArticulo() + " = " + idArticulo;
            Equivalencia = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

           // obtengo los registros de la tabla TRA cuya cantidad sea mas grande que la equivalencia y que no hayan sido procesados.
            SQL = "";
            SQL = "SELECT Idestado FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblEstado() +
                  "  WHERE Nombre = 'Ingresado'";
            IdestadoIng = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);  // obtengo el Idestado para transacciones de ingreso.
            SQL = "";
            SQL = "SELECT " + e_TblTransaccionFields.idRegistro() + "," +
                               e_TblTransaccionFields.Cantidad() + "," +
                               e_TblTransaccionFields.Lote() + "," +
                               e_TblTransaccionFields.FechaVencimiento() + "," +
                               e_TblTransaccionFields.idUbicacion() +
                  "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + " AS Tra " +
                  "  WHERE " + e_TblTransaccionFields.idArticulo() + " = " + idArticulo +
                  "        AND " + e_TblTransaccionFields.Cantidad() + " > " + Equivalencia +
                  "        AND (SELECT Abreviatura FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaMaestroUbicaciones() + " WHERE txtidubicacion = Tra." + e_TblTransaccionFields.idUbicacion() + ") = '" + ConfigurationManager.AppSettings["Zona_Almacenamiento"].ToString() + "'" +
                  "        AND CAST(" + e_TblTransaccionFields.idRegistro() + " AS NVARCHAR(10)) NOT IN (SELECT " + e_TblTransaccionFields.NumDocumentoAccion() +
                                                                                                        "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                                                                                        "  WHERE " + e_TblTransaccionFields.idMetodoAccion() + " = " + Idmetodoaccion +
                                                                                                        "        AND " + e_TblTransaccionFields.idArticulo() + " = " + idArticulo + ")";
            Idreg = n_ConsultaDummy.GetDataSet(SQL, idUsuario);

            if (Idreg.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow filas in Idreg.Tables[0].Rows)  // por cada registro encontrado se procesa en TRA.
                {
                    Regtran = Idreg.Tables[0].Rows[0][0].ToString(); // IdRegistro
                    Canti = decimal.Parse(Idreg.Tables[0].Rows[0][1].ToString()); // cantidad
                    Saldo = (Canti - decimal.Parse(cantidad)).ToString();
                    fv = Idreg.Tables[0].Rows[0][3].ToString(); // fecha vencimiento
                    Feve = DateTime.Parse(fv);
                    fv = Feve.Year.ToString() + "-" + Feve.Month.ToString() + '-' + Feve.Day.ToString();
                    lt = Idreg.Tables[0].Rows[0][2].ToString();  // lote
                    idubic = Idreg.Tables[0].Rows[0][4].ToString(); ;  //idubicacion

                    // esta primer transacción saca la totalidad de la cantidad del artículo, del registro de la tabla TRA, que tiene cantidad mayor que la equivalencia.
                    SQL = "";
                    SQL = "SELECT Idestado FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblEstado() +
                          "  WHERE Nombre = 'Traslado'";
                    Idestado = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);  // estado de la transacción de salida.
                    string respuesta = TransaccionMD(idArticulo, Canti.ToString(), TablaOrigen, IdCampo, Regtran, "TABLA.INSERT", Idestado, "0", idUsuario, "8", fv, lt, idubic);

                   // esta segunda transacción inserta la cantidad del artículo solicitado en la tabla TRA.
                    SQL = "";
                    SQL = "SELECT " + e_TblTransaccionFields.idRegistro() +
                          "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                          "  WHERE " + e_TblTransaccionFields.NumDocumentoAccion() + " = '" + Regtran + "'" +
                          "        AND " + e_TblTransaccionFields.idTablaCampoDocumentoAccion() + " = '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + "'" +
                          "        AND " + e_TblTransaccionFields.idCampoDocumentoAccion() + " = '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + "." + e_TblTransaccionFields.idRegistro() + "'";
                    string NumDocAcc = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);  // obtengo el IdRegistro de la transaccion anterior.

                    respuesta = TransaccionMD(idArticulo, cantidad, TablaOrigen, IdCampo, NumDocAcc, "TABLA.INSERT", IdestadoIng, "1", idUsuario, "8", fv, lt, idubic);

                   // esta tercer transacción inserta el saldo del artículo solicitado en la tabla TRA.
                    SQL = "";
                    SQL = "SELECT " + e_TblTransaccionFields.idRegistro() +
                          "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                          "  WHERE " + e_TblTransaccionFields.NumDocumentoAccion() + " = '" + NumDocAcc + "'" +
                          "        AND " + e_TblTransaccionFields.idTablaCampoDocumentoAccion() + " = '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + "'" +
                          "        AND " + e_TblTransaccionFields.idCampoDocumentoAccion() + " = '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + "." + e_TblTransaccionFields.idRegistro() + "'";
                    NumDocAcc = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);  // Numdocumentoaccion de la última transacción.
                    respuesta = TransaccionMD(idArticulo, Saldo, TablaOrigen, IdCampo, NumDocAcc, "TABLA.INSERT", IdestadoIng, "1", idUsuario, "8", fv, lt, idubic);
                }

            }

            return true;
        }

        #endregion Eventos
        //}

        public static string CargarHoras(string datos)
        {
           // inserta tareas en la tabla MDTareasAsignadasUsuarios
            string[] data = datos.Split(';');
            string idUsuario = data[0];
            string FechaDespacho = data[1];
            string Idmaestrosolicitud = data[2];
            string idBodega = data[3];
            string idMetodoAccion = "28";
            string mensaje = "";
            StringBuilder SQLBuilder = new StringBuilder();

            //SQLBuilder.AppendLine("DECLARE @idusuario nvarchar(10),");
            //SQLBuilder.AppendLine("        @idMaestrosolicitud nvarchar(10)");

            e_UsuarioProductivo UPR = new e_UsuarioProductivo();
            e_UsuarioProductivo UP = new e_UsuarioProductivo();
            e_TareaUsuario TareaAsignada = new e_TareaUsuario();
            List<e_TareaUsuario> TareasAsignadas = new List<e_TareaUsuario>();  // lista de TareaAsignada.

            List<string> usuarios = new List<string>();
            List<e_TareaUsuario> TareasUsuariosAsignada = new List<e_TareaUsuario>();
            e_TareaUsuario TareaUsuario = new e_TareaUsuario();
            List<e_TareaUsuario> TareasUsuarios = new List<e_TareaUsuario>();  // lista de TareaUsuario.
            e_UsuarioProductivo UsuarioProductivo = new e_UsuarioProductivo();
            List<e_UsuarioProductivo> UsuariosProductivosOcupados = new List<e_UsuarioProductivo>();  // lista de UsuarioProductivo.
            TareasUsuarios = n_WMS.ObtenerTareasUsuarios(idUsuario, idMetodoAccion);  // tareas que aparecen en la vista Vista_TareasUsuario.
            

            foreach (e_TareaUsuario TU in TareasUsuarios)
            {
                foreach (e_UsuarioProductivo _UP in TU.UsuariosProductivos)
                {
                    bool Ocupado = false;
                    bool EstaLaTareaAsignada = false;
                    foreach (e_UsuarioProductivo up in UsuariosProductivosOcupados)  // UsuariosProductivosOcupados es una lista en blanco.
                    {
                        if (up != null)
                        {
                            if (up.TareasAsignadas != null)
                                foreach (e_TareaUsuario ta in up.TareasAsignadas)
                                {
                                    if (TU.idTarea == ta.idTarea)
                                    {
                                        EstaLaTareaAsignada = true;
                                    }
                                }
                        }
                    }
                    if (EstaLaTareaAsignada == false)
                    {
                        UP = new e_UsuarioProductivo();
                        UP = UsuariosProductivosOcupados.FindLast(x => x.idUsuario == _UP.idUsuario);
                        if (UP != null)
                        {
                            Ocupado = UP.Ocupado;
                        }
                        else
                        {
                            UP = _UP;
                        }
                        /// Si las horas Disponibles es mayor al Porcentaje de Desecho la tarea debe hacerse aunque se paguen horas extra.
                        if (/*Esta*/Ocupado == false && UP.idUsuario == _UP.idUsuario)
                        {
                            decimal HorasDisponibles = UP.HHDisponiblesParaTarea;   // 2
                            double PorcentajeDesperdicio = 0.05;                    // 5%
                            string idUsuarioP = UP.idUsuario;                        // 0
                            string idRegistro = TU.idRegistro;                      // 3114
                            string idTarea = TU.idTarea;                            // Alisto-3114
                            decimal TiempoNecesario = TU.TiempoEstimado;            // 2.56

                            if (double.Parse(HorasDisponibles.ToString()) > double.Parse(HorasDisponibles.ToString()) * PorcentajeDesperdicio)
                            {
                                decimal HorasATrabajar = TiempoNecesario;
                                decimal HorasExtraNecesarias = HorasATrabajar - HorasDisponibles;
                                if (HorasExtraNecesarias < 0 /*HorasExtraNecesarias es Negativo*/)
                                {
                                    HorasExtraNecesarias = 0;
                                }
                                else
                                {
                                    Ocupado = true;
                                }

                                HorasDisponibles = HorasDisponibles + HorasExtraNecesarias - HorasATrabajar;
                                /*Fin de los calculos */
                                UsuarioProductivo = new e_UsuarioProductivo();
                                TareaAsignada = new e_TareaUsuario();
                                TareasAsignadas = new List<e_TareaUsuario>();
                                UsuarioProductivo.idUsuario = idUsuarioP;
                                UsuarioProductivo.Ocupado = Ocupado;
                                UsuarioProductivo.HHDisponiblesParaTarea = HorasDisponibles;
                                UsuarioProductivo.HorasExtraNecesarias = HorasExtraNecesarias;
                                TareaAsignada = TU;

                                //TraceID.(2016). Operaciones/Consultas/wf_Consultas.En Trace ID Codigos documentados(6).Costa Rica:Grupo Diverscan.       

                                TareasAsignadas.Add(TareaAsignada);
                                UsuarioProductivo.TareasAsignadas = TareasAsignadas;
                                UsuariosProductivosOcupados.Add(UsuarioProductivo);
                            }
                            else
                            {
                                Ocupado = true;
                            }
                        }
                    }
                    else
                    {
                        //break; // saltar siguiente tarea;
                    }
                }
            }
            string up_idusuario = String.Empty;
            List<e_TareaUsuario> TareasParaUsuarios = new List<e_TareaUsuario>();
            e_TareaUsuario TareaParaUsuario = new e_TareaUsuario();
            e_UsuarioProductivo usr_Prod = new e_UsuarioProductivo();
            List<e_UsuarioProductivo> usr_Prods = new List<e_UsuarioProductivo>();
            foreach (e_UsuarioProductivo UsrProdOcupado in UsuariosProductivosOcupados)
            {
                foreach (e_TareaUsuario TareaUsrProdOcupado in UsrProdOcupado.TareasAsignadas)
                {
                    if (up_idusuario != UsrProdOcupado.idUsuario) // Cambio de usuarios
                    {
                        TareasParaUsuarios.Add(TareaParaUsuario);
                        TareasParaUsuarios = new List<e_TareaUsuario>();
                        TareaParaUsuario = new e_TareaUsuario();
                        up_idusuario = UsrProdOcupado.idUsuario;
                        TareaParaUsuario = TareaUsrProdOcupado;
                        TareasParaUsuarios.Add(TareaParaUsuario);
                        usr_Prod = UsrProdOcupado;
                        usr_Prod.TareasAsignadas = TareasParaUsuarios;
                        usr_Prods.Add(usr_Prod);
                    }
                    else
                    {
                        usr_Prod = UsrProdOcupado;
                        foreach (e_UsuarioProductivo _usrprod_ in usr_Prods)
                        {
                            if (_usrprod_.idUsuario == usr_Prod.idUsuario)
                            {
                                _usrprod_.HorasExtraNecesarias = usr_Prod.HorasExtraNecesarias;
                                _usrprod_.HHDisponiblesParaTarea = usr_Prod.HHDisponiblesParaTarea;
                            }
                        }
                        TareaParaUsuario = TareaUsrProdOcupado;
                        TareasParaUsuarios.Add(TareaParaUsuario);
                    }
                }
            }
            foreach (e_UsuarioProductivo usrProd in usr_Prods)
            {
                DataSet DS = new DataSet();
                string SQL = "SELECT " + e_VistaTareasUsuario.IdTarea() +
                             "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TBLTareasAsignadasUsuarios() + 
                             "  ORDER BY " + e_VistaTareasUsuario.IdTarea();
                DS = n_ConsultaDummy.GetDataSet(SQL, usrProd.idUsuario);
                List<e_Tarea> TaReAs = new List<e_Tarea>();
                e_Tarea TaReA = new e_Tarea();
                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    TaReA = new e_Tarea();
                    TaReA.idTarea = DR["idtarea"].ToString();
                    TaReAs.Add(TaReA);
                }

                foreach (e_TareaUsuario tareaUsr in usrProd.TareasAsignadas)
                {
                    //var _id_tarea = String.Empty;
                    //if (TaReAs.Count > 0)
                    //{
                    //    try
                    //    {
                    //        var _id_tarea_ = TaReAs.Find(x => x.idTarea == tareaUsr.idTarea);
                    //        if (_id_tarea_ != null) _id_tarea = _id_tarea_.idTarea;
                    //    }
                    //    catch (Exception) { }
                    //}
    
                    //if (tareaUsr.idTarea != _id_tarea)
                    //{
                        //SQLBuilder.AppendLine();
                        //SQLBuilder.AppendLine("SELECT @idusuario = " + e_VistaTareasUsuario.IDUSUARIO() + ",");
                        //SQLBuilder.AppendLine("       @idMaestrosolicitud = " + e_VistaTareasUsuario.num_solicitud());
                        //SQLBuilder.AppendLine("  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaTareasUsuario());
                        //SQLBuilder.AppendLine("  WHERE " + e_VistaTareasUsuario.IdTarea() + " = '" + tareaUsr.idTarea + "'");
                        //SQLBuilder.AppendLine();
                        //SQLBuilder.AppendLine("IF ISNULL(@idusuario,'0') = '0'");
                        //SQLBuilder.AppendLine("  SET @idusuario = 'x'");
                        //SQLBuilder.AppendLine();
                        //SQLBuilder.AppendLine("IF ISNULL(@idMaestrosolicitud,'0') = '0'");
                        //SQLBuilder.AppendLine("  SET @idMaestrosolicitud = 'x'");
                        //SQLBuilder.AppendLine();
                        //DS.Clear();
                        //DS = n_ConsultaDummy.GetDataSet(SQL, idUsuario);  // obtener el número de solicitud(IdMaestroSolicitud) y el usuario asignado a esta solicitud.

                        //string solicitud = DS.Tables[0].Rows[0][1].ToString();
                        //string _idUsuario  = "";
                        //if (idUsuario == null || idUsuario == String.Empty)
                        //{
                        //    _idUsuario = DS.Tables[0].Rows[0][0].ToString();
                        //}
                        //else 
                        //{
                        //    _idUsuario = idUsuario;
                        //}

                        string _idMetodoAccion = tareaUsr.idMetodoAccion;
                        string _HorasExtra_ = usrProd.HorasExtraNecesarias.ToString().Replace(',', '.');
                        string _HorasDisponibles_ = usrProd.HHDisponiblesParaTarea.ToString().Replace(',', '.');
                        string _idTarea_ = tareaUsr.idTarea;
                        string _TiempoEstimadoHora_ = tareaUsr.TiempoEstimado.ToString().Replace(',', '.');

                        SQLBuilder.Clear();
                        SQLBuilder.AppendLine("INSERT INTO " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TBLTareasAsignadasUsuarios());
                        SQLBuilder.AppendLine("(" + e_TBLTareasAsignadasUsuarios.idMetodoAccion() + ",");
                        SQLBuilder.AppendLine(e_TBLTareasAsignadasUsuarios.idUsuario() + ",");
                        SQLBuilder.AppendLine(e_TBLTareasAsignadasUsuarios.HorasExtra() + ",");
                        SQLBuilder.AppendLine(e_TBLTareasAsignadasUsuarios.HorasDisponibles() + ",");
                        SQLBuilder.AppendLine(e_TBLTareasAsignadasUsuarios.idTarea() + ",");
                        SQLBuilder.AppendLine(e_TBLTareasAsignadasUsuarios.TiempoEstimadoHora() + ",");
                        SQLBuilder.AppendLine(e_TBLTareasAsignadasUsuarios.Num_Solicitud() + ",");
                        SQLBuilder.AppendLine(e_TBLTareasAsignadasUsuarios.Fecha_Despacho() + ",");
                        SQLBuilder.AppendLine(e_TBLTareasAsignadasUsuarios.Alistado() + ",");
                        SQLBuilder.AppendLine(e_TBLTareasAsignadasUsuarios.Lote() + ",");
                        SQLBuilder.AppendLine(e_TBLTareasAsignadasUsuarios.Fechavencimiento() + ",");
                        SQLBuilder.AppendLine(e_TBLTareasAsignadasUsuarios.idUbicacion() + ",");
                        SQLBuilder.AppendLine(e_TBLTareasAsignadasUsuarios.Cantidad() + ",");
                        SQLBuilder.AppendLine(e_TBLTareasAsignadasUsuarios.IdArticulo() + ",");
                        SQLBuilder.AppendLine(e_TBLTareasAsignadasUsuarios.idRegistro() + ",");
                        SQLBuilder.AppendLine(e_TBLTareasAsignadasUsuarios.Secuencia() + ",");
                        SQLBuilder.AppendLine(e_TBLTareasAsignadasUsuarios.idBodega() + ")");
                        SQLBuilder.AppendLine(" SELECT " + e_VistaTareaUsuarioFields.idMetodoAccion() + ",");
                        SQLBuilder.AppendLine(" " + e_VistaTareaUsuarioFields.IDUSUARIO() + ",");
                        SQLBuilder.AppendLine(" " + _HorasExtra_ + ",");
                        SQLBuilder.AppendLine(" " + _HorasDisponibles_ + ",");
                        SQLBuilder.AppendLine(" " + e_VistaTareaUsuarioFields.IdTarea() + ",");
                        SQLBuilder.AppendLine(" " +_TiempoEstimadoHora_ + ",");
                        SQLBuilder.AppendLine(" " + e_VistaTareaUsuarioFields.num_solicitud() + ",");
                        SQLBuilder.AppendLine(" '" + FechaDespacho.Replace("-","") + "',");
                        SQLBuilder.AppendLine(" 0,");
                        SQLBuilder.AppendLine(" " + e_VistaTareaUsuarioFields.Lote() + ",");
                        SQLBuilder.AppendLine(" " + e_VistaTareaUsuarioFields.FechaVencimiento() + ",");
                        SQLBuilder.AppendLine(" " + e_VistaTareaUsuarioFields.idUbicacion() + ",");
                        SQLBuilder.AppendLine(" " + e_VistaTareaUsuarioFields.Cantidad() + ",");
                        SQLBuilder.AppendLine(" " + e_VistaTareaUsuarioFields.idArticulo() + ",");
                        SQLBuilder.AppendLine(" " + e_VistaTareasUsuario.idRegistro() + ",");
                        SQLBuilder.AppendLine(" " + e_VistaTareaUsuarioFields.Secuencia() + ",");
                        SQLBuilder.AppendLine(" " + e_VistaTareaUsuarioFields.idBodega());
                        SQLBuilder.AppendLine(" FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaTareasUsuario());
                        SQLBuilder.AppendLine(" WHERE " + e_VistaTareaUsuarioFields.num_solicitud() + " = " + Idmaestrosolicitud);
                        SQLBuilder.AppendLine("   AND " + e_VistaTareaUsuarioFields.IDUSUARIO() + " = " + idUsuario);
                        SQLBuilder.AppendLine("   AND " + e_VistaTareaUsuarioFields.idBodega() + " = " + idBodega);
                        SQLBuilder.AppendLine();
                        break;
                    //}
                }

                SQLBuilder.AppendLine("SELECT 'TAREAS GENERADAS EXITOSAMENTE' AS 'RESULTADO'");
                mensaje = EjecutaSQLHH(SQLBuilder.ToString(), idUsuario);
                break;
            }

            return mensaje;
        }

        public static string validaDiasminimosvencimientoArticulo(string Idarticulo, string FV, string Idusuario)
        {
            // valida la mínima vida útil del articulo a recibir en el formulario de calidad.
            try
            {
                string SQL = "";
                SQL = "SELECT " + e_TblMaestroArticulosFields.DiasMinimosVencimiento() +
                      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroArticulos() +
                      "  WHERE " + e_TblMaestroArticulosFields.idArticulo() + " = " + Idarticulo;
                string DMV = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);  // este query devuelve los días minimos de vencimiento para este artículo.

                DateTime Hoy = DateTime.Parse(da_ConsultaDummy.GetUniqueValue("SELECT CAST(CONVERT(nvarchar(10),GETDATE(),112) AS DATE)", idUsuario));  // obtengo la fecha de hoy del servidor.
                DateTime FechaVenc = DateTime.Parse(FV); // comvierto la fecha de vencimiento.

                TimeSpan ts = FechaVenc - Hoy; // obtengo la diferencia en dias, horas y minutos.

                int dias = ts.Days + 1 ;  // obtengo los días, se le agrega 1 para que cuente desde el día de hoy.


                if (dias >= int.Parse(DMV) || int.Parse(DMV) == 0)  // si los días calculados son mayores o iguales a los día mínimos de vencimiento del artículo o
                    // los día mínimos de vencimiento son 00  el proceso puede continuar.
                    return "Vencimiento del producto dentro de los parámetros aceptables.;True;" + DMV + " Días-FV: " + FechaVenc.ToShortDateString();
                else
                {
                    DateTime FVReal = Hoy.AddDays(int.Parse(DMV));
                    return "Vencimiento del producto fuera de los parámetros aceptables.;False;" + DMV + " Días-FV: " + FVReal.ToShortDateString();
                }
            }

            catch
            {
                return "error al evaluar días mínimos de vencimiento... intente de nuevo.;False;0";
            }
        }

        public static string EjecutaSQLHH(string CodLeido, string idUsuario)
        {
           string mensaje = "Exitoso";
           try
            {
                string Exito = n_ConsultaDummy.GetUniqueValue(CodLeido, idUsuario);
                return mensaje + Exito;
            }

            catch (Exception ex)
            {
                return ex.Message ;
            }
        }

        public static string DetalleOrdenCompra(string CodLeido, string idUsuario)
        {
            StringBuilder  Respuesta = new StringBuilder();
            //Respuesta.AppendLine ("");
            string Columnas = "ArticuloID--- ----------  DESCRIPCION  ----------  ---Cantidad"; //idProveedor  Proveedor \n";
            string SQL = "";
            int CuentaOC = 0;
            Single SumaOC = 0.00F;
            string Artic = "";

            try
            {
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);
                SQL = "SELECT * FROM " + 
                              e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaDetalleOrdenCompraCEDI() + 
                              " WHERE " + e_vistaDetalleOrdenCompraCEDI.OCTraceid() + " = " + CodLeido;
                DataSet DS = n_ConsultaDummy.GetDataSet(SQL, idUsuario);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    Respuesta.AppendLine(DS.Tables[0].Rows[0]["Compania"].ToString() + "               * LISTADO ORDEN DE COMPRA *               Fecha: " + DateTime.Now.ToString());
                    Respuesta.AppendLine();
                    Respuesta.AppendLine("  Orden de Compra: " + DS.Tables[0].Rows[0]["OC_ERP"].ToString() + "-" + DS.Tables[0].Rows[0]["OC_Traceid"].ToString());
                    Respuesta.AppendLine("  Proveedor......: " + DS.Tables[0].Rows[0]["Proveedor"].ToString());
                    Respuesta.AppendLine("  Fecha Recepción: " + DS.Tables[0].Rows[0]["Fecha_programada_recepcion"].ToString());
                    Respuesta.AppendLine();
                    Respuesta.AppendLine(Columnas);
                    foreach (DataRow DR in DS.Tables[0].Rows )
                    {
                        Artic = DR["idArticuloERP"].ToString() + "-" + DR["idArticulo"].ToString();
                        Respuesta.Append(Artic + new string(' ', (13-Artic.Length)));
                        if (DR["Nombre"].ToString().Trim().Length > 38)
                          Respuesta.Append(" " + DR["Nombre"].ToString().Trim().Substring(0,38)); 
                        else
                          Respuesta.Append(" " + DR["Nombre"].ToString().Trim() + new string(' ', 38-DR["Nombre"].ToString().Trim().Length));

                        Respuesta.Append(String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", DR["CantidadxRecibir"]).PadLeft(10));  //.ToString()
                      
                        Respuesta.AppendLine();
                        CuentaOC++;
                        SumaOC = SumaOC + Single.Parse(DR["CantidadxRecibir"].ToString());
                    }

                    Respuesta.AppendLine("----------------------------------------------------------------------------------------------------------");
                    Respuesta.AppendLine("   Cuenta: " + CuentaOC.ToString() + new string(' ', 33) + " Suma: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", SumaOC));

                }
                else
                   Respuesta.AppendLine("OC sin detalle o no existe...");

            }

            catch (Exception ex)
            {
                Respuesta.AppendLine(" Problemas con el proceso..." + ex.Message);
            }

            return Respuesta.ToString();
        }

        public static string ArmaDatsetAsociarSSCC(string CodLeido, string idUsuario)
        {
            string result = "NO exitoso-";
            string SQL = "";
            try
            {
                string[] trama = CodLeido.Split(';');
                string SSCC = trama[0];
                string ubic = trama[1];
                string zona = ubic.Substring(6, 3);
                string idCompania = ObtenerCompaniaXUsuario(idUsuario);


                n_ProcesarSolicitud AsociarSSCC = new n_ProcesarSolicitud();
                result = AsociarSSCC.AsociarSSCCTransito(CargarEntidadesGS1.GS1128_DevolverSSCCGenerado(SSCC), idCompania, int.Parse(idUsuario), ubic, ConfigurationManager.AppSettings["Zona_Transito"].ToString());

            }

            catch (Exception ex)
            {
                result += ex.Message;
            }

            return result;

        }

        public static string Eliminaregistrossinprocesar(int tabla, string documento, string idUsuario)
        {
            string resultado = "NO exitoso-";
            string SQL = "";
            try
            {
                switch (tabla)
                {
                    case 1:
                        SQL = "DELETE FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleOrdenesCompra() +
                              "  WHERE " + e_TblDetalleOrdenesCompraFields.idMaestroOrdenCompra() + " = " + documento +
                              "        AND " + e_TblDetalleOrdenesCompraFields.Procesado() + " = 0";
                        n_ConsultaDummy.PushData(SQL, idUsuario);
                        resultado = "exitoso";
                        break;

                    case 2:
                        SQL = "DELETE FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleSolicitud() +
                              "  WHERE " + e_TblDetalleSolicitudFields.idMaestroSolicitud() + " = " + documento +
                              "        AND " + e_TblDetalleSolicitudFields.Procesado() + " = 0";
                        n_ConsultaDummy.PushData(SQL, idUsuario);
                        resultado = "exitoso";
                        break;

                }
            }

            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }

        public static int Diasminimosvencimientoresturantes(string idarticulo, string idusuario)
        {
            string SQL = "";
            int dmvr = 1;
            try
            {
                SQL = "SELECT " + e_TblMaestroArticulosFields.DiasMinimosVencimientoRestaurantes() +
                      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroArticulos() +
                      "  WHERE " + e_TblMaestroArticulosFields.idArticulo() + " = " + idarticulo;
                string dias = n_ConsultaDummy.GetUniqueValue(SQL, idusuario);

                if (!string.IsNullOrEmpty(dias))
                    dmvr = (int.Parse(dias) + 1 ) * -1;

            }

            catch (Exception)
            {
                dmvr = 2;
            }

            return dmvr;
        }

        public static string CierraSolicitud(string CodLeido, string idUsuario)
        {
            StringBuilder SQL = new StringBuilder();
            DataSet Mensaje = new DataSet();

            try
            {

                SQL.AppendLine(" DECLARE @Tareas INT, ");
                SQL.AppendLine(" @Tareascompletadas INT ");
                SQL.AppendLine();

                // Cuento todas las tareas generadas para esta solicitud.
                SQL.AppendLine(" SELECT @Tareas = COUNT(" + e_TBLTareasAsignadasUsuarios.idRegistroTareaAsignada() + ")");
                SQL.AppendLine("  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TBLTareasAsignadasUsuarios());
                SQL.AppendLine("  WHERE " + e_TBLTareasAsignadasUsuarios.Num_Solicitud() + " = " + CodLeido);
                SQL.AppendLine();

                // Cuento todas las tareas generadas para esta solicitud que hayan sido completadas.
                SQL.AppendLine(" SELECT @Tareascompletadas = COUNT(" + e_TBLTareasAsignadasUsuarios.idRegistroTareaAsignada() + ")");
                SQL.AppendLine("  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TBLTareasAsignadasUsuarios());
                SQL.AppendLine("  WHERE " + e_TBLTareasAsignadasUsuarios.Num_Solicitud() + " = " + CodLeido);
                SQL.AppendLine("        AND " + e_TBLTareasAsignadasUsuarios.Alistado() + " = 1");
                SQL.AppendLine();

                SQL.AppendLine(" IF @Tareas = @Tareascompletadas"); // si son iguales cierro la solicitud y todas las tareas.
                SQL.AppendLine(" BEGIN");
                SQL.AppendLine("  UPDATE " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroSolicitud());
                SQL.AppendLine("    SET " + e_TblMaestroSolicitudField.Procesada() + " = 1," + e_TblMaestroSolicitudField.FechaProcesado() + " = GETDATE()");
                SQL.AppendLine("    WHERE " + e_TblMaestroSolicitudField.idMaestroSolicitud() + " = " + CodLeido);


                SQL.AppendLine("  UPDATE " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TBLTareasAsignadasUsuarios());
                SQL.AppendLine("    SET " + e_TBLTareasAsignadasUsuarios.Alistado() + " = 1");
                SQL.AppendLine("    WHERE " + e_TBLTareasAsignadasUsuarios.Num_Solicitud() + " = " + CodLeido);
                SQL.AppendLine();
                SQL.AppendLine("  SELECT 'CIERRE DE SOLICITUD !! EXITOSO !!' AS 'Resultado'");
                SQL.AppendLine(" END");
                SQL.AppendLine(" ELSE");
                SQL.AppendLine("  SELECT 'FALTA COMPLETAR TAREAS, NO SE PUEDE CERRAR LA SOLICITUD' AS 'Resultado'");

                Mensaje = n_ConsultaDummy.GetDataSet(SQL.ToString(), idUsuario);
            }

            catch(Exception ex)
            {
                return ex.Message;
            }

             return Mensaje.Tables[0].Rows[0][0].ToString();
        }

        public static string ObtenerAbreviaturaBodega(string Etiqueta, string idUsuario)
        {
            string trama = "0;0";
            string SQL = "";
            string[] etiqcompa = Etiqueta.Split(';');

            try
            {
                SQL = "SELECT " + e_VistaCodigosUbicacionFields.idUbicacion() + "," +
                                  e_VistaCodigosUbicacionFields.idZona() + "," +
                                  e_VistaCodigosUbicacionFields.idBodega () +
                      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaCodigosUbicacion() +
                      "  WHERE " + e_VistaCodigosUbicacionFields.idCompania() + " = '" + etiqcompa[1] + "'" +
                      "        AND REPLACE((REPLACE(" + e_VistaCodigosUbicacionFields.ETIQUETA() + ",'(','')),')','') = '" + etiqcompa[0] + "'";
                DataSet DB = da_ConsultaDummy.GetDataSet(SQL, idUsuario);   // se obtiene el IdUbicacion y la zona a partir de la etiqueta pistoleada de la ubicación.

                if (DB.Tables[0].Rows.Count > 0)
                {
                    string idzona = DB.Tables[0].Rows[0][1].ToString();
                    string idUbi = DB.Tables[0].Rows[0][0].ToString();
                    SQL = "SELECT " + e_TblZonasFields.Abreviatura() +
                          "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblZonas() +
                          "  WHERE " + e_TblZonasFields.idZona() + " = " + idzona;
                    string abrev = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                    trama = abrev + ";" + DB.Tables[0].Rows[0][2].ToString();
                }
                else
                    trama = "0;NO encontrado";
            }
            catch (Exception ex) 
            {
                trama = "0;" + ex.Message;
            }
              
            return trama;
        }

        public static string InsertaPreDetalleSolicitud(string CodLeido, string idUsuario)
        {
            string[] splito = CodLeido.Split(';');

            n_Insertapredetallesolicitud InsertapredetallesolicitudN = new n_Insertapredetallesolicitud();
            e_Insertapredetallesolicitud Insertapredetallesolicitud = new e_Insertapredetallesolicitud(Int64.Parse(splito[0]),
                                                                                                       Int64.Parse(splito[1]),
                                                                                                       Int64.Parse(splito[2]),
                                                                                                       int.Parse(splito[3]),
                                                                                                       splito[4]);

            string mensaje = InsertapredetallesolicitudN.InsertarPreDetalleSolicitud(Insertapredetallesolicitud);
            return mensaje;
        }

        public static string CierraSSCC(string CodLeido, string idUsuario)
        {
           StringBuilder SQL = new StringBuilder();
           string resultado = "Transacción No Exitosa...";
           string SSCCLeido = CargarEntidadesGS1.GS1128_DevolverSSCCGenerado(CodLeido);

           try
           {
               string valida = ValidaCierreSSCC(CodLeido, idUsuario);

               if (valida == "SSCC no está Cerrado")
               {
                   SQL.AppendLine("UPDATE " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblConsecutivosSSCC());
                   SQL.AppendLine("  SET " + e_TblConsecutivosSSCCFields.Procesado() + " = 1," + e_TblConsecutivosSSCCFields.FechaProcesado() + " = GETDATE()");
                   SQL.AppendLine("  WHERE " + e_TblConsecutivosSSCCFields.SSCCGenerado() + " = '" + SSCCLeido + "'");

                   if (da_ConsultaDummy.PushData(SQL.ToString(), idUsuario))
                       resultado = "SSCC Cerrado con exito";
               }
               else
                   resultado = valida;
           }

            catch (Exception ex)
           {
               return resultado + "-" + ex.Message;
           }

           return resultado;
        }

        public static string ValidaCierreSSCC(string CodLeido, string idUsuario)
        {
            string SQL = "";
            //string resultado = "SSCC no está Cerrado";
            string resultado = "SSCC no válido";

            try
            {
                string SSCCLeido = CargarEntidadesGS1.GS1128_DevolverSSCCGenerado(CodLeido);
                if (string.IsNullOrEmpty(SSCCLeido))
                {
                    resultado = "SSCC no válido";
                    return resultado;
                }

                SQL = "EXEC SP_ValidaCierreSSCC '" + SSCCLeido + "'";

                resultado = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
            }

            catch (Exception ex)
            {
                return resultado + "-" + ex.Message;
            }

            return resultado;
        }

        public static string CierraAlisto(string CodLeido, string idUsuario)
        {
            string respuesta = "No Exitoso-";
            string SQL = "";
            bool CierraAlisto = false;

            try
            {
                string[] IdmaestroUsuario = CodLeido.Split(';');
                if (IdmaestroUsuario.Count() != 3)
                    respuesta = "Falta maestro de solicitud/usuario asignado/Bodega";
                else
                {
                    //SQL = "UPDATE " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TBLTareasAsignadasUsuarios() +
                    //      "  SET "  + e_TBLTareasAsignadasUsuarios.Alistado() + " = 1," +
                    //      "      "  + e_TBLTareasAsignadasUsuarios.Suspendido() + " = 0 " +
                    //      "  WHERE " + e_TBLTareasAsignadasUsuarios.Num_Solicitud() + " = " + IdmaestroUsuario[0] +
                    //      "        AND " + e_TBLTareasAsignadasUsuarios.idUsuario() + " = " + IdmaestroUsuario[1];

                    SQL = "UPDATE " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + ".TareasUsuario " + 
                          "   SET Alistado = 1, Suspendido = 0 " +
                          "   FROM OPESALDetalleSolicitud     AS S " + 
                          "     INNER JOIN TareasUsuario      AS T ON (S.idLineaDetalleSolicitud = T.IdLineaDetalleSolicitud) " +
                          "     INNER JOIN ADMMaestroArticulo AS M ON (S.idArticulo = M.idArticulo) " +
                          "   WHERE S.idMaestroSolicitud = " + IdmaestroUsuario[0] + 
                          "         AND T.IdUsuario = " + IdmaestroUsuario[1] +
                          "         AND M.idBodega = " + IdmaestroUsuario[2];

                    CierraAlisto = n_ConsultaDummy.PushData(SQL, idUsuario);

                    if (CierraAlisto)
                        respuesta = "Alisto cerrado Correctamente";
                    else
                        respuesta = "No exitoso... cierre del Alisto";

                }
            }
            catch (Exception ex)
            {
                respuesta = "Problemas con el cierre del alisto..." + ex.Message;
            }

            return respuesta;
        }

        public static string SaltarSiguienteTarea(string CodLeido, string idUsuario)
        {
            string respuesta = "No Exitoso... ";
            string SQL = "";
            bool PasaSiguienteTarea = false;

            try
            {
               string[] Parametros = CodLeido.Split(';');
               if (Parametros.Count() != 6)
                   respuesta += "Faltan Parámetros de entrada";
               else
               {
                    //SQL = "UPDATE " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TBLTareasAsignadasUsuarios() +
                    //      "  SET " + e_TBLTareasAsignadasUsuarios.Alistado() + " = 1," +
                    //                 e_TBLTareasAsignadasUsuarios.Suspendido() + " = 1" +
                    //      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaTareasUsuario() + " AS b" +
                    //      "  WHERE b." + e_VistaTareaUsuarioFields.idArticulo() + " = " + Parametros[0].ToString() +
                    //      "        AND b." + e_VistaTareaUsuarioFields.Lote() + " = '" + Parametros[1].ToString() + "'" +
                    //      "        AND b." + e_VistaTareaUsuarioFields.FechaVencimiento() + " = '" + Parametros[2].ToString() + "'" +
                    //      "        AND b." + e_VistaTareaUsuarioFields.num_solicitud() + " = " + Parametros[3].ToString() +
                    //      "        AND b." + e_VistaTareaUsuarioFields.IDUSUARIO() + " = " + Parametros[4].ToString() +
                    //      "        AND " + e_TablasBaseDatos.TBLTareasAsignadasUsuarios() + "." + e_TBLTareasAsignadasUsuarios.Alistado() + " = 0" +
                    //      "        AND " + e_TablasBaseDatos.TBLTareasAsignadasUsuarios() + "." + e_TBLTareasAsignadasUsuarios.Suspendido() + " = 0" +
                    //      "        AND " + e_TablasBaseDatos.TBLTareasAsignadasUsuarios() + "." + e_TBLTareasAsignadasUsuarios.idTarea() + " = b." + e_VistaTareaUsuarioFields.IdTarea();

                    SQL = "UPDATE TEST_TRACEID_V2.dbo.TareasUsuario " +
                          "SET Alistado = 1, Suspendido = 1 " +
                          "WHERE IdTareasUsuario = " + Parametros[5].ToString();



                   PasaSiguienteTarea = n_ConsultaDummy.PushData(SQL, idUsuario);
                   if (!PasaSiguienteTarea)
                       respuesta += "Problemas con la siguiente tarea.";
                   else
                       respuesta = "Proceso Exitoso...";

               }
            }
            catch(Exception ex)
            {
                return respuesta + ex.Message;
            }

            return respuesta;
        }

        public static string MuestraInfoArticuloHH(string CodLeido, string idUsuario)
        {  
            string respuesta = "";
            n_ProcesarSolicitud InfoArticulo = new n_ProcesarSolicitud();

            try
            {
                int Cant = 0;
                n_MotorDecisiones.Metodos MD = new n_MotorDecisiones.Metodos();

                if (CodLeido.Substring(0, 2) != "01")   // en caso de leer un GTIN13-GTIN14 se le pone adelante el 01 para poder procesar la recepción-ubicación-alisto.
                {
                    if (CodLeido.Length == 13)
                        CodLeido = "010" + CodLeido;
                    else if (CodLeido.Length == 14)
                        CodLeido = "01" + CodLeido;
                }

                string idCompania = ObtenerCompaniaXUsuario(idUsuario);

                respuesta = InfoArticulo.DevuelveInfoArticulo(CodLeido, idCompania);
                respuesta += ";" + CargarEntidadesGS1.GS1128_DevolverFechaVencimiento(CodLeido) + ";" + CargarEntidadesGS1.GS1128_DevolveLote(CodLeido);

            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(CodLeido))
                    respuesta = "No Procesado-Código en blanco...;0";
                else
                    respuesta = "No Procesado-" + ex.Message + ";0";
            }

            return respuesta;
        }
    }
}
/*
    string idartic = "";
                string SQLTareaAsigna = "SELECT A." + e_TBLTareasAsignadasUsuarios.idMetodoAccion() + "," +
                                              " A." + e_TBLTareasAsignadasUsuarios.idUsuario() + "," +
                                              " A." + e_TBLTareasAsignadasUsuarios.idTarea() + "," +
                                              " A." + e_TBLTareasAsignadasUsuarios.TiempoEstimadoHora() +
                                        "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TBLTareasAsignadasUsuarios() + " AS A" +
                                        "    INNER JOIN " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + " AS B ON (A." + e_TBLTareasAsignadasUsuarios.idMetodoAccion() + " = B." + e_TblTransaccionFields.idMetodoAccion() +
                                        "                                                                                              AND SUBSTRING(A." + e_TBLTareasAsignadasUsuarios.idTarea() + ",8,10) = B." + e_TblTransaccionFields.idRegistro() + ")" +
                                        "    INNER JOIN " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroArticulos() + " AS C ON (B." + e_TblTransaccionFields.idArticulo() + " = C." + e_TblMaestroArticulosFields.idArticulo() + ")" +
                                        "  WHERE A." + e_TBLTareasAsignadasUsuarios.idUsuario() + " = " + identificadorUsuario.ToString() +
                                        "        AND A." + e_TBLTareasAsignadasUsuarios.Alistado() + " = 0" +
                                        "        AND A." + e_TBLTareasAsignadasUsuarios.Num_Solicitud() + " = " + idMaestrosolicitud +
                                        "  ORDER BY B." + e_TblTransaccionFields.idUbicacion() + ",C." + e_TblMaestroArticulosFields.Nombre() + ",B." + e_TblTransaccionFields.Lote() + ",B." + e_TblTransaccionFields.idArticulo() + ",B." + e_TblTransaccionFields.FechaVencimiento();

                DataSet tareaAsignada = new DataSet();
                DataSet AlistoUsuario = new DataSet();
                DataSet InfoUbicacion = new DataSet();
                tareaAsignada = n_ConsultaDummy.GetDataSet2(SQLTareaAsigna, identificadorUsuario.ToString());

                if (tareaAsignada.Tables[0].Rows.Count > 0)
                {
                    resp = "";
                    foreach (DataRow DR in tareaAsignada.Tables[0].Rows)
                    {
                        int idMetodAccion = Convert.ToInt32(DR["IdMetodoAccion"].ToString());
                        int Usuario = Convert.ToInt32(DR["IDUSUARIO"].ToString());
                        string idTarea = DR["IdTarea"].ToString();
                        string SQLTarea = "SELECT " + e_VistaTareasUsuario.idRegistro() + "," + 
                                                      e_VistaTareasUsuario.num_solicitud() + "," +
                                                      e_VistaTareasUsuario.IDUSUARIO() + "," +
                                                      e_VistaTareasUsuario.Lote() + "," +
                                                      e_VistaTareasUsuario.Fechavencimiento() + "," +
                                                      e_VistaTareasUsuario.IdArticulo() +
                                          "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaTareasUsuario() +
                                          "  WHERE " + e_VistaTareasUsuario.IdTarea() + " = '" + idTarea + "'";

                        DataSet tareas = n_ConsultaDummy.GetDataSet(SQLTarea, idUsuario);

                        if (tareas.Tables[0].Rows.Count <= 0)
                        {
                            resp += "-Tarea para este usuario no registrada.";
                            break;
                        }

                        idartic = tareas.Tables[0].Rows[0][5].ToString();
                        DateTime fv = Convert.ToDateTime(tareas.Tables[0].Rows[0][4].ToString());
                        string ano = fv.Year.ToString();
                        string mes = fv.Month.ToString();
                        string dia = fv.Day.ToString();
                        string FechaSQL = ano + "-" + mes + "-" + dia;
                        string SQLAlisto = "SELECT " + e_VistaMaestroSolicitudField.Nombre() + "," + 
                                                       e_VistaMaestroSolicitudField.Lote() + "," + 
                                                       e_VistaMaestroSolicitudField.idUbicacion() + 
                                                       ",SUM(" + e_VistaMaestroSolicitudField.Cantidad() + ") AS Cantidad," +
                                                       e_VistaMaestroSolicitudField.idArticulo() + "," +
                                                       e_VistaMaestroSolicitudField.FechaVencimiento() +
                                           "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaAlistosPendientes() +
                                           "  WHERE " + e_VistaMaestroSolicitudField.Num_Solicitud() + " = " + tareas.Tables[0].Rows[0][1].ToString() +
                                           "        AND " + e_VistaMaestroSolicitudField.idUsuario() + " = " + tareas.Tables[0].Rows[0][2].ToString() +
                                           "        AND " + e_VistaMaestroSolicitudField.FechaVencimiento()  + " = '" + FechaSQL + "'" +
                                           "        AND " + e_VistaMaestroSolicitudField.Lote() + " = '" + tareas.Tables[0].Rows[0][3].ToString() + "'" +
                                           "        AND " + e_VistaMaestroSolicitudField.idArticulo() + " = " + idartic +
                                           "  GROUP BY " + e_VistaMaestroSolicitudField.idUbicacion() + "," + e_VistaMaestroSolicitudField.Nombre() + "," + e_VistaMaestroSolicitudField.idArticulo() + "," + e_VistaMaestroSolicitudField.Lote() + "," + e_VistaMaestroSolicitudField.FechaVencimiento() +
                                           "  ORDER BY " + e_VistaMaestroSolicitudField.idUbicacion() + "," + e_VistaMaestroSolicitudField.Nombre() + "," + e_VistaMaestroSolicitudField.idArticulo() + "," + e_VistaMaestroSolicitudField.Lote() + "," + e_VistaMaestroSolicitudField.FechaVencimiento();
                         AlistoUsuario = n_ConsultaDummy.GetDataSet2(SQLAlisto, idUsuario);

                        if ((AlistoUsuario.Tables[0].Rows.Count > 0) && (idartic == AlistoUsuario.Tables[0].Rows[0][4].ToString()))
                        {
                            string Nombre = AlistoUsuario.Tables[0].Rows[0][0].ToString();
                            string Lote = AlistoUsuario.Tables[0].Rows[0][1].ToString();
                            Int64 idUbicacion = Convert.ToInt64(AlistoUsuario.Tables[0].Rows[0][2].ToString());
                            Single cantidad = Single.Parse(AlistoUsuario.Tables[0].Rows[0][3].ToString());
                            idartic = AlistoUsuario.Tables[0].Rows[0][4].ToString();
                            string SQLInfoUbicacion = "SELECT " + e_TblMaestroUbicacion.idZona() + "," + 
                                                                  e_TblMaestroUbicacion.estante() + "," + 
                                                                  e_TblMaestroUbicacion.nivel() + "," +
                                                                  e_TblMaestroUbicacion.columna() + "," + 
                                                                  e_TblMaestroUbicacion.pos() + 
                                                     "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroUbicaciones() +
                                                     "  WHERE " + e_TblMaestroUbicacion.idUbicacion() + " = " + idUbicacion;
                            InfoUbicacion = n_ConsultaDummy.GetDataSet2(SQLInfoUbicacion, idUsuario);

                            //(SELECT A." + e_VistaMaestroSolicitudField.idArticulo() +
                            //    "      FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaAlistosPendientes() + " AS A " +
                            //    "      WHERE A." + e_VistaMaestroSolicitudField.idRegistro() + " = " + tareas.Tables[0].Rows[0][0].ToString() + ")" +

                            string estante = InfoUbicacion.Tables[0].Rows[0][1].ToString();
                            string idZona = InfoUbicacion.Tables[0].Rows[0][0].ToString();
                            string nivel = InfoUbicacion.Tables[0].Rows[0][2].ToString();
                            string columna = InfoUbicacion.Tables[0].Rows[0][3].ToString();
                            string pos = InfoUbicacion.Tables[0].Rows[0][4].ToString();
                            string estantetitulo = "Estante: ";

                            string SQL = "SELECT replace((replace(" + e_VistaCodigosUbicacionFields.ETIQUETA() + ",'(','')),')','')" +
                                         "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaCodigosUbicacion() +
                                         "  WHERE " + e_VistaCodigosUbicacionFields.idCompania() + " = '" + idcompania + "'" +
                                         "        AND " + e_VistaCodigosUbicacionFields.idUbicacion() + " = " + idUbicacion;
                            string etiquetaubicacion = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);  // obtengo la etiqueta de ubicación a partir del id.

                           // cuenta los articulos pendientes de ubicar.
                            SQL = "";
                            SQL = "SELECT COUNT(" + e_TblTransaccionFields.idRegistro() + ")" +
                                  "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                  "  WHERE " + e_TblTransaccionFields.idMetodoAccion() + " = 28" +
                                  "        AND " + e_TblTransaccionFields.Cantidad() + " > 0 " +
                                  "        AND " + e_TblTransaccionFields.idEstado() + " = 13" +
                                  "        AND " + e_TblTransaccionFields.idArticulo() + " = " + idartic +
                                  "        AND " + e_TblTransaccionFields.Lote() + " = '" + Lote + "'" +
                                  "        AND " + e_TblTransaccionFields.FechaVencimiento() + " = '" + FechaSQL + "'" +
                                  "        AND " + e_TblTransaccionFields.idUbicacion() + " = " + idUbicacion +
                                  "        AND " + e_TblTransaccionFields.idRegistro() + " NOT IN (SELECT " + e_TblSSCCTRAFields.idRegistro() +
                                                                                                "  FROM  " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblSSCCTRA() + ")";
                            string pendientealisto = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                                if (idcompania == ConfigurationManager.AppSettings["Compania"].ToString())
                                estantetitulo = "Pasillo.: ";
                resp  = idTarea + ";" +                             //0
                        "Articulo: " + Nombre + ";" +               //1
                        Lote + ";" +                                //2
                        etiquetaubicacion + ";" +                   //3
                        "Cantidad: " + cantidad + ";" +             //4
                        estantetitulo + estante + ";" +             //5
                        "Nivel...: " + nivel + ";" +                //6
                        "Columna.: " + columna + ";" +              //7
                        "Posicion: " + pos + ";" +                  //8
                        idartic + ";" +                             //9
                        "IdZona: " + idZona + ";" +                 //10
                        AlistoUsuario.Tables[0].Rows[0][5].ToString() + ";" + //11  FechaVenc.
                        tareas.Tables[0].Rows[0][1].ToString() + ";" +        //12  IdMaestroSolicitud.
                        tareas.Tables[0].Rows[0][0].ToString() + ";" +        //13  Idregistro(TRA)
                        pendientealisto + "\n";                               //14  pendiente por alistar.
 * 
                   string SQL = "";
                   StringBuilder SQLBuilder = new StringBuilder();
                   string mensaje = String.Empty;
                   string LineaNueva = "\n" + "Articulo ya fue procesado en su solicitud.";
                   string LineaNuevaInsertada = String.Empty;
                   string idMaestroSolicitud = "";
                   string idDestino = "";
                   string Descripcion = "";
                   string Procesado = "1";
                   string UniInven = "";
                   string idArticulo = "";
                   string GTIN_LEIDO = "";
                   string NombreArticulo = "";
                   decimal Equivalencia = 0;
                   decimal cantidad = 0;
                   bool Registra = true;

                SQLBuilder.Clear();
                SQLBuilder.AppendLine("DECLARE @idArticulo BIGINT,");
                SQLBuilder.AppendLine("        @UniInven INT,");              
                SQLBuilder.AppendLine("        @idMaestroSolicitud BIGINT,"); 
                SQLBuilder.AppendLine("        @idDestino BIGINT,");                        
                SQLBuilder.AppendLine("        @GTIN_LEIDO NVARCHAR(15),");
                SQLBuilder.AppendLine("        @NombreArticulo NVARCHAR(20),");
                SQLBuilder.AppendLine("        @Equivalencia AS INT,");
                SQLBuilder.AppendLine("        @Tabla NVARCHAR(100),");
                SQLBuilder.AppendLine("        @Campo NVARCHAR(150),");
                SQLBuilder.AppendLine("        @Suma_Resta BIT,");
                SQLBuilder.AppendLine("        @Idestado BIGINT,");
                SQLBuilder.AppendLine("        @IdZonaAlmacen BIGINT,");
                SQLBuilder.AppendLine("        @IdZonaPicking BIGINT,");
                SQLBuilder.AppendLine("        @Disponible INTEGER,");
                SQLBuilder.AppendLine("        @IdZonaProceso BIGINT,");
                SQLBuilder.AppendLine("        @UltimoIdentityDetalleSolicitud BIGINT,");
	            SQLBuilder.AppendLine("        @UltimoIdentityTra BIGINT,");
                SQLBuilder.AppendLine("        @IdRegistro BIGINT,");
                SQLBuilder.AppendLine("        @FV  NVARCHAR(10),");
                SQLBuilder.AppendLine("        @IdMetodoAccion BIGINT = " + idMetodoAccion + ",");
                SQLBuilder.AppendLine("        @IdUbicacion BIGINT,");
                SQLBuilder.AppendLine("        @idUsuario INT = " + idusuario + ",");
                SQLBuilder.AppendLine("        @Lote  NVARCHAR(10)");
                SQLBuilder.AppendLine();
                SQLBuilder.AppendLine("SELECT @Tabla = (" + e_TblEstadoTransaccionalFields.BaseDatos() + " + '.' + " + e_TblEstadoTransaccionalFields.idTabla() + "),");
                SQLBuilder.AppendLine("       @Campo = (" + e_TblEstadoTransaccionalFields.BaseDatos() + " + '.' + " + e_TblEstadoTransaccionalFields.idTabla() + " + '.' + " + e_TblEstadoTransaccionalFields.IdCampo() + "),");
                SQLBuilder.AppendLine("       @Suma_Resta = " + e_TblEstadoTransaccionalFields.SumUno_RestaCero() + ",");
                SQLBuilder.AppendLine("       @Idestado = " + e_TblEstadoTransaccionalFields.IdEstado());
                SQLBuilder.AppendLine("  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblEstadoTransaccional());
                SQLBuilder.AppendLine("  WHERE " + e_TblEstadoTransaccionalFields.idMetodoAccion() + " = @IdMetodoAccion");
                SQLBuilder.AppendLine();
                SQLBuilder.AppendLine("SELECT @idArticulo = Idarticulo,");
                SQLBuilder.AppendLine("       @UniInven = UnidadesInventario,");
                SQLBuilder.AppendLine("       @idMaestroSolicitud = idMaestroSolicitud,");
                SQLBuilder.AppendLine("       @idDestino = idDestino,");
                SQLBuilder.AppendLine("       @GTIN_LEIDO = GTIN,");
                SQLBuilder.AppendLine("       @NombreArticulo = Nombre,");
                SQLBuilder.AppendLine("       @Equivalencia = Equivalencia");
                SQLBuilder.AppendLine("  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaPendientesDetalleAlisto());
                SQLBuilder.AppendLine("  WHERE idLineaDetalleSolicitud = " + idLineaDetalleSolicitud);
                SQLBuilder.AppendLine("        AND IdCompania = '" + idCompania + "'");  // esta vista devuelve los articulos solicitados que aún no tiene registro en la tabla TRA.
                SQLBuilder.AppendLine();
                SQLBuilder.AppendLine("SELECT @IdZonaAlmacen = Idzona FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblZonas() + " WHERE Abreviatura = '" + ConfigurationManager.AppSettings["Zona_Almacenamiento"].ToString() + "'");
                SQLBuilder.AppendLine("SELECT @IdZonaPicking = Idzona FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblZonas() + " WHERE Abreviatura = '" + ConfigurationManager.AppSettings["Zona_Picking"].ToString() + "'");
                SQLBuilder.AppendLine();
               
                //SQL = "SELECT " + e_VistaPendientesAlistoFields.Cantidad() +
                //      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.VistaPendientesDetalleAlisto() +
                //      "  WHERE idLineaDetalleSolicitud = " + idLineaDetalleSolicitud;
                // da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                List<string> Solicitudes = new List<string>();
                Solicitudes.Add(idLineaDetalleSolicitud);
               
               foreach (string Solicitud in Solicitudes)
                {
                  double CantidadLineas = double.Parse(cant);
                  for (int Linea = 0; Linea < CantidadLineas; Linea++)
                  {
                     SQLBuilder.AppendLine();
                     SQLBuilder.AppendLine("INSERT INTO " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleSolicitud());
                     SQLBuilder.AppendLine("   (" + e_TblDetalleSolicitudFields.Nombre() + ",");
                     SQLBuilder.AppendLine("    " + e_TblDetalleSolicitudFields.idMaestroSolicitud() + ",");
                     SQLBuilder.AppendLine("    " + e_TblDetalleSolicitudFields.idDestino() + ",");
                     SQLBuilder.AppendLine("    " + e_TblDetalleSolicitudFields.idArticulo() + ",");
                     SQLBuilder.AppendLine("    " + e_TblDetalleSolicitudFields.Cantidad() + ",");
                     SQLBuilder.AppendLine("    " + e_TblDetalleSolicitudFields.Descripcion() + ",");
                     SQLBuilder.AppendLine("    " + e_TblDetalleSolicitudFields.IdCompania() + ",");
                     SQLBuilder.AppendLine("    " + e_TblDetalleSolicitudFields.idUsuario() + ",");
                     SQLBuilder.AppendLine("    " + e_TblDetalleSolicitudFields.Procesado() + ")");
                     SQLBuilder.AppendLine();
                     SQLBuilder.AppendLine("   VALUES (");
                     SQLBuilder.AppendLine("           'Linea(" + (Linea + 1).ToString() + ") Artículo: ' + @NombreArticulo + ' GTIN: ' + @GTIN_LEIDO,");
                     SQLBuilder.AppendLine("           @idMaestroSolicitud,");
                     SQLBuilder.AppendLine("           @idDestino,");
                     SQLBuilder.AppendLine("           @idArticulo,");
                     SQLBuilder.AppendLine("           @Equivalencia,");
                     SQLBuilder.AppendLine("           '',");
                     SQLBuilder.AppendLine("           '" + idCompania + "',");
                     SQLBuilder.AppendLine("           " + idusuario + ",");
                     SQLBuilder.AppendLine("           0)");
                     SQLBuilder.AppendLine();
                     SQLBuilder.AppendLine("SET @UltimoIdentityDetalleSolicitud = SCOPE_IDENTITY()");
                     SQLBuilder.AppendLine();
                  
                     SQLBuilder.AppendLine("SELECT TOP 1 @IdRegistro = A." + e_TblTransaccionFields.idRegistro() + ",");
                     SQLBuilder.AppendLine("             @Lote = A." + e_TblTransaccionFields.Lote() + ",");
                     SQLBuilder.AppendLine("             @FV = A." + e_TblTransaccionFields.FechaVencimiento() + ",");
                     SQLBuilder.AppendLine("             @IdUbicacion = A." + e_TblTransaccionFields.idUbicacion());
                     SQLBuilder.AppendLine("  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + " AS A");
                     SQLBuilder.AppendLine("    INNER JOIN " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + ".TRAResumen AS B ON (A." + e_TblTransaccionFields.idRegistro() + " = B.idRegistro)");   // + e_TblTransaccionFields.idArticulo()
                     SQLBuilder.AppendLine("  WHERE A." + e_TblTransaccionFields.idArticulo() + " = @idArticulo");
                     SQLBuilder.AppendLine("        AND A." + e_TblTransaccionFields.idEstado() + " = 12");
                     SQLBuilder.AppendLine("        AND A." + e_TblTransaccionFields.Cantidad() + " = @Equivalencia");
                     //SQLBuilder.AppendLine("        AND A." + e_TblTransaccionFields.idMetodoAccion() + " = 59");
                     SQLBuilder.AppendLine("        AND A." + e_TblTransaccionFields.idUbicacion() + " IN (SELECT " + e_TblMaestroUbicacion.idUbicacion());
                     SQLBuilder.AppendLine("                                 FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroUbicaciones());
                     SQLBuilder.AppendLine("                                 WHERE " + e_TblMaestroUbicacion.idZona() + " IN (@IdZonaAlmacen,@IdZonaPicking))");
                     SQLBuilder.AppendLine("  ORDER BY A." + e_TblTransaccionFields.FechaVencimiento() + ",A." + e_TblTransaccionFields.idUbicacion() + " DESC,A." + e_TblTransaccionFields.Fecharegistro());
                     SQLBuilder.AppendLine();
                     SQLBuilder.AppendLine("INSERT INTO " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion());
                     SQLBuilder.AppendLine("   (" + e_TblTransaccionFields.SumUno_RestaCero() + ",");
                     SQLBuilder.AppendLine(      e_TblTransaccionFields.idArticulo() + ",");
				     SQLBuilder.AppendLine(      e_TblTransaccionFields.FechaVencimiento() + ",");
				     SQLBuilder.AppendLine(      e_TblTransaccionFields.Lote() + ",");
					 SQLBuilder.AppendLine(      e_TblTransaccionFields.idUsuario() + ",");
					 SQLBuilder.AppendLine(      e_TblTransaccionFields.idMetodoAccion() + ",");
					 SQLBuilder.AppendLine(      e_TblTransaccionFields.idTablaCampoDocumentoAccion() + ",");
					 SQLBuilder.AppendLine(      e_TblTransaccionFields.idCampoDocumentoAccion() + ",");
					 SQLBuilder.AppendLine(      e_TblTransaccionFields.NumDocumentoAccion() + ",");
					 SQLBuilder.AppendLine(      e_TblTransaccionFields.idUbicacion() + ",");
					 SQLBuilder.AppendLine(      e_TblTransaccionFields.Cantidad() + ",");
					 SQLBuilder.AppendLine(      e_TblTransaccionFields.Procesado() + ",");
					 SQLBuilder.AppendLine(      e_TblTransaccionFields.idEstado() + ")");
                     SQLBuilder.AppendLine("	VALUES(@Suma_Resta,");
                     SQLBuilder.AppendLine("        @idArticulo,");
                     SQLBuilder.AppendLine("        @FV,");
                     SQLBuilder.AppendLine("        @Lote,");
                     SQLBuilder.AppendLine("        @idUsuario,");
                     SQLBuilder.AppendLine("        @IdMetodoAccion,");
                     SQLBuilder.AppendLine("        @Tabla,");
                     SQLBuilder.AppendLine("        @Campo,");
                     SQLBuilder.AppendLine("        (CAST(@UltimoIdentityDetalleSolicitud AS NVARCHAR(10)) + '-' + CAST(@IdRegistro AS NVARCHAR(10))),");
                     SQLBuilder.AppendLine("        @IdUbicacion,");
                     SQLBuilder.AppendLine("        @Equivalencia,");
                     SQLBuilder.AppendLine("        0,");
                     SQLBuilder.AppendLine("        @Idestado)");
                    //SQLBuilder.AppendLine("  SELECT @Suma_Resta,");
                    //SQLBuilder.AppendLine("         A.idArticulo,");
                    //SQLBuilder.AppendLine("         A.FechaVencimiento,");
                    //SQLBuilder.AppendLine("         A.Lote,");
                    //SQLBuilder.AppendLine("         C.idUsuario,");
                    //SQLBuilder.AppendLine("         @IdMetodoAccion,");
                    //SQLBuilder.AppendLine("         @Tabla,");
                    //SQLBuilder.AppendLine("         @Campo,");
                    //SQLBuilder.AppendLine("         (CAST(C.idLineaDetalleSolicitud AS VARCHAR(10)) + '-' + CAST(A.idRegistro AS VARCHAR(10))),");
                    //SQLBuilder.AppendLine("         A.idUbicacion,");
                    //SQLBuilder.AppendLine("         A.Cantidad,");
                    //SQLBuilder.AppendLine("         0,");
                    //SQLBuilder.AppendLine("         @Idestado");
                    //SQLBuilder.AppendLine("     FROM traceid..TRAIngresoSalidaArticulos      AS A");
                    //SQLBuilder.AppendLine("       INNER JOIN traceid..TRAResumen             AS B ON (A.idRegistro = B.idRegistro)");
                    //SQLBuilder.AppendLine("       INNER JOIN TRACEID..OPESALDetalleSolicitud AS C ON (A.idArticulo = C.idArticulo)");
                    //SQLBuilder.AppendLine("     WHERE A.idarticulo = @idArticulo");
                    //SQLBuilder.AppendLine("            AND A.idEstado = 12");
                    //SQLBuilder.AppendLine("            AND A.Cantidad = @Equivalencia");
                    //SQLBuilder.AppendLine("            AND C.Procesado = 0");
                    //SQLBuilder.AppendLine("            AND A.idUbicacion IN (SELECT idUbicacion");
                    //SQLBuilder.AppendLine("         			               FROM traceid..ADMMaestroUbicacion");
                    //SQLBuilder.AppendLine("         			               WHERE idZona in (@IdZonaAlmacen,@IdZonaPicking))");
                    //SQLBuilder.AppendLine("     ORDER BY A.FechaVencimiento,A.idubicacion desc,A.Fecharegistro");
                  }
                }
                    
                if (LineaNuevaInsertada != String.Empty) LineaNueva = "\n" + LineaNuevaInsertada;

                 SQLBuilder.AppendLine();
                 SQLBuilder.AppendLine("UPDATE " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." +  e_TablasBaseDatos.TblDetalleSolicitud() + " SET Procesado = 1 WHERE " + e_TblDetalleSolicitudFields.idLineaDetalleSolicitud() + " = " + idLineaDetalleSolicitud + " AND " + e_TblDetalleSolicitudFields.IdCompania() + " = '" + idCompania + "'");
                 da_ConsultaDummy.PushData(SQLBuilder.ToString(), idUsuario);
                 respuesta = "Solicitud Exitoso";
 * 
               //OBTIENE EL idConsecutivoSSCC DEL SSCC LEIDO
                SQL = "SELECT " + e_TblConsecutivosSSCCFields.idConsecutivoSSCC() +
                      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblConsecutivosSSCC() +
                      "  WHERE " + e_TblConsecutivosSSCCFields.idCompania() + " = '" + idCompania + "'" +
                      "        AND " + e_TblConsecutivosSSCCFields.SSCCGenerado() + " = '" + SSCCLeido + "'" +
                      "        AND " + e_TblConsecutivosSSCCFields.SSCCGenerado() + " IS NOT NULL ";
                idConsecutivoSSCC = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                if (!string.IsNullOrEmpty(idConsecutivoSSCC.Trim()))
                {
                   //OBTIENE LOS ARTICULOS QUE TIENEN ESTADO DE PEDIDO (IDMETODOACCION = 28) Y QUE NO HAYAN SIDO PROCESADOS.
                    SQL = "SELECT " + e_TblTransaccionFields.idRegistro() + "," +
                                    " SUBSTRING(" + e_TblTransaccionFields.NumDocumentoAccion() + ",0,CHARINDEX('-'," + e_TblTransaccionFields.NumDocumentoAccion() + ")) AS NumDocumentoAccion" + "," +
                                    e_TblTransaccionFields.idUbicacion() +
                          "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                          "  WHERE " + e_TblTransaccionFields.idMetodoAccion() + " = 28" +
                          "        AND " + e_TblTransaccionFields.Cantidad() + " > 0 " +
                          "        AND " + e_TblTransaccionFields.idEstado() + " = 13" +
                          "        AND " + e_TblTransaccionFields.idArticulo() + " = " + idArticulo +
                          "        AND " + e_TblTransaccionFields.Lote() + " = '" + Lote + "'" +
                          "        AND " + e_TblTransaccionFields.FechaVencimiento() + " = '" + FV + "'" +
                          "        AND " + e_TblTransaccionFields.idUbicacion() + " = " + idUbicacion  +
                          //"        AND " + e_TblTransaccionFields.idRegistro() + " = " + regtra +
                          "        AND " + e_TblTransaccionFields.idRegistro() + " NOT IN (SELECT " + e_TblSSCCTRAFields.idRegistro() + 
                                                                                        "    FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblSSCCTRA() + ")";
                    DataSet DS = new DataSet();
                    DS = n_ConsultaDummy.GetDataSet(SQL, idUsuario);

                    // se insertan los registros de la tabla TRA (Idregistro) en RELSSCCTRA.
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                       

                            if (CantidadLineas > GTIN.VLs[0].Cantidad)  // en caso de que se procese un GTIN14 o un AIS 37, se vuelve a ejecutar el query anterior
                            {                                           // para obtener todos los registros de la solicitud para ese artículo y aplicar los que cantidadlineas permita.
                                DS.Clear();
                                SQL = "";
                                SQL = "SELECT " + e_TblTransaccionFields.idRegistro() + "," +
                                      " SUBSTRING(" + e_TblTransaccionFields.NumDocumentoAccion() + ",0,CHARINDEX('-'," + e_TblTransaccionFields.NumDocumentoAccion() + ")) AS NumDocumentoAccion" + "," +
                                      e_TblTransaccionFields.idUbicacion() +
                                      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                      "  WHERE " + e_TblTransaccionFields.idMetodoAccion() + " = 28" +
                                      "        AND " + e_TblTransaccionFields.Cantidad() + " > 0 " +
                                      "        AND " + e_TblTransaccionFields.idEstado() + " = 13" +
                                      "        AND " + e_TblTransaccionFields.idArticulo() + " = " + idArticulo +
                                      "        AND " + e_TblTransaccionFields.Lote() + " = '" + Lote + "'" +
                                      "        AND " + e_TblTransaccionFields.FechaVencimiento() + " = '" + FV + "'" +
                                      "        AND " + e_TblTransaccionFields.idUbicacion() + " = " + idUbicacion +
                                      "        AND " + e_TblTransaccionFields.idRegistro() + " NOT IN (SELECT " + e_TblSSCCTRAFields.idRegistro() +
                                                                                                   "    FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblSSCCTRA() + ")";
                                DS = n_ConsultaDummy.GetDataSet(SQL, idUsuario);
                            }

                            int Linea = 1;
                            foreach (DataRow DSRow in DS.Tables[0].Rows)
                            {
                                string idRegistro = DSRow["idRegistro"].ToString();
                                string numdocaccion = DSRow["NumDocumentoAccion"].ToString();
                                //string ubicavalida = DSRow["idubicacion"].ToString();

                               // se obtiene el idmaestrosolicitud para registrarlo en (TblSSCCTRA).
                                SQL = "";
                                SQL = "SELECT " + e_TblDetalleSolicitudFields.idMaestroSolicitud() + 
                                      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleSolicitud() +
                                      "  WHERE CAST(" +  e_TblDetalleSolicitudFields.idLineaDetalleSolicitud() + " AS nvarchar(10)) = '" + numdocaccion + "'" +
                                      "        AND "  + e_TblDetalleSolicitudFields.IdCompania() + " = '" + idCompania + "'";
                                string Maestrosolicitud = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                                if (Maestrosolicitud == "")
                                {
                                    Respuesta += ";Inconsistencia en proceso, alisto no tiene registro de solicitud...";
                                    return Respuesta;
                                }

                               

                                if (idUbicacion == ubicavalida)
                                {
                                // inserta los datos en tabla relacional del SSCC y transaccional general (TblSSCCTRA).
                                SQL = "INSERT INTO " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblSSCCTRA() +
                                        "        (" + e_TblSSCCTRAFields.idRegistro() + "," + e_TblSSCCTRAFields.idConsecutivoSSCC() + "," + e_TblSSCCTRAFields.idMaestroSolicitud() + ")" +
                                        " VALUES ('" + idRegistro + "','" + idConsecutivoSSCC + "'," + Maestrosolicitud + ")";
                                n_ConsultaDummy.PushData(SQL, idUsuario);
                                }
                                else
                                {
                                    Respuesta += ";Ubicación no corresponde...";
                                    break;
                                }

                               // cuenta los articulos pendientes de ubicar.
                                SQL = "";  
                                SQL = "SELECT COUNT(" + e_TblTransaccionFields.idRegistro() + ")" +
                                      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                                      "  WHERE " + e_TblTransaccionFields.idMetodoAccion() + " = 28" +
                                      "        AND " + e_TblTransaccionFields.Cantidad() + " > 0 " +
                                      "        AND " + e_TblTransaccionFields.idEstado() + " = 13" +
                                      "        AND " + e_TblTransaccionFields.idArticulo() + " = " + idArticulo +
                                      "        AND " + e_TblTransaccionFields.Lote() + " = '" + Lote + "'" +
                                      "        AND " + e_TblTransaccionFields.FechaVencimiento() + " = '" + FV + "'" +
                                      "        AND " + e_TblTransaccionFields.idRegistro() + " NOT IN (SELECT " + e_TblSSCCTRAFields.idRegistro() + 
                                                                                                    "  FROM  " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblSSCCTRA() + ")";
                                AlistoPendiente = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                               // cada vez que se asocia un artículo se le pone marca de alistado en la tabla de tareas de usuarios.
                                SQL = "";
                                SQL = "UPDATE " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TBLTareasAsignadasUsuarios() +
                                       "  SET " + e_TBLTareasAsignadasUsuarios.Alistado() + " = 1" +
                                       "  WHERE " + e_TBLTareasAsignadasUsuarios.idTarea() + " = 'ALISTO-" + idRegistro + "'";
                                n_ConsultaDummy.PushData(SQL, idUsuario);

                               // cada vez que se asocia un artículo se le pone marca de procesado en la tabla  DetalleSolicitud.
                                SQL = "";
                                SQL = "UPDATE " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleSolicitud() +
                                       "  SET " + e_TblDetalleSolicitudFields.Procesado() + " = 1" +
                                       "  WHERE " + e_TblDetalleSolicitudFields.idLineaDetalleSolicitud() + " = " + numdocaccion;
                                n_ConsultaDummy.PushData(SQL, idUsuario);

                                if (Linea >= CantidadLineas)
                                {
                                    Respuesta = "oK;" + AlistoPendiente + ";Procesado Exitosamente-" + NombreArticulo;
                                  break;
                                }
                               
                                Linea++;
                            }  //-foreach (DataRow DSRow in DS.Tables[0].Rows)
                        }  //-if (EsGTIN)   // es un GTIN valido
                        else
                        {
                            Respuesta += ";GTIN no válido...";
                            return Respuesta;
                        }
                    }
                    else
                    {
                        Respuesta += ";Este artículo no se ha solicitado, ya fue alistado o no corresponde con el artículo de la tarea.";
                    }
                }
                else
                {
                    Respuesta = ";El SSCC ya se encuentrá en uso";
                }                      
 * VerInfoSSCC
  //DataSet ListaAlistosSSCC = new DataSet();
                    //DataSet Aux = new DataSet();
                    //DataSet Aux2 = new DataSet();

                    
                    //string SQLConsecutivo = "SELECT " + e_TblConsecutivosSSCCFields.idConsecutivoSSCC() + 
                    //                        "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblConsecutivosSSCC() +
                    //                        "  WHERE " + e_TblConsecutivosSSCCFields.SSCCGenerado() + " = '" + SSCCLeido + "'";
                    //int idConsecutivoSSCC = Convert.ToInt32(n_ConsultaDummy.GetUniqueValue(SQLConsecutivo, idUsuario));  // obtengo el id de SSCC generado

                    //string SQLAlistosSSCC = "SELECT " + e_TblSSCCTRAFields.idRegistro() +
                    //                        "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblSSCCTRA() + 
                    //                        "  where " + e_TblSSCCTRAFields.idConsecutivoSSCC() + " = " + idConsecutivoSSCC +
                    //                        "        AND " + e_TblSSCCTRAFields.artDevuelto() + " = 0";
                    //ListaAlistosSSCC = n_ConsultaDummy.GetDataSet2(SQLAlistosSSCC, idUsuario); // obtengo un dataset con el idregistro de los artículos que estan 
                    //                                                                           // contenidos en el SSCC.

                    //if (ListaAlistosSSCC.Tables[0].Rows.Count > 0)
                    //{

                    //    for (int contador = 0; contador < ListaAlistosSSCC.Tables[0].Rows.Count; contador++)
                    //    {
                    //        int NumDocAccion = Convert.ToInt32(ListaAlistosSSCC.Tables[0].Rows[contador][0]);

                          
                    //        string BanderaAlistoAprobado = "SELECT " + e_TblDetalleSolicitudFields.idLineaDetalleSolicitud() + 
                    //                                       "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblSSCCTRA() + "  AS A" +
                    //                                       "    INNER JOIN " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + " AS B ON (A." + e_TblSSCCTRAFields.idRegistro() + " = B." +  e_TblTransaccionFields.idRegistro() + ")" +
                    //                                       "    INNER JOIN " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleSolicitud() + " AS C ON (SUBSTRING(B." + e_TblTransaccionFields.NumDocumentoAccion() + ",0,CHARINDEX('-',B." + e_TblTransaccionFields.NumDocumentoAccion() + ")) = CAST(C." + e_TblDetalleSolicitudFields.idLineaDetalleSolicitud() + " AS NVARCHAR(15)))" +                                                          
                    //                                       "  WHERE A." + e_TblSSCCTRAFields.idConsecutivoSSCC() + " = " + idConsecutivoSSCC +
                    //                                       "         AND B." + e_TblTransaccionFields.idRegistro() + " = " + NumDocAccion;

                    //        Aux2 = n_ConsultaDummy.GetDataSet2(BanderaAlistoAprobado, "0");  // obtengo el idregistro de la tabla de detalles de solicitud.

                    //        if (Aux2.Tables[0].Rows.Count > 0)
                    //        {
                    //            string SQLInfo = "SELECT A." + e_TblDetalleSolicitudFields.idArticulo() + "," +
                    //                             "       B." + e_TblMaestroArticulosFields.Nombre() + "," +
                    //                             "       B." + e_TblMaestroArticulosFields.GTIN() + "," +
                    //                             "       A." + e_TblDetalleSolicitudFields.Cantidad() + "," +
                    //                             "       A." + e_TblDetalleSolicitudFields.idDestino() + "," +
                    //                             "       C." + e_TBLdestino.Nombre() +"," +
                    //                             "       A." + e_TblDetalleSolicitudFields.idMaestroSolicitud() + "," +
                    //                             "       A." + e_TblDetalleSolicitudFields.idUsuario() + "," +
                    //                             "       B." + e_TblMaestroArticulosFields.Equivalencia() + "," +
                    //                             "       B." + e_TblMaestroArticulosFields.PesoKilos() + "," +
                    //                             "       B." + e_TblMaestroArticulosFields.DimensionUnidadM3() +
                    //                             "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleSolicitud() + " AS A," +
                    //                             "       " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroArticulos() + " AS B," +
                    //                             "       " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDestino() + " AS C" +
                    //                             "  WHERE A." + e_TblDetalleSolicitudFields.idArticulo() + " = B." + e_TblMaestroArticulosFields.idArticulo() +
                    //                             "        AND A." + e_TblDetalleSolicitudFields.idDestino() + " = C." + e_TBLdestino.idDestino() +
                    //                             "        AND A." + e_TblDetalleSolicitudFields.idLineaDetalleSolicitud() + " = " + Aux2.Tables[0].Rows[0]["idLineaDetalleSolicitud"].ToString();

/*
                string idCompaniaArticulo = ObtenerCompaniaXArticulo(Articulo[0], idUsuario);
                string[] Articulo = ObtenerIdArticuloNombreCodigoLeido_GS1128(CodLeido, idUsuario).Split(';');
                if (idCompaniaArticulo.Equals(idCompania))
                {
                    MD.CargarTextBox(Contenedor, txtidArticulo, Articulo[0]);
                    MD.CargarTextBox(Contenedor, txtNombre, Articulo[1]);
                    MD.CargarTextBox(Contenedor, txtFechaVencimiento, CargarEntidadesGS1.GS1128_DevolverFechaVencimiento(CodLeido));
                    MD.CargarTextBox(Contenedor, txtInfoCod, CargarEntidadesGS1.GS1128_DevolverInfoCodLeidoTxt(CodLeido));
                    MD.CargarTextBox(Contenedor, txtLote, CargarEntidadesGS1.GS1128_DevolveLote(CodLeido));
                    MD.CargarTextBox(Contenedor, TxtidarticuloERP, Articulo[3]);

                   // esta consulta permite saber si el artículo es de granel o no.
                    string SQL = "SELECT " + e_TblMaestroArticulosFields.Granel() +
                                 " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroArticulos() + 
                                 " WHERE " + e_TblMaestroArticulosFields.idArticulo() + " = '" + Articulo[0] + "'";
                    string Granel = Negocio.UsoGeneral.n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                    if (Granel.Equals("False"))  // si no es de granel realiza el procedimeinto de siempre.
                    {
                        Cantidad = CargarEntidadesGS1.GS1128_DevolverCantidad(CodLeido);
                        //bool EsNum = int.TryParse(Cantidad, out Cant);
                        MD.CargarTextBox(Contenedor, txtCantidad, Cantidad);

                        //MD.VisibilidadTextBox(Contenedor, txtCantidad, false);
                    }
                    else  // si es de granel hace que en el texbox de recepción aparezca el mensaje "Digite la cantidad"
                    {
                        Cantidad =  "Digite la cantidad";
                        //MD.VisibilidadTextBox(Contenedor, txtCantidad, true);
                    }

                    //MD.CargarTextBox(Contenedor, txtUbicacionSugerida,
                    //UbicSugerida = n_WMS.ObtenerUbicacionSugeridaAlmacenar(Articulo[0], Cant, idUsuario, false);
                    respuesta = "Artículo procesado correctamente;" + Articulo[0] + ";" +
                                                                      Articulo[3] + ";" +
                                                                      Articulo[1] + ";" +
                                                                      CargarEntidadesGS1.GS1128_DevolverFechaVencimiento(CodLeido) + ";" +
                                                                      CargarEntidadesGS1.GS1128_DevolveLote(CodLeido) + ";" +
                                                                      Cantidad + ";" +
                                                                      UbicSugerida;
                }
                else
                {
                    if (CodLeido == "")
                        respuesta = "Elija un artículo para ver los datos asociados.";
                    else
                        respuesta = "El artículo consultado no corresponde a la empresa asociada";
                }
 * devolver artículo del SSCC
 *  /*  obtengo el idregistro de la tabla RELSSCCTRA, la condición es: el consecutivo del SSCC exista, 
                        se excluyen los artículos generados previamente y se filtra que sea solo del artículo a devolver
                        registrado en TRAIngresoSalidaArticulos  
                    SQL = "SELECT a." + e_TblSSCCTRAFields.idRegistro() +
                          "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblSSCCTRA() + "  AS a " +
                          "    INNER JOIN " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblConsecutivosSSCC() + " AS b on (a." + e_TblSSCCTRAFields.idConsecutivoSSCC() + " = b." + e_TblConsecutivosSSCCFields.idConsecutivoSSCC() + ")" +
                          "    INNER JOIN " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + " AS c on (a." + e_TblSSCCTRAFields.idRegistro() + " = c." + e_TblTransaccionFields.idRegistro() + ")" +
                          "  WHERE b." + e_TblConsecutivosSSCCFields.SSCCGenerado() + " = '" + SSCCLeido + "'" +
                          "        and a." + e_TblSSCCTRAFields.artDevuelto() + " = 0" +
                          "        and c." + e_TblTransaccionFields.idArticulo() + " = '" + ArticuloDevolver + "'";

                   string idRegistro = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);
                   string idRegistroriginal = idRegistro;  //  se utilizará la variable idRegistroriginal para actualizar la tabla TblSSCCTRA, en caso de que el artículo sea devuelto.
                  // este proceso verifica que el artículo que pertenece al SSCC, no haya sido despachado.
                    DataSet DS = new DataSet();
                   while (true)
                   {  
                      SQL = "";
                      SQL = "SELECT top 1 " + e_TblTransaccionFields.idRegistro() + "," +
                            "        case when idMetodoaccion = 67 and idEstado = 12 then 0 else 1 end as PermiteDevoluion" + 
                            "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                            "  WHERE " + e_TblTransaccionFields.NumDocumentoAccion() + " = '" + idRegistro + "'";
                        DS = n_ConsultaDummy.GetDataSet(SQL, idUsuario);
                      if (DS.Tables[0].Rows.Count > 0)
                      {
                          if (DS.Tables[0].Rows[0]["PermiteDevoluion"].ToString() == "1")
                              idRegistro = DS.Tables[0].Rows[0]["idRegistro"].ToString();
                          else
                              return "Artículo ya fue despachado..";
                      }
                      else
                      {
                          idRegistro = idRegistroriginal;
                          break;
                      }
                   }

                  //   verifico que el idregistro encontrado en RELSSCCTRA, efectivamente exista en TRAIngresoSalidaArticulos.
                   SQL = "SELECT  1 FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                         "   where  " + e_TblTransaccionFields.idRegistro() + " = " + idRegistro +
                         "         AND " + e_TblTransaccionFields.idArticulo() + " = '" + ArticuloDevolver + "'";
                   string existearticulo = n_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                   if (existearticulo == "1")  // el artículo es parte del SSCC
                   {  
                     //  se hace consulta  la tabla TRACEID.dbo.TRAIngresoSalidaArticulos, hacia adelante, para ver la ultima transacción y con eso determinar 
                     //  si el artículo puede ser devuelto o no.

                      SQL = "SELECT  " + e_TblTransaccionFields.idUbicacion() +
                              " FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() +
                              " WHERE " + e_TblTransaccionFields.idTablaCampoDocumentoAccion() + " = '" + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleSolicitud() + "'" +
                              "       and " + e_TblTransaccionFields.idCampoDocumentoAccion() + " = '"  + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblDetalleSolicitud() + "." + e_TblDetalleSolicitudFields.idLineaDetalleSolicitud() + "'" +
                              "       and " + e_TblTransaccionFields.idRegistro() + " = " + idRegistro;

                         RegistrosTRA = n_ConsultaDummy.GetDataSet(SQL, idUsuario);

                         if (RegistrosTRA.Tables[0].Rows.Count == 1)  //  debe haber un solo registo de transacción(idregistro)
                         {    
                            Ubicacionactual = RegistrosTRA.Tables[0].Rows[0]["idubicacion"].ToString();  // esta es la ubicación actual 
                            Permitedevolucion = 1;                                                       // de donde sera sacado el artículo.
                         }
                         else
                         {
                           if (RegistrosTRA.Tables[0].Rows.Count > 1)  //  si hay más de un registro, hay un problema de trazabilidad 
                           {  
                              Respuesta += "Inconsistencia en tabla de Transacciones";
                           }

                         }

                   //cuando la Transacción permite la devolución, se extrae la etiqueta de la ubicación actual y la ubicación anterior; para poder ejecutar el metodo
                   //LeerCodigoParaUbicarHH con ambas ubicaciones y hacer la devolución.
                      if (Permitedevolucion == 1)
                      {
                          /* se comenta esta parte del código, por sugerencia de JGondres, se sigue el flujo normal de despacho y 
                             despues se aplican las transacciones en la tabla TRA
                           SQL = "select  replace((replace(" + e_VistaCodigosUbicacionFields.ETIQUETA() + ",'(','')),')','') as etiqueta from " + 
                                       e_TablasBaseDatos.VistaCodigosUbicacion() + " where " + e_VistaCodigosUbicacionFields.idUbicacion() + " = '" + Ubicacionactual + "'";
                           Ubicacionactual = da_ConsultaDummy.GetUniqueValue(SQL, idUsuario);

                           string Codigo = spl[1].Trim() + ";" + Ubicacionactual + ";" + Ubicacionmover + ";0";

                           Respuesta = LeerCodigoParaUbicarHH(Codigo, idUsuario, "75");

                           if (Respuesta == "Transaccion exitosa")  // si fue positivo el traslado, se actualiza a devuelto en la tabla RELSSCCTRA.
                           { 
                          SQL = "UPDATE " + e_TablasBaseDatos.TblSSCCTRA() + 
                                  "  SET  " + e_TblSSCCTRAFields.artDevuelto() + " = 1," +
                                  "       " + e_TblSSCCTRAFields.ubicacionDevuelto() + " = '" + Ubicacionmover + "'" +
                                  "  WHERE " + e_TblSSCCTRAFields.idRegistro() + " = " + idRegistroriginal;
                            bool devuelto = da_ConsultaDummy.PushData(SQL, idUsuario);

                            if (!devuelto)
                                Respuesta += "-NO pudo actualizarse SSCC";
                            else
                                Respuesta = "Transacción Exitosa.";

                         //}
                    }
                    else
                        Respuesta += "-Artículo no puede ser devuelto ";
                }
                else
                    Respuesta += "-SSCC o Artículo a Devolver no corresponde ";
                }
                else
                {
                    Respuesta = "-El artículo consultado no corresponde a la empresa asociada";
                }
 * 
 
                //if ( zona != ConfigurationManager.AppSettings["Zona_Transito"].ToString())
                //{
                //    resultado += "Ubicación no permitida para este proceso...";
                //    return resultado;
                //}

                //SQL = "SELECT B." + e_TblTransaccionFields.idArticulo() + "," + 
                //                    e_TblTransaccionFields.FechaVencimiento() + "," + 
                //                    e_TblTransaccionFields.Lote() + "," + 
                //                    e_TblTransaccionFields.Cantidad() + 
                //            ",A." + e_TblSSCCTRAFields.idRegistro() +
                //            ",C." + e_TblMaestroArticulosFields.Granel() +
                //      "  FROM " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblSSCCTRA() + " AS A" +
                //      "    INNER JOIN " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblTransaccion() + " AS B ON (A." + e_TblSSCCTRAFields.idRegistro() + " = B." + e_TblTransaccionFields.idRegistro() + ")" +
                //      "    INNER JOIN " + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblMaestroArticulos() + " AS C ON (B." + e_TblTransaccionFields.idArticulo() + " = C." + e_TblMaestroArticulosFields.idArticulo() + ")" +
                //      "  WHERE " + e_TblSSCCTRAFields.idConsecutivoSSCC() + " = (SELECT " + e_TblConsecutivosSSCCFields.idConsecutivoSSCC() + 
                //                                                            "      FROM "  + e_BaseDatos.NombreBD() + "." + e_BaseDatos.Esquema() + "." + e_TablasBaseDatos.TblConsecutivosSSCC() +
                //                                                            "      WHERE " + e_TblConsecutivosSSCCFields.SSCCGenerado() + " = '" + SSCC.Substring(2) + "')" +
                //     "  ORDER BY A." + e_TblSSCCTRAFields.idRegistro();

                //DataSet DS = n_ConsultaDummy.GetDataSet(SQL, idUsuario);

                //if (DS.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow DR in DS.Tables[0].Rows)
                //    {
                //        string idArticulo = DR["idArticulo"].ToString();
                //        string FechaVencimiento = DR["FechaVencimiento"].ToString();
                //        string Lote = DR["Lote"].ToString();
                //        string Cantidad = DR["Cantidad"].ToString();
                //        string idregistro = DR["idRegistro"].ToString();
                //        string Granel = DR["Granel"].ToString();
                //        DateTime FechaSQL = Convert.ToDateTime(FechaVencimiento);
                //        string year = FechaSQL.Year.ToString().Substring(2);
                //        string month = FechaSQL.Month.ToString();
                //        string day = FechaSQL.Day.ToString();

                //        if (month.Length == 1)
                //        {
                //            month = "0" + month;
                //        }

                //        if (day.Length == 1)
                //        {
                //            day = "0" + day;
                //        }

                //        FechaVencimiento = year + month + day;

                //        if (Granel == "True")
                //        {
                //            Single Cantgranel = 0.0F;
                //            Cantgranel = Single.Parse(Cantidad, System.Globalization.CultureInfo.InvariantCulture) / 1000;
                //            Cantidad = Cantgranel.ToString().Replace(",", ".");
                //        }

                //        string codigoGS1 = CrearCodigoGS1(idArticulo, Cantidad, FechaVencimiento, Lote, idUsuario);

                //        codigoGS1 = codigoGS1 + ";" + ubic + ";" + idregistro;

                //        registroSSCC++;

                //        string resulta = LeerCodigoParaUbicarHH(codigoGS1, idUsuario, "62");  // realiza asociación SSCC con ubicación.

                //        if (!resulta.Contains("Transaccion exitosa"))
                //        {
                //            resultado = resulta;
                //            break;
                //        }

                //        resultado = resulta;
                //    }
                //}
                //else
                //    resultado += "SSCC sin contenido";*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.AccesoDatos.ProcesarSolicitud;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using Diverscan.MJP.Negocio.GS1;
using Diverscan.MJP.AccesoDatos.UsoGeneral;
using Diverscan.MJP.Negocio.LogicaWMS;

namespace Diverscan.MJP.Negocio.ProcesarSolicitud
{
    public class n_ProcesarSolicitud
    {
        public string InsertarDetalleSolicitud(e_ProcesarSolicitud ProcesarSolictud)
        {
            da_ProcesarSolicitud DetalleSolicitud = new da_ProcesarSolicitud();
            string result = DetalleSolicitud.InsertarDetalleSolicitud(ProcesarSolictud);
            return result;
        }

        public string GeneraTarea(int idUsuario, Int64 idMaestroSolicitud, string Actualiza, int idBodega)  //, string SSCC)
        {
            da_ProcesarSolicitud GeneraTarea = new da_ProcesarSolicitud();
            string resultado = GeneraTarea.GeneraTarea(idUsuario, idMaestroSolicitud, Actualiza, idBodega);  //, SSCC);
            return resultado;
        }

        public string AlistarArticulo(Int64 idArticulo, string idCompania, string SSCCGenerado, int idMetodoAccion, string Lote, string FV,
            Int64 idMaestroSolicitud, Int64 Cantidad, Int64 Idubicacion, string idTarea)
        {
            da_ProcesarSolicitud Alistararticulo = new da_ProcesarSolicitud();
            string resultado = Alistararticulo.AlistarArticulo(idArticulo, idCompania, SSCCGenerado, idMetodoAccion, Lote, FV, idMaestroSolicitud, Cantidad, Idubicacion, idTarea);
            return resultado;
        }

        public string DevuelveInfoArticulo(string GS1, string idCompania)
        {
            da_ProcesarSolicitud DevuelveInfo = new da_ProcesarSolicitud();
            string resultado = DevuelveInfo.DevuelveInfoArticulo(GS1, idCompania);
            return resultado;
        }

        public DataSet DevuelveInfoSSCC(string SSCCGenerado, string idcompania)
        {
            DataSet DB = new DataSet();
            da_ProcesarSolicitud DevuelveInfoSSCC = new da_ProcesarSolicitud();
            DB = DevuelveInfoSSCC.DevuelveInfoSSCC(SSCCGenerado, idcompania);
            return DB;
        }

        public string DevolverArticuloSSCC(string idCompania, string SSCCGenerado, Int64 Cantidad, string UbicacionaMover, Int64 idArticulo)
        {
            da_ProcesarSolicitud DevuelveArticulo = new da_ProcesarSolicitud();
            string resultado = DevuelveArticulo.DevolverArticuloSSCC(idCompania, SSCCGenerado, Cantidad, UbicacionaMover, idArticulo);
            return resultado;
        }

        public string ProcesarDespacho(string SSCCGenerado, string idCompania, int idUsuario)
        {
            da_ProcesarSolicitud ProcesaDespacho = new da_ProcesarSolicitud();
            string resultado = ProcesaDespacho.ProcesarDespacho(SSCCGenerado, idCompania, idUsuario);
            return resultado;
        }

        public string AsociarSSCCTransito(string SSCCGenerado, string idCompania, int idUsuario, string EtiquetaUbicacion, string ZonaTransito)
        {
            da_ProcesarSolicitud ProcesaDespacho = new da_ProcesarSolicitud();
            string resultado = ProcesaDespacho.AsociarSSCCTransito(SSCCGenerado, idCompania, idUsuario, EtiquetaUbicacion, ZonaTransito);
            return resultado;
        }

        public string AsociarSSCCV2(string CodLeido, string idUsuario)
        {
            //da_ProcesarSolicitud _da_ProcesarSolicitud = new da_ProcesarSolicitud();
            //string resultado = "";
            //resultado = _da_ProcesarSolicitud.AlistarArticuloV2(
            //    idArticulo, idCompania, SSCCGenerado, idMetodoAccion, Lote, FV,
            //    idMaestroSolicitud, Cantidad, Idubicacion, idTarea, esAutorizado);
            //return resultado;
            string SQL = "";
            string Respuesta = "No Procesado;-1";

            try
            {
                string idCompania = n_WMS.ObtenerCompaniaXUsuario(idUsuario);
                //TRAMA = SSCC;CODGS1;UBICACIÓN_PISTOLEADA;UBICACIÓN_TAREA;Idmaestrosolicitud;idtarea
                string[] spl = CodLeido.Split(';'); //Se obtienen los datos enviado en la trama desde HH           
                
                string esUsuarioAutorizado = spl[6].Trim(); //Obtiene true si un usuario administrador autorizó el alisto del artículo
                bool esUsuarioAutorizadoBoolean = false; //Permite determinar si el alisto se realizará ya con autorización
                if (("true").Equals(spl[6].ToLower()))
                {
                    esUsuarioAutorizadoBoolean = true;
                }
                else
                {
                    esUsuarioAutorizadoBoolean = false;
                }
                string idUsuarioAutorizador = spl[7].Trim();//Se obtienen un valor diferente de -1 cuando se autorizó el alisto                



                // Obtengo información del artículo a alistar
                if (spl[1].Substring(0, 2) != "01")  // en caso de leer un GTIN13-GTIN14 se le pone adelante el 01 para poder procesar la recepción-ubicación-alisto.
                {
                    if (spl[1].Length == 13)
                        spl[1] = "010" + spl[1];
                    else if (spl[1].Length == 14)
                        spl[1] = "01" + spl[1];
                }

                // valido que todo lo que venga en la trama este correcto
                string[] Articulo = n_WMS.ObtenerIdArticuloNombreCodigoLeido_GS1128(spl[1].Trim(), idUsuario).Split(';'); //SE obtiene el artículo leído por la HH para verificar el mismo existe
                string SSCCLeido = CargarEntidadesGS1.GS1128_DevolverSSCCGenerado(spl[0].Trim());
                string idUbicacion = n_WMS.DevolverIdUbicacion(spl[2].Trim(), idUsuario);  // se obtiene el IdUbicacion a partir de la etiqueta pistoleada.
                string ubicavalida = n_WMS.DevolverIdUbicacion(spl[3].Trim(), idUsuario);  // se obtiene el IdUbicacion a partir de la etiqueta de la tarea.
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
                //Se comenta dado que la ubicación al alistar puede ser otra diferente a la del alisto si se autoriza el alisto
                //if (idUbicacion != ubicavalida && esUsuarioAutorizadoBoolean == false)//Se valida que la ubicación se la misma de la tarea en caso de que no se tenga autorización para el  alisto de un producto que no es el más pronto a vecer
                //{
                //    Respuesta = "Ubicación no corresponde...;-1";
                //    return Respuesta;
                //}

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
                    int diasminimosrest = n_WMS.Diasminimosvencimientoresturantes(idArticulo, idUsuario);
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

                //if (idUbicacion == ubicavalida && esUsuarioAutorizadoBoolean == true)
                //{
                //    da_ProcesarSolicitud da_ProcesarSolicitud = new da_ProcesarSolicitud();
                //    Respuesta = da_ProcesarSolicitud.AlistarArticuloV2(Int64.Parse(idArticulo),
                //                                                idCompania,
                //                                                SSCCLeido,
                //                                                28,
                //                                                Lote,
                //                                                FV,
                //                                                Int64.Parse(idMaestrosolicitud),
                //                                                Int64.Parse(CantidadLineas.ToString()),
                //                                                Int64.Parse(idUbicacion),
                //                                                spl[5],
                //                                                esUsuarioAutorizadoBoolean,
                //                                                Int32.Parse(idUsuarioAutorizador)
                //                                                );
                //}

                da_ProcesarSolicitud da_ProcesarSolicitud = new da_ProcesarSolicitud();
                Respuesta = da_ProcesarSolicitud.AlistarArticuloV2(Int64.Parse(idArticulo),
                                                            idCompania,
                                                            SSCCLeido,
                                                            28,
                                                            Lote,
                                                            FV,
                                                            Int64.Parse(idMaestrosolicitud),
                                                            Int64.Parse(CantidadLineas.ToString()),
                                                            Int64.Parse(ubicavalida),
                                                            spl[5],
                                                            esUsuarioAutorizadoBoolean,
                                                            Int32.Parse(idUsuarioAutorizador)
                                                            );
                



                return Respuesta;
            }
            catch (Exception ex)
            {
                return ex.Message + "-" + Respuesta;
            }

        }
    }   
}

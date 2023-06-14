using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Utilidades;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Diverscan.MJP.AccesoDatos.Reportes
{
    public class da_KardexReport
    {

        /// <summary>
        /// Autor:Guillermo Ramirez
        /// Description: Retorna id,nombre corto y un concatenado del nombre corto y la descripcion.
        /// </summary>
        /// <param name="sNombreBaseDatos"></param>
        /// <param name="sUsuario"></param>
        /// <returns></returns>
        static decimal SA = 0.0M;
        public static List<e_KardexReport> ObtenerKardexReportInfo(DateTime fechaIni, DateTime fechaFin,string idArticulo)
        {
           
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("Obtener_KardexReportInfo");

                dbTse.AddInParameter(dbCommand, "@saldo", DbType.Decimal, 0);
                dbTse.AddInParameter(dbCommand, "@Idma", DbType.Int32, 0);

                dbTse.AddInParameter(dbCommand, "@Fechain", DbType.DateTime, fechaIni);
                dbTse.AddInParameter(dbCommand, "@fechafin", DbType.DateTime, fechaFin);
                dbTse.AddInParameter(dbCommand, "@idArticulo", DbType.String, idArticulo);

                var ekardex = new List<e_KardexReport>();

                dbCommand.CommandTimeout = 3600;

                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        ekardex.Add(CargarInfoKardex(reader));
                    }
                }

                SA = 0.0M;
                return ekardex;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "GetProgramas";
                const string listaParametros = "";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0)
                    indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal);
                // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_Programa.cs", "GetProgramas()", "", lineaError,
                                             nombreProcedimiento, listaParametros, exError.Message);

                return null;
            }
        }

        /// <summary>
        /// Autor: Guillermo Ramirez
        /// Description: Retorna los datos que se mostraban en el reporte llamado Kardex.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static e_KardexReport CargarInfoKardex(IDataReader reader)
        {
            var ekardex = new e_KardexReport();

            if (decimal.Parse(reader["saldo_inicial"].ToString()) > 0 && SA == 0)
                SA = decimal.Parse(reader["saldo_inicial"].ToString());

            //ekardex.Linea = int.Parse(reader["linea"].ToString());
            //ekardex.IdRegistro = int.Parse(reader["idregistro"].ToString());
            ekardex.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());  // 
            //ekardex.IdArticulo = reader["idarticulo"].ToString();
            ekardex.ArticuloSAP = reader["art_sap"].ToString();
            ekardex.Articulo = reader["articulo"].ToString();
            ekardex.Zona = reader["zona"].ToString();
            ekardex.Motivo = reader["motivo"].ToString();
            //ekardex.Operacion = char.Parse(reader["operacion"].ToString());
            ekardex.SaldoInicial = decimal.Parse(reader["saldo_inicial"].ToString());
            ekardex.Cantidad = decimal.Parse(reader["cantidad"].ToString());
            //ekardex.Cantidad_Unidad = decimal.Parse(reader["saldo"].ToString());
            ekardex.Saldo = SA + decimal.Parse(reader["saldo"].ToString());
            ekardex.Saldo_actual = decimal.Parse(reader["Saldo_actual"].ToString());
            ekardex.Lote = reader["lote"].ToString();
            ekardex.FechaVencimiento = DateTime.Parse(reader["fechavencimiento"].ToString());
            //ekardex.Orden = reader["orden"].ToString();
            ekardex.OCDestino = reader["OC_Destino"].ToString();
            ekardex.IdMetodoAccion = int.Parse(reader["idmetodoaccion"].ToString());
            ekardex.Peso = decimal.Parse(reader["Peso"].ToString());
            ekardex.Unid_inventario = decimal.Parse(reader["Unid_Inventario"].ToString());
            ekardex.Unidad = reader["Unidad_medida"].ToString().Replace("-","");
            ekardex.Unidad_medida = ekardex.Unid_inventario.ToString() + reader["Unidad_medida"].ToString();
            ekardex.Saldo_Unidad = reader["Saldo_Unidad"].ToString();
            SA += decimal.Parse(reader["saldo"].ToString());

            return ekardex;
        }
    }
}

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
    public class da_HistoricoDemanda
    {
        public static List<e_HistoricoDemanda> ObtenerReporteHistoricoDemanda(DateTime fechaIni, DateTime fechaFin, string IdArticulo)
        {
            try
            {
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("Obtener_HistoricoDemanda");

                dbTse.AddInParameter(dbCommand, "@Fechaini", DbType.DateTime, fechaIni);
                dbTse.AddInParameter(dbCommand, "@Fechafin", DbType.DateTime, fechaFin);
                dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.String, IdArticulo);

                var HistoricoDemanda = new List<e_HistoricoDemanda>();

                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        HistoricoDemanda.Add(CargarHistoricoDemanda(reader));
                    }
                }

                return HistoricoDemanda;
            }
            catch (Exception exError)
            {
                var clLog = new clErrores();
                const string nombreProcedimiento = "ProveedorReportInfo";
                const string listaParametros = "";
                var indexNumLinea = exError.StackTrace.LastIndexOf("línea", StringComparison.Ordinal);
                if (indexNumLinea < 0)
                    indexNumLinea = exError.StackTrace.LastIndexOf("line", StringComparison.Ordinal);
                // Si no existe, está en inglés
                var lineaError = exError.StackTrace.Substring(indexNumLinea);
                clLog.escribirErrorDetallado("da_ProveedorReport.cs", "ObtenerProveedorReportInfo()", "", lineaError,
                                             nombreProcedimiento, listaParametros, exError.Message);

                return null;
            }
        }

        private static e_HistoricoDemanda CargarHistoricoDemanda(IDataReader reader)
        {
            var eHistoricoDemanda = new e_HistoricoDemanda();

            eHistoricoDemanda.NombreArticulo = reader["NombreArticulo"].ToString();
            eHistoricoDemanda.CodInterno = reader["idInterno"].ToString();

            eHistoricoDemanda.Año = reader["Anno"].ToString();
            eHistoricoDemanda.Mes = reader["Mes"].ToString();
            eHistoricoDemanda.Semana = reader["Semana"].ToString();

            eHistoricoDemanda.TotalSemana = Convert.ToSingle(reader["CantidadSemana"].ToString());

            eHistoricoDemanda.Lunes = Convert.ToSingle(reader["CantLunes"].ToString());
            eHistoricoDemanda.Martes = Convert.ToSingle(reader["CantMartes"].ToString());
            eHistoricoDemanda.Miercoles = Convert.ToSingle(reader["CantMiercoles"].ToString());
            eHistoricoDemanda.Jueves = Convert.ToSingle(reader["CantJueves"].ToString());
            eHistoricoDemanda.Viernes = Convert.ToSingle(reader["CantViernes"].ToString());
            eHistoricoDemanda.Sabado = Convert.ToSingle(reader["CantSabado"].ToString());
            eHistoricoDemanda.Domingo = Convert.ToSingle(reader["CantDomingo"].ToString());

            eHistoricoDemanda.TotalLunes = Convert.ToSingle(reader["CantLunesTotal"].ToString());
            eHistoricoDemanda.TotalMartes = Convert.ToSingle(reader["CantMartesTotal"].ToString());
            eHistoricoDemanda.TotalMiercoles = Convert.ToSingle(reader["CantMiercolesTotal"].ToString());
            eHistoricoDemanda.TotalJueves = Convert.ToSingle(reader["CantJuevesTotal"].ToString());
            eHistoricoDemanda.TotalViernes = Convert.ToSingle(reader["CantViernesTotal"].ToString());
            eHistoricoDemanda.TotalSabado = Convert.ToSingle(reader["CantSabadoTotal"].ToString());
            eHistoricoDemanda.TotalDomingo = Convert.ToSingle(reader["CantDomingoTotal"].ToString());


            return eHistoricoDemanda;
        }
    }
}

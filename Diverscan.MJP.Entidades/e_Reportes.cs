using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrystalDecisions.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Diverscan.MJP.Entidades
{
    public static class e_Reportes
    {
       public static ReportDocument DocumentoReporte { get; set; }        // objeto que representa el reporte.
       public static ParameterFieldDefinitions Parametros  { get; set; }  // Contendra todos los objetos parámetros del informe.
       public static ParameterFieldDefinition Parametro  { get; set; }    // representa un parametro de reporte.
       public static ParameterValues valores { get; set; }                // objeto arreglo de valores del parametro.
       public static ParameterDiscreteValue valor { get; set; }           // El valor del parametro.  
       public static string nomrep { get; set; }                          // nombre del reporte.
       public static string Serrep { get; set; }                          // nombre del servidor. 
       public static string DBrep  { get; set; }                          // base de datos.
       public static string Usrep  { get; set; }                          // usuario de la base de datos.
       public static string Pasrep { get; set; }                          // Password del usuario.
    }
}

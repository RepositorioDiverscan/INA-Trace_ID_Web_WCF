using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{

    public class e_TareaAsignada 
    {
        public int idRegistro { get; set; }
        public int idMetodAccion { get; set; }
        public int idUsuario { get; set; }
        public decimal HorasExtra { get; set; }
        public decimal HorasDisponible { get; set; }
        public string idTarea { get; set; }
        public decimal TiempoEstimadoHora { get; set; }
        public int Num_solicitud { get; set; }
        public bool Alistado { get; set; }
    }

    public class e_TareaUsuario
    {
        public string idTarea { get; set; }
        public string idRegistro { get; set; }
        public string idMetodoAccion { get; set; }
        public decimal TiempoEstimado { get; set; }
        public List<e_UsuarioProductivo> UsuariosProductivos { get; set; }
    }

    public class e_UsuarioProductivo
    {
        public string idUsuario {get; set;}
        public decimal HorasExtraNecesarias { get; set; }
        public decimal HHDisponiblesParaTarea {get;set;}
        public bool Ocupado { get; set; }
        public List<e_TareaUsuario> TareasAsignadas { get; set; }
    }


     public class e_Usuario
     {
         public bool Aprobador { get; set; }
         public string Nombre { get; set; }
         public string Apellido { get; set; }
         public string NombreCompleto { get; set; }
         public string IdUsuario { get; set; }
         public string Usuario { get; set; }
         public string Contrasenna { get; set; }
         public string Email { get; set; }
         public string IdRoles { get; set; }
         public string Comentarios { get; set; }
         public string idCompania { get; set; }
         public bool Bloqueado { get; set; }
         public decimal HHDisponiblesParaTarea { get; set; }
         public int IdBodega { get; set; }
         public bool TrazableBodega { get; set; }

    }

   public class e_Tarea
    {
        public string idTarea { get; set; }
        public int idRegistro { get; set; }
        public decimal TiempoEstimado { get; set; }
        public List<e_Usuario> Valores { get; set; }
    }



    public class Palabra
    {
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public int LargoMin { get; set; }
        public int LargoMax { get; set; }
        public bool TieneNumeros { get; set; }
        public decimal NumeroDecimal { get; set; }
        public string Tipo { get; set; }
        public int Precision { get; set; }
        public bool is_identity { get; set; }
        public string idMetodoAccion { get; set; }
        public List<string> Valores { get; set; }
    }

    public class Diccionario
    {
        public Palabra _Diccionaro { get;set; }
        public List<Dependiente> Dependientes { get; set; }        
    }

    public class Dependiente
    {
        public Palabra _Dependiente { get;set ; }
        public List<Independiente> Independientes { get; set; }
        public List<Palabra> Sinonimos { get; set; }
    }

    public class Independiente
    {
        public Palabra _Independiente { get; set; }
        public List<Palabra> Sinonimos { get; set; }

    }

    public class Pregunta
    {
        public List<Palabra> Palabras { get; set; }
        public string Enunciado { get; set; }
        public List<Respuesta> Respuestas = new List<Respuesta>();
    }

    public class Respuesta
    {
        public List<Palabra> Palabras { get; set; }
        public string Enunciado { get; set; }
    }


    //tabla ADMAcciones
    public class e_AccionFlujo 
    {
        public double IdAccion { get; set; }
        public int IdActividad { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Fuente { get; set; }
        public string ObjetoFuente { get; set; }
        public e_Evento Envento { get; set; }
        public List<e_Metodo> Metodos { get; set; }
        public string Idrol { get; set; }
    }

    public class e_Evento
    {
        public int idEvento { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    //Tabla Flow_MetodoAccion
    public class e_Metodo
    {
        public double idMetodoAccion { get; set; }
        public string IdMetodo { get; set; }
        public double idTipoMetodo { get; set; }
        public string NombreTipoMetodo { get; set; }
        public string idParametroAccionSalida { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool AcumulaSalida { get; set; }
        public int Secuencia { get; set; }
        public double IdParametroAccion { get; set; } //este es el parametro que se actualizara cuando el metodo de resultado
        public List<e_ParametrosWF> Parametros { get; set; }
    }

    //Tabla Flow_ParametroWF
    public class e_ParametrosWF
    {

        public double IdParametroAccion { get; set; }
        public double IdMetodoAccion { get; set; }
        public string Nombre { get; set; }
        public double Numero { get; set; }
        public string Valor { get; set; }
        public bool MultipleValor {get; set;}
        public List<e_ValorMultipleValor> ValorM { get; set; }
        public e_TipoParametro TipoParametro { get; set; }
    }

    /// <summary>
    /// Contendra todos los valores que contenga VALOR separaos por ;
    /// </summary>
    public class e_ValorMultipleValor
    {
        public string ValorMulti { get; set; }
        public e_TipoParametro TipoParametro { get; set; }
    }

    //Tabla Flow_TipoParametro
    public class e_TipoParametro
    {
        public int IdTipoParametro { get; set; }
        public string Nombre { get; set; }
    }


    public class e_AlistoUsuario 
    {
        public int idRegistro { get; set; }
        public string Nombre { get; set; }
        public string Lote { get; set; }
        public int idUbicacion { get; set; }
        public decimal cantidad { get; set; }

    }

}

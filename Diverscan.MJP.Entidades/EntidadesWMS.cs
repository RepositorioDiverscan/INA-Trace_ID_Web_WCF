using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{

    public class e_AsignarAlisto
    {
        List<e_Articulo> Articulos = new List<e_Articulo>();
        List<e_Usuario> Usuarios = new List<e_Usuario>();
    }

    public class e_ArticuloParaAlisto
    {
        e_Articulo Articulo { get; set; }
        decimal TiempoPorAsignar { get; set; }
    }

    public class e_Ubicacion
    {
        public double idAlmacen { get; set; }
        public string AbreviaturaAlmacen { get; set; }
        public int idBodega { get; set; }
        public int SecuenciaBodega { get; set; }
        public string AbreviaturaBodega { get; set; }
        public string idZona { get; set; }
        public string AbreviaturaZona { get; set; }
        public int idUbicacion { get; set; }
        public string CodUbicacion { get; set; } // codigo que se lee por hand held
        public string Etiqueta { get; set; }
        public string estante { get; set; }
        public string columna { get; set; }
        public string nivel { get; set; }
        public string pos { get; set; }
        public decimal largo { get; set; }
        public decimal areaAncho { get; set; }
        public decimal alto { get; set; }
        public string cara { get; set; }
        public string profundidad { get; set; }
        public decimal CapacidadPesoKilos { get; set; }
        public decimal CapacidadVolumenM3 { get; set; }
        public decimal DisponibilidadPesoKilos { get; set; }
        public decimal DisponibilidadVolumenM3 { get; set; }
        public decimal RelacionPesoVolLibre { get; set; }
        public List<e_Articulo> Articulos{ get; set; }
        public int Secuencia { get; set; }
        public bool PermiteIngreso { get; set; }
        public int idBodegaUbicacion { get; set; }
    }

    [Serializable]
    public class e_Articulo
    {
        public int IdArticulo { get; set; }
        public int IdInterno { get; set; }
        public string IdCompania { get; set; }
        public string Nombre { get; set; }
        public string NombreHH { get; set; }
        public string GTIN { get; set; }
        public int idUnidadMedida { get; set; }
        public int idTipoEmpaque { get; set; }
        public int idEtiqueta { get; set; }
        public decimal DuracionHoraAlisto { get; set; }
        public int idBodega { get; set; }
        public decimal PesoKilos { get; set; }
        public decimal DimensionUnidadM3 { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Lote { get; set; }
        public string CantidadDisponible { get; set; }
        public List<e_CantidadeEstado> CantidadesEstado { get; set; }
      
    }
    public class e_CantidadeEstado 
    {
        // La cantidad por estado se obtiene de la tabla TRAIngresoSalidaArticulos y el estado de ADMEstado
       public int idEstado { get; set; }
       public string Nombre { get; set; }
       public double Cantidad { get; set; }
    }


    public class e_Rutas 
    {
        public int idRuta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Comentarios { get; set; }
        public List<e_Vehiculo> Vehiculos { get; set; }
    
    }


    public class e_Vehiculo 
    {
        public int IdVehiculo { get; set; }
        public string TipoVehiculo { get; set; }
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string Comentario { get; set; }
        public decimal CapacidadPeso { get; set; }
        public decimal CapacidadVolumen { get; set; }
        public string Color { get; set; }
        public string MarcaVehiculo { get; set; }     
        public int idTransportista { get; set; }
        public string NombreTransportista { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string ComentariosTransportista { get; set; }
        public string NombreCompañia { get; set; }
        public List<e_Destinos_Dev> Destinos { get; set; }

    }

    public class e_Destinos_Dev
    {
        public int idDestino { get; set; }
        public string Nombre { get; set; }
        public string NombreDestino { get; set; }
        public string Direccion { get; set; }
        public string DescripcionDestino { get; set; }
        public bool Estado { get; set; }

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Trazabilidad.Entidades
{
    [Serializable]
    public class EAjusteInventario
    {
        private string idMovimiento;
        private string numeroMovimiento;
        private int idArticulo;
        private string idInternoSAP;
        private string nombreArticulo;
        private decimal cantidad;
        private string unidadMedida;
        private string ajusteInventarioDescripcion;
        private string efectoInventario;
        private DateTime fechaRegistroTrazabilidad;
        private string descripcionUbicacion;
        private string nombreUsuario;
        private string lote;


        //Para exportar a excel las fechas 
        private string fechaSolicitudExport;
        private string fechaAprobadoExport;
        private string fechaRegistroTrazabilidadExport;
        private string idDestino;
        private string usuarioNombreCompleto;
        private string efectoEnInventario;
        private string idUbicacion;

        public EAjusteInventario(string idMovimiento, string numeroMovimiento, int idArticulo, string idInternoSAP, string nombreArticulo, decimal cantidad, 
            string unidadMedida, string ajusteInventarioDescripcion, string efectoEnInventario, DateTime fechaRegistroTrazabilidad, 
            string idUbicacion, string usuarioNombreCompleto, string lote)
        {
            this.idMovimiento = idMovimiento;
            this.numeroMovimiento = numeroMovimiento;
            this.idArticulo = idArticulo;
            this.idInternoSAP = idInternoSAP;
            this.nombreArticulo = nombreArticulo;
            this.cantidad = cantidad;
            this.unidadMedida = unidadMedida;
            this.ajusteInventarioDescripcion = ajusteInventarioDescripcion;
            this.efectoEnInventario = efectoEnInventario;
            this.fechaRegistroTrazabilidad = fechaRegistroTrazabilidad;
            this.idUbicacion = idUbicacion;
            this.usuarioNombreCompleto = usuarioNombreCompleto;
            this.lote = lote;
        }

        public string IdMovimiento
        {
            get { return idMovimiento; }
        }

        public string NumeroMovimiento
        {
            get { return numeroMovimiento; }
        }

        public int IdArticulo
        {
            get { return idArticulo; }
        }

        public string IdInternoSAP
        {
            get { return idInternoSAP; }
        }

        public string NombreArticulo
        {
            get { return nombreArticulo; }
        }

        public decimal Cantidad
        {
            get { return cantidad; }
        }

         public string UnidadMedida
        {
            get { return unidadMedida; }
        }

        public string AjusteInventarioDescripcion
        {
            get { return ajusteInventarioDescripcion; }
        }

        public string EfectoEnInventario
        {
            get { return efectoEnInventario; }
        }

        public DateTime FechaRegistroTrazabilidad
        {
            get { return fechaRegistroTrazabilidad; }
        }

        public string IdUbicacion
        {
            get { return idUbicacion; }
        }

        public string UsuarioNombreCompleto
        {
            get { return usuarioNombreCompleto; }
        }

        public string Lote
        {
            get { return lote; }
        }

    }

}



        /* public EAjusteInventario(
             string IdMovimiento,
             string NumeroMovimiento,
             int IdArticulo,
             string IdInternoSAP,
             string NombreArticulo,
             int Cantidad,
             string UnidadMedida,
             string AjusteInventarioDescripcion,
             string EfectoInventario, 
             DateTime FechaRegistroTrazabilidad,
             string DescripcionUbicacion,
             string NombreUsuario, 
             string Lote)
         {
             idMovimiento = IdMovimiento;
             numeroMovimiento = NumeroMovimiento;
             idArticulo = IdArticulo;
             idInternoSAP = IdInternoSAP;
             nombreArticulo = NombreArticulo;
             cantidad = Cantidad;
             unidadMedida = UnidadMedida;
             ajusteInventarioDescripcion = AjusteInventarioDescripcion;
             efectoInventario = EfectoInventario;
             fechaRegistroTrazabilidad = FechaRegistroTrazabilidad;
             descripcionUbicacion = DescripcionUbicacion;
             nombreUsuario = NombreUsuario;
             lote = Lote;
     }*/



        /*



            //Original
            public e_Ajustes_Inventario_Articulo(
                 string idMovimiento,
                 string numeroMovimiento,
                 int idArticulo,
                 string idInternoSAP,
                 int cantidad,
                 string unidadMedida,
                 long idAjusteInventario,
                 string ajusteInventarioDescripcion,
                 long idSolicitudAjusteInventario,
                 DateTime fechaSolicitud,
                 DateTime fechaAprobado,
                 DateTime fechaRegistroTrazabilidad,
                 string idDestino,
                 string descripcionDestino,
                 int idUsuario,
                 string usuario,
                 string usuarioNombreCompleto,
                 int idEstado,
                 int idMetodoAccion,
                 string metodoAccionDetalle,
                 string lote
            )
            {
                this.idMovimiento = idMovimiento;
                this.numeroMovimiento = numeroMovimiento;
                this.idArticulo = idArticulo;
                this.idInternoSAP = idInternoSAP;
                this.cantidad = cantidad;
                this.unidadMedida = unidadMedida;
                this.idAjusteInventario = idAjusteInventario;
                this.ajusteInventarioDescripcion = ajusteInventarioDescripcion;
                this.idSolicitudAjusteInventario = idSolicitudAjusteInventario;
                this.fechaSolicitud = fechaSolicitud;
                this.fechaAprobado = fechaAprobado;
                this.fechaRegistroTrazabilidad = fechaRegistroTrazabilidad;
                this.idDestino = idDestino;
                this.descripcionDestino = descripcionDestino;
                this.idUsuario = idUsuario;
                this.usuario = usuario;
                this.usuarioNombreCompleto = usuarioNombreCompleto;
                this.idEstado = idEstado;
                this.idMetodoAccion = idMetodoAccion;
                this.metodoAccionDetalle = metodoAccionDetalle;
                this.lote = lote;
            }


            //Para implementar el DISTINCT EN LA CONSULTA
            public e_Ajustes_Inventario_Articulo
            (
                 string idMovimiento,
                 string numeroMovimiento,
                 int idArticulo,
                 string idInternoSAP,
                 string nombreArticulo,
                 decimal cantidad,
                 string unidadMedida,
                 string ajusteInventarioDescripcion,
                 DateTime fechaRegistroTrazabilidad,
                 string idDestino,
                 string descripcionDestino,
                 int idUsuario,
                 string usuario,
                 string usuarioNombreCompleto,
                 int idEstado,
                 int idMetodoAccion,
                 string metodoAccionDetalle,
                 string lote
            )
            {
                this.idMovimiento = idMovimiento;
                this.numeroMovimiento = numeroMovimiento;
                this.idArticulo = idArticulo;
                this.idInternoSAP = idInternoSAP;
                this.nombreArticulo = nombreArticulo;
                this.cantidad = cantidad;
                this.unidadMedida = unidadMedida;
                this.ajusteInventarioDescripcion = ajusteInventarioDescripcion;
                this.fechaRegistroTrazabilidad = fechaRegistroTrazabilidad;
                this.idDestino = idDestino;
                this.descripcionDestino = descripcionDestino;
                this.idUsuario = idUsuario;
                this.usuario = usuario;
                this.usuarioNombreCompleto = usuarioNombreCompleto;
                this.idEstado = idEstado;
                this.idMetodoAccion = idMetodoAccion;
                this.metodoAccionDetalle = metodoAccionDetalle;
                this.lote = lote;
            }

         */
    

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes.Kardex
{
    [Serializable]
    public class e_Ajustes_Inventario_Articulo
    {        
        private string idMovimiento;
        private string numeroMovimiento;
        private int idArticulo;
        private string idInternoSAP;
        private string nombreArticulo;
        private decimal cantidad;
        private string unidadMedida;
        private long idAjusteInventario;
        private string ajusteInventarioDescripcion;
        private long idSolicitudAjusteInventario;
        private DateTime fechaSolicitud;
        private DateTime fechaAprobado;
        private DateTime fechaRegistroTrazabilidad;
        private string idDestino;
        private string descripcionDestino;
        private int idUsuario;
        private string usuario;
        private string usuarioNombreCompleto;
        private int idEstado;
        private int idMetodoAccion;
        private string metodoAccionDetalle;
        private string lote;

        //Para exportar a excel las fechas 
        private string fechaSolicitudExport;
        private string fechaAprobadoExport;
        private string fechaRegistroTrazabilidadExport;



        public e_Ajustes_Inventario_Articulo()
        {

        }


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
        public string IdMovimiento
        {
            get { return idMovimiento; }
            set { idMovimiento = value; }
        }


        public string NumeroMovimiento
        {
            get { return numeroMovimiento; }
            set { numeroMovimiento = value; }
        }

        public int IdArticulo
        {
            get { return idArticulo; }
            set { idArticulo = value; }
        }
        public string IdInternoSAP
        {
            get { return idInternoSAP; }
            set { idInternoSAP = value; }
        }
        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        public string UnidadMedida
        {
            get { return unidadMedida; }
            set { unidadMedida = value; }
        }
        public long IdAjusteInventario
        {
            get { return idAjusteInventario; }
            set { idAjusteInventario = value; }
        }
        public string AjusteInventarioDescripcion
        {
            get { return ajusteInventarioDescripcion; }
            set { ajusteInventarioDescripcion = value; }
        }
        public DateTime FechaSolicitud
        {
            get { return fechaSolicitud; }
            set { fechaSolicitud = value; }
        }
        public DateTime FechaAprobado
        {
            get { return fechaAprobado; }
            set { fechaAprobado = value; }
        }
        public DateTime FechaRegistroTrazabilidad
        {
            get { return fechaRegistroTrazabilidad; }
            set { fechaRegistroTrazabilidad = value; }
        }
        public string IdDestino
        {
            get { return idDestino; }
            set { idDestino = value; }
        }
        public string DescripcionDestino
        {
            get { return descripcionDestino; }
            set { descripcionDestino = value; }
        }
        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }
        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }
        public string UsuarioNombreCompleto
        {
            get { return usuarioNombreCompleto; }
            set { usuarioNombreCompleto = value; }
        }

        public int IdEstado 
        {
            get { return idEstado; }
            set { idEstado = value; }
        }
        public int IdMetodoAccion
        {
            get { return idMetodoAccion; }
            set { idMetodoAccion = value; }
        }
        public string MetodoAccionDetalle 
        {
            get { return metodoAccionDetalle; }
            set { metodoAccionDetalle = value; }
        }
        public string Lote 
        {
            get { return lote; }
            set { lote = value; }
        }
        public string NombreArticulo 
        {
            get { return nombreArticulo; }
            set { nombreArticulo = value; }
        }

        //Parseo para exportar excel fechas 
        public string FechaSolicitudExport
        {
            get { return fechaSolicitud.ToShortDateString(); }
        }
        public string FechaAprobadoExport
        {
            get { return fechaAprobado.ToShortDateString(); }
        }

        public string FechaRegistroTrazabilidadExport
        {
            get { return fechaRegistroTrazabilidad.ToShortDateString(); }
        }
    }

























    public class e_Kardex_Macro
    {
        private string idInternoSAP;
        private string nombreArticulo;
        private Single cantidad;
        private string unidadMedida;
        private string detalleMovimiento;
        private string efectoInventario;
        private DateTime fechaRegistro;


        //Para exportar a excel las fechas 
        private string fechaSolicitudExport;
        private string fechaAprobadoExport;
        private string fechaRegistroExport;



        public e_Kardex_Macro()
        {

        }

        //Original
        public e_Kardex_Macro(
                         string idInternoSAP,
                         Single cantidad,
                         string unidadMedida,
                         string detalleMovimiento,
                         string efectoInventario,
                         DateTime FechaRegistro
                             )
        {
            this.idInternoSAP = idInternoSAP;
            this.cantidad = cantidad;
            this.unidadMedida = unidadMedida;
            this.DetalleMovimiento = detalleMovimiento;
            this.EfectoInventario = efectoInventario;
            this.fechaRegistro = FechaRegistro;
        }

        public string IdInternoSAP
        {
            get { return idInternoSAP; }
            set { idInternoSAP = value; }
        }

        public Single Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public string UnidadMedida
        {
            get { return unidadMedida; }
            set { unidadMedida = value; }
        }

        public string DetalleMovimiento
        {
            get { return detalleMovimiento; }
            set { detalleMovimiento = value; }
        }

        public string EfectoInventario
        {
            get { return efectoInventario; }
            set { efectoInventario = value; }
        }

        public DateTime FechaRegistro
        {
            get { return fechaRegistro; }
            set { fechaRegistro = value; }
        }


    }

}

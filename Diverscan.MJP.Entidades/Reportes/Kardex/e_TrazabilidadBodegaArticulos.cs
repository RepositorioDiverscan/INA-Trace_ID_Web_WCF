using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes.Kardex
{
    [Serializable]
    public class e_TrazabilidadBodegaArticulos
    {
        private int idArticulo { get; set; }
        private string lote{ get; set; }
        private string idUbicacion{ get; set; }
        private int idEstado{ get; set; }
        private string estadoDescripcion{ get; set; }
        private DateTime fechaRegistroDate{ get; set; }
        private DateTime fechaVencimiento{ get; set; }
        private decimal cantidad{ get; set; }
        private string etiquetaUbicacion{ get; set; }
        private string unidadMedida{ get; set; }       

        private string fechaRegistroExport;
        private string fechaVencimientoExport;

        private string nombreUsuario { get; set; }
        private string detalleMovimiento{ get; set; }
        private string idMaestroOC{ get; set; }
        private string numeroOC{ get; set; }

        public e_TrazabilidadBodegaArticulos() { }

        public e_TrazabilidadBodegaArticulos(
            int pIdArticulo,
            string pLote,
            DateTime pfechaVencimiento,
            string pIdUbicacion,
            int pIdEstado,
            string pEstadoDescripcion,
            DateTime pFechaRegistroDate,
            decimal pCantidad,
            string pEtiquetaUbicacion,
            string pUnidadMedida,
            string pNombreUsuario,
            string pDetalleMovimiento,
            string pIdMaestroOC,
            string pNumeroOC)
        {
            idEstado = pIdArticulo;
            lote = pLote;
            FechaVencimiento = pfechaVencimiento;
            idUbicacion = pIdUbicacion;
            idEstado = pIdEstado;
            estadoDescripcion = pEstadoDescripcion;
            fechaRegistroDate = pFechaRegistroDate;
            cantidad = pCantidad;
            etiquetaUbicacion = pEtiquetaUbicacion;
            unidadMedida = pUnidadMedida;
            nombreUsuario = pNombreUsuario;
            detalleMovimiento = pDetalleMovimiento;
            IdMaestroOC = pIdMaestroOC;
            NumeroOC = pNumeroOC;

        }

        public int IdArticulo
        {
            get { return idArticulo; }
            set { idArticulo = value; }
        }
        public string Lote
        {
            get { return lote; }
            set { lote = value; }
        }
        public string IdUbicacion
        {
            get { return idUbicacion; }
            set { idUbicacion = value; }
        }
        public int IdEstado
        {
            get { return idEstado; }
            set { idEstado = value; }
        }
        public string EstadoDescripcion
        {
            get { return estadoDescripcion; }
            set { estadoDescripcion = value; }
        }
        public DateTime FechaRegistroDate
        {
            get { return fechaRegistroDate; }
            set { fechaRegistroDate = value; }
        }
        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        public string EtiquetaUbicacion
        {
            get { return etiquetaUbicacion; }
            set { etiquetaUbicacion = value; }
        }


        public DateTime FechaVencimiento
        {
            get { return fechaVencimiento; }
            set { fechaVencimiento = value; }
        }


        //Para poder retornar fechas en formato date al reporte
        public string FechaRegistroExport
        {
            get
            {
                return fechaRegistroDate.ToShortDateString();
            }
        }

        public string FechaVencimientoExport
        {
            get
            {
                return fechaVencimiento.ToShortDateString();
            }
        }

        public string UnidadMedida
        {
            get { return unidadMedida; }
            set { unidadMedida = value; }
        }

        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set { nombreUsuario = value; }
        }

        public string DetalleMovimiento
        {

            get { return detalleMovimiento; }
            set { detalleMovimiento = value; }
        }

        public string IdMaestroOC
        {
            get { return idMaestroOC; }
            set { idMaestroOC = value; }
        }
        public string NumeroOC
        {
            get { return numeroOC; }
            set { numeroOC = value; }
        }
    }
}

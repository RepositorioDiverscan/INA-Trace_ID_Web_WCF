using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes.Kardex
{
    [Serializable]
    public class e_ArticulosDespachadosPorLoteRangoFecha
    {
        private string idInternoSAPSolicitud;
        private long idSolicitudTID;
        private string idArticulo;
        private string idInterno;
        private string lote;
        private DateTime fechaVencimiento;
        private decimal cantidadDespachada;
        private decimal cantidadSolicitada;
        private string unidadMedida;
        private int cantidadUnidadAlistoSolicitud;
        private string etiquetaUbicacionDespacho;
        private string destinoSolicitud;
        private DateTime fechaDespacho;

        private string fechaDespachoExport;
        private string fechaVencimientoExport;

        public e_ArticulosDespachadosPorLoteRangoFecha()
        {

        }

        public e_ArticulosDespachadosPorLoteRangoFecha(
            string pIdInternoSAPSolicitud,
            long pidSolicitudTID,
            string pIdArticulo,
            string pIdInterno,
            string pLote,
            DateTime pFechaVencimiento,
            decimal pCantidadDespachada,
            decimal pCantidadSolicitada,
            string pUnidadMedida,
            int pCantidadUnidadAlisto,
            string pEtiquetaUbicacionDespacho,
            string pDestinoSolicitud,
            DateTime pFechaDespacho)
        {
            idInternoSAPSolicitud = pIdInternoSAPSolicitud;
            idSolicitudTID = pidSolicitudTID;
            idArticulo = pIdArticulo;
            idInterno = pIdInterno;
            lote = pLote;
            FechaVencimiento = pFechaVencimiento;
            cantidadDespachada = pCantidadDespachada;
            cantidadSolicitada = pCantidadSolicitada;
            unidadMedida = pUnidadMedida;
            CantidadUnidadAlistoSolicitud = pCantidadUnidadAlisto;
            etiquetaUbicacionDespacho = pEtiquetaUbicacionDespacho;
            destinoSolicitud = pDestinoSolicitud;
            fechaDespacho = pFechaDespacho;
        }


        public string IdInterno
        {
            get { return idInterno; }
            set { idInterno = value; }
        }

        public string Lote
        {
            get { return lote; }
            set { lote = value; }
        }
        public decimal CantidadDespachada
        {
            get { return cantidadDespachada; }
            set { cantidadDespachada = value; }
        }
        public decimal CantidadSolicitada
        {
            get { return cantidadSolicitada; }
            set { cantidadSolicitada = value; }
        }

        public string EtiquetaUbicacionDespacho
        {
            get { return etiquetaUbicacionDespacho; }
            set { etiquetaUbicacionDespacho = value; }
        }
        public string DestinoSolicitud
        {
            get { return destinoSolicitud; }
            set { destinoSolicitud = value; }
        }

        public DateTime FechaDespacho
        {
            get { return fechaDespacho; }
            set { fechaDespacho = value; }
        }

        public string FechaDespachoExport
        {
            get
            {
                return fechaDespacho.ToShortDateString();
            }
        }

        public int CantidadUnidadAlistoSolicitud
        {
            get { return cantidadUnidadAlistoSolicitud; }
            set { cantidadUnidadAlistoSolicitud = value; }
        }

        public string FechaVencimientoExport
        {
            get
            {
                return fechaVencimiento.ToShortDateString();
            }
        }

        public DateTime FechaVencimiento
        {

            get { return fechaVencimiento; }
            set { fechaVencimiento = value; }
        }

        public string UnidadMedida
        {
            get { return unidadMedida; }
            set { unidadMedida = value; }
        }

        public string IdInternoSAPSolicitud 
        {
            get { return idInternoSAPSolicitud; }
            set { idInternoSAPSolicitud = value; }
        }
        public long IdSolicitudTID
        {
            get { return idSolicitudTID; }
            set { idSolicitudTID = value; }
        }

    }
}

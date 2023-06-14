using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Traslados
{
    [DataContract]
    public class ETraslado
    {
        [DataMember]
        public int IdArticulo { get; set; }

        [DataMember]
        public string Lote { get; set; }

        [DataMember]
        public DateTime FechaVencimiento { get; set; }

        [DataMember]
        public int IdUbicacionOrigen { get; set; }

        [DataMember]
        public int Cantidad { get; set; }

        [DataMember]
        public int IdUsuario { get; set; }

        [DataMember]
        public int IdMetodoAccionSalida { get; set; }

        [DataMember]
        public string ConTrazabilidad { get; set; }

        [DataMember]
        public double CANTIDADGTIN14 { get; set; }

        [DataMember]
        public string NOMBREARTICULO { get; set; }

        [DataMember]
        public string NOMBREGTIN14 { get; set; }

        [DataMember]
        public string NOMBREGTIN13 { get; set; }

        [DataMember]
        public string FechaVencimientoAndroid { get; set; }
    }
}

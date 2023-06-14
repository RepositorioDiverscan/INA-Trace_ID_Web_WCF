using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.Invertario
{
    [DataContract]
    public class GS1Data
    {
        public GS1Data(string idArticulo, string nombreArticulo, string fechaVencimiento,
        string lote, string idZona, string cantidad, int peso, string idUbicacion)
        {
            IdArticulo = idArticulo;
            NombreArticulo = nombreArticulo;
            FechaVencimiento = fechaVencimiento;
            Lote = lote;
            IdZona = idZona;
            Cantidad = cantidad;
            Peso = peso;
            IdUbicacion = IdUbicacion;
        }

        [DataMember]
        public string IdArticulo { set; get; }
        [DataMember]
        public string NombreArticulo { set; get; }
        [DataMember]
        public string FechaVencimiento { set; get; }
        [DataMember]
        public string Lote { set; get; }
        [DataMember]
        public string IdZona { set; get; }
        [DataMember]
        public string Cantidad { set; get; }
        [DataMember]
        public int Peso { set; get; }
        [DataMember]
        public string IdUbicacion { set; get; }
    }
}

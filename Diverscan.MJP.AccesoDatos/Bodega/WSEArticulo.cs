using Diverscan.MJP.AccesoDatos.ModuloConsultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Bodega
{
    [DataContract]
    public class WSEArticulo
    {
        public WSEArticulo()
        {
        }

        public WSEArticulo(EArticulo eArticulo)
        {
            IdInterno = eArticulo.IdInterno;
            Nombre = eArticulo.Nombre;
            IdArticulo = eArticulo.IdArticulo;
            Cantidad = eArticulo.Cantidad;
            Lote = eArticulo.Lote;
            Descripcion = eArticulo.Descripcion;
            FechaVencimiento = eArticulo.FechaVencimiento;
            FechaAndroid = eArticulo.FechaAndroid;
            FUTSAlida = eArticulo.FUTSAlida;
            ConTrazabilidad = eArticulo.ConTrazabilidad;
        }

        [DataMember]
        public string IdInterno { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public int IdArticulo { get; set; }       

        [DataMember]
        public double Cantidad { get; set; }
        [DataMember]
        public string Lote { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public DateTime FechaVencimiento { get; set; }
        [DataMember]
        public string FechaAndroid { get; set; }
        [DataMember]
        public DateTime FUTSAlida { get; set; }
        [DataMember]
        public bool ConTrazabilidad { get; set; }

    }
}

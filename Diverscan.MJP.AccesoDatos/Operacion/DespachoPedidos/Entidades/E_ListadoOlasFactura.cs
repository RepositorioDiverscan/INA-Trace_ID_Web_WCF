using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Operacion.DespachoPedidos.Entidades
{
    [Serializable]
    public class E_ListadoOlasFactura
    {
        public E_ListadoOlasFactura()
        {
        }
        public E_ListadoOlasFactura(int idRegistroOla, string fechaIngreso, string observacion, string ruta, string porcentajeAlisto, string facturado)
        {
            this.idRegistroOla = idRegistroOla;
            FechaIngreso = fechaIngreso;
            Observacion = observacion;
            Ruta = ruta;
            PorcentajeAlistado = porcentajeAlisto;
            Facturado = facturado;
        }

        public int idRegistroOla { get; set; }
        public string FechaIngreso { get; set; }
        public string Observacion { get; set; }
        public string Ruta { get; set; }
        public string PorcentajeAlistado { get; set; }
        public string Facturado { get; set; }

    }
}


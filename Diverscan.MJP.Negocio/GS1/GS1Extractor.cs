using Diverscan.MJP.Entidades.Invertario;
using Diverscan.MJP.Negocio.GS1;
using Diverscan.MJP.Negocio.LogicaWMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diverscan.MJP.Negocio.GS1
{
    public class GS1Extractor
    {
        public static GS1Data ExtraerGS1(string CodLeido, string idUsuario)
        {
            string[] Articulo =n_WMS.ObtenerIdArticuloNombreCodigoLeido_GS1128(CodLeido, idUsuario).Split(';');
            string idArticulo = Articulo[0];
            string nombreArticulo = Articulo[1];
            //int separador = CodLeido.IndexOf(';');
            //string CodGS1 = CodLeido.Substring(0, separador - 1);

            string fechaVencimiento = CargarEntidadesGS1.GS1128_DevolverFechaVencimiento(CodLeido);

            string lote ="";
            var gS1128_DevolveLote = CargarEntidadesGS1.GS1128_DevolveLote(CodLeido).Split(';');
            if(gS1128_DevolveLote.Length>0)
                lote = gS1128_DevolveLote[0];
            
            string idZona = "";
            string cantidad_s = CargarEntidadesGS1.GS1128_DevolverCantidad(CodLeido);
            Single cantidad = Single.Parse(cantidad_s);
            string idUbicacion = CargarEntidadesGS1.GS1128_DevolveridUbicacion(CodLeido);
            string peso = CargarEntidadesGS1.GS1128_DevolverPeso(CodLeido);
            var pesoDecimal = decimal.Parse(peso);
            int pesoGramos = (int)(pesoDecimal * 1000);
            if (pesoGramos == 0)
                pesoGramos = (int)(Convert.ToInt32(cantidad) * 1000);
           
            return new GS1Data(idArticulo, nombreArticulo, fechaVencimiento, lote, idZona, cantidad_s, pesoGramos, idUbicacion);
        
        }        
    }
}
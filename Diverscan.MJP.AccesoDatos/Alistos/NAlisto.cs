using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Alistos
{
    public class NAlisto
    {
        public List<EEnlist> ObtenerProductoPorGTIN(string GTIN)
        {
            EstadoAlistos dAConsultas = new EstadoAlistos();

            return dAConsultas.ProductDetailFromGtin(GTIN);
        }

    }
}

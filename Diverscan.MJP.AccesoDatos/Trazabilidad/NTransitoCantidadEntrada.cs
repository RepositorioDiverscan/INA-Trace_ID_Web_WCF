using CodeUtilities;
using Diverscan.MJP.GestorImpresiones.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.Trazabilidad
{
    public class NTransitoCantidadEntrada
    {
        //Método para obtener la cantidad entrada de un artículo por bodega
        public int ObtenerCantidadTransitoEntrada(int idArticulo, int idBodega)
        {
            DATransitoCantidadEntrada dATrazabilidadCantidadEntrada = new DATransitoCantidadEntrada();
            return dATrazabilidadCantidadEntrada.ObtenerCantidadTransitoEntrada(idArticulo, idBodega);
        }
    }
}

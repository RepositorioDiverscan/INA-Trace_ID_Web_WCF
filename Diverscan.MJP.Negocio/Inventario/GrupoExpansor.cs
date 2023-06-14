using Diverscan.MJP.Entidades.Invertario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Inventario
{
    public class GrupoExpansor
    {
        public static List<ICantidadPorUbicacionArticuloRecord> ExpandirGrupo(List<ICantidadPorUbicacionArticuloRecord> articulosAgrupados)
        {
            List<ICantidadPorUbicacionArticuloRecord> articulosSeparados = new List<ICantidadPorUbicacionArticuloRecord>();
            foreach (var articulo in articulosAgrupados)
            {
                if (!articulo.EsGranel)
                {
                    for (int x = 0; x < articulo.Cantidad; x++)
                    {
                        var newRecord = new CantidadPorUbicacionArticuloRecord(articulo.IdUbicacion, articulo.Etiqueta, articulo.IdArticulo, 1, articulo.FechaVencimiento,
                            articulo.Lote, articulo.UnidadMedida, articulo.EsGranel, articulo.NombreArticulo, articulo.UnidadInventario);
                        articulosSeparados.Add(newRecord);
                    }
                }
                else
                    articulosSeparados.Add(articulo);
            }
            return articulosSeparados;
        }
    }
}

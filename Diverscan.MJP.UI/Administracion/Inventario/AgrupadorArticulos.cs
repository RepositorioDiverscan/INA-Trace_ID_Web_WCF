using Diverscan.MJP.Entidades.Invertario;
using System.Collections.Generic;
using System.Linq;

namespace Diverscan.MJP.UI.Administracion.Inventario
{
    public class AgrupadorArticulos
    {

        public static List<IArticuloInventario> AgruparArticulosPorUbicacion<T>(List<T> articulos) where T :IArticuloInventario
        {
            T[] articulosToCopy = new T[articulos.Count];
            articulos.CopyTo(articulosToCopy);
            var articulosCopiados = articulosToCopy.ToList();

            List<IArticuloInventario> newList = new List<IArticuloInventario>();

            while (articulosCopiados.Count > 0)
            {
                var idUbicacionToCompare = articulosCopiados[0].IdUbicacion;
                var idArticuloToCompare = articulosCopiados[0].IdArticulo;
                var loteToCompare = articulosCopiados[0].Lote;
                var fvToCompare = articulosCopiados[0].FechaVencimiento;
                var recordToGroup = new ArticuloInventario(articulosCopiados[0].IdUbicacion, articulosCopiados[0].Etiqueta, articulosCopiados[0].IdArticulo, articulosCopiados[0].Cantidad,
                    articulosCopiados[0].FechaVencimiento, articulosCopiados[0].Lote, articulosCopiados[0].UnidadMedida, articulosCopiados[0].EsGranel, articulosCopiados[0].NombreArticulo,
                     articulosCopiados[0].UnidadInventario);
                recordToGroup.IdInterno = articulosCopiados[0].IdInterno;

                articulosCopiados.RemoveAt(0);
                for (int i = 0; i < articulosCopiados.Count; i++)
                {
                    if (idUbicacionToCompare == articulosCopiados[i].IdUbicacion 
                        && idArticuloToCompare == articulosCopiados[i].IdArticulo
                        && loteToCompare == articulosCopiados[i].Lote
                        && fvToCompare == articulosCopiados[i].FechaVencimiento)
                    {
                        recordToGroup.Cantidad += articulosCopiados[i].Cantidad;
                        articulosCopiados.RemoveAt(i);
                        i--;
                    }
                }
                newList.Add(recordToGroup);
            }
            return newList;
        }

        public static List<BodegaFisica_SistemaRecord> ObtenerBodegaFisica_SistemaRecord(List<IArticuloInventario> sistemaAgrupados, List<IArticuloInventario> tomafisicaAgrupados)
        {
            List<BodegaFisica_SistemaRecord> existenciaList = new List<BodegaFisica_SistemaRecord>();

            for (int i = 0; i < sistemaAgrupados.Count; i++)
            {
                var tomaFisicaRecord = tomafisicaAgrupados.FirstOrDefault(x => x.IdUbicacion == sistemaAgrupados[i].IdUbicacion &&
                    x.IdArticulo == sistemaAgrupados[i].IdArticulo &&
                    x.Lote == sistemaAgrupados[i].Lote &&
                    x.FechaVencimiento == sistemaAgrupados[i].FechaVencimiento);               
                var cantidadTomaFisica = 0;
                if (tomaFisicaRecord != null)
                {
                    cantidadTomaFisica = tomaFisicaRecord.Cantidad;
                    tomafisicaAgrupados.Remove(tomaFisicaRecord);
                }
                var cantidadSistema = sistemaAgrupados[i].Cantidad;
                if (cantidadSistema == 0 && cantidadTomaFisica == 0)
                    continue;
                BodegaFisica_SistemaRecord bodegaFisica_SistemaRecord = new BodegaFisica_SistemaRecord(
                    sistemaAgrupados[i].IdUbicacion, sistemaAgrupados[i].Etiqueta, cantidadTomaFisica, cantidadSistema,
                    sistemaAgrupados[i].UnidadMedida, sistemaAgrupados[i].EsGranel, sistemaAgrupados[i].NombreArticulo,sistemaAgrupados[i].UnidadInventario,
                    sistemaAgrupados[i].Lote, sistemaAgrupados[i].FechaVencimiento, sistemaAgrupados[i].IdArticulo);
                bodegaFisica_SistemaRecord.IdInterno = sistemaAgrupados[i].IdInterno;
                existenciaList.Add(bodegaFisica_SistemaRecord);
            }

            for (int i = 0; i < tomafisicaAgrupados.Count; i++)
            {
                BodegaFisica_SistemaRecord bodegaFisica_SistemaRecord = new BodegaFisica_SistemaRecord(
                    tomafisicaAgrupados[i].IdUbicacion, tomafisicaAgrupados[i].Etiqueta, tomafisicaAgrupados[i].Cantidad, 0,
                    tomafisicaAgrupados[i].UnidadMedida, tomafisicaAgrupados[i].EsGranel, tomafisicaAgrupados[i].NombreArticulo, tomafisicaAgrupados[i].UnidadInventario,
                    tomafisicaAgrupados[i].Lote, tomafisicaAgrupados[i].FechaVencimiento, tomafisicaAgrupados[i].IdArticulo);
                bodegaFisica_SistemaRecord.IdInterno = tomafisicaAgrupados[i].IdInterno;
                existenciaList.Add(bodegaFisica_SistemaRecord);
            }

            return existenciaList;
        }
    }
}
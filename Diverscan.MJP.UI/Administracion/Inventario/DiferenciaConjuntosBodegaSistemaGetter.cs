using Diverscan.MJP.Entidades.Invertario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diverscan.MJP.UI.Administracion.Inventario
{
    public class DiferenciaConjuntosBodegaSistemaGetter
    {
        //AjusteSalida articulosSistema tiene que estar en 0
        public static List<CantidadPorUbicacionArticuloRecord> ObtenerDiferenciaBodegaVO(List<CantidadPorUbicacionArticuloRecord> articulosBodega, List<ArticulosDisponibles> articulosSistema)
        {
            for (int i = 0; i < articulosBodega.Count; i++)
            {
                for (int x = 0; x < articulosSistema.Count; x++)
                {
                    if (articulosSistema[x].Lote == articulosBodega[i].Lote &&
                   articulosSistema[x].FechaVencimiento == articulosBodega[i].FechaVencimiento)
                    {
                        articulosBodega[i].Cantidad -= articulosSistema[x].Cantidad;
                        articulosSistema.RemoveAt(x);
                        x--;
                        if (articulosBodega[i].Cantidad == 0)
                        {
                            articulosBodega.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
            }
            var cantidadSinCoincidencia = articulosSistema.Count;
            if (cantidadSinCoincidencia > 0)
            {
                for (int i = 0; i < articulosBodega.Count; i++)
                {
                    var dif = articulosBodega[i].Cantidad - cantidadSinCoincidencia;
                    if (dif > 0)
                        articulosBodega[i].Cantidad -= cantidadSinCoincidencia;
                    else
                    {
                        cantidadSinCoincidencia = Math.Abs(dif);
                        articulosBodega.RemoveAt(i);
                        i--;
                    }
                }
            }
            return articulosBodega;
        }

        //AjusteSalida articulosSistema tiene que estar en 0
        public static List<CantidadPorUbicacionArticuloRecord> ObtenerDiferenciaBodegaV1(List<CantidadPorUbicacionArticuloRecord> articulosBodega, List<ArticulosDisponibles> articulosSistema, out bool tieneIntegridad)
        {
            tieneIntegridad = true;
            for (int i = 0; i < articulosBodega.Count; i++)
            {
                for (int x = 0; x < articulosSistema.Count; x++)
                {
                    if (articulosSistema[x].IdArticulo == articulosBodega[i].IdArticulo &&
                        articulosSistema[x].Lote == articulosBodega[i].Lote &&
                   articulosSistema[x].FechaVencimiento == articulosBodega[i].FechaVencimiento)
                    {
                        articulosBodega[i].Cantidad -= articulosSistema[x].Cantidad;
                        articulosSistema.RemoveAt(x);
                        x--;
                        if (articulosBodega[i].Cantidad == 0)
                        {
                            articulosBodega.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
            }
            if (articulosSistema.Count != 0)
            {
                tieneIntegridad = false;
            }
            return articulosBodega;
        }

        //AjusteSalida articulosSistema tiene que estar en 0
        public static List<ICantidadPorUbicacionArticuloRecord> ObtenerDiferenciaBodega(List<ICantidadPorUbicacionArticuloRecord> articulosBodega, List<ArticulosDisponibles> articulosSistema, out bool tieneIntegridad)
        {
            var articulosSistemaOrdered = articulosSistema.OrderByDescending(p => p.Cantidad).ToList();
            tieneIntegridad = true;
            for (int i = 0; i < articulosBodega.Count; i++)
            {
                for (int x = 0; x < articulosSistemaOrdered.Count; x++)
                {
                    if (articulosSistemaOrdered[x].IdArticulo == articulosBodega[i].IdArticulo &&
                        articulosSistemaOrdered[x].Lote == articulosBodega[i].Lote &&
                   articulosSistemaOrdered[x].FechaVencimiento == articulosBodega[i].FechaVencimiento)
                    {
                        articulosBodega[i].Cantidad -= articulosSistemaOrdered[x].Cantidad;
                        articulosSistemaOrdered.RemoveAt(x);
                        x--;
                        if (articulosBodega[i].Cantidad == 0)
                        {
                            articulosBodega.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
            }
            if (articulosSistemaOrdered.Count != 0)
            {
                tieneIntegridad = false;
            }
            return articulosBodega;
        }

        //AjusteSalida articulosBodega tiene que estar en 0
        public static List<ArticulosDisponibles> ObtenerDiferenciaSistemaVO(List<CantidadPorUbicacionArticuloRecord> articulosBodega, List<ArticulosDisponibles> articulosSistema, out bool tieneIntegridad)
        {
            tieneIntegridad = true;
            int cantidadFantanteNOEncontrada = 0;
            for (int i = 0; i < articulosBodega.Count; i++)
            {
                int cantidadBodega = articulosBodega[i].Cantidad;
                for (int x = 0; x < articulosSistema.Count && cantidadBodega > 0; x++)
                {
                    if (articulosSistema[x].IdArticulo == articulosBodega[i].IdArticulo &&
                        articulosSistema[x].Lote == articulosBodega[i].Lote &&
                        articulosSistema[x].FechaVencimiento == articulosBodega[i].FechaVencimiento)
                    {
                        cantidadBodega -= articulosSistema[x].Cantidad;
                        articulosSistema.RemoveAt(x);
                        x--;
                    }
                }
                cantidadFantanteNOEncontrada += cantidadBodega;
            }

            if (cantidadFantanteNOEncontrada > 0)
                tieneIntegridad = false;

            return articulosSistema;
        }

        //AjusteSalida articulosBodega tiene que estar en 0
        public static List<ArticulosDisponibles> ObtenerDiferenciaSistema(List<ICantidadPorUbicacionArticuloRecord> articulosBodega, List<ArticulosDisponibles> articulosSistema, out bool tieneIntegridad)
        {
            var articulosSistemaOrdered = articulosSistema.OrderByDescending(p => p.Cantidad).ToList();
            tieneIntegridad = true;
            for (int i = 0; i < articulosBodega.Count; i++)
            {
                int cantidadBodega = articulosBodega[i].Cantidad;
                List<int> indexAceptados = new List<int>();
                int index = 0;
                while (cantidadBodega != 0)
                {
                    indexAceptados = new List<int>();
                    cantidadBodega = articulosBodega[i].Cantidad;
                    for (int p = index; p < articulosSistemaOrdered.Count && cantidadBodega > 0; p++)
                    {
                        if (articulosSistemaOrdered[p].IdArticulo == articulosBodega[i].IdArticulo &&
                            articulosSistemaOrdered[p].Lote == articulosBodega[i].Lote &&
                            articulosSistemaOrdered[p].FechaVencimiento == articulosBodega[i].FechaVencimiento)
                        {
                            if (articulosSistemaOrdered[p].Cantidad <= cantidadBodega)
                            {
                                indexAceptados.Add(p);
                                cantidadBodega -= articulosSistemaOrdered[p].Cantidad;
                            }
                        }
                    }
                    index++;
                }
                for (int p = 0; p < indexAceptados.Count; p++)
                {
                    articulosSistemaOrdered.RemoveAt(indexAceptados[p]);
                    p--;
                }
            }

            if (articulosBodega.Count > 0)
                tieneIntegridad = false;

            return articulosSistemaOrdered;
        }





        //La lista articulosA siempre tiene que tener lo que tiene la lista articulosB
        public static List<M> Validar_ObtenerDiferenciaVO<T, M>(List<T> articulosA, List<M> articulosB, out bool tieneIntegridad)
            where T : IArticuloInventario
            where M : IArticuloInventario
        {

            tieneIntegridad = true;
            M[] articulosBToCopy = new M[articulosB.Count];
            articulosB.CopyTo(articulosBToCopy); //Se copia articulos B 
            var articulosBCopiados = articulosBToCopy.ToList();
            //if (articulosB.Count == 0)
            //    return articulosBCopiados;                        
            for (int i = 0; i < articulosA.Count; i++)
            {
                var index = articulosBCopiados.FindIndex
                    (
                    x => x.IdArticulo == articulosA[i].IdArticulo &&
                    x.Lote == articulosA[i].Lote &&
                    x.FechaVencimiento == articulosA[i].FechaVencimiento &&
                    x.Cantidad == articulosA[i].Cantidad
                    );
                if (index > -1)
                {
                    articulosBCopiados.RemoveAt(index);
                }
                else
                {
                    tieneIntegridad = false;
                    break;
                }
            }
            return articulosBCopiados;
        }




        //La lista articulosA siempre tiene que tener lo que tiene la lista articulosB [VERSION 2]
        public static List<M> Validar_ObtenerDiferencia_V2<T, M>(List<T> articulosA, List<M> articulosB, out bool tieneIntegridad)
            where T : IArticuloInventario
            where M : IArticuloInventario
        {

            tieneIntegridad = true;
            M[] articulosBToCopy = new M[articulosB.Count];
            articulosB.CopyTo(articulosBToCopy); //Se copia articulos B 
            var articulosBCopiados = articulosBToCopy.ToList();
            //if (articulosB.Count == 0)
            //    return articulosBCopiados;                        
            for (int i = 0; i < articulosA.Count; i++)
            {

                //[1].Busca el artículoA en los articulosB filtrando por IdArticulo, Lote, FechaVencimiento.
                //    Si el artículo no es encontrado retornará -1, sino indicará el idex en la lista del artículo
                var index = articulosBCopiados.FindIndex
                    (
                    x => x.IdArticulo == articulosA[i].IdArticulo
                    && x.Lote == articulosA[i].Lote
                    && x.FechaVencimiento == articulosA[i].FechaVencimiento
                    //&& x.Cantidad == articulosA[i].Cantidad
                    );
                if (index > -1)
                {

                    ////[1].Si la cantidad articulosA > articulosB el resultado en ese item sera la diferencia entre la cantidad 
                    ////Ejemplo:  articulosA[i].Cantidad = 40 
                    ////          articulosB[i].Cantidad = 30
                    ////          Diferencia = -10 "Hay 10 de menos en artículosB"
                    if (articulosA[i].Cantidad > articulosBCopiados[index].Cantidad)
                    {
                        articulosBCopiados[index].Cantidad = (articulosB[index].Cantidad - articulosA[i].Cantidad);
                    }

                    ////[2].Si la cantidad articulosA < articulosB el resultado en ese item sera la diferencia entre la cantidad 
                    ////Ejemplo:  articulosA[i].Cantidad = 5 
                    ////          articulosB[i].Cantidad = 10
                    ////          Diferencia = 5 "Hay 5 de más en artículosB"
                    else if (articulosA[i].Cantidad < articulosBCopiados[index].Cantidad)
                    {
                        articulosBCopiados[index].Cantidad = (articulosB[index].Cantidad - articulosA[i].Cantidad);
                    }
                    ////[3].Si la cantidad cantidad articulosA == articulosB  elimina el item de la lista dado que no hay diferencia
                    else
                    {
                        articulosBCopiados.RemoveAt(index);
                    }

                }
                else //La integridad se rompe únicamente cuando se quiere comparar un articulo en articulosA que no estén e articulosB
                {
                    tieneIntegridad = false;
                    break;
                }
            }

            return articulosBCopiados;
        }


        //La lista articulosA siempre tiene que tener lo que tiene la lista articulosB
        public static List<M> Validar_ObtenerDiferencia<T, M>(List<T> articulosA, List<M> articulosB, out bool tieneIntegridad)
            where T : IArticuloInventario
            where M : IArticuloInventario
        {

            tieneIntegridad = true;
            M[] articulosBToCopy = new M[articulosB.Count];
            articulosB.CopyTo(articulosBToCopy); //Se copia articulos B 
            var articulosBCopiados = articulosBToCopy.ToList();
            //if (articulosB.Count == 0)
            //    return articulosBCopiados;                        
            for (int i = 0; i < articulosA.Count; i++)
            {
                int cantidadRestante = articulosA[i].Cantidad;
                for (var m = 0; m < articulosBCopiados.Count; m++)
                {
                    if (articulosBCopiados[m].IdArticulo == articulosA[i].IdArticulo &&
                        articulosBCopiados[m].Lote == articulosA[i].Lote &&
                        articulosBCopiados[m].FechaVencimiento == articulosA[i].FechaVencimiento)
                    {
                        if (cantidadRestante < articulosBCopiados[m].Cantidad)
                        {
                            articulosBCopiados[m].Cantidad -= cantidadRestante;
                            cantidadRestante = 0;
                            break;
                        }
                        else if (cantidadRestante == articulosBCopiados[m].Cantidad)
                        {
                            articulosBCopiados.RemoveAt(m);
                            cantidadRestante = 0;
                            break;
                        }
                        else if (cantidadRestante > articulosBCopiados[m].Cantidad)
                        {
                            cantidadRestante -= articulosBCopiados[m].Cantidad;
                            articulosBCopiados.RemoveAt(m);
                            m--;
                        }
                    }
                }
                //if(cantidadRestante>0)
                //{
                //    tieneIntegridad = false;
                //    break;
                //}              
            }
            return articulosBCopiados;
        }
    }
}
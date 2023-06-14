using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.Reportes.Articulos;
using System;
using System.Collections.Generic;

namespace Diverscan.MJP.Negocio.Reportes
{
    public class n_ArticulosDisponiblesBodega
    {
        //metodo para obtener articulos dsponibles en bodega
        public List<e_ArticulosDisponiblesBodega> ConsultarArticulosDisponiblesBodegas(int idBodega)
        {
            try
            {
                da_ArticulosDisponiblesBodega articulos = new da_ArticulosDisponiblesBodega();
                return articulos.ObtenerArticulosDisponiblesBodega(idBodega);
            }
            catch(Exception e)
            {
                var p = e.Message;
                return null;
            }
        }

        //Metodo para obtener las bodegas 
        public List<EBodegas> CargarBodegas()
        {
            try
            {
               da_ArticulosDisponiblesBodega articulos= new da_ArticulosDisponiblesBodega();
                return articulos.CargarBodegas();
            }
            catch (Exception e)
            {
                var prueba = e.Message;
                return null;
            }
        }


    }
}

using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.MaestroArticulo;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Consultas
{
    public class DConsultarMaestroArticulo
    {
        //metodo para obtener el maestro articulo
        public List<EMaestroArticulo> ObtenerMaestroArticulo()
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_BuscarMaestroArticulo");

            //se guarda en una lista
            List<EMaestroArticulo> listaMaestr = new List<EMaestroArticulo>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        listaMaestr.Add(new EMaestroArticulo(reader));
                    }
                }
                
            }
            catch(Exception e)
            {
                var prueba = e.Message;
                return null;
            }

            return listaMaestr;

        }

        public List<EMaestroArticulo> ObtenerMaestroArticulosDisponiblesBodega()
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_BuscarMaestroArticulo");

            List<EMaestroArticulo> listaArticulos = new List<EMaestroArticulo>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {

                    while (reader.Read())
                    {
                        listaArticulos.Add(new EMaestroArticulo(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaArticulos;
        }

    }
}

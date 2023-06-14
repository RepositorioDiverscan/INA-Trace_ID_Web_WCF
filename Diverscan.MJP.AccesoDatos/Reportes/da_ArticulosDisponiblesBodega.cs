using Diverscan.MJP.Entidades;
using Diverscan.MJP.Entidades.Reportes.Articulos;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.Reportes
{

    public class da_ArticulosDisponiblesBodega
    {
        public List<e_ArticulosDisponiblesBodega> ObtenerArticulosDisponiblesBodega(int idBodega)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");           
            var dbCommand = dbTse.GetStoredProcCommand("SP_Obtener_Articulos_Disponibles_Bodega");
            dbTse.AddInParameter(dbCommand, "@idWarehouse", DbType.Int32, idBodega);

            List<e_ArticulosDisponiblesBodega> listaArticulos = new List<e_ArticulosDisponiblesBodega>();
            try
            {
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {

                    while (reader.Read())
                    {
                        listaArticulos.Add(new e_ArticulosDisponiblesBodega(reader));
                    }
                }      
            }
            catch (Exception ex)
            {
                throw ex;
            }        
            return listaArticulos;
        }
        public List<EBodegas> CargarBodegas()
        {
            //Configuracion de la BD y SP a ejecutar
            var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_GETBODEGA");

            //Lista que almacena la informacion
            List<EBodegas> devolucionBodegas = new List<EBodegas>();

            //Leer la informacion de la BD
            using (var reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    devolucionBodegas.Add(new EBodegas(reader));
                }
            }
            return devolucionBodegas;
        }

    }
}

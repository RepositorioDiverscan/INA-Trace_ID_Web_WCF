using Diverscan.MJP.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.AprobarSalida
{
    public class da_AprobarSalida
    {
        public  List<e_AprobarSalida> GetAprobarSalidas()
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("GetAprobarSalida");

            List<e_AprobarSalida> ListAprobarSalida = new List<e_AprobarSalida>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    long idMaestroSolicitud = long.Parse(reader["idMaestroSolicitud"].ToString());
                    string solicitud = reader["Solicitud"].ToString();
                    string idBodega = reader["idBodega"].ToString();
                    string bodega = reader["Bodega"].ToString();
                    string destino = reader["Destino"].ToString();
                    string idInterno = reader["IdInterno"].ToString();
                    string fecha = reader["Fecha"].ToString();

                    ListAprobarSalida.Add(new e_AprobarSalida(idMaestroSolicitud, solicitud, idBodega, bodega, destino,idInterno,fecha));
                }
            }
            return ListAprobarSalida;
        }
    }
}

using Diverscan.MJP.Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.AprobarDespacho
{
    public class da_AprobarDespacho
    {
        public List<e_AprobarDespacho> GetAprobarDespacho()
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("GetAprobarDespacho");

            List<e_AprobarDespacho> ListAprobarDespacho = new List<e_AprobarDespacho>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    //long idMaestroSolicitud = long.Parse(reader["idMaestroSolicitud"].ToString());
                    //string solicitud = reader["Solicitud"].ToString();
                    int idMaestroSolicitud = int.Parse(reader["idMaestroSolicitud"].ToString());
                    int idUsuario = int.Parse(reader["idUsuario"].ToString());
                    DateTime FechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
                    string Nombre = reader["Nombre"].ToString();
                    string Comentarios = reader["Comentarios"].ToString();
                    string IdCompania = reader["IdCompania"].ToString();
                    int idDestino = int.Parse(reader["idDestino"].ToString());
                    string idInterno = reader["idInterno"].ToString();
                    string idInternoSAP = reader["idInternoSAP"].ToString();
                    bool Procesada = bool.Parse(reader["Procesada"].ToString());
                    DateTime FechaProcesamiento = DateTime.Parse(reader["FechaProcesamiento"].ToString());
                    bool Activo = bool.Parse(reader["Activo"].ToString());
                    bool Sincronizado = bool.Parse(reader["Sincronizado"].ToString());
                    int idBodega = int.Parse(reader["idBodega"].ToString());
                    int Prioridad = int.Parse(reader["Prioridad"].ToString());
                    DateTime FechaEntrega = DateTime.Parse(reader["FechaEntrega"].ToString());

                    ListAprobarDespacho.Add(new e_AprobarDespacho(idMaestroSolicitud, idUsuario, FechaCreacion, Nombre, Comentarios, IdCompania, idDestino, idInterno,
                        idInternoSAP, Procesada, FechaProcesamiento, Activo, Sincronizado, idBodega, Prioridad, FechaEntrega));
                }
            }
            return ListAprobarDespacho;
        }
    }
}

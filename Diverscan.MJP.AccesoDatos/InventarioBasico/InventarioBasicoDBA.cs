using Diverscan.MJP.Entidades.InventarioBasico;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.InventarioBasico
{
    public class InventarioBasicoDBA
    {
        //Método para ingresar un Inventario a la BD
        public string InsertLogAjusteDeInventario(InventarioBasicoRecord inventarioBasicoRecord, int idBodega)
        {
            try
            {
                //Instanciar la Base de Datos
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_IngresarInventarioBasico");

                //Agregar los parámetros necesarios
                dbTse.AddInParameter(dbCommand, "@Nombre", DbType.String, inventarioBasicoRecord.Nombre);
                dbTse.AddInParameter(dbCommand, "@Descripcion", DbType.String, inventarioBasicoRecord.Descripcion);
                dbTse.AddInParameter(dbCommand, "@FechaPorAplicar", DbType.DateTime, inventarioBasicoRecord.FechaPorAplicar);
                dbTse.AddInParameter(dbCommand, "@idBodega", DbType.Int32, idBodega);
                dbTse.AddOutParameter(dbCommand, "@resultado", DbType.String, 200);

                //Ejecutar el SP
                dbTse.ExecuteNonQuery(dbCommand);

                //Retornar el mensaje de resultado
                string resultado = dbCommand.Parameters["@resultado"].Value.ToString();
                return resultado;
            } catch(Exception ex)
            {
                //Mensaje en caso de error
                return ex.Message;
            }
           
        }

        public List<InventarioBasicoRecord> ObtenerInventarioBasicoRecords(DateTime fechaInicio, DateTime fechaFin)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerInventarioBasico");

            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, fechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, fechaFin);

            List<InventarioBasicoRecord> records = new List<InventarioBasicoRecord>();
    
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    records.Add(new InventarioBasicoRecord(reader));
                 
                }
            }
            return records;
        }

        //Método para obtener los inventarios entre fechas 
        public List<InventarioBasicoRecord> ObtenerTodosInventarioBasicoRecords(DateTime fechaInicio, DateTime fechaFin, int idBodega)
        {
            //Instanciar la Base de Datos
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerTodosInventarioBasico");

            //Agregar los parámetros necesarios
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, fechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, fechaFin);
            dbTse.AddInParameter(dbCommand, "@IdBodega", DbType.Int32, idBodega);

            //Crear una lista de la clase de inventarios
            List<InventarioBasicoRecord> records = new List<InventarioBasicoRecord>();
    
            //Ejecutar el comando
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                //Recorrer cada objeto de la BD y agregar la información
                while (reader.Read())
                {
                    InventarioBasicoRecord inventario = new InventarioBasicoRecord();
                    inventario.IdInventarioBasico = long.Parse(reader["IdInventarioBasico"].ToString());
                    inventario.Nombre = reader["Nombre"].ToString();
                    inventario.Descripcion = reader["Descripcion"].ToString();
                    inventario.FechaPorAplicar = DateTime.Parse(reader["FechaPorAplicar"].ToString());
                    inventario.Estado = Convert.ToBoolean(reader["Estado"]);
                    inventario.IdInternoBodega = reader["idInterno"].ToString();
                    inventario.TrazableBodega = Convert.ToBoolean(reader["trazable"]);
                    records.Add(inventario); //Agregar a la lista el objeto de la clase
                }
            }
            return records; //Retornar la lista
        }


        //Método para cerrar un Inventario a la BD
        public string CerrarInventarioBasico(long idInventarioBasico)
        {
            try
            {
                //Instanciar la Base de Datos
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_CerrarInventarioBasico");

                //Agregar los parámetros necesarios
                dbTse.AddInParameter(dbCommand, "@IdInventarioBasico", DbType.Int64, idInventarioBasico);
                dbTse.AddOutParameter(dbCommand, "@resultado", DbType.String, 200);

                //Ejecutar el SP
                dbTse.ExecuteNonQuery(dbCommand);

                //Retornar el mensaje de resultado
                string resultado = dbCommand.Parameters["@resultado"].Value.ToString();
                return resultado;
            } catch(Exception ex)
            {
                //Mensaje en caso de error
                return ex.Message;
            }

}
    }
}

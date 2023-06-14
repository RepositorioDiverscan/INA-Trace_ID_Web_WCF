using Diverscan.MJP.Entidades.Reportes.OlasFinalizadas;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.OlasFinalizadas
{
    public class DOlaFinalizada
    {

        //metodo para obtener los datos 
        public List<EOlaFinalizada> ConsultarOla( DateTime f1, DateTime f2)
        {
            //Configuracion de la bd y llamado del sp a ejecutar
            var db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_ObtenerOlasFinalizadas");

            //Agregar los parametros que solicita el SP
            db.AddInParameter(dbCommand, "@fechaInicio", DbType.Date, Convert.ToDateTime(f1).Date);
            db.AddInParameter(dbCommand, "@fechaFin", DbType.Date, Convert.ToDateTime(f2).Date);

            //Lista que almacena la informacion
            List<EOlaFinalizada> ola = new List<EOlaFinalizada>();

            //Leer la informacion de Base de Datos
            using (var reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    ola.Add(new EOlaFinalizada(reader));
                }
            }

            return ola;


        }
    }
}

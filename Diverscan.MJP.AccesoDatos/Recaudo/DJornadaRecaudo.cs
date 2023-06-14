using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Recaudo
{
    public class DJornadaRecaudo
    {
        public List<EJornadaRecaudo> GetJornadaRecaudosCorreo( string  correoRecaudador,
            DateTime fechaInicio, DateTime fechaFin)
        {

            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("Sp_Bus_JornadasPorCorreo_VP");
            db.AddInParameter(dbCommand, "@CorreoRecaudador", DbType.String, correoRecaudador);
            db.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, fechaInicio);
            db.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, fechaFin);
            List<EJornadaRecaudo> jornadasCorreo = new List<EJornadaRecaudo>();
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    EJornadaRecaudo jornadaRecaudo = new EJornadaRecaudo(reader);

                    jornadasCorreo.Add(jornadaRecaudo);
                }
            }

            return jornadasCorreo;
        }

        public EJornadaRecaudo GetJornadaXIdJornada(int idJornada)
        {

            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("Sp_Bus_JornadasPorJornada_VP");
            db.AddInParameter(dbCommand, "@IdJornada", DbType.Int32, idJornada);

            EJornadaRecaudo jornadaRecaudo = new EJornadaRecaudo();

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    jornadaRecaudo = new EJornadaRecaudo(reader);
                }
            }

            return jornadaRecaudo;
        }

    }
}

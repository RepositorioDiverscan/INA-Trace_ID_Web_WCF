using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.MotivoAjusteInventario
{
    public class CentroCostosDBA
    {
        public List<CentroCostosRegistro> ObtenerCentroDeCostos()
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerCentroDeCostos");
            List<CentroCostosRegistro> centroCostosList = new List<CentroCostosRegistro>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    var idCentroCostos = long.Parse(reader["idDestino"].ToString());
                    var nombre = reader["Nombre"].ToString();
                    centroCostosList.Add(new CentroCostosRegistro(idCentroCostos, nombre));
                }
            }
            return centroCostosList;
        }        
    }
}

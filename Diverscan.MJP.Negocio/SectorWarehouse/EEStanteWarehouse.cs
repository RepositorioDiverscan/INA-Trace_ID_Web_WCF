using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.SectorWarehouse
{
    [Serializable]
   public class EEStanteWarehouse
    {
        private string _estante;

        public EEStanteWarehouse(string estante)
        {
            _estante = estante;
        }

        public EEStanteWarehouse(IDataReader reader)
        {
            _estante = reader["estante"].ToString();
        }

        public string Estante { get => _estante; set => _estante = value; }

    }
}

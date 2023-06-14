using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Diverscan.MJP.Entidades.OrdenCompra;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace Diverscan.MJP.AccesoDatos.OrdenCompra
{
    public class EstadoOrdenC : IEstadoOrdenC
    {

        public List<EstadoOrdenCompra> StatusActualOrden(string idMaestroArticulo)
        {
            try
            {
                int valor = Convert.ToInt32(idMaestroArticulo);
                var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
                var dbCommand = dbTse.GetStoredProcCommand("SP_ConsultaCantidadPendiente_OrdenC");

                dbTse.AddInParameter(dbCommand, "@idMaestroOrden", DbType.Int32, valor);

                List<EstadoOrdenCompra> statusList = new List<EstadoOrdenCompra>();
                using (var reader = dbTse.ExecuteReader(dbCommand))
                {
                    while (reader.Read())

                    {
                        statusList.Add(
                            new EstadoOrdenCompra(reader));
                    }
                }
                return statusList;
            }
            catch (Exception)
            {
                return new List<EstadoOrdenCompra>();
            }
        }
    }
}

using Diverscan.MJP.Entidades.Invertario;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Consultas
{
   public  class DInventario
    {
        //metodo para obterner el encabezado del inventario 
        public List<EEncabezadoInventario>InventarioEncabezado(DateTime f1, DateTime f2) {

            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_Consulta_MaestroInventario");
            db.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, f1);
            db.AddInParameter(dbCommand, "@FechaFinal", DbType.DateTime, f2);

            //se guarda en una lista
            List<EEncabezadoInventario> listaInventario = new List<EEncabezadoInventario>();
            try
            { 
                using (var reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        listaInventario.Add(new EEncabezadoInventario(reader));
                    }
                }

            }catch(Exception e)
            {
                var p = e.Message;
            }
            return listaInventario;
        }

        //metodo para obtener el detalle del inventario
         public List<EDetalleInventario> ObtenerDetalleInventario(int id)
        {
            Database db = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = db.GetStoredProcCommand("SP_Consulta_DetalleInventario");
            db.AddInParameter(dbCommand, "@idInventario", DbType.Int32, id);

            //guardar en una lista
            List<EDetalleInventario> listaDetalleInventario = new List<EDetalleInventario>();
            try
            { 
                using(var reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        listaDetalleInventario.Add(new EDetalleInventario(reader));
                    }
                }

            }catch(Exception e)
            {
                var p = e.Message;
            }
            return listaDetalleInventario;
        }
    }
}

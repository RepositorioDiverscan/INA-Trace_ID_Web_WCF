using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diverscan.MJP.AccesoDatos.Reportes.Reporte_Ajuste_de_Inventario
{
    public class ObtenerPromociones : IObtenerPromociones
    {
        public List<EObtieneArticulosSolicitud> ObtenerArticulosPorAjuste(int idSolicitud)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerArticulosPorSolicitud");
            dbTse.AddInParameter(dbCommand, "@idSolicitud", DbType.Int32, idSolicitud);
           

            List<EObtieneArticulosSolicitud> ListaArticulosPorSolicitud = new List<EObtieneArticulosSolicitud>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    int IdSolicitudAjusteInventario = Int32.Parse(reader["IdSolicitudAjusteInventario"].ToString());
                    int IdArticulo = Convert.ToInt32(reader["IdArticulo"].ToString());
                    string IdInterno = reader["IdInterno"].ToString();
                    string Nombre = reader["Nombre"].ToString();
                    decimal Cantidad = Convert.ToDecimal(reader["Cantidad"].ToString());


                    ListaArticulosPorSolicitud.Add(new EObtieneArticulosSolicitud(IdSolicitudAjusteInventario, IdArticulo, IdInterno, Nombre,
                        Cantidad));
                }
            }
            return ListaArticulosPorSolicitud;
        }

        public List<AjusteSolicitudRecord> ObtenesAjustesPorFechas(DateTime fechaInicio, DateTime fechaFinal)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerAjusteInventarioPorFechas");
            dbTse.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, fechaInicio);
            dbTse.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, fechaFinal);

            List<AjusteSolicitudRecord> ListaAjustesPorFechas = new List<AjusteSolicitudRecord>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    long idSolicitudAjusteInventario = long.Parse(reader["IdSolicitudAjusteInventario"].ToString());
                    DateTime fechaSolicitud = DateTime.Parse(reader["FechaSolicitud"].ToString());
                    string nombre = reader["Nombre"].ToString();
                    string apellidos = reader["Apellidos"].ToString();
                    string motivoAjuste = reader["MotivoInventario"].ToString();
                    bool tipoAjuste = bool.Parse(reader["TipoAjuste"].ToString());
                    string centroCosto = reader["CentroCosto"].ToString();

                    ListaAjustesPorFechas.Add(new AjusteSolicitudRecord(idSolicitudAjusteInventario, fechaSolicitud, nombre, apellidos,
                        motivoAjuste, tipoAjuste, centroCosto));
                }
            }
            return ListaAjustesPorFechas;
        }

        List<EObtenerArticulosPromocion> IObtenerPromociones.ObtenerArticulosDePromocion(int idMaestroPromocion_P)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerDetallePromocion");
            dbTse.AddInParameter(dbCommand, "@idMaestroPromocion", DbType.Int32, idMaestroPromocion_P);

            List<EObtenerArticulosPromocion> ListArticulosPromocion = new List<EObtenerArticulosPromocion>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    int idDetallePromocion = int.Parse(reader["idDetallePromocion"].ToString());
                    int idMaestroPromocion = int.Parse(reader["idMaestroPromocion"].ToString());
                    int idInternoArticulo = int.Parse(reader["idInternoArticulo"].ToString());
                    int idArticuloNuevo = int.Parse(reader["idArticuloNuevo"].ToString());
                    string idInternoPanal = reader["idInternoPanal"].ToString();
                    string Gtin = reader["Gtin"].ToString();
                    string Nombre = reader["Nombre"].ToString();
                    decimal Cantidad = Convert.ToDecimal(reader["Cantidad"].ToString());

                    ListArticulosPromocion.Add(new EObtenerArticulosPromocion(idDetallePromocion, idArticuloNuevo, idInternoPanal, idMaestroPromocion, idInternoArticulo, Nombre, Gtin, Cantidad));
                }
            }
            return ListArticulosPromocion;
        }

        List<NObtenerEncabezadoPromocion> IObtenerPromociones.ObtenerPromociones()
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ConsultaEncabezadoPromocion");

            List<NObtenerEncabezadoPromocion> ListEncabezadosPromociones = new List<NObtenerEncabezadoPromocion>();

            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    int idMaestroPromocion = int.Parse(reader["idMaestroPromocion"].ToString());
                    int idInternoArticulo = int.Parse(reader["idInternoArticulo"].ToString());
                    string Nombre = reader["Nombre"].ToString();
                    DateTime Fecha = Convert.ToDateTime(reader["Fecha"].ToString());

                    ListEncabezadosPromociones.Add(new NObtenerEncabezadoPromocion(idMaestroPromocion, idInternoArticulo, Nombre, Fecha));
                }
            }
            return ListEncabezadosPromociones;
        }
    }
}

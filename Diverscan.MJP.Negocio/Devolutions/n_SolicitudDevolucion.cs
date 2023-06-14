using Diverscan.MJP.AccesoDatos.Devolutions;
using Diverscan.MJP.Entidades.Devolutions.SolicitudDevolucion;
using Diverscan.MJP.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Devolutions
{
    public class n_SolicitudDevolucion
    {
        private IFileExceptionWriter _fileExceptionWriter;
        private da_SolicitudDevolucion Da_SolicitudDevolucion;
        public n_SolicitudDevolucion(IFileExceptionWriter fileExceptionWriter)
        {
            _fileExceptionWriter = fileExceptionWriter;
            Da_SolicitudDevolucion = new da_SolicitudDevolucion();
        }

        public ResultadoObtenerTransportistas ObtenerTransportistas(string idBodega)
        {
            ResultadoObtenerTransportistas resultado = new ResultadoObtenerTransportistas();
            try
            {
                resultado.transportistas = Da_SolicitudDevolucion.ObtenerTransportistas(idBodega);
                resultado.Description = "Succesful";
                resultado.state = true;

            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.DEVOLUCIONFILEPATHEXCEPTION);
                resultado.Description = ex.Message;
                resultado.state = false;

            }
            return resultado;

        }

        public string IngresarSolicitudDevolucion(e_EncabezadoSolicitudDevolucion solicitudDevolucion)
        {
            string resultado;
            try
            {
                //Se crea una tabla para agregar los datos del detalle
                DataTable dataTable = new DataTable();
                //Se crean las columnas de la tabla
                dataTable.Columns.Add("idArticulo", typeof(int));
                dataTable.Columns.Add("Cantidad", typeof(int));
                dataTable.Columns.Add("Placa", typeof(string));
                dataTable.Columns.Add("FechaVencimiento", typeof(string));
                dataTable.Columns.Add("Lote", typeof(string));

                //se recorre normalmente la lista de detalles del pedido 
                foreach (var item in solicitudDevolucion.DetallesSolicitud)
                {
                    //se crea una nueva fila con los datos del detalle 
                    DataRow row = dataTable.NewRow();
                    row["idArticulo"] = item.IdArticulo;
                    row["Cantidad"] = item.Cantidad;
                    row["Placa"] = item.Placa;
                    row["FechaVencimiento"] = item.FechaVencimiento.ToString();
                    row["Lote"] = item.Lote;
                    //se agrega la fila a la tabla
                    dataTable.Rows.Add(row);
                }

                resultado = Da_SolicitudDevolucion.IngresarSolicitudDevolucion(solicitudDevolucion, dataTable);

            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.DEVOLUCIONFILEPATHEXCEPTION);
                resultado= ex.Message;

            }
            return resultado;

        }

        public ResultadoObtenerSolicitudesDevolucion ObtenerSolicitudesDevolucion(string idBodega)
        {
            ResultadoObtenerSolicitudesDevolucion resultado = new ResultadoObtenerSolicitudesDevolucion();
            try
            {
                resultado.encabezadosSolicitudDev = Da_SolicitudDevolucion.ObtenerSolicitudesDevolucion(idBodega);
                resultado.Description = "Succesful";
                resultado.state = true;

            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.DEVOLUCIONFILEPATHEXCEPTION);
                resultado.Description = ex.Message;
                resultado.state = false;

            }
            return resultado;

        }
        public ResultadoObtenerDetallesSolicitud ObtenerDetallesSolicitud(long idSolicitud)
        {
            ResultadoObtenerDetallesSolicitud resultado = new ResultadoObtenerDetallesSolicitud();
            try
            {
                resultado.detalles = Da_SolicitudDevolucion.ObtenerDetallesSolicitud(idSolicitud);
                resultado.Description = "Succesful";
                resultado.state = true;

            }
            catch (Exception ex)
            {
                _fileExceptionWriter.WriteException(ex, PathFileConfig.DEVOLUCIONFILEPATHEXCEPTION);
                resultado.Description = ex.Message;
                resultado.state = false;

            }
            return resultado;

        }
    }
}

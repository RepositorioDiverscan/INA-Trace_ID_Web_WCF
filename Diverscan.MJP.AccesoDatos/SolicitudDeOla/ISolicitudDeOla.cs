using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SolicitudDeOla
{
    public interface ISolicitudDeOla
    {
        List<E_ListadoPreMaestro> ObtenerListadoPreMaestro(int idBodega);
        List<E_ListadoPreMaestro> ObtenerListadoPreMaestroRuta(string ruta, int idBodega);
        List<E_ListadoPreMaestro> ObtenerListadoPreMaestroFechas(
            DateTime fechaInicio, DateTime fechaFinal,  int idBodega);  //  string ruta,
        void EliminarListadoPreMaestro(List<int> ListadoSolicitudes, int idBodega);
        List<E_ListadoRutas> ObtenerListadoRutas(int idBodega);
        void InsertarOla(List<int> ListadoSolicitudes, string Observacion, int prioridad, int idBodega);
        void InsertarOlaCompleta(List<int> ListadoSolicitudes, string Observacion, int prioridad, int idBodega);
        List<E_ListadoOlasCreadas> ObtenerListadoOlas(int Estado, int idBodega);
        void AprobarOla(List<int> ListadoOlasAprobadas);
        List<E_ListadoOlasCreadas> ObtenerListadoOlasPendientesSeleccionadas(int idRegistro, int idBodega);
        void EditarOla(List<int> ListadoOlas, int RegistroOla);
        List<E_ListadoSolicitudesEliminarOla> ObtenerListadoSolicitudesEliminar(int idRegistroOla);
        void EliminarSolicitudOla(List<int> ListadoSolicitudesEliminar, int Ola);
        List<E_ListadoDetallesMaestro> ObtenerListadoDetalleMaestro(int idMaestro);
        int RevertirOla(int idMaestro);
    }
}

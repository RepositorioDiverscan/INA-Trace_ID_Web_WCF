using Diverscan.MJP.AccesoDatos.InventarioBasico;
using Diverscan.MJP.Entidades.InventarioBasico;
using System;
using System.Collections.Generic;

namespace Diverscan.MJP.Negocio.InventarioBasico
{
    public class N_InventarioBasico
    {
        //Método para insertar un Inventario
        public static string InsertLogAjusteDeInventario(InventarioBasicoRecord inventarioBasicoRecord,int idBodega)
        {
            //Instanciar la capa de BD y retornar el método de insertar
            InventarioBasicoDBA inventarioBasicoDBA = new InventarioBasicoDBA();
            return inventarioBasicoDBA.InsertLogAjusteDeInventario(inventarioBasicoRecord, idBodega);
        }

        //Método para obtener una lista de inventarios entre fechas 
        public static List<InventarioBasicoRecord> ObtenerTodosInventarioBasicoRecords(DateTime fechaInicio, DateTime fechaFin, int idBodega)
        {
            //Instanciar la capa de BD y retornar el método de la lista
            InventarioBasicoDBA inventarioBasicoDBA = new InventarioBasicoDBA();
            return inventarioBasicoDBA.ObtenerTodosInventarioBasicoRecords(fechaInicio, fechaFin, idBodega);
        }


        //Método para obtener una lista de inventarios entre fechas - VERIFICAR SI SE BORRA PUES NO SE USA LA PANTALLA DÓNDE SE INVOCA
        public static List<InventarioBasicoRecord> ObtenerInventarioBasicoRecords(DateTime fechaInicio, DateTime fechaFin)
        {
            //Instanciar la capa de BD y retornar el método de la lista
            InventarioBasicoDBA inventarioBasicoDBA = new InventarioBasicoDBA();
            return inventarioBasicoDBA.ObtenerInventarioBasicoRecords(fechaInicio, fechaFin);
        }


        //Método para cerrar el inventario
        public static string CerrarInventarioBasico(long idInventarioBasico)
        {
            //Instanciar la capa de BD y retornar el método de cierre
            InventarioBasicoDBA inventarioBasicoDBA = new InventarioBasicoDBA();
            return inventarioBasicoDBA.CerrarInventarioBasico(idInventarioBasico);
        }


        #region Hand Held
        //Método para obtener los inventarios desde la Hand Held
        public static List<InventarioBasicoRecord> ObtenerInventarioBasicoRecords(string fechaInicio, string fechaFin, string pidBodega)
        {
           
            DateTime dateInit = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dateEnd = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            int idBodega = int.Parse(pidBodega);
            return ObtenerTodosInventarioBasicoRecords(dateInit, dateEnd, idBodega);
        }
        #endregion Hand Held
    }
}

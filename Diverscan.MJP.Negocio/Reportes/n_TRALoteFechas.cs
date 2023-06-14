using Diverscan.MJP.AccesoDatos.Reportes;
using Diverscan.MJP.Entidades.Trazabilidad;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diverscan.MJP.Negocio.Reportes
{
    public class n_TRALoteFechas
    {
        public static List<TraResumen> GetTRALote(long idArticulo, string lote)
        {
            TRALoteFechasDBA _TRALoteFechasDBA = new TRALoteFechasDBA();
            var traList = _TRALoteFechasDBA.GetTRALote(idArticulo, lote);            
            return Agurpar(traList);
        }

        public static List<TraResumen> GetTRALoteV2(long idArticulo, string lote)
        {
            TRALoteFechasDBA _TRALoteFechasDBA = new TRALoteFechasDBA();
            var traList = _TRALoteFechasDBA.GetTRALote(idArticulo, lote);
            return ConvertirTRARecordTrazabilidadATraResumen(traList);
        }



        public static List<TraResumen> GetTRAVencimiento(long idArticulo, DateTime fechaVencimiento)
        {
            TRALoteFechasDBA _TRALoteFechasDBA = new TRALoteFechasDBA();
            var traList = _TRALoteFechasDBA.GetTRAVencimiento(idArticulo, fechaVencimiento);
            return Agurpar(traList);
        }


        public static List<TraResumen> GetTRAVencimientoV2(long idArticulo, DateTime fechaVencimiento)
        {
            TRALoteFechasDBA _TRALoteFechasDBA = new TRALoteFechasDBA();
            var traList = _TRALoteFechasDBA.GetTRAVencimiento(idArticulo, fechaVencimiento);
            return ConvertirTRARecordTrazabilidadATraResumen(traList);
        }

        public static List<TraResumen> Agurpar(List<TRARecordTrazabilidad> traList)
        {
            List<TraResumen> resumen = new List<TraResumen>();
            if (traList.Count > 0)
            {
                var minDate = traList.Min(x => x.FechaRegistro);
                var maxDate = traList.Max(x => x.FechaRegistro);

                var minOnlyDate = new DateTime(minDate.Year, minDate.Month, minDate.Day);
                var maxOnlyDate = new DateTime(maxDate.Year, maxDate.Month, maxDate.Day);

                for (var x = minOnlyDate; x <= maxOnlyDate; x = x.AddDays(1))
                {
                    //var dataByDate = traList.Where(i => new DateTime(i.FechaRegistro.Year, i.FechaRegistro.Month, i.FechaRegistro.Day).CompareTo(x) == 0);
                    var dataByDate2 = traList.Where(i => i.FechaRegistro.Date.CompareTo(x.Date) == 0);
                    var result = AgruparPorUbicacionIdEstado(dataByDate2.ToList(), x.Date);
                    resumen.AddRange(result);
                }
            }
            return resumen;
        }

        private static List<TraResumen> AgruparPorUbicacionIdEstado(List<TRARecordTrazabilidad> traList, DateTime fecha)
        {           
            List<TraResumen> resumen = new List<TraResumen>();
            var result = traList.GroupBy(i => new {i.IdUbicacion, i.IdEstado, i.IdMetodoAccion,i.OC_Destino});
            
            foreach(var obj1 in result)
            {
                var cantidad = obj1.Count();
                var etiqueta = obj1.First().Etiqueta; 
                
                resumen.Add(new TraResumen(obj1.Key.IdUbicacion, etiqueta, obj1.Key.IdEstado, cantidad,
                    fecha,obj1.Key.IdMetodoAccion,obj1.First().Unidad_Medida));                
            }            
            return resumen;
        }            

        private static List<TraResumen> ConvertirTRARecordTrazabilidadATraResumen(List<TRARecordTrazabilidad> traList)
        {
            List<TraResumen> resumen = new List<TraResumen>();
            foreach (var item in traList)
            {
                resumen.Add(new TraResumen(item.IdUbicacion, item.Etiqueta, item.IdEstado, Convert.ToInt32(item.Cantidad), item.FechaVencimiento, item.IdMetodoAccion, item.Unidad_Medida));
            }
            return resumen;
        }

    }
    [Serializable]
    public class TraResumen
    {
        public long IdUbicacion { get; set; }
        public string Etiqueta { get; set; }
        public long IdEstado { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public long IdMetodoAccion { set; get; }        
        string _unidad_Medida="";
    

        public TraResumen(long idUbicacion, string etiqueta, long idEstado, int cantidad, 
            DateTime fecha, long idMetodoAccion, string unidad_Medida)
        {
            IdUbicacion = idUbicacion;
            Etiqueta = etiqueta;
            IdEstado = idEstado;
            Cantidad = cantidad;
            Fecha = fecha;
            IdMetodoAccion = idMetodoAccion;
            _unidad_Medida = unidad_Medida;
        }
        public string EstadoToShow
        { 
            get{
                if (IdEstado == 12) 
                    return "Entrada";
                if (IdEstado == 16)
                    return "Recibido";
                    return "Salida";
            }
        }

        public string AccionMostrar
        {
            get
            {
                if (IdMetodoAccion == 19)
                    return "Alistado";
                if (IdMetodoAccion == 28)
                    return "Solicitud/Restaurante";
                if (IdMetodoAccion == 55)
                    return "RECEPCIÓN/Entrada";
                if (IdMetodoAccion == 58)
                    return "RECEPCIÓN/Salida";
                if (IdMetodoAccion == 59 )
                    return "RECEPCIÓN/UBICACIÓN";
                if (IdMetodoAccion == 62)
                    return "Zona De Transito";
                if (IdMetodoAccion == 66)
                    return "Salida de Ubicacion";
                 if (IdMetodoAccion == 67 )
                     return "DESPACHO";
                 if (IdMetodoAccion == 75)
                     return "TRASLADO-EGRESO";
                 if (IdMetodoAccion == 76)
                     return "TRASLADO-INGRESO";
                 if (IdMetodoAccion == 8)
                     return "AJUSTE";              
                return "NA";
            }
        }

        public string Unidad_Medida 
        {
            get 
            {
                var words = _unidad_Medida.Split('-');
                if (words.Length>1)
                    return words[1];
                return ""; 
            }
        }
    }
}

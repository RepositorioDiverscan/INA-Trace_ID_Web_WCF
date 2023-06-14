using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Diverscan.MJP.AccesoDatos.DisponibleBodegaWebService;
using System.Net;
using System.IO;

namespace Diverscan.MJP.AccesoDatos.DisponibleBodegaWebService
{
    public class WSDisponibleBodega
    {
        public EDisponibleBodega ObtenerDisponibleBodegaSap(string idInterno,string bodega)
        {
            EDisponibleBodega disponibleBodega = new EDisponibleBodega();
            string url = @"http://52.42.16.53:8085/Api/DisponibleBodega" + "/" + idInterno + "/" + bodega;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 3000;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                 disponibleBodega = JsonConvert.DeserializeObject<EDisponibleBodega>(json);
            }
            return disponibleBodega;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.Reportes
{

    [Serializable]
    public class Entidad_Despacho : IEntidad_Despacho
    {

        private int _solicitud;

        private string _nombreArticulo;

        private string _referencia;

        private double _cantidad;

        private string _sscc;

        private string _destino;

        private DateTime _fechadespacho; 

        public Entidad_Despacho(int Solicitud,string NombreArticulo,string Referencia,double Cantidad,string SSCC,string Destino,DateTime FechaDespacho)
        {
            _solicitud = Solicitud;
            _nombreArticulo = NombreArticulo;
            _referencia = Referencia;
            _cantidad = Cantidad;
            _sscc = SSCC;
            _destino = Destino;
            _fechadespacho = FechaDespacho;

        }
        public int Solicitud
        {
            get { return _solicitud; }
            set { _solicitud = value; }
        }

        public string NombreArticulo
        {
            get { return _nombreArticulo; }
            set { _nombreArticulo = value; }
        }

        public string Referencia
        {
            get { return _referencia; }
            set { _referencia = value; }
        }


        public double Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public string SSCC
        {
            get { return _sscc; }
            set { _sscc = value; }
        }

        public string Destino
        {
            get { return _destino; }
            set { _destino = value; }
        }

        public DateTime FechaDespacho
        {
            get { return _fechadespacho; }
            set { _fechadespacho = value; }
        }

    }
}


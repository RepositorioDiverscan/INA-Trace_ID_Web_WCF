using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using Diverscan.MJP.Entidades;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Diverscan.MJP.Entidades
{
    public class EntidadesGS1
    {
        public class e_EstructuraCodigo
        {
            public e_GTIN GTIN { get; set; }
        }

        public class e_IdentificadorAplicacion
        {
            public string idIdentificadorAplicacion { get; set; }
            public string Nombre { get; set; }
            public string Estructura { get; set; }
            public string Descripcion { get; set; }
            public int CantidadDigitos { get; set; }
            public bool Variable { get; set; }
            public bool EsFecha { get; set; }
            public bool EsNumero { get; set; }
            public bool EsTexto { get; set; }
            public string ValorEncontrado { get; set; }
        }

        public class e_Etiqueta
        {
            public List<e_IdentificadorAplicacion> AI { get; set; }
            public int idArticulo { get; set; }
        }


        /// <summary>
        /// El GTIN puede ser 14,13,12,10 y 8
        /// En caso de ser 14 la cantidad puede ser diferente a 1. Esto se da si el ValorLogistico
        /// tiene relacion en la base de datos con el proveedor, ya que este podria emparcar
        /// diferente que otro proveedor el mismo articulo. Esta practica no es recomendable, pues
        /// para eso existe el AI=37 que especifica la cantidad contenida en la caja del GTIN 14
        /// </summary>
        public class e_GTIN
        {
            public List<e_Digito> Digitos { get; set; }
            public string ValorLeido { get; set; }
            public string Tipo { get; set; }
            public int DigitoVerificador { get; set; }
            public e_ValorLogistico VL { get; set; } // esto se usa cuando se quiere agrupar en GTIN14
            public int Cantidad { get; set; }
            public List<e_ValorLogistico> VLs { get; set; } // esto se usa cuando se quiere agrupar en GTIN14
        }

        public class e_SSCC
        {
            public string Codigo { get; set; }
        }

        public class e_FechaVencimiento
        {
            public DateTime FechaVencimiento { get; set; }
            public string TextoOriginal { get; set; }
        }

        public class e_Digito
        {
            public int NumDigito { get; set; }
            public int Valor { get; set; }
        }

        public class e_ValorLogistico
        {
            public int idVL { get; set; }
            public Single Cantidad { get; set; }
            public string Codigo { get; set; }
        }
    }
}


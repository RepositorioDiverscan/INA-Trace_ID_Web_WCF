using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Diverscan.MJP.Utilidades
{
    public class clEncriptar
    {
        private const string key = "S3tD1v201b";

        public string Encriptar(string texto)
        {
            try
            {
                byte[] keyArray;
                byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);

                //Se utilizan las clases de encriptación MD5

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

                //Algoritmo TripleDES
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();

                byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);

                tdes.Clear();

                //se regresa el resultado en forma de una cadena
                texto = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);

            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError("Method: Encriptar.", ex.StackTrace);
            }
            return texto;
        }

        public string Desencriptar(string textoEncriptado)
        {
            try
            {
                byte[] keyArray;
                byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);

                //algoritmo MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();

                byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);

                tdes.Clear();
                textoEncriptado = UTF8Encoding.UTF8.GetString(resultArray);

            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError("Method: Desencriptar.", ex.StackTrace);
            }
            return textoEncriptado;
        }

        public bool EncriptaConexion(string ConnName, string ConnString)
        {
            try
            {
                string conexionEncriptada = Encriptar(ConnString);
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings[ConnName].ConnectionString = conexionEncriptada;
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");

                return true;
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError("Method: EncriptaConexion.", ex.StackTrace);
                return false;
            }
        }

        public bool DesencriptaConexion(string ConnName, string ConnString)
        {
            try
            {
                string conexionDesencriptada = Desencriptar(ConnString);
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings[ConnName].ConnectionString = conexionDesencriptada;
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");

                return true;
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError("Method: DesencriptaConexion.", ex.StackTrace);
                return false;
            }
        }
    }
}

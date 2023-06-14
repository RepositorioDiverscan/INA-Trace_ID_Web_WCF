using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de LeftRightMid
/// </summary>
/// 

namespace Diverscan.MJP.Utilidades
{
    public class LeftRightMid
    {
        [STAThread]
        private static void Main(string[] args)
        {

            string myString = "This is a string";
            Console.WriteLine(Left(myString, 4));
            Console.WriteLine(Right(myString, 6));
            Console.WriteLine(Mid(myString, 5, 4));
            Console.WriteLine(Mid(myString, 5));
            Console.ReadLine();
        }

        public static string Left(string param, int length)
        {
            string result = param.Substring(0, length);
            return result;
        }

        public static string Right(string param, int length)
        {
            int value = param.Length - length;
            string result = param.Substring(value, length);
            return result;
        }

        public static string Mid(string param, int startIndex, int length)
        {
            string result = param.Substring(startIndex, length);
            return result;
        }

        public static string Mid(string param, int startIndex)
        {
            string result = param.Substring(startIndex);
            return result;
        }

        #region instance

        private static LeftRightMid m_instance;
        public static LeftRightMid Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new LeftRightMid();
                }
                return m_instance;
            }
        }
        #endregion
    }

}
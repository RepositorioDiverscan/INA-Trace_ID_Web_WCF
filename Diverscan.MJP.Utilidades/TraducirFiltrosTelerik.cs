using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Web.UI;

namespace Diverscan.MJP.Utilidades
{
    public class TraducirFiltrosTelerik
    {
        public static void traducirFiltros(GridFilterMenu menu)
        {
            foreach (RadMenuItem item in menu.Items)
            {
                switch (item.Value)
                {
                    case "NoFilter":
                        item.Text = "Sin Filtro";
                        break;
                    case "Contains":
                        item.Text = "Contiene";
                        break;
                    case "DoesNotContain":
                        item.Text = "No contiene";
                        break;
                    case "StartsWith":
                        item.Text = "Comienza con";
                        break;
                    case "EndsWith":
                        item.Text = "Termina con";
                        break;
                    case "EqualTo":
                        item.Text = "Igual que";
                        break;
                    case "NotEqualTo":
                        item.Text = "No Igual que";
                        break;
                    case "GreaterThan":
                        item.Text = "Mayor que";
                        break;
                    case "LessThan":
                        item.Text = "Menos que";
                        break;
                    case "GreaterThanOrEqualTo":
                        item.Text = "Mayor o igual que";
                        break;
                    case "LessThanOrEqualTo":
                        item.Text = "Menor o igual que";
                        break;
                    case "Between":
                        item.Text = "Entre";
                        break;
                    case "NotBetween":
                        item.Text = "No entre";
                        break;
                    case "IsEmpty":
                        item.Text = "Es vacio";
                        break;
                    case "NotIsEmpty":
                        item.Text = "No es vacio";
                        break;
                    case "IsNull":
                        item.Text = "Es nulo";
                        break;
                    case "NotIsNull":
                        item.Text = "No es nulo";
                        break;
                }
            }
        }
    }
}

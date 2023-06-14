using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Diverscan.MJP.AccesoDatos
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Creates data table from source data.
        /// </summary>
        public static DataTable ToDataTable<T>(this IEnumerable<T> source)
        {
            DataTable table = new DataTable();

            //// get properties of T
            var binding = BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty;
            var options = PropertyReflectionOptions.IgnoreEnumerable | PropertyReflectionOptions.IgnoreIndexer;

            var properties = ReflectionExtensions.GetProperties<T>(binding, options).ToList();

            //// create table schema based on properties
            foreach (var property in properties)
            {
                table.Columns.Add(property.Name, property.PropertyType);
            }

            //// create table data from T instances
            object[] values = new object[properties.Count];

            foreach (T item in source)
            {
                for (int i = 0; i < properties.Count; i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }

                table.Rows.Add(values);
            }

            return table;
        }

        public static DataTable ToDataTablePrimitive<T>(this IEnumerable<T> source, string columnName)
        {
            DataTable table = new DataTable();

            //// get properties of T
            var binding = BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty;
            var options = PropertyReflectionOptions.IgnoreEnumerable | PropertyReflectionOptions.IgnoreIndexer;

            var properties = ReflectionExtensions.GetProperties<T>(binding, options).ToList();

            //// create table schema based on properties           
            table.Columns.Add(columnName);

            //// create table data from T instances
            object[] values = new object[1];

            foreach (T item in source)
            {
                values[0] = item;
                table.Rows.Add(values);
            }

            return table;
        }
    }
}

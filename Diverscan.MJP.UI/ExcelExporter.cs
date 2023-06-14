using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Diverscan.MJP.UI
{
    public class ExcelExporter
    {
        public static void ExportData<T>(IEnumerable<T> pdataTable, string pfilePath, PropertyDescriptorCollection properties)
        {
            if (pdataTable == null)
                throw new ArgumentNullException("pdataTable", "pdataTable is null");
            if (String.IsNullOrEmpty(pfilePath))
                throw new ArgumentException("pfilePath is null or empty", "pfilePath");
        
            if (File.Exists(pfilePath))
            {
                File.Delete(pfilePath);
            }

            var newFile = new FileInfo(pfilePath);
            using (ExcelPackage excelPackage = new ExcelPackage(newFile))
            {
                excelPackage.Workbook.Worksheets.Add("Hoja 1");
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[1];

                for (int x = 0; x < properties.Count; x++)
                {
                    excelWorksheet.Cells[1, x + 1].Value = properties[x].Name;
                }
                int index = 0;
                foreach (var data in pdataTable)
                {
                    index++;
                    for (int x = 0; x < properties.Count; x++)
                    {
                        excelWorksheet.Cells[index + 2, x + 1].Value = properties[x].GetValue(data) ?? DBNull.Value;
                    }
                }
                excelWorksheet.Cells["A1:Z10000"].AutoFitColumns();
                excelPackage.Save();
            }
        }

        public static void ExportData<T>(IEnumerable<T> pdataTable, string pfilePath, PropertyDescriptorCollection properties, List<string> headers)
        {
            if (pdataTable == null)
                throw new ArgumentNullException("pdataTable", "pdataTable is null");
            if (String.IsNullOrEmpty(pfilePath))
                throw new ArgumentException("pfilePath is null or empty", "pfilePath");

            if (File.Exists(pfilePath))
            {
                File.Delete(pfilePath);
            }

            var newFile = new FileInfo(pfilePath);
            using (ExcelPackage excelPackage = new ExcelPackage(newFile))
            {
                excelPackage.Workbook.Worksheets.Add("Hoja 1");
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[1];

                for (int x = 0; x < headers.Count; x++)
                {
                    excelWorksheet.Cells[1, x + 1].Value = headers[x];
                }
                int index = 0;
                foreach (var data in pdataTable)
                {
                    index++;
                    for (int x = 0; x < properties.Count; x++)
                    {
                        excelWorksheet.Cells[index + 2, x + 1].Value = properties[x].GetValue(data) ?? DBNull.Value;
                    }
                }
                excelWorksheet.Cells["A1:Z10000"].AutoFitColumns();
                excelPackage.Save();
            }
        }

        public static void ExportData<T>(IEnumerable<T> pdataTable, string pfilePath, PropertyDescriptorCollection properties, List<List<string>> headers)
        {
            if (pdataTable == null)
                throw new ArgumentNullException("pdataTable", "pdataTable is null");
            if (String.IsNullOrEmpty(pfilePath))
                throw new ArgumentException("pfilePath is null or empty", "pfilePath");

            if (File.Exists(pfilePath))
            {
                File.Delete(pfilePath);
            }

            var newFile = new FileInfo(pfilePath);
            using (ExcelPackage excelPackage = new ExcelPackage(newFile))
            {
                excelPackage.Workbook.Worksheets.Add("Hoja 1");
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[1];

                int index = 0;
                foreach (var header in headers)
                {
                    index++;
                    for (int x = 0; x < header.Count; x++)
                    {
                        excelWorksheet.Cells[index, x + 1].Value = header[x];
                    }
                }
                try { 
                foreach (var data in pdataTable)
                {
                    index++;
                    for (int x = 0; x < properties.Count; x++)
                    {
                        excelWorksheet.Cells[index, x + 1].Value = properties[x].GetValue(data) ?? DBNull.Value;
                    }
                }
                }
                catch(Exception ex)
                {
                    var error = ex.Message;
                }
                excelWorksheet.Cells["A1:Z10000"].AutoFitColumns();
                excelPackage.Save();
            }
        }
    }
}
using CARS.Interfaces;
using ClosedXML.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CARS.Export_Excel
{
    public class ExportExcel
    {
        public DataTable ToDataTable(IList<IExportable> lista)
        {
            var tipoObjeto = lista.FirstOrDefault();
            DataTable tabla = new DataTable();
            foreach (PropertyInfo prop in tipoObjeto.GetType().GetProperties())
            {
                if (!tabla.Columns.Contains(prop.Name))
                {
                    tabla.Columns.Add(prop.Name);
                }
                else
                {
                    if (tabla.Columns.Count == tipoObjeto.GetType().GetProperties().Count())
                    {
                        break;
                    }
                }
            }

            if (lista.Count != 0)
            {
                object[] valores = new object[tipoObjeto.GetType().GetProperties().Count()];
                foreach (var item in lista)
                {
                    int posicion = 0;
                    foreach (PropertyInfo prop in item.GetType().GetProperties())
                    {
                        if (prop.PropertyType.FullName.Contains("CARS.Models")/*!(prop.GetValue(item, null) is Nullable) && prop.GetValue(item, null) != null*/)
                        {
                            //valores[posicion] = prop.GetValue(item, null).MiNombre();
                        }
                        else
                        {
                            valores[posicion] = prop.GetValue(item, null);
                        }
                        posicion++;
                    }
                    tabla.Rows.Add(valores);
                }
            }
            
            
            return tabla;
        }

        public void ExportToExcel(List<IExportable> lista, HttpServerUtilityBase server, HttpResponseBase response, string nombreArchivo)
        {
            DataTable dt = ToDataTable(lista);
            XLWorkbook workbook = new XLWorkbook();

            workbook.Worksheets.Add(dt, "Reporte");

            string myName = server.UrlEncode(nombreArchivo + ".xlsx");

            response.Clear();
            response.Buffer = true;
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("content-disposition", "attachment; filename=" + myName);
            using MemoryStream memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            memoryStream.Position = 0;
            memoryStream.WriteTo(response.OutputStream);
            response.Flush();
            response.End();
        }

        string GetDownloadFolderPath()
        {
            return Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString();
        }

        private MemoryStream GetStream(XLWorkbook workbook)
        {
            MemoryStream ms = new MemoryStream();
            workbook.SaveAs(ms);
            ms.Position = 0;
            return ms;
        }
    }

}
using CARS.Interfaces;
using ClosedXML.Excel;
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
            foreach (var item in lista)
            {
                foreach (PropertyInfo prop in item.GetType().GetProperties())
                {
                    tabla.Columns.Add(prop.Name);
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
            var workbook = new XLWorkbook();
            dt.TableName = "Reporte";
            workbook.Worksheets.Add(dt);

            string myName = server.UrlEncode(nombreArchivo + ".xlsx");
            MemoryStream stream = GetStream(workbook);

            response.Clear();
            response.Buffer = true;
            response.AddHeader("content-disposition", "attachment; filename=" + myName);
            response.ContentType = "application/vnd.ms-excel";
            response.BinaryWrite(stream.ToArray());
            response.End();
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
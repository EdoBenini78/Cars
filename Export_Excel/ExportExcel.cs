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

            object[] valores = new object[tipoObjeto.GetType().GetProperties().Count()];
            foreach (var item in lista)
            {
                int posicion = 0;
                foreach (PropertyInfo prop in item.GetType().GetProperties())
                {
                    if (!(prop.GetValue(item,null) is Nullable) && prop.GetValue(item,null) != null)
                    {
                        valores[posicion] = item.MiNombre();
                    }
                    else
                    {
                        valores[posicion] = prop.GetValue(item, null);
                    }
                    posicion++;
                }
                tabla.Rows.Add(valores);
            }
            return tabla;
        }

        public void ExportToExcel(List<IExportable> lista)
        {
            DataTable dt = ToDataTable(lista);
            var workbook = new XLWorkbook();
            dt.TableName = "Reporte";
            workbook.Worksheets.Add(dt);
            string filename = "Export Reporte";
            MemoryStream strem = GetStream(workbook);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("contenet_disposition","attachment; filename=" + filename + ".xlsx");
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.BinaryWrite(strem.ToArray());
            HttpContext.Current.Response.End();
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
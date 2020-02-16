using CARS.Interfaces;
using CARS.Models;
using ClosedXML.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                        if (prop.PropertyType.FullName.Contains("CARS.Models"))
                        {
                            if (prop.PropertyType.FullName.Contains("Taller"))
                            {
                                Taller taller = (Taller)item;
                                valores[posicion] = taller.MiNombre();
                            }
                            if (prop.PropertyType.FullName.Contains("Vehiculo"))
                            {
                                Vehiculo vehiculo = (Vehiculo)item;
                                valores[posicion] = vehiculo.MiNombre();
                            }
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

        public void ExportExcel1(List<IExportable> lista, HttpResponseBase Response, string nombreArchivo)
        {
            DataTable dt = ToDataTable(lista);
            var gv = new GridView();
            gv.DataSource = dt;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xlsx");
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
        }


            public void ExportToExcel(List<IExportable> lista, HttpServerUtilityBase server, HttpResponseBase response, string nombreArchivo)
        {
            DataTable dt = ToDataTable(lista);
            XLWorkbook workbook = new XLWorkbook();
            
            workbook.Worksheets.Add(dt, "Reporte");

            string myName = server.UrlEncode(nombreArchivo + ".xls");

            response.Clear();
            response.Buffer = true;
            //response.ContentType = "application/vnd.ms-excel";
            response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xlsx");
            using MemoryStream memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            memoryStream.Position = 0;
            memoryStream.WriteTo(response.OutputStream);
            response.Flush();
            response.End();
        }

        public void ToExcel(HttpResponseBase Response, List<IExportable> lista)
        {
            var grid = new GridView();
            DataTable dt = ToDataTable(lista);
            grid.DataSource = dt;
            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=FileName.xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        string GetDownloadFolderPath()
        {
            return Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString();
        }

        public MemoryStream GetStream(XLWorkbook workbook)
        {
            MemoryStream ms = new MemoryStream();
            workbook.SaveAs(ms);
            ms.Position = 0;
            return ms;
        }
    }

}
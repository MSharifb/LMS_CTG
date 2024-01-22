using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;

namespace LMS.Web
{
    public class ExportToExcel : System.Web.UI.Page
    {
        private string fileName;

        public ExportToExcel(string exportFileName)
        {
            fileName = exportFileName;
        }

        public void ExportDataToExcel(DataTable dataTobeExported, HttpResponse response)
        {
            string content = string.Empty;
            content = BuildExportContent(dataTobeExported);
            Export(content, response);

        }

        public string ExportDataToExcel(DataTable dataTobeExported, DataRow footerRow, HttpResponse response)
        {
            string content = string.Empty;
            content = BuildExportContent(dataTobeExported, footerRow);
            return content;
            //Export(content, response);
        }       

        private string BuildExportContent(DataTable dataToExport)
        {
            StringBuilder contentBuilder = new StringBuilder();

            foreach (DataColumn dc in dataToExport.Columns)
            {
                contentBuilder.Append(dc.ColumnName);
                contentBuilder.Append("\t");
            }

            contentBuilder.Append(Environment.NewLine);
            contentBuilder.Append(Environment.NewLine);

            foreach (DataRow dr in dataToExport.Rows)
            {
                foreach (DataColumn vc in dataToExport.Columns)
                {
                    contentBuilder.Append(dr[vc.ColumnName]);
                    contentBuilder.Append("\t");
                }
                contentBuilder.Length = contentBuilder.Length - 1;
                contentBuilder.Append(Environment.NewLine);
            }

            return contentBuilder.ToString();
        }

        private string BuildExportContent(DataTable dataToExport, DataRow footerRow)
        {
            StringBuilder contentBuilder = new StringBuilder();
            contentBuilder.Append("<table border='1'>");
            contentBuilder.Append("<tr style='font-weight:bold;'>");
            foreach (DataColumn dc in dataToExport.Columns)
            {
                contentBuilder.Append("<td>");
                contentBuilder.Append(dc.ColumnName);
                contentBuilder.Append("</td>");
            }
            contentBuilder.Append("</tr>");


            foreach (DataRow dr in dataToExport.Rows)
            {
                if (dr[0].ToString() == "grouptotal")
                {
                    contentBuilder.Append("<tr style='font-weight:bold;'>");
                    dr[0] = "";
                }
                else
                {
                    contentBuilder.Append("<tr>");
                }
                foreach (DataColumn vc in dataToExport.Columns)
                {
                    contentBuilder.Append("<td>");
                    contentBuilder.Append(dr[vc.ColumnName]);
                    contentBuilder.Append("</td>");
                }
                contentBuilder.Append("</tr>");
            }

            if (footerRow != null)
            {
                contentBuilder.Append("<tr style='font-weight:bold;'>");
                foreach (DataColumn vc in dataToExport.Columns)
                {
                    contentBuilder.Append("<td>");
                    contentBuilder.Append(footerRow[vc.ColumnName]);
                    contentBuilder.Append("</td>");
                }
                contentBuilder.Append("</tr>");
            }
            return contentBuilder.ToString();
        }

        private void  Export(string exportContent, HttpResponse response)
        {
            try
            {
                //response.Clear();
                //response.AddHeader("content-disposition", "attachment;filename=" + fileName);

                //response.Charset = "";
                //response.ContentType = "application/vnd.ms-excel";
                ////response.ContentType = "application/vnd.xls";

                //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

                //response.Write(exportContent);
                //response.End();

                response.ContentType = "application/force-download";
                response.AddHeader("content-disposition", "attachment; filename=Print.xls");
                response.BufferOutput = true;
                response.Write("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
                response.Write("<head>");
                response.Write("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
                response.Write("<!--[if gte mso 9]><xml>");
                response.Write("<x:ExcelWorkbook>");
                response.Write("<x:ExcelWorksheets>");
                response.Write("<x:ExcelWorksheet>");
                response.Write("<x:Name>Report Data</x:Name>");
                response.Write("<x:WorksheetOptions>");
                response.Write("<x rint>");
                response.Write("<x:ValidPrinterInfo/>");
                response.Write("</x rint>");
                response.Write("</x:WorksheetOptions>");
                response.Write("</x:ExcelWorksheet>");
                response.Write("</x:ExcelWorksheets>");
                response.Write("</x:ExcelWorkbook>");
                response.Write("</xml>");
                response.Write("<![endif]--> ");                
                response.Write(exportContent);
               // form1.innerHTML = output; // give ur html string here
                response.Write("</head>");
                response.Flush();

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Charset = "";
                //HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF7;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;Filename=" + fileName);


                //using (StringWriter sw = new StringWriter())
                //{
                //    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                //    {
                //        table.GridLines = GridLines.Both;
                //        table.RenderControl(htw);
                //        HttpContext.Current.Response.Write(sw.ToString());
                //        HttpContext.Current.Response.End();
                //    }
                //}





            }
            catch (Exception ex)
            { 
            
            }

        }

    }
}
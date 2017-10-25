using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;
using NPOI.HSSF.Util;
namespace DataDictionary
{
    public partial class POC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = "ITS_RFP";
            //str = str.Substring(str.Length-3);

            str = str.Remove(3);
            //GenearateExcel();
        }
        void GenearateExcel()
        {
            // Create a new workbook and a sheet named "Test"
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Test");
            var sheet1 = workbook.CreateSheet("ITSRFP_SendEmailToCarriers_TaskStatus");

            // Add header labels
            //var rowIndex = 0;
            //var row = sheet.CreateRow(rowIndex);
            //row.CreateCell(0).SetCellValue("Username");
            //row.CreateCell(1).SetCellValue("Email");
            //row.CreateCell(3).SetCellValue("Link");
            //rowIndex++;
            //row = sheet.CreateRow(rowIndex);
            //row.CreateCell(0).SetCellValue("Ram");
            //row.CreateCell(1).SetCellValue("nunnaramudu@gmail.com");
            //rowIndex++;

            //insert a hyperlink  
            ICellStyle hlink_style = workbook.CreateCellStyle();
            IFont hlink_font = workbook.CreateFont();
            hlink_font.Underline = NPOI.SS.UserModel.FontUnderlineType.Single;
            hlink_font.Color = HSSFColor.Blue.Index;
            hlink_style.SetFont(hlink_font);
            ICell cell;
            //URL  
            cell = sheet.CreateRow(0).CreateCell(0);
            cell.SetCellValue("URL Link");
            HSSFHyperlink link = new HSSFHyperlink(HyperlinkType.Document);
            link.Address = ("'Test2'!A1");
            cell.Hyperlink = (link);
            cell.CellStyle = (hlink_style);


            string filename = @"C:\Users\RNUNNA\Documents\Visual Studio 2013\Projects\DataDictionary\DataDictionary\Download\Test.xls";
            // Save the Excel spreadsheet to a file on the web server's file system
            using (var fileData = new FileStream(filename, FileMode.Create))
            {
                workbook.Write(fileData);
            }

            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                string saveAsFileName = string.Format("MembershipExport-{0:d}.xls", DateTime.Now).Replace("/", "-");
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }
        }
    }
}
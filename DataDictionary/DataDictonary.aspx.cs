using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;
using NPOI.HSSF.Util;
namespace DataDictionary
{
    public partial class DataDictonary : System.Web.UI.Page
    {
        string connStr = string.Empty;
        DataSet ds = new DataSet();
        string query = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["connStr"] != null)
            {
                BindDatadictonary();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindDatadictonary();
        }

        public void BindDatadictonary()
        {
            if (Session["connStr"] == null)
            {
                connStr = @"Data Source=" + txtDataSource.Text.Trim() + "; Database=" + txtDatabase.Text.Trim() + "; user id=" + txtUserId.Text.Trim() + ";password=" + txtPassword.Text.Trim() + "";
                Session["connStr"] = connStr;
            }
            else
            {
                connStr = Session["connStr"].ToString();
            }
            string curConStr = @"Data Source=" + txtDataSource.Text.Trim() + "; Database=" + txtDatabase.Text.Trim() + "; user id=" + txtUserId.Text.Trim() + ";password=" + txtPassword.Text.Trim() + "";
            //if (!curConStr.CompareTo(connStr))
            //{
            //    connStr = curConStr;
            //}

            query = @"
/* tables */
SELECT * FROM INFORMATION_SCHEMA.TABLES
select * from INFORMATION_SCHEMA.routines 
/* Triggres */
SELECT * FROM sysobjects INNER JOIN sysusers ON sysobjects.uid = sysusers.uid 
INNER JOIN sys.tables t ON sysobjects.parent_obj = t.object_id 
INNER JOIN sys.schemas s ON t.schema_id = s.schema_id 
WHERE sysobjects.type = 'TR' 
";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter da = null;
                    using (da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }

            ViewState["ds"] = ds;
            // Tables



            gvTableData.DataSource = (from DataRow row in ds.Tables[0].Rows
                                      where row["TABLE_TYPE"].ToString() == "BASE TABLE"
                                      select new { TABLE_NAME = row["TABLE_NAME"] }).ToList();
            gvTableData.DataBind();


            //Stored Proc


            gvProcData.DataSource = (from DataRow row in ds.Tables[1].Rows
                                     where row["ROUTINE_TYPE"].ToString() == "PROCEDURE"
                                     select new { ROUTINE_NAME = row["ROUTINE_NAME"] }).ToList();
            gvProcData.DataBind();


            //Functions


            gvFuncData.DataSource = (from DataRow row in ds.Tables[1].Rows
                                     where row["ROUTINE_TYPE"].ToString() == "FUNCTION"
                                     select new { ROUTINE_NAME = row["ROUTINE_NAME"] }).ToList();
            gvFuncData.DataBind();


            //Views


            gvViewData.DataSource = (from DataRow row in ds.Tables[0].Rows
                                     where row["TABLE_TYPE"].ToString() == "VIEW"
                                     select new { TABLE_NAME = row["TABLE_NAME"] }).ToList();
            gvViewData.DataBind();

            //Triggers

            gvTrigData.DataSource = (from DataRow row in ds.Tables[2].Rows
                                     select new { NAME = row["NAME"] }).ToList();
            gvTrigData.DataBind();


        }
        protected void gvTableData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTableData.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)ViewState["ds"];
            gvTableData.DataSource = (from DataRow row in ds.Tables[0].Rows
                                      where row["TABLE_TYPE"].ToString() == "BASE TABLE"
                                      select new { TABLE_NAME = row["TABLE_NAME"] }).ToList();
            gvTableData.DataBind();
        }

        protected void gvTrigData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTrigData.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)ViewState["ds"];
            gvTrigData.DataSource = (from DataRow row in ds.Tables[2].Rows
                                     select new { NAME = row["NAME"] }).ToList();
            gvTrigData.DataBind();
        }

        protected void gvViewData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvViewData.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)ViewState["ds"];
            gvViewData.DataSource = (from DataRow row in ds.Tables[0].Rows
                                     where row["TABLE_TYPE"].ToString() == "VIEW"
                                     select new { TABLE_NAME = row["TABLE_NAME"] }).ToList();
            gvViewData.DataBind();

        }

        protected void gvFuncData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFuncData.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)ViewState["ds"];
            gvFuncData.DataSource = (from DataRow row in ds.Tables[1].Rows
                                     where row["ROUTINE_TYPE"].ToString() == "FUNCTION"
                                     select new { ROUTINE_NAME = row["ROUTINE_NAME"] }).ToList();
            gvFuncData.DataBind();
        }

        protected void gvProcData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProcData.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)ViewState["ds"];
            gvProcData.DataSource = (from DataRow row in ds.Tables[1].Rows
                                     where row["ROUTINE_TYPE"].ToString() == "PROCEDURE"
                                     select new { ROUTINE_NAME = row["ROUTINE_NAME"] }).ToList();
            gvProcData.DataBind();
        }

        protected void btnTblExport_Click(object sender, EventArgs e)
        {
            /* Table Info*/
            DataSet ds = (DataSet)ViewState["ds"];
            /*Table info Detail's*/
            DataSet dsTableInfo = new DataSet();
            query = query = @"
/* Columns */
Select * From INFORMATION_SCHEMA.COLUMNS
/* getting PK,FK & depndent tables Info*/
select * from (
SELECT
    FK_Table = FK.TABLE_NAME,
    FK_Column = CU.COLUMN_NAME,
    PK_Table = PK.TABLE_NAME,
    PK_Column = PT.COLUMN_NAME,
    Constraint_Name = C.CONSTRAINT_NAME
FROM
    INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C
INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK
    ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK
    ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME
INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU
    ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
INNER JOIN (
            SELECT
                i1.TABLE_NAME,
                i2.COLUMN_NAME
            FROM
                INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1
            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2
                ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
            WHERE
                i1.CONSTRAINT_TYPE = 'PRIMARY KEY'
           ) PT
    ON PT.TABLE_NAME = PK.TABLE_NAME )A
/* Getting Dependent SP's ,Views & Triggers*/
;WITH stored_procedures AS
(
SELECT  
o.name AS proc_name, oo.name AS table_name,o.xtype,
ROW_NUMBER() OVER(partition by o.name,oo.name ORDER BY o.name,oo.name)AS row
FROM sysdepends d 
INNER JOIN sysobjects o ON o.id=d.id
INNER JOIN sysobjects oo ON oo.id=d.depid
)
SELECT proc_name, table_name,xtype FROM stored_procedures
WHERE row = 1 ORDER BY proc_name,table_name  /* Clustered & non clustered Keys*/ SELECT   so.name AS TableName, si.name AS IndexName, si.type_desc AS IndexType FROM sys.indexes si JOIN sys.objects so ON si.[object_id] = so.[object_id] WHERE so.type = 'U'    AND si.name IS NOT NULL ORDER BY so.name, si.type ";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter da = null;
                    using (da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsTableInfo);
                    }
                }
            }

            /* Table Info */

            // Create a new workbook and a sheet named "Test"
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Index");

            //insert a hyperlink  
            ICellStyle hlink_style = workbook.CreateCellStyle();
            IFont hlink_font = workbook.CreateFont();
            hlink_font.Underline = NPOI.SS.UserModel.FontUnderlineType.Single;
            hlink_font.Color = HSSFColor.Blue.Index;
            hlink_style.SetFont(hlink_font);
            ICell cell;

            /*Add header labels*/
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("SI No");
            row.CreateCell(1).SetCellValue("Table Name");
            rowIndex++;

            // Add data rows

            //select All rows
            IEnumerable<DataRow> allRows = from tblrows in ds.Tables[0].AsEnumerable() select tblrows;
            IEnumerable<DataRow> tablerows = allRows.Where(p => p.Field<string>("TABLE_TYPE") == "BASE TABLE");

            //Rows of Table structure
            IEnumerable<DataRow> allTableStrRows = from tblrows in dsTableInfo.Tables[0].AsEnumerable() select tblrows;
            IEnumerable<DataRow> allTableDependentRows = from tblrows in dsTableInfo.Tables[1].AsEnumerable() select tblrows;
            IEnumerable<DataRow> allTableDependentothersRows = from tblrows in dsTableInfo.Tables[2].AsEnumerable() select tblrows;

            foreach (DataRow datarow in tablerows)
            {
                //creating table sheets
                string hypLinkText = string.Empty;
                string sheetName = "";
                sheetName = datarow["TABLE_NAME"].ToString().Trim();
                if (sheetName.Length > 31)
                {
                    sheetName = sheetName.Remove(26).ToString();
                    sheetName = sheetName += "....";
                }
                hypLinkText = sheetName;
                sheetName = workbook.CreateSheet(sheetName).ToString();

                /* create a new Data Row */
                row = sheet.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(rowIndex.ToString());

                /* Create Table HyperLink */
                cell = sheet.CreateRow(rowIndex).CreateCell(1);
                cell.SetCellValue(hypLinkText);
                HSSFHyperlink link = new HSSFHyperlink(HyperlinkType.Document);
                link.Address = ("'" + hypLinkText + "'!A1");
                cell.Hyperlink = (link);
                cell.CellStyle = (hlink_style);
                rowIndex++;
            }

            /*Create Each Table Info */
            var tblrowIndex = 0;
            foreach (DataRow tbldatarow in tablerows)
            {
                tblrowIndex = 0;
                string tblsheetName = tbldatarow["TABLE_NAME"].ToString().Trim();
                if (tblsheetName.Length > 31)
                {
                    tblsheetName = tblsheetName.Remove(26).ToString();
                    tblsheetName = tblsheetName += "....";
                }
                /*Add header labels*/
                ISheet tblsheet = workbook.GetSheet(tblsheetName);
                var tblInforow = tblsheet.CreateRow(tblrowIndex);
                tblInforow.CreateCell(0).SetCellValue("SI No");
                tblInforow.CreateCell(1).SetCellValue("COLUMN NAME");
                tblInforow.CreateCell(2).SetCellValue("DATA TYPE");
                tblInforow.CreateCell(3).SetCellValue("IS NULLABLE");
                tblInforow.CreateCell(4).SetCellValue("MAXIMUM LENGTH");

                /*Add header labels for dependent tables*/
                tblInforow.CreateCell(5).SetCellValue("");
                tblInforow.CreateCell(6).SetCellValue("SI No");
                tblInforow.CreateCell(7).SetCellValue("Column Name");
                tblInforow.CreateCell(8).SetCellValue("Table Name");
                tblrowIndex++;

                IEnumerable<DataRow> TableStrRows = allTableStrRows.Where(p => p.Field<string>("TABLE_NAME") == tbldatarow["TABLE_NAME"].ToString());
                /* create a new Data Row */
                tblInforow = tblsheet.CreateRow(tblrowIndex);
                foreach (DataRow tblInfoRow in TableStrRows)
                {
                    tblInforow.CreateCell(0).SetCellValue((tblrowIndex.ToString()).ToString());
                    tblInforow.CreateCell(1).SetCellValue(tblInfoRow["COLUMN_NAME"].ToString());
                    tblInforow.CreateCell(2).SetCellValue(tblInfoRow["DATA_TYPE"].ToString());
                    tblInforow.CreateCell(3).SetCellValue(tblInfoRow["IS_NULLABLE"].ToString());
                    tblInforow.CreateCell(4).SetCellValue(tblInfoRow["CHARACTER_MAXIMUM_LENGTH"].ToString());
                    tblrowIndex++;
                }

                tblInforow.CreateCell(5).SetCellValue("");
                /*Create table Dependent Tables Info. */

                IEnumerable<DataRow> TableDependentRows = allTableDependentRows.Where(p => p.Field<string>("FK_Table") == tbldatarow["TABLE_NAME"].ToString());
                /* create a new Data Row */
                foreach (DataRow tblInfoRow in TableDependentRows)
                {
                    tblInforow.CreateCell(6).SetCellValue((tblrowIndex.ToString()).ToString());
                    tblInforow.CreateCell(7).SetCellValue(tblInfoRow["PK_Column"].ToString());
                    tblInforow.CreateCell(8).SetCellValue(tblInfoRow["PK_Table"].ToString());
                    tblrowIndex++;
                }
            }





            string filename = @"C:\New folder\Test_dat.xls";
            // Save the Excel spreadsheet to a file on the web server's file system
            using (var fileData = new FileStream(filename, FileMode.Create))
            {
                workbook.Write(fileData);
            }

            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                string saveAsFileName = string.Format("DatabaseDictonary-{0:d}.xls", DateTime.Now).Replace("/", "-");
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }
        }
    }
}

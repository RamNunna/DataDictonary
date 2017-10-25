using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace DataDictionary
{
    public partial class TableInfo : System.Web.UI.Page
    {
        string connStr = string.Empty;
        DataSet ds = new DataSet();
        string query = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["tablename"] != null)
            {
                BindTableInfo(Request.QueryString["tablename"].ToString());
            }
        }
        private void BindTableInfo(string tableName)
        {
            DataSet ds = new DataSet();
            connStr = Session["connStr"].ToString();
            ViewState["tableName"] = tableName;
            query = @"
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
EXEC sp_depends @objname = N'" + tableName + "' /* Clustered & non clustered Keys*/ SELECT   so.name AS TableName, si.name AS IndexName, si.type_desc AS IndexType FROM sys.indexes si JOIN sys.objects so ON si.[object_id] = so.[object_id] WHERE so.type = 'U'    AND si.name IS NOT NULL ORDER BY so.name, si.type ";

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

            ViewState["vsTable"] = ds;
            gvTavleInfo.DataSource = (from DataRow row in ds.Tables[0].Rows
                                      where row["TABLE_NAME"].ToString() == tableName
                                      select new
                                      {
                                          COLUMN_NAME = row["COLUMN_NAME"],
                                          DATA_TYPE = row["DATA_TYPE"],
                                          IS_NULLABLE = row["IS_NULLABLE"],
                                          CHARACTER_MAXIMUM_LENGTH = row["CHARACTER_MAXIMUM_LENGTH"]
                                      }).ToList();
            gvTavleInfo.DataBind();

            gvDependentTables.DataSource = (from DataRow row in ds.Tables[1].Rows
                                            where row["FK_Table"].ToString() == tableName
                                            select new
                                            {
                                                PK_Table = row["PK_Table"],
                                                PK_Column = row["PK_Column"]
                                            }).ToList();
            gvDependentTables.DataBind();

            gvDependentOthers.DataSource = (from DataRow row in ds.Tables[1].Rows
                                            where row["PK_Table"].ToString() == tableName
                                            select new
                                            {
                                                FK_Table = row["FK_Table"],
                                                FK_Column = row["FK_Column"]
                                            }).ToList();
            gvDependentOthers.DataBind();

            dependentProc.DataSource = (from DataRow row in ds.Tables[2].Rows
                                        where row["type"].ToString() == "stored procedure"
                                        select new
                                        {
                                            Name = row["name"]
                                        }).ToList();
            dependentProc.DataBind();

            gvDependentViews.DataSource = (from DataRow row in ds.Tables[2].Rows
                                        where row["type"].ToString() == "view"
                                        select new
                                        {
                                            Name = row["name"]
                                        }).ToList();
            gvDependentViews.DataBind();

            gvDenTriggers.DataSource = (from DataRow row in ds.Tables[2].Rows
                                        where row["type"].ToString() == "trigger"
                                        select new
                                        {
                                            Name = row["name"]
                                        }).ToList();
            gvDenTriggers.DataBind();

            grdIndexes.DataSource = (from DataRow row in ds.Tables[3].Rows
                                     where row["TableName"].ToString() == tableName
                                     select new
                                     {
                                         IndexName = row["IndexName"],
                                         IndexType = row["IndexType"],
                                     }).ToList();
            grdIndexes.DataBind();

        }

        protected void gvTavleInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTavleInfo.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)ViewState["vsTable"];
            gvTavleInfo.DataSource = (from DataRow row in ds.Tables[0].Rows
                                      where row["TABLE_NAME"].ToString() == ViewState["tableName"].ToString()
                                      select new
                                      {
                                          COLUMN_NAME = row["COLUMN_NAME"],
                                          DATA_TYPE = row["DATA_TYPE"],
                                          IS_NULLABLE = row["IS_NULLABLE"],
                                          CHARACTER_MAXIMUM_LENGTH = row["CHARACTER_MAXIMUM_LENGTH"]
                                      }).ToList();
            gvTavleInfo.DataBind();
        }


        protected void gvDependentTables_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDependentTables.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)ViewState["vsTable"];
            gvDependentTables.DataSource = (from DataRow row in ds.Tables[1].Rows
                                            where row["FK_Table"].ToString() == ViewState["tableName"].ToString()
                                            select new
                                            {
                                                PK_Table = row["PK_Table"],
                                                PK_Column = row["PK_Column"]
                                            }).ToList();
            gvDependentTables.DataBind();
        }

        protected void gvDependentOthers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDependentOthers.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)ViewState["vsTable"];
            gvDependentOthers.DataSource = (from DataRow row in ds.Tables[1].Rows
                                            where row["PK_Table"].ToString() == ViewState["tableName"].ToString()
                                            select new
                                            {
                                                FK_Table = row["FK_Table"],
                                                FK_Column = row["FK_Column"]
                                            }).ToList();
            gvDependentOthers.DataBind();
        }

        protected void dependentProc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvDependentViews_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvDenTriggers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

    }
}
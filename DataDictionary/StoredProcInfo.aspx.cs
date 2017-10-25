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
    public partial class StoredProcInfo : System.Web.UI.Page
    {
        string connStr = string.Empty;
        DataSet ds = new DataSet();
        string query = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["procname"] != null)
            {
                BindProcInfo(Request.QueryString["procname"].ToString());
            }
        }
        private void BindProcInfo(string procName)
        {
            DataSet ds = new DataSet();
            connStr = Session["connStr"].ToString();
            ViewState["procName"] = procName;
            query = @"
/* Stored Proc parameters */
select * from information_schema.parameters
/*Proc inside tables*/

;WITH stored_procedures AS
(
SELECT  
o.name AS proc_name, oo.name AS table_name,oo.xtype,
ROW_NUMBER() OVER(partition by o.name,oo.name ORDER BY o.name,oo.name)AS row
FROM sysdepends d 
INNER JOIN sysobjects o ON o.id=d.id
INNER JOIN sysobjects oo ON oo.id=d.depid
)
SELECT proc_name, table_name,xtype FROM stored_procedures
WHERE row = 1 and proc_name in('" + procName + "') ORDER BY proc_name,table_name ";

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

            ViewState["vsStoreProc"] = ds;
            gvPrcoInfo.DataSource = (from DataRow row in ds.Tables[0].Rows
                                     where row["SPECIFIC_NAME"].ToString() == procName
                                     select new
                                     {
                                         PARAMETER_NAME = row["PARAMETER_NAME"],
                                         DATA_TYPE = row["DATA_TYPE"],
                                         CHARACTER_MAXIMUM_LENGTH = row["CHARACTER_MAXIMUM_LENGTH"]
                                     }).ToList();
            gvPrcoInfo.DataBind();

            gvDependentTables.DataSource = (from DataRow row in ds.Tables[1].Rows
                                            where row["xtype"].ToString().Contains("U")
                                            select new
                                            {
                                                table_name = row["table_name"]
                                            }).ToList();
            gvDependentTables.DataBind();

            gvDependentOthers.DataSource = (from DataRow row in ds.Tables[1].Rows
                                            where row["xtype"].ToString().Contains("P")
                                            select new
                                            {
                                                table_name = row["table_name"]
                                            }).ToList();
            gvDependentOthers.DataBind();

            gvDependentViews.DataSource = (from DataRow row in ds.Tables[1].Rows
                                           where row["xtype"].ToString().Contains("V")
                                           select new
                                           {
                                               table_name = row["table_name"]
                                           }).ToList();
            gvDependentViews.DataBind();
            gvDenTriggers.DataSource = (from DataRow row in ds.Tables[1].Rows
                                        where row["xtype"].ToString().Contains("TR")
                                        select new
                                        {
                                            table_name = row["table_name"]
                                        }).ToList();
            gvDenTriggers.DataBind();

        }

        protected void gvPrcoInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPrcoInfo.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)ViewState["vsStoreProc"];
            gvPrcoInfo.DataSource = (from DataRow row in ds.Tables[0].Rows
                                     where row["specific_name"].ToString() == ViewState["procName"].ToString()
                                     select new
                                     {
                                         PARAMETER_NAME = row["PARAMETER_NAME"],
                                         DATA_TYPE = row["DATA_TYPE"],
                                         CHARACTER_MAXIMUM_LENGTH = row["CHARACTER_MAXIMUM_LENGTH"]
                                     }).ToList();
            gvPrcoInfo.DataBind();
        }


        protected void gvDependentTables_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDependentTables.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)ViewState["vsStoreProc"];
            gvDependentTables.DataSource = (from DataRow row in ds.Tables[1].Rows
                                            where row["xtype"].ToString().Contains("U")
                                            select new
                                            {
                                                table_name = row["table_name"]
                                            }).ToList();
            gvDependentTables.DataBind();
        }

        protected void gvDependentOthers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDependentOthers.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)ViewState["vsStoreProc"];
            gvDependentOthers.DataSource = (from DataRow row in ds.Tables[1].Rows
                                            where row["xtype"].ToString().Contains("P")
                                            select new
                                            {
                                                table_name = row["table_name"]
                                            }).ToList();
            gvDependentOthers.DataBind();
        }

        protected void gvDependentViews_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvDenTriggers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        //protected void dependentProc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{

        //}
    }
}
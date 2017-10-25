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
    public partial class ViewInfo : System.Web.UI.Page
    {
        string connStr = string.Empty;
        DataSet ds = new DataSet();
        string query = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["viewname"] != null)
            {
                BindProcInfo(Request.QueryString["viewname"].ToString());
            }
        }
        private void BindProcInfo(string viewname)
        {
            DataSet ds = new DataSet();
            connStr = Session["connStr"].ToString();
            ViewState["viewname"] = viewname;
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
WHERE row = 1 and proc_name in('" + viewname + "') ORDER BY proc_name,table_name ";

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

            ViewState["vsview"] = ds;
       

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



        protected void gvDependentTables_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDependentTables.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)ViewState["vsview"];
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
            DataSet ds = (DataSet)ViewState["vsview"];
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
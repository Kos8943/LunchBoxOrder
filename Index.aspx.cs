using LunchBoxOrder.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LunchBoxOrder
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //DataBaseMethods methods = new DataBaseMethods();
            //this.GroupRepeater.DataSource = methods.GetGroup();
            //this.GroupRepeater.DataBind();


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string queryPage = Request.QueryString["Page"];
            string querySearch = Request.QueryString["GroupName"];
            int currentPage;
            int total;
            if (!Int32.TryParse(queryPage, out currentPage))
            {
                currentPage = 1;
            }

            if (string.IsNullOrWhiteSpace(querySearch))
            {
                querySearch = string.Empty;
            }

            
            DataBaseMethods methods = new DataBaseMethods();
            this.GroupRepeater.DataSource = methods.GetGroup(out total, querySearch, currentPage);
            ChangePages.TotalSize = total;
            this.GroupRepeater.DataBind();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (LoginHelper.HasLogined())
            {
                Response.Write("<Script language='JavaScript'>alert('已經登入囉~');</Script>");
                return;
            }

            Response.Redirect("~/Login.aspx");
        }

        protected void CreatGroup_Click(object sender, EventArgs e)
        {
            

            Response.Redirect("~/CreatOrder.aspx");
        }

        protected void GroupRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyWord = this.TxtSearch.Text;

            if (!string.IsNullOrEmpty(keyWord))
            {
                Response.Redirect($"~/Index.aspx?Page=1&GroupName={keyWord}");
            }
            else
            {
                Response.Redirect($"~/Index.aspx?Page=1");
            }
        }

        protected void GroupRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dr = e.Item.DataItem as DataRowView;
            Repeater menuRepeater = (Repeater)e.Item.FindControl("MenuRepeater");
            int sid = Convert.ToInt32(dr["Sid"]);
            DataBaseMethods methods = new DataBaseMethods();
            menuRepeater.DataSource = methods.GetShopMenu(sid);
            menuRepeater.DataBind();
        }
    }
}
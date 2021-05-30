using LunchBoxOrder.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LunchBoxOrder
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBaseMethods methods = new DataBaseMethods();
            this.GroupRepeater.DataSource = methods.GetGroup();
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
    }
}
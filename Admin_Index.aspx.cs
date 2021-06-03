using LunchBoxOrder.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LunchBoxOrder
{
    public partial class Admin_Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!LoginHelper.HasLogined())
            {
                Response.Redirect("~/Index.aspx");
            }

            LoginInfo loginInfo = HttpContext.Current.Session["IsLogined"] as LoginInfo;

            this.title.InnerText = $"您好{loginInfo.UserName},歡迎進入後台管理~";
        }
    }
}
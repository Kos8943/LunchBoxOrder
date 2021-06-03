using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LunchBoxOrder
{
    public partial class Admin_UserAccountList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBaseMethods methods = new DataBaseMethods();
                int totalData;
                this.UserAccountRepeater.DataSource = methods.GetUserAccount(out totalData, null, 1, 10);
                this.UserAccountRepeater.DataBind();
            }
            
        }

        protected void UserAccountRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string commName = e.CommandName;
            string commArug = e.CommandArgument.ToString();

            if("Modify" == commName)
            {
                Response.Redirect($"~/Admin_CreateUserAccount.aspx?AccountSid={commArug}");
            }

            if("Delete" == commName)
            {

            }
        }

        protected void Create_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin_CreateUserAccount.aspx");
        }
    }
}
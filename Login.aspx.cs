using LunchBoxOrder.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LunchBoxOrder
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string account = this.Account.Text;
            string password = this.Password.Text;

            if(string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                this.ErrorMsg.Text = "請輸入帳號密碼";
                this.ErrorMsg.Visible = true;
                return;
            }

            if(LoginHelper.TryLogin(account, password))
            {
                Response.Redirect("~/Index.aspx");
            }
            else
            {
                this.ErrorMsg.Text = "帳號或密碼錯誤";
                this.ErrorMsg.Visible = true;
                return;
            }
        }
    }
}
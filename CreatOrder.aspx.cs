using LunchBoxOrder.Helper;
using LunchBoxOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LunchBoxOrder
{
    public partial class CreatOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!LoginHelper.HasLogined())
            //{
            //    Response.Write("<Script language='JavaScript'>alert('請先登入帳號');</Script>");
            //    Response.Write("<script language=javascript>window.location.href='Login.aspx'</script>");
            //}

            DataBaseMethods methods = new DataBaseMethods();
            this.ShopName.DataSource = methods.GetAllShop();
            this.ShopName.DataTextField = "ShopName";
            this.ShopName.DataValueField = "Sid";
            this.ShopName.DataBind();

        }

        protected void CreateGroup_Click(object sender, EventArgs e)
        {
            string GroupName = this.GroupName.Text;

            if (string.IsNullOrWhiteSpace(GroupName))
            {
                this.ErrorMsg.Text = "請輸入團名";
                this.ErrorMsg.Visible = true;
                return;
            }

            GroupModel model = new GroupModel();

            LoginInfo loginInfo = HttpContext.Current.Session["IsLogined"] as LoginInfo;
            //取得session的使用者權限
            model.AccountSid = loginInfo.Sid;
            model.GroupLeader = loginInfo.UserName;
            model.ShopSid = Convert.ToInt32(this.ShopName.SelectedValue);
            model.GroupImgName = this.FoodDropList.SelectedValue;
            model.GroupName = GroupName;

            DataBaseMethods methods = new DataBaseMethods();
            methods.CreateGroup(model);

            Response.Write("<Script language='JavaScript'>alert('開團成功');</Script>");
            Response.Write("<script language=javascript>window.location.href='Index.aspx'</script>");

        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/index.aspx");
        }

        protected void FoodImgs_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FoodImg.Src = $"Imgs/{this.FoodDropList.SelectedValue}";
        }
    }
}
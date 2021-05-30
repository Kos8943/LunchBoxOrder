using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LunchBoxOrder
{
    public partial class CheckOrder : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            string queryString = Request.QueryString["Sid"];
            int Sid = 0;

            if (string.IsNullOrWhiteSpace(queryString))
            {
                Response.Redirect("~/Index.aspx");
            }

            DataBaseMethods methods = new DataBaseMethods();

            if(Int32.TryParse(queryString, out Sid))
            {
                DataTable dt = methods.GetSingleGroupDetail(Sid);
                this.GroupName.InnerText = dt.Rows[0]["GroupName"].ToString();
                this.ShopName.Text = $"店名: {dt.Rows[0]["ShopName"]}";
                this.GroupLeader.Text = $"主揪: {dt.Rows[0]["GroupLeader"]}";
                this.GroupImg.Src = $"~/Imgs/{dt.Rows[0]["GroupImgName"]}";
                this.GroupStatusDropList.SelectedValue = dt.Rows[0]["GroupStatus"].ToString();

                this.MenuRepeater.DataSource = dt;
                this.MenuRepeater.DataBind();

                this.OrderRepeater.DataSource = methods.GetOrder(Sid);
                this.OrderRepeater.DataBind();
            }
            


        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OrderRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = e.Item.DataItem as DataRowView;
                int accountSid = Convert.ToInt32(dr["AccountSid"]);
                Repeater OrderDetailRepeater = (Repeater)e.Item.FindControl("OrderDetailRepeater");

                DataBaseMethods methods = new DataBaseMethods();
                OrderDetailRepeater.DataSource = methods.GetOrderDetail(accountSid);
                OrderDetailRepeater.DataBind();
            }
        }

        protected void OrderRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}
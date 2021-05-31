using LunchBoxOrder.Helper;
using LunchBoxOrder.Models;
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
        static List<OrderModel> orderModels = new List<OrderModel>();
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

                this.TotalCountRepeater.DataSource = methods.GetCountTotal(Sid);
                this.TotalCountRepeater.DataBind();
            }
            


        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OrderRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int GroupSid = Convert.ToInt32(Request.QueryString["Sid"]);
                DataRowView dr = e.Item.DataItem as DataRowView;
                int accountSid = Convert.ToInt32(dr["AccountSid"]);
                Repeater OrderDetailRepeater = (Repeater)e.Item.FindControl("OrderDetailRepeater");

                DataBaseMethods methods = new DataBaseMethods();
                DataTable dt = methods.GetOrderDetail(accountSid, GroupSid);

                List<OrderModel> model = new List<OrderModel>();

                foreach(DataRow item in dt.Rows)
                {
                    if(model.FindIndex(obj => obj.FoodName == item["FoodName"].ToString()) == -1)
                    {
                        OrderModel orderModel = new OrderModel();
                        orderModel.FoodName = item["FoodName"].ToString();
                        orderModel.Qty = Convert.ToInt32(item["Qty"]);
                        model.Add(orderModel);
                    }
                    else
                    {
                        model[model.FindIndex(obj => obj.FoodName == item["FoodName"].ToString())].Qty += Convert.ToInt32(item["Qty"]);
                    }
                }

                OrderDetailRepeater.DataSource = model;
                OrderDetailRepeater.DataBind();
            }
        }

        protected void OrderRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string comName = e.CommandName;
            string[] comarug = e.CommandArgument.ToString().Split(',');
            DataBaseMethods methods = new DataBaseMethods();
            if ("Delete" == comName)
            {
                methods.DelectOrderDetail(Convert.ToInt32(comarug[0]), Convert.ToInt32(comarug[1]));
            }
            string Sid = Request.QueryString["Sid"];
            Response.Redirect($"~/CheckOrder.aspx?Sid={Sid}");
        }

        protected void QtyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dish = sender as DropDownList;
            string[] dishToolTip = dish.ToolTip.Split(',');
            string queryGroupSid = Request.QueryString["Sid"];
            int menuSid = Convert.ToInt32(dishToolTip[1]);
            int Sid;
            if(!Int32.TryParse(queryGroupSid, out Sid))
            {
                Response.Redirect("~/Index.aspx");
            }

            LoginInfo loginInfo = HttpContext.Current.Session["IsLogined"] as LoginInfo;


            if (orderModels.FindIndex(obj => obj.FoodName == dishToolTip[0]) == -1)
            {
                OrderModel model = new OrderModel();
                model.Qty = Convert.ToInt32(dish.SelectedValue);
                model.AccountSid = loginInfo.Sid;
                model.WhoOrder = loginInfo.UserName;
                model.GroupSid = Sid;
                model.MenuSid = menuSid;
                model.FoodName = dishToolTip[0];
                orderModels.Add(model);
            }
            else
            {
                orderModels[orderModels.FindIndex(obj => obj.FoodName == dishToolTip[0])].Qty = Convert.ToInt32(dish.SelectedValue);
            }


            this.OrderConfirmRepeater.DataSource = orderModels;
            this.OrderConfirmRepeater.DataBind();

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string Sid = Request.QueryString["Sid"];
            DataBaseMethods methods = new DataBaseMethods();
            methods.InsertOrder(orderModels);
            orderModels.Clear();
            Response.Redirect($"~/CheckOrder.aspx?Sid={Sid}");
        }

        int totalPrice;
        protected void TotalCountRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = e.Item.DataItem as DataRowView;
                totalPrice += Convert.ToInt32(dr["Qty"]) * Convert.ToInt32(dr["Price"]);

            }

            if(e.Item.ItemType == ListItemType.Footer)
            {
                Literal literal = (Literal)e.Item.FindControl("TotalPriceLiteral");

                literal.Text = $"總計:{totalPrice}";
            }
            
        }
    }
}
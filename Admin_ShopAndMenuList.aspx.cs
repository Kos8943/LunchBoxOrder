using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LunchBoxOrder
{
    public partial class Admin_ShopAndMenuList : System.Web.UI.Page
    {
        DataBaseMethods methods = new DataBaseMethods();
        protected void Page_Init(object sender, EventArgs e)
        {
            var dt = methods.Admin_GetAllShop();

            this.ShopDropDownList.DataSource = dt;
            this.ShopDropDownList.DataTextField = "ShopName";
            this.ShopDropDownList.DataValueField = "Sid";
            this.ShopDropDownList.DataBind();

            string queryShopSid = Request.QueryString["ShopSid"];
            int ShopSid = 0;

            

            if (!string.IsNullOrWhiteSpace(queryShopSid))
            {
                Int32.TryParse(queryShopSid, out ShopSid);
                this.ShopDropDownList.SelectedValue = queryShopSid;
            }
            else
            {
                if (dt.Rows.Count != 0)
                {
                    ShopSid = Convert.ToInt32(dt.Rows[0]["Sid"]);
                }
            }
            

            int? totalMenu;
            this.ShopMenuRepeater.DataSource = methods.Admin_GetShopMenu(out totalMenu, ShopSid);
            this.ShopMenuRepeater.DataBind();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void ShopDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string shopSid = this.ShopDropDownList.SelectedValue;

            Response.Redirect($"~/Admin_ShopAndMenuList.aspx?Page=1&ShopSid={shopSid}");
        }
    }
}
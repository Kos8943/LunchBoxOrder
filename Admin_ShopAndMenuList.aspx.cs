using LunchBoxOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LunchBoxOrder
{
    public partial class Admin_ShopAndMenuList : System.Web.UI.Page
    {
        DataBaseMethods methods = new DataBaseMethods();
        private string[] _allowFileExe = { ".jpg", ".png", ".gif" };
        private string _saveFolder = "~/Imgs";
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

        protected void btnCreateShop_Click(object sender, EventArgs e)
        {
            string shopName = this.ShopName.Text;
            string foodName = this.FoodName.Text;
            string txtPrice = this.Price.Text;
            var foodImg = this.FoodImgFile;

            bool canSubmit = true;
            ShopModel shopModel = new ShopModel();
            MenuModel muneModel = new MenuModel();
            if (!string.IsNullOrWhiteSpace(shopName))
            {

                shopModel.ShopName = shopName;              
            }
            else
            {
                this.LabelMsg.Text = "請輸入店名";
                this.LabelMsg.Visible = true;
                canSubmit = false;
            }

            if (!string.IsNullOrWhiteSpace(foodName))
            {
                muneModel.FoodName = foodName;
            }
            else
            {
                this.FoodNameLabel.Text = "請輸入餐點名稱";
                this.FoodNameLabel.Visible = true;
                canSubmit = false;
            }

            if (!string.IsNullOrWhiteSpace(txtPrice))
            {
                int price;
                if(Int32.TryParse(txtPrice, out price))
                {
                    muneModel.Price = price;
                }
                else
                {
                    this.PriceLabel.Text = "請輸入數字";
                    this.PriceLabel.Visible = true;
                    canSubmit = false;
                }
            }
            else
            {
                this.PriceLabel.Text = "請輸入單價";
                this.PriceLabel.Visible = true;
                canSubmit = false;
            }

            if (foodImg.HasFile)
            {
                if (CheckPhotoExe(System.IO.Path.GetExtension(foodImg.FileName)))
                {
                    
                    muneModel.FoodImgName = GetNewFileName(foodImg, "");
                }
                else
                {
                    this.FoodImgFileLabel.Text = "圖片格式錯誤";
                    this.FoodImgFileLabel.Visible = true;
                    canSubmit = false;
                }
            }
            else
            {
                this.FoodImgFileLabel.Text = "請上傳餐點圖片";
                this.FoodImgFileLabel.Visible = true;
                canSubmit = false;
            }


            if (canSubmit)
            {
                methods.CreateNewShop(shopModel, muneModel);
                Response.Write("<Script language='JavaScript'>alert('新增成功');</Script>");
                Response.Write($"<script language=javascript>window.location.href='Admin_ShopAndMenuList.aspx'</script>");
            }
        }

        private bool CheckPhotoExe(string fileName)
        {
            bool flag = false;

            for (int i = 0; i < _allowFileExe.Length; i++)
            {
                if (_allowFileExe[i].ToString().Equals(fileName))
                {
                    flag = true;
                }
            }

            return flag;
        }

        private string GetNewFileName(FileUpload fileUpload, string oldFileName = "")
        {
            var fileName = fileUpload.FileName;
            string fileExt = System.IO.Path.GetExtension(fileName);


            string path = Server.MapPath(_saveFolder);
            string newFileName;

            if (string.IsNullOrWhiteSpace(oldFileName))
            {
                newFileName = DateTime.Now.Ticks.ToString() + fileExt;
            }
            else
            {
                newFileName = oldFileName;
            }

            string fullPath = System.IO.Path.Combine(path, newFileName);

            fileUpload.SaveAs(fullPath);

            return newFileName;
        }
    }
}
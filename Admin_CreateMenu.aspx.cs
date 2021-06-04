using LunchBoxOrder.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LunchBoxOrder
{
    public partial class Admin_CreateMenu : System.Web.UI.Page
    {
        bool IsCreateMode = true;
        private string[] _allowFileExe = { ".jpg", ".png", ".gif" };
        private string _saveFolder = "~/Imgs";
        DataBaseMethods methods = new DataBaseMethods();
        private string _oldImgName = string.Empty;
        protected void Page_Init(object sender, EventArgs e)
        {
            var dt = methods.Admin_GetAllShop();

            this.ShopDropDownList.DataSource = dt;
            this.ShopDropDownList.DataTextField = "ShopName";
            this.ShopDropDownList.DataValueField = "Sid";
            this.ShopDropDownList.DataBind();

            string menuQueryString = Request.QueryString["menuSid"];
            
            if (string.IsNullOrWhiteSpace(menuQueryString))
            {
                return;
            }
            else
            {
                IsCreateMode = false;
                int menuSid;
                if(Int32.TryParse(menuQueryString, out menuSid))
                {
                    this.ShopDropDownList.Enabled = false;
                    DataTable menuDataTable = methods.Admin_GetSingleMenu(menuSid);

                    if(menuDataTable.Rows.Count != 0)
                    {
                        this.ShopDropDownList.SelectedValue = menuDataTable.Rows[0]["ShopSid"].ToString();
                        this.FoodName.Text = menuDataTable.Rows[0]["FoodName"].ToString();
                        this.Price.Text = menuDataTable.Rows[0]["Price"].ToString();
                        this._oldImgName = menuDataTable.Rows[0]["FoodImg"].ToString();
                    }
                    else
                    {
                        Response.Redirect("~/Admin_CreateMenu.aspx");
                    }
                    
                }
                else
                {
                    Response.Redirect("~/Admin_CreateMenu.aspx");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnCreateShop_Click(object sender, EventArgs e)
        {
            MenuModel menuModel = new MenuModel();
            int shopSid = Convert.ToInt32(this.ShopDropDownList.SelectedValue);
            string foodName = this.FoodName.Text;
            string txtPrice = this.Price.Text;
            var foodImg = this.FoodImgFile;
            bool canSubmit = true;

            menuModel.ShopSid = shopSid;

            if (!string.IsNullOrWhiteSpace(foodName))
            {
                menuModel.FoodName = foodName;
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
                    menuModel.Price = price;
                }
                else
                {
                    this.PriceLabel.Text = "只能輸入數字";
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
                if (this.CheckPhotoExe(Path.GetExtension(foodImg.FileName).ToLower()))
                {
                    if (IsCreateMode)
                    {
                        menuModel.FoodImgName = this.GetNewFileName(foodImg, "");
                    }
                    else
                    {
                        menuModel.FoodImgName = this.GetNewFileName(foodImg, _oldImgName);
                    }
                    
                }
                else
                {
                    this.FoodImgFileLabel.Text = "上傳圖片格式錯誤";
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
                if (IsCreateMode)
                {
                    methods.Admin_CreateNewMenu(menuModel);
                    this.submitMsg.Text = "新增成功";
                    this.submitMsg.Visible = true;

                }
                else
                {
                    menuModel.Sid = Convert.ToInt32(Request.QueryString["menuSid"]);
                    methods.Admin_UpDateMenu(menuModel);
                    this.submitMsg.Text = "修改成功";
                    this.submitMsg.Visible = true;
                }
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin_ShopAndMenuList.aspx");
        }
    }
}
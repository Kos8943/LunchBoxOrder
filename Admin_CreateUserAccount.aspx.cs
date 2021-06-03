using LunchBoxOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

namespace LunchBoxOrder
{
    public partial class Admin_CreateUserAccount : System.Web.UI.Page
    {
        bool IsCreateMode = true;
        private string[] _allowFileExe = { ".jpg", ".png", ".gif" };
        private string _saveFolder = "~/Imgs";
        DataBaseMethods methods = new DataBaseMethods();
        private string _oldImgName = string.Empty; 
        protected void Page_Load(object sender, EventArgs e)
        {
            string queryAccountSid = Request.QueryString["AccountSid"];

            if (string.IsNullOrWhiteSpace(queryAccountSid))
            {
                return;
            }
            else
            {
                int AccountSid;
                if(Int32.TryParse(queryAccountSid, out AccountSid))
                {

                    DataTable dt = methods.GetSingleAccount(AccountSid);
                    if(dt.Rows.Count != 0)
                    {
                        this.txtAccount.Text = dt.Rows[0]["Account"].ToString();
                        this.txtAccount.Enabled = false;
                        this.txtUserName.Text = dt.Rows[0]["UserName"].ToString();

                        if("1" == dt.Rows[0]["IsAdmin"].ToString())
                        {
                            this.CheckBoxIsAdmin.Checked = true;
                        }
                        else
                        {
                            this.CheckBoxIsAdmin.Checked = false;
                        }
                        _oldImgName = dt.Rows[0]["UserImgName"].ToString();
                        IsCreateMode = false;
                        
                    }
                    else
                    {
                        Response.Redirect("~/Admin_UserAccountList.aspx");
                    }
                }
            }


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string account = this.txtAccount.Text;
            string passWord = this.txtPassword.Text;
            string userName = this.txtUserName.Text;
            bool IsAdmin = this.CheckBoxIsAdmin.Checked;
            bool allDataIsOK = true;
            var imgFile = this.UserImgFile;

            AccountModel model = new AccountModel();
            Regex regex = new Regex(@"^[A-Za-z0-9]");

            if (!string.IsNullOrWhiteSpace(account))
            {
                if (regex.IsMatch(account))
                {
                    model.Account = account;
                }
                else
                {
                    this.ltAccount.Text = "只能輸入英數字";
                    this.ltAccount.Visible = false;
                    allDataIsOK = false;
                }
            }
            else
            {
                this.ltAccount.Text = "請輸入帳號";
                this.ltAccount.Visible = true;
                allDataIsOK = false;
            }

            if (!string.IsNullOrWhiteSpace(passWord))
            {
                if (regex.IsMatch(passWord))
                {
                    model.Password = passWord;
                }
                else
                {
                    this.ltPassword.Text = "請輸入英數字";
                    this.ltPassword.Visible = true;
                    allDataIsOK = false;
                }
            }
            else
            {
                this.ltPassword.Text = "請輸入密碼";
                this.ltPassword.Visible = true;
                allDataIsOK = false;
            }

            if (!string.IsNullOrWhiteSpace(userName))
            {
                model.UserName = userName;
            }
            else
            {
                this.ltUserName.Text = "請輸入名稱";
                this.ltUserName.Visible = true;
                allDataIsOK = false;
            }


            if (IsAdmin)
            {
                model.IsAdmin = 1;
            }
            else
            {
                model.IsAdmin = 0;
            }

            if (UserImgFile.HasFile)
            {
                if (this.CheckPhotoExe(Path.GetExtension(imgFile.FileName).ToLower()))
                {
                    string fileName = string.Empty;

                    if (IsCreateMode)
                    {
                        fileName = this.GetNewFileName(imgFile);
                    }
                    else
                    {
                        fileName = this.GetNewFileName(imgFile, _oldImgName);
                    }
                    

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        model.UserImgName = fileName;
                    }
                    else
                    {
                        this.ltUploadImg.Text = "上傳圖片格式錯誤";
                        this.ltUploadImg.Visible = true;
                        allDataIsOK = false;
                    }
                }
                else
                {
                    this.ltUploadImg.Text = "上傳圖片格式錯誤";
                    this.ltUploadImg.Visible = true;
                    allDataIsOK = false;
                }
            }
            else
            {
                this.ltUploadImg.Text = "請上傳圖片";
                this.ltUploadImg.Visible = true;
                allDataIsOK = false;
            }

            if (allDataIsOK)
            {
                if (this.IsCreateMode)
                {
                   
                    methods.InsertNewAccount(model);
                    this.ltMsg.Text = "新增成功";
                    this.ltMsg.Visible = true;
                }
                else
                {
                    int AccountSid = Convert.ToInt32(Request.QueryString["AccountSid"]);
                    methods.UpDateAccount(model, AccountSid);
                    this.ltMsg.Text = "修改成功";
                    this.ltMsg.Visible = true;
                }
            }
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private bool CheckPhotoExe(string fileName)
        {
            bool flag = false;

            for(int i = 0; i < _allowFileExe.Length; i++)
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
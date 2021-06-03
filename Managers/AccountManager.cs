using LunchBoxOrder.Helper;
using LunchBoxOrder.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LunchBoxOrder.Managers
{
    public class AccountManager:DataBaseHelper
    {
        public AccountModel GetAccount(string Account)
        {
            //string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Yubay_Drone; Integrated Security=true";
            string queryString =
                $@" SELECT * FROM UserAccount
                    WHERE Account = @Account
                ";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Account", Account)
                };

            var dt = this.GetDataTable(queryString, parameters);

            AccountModel model = new AccountModel();

            if (dt.Rows.Count > 0)
            {

                if (!Convert.IsDBNull(dt.Rows[0]["Sid"]))
                {
                    model.Sid = (int)dt.Rows[0]["Sid"];
                }

                if (!Convert.IsDBNull(dt.Rows[0]["Account"]))
                {
                    model.Account = (string)dt.Rows[0]["Account"];
                }

                if (!Convert.IsDBNull(dt.Rows[0]["Password"]))
                {
                    model.Password = (string)dt.Rows[0]["Password"];
                }

                if (!Convert.IsDBNull(dt.Rows[0]["UserName"]))
                {
                    model.UserName = (string)dt.Rows[0]["UserName"];
                }

                if (!Convert.IsDBNull(dt.Rows[0]["UserImgName"]))
                {
                    model.UserImgName = (string)dt.Rows[0]["UserImgName"];
                }

                if (!Convert.IsDBNull(dt.Rows[0]["IsAdmin"]))
                {
                    model.IsAdmin = (int)dt.Rows[0]["IsAdmin"];
                }
            }
            return model;
        }
    }
}
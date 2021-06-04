using LunchBoxOrder.Helper;
using LunchBoxOrder.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LunchBoxOrder
{
    public class DataBaseMethods:DataBaseHelper
    {
        public DataTable GetAllShop()
        {
            string queryString = $@" SELECT * FROM Shop";

            List<SqlParameter> parameters = new List<SqlParameter>();

                //{
                //   new SqlParameter("@Sid", Drone_ID),
                //};

            var dt = this.GetDataTable(queryString, parameters);

            return dt;
        }


        public void CreateGroup(GroupModel model)
        {
            string queryString = $@"INSERT INTO [Group](AccountSid, GroupLeader, ShopSid, GroupImgName, GroupName, GroupStatus)
                                                VALUES(@AccountSid, @GroupLeader, @ShopSid, @GroupImgName, @GroupName, @GroupStatus);";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@AccountSid", model.AccountSid),
                new SqlParameter("@GroupName", model.GroupName),
                new SqlParameter("@GroupLeader", model.GroupLeader),
                new SqlParameter("@ShopSid", model.ShopSid),
                new SqlParameter("@GroupImgName", model.GroupImgName),               
                new SqlParameter("@GroupStatus", model.GroupStatus)
            };

            this.ExecuteNonQuery(queryString, parameters);
        }

        public DataTable GetGroup(out int total, string groupName, int currentPage = 1, int pageSize = 5)
        {
            //string queryString = $@"SELECT * FROM [Group] JOIN Shop ON [Group].ShopSid = Shop.Sid;";
            string wherestring = string.Empty;
            if (!string.IsNullOrWhiteSpace(groupName))
            {
                wherestring = "AND GroupName LIKE @GroupName";
            }

            string queryString = $@"SELECT TOP 5 * FROM 
                                    (SELECT [Group].[Sid], [Group].GroupImgName, [Group].GroupName, Shop.ShopName,
                                    ROW_NUMBER() OVER(ORDER BY [Group].[Sid] )  AS ROWSID FROM [Group]
                                    JOIN Shop ON [Group].ShopSid = Shop.Sid WHERE [Group].GroupStatus != 2 {wherestring}) a
                                    WHERE ROWSID > {pageSize * (currentPage - 1)};";

            var countQuery = $@"SELECT COUNT([Sid]) From [Group] WHERE [Group].GroupStatus != 2 {wherestring};";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@GroupName", "%" + groupName + "%"),
            };

            var dt = this.GetDataTable(queryString, parameters);
            var dataTotal = this.GetScale(countQuery, parameters) as int?;
            total = (dataTotal.HasValue) ? dataTotal.Value : 0;
            return dt;
        }

        public DataTable GetSingleGroupDetail(int Sid)
        {
            string queryString = $@"SELECT *, Menu.Sid AS MenuSid, [Group].Sid as GroupSid FROM [Group] 
                                    JOIN Shop ON [Group].ShopSid = Shop.Sid 
                                    JOIN Menu ON Shop.Sid = Menu.ShopSId  WHERE [Group].Sid = @Sid;";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@Sid", Sid)
            };

            var dt = this.GetDataTable(queryString, parameters);

            return dt;
        }

        public DataTable GetOrder(int Sid)
        {
            string queryString = $@"SELECT GroupSid, AccountSid, WhoOrder, UserImgName
                                    FROM [Order]
                                    JOIN [UserAccount] ON [Order].AccountSid = [UserAccount].Sid
                                    Where GroupSid = @Sid
                                    Group By AccountSid, WhoOrder, GroupSid, UserImgName;";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@Sid", Sid)
            };

            var dt = this.GetDataTable(queryString, parameters);
            return dt;
        }

        public DataTable GetOrderDetail(int accountSid, int GroupSid)
        {
            string queryString = $@"SELECT * FROM [Order] JOIN Menu ON MenuSid = Menu.Sid Where AccountSid = @AccountSid AND GroupSid = @GroupSid;";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@AccountSid", accountSid),
               new SqlParameter("@GroupSid", GroupSid),
            };

            var dt = this.GetDataTable(queryString, parameters);
            return dt;
        }

        public void InsertOrder(List<OrderModel> model)
        {
            string queryString = $@"INSERT INTO [Order](MenuSid, GroupSid, AccountSid, WhoOrder, Qty)
                                                VALUES(@MenuSid, @GroupSid, @AccountSid, @WhoOrder, @Qty);";

            foreach(var item in model)
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
            {

                new SqlParameter("@MenuSid",item.MenuSid),
                new SqlParameter("@GroupSid", item.GroupSid),
                new SqlParameter("@AccountSid", item.AccountSid),
                new SqlParameter("@WhoOrder", item.WhoOrder),
                new SqlParameter("@Qty", item.Qty)
            };
                this.ExecuteNonQuery(queryString, parameters);
            }
                      
        }

        public DataTable DelectOrderDetail(int accountSid, int GroupSid)
        {
            string queryString = $@"DELETE [Order] WHERE AccountSid = @AccountSid AND GroupSid = @GroupSid;";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@AccountSid", accountSid),
               new SqlParameter("@GroupSid", GroupSid),
            };

            var dt = this.GetDataTable(queryString, parameters);
            return dt;
        }

        public DataTable GetCountTotal(int Sid)
        {
            string queryString = $@"SELECT [Order].MenuSid, Menu.FoodName, sum([Order].Qty) AS Qty, Menu.Price FROM [Group] 
                                    JOIN [Order] ON [Group].Sid = [Order].GroupSid 
                                    JOIN [Menu] ON [Order].MenuSid = Menu.Sid
                                    WHERE [Group].Sid = @Sid
                                    Group by FoodName, [Order].MenuSid, Menu.Price;";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@Sid", Sid)
            };

            var dt = this.GetDataTable(queryString, parameters);

            return dt;
        }


        public void UpdateGroupStatus(int GroupSid, int Status)
        {
            string queryString = $@"UPDATE [Group] SET GroupStatus = @GroupStatus WHERE Sid = @Sid;";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@Sid", GroupSid),
               new SqlParameter("@GroupStatus", Status)
            };

            this.ExecuteNonQuery(queryString, parameters);
            
        }

        public DataTable GetShopMenu(int GroupSid)
        {
            string queryString = $@"SELECT FoodName FROM [Group] 
                                    JOIN Shop ON [Group].ShopSid = Shop.Sid 
                                    JOIN Menu ON Shop.Sid = Menu.ShopSId 
                                    WHERE [Group].Sid = @Sid;";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@Sid", GroupSid),
            };

            var dt = this.GetDataTable(queryString, parameters);

            return dt;
        }


        public DataTable GetUserAccount(out int total, string account, int currentPage = 1, int pageSize = 10)
        {
            //string queryString = $@"SELECT * FROM [Group] JOIN Shop ON [Group].ShopSid = Shop.Sid;";
            string wherestring = string.Empty;
            if (!string.IsNullOrWhiteSpace(account))
            {
                wherestring = "AND Account LIKE @Account";
            }

            string queryString = $@"SELECT TOP 10 * FROM 
                                    (SELECT [Sid], Account, UserName,
                                    ROW_NUMBER() OVER(ORDER BY UserAccount.[Sid] ) AS RowSid FROM UserAccount) a
                                    WHERE ROWSID > {pageSize * (currentPage -1)};";

            var countQuery = $@"SELECT COUNT([Sid]) From [UserAccount] {wherestring};";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@Account", "%" + account + "%"),
            };

            var dt = this.GetDataTable(queryString, parameters);
            var dataTotal = this.GetScale(countQuery, parameters) as int?;
            total = (dataTotal.HasValue) ? dataTotal.Value : 0;
            return dt;
        }

        public void InsertNewAccount(AccountModel model)
        {
            string queryString = $@"INSERT INTO [UserAccount](Account, Password, UserName, UserImgName, IsAdmin)
                                                VALUES(@Account, @Password, @UserName, @UserImgName, @IsAdmin);";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@Account", model.Account),
               new SqlParameter("@Password", model.Password),
               new SqlParameter("@UserName", model.UserName),
               new SqlParameter("@UserImgName", model.UserImgName),
               new SqlParameter("@IsAdmin", model.IsAdmin)
            };

            this.ExecuteNonQuery(queryString, parameters);
        }

        public DataTable GetSingleAccount(int AccountSid)
        {
            string queryString = $@"SELECT [Sid], Account, [Password], UserName, IsAdmin, UserImgName From UserAccount WHERE [Sid] = @Sid;";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@Sid", AccountSid)
            };

            var dt = this.GetDataTable(queryString, parameters);
            return dt;
        }

        public void UpDateAccount(AccountModel model ,int Sid)
        {
            string queryString = $@"UPDATE UserAccount SET [Password] = @Password, UserName = @UserName, IsAdmin = @IsAdmin, UserImgName = @UserImgName WHERE Sid = @Sid;";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@Sid", Sid),
               new SqlParameter("@Password", model.Password),
               new SqlParameter("@UserName", model.UserName),
               new SqlParameter("@UserImgName", model.UserImgName),
               new SqlParameter("@IsAdmin", model.IsAdmin)
            };

            this.ExecuteNonQuery(queryString, parameters);
        }

        public DataTable Admin_GetShopMenu(out int? countMenu, int shopSid, int currentPage = 1, int pageSize = 10)
        {
            string queryString = $@"SELECT * FROM
                                    (SELECT Shop.[Sid], ShopName, FoodName, Price, Menu.[SId] AS MenuSid,
                                    ROW_NUMBER() OVER(ORDER BY Menu.[Sid]) AS RowSid
                                    FROM Shop Shop JOIN Menu ON Shop.Sid = Menu.ShopSId WHERE Shop.[Sid] = @ShopSid) ShopDetail
                                    WHERE RowSid > {pageSize * (currentPage - 1)};";

            string countQuery = $@"SELECT COUNT([Sid]) FROM Menu WHERE ShopSId = @ShopSId;";
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@ShopSid", shopSid)
            };

            countMenu = this.GetScale(countQuery, parameters) as int?;

            var dt = this.GetDataTable(queryString, parameters);

            return dt;
        }


        public DataTable Admin_GetAllShop()
        {
            string queryString = $@"SELECT [Sid], ShopName FROM Shop;";
            List<SqlParameter> parameters = new List<SqlParameter>();

            var dt = this.GetDataTable(queryString, parameters);


            return dt;
        }

        public void CreateNewShop(ShopModel model, MenuModel menuModel)
        {
            string queryString = $@"INSERT INTO Shop(ShopName)
                                            VALUES(@ShopName)";
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@ShopName", model.ShopName)
            };

            this.ExecuteNonQuery(queryString, parameters);

            string ShopSidQueryString = $@"SELECT TOP 1 [Sid] FROM Shop ORDER BY [Sid] DESC; ";
            List<SqlParameter> ShopSidParameters = new List<SqlParameter>();
            int maxSid = (int)this.GetScale(ShopSidQueryString, ShopSidParameters);

            string menuQueryString = $@"INSERT INTO Menu(ShopSId, FoodName, Price, FoodImg) 
	                                            VALUES( @ShopSId, @FoodName, @Price, @FoodImg );";

            List<SqlParameter> MenuParameters = new List<SqlParameter>() 
            {
                new SqlParameter("@ShopSId", menuModel.ShopSid = maxSid),
                new SqlParameter("@FoodName", menuModel.FoodName),
                new SqlParameter("@Price", menuModel.Price),
                new SqlParameter("@FoodImg", menuModel.FoodImgName)
            };

            this.ExecuteNonQuery(menuQueryString, MenuParameters);
        }


        public void Admin_CreateNewMenu(MenuModel model)
        {
            string queryString = $@"INSERT INTO [Menu](ShopSid, FoodName, Price, FoodImg)
                                                VALUES(@ShopSid, @FoodName, @Price, @FoodImg);";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@ShopSid", model.ShopSid),
               new SqlParameter("@FoodName", model.FoodName),
               new SqlParameter("@Price", model.Price),
               new SqlParameter("@FoodImg", model.FoodImgName)
            };

            this.ExecuteNonQuery(queryString, parameters);
        }

        public DataTable Admin_GetSingleMenu(int menuSid)
        {
            string queryString = $@"SELECT * FROM Menu WHERE Sid = @Sid;";
            List<SqlParameter> parameters = new List<SqlParameter>() 
            {
                new SqlParameter("@Sid", menuSid)
            };

            var dt = this.GetDataTable(queryString, parameters);

            return dt;
        }

        public void Admin_UpDateMenu(MenuModel model)
        {
            string queryString = $@"UPDATE [Menu] SET FoodName = @FoodName, Price = @Price, FoodImg = @FoodImg WHERE Sid = @Sid;";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@Sid", model.Sid),
               new SqlParameter("@FoodName", model.FoodName),
               new SqlParameter("@Price", model.Price),
               new SqlParameter("@FoodImg", model.FoodImgName)
            };

            this.ExecuteNonQuery(queryString, parameters);
        }

        public void Admin_DeleteMenu(int menuSid)
        {
            string queryString = $@"Delete [Menu] WHERE Sid = @Sid;";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@Sid", menuSid)
            };

            this.ExecuteNonQuery(queryString, parameters);
        }
    }
}
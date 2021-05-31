﻿using LunchBoxOrder.Helper;
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
                wherestring = "WHERE GroupName LIKE @GroupName";
            }


            string queryString = $@"SELECT TOP 5 * FROM 
                                    (SELECT [Group].[Sid], [Group].GroupImgName, [Group].GroupName, Shop.ShopName, 
                                    ROW_NUMBER() OVER(ORDER BY [Group].[Sid] )  AS ROWSID FROM [Group]
                                    JOIN Shop ON [Group].ShopSid = Shop.Sid {wherestring}) a
                                    WHERE ROWSID > {pageSize * (currentPage - 1)};";

            var countQuery = $@"SELECT COUNT([Sid]) From [Group] {wherestring};";

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
            string queryString = $@"SELECT GroupSid, AccountSid, WhoOrder 
                                    FROM [Order]
                                    Where GroupSid = @Sid 
                                    Group By AccountSid, WhoOrder, GroupSid;";

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
    }
}
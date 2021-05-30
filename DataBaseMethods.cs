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
            string queryString = $@"INSERT INTO [Group](AccountSid, GroupLeader, ShopSid, GroupImgName, GroupName)
                                                VALUES(@AccountSid, @GroupLeader, @ShopSid, @GroupImgName, @GroupName);";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@AccountSid", model.AccountSid),
                new SqlParameter("@GroupLeader", model.GroupLeader),
                new SqlParameter("@ShopSid", model.ShopSid),
                new SqlParameter("@GroupImgName", model.GroupImgName),
                new SqlParameter("@GroupName", model.GroupName)
            };

            this.ExecuteNonQuery(queryString, parameters);
        }

        public DataTable GetGroup()
        {
            string queryString = $@"SELECT * FROM [Group] JOIN Shop ON [Group].ShopSid = Shop.Sid;";

            List<SqlParameter> parameters = new List<SqlParameter>();

            //{
            //   new SqlParameter("@Sid", Drone_ID),
            //};

            var dt = this.GetDataTable(queryString, parameters);

            return dt;
        }

        public DataTable GetSingleGroupDetail(int Sid)
        {
            string queryString = $@"SELECT * FROM [Group] 
                                    JOIN Shop ON [Group].Sid = Shop.Sid 
                                    JOIN Menu ON Shop.Sid = Menu.ShopSId WHERE [Group].Sid = @Sid;";

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

        public DataTable GetOrderDetail(int accountSid)
        {
            string queryString = $@"SELECT * FROM [Order] Where AccountSid = @AccountSid;";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@AccountSid", accountSid)
            };

            var dt = this.GetDataTable(queryString, parameters);
            return dt;
        }
    }
}
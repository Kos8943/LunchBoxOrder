using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunchBoxOrder.Models
{
    public class GroupModel
    {
        public int Sid { get; set; }

        public int AccountSid { get; set; }

        public string GroupLeader { get; set; }

        public int ShopSid { get; set; }

        public string GroupImgName { get; set; }

        public string GroupName { get; set; }
    }
}
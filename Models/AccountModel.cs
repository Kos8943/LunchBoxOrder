using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunchBoxOrder.Models
{
    public class AccountModel
    {
        public int Sid { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public string UserImgName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunchBoxOrder.Models
{
    public class MenuModel
    {
        public int Sid { get; set; }
        public int ShopSid { get; set; }
        public string FoodName { get; set; }
        public int Price { get; set; }
        public string FoodImgName { get; set; }
    }
}
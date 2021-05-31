using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunchBoxOrder.Models
{
    public class OrderModel
    {
       public int Sid {get;set;}
       public int MenuSid {get;set;}
       public int GroupSid {get;set;}
       public int AccountSid {get;set;}
       public string WhoOrder {get;set;}
       public string FoodName { get; set; }
       public int Price { get; set; }
       public int Qty { get; set; }

    }
}
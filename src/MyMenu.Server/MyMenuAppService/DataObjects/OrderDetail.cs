using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace MyMenuAppService.DataObjects
{
    public class OrderDetail : EntityData
    {
        public string FoodId { get; set; }
        public string FoodName { get; set; }
        public double Quantity { get; set; }
        public double SellingPrice { get; set; }
        public string OrderId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyMenu
{
    public class OrderDetail
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string FoodId { get; set; }
        public string FoodName { get; set; }
        public double Quantity { get; set; }
        public double SellingPrice { get; set; }
        public string OrderId { get; set; }
    }
}

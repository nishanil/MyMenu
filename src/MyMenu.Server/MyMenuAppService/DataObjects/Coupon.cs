using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Mobile.Server;

namespace MyMenuAppService.DataObjects
{
    public class Coupon : EntityData
    {
        public string Code { get; set; }
        public double Discount { get; set; }

    }
}

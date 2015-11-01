using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Mobile.Server;

namespace MyMenuAppService.DataObjects
{
    public class Address : EntityData
    {
        public string AddressType { get; set; }
        public string FullAddress { get; set; }
    }
}

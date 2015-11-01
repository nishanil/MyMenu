using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace MyMenuAppService.DataObjects
{
    public class Feedback : EntityData
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}
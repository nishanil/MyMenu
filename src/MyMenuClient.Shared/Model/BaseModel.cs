using System;
using System.Collections.Generic;
using System.Text;
using MyMenu;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;

namespace MyMenu
{
    public class BaseModel : IAzureEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [Version]
        public string Version { get; set; }

        [CreatedAt]
        public DateTime CreatedDateTime { get; set; }
    }
}

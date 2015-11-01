using Microsoft.Azure.Mobile.Server;

namespace MyMenuAppService.DataObjects
{
    public class User : EntityData
    {
        public string UserId { get; set; }
        public string AuthToken { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
    }
}
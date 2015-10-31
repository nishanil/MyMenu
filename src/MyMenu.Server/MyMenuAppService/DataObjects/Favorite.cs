
using Microsoft.Azure.Mobile.Server;

namespace MyMenuAppService.DataObjects
{
    public class Favorite : EntityData
    {
        public string FoodItemId { get; set; }
        public string UserId { get; set; }

        public bool IsRemoved { get; set; }
    }

}

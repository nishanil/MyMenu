using Microsoft.Azure.Mobile.Server;


namespace MyMenuAppService.DataObjects
{
    public class Food : EntityData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double PricePerQty { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsEnabled { get; set; }
        public string Cuisine { get; set; }

    }
}
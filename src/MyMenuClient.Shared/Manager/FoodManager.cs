using MyMenu;
using Xamarin.Forms;

[assembly: Dependency(typeof(FoodManager))]
namespace MyMenu
{
    public class FoodManager : BaseDataManager<Food>
    {
       // BaseDataManager handles all data sync by default. Add methods here for customization
      
    }
}

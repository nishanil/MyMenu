using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyMenuAdmin
{
    public class HomePage : TabbedPage
    {
        public HomePage()
        {
            Children.Add(new NavigationPage(new FoodListPage()) {Title = "Foods", Icon = "Spoon.png"});
            Children.Add(new NavigationPage(new OrdersPage()) {Title = "Orders", Icon = "Orders.png"});
		    Children.Add(new NavigationPage(new CouponsPage()) {Title = "Coupons", Icon = "ticket"});
        }
    }
}

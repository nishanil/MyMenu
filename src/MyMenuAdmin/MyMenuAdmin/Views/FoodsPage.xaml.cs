using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MyMenuAdmin
{
    public partial class FoodsPage : ContentPage
    {
        private FoodsViewModel vm;
        public FoodsPage()
        {
            InitializeComponent();
            BindingContext = vm = new FoodsViewModel();
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            PerformSearch(SearchBar.Text);
        }

        private void PerformSearch(string searchtext)
        {
            FoodList.BeginRefresh();

            FoodList.ItemsSource = string.IsNullOrEmpty(searchtext) ? vm.FoodItems : vm.FoodItems.Where(x => x.Name.ToLower().Contains(searchtext.ToLower()));
            FoodList.EndRefresh();
        }
    }
}

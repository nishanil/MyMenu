using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMenu;
using MyMenuAdmin.ViewModel;
using MyMenuAdmin.Views;
using Xamarin.Forms;

namespace MyMenuAdmin
{
    public partial class FoodListPage : ContentPage
    {
        private FoodListViewModel vm;
        public FoodListPage()
        {
            InitializeComponent();
            BindingContext = vm = new FoodListViewModel();
        }

        protected override void OnAppearing()
        {
            vm.LoadFoodItems();
            base.OnAppearing();
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

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FoodDetailPage(new FoodDetailViewModel()));
        }

        private async void FoodList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var selectedItem = vm.FoodItems.First(x => x.Name == (e.SelectedItem as Food).Name);

            Navigation.PushAsync(new FoodDetailPage(new FoodDetailViewModel(selectedItem)), true);
        }

        private void FoodList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}

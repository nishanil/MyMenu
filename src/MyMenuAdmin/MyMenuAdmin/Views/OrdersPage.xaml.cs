using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMenuAdmin.ViewModel;
using Xamarin.Forms;

namespace MyMenuAdmin
{
    public partial class OrdersPage : ContentPage
    {
        private OrdersViewModel vm;
        public OrdersPage()
        {
            InitializeComponent();
            BindingContext = vm = new OrdersViewModel();
        }

        protected override void OnAppearing()
        {
            vm.LoadOrders();
            base.OnAppearing();
        }

        private async void OrderList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var selectedItem = e.SelectedItem as OrderItemViewModel;
            var orderDetailVm = await vm.GetViewModelForOrderDetail(selectedItem);
            await  Navigation.PushAsync(new OrderDetailsPage(orderDetailVm), true);
        }

        private void OrderList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            PerformSearch(SearchBar.Text);
        }

        private void PerformSearch(string searchtext)
        {
            OrderList.BeginRefresh();

            OrderList.ItemsSource = string.IsNullOrEmpty(searchtext) ? vm.OrderItems : vm.OrderItems.Where(x => x.OrderName.ToLower().Contains(searchtext.ToLower()));
            OrderList.EndRefresh();
        }
    }
}

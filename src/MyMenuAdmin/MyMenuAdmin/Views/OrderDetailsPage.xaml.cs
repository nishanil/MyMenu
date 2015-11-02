using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMenuAdmin.ViewModel;
using Xamarin.Forms;

namespace MyMenuAdmin
{
    public partial class OrderDetailsPage : ContentPage
    {
        public OrderDetailsPage()
        {
            InitializeComponent();
        }

        public OrderDetailViewModel ViewModel {
            get { return (OrderDetailViewModel) BindingContext; }
        }

        public OrderDetailsPage(OrderDetailViewModel viewModel) : this()
        {
            if (viewModel == null)
                viewModel = new OrderDetailViewModel();
            BindingContext = viewModel;
        }

        private void FoodList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void PerformOrderPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Update Order", "User will be notified now?", "Ok", "Cancel");
            if (result)
            {
                var PerformOrderPicker = sender as Picker;
                string orderStatus = PerformOrderPicker.Items[PerformOrderPicker.SelectedIndex];
                ViewModel.UpdateOrder(orderStatus);
            }

        }

         
    }
}

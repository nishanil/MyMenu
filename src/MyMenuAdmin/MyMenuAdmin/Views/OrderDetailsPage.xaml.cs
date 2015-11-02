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

    }
}

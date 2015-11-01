using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMenuAdmin.ViewModel;
using Xamarin.Forms;

namespace MyMenuAdmin.Views
{
    public partial class FoodDetailPage : ContentPage
    {
        public FoodDetailPage() 
        {
            InitializeComponent();
        }

        public FoodDetailPage(FoodDetailViewModel viewModel) : this()
        {
            if(viewModel==null)
                viewModel = new FoodDetailViewModel();
            viewModel.CurrentPage = this;
            BindingContext = viewModel;
        }
    }
}

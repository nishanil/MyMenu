using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MyMenu;
using Xamarin.Forms;

namespace MyMenuAdmin.ViewModel
{
    public class FoodDetailViewModel : BaseViewModel
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(); }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; RaisePropertyChanged(); }
        }
        private string imageUrl;

        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; RaisePropertyChanged(); }
        }

        private double price;

        public double Price
        {
            get { return price; }
            set { price = value; RaisePropertyChanged(); }
        }

        private bool isFeatured;

        public bool IsFeatured
        {
            get { return isFeatured; }
            set { isFeatured = value; }
        }


        private Food selectedFood;

        public Food SelectedFood
        {
            get { return selectedFood; }
            set { selectedFood = value; RaisePropertyChanged(); }
        }

        private ICommand saveCommand;

        public ICommand SaveCommand
        {
            get { return saveCommand; }
            set { saveCommand = value; RaisePropertyChanged(); }
        }

        public bool InEditMode { get; set; }

        public Page CurrentPage { get; set; }

        public FoodDetailViewModel(Food selectedFood=null)
        {
             if(selectedFood==null)
                SelectedFood = new Food();
             else
             {
                 InEditMode = true;
                 SelectedFood = selectedFood;
                 Name = selectedFood.Name;
                 Description = selectedFood.Description;
                 Price = selectedFood.PricePerQty;
                 ImageUrl = selectedFood.ImageUrl;
                 IsFeatured = selectedFood.IsFeatured;
                 
             }

             SaveCommand = new Command(async () =>
             {
                 SelectedFood.Name = Name;
                 SelectedFood.Description = Description;
                 SelectedFood.PricePerQty = Price;
                 SelectedFood.ImageUrl = ImageUrl;
                 SelectedFood.IsFeatured = IsFeatured;

                 try
                 {
                     var manager = DependencyService.Get<IAzureDataManager<Food>>();
                     IsBusy = true;
                     await manager.SaveAsync(SelectedFood);
                     await manager.SyncAsync();
                     IsBusy = false;
                     if(CurrentPage!=null)
                     await CurrentPage.Navigation.PopAsync();

                 }
                 catch (Exception ex)
                 {
                     IsBusy = false;
                     if (CurrentPage != null)
                         await CurrentPage.DisplayAlert("Error", "Failed to sync. Try again later.", "Ok");
                 }
               
             });
        }
    }
}

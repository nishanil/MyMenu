using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMenu
{
    public class XCartViewModel : BaseViewModel
    {
        private string totalPrice;

        public string TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; RaisePropertyChanged(); }
        }

        private string totalQuantity;

        public string TotalQuantity
        {
            get { return totalQuantity; }
            set { totalQuantity = value; RaisePropertyChanged(); }
        }

        private bool isEnabled;

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; RaisePropertyChanged(); }
        }


        public XCartViewModel()
        {
            IsEnabled = false;
            App.CheckoutItems.CollectionChanged += CheckoutItems_CollectionChanged;  
        }

        private void CheckoutItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var count = App.CheckoutItems.Count;
            IsEnabled = count > 0;
            var item = count == 1 ? "Item" : "Items";
            TotalQuantity = $"{count} {item}";
            var tp = App.CheckoutItems.Sum(_ => _.PricePerQty);
            TotalPrice = $"{tp:C}";

        }
    }
}

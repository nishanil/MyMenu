using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMenu;
using MyMenu.Helpers;
using Xamarin.Forms;

namespace MyMenuAdmin.ViewModel
{
    class OrdersViewModel : BaseViewModel
    {
        private ObservableCollection<OrderItemViewModel> orderItems;

        public ObservableCollection<OrderItemViewModel> OrderItems
        {
            get { return orderItems; }
            set
            {
                orderItems = value;
                RaisePropertyChanged();

            }
        }

        public OrdersViewModel()
        {
            Title = "Order";
        }

        public async void LoadOrders()
        {
            var azureService = DependencyService.Get<IDataService>();
            IsBusy = true;
            var orderItems = await azureService.GetOrdersAsync();
            var orderDetailItems = await azureService.GetOrderDetailsAsync();

            var orderVmList = new ObservableCollection<OrderItemViewModel>();
            foreach (var orderItem in orderItems)
            {
                var orderVm = new OrderItemViewModel();
                orderVm.OrderName = string.Format("Order: {0}", orderItem.Number);
                orderVm.DeliveryAddress = orderItem.Address;
                orderVm.CreatedDateTime = orderItem.CreatedDateTime;
                orderVm.OrderStatus = orderItem.Status;
                var orderDetailSpecificToOrder = orderDetailItems.Where((detail => detail.OrderId == orderItem.Id));
                string formatter = "{0} ({1}), ";
                var strBuilder = new StringBuilder();
                foreach (var orderDetail in orderDetailSpecificToOrder)
                {
                    strBuilder.Append(string.Format(formatter, orderDetail.FoodName, orderDetail.Quantity));
                }
                orderVm.OrderDetails = strBuilder.ToString();
                orderVm.TotalAmount = orderItem.TotalAmount;
                orderVmList.Add(orderVm);
            }
            OrderItems = orderVmList;
            IsBusy = false;
        }
    }

    /// <summary>
    /// Specially designed VM for Binding to the Order List View
    /// </summary>
    public class OrderItemViewModel : BaseViewModel
    {
        private string orderName;

        public string OrderName
        {
            get { return orderName; }
            set { orderName = value; RaisePropertyChanged(); }
        }

        private string orderDetails;

        public string OrderDetails
        {
            get { return orderDetails; }
            set { orderDetails = value; RaisePropertyChanged(); }
        }

        private string deliveryAddress;

        public string DeliveryAddress
        {
            get { return deliveryAddress; }
            set { deliveryAddress = value; RaisePropertyChanged();}
        }

        private string orderStatus;

        public string OrderStatus
        {
            get { return orderStatus; }
            set { orderStatus = value;
                RaisePropertyChanged();
                RaisePropertyChanged("OrderColorHex");
            }
        }

        public Color OrderColorHex {
            get
            {
                var colorHex = "#F44336";
                switch (OrderStatus.ToLower())
                {
                    case "delivered":
                        colorHex = "#4CAF50";
                        break;
                    case "out for delivery":
                        colorHex = "#FF9800";
                        break;

                }
                return Color.FromHex(colorHex);
            }
        }

        private DateTime createdDateTime;

        public DateTime CreatedDateTime
        {
            get { return createdDateTime; }
            set { createdDateTime = value;
                RaisePropertyChanged();
                RaisePropertyChanged("Moment");

            }
        }

        public string Moment {
            get { return createdDateTime.ToLocalTime().ToRelativeDate(); }
        }

        private double amount;

        public double TotalAmount
        {
            get { return amount; }
            set { amount = value; RaisePropertyChanged(); RaisePropertyChanged("Price"); }
        }

        public string Price
        {
            get
            {
                return string.Format("{0:C}", TotalAmount);
            }
        }

    }
}

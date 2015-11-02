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

        public List<Order> Orders { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }


        public OrdersViewModel()
        {
            Title = "Orders";
        }

        public async void LoadOrders()
        {
            var azureService = DependencyService.Get<IDataService>();
            IsBusy = true;
            Orders = await azureService.GetOrdersAsync();
            OrderDetails = await azureService.GetOrderDetailsAsync();

            var orderVmList = new ObservableCollection<OrderItemViewModel>();
            foreach (var orderItem in Orders)
            {
                var orderVm = new OrderItemViewModel
                {
                    OrderName = string.Format("Order: {0}", orderItem.Number),
                    DeliveryAddress = orderItem.Address,
                    CreatedDateTime = orderItem.CreatedDateTime,
                    OrderStatus = orderItem.Status
                };
                var orderDetailSpecificToOrder = OrderDetails.Where((detail => detail.OrderId == orderItem.Id));
                string formatter = "{0} ({1}), ";
                var strBuilder = new StringBuilder();
                foreach (var orderDetail in orderDetailSpecificToOrder)
                {
                    strBuilder.Append(string.Format(formatter, orderDetail.FoodName, orderDetail.Quantity));
                }
                orderVm.OrderDetails = strBuilder.ToString();
                orderVm.Discount = orderItem.Discount.ToString() + " %";
                orderVm.TotalAmount = orderItem.TotalAmount;
                orderVm.UserEmail = orderItem.UserEmail;
                orderVm.UserPhone = orderItem.UserPhone;
                orderVm.UserName = orderItem.UserName;
                orderVm.OrderId = orderItem.Id;
                orderVm.Payment = orderItem.Payment;
                orderVmList.Add(orderVm);
                
            }
            OrderItems = orderVmList;
            IsBusy = false;
        }

        public async Task<OrderDetailViewModel> GetViewModelForOrderDetail(OrderItemViewModel selectedOrderItemViewModel)
        {

            return await  Task.Run(() =>
            {
                var selectedOrder = Orders.Find((order => selectedOrderItemViewModel.OrderId == order.Id));

                var orderDetailSpecificToOrder =
                    OrderDetails.Where((detail => detail.OrderId == selectedOrder.Id)).ToList();
                var orderDetailViewModel = new OrderDetailViewModel
                {
                    SelectedOrderItemViewModel = selectedOrderItemViewModel,
                    SelectedOrder = selectedOrder,
                    SelectedOrderDetails = orderDetailSpecificToOrder,

                };
                return orderDetailViewModel;

            });
        }
    }

    /// <summary>
    /// Specially designed VM for Binding to the Order List View
    /// </summary>
    public class OrderItemViewModel : BaseViewModel
    {
        public string OrderId { get; set; }

        private string orderName;

        public string OrderName
        {
            get { return orderName; }
            set { orderName = value; RaisePropertyChanged(); }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }
        private string payment;

        public string Payment
        {
            get { return payment; }
            set { payment = value; RaisePropertyChanged(); }
        }

        private string userEmail;

        public string UserEmail
        {
            get { return userEmail; }
            set { userEmail = value; RaisePropertyChanged(); }
        }

        private string userPhone;

        public string UserPhone
        {
            get { return userPhone; }
            set { userPhone = value; RaisePropertyChanged();}
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

        private string discount;

        public string Discount
        {
            get { return discount; }
            set { discount = value; RaisePropertyChanged(); }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMenu;

namespace MyMenuAdmin.ViewModel
{
    public class OrderDetailViewModel : BaseViewModel
    {
        private OrderItemViewModel selectedOrderItemViewModel;

        public OrderItemViewModel SelectedOrderItemViewModel
        {
            get { return selectedOrderItemViewModel; }
            set { selectedOrderItemViewModel = value; RaisePropertyChanged(); }
        }

        private Order selectedOrder;

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set { selectedOrder = value; RaisePropertyChanged(); }
        }

        private List<OrderDetail> selectedOrderDetails;

        public List<OrderDetail> SelectedOrderDetails
        {
            get { return selectedOrderDetails; }
            set { selectedOrderDetails = value; RaisePropertyChanged();}
        }

        public OrderDetailViewModel()
        {
            Title = "Order Detail";
        }
    }
}

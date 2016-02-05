using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyMenu;
using Xamarin.Forms;

[assembly: Dependency(typeof(OrderManager))]
[assembly: Dependency(typeof(OrderDetailManager))]
namespace MyMenu
{
    public class OrderManager : BaseDataManager<Order>
    {
        
    }

    public class OrderDetailManager : BaseDataManager<OrderDetail>
    {
        public async Task<List<OrderDetail>> GetOrderDetailsAsync(Order order)
        {
            SetQuery("detailOnOrderId", SyncTable
                                        .CreateQuery()
                                        .Where(od => od.OrderId == order.Id));
            await SyncAsync();
            return await GetAsync();
        }
    }
}

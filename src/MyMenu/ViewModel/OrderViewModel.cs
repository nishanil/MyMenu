//
// OrderViewModel.cs
//
// Author:
//       Prashant Cholachagudda <prashant@xamarin.com>
//
// Copyright (c) 2015 Xamarin Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using MyMenu;
using System.Threading.Tasks;

[assembly:Dependency(typeof(OrderViewModel))]
namespace MyMenu
{
	public class OrderViewModel : BaseViewModel, IOrderViewModel
	{
		public OrderViewModel ()
		{
			Title = "Orders";
		}

		public void SetOrderDetails (Order order, List<OrderDetail> orderDetails)
		{
			this.order = order;
			this.orderDetails = orderDetails;
		}

		public string Price {
			get{
				return string.Format ("{0:C}", order.TotalAmount);
			}
		}

		public string Address {
			get{
				return order.Address;
			}
		}

		public Command OrderCommand{
			get{
				return orderCommand ?? (orderCommand = new Command (async () => await PlaceOrder ()));
			}
		}

		async Task PlaceOrder()
		{
			var orderService = DependencyService.Get<IDataService> ();

			IsBusy = true;

			try {
				await orderService.InsertOrderAsync (order);
				
				foreach (var item in orderDetails) {
					item.OrderId = order.Id;
					await orderService.InsertOrderDetailAsync (item);
				}
			} finally {
				IsBusy = false;
			}
		}

		Command orderCommand;
		List<OrderDetail> orderDetails;

		Order order;

	}

	public interface IOrderViewModel
	{
		void SetOrderDetails(Order order, List<OrderDetail> orderDetails);
	}
}


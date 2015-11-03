//
// OrderConfirm.cs
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
using Xamarin.Forms;
using MyMenu;

[assembly:Dependency(typeof(OrderConfirmViewModel))]
namespace MyMenu
{
	public interface IOrderConfirm
	{
		void SetOrder (Order order);
	}

	public class OrderConfirmViewModel : BaseViewModel, IOrderConfirm
	{
		public void SetOrder (Order order)
		{
			this.order = order;
			Title = string.Format ("Order #{0}", this.order.Number);
		}


		public string Price {
			get {
				return string.Format ("TOTAL: {0:C}", order.TotalAmount);
			}
		}

		public string Status {
			get {
				return order.Status;
			}
		}

		Order order;
	}
}


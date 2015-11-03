//
// OrderDetailsViewModel.cs
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
using System.Diagnostics;

namespace MyMenu
{
	public class OrderDetailsViewModel :BaseViewModel
	{
		readonly OrderDetail order;
		readonly ICheckoutViewModel checkoutVM;

		public OrderDetailsViewModel (OrderDetail order, ICheckoutViewModel checkoutVM)
		{
			this.checkoutVM = checkoutVM;
			this.order = order;
		}

		public OrderDetail Details {
			get {
				return order;
			}
		}

		Command quantityCommand;

		public Command QuantityCommand {
			get {
				return quantityCommand ?? (quantityCommand = new Command<string> (QuantityCommandMethod));
			}
		}

		void QuantityCommandMethod (string command)
		{
			Debug.WriteLine (command);

			if (command == "add") {
				if (Quantity == 5) {
					return;
				}
				Quantity = Quantity + 1;
			}

			if (command == "remove") {
				if (Quantity == 1) {
					return;
				}

				Quantity = Quantity - 1;
			}

			RaisePropertyChanged ("TotalPrice");
			checkoutVM.UpdateTotalPrice ();
		}

		public double Quantity {
			get {
				return order.Quantity;
			}
			set {
				order.Quantity = value;
				RaisePropertyChanged ();
			}
		}

		public double TotalPrice {
			get { 
				return order.TotalPrice;
			}
		}

	}
}


//
// CheckoutViewModel.cs
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
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MyMenu
{
	public interface ICheckoutViewModel
	{
		void UpdateTotalPrice ();
	}

	public class CheckoutViewModel : BaseViewModel, ICheckoutViewModel
	{
		public CheckoutViewModel ()
		{
			CheckoutItems = new ObservableCollection<OrderDetailsViewModel> ();
			TotalPrice = 0;

			PopulateOrders ();
		}

		void PopulateOrders ()
		{
			var foodItems = App.CheckoutItems.GroupBy (p => p.Id);
			foreach (var item in foodItems) {
				var food = item.FirstOrDefault ();

				if (food == null)
					continue;

				var order = new OrderDetail {
					FoodId = food.Id,
					FoodName = food.Name,
					Quantity = item.Count (),
					SellingPrice = food.PricePerQty,
				};

				CheckoutItems.Add (new OrderDetailsViewModel (order, this));
			}

			if (CheckoutItems.Count <= 0) {
				return;
			}

			TotalPrice = CheckoutItems.Sum (p => p.Details.TotalPrice);
		}

		public double TotalPrice {
			get {
				return totalPrice;
			}
			set {
				totalPrice = value;
				RaisePropertyChanged ();
				RaisePropertyChanged ("Price");
			}
		}

		public Command AddCommand {
			get {
				return addCommand ?? (addCommand = new Command (() => Debug.WriteLine (DateTime.Now)));
			}
		}

		public void UpdateTotalPrice()
		{
			TotalPrice = CheckoutItems.Sum (p => p.Details.TotalPrice);
		}

		Command addCommand;

		public string Price {
			get {
				return string.Format ("{0:C}", TotalPrice);
			}
		}

		double totalPrice;

		public ObservableCollection<OrderDetailsViewModel> CheckoutItems { get; set; }
	}
}


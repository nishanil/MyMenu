//
// HomeViewModel.cs
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
using System.Collections.ObjectModel;

namespace MyMenu
{
	public class HomeViewModel : BaseViewModel
	{
		public HomeViewModel ()
		{
			Title = "Home";
			FoodItems = new ObservableCollection<FoodViewModel> ();
			LoadFoodItems ();
		}

		async void LoadFoodItems ()
		{
			try {
				IsBusy = true;

				var items = await client.GetFoodItems ();
				foreach (var item in items) {
					FoodItems.Add (new FoodViewModel (item));
				}
				
			} finally {
				IsBusy = false;
			}

		}

		public ObservableCollection<FoodViewModel> FoodItems {
			get;
			set;
		}

		readonly IFoodServiceClient client = new DummyFoodServiceClient ();
	}
}
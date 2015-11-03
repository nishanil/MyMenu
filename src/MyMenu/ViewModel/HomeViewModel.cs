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
using System.Linq;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;

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

		async Task LoadFoodItems ()
		{
			try {
				IsBusy = true;
				var dataService = DependencyService.Get<IDataService> ();
				var items = await dataService.GetFoodItemsAsync ();
				var favorites = await dataService.GetUserFavoritesAsync ();

				var fooditems = from fi in items
				                join  fav in favorites on fi.Id equals fav.FoodItemId into prodGroup
				                from g in prodGroup.DefaultIfEmpty (null)
				                select new {FoodItem = fi, FavoriteItem = g};

				foreach (var item in fooditems) {
					item.FoodItem.IsFavorite = (item.FavoriteItem != null);
					FoodItems.Add (new FoodViewModel (item.FoodItem));
				}
				
			} finally {
				IsBusy = false;
			}

		}

		public ObservableCollection<FoodViewModel> FoodItems {
			get;
			set;
		}

	}
}
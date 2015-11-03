//
// FavoriteViewModel.cs
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
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MyMenu
{
	public class FavoriteViewModel : BaseViewModel
	{
		public FavoriteViewModel ()
		{
			Title = "Favourites";
			FoodItems = new ObservableCollection<FoodViewModel> ();

			LoadDataAsync ();
		}

		async Task LoadDataAsync ()
		{
			try {
				IsBusy = true;

				var dataService = DependencyService.Get<IDataService> ();
				var items = await dataService.GetFoodItemsAsync ();
				var favorites = await dataService.GetUserFavoritesAsync ();

				var fooditems = from fi in items
				                join  fav in favorites on fi.Id equals fav.FoodItemId
				                select fi;

				foreach (var item in fooditems) {
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
	}
}


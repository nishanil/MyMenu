//
// HomePage.xaml.cs
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

namespace MyMenu
{
	public partial class HomePage : ContentPage
	{
		HomeViewModel vm;

		public HomePage ()
		{
			InitializeComponent ();

			var favoriteTable = App.Client.GetSyncTable<FavoriteItem> ();
			App.Manager = new DataManager (App.Client, favoriteTable);

			vm = new HomeViewModel ();
			vm.PropertyChanged += Vm_PropertyChanged;
			BindingContext = vm;
		}

		void Favourites_Clicked (object sender, EventArgs e)
		{
			Navigation.PushAsync (new FavoritePage());
		}

		void Vm_PropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName != "IsBusy")
				return;

			var progress = DependencyService.Get<IProgressDisplay> ();
			if (vm.IsBusy && progress != null)
				progress.Show ();
			else
				progress.Dismiss ();
		}

	}
}


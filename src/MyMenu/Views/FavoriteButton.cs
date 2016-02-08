//
// ImageButton.cs
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
	public class FavoriteButton : Image
	{
		readonly TapGestureRecognizer tap;
		FoodViewModel vm;

		public FavoriteButton ()
		{
			tap = new TapGestureRecognizer ();
			tap.Tapped += Tap_Tapped;

			GestureRecognizers.Add (tap);
			vm = BindingContext as FoodViewModel;
		}


		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			vm = BindingContext as FoodViewModel;
			Source = vm != null && vm.IsFavourite ? "fav.png" : "nofav.png";
		}

		void Tap_Tapped (object sender, EventArgs e)
		{
			vm.AddFavorite.Execute (null);
			Source = vm.IsFavourite ? "fav.png" : "nofav.png";
		}
	}

	public class QuantityButton : Image
	{
		public QuantityButton ()
		{
			tap = new TapGestureRecognizer ();
			tap.Tapped += Tap_Tapped;

			GestureRecognizers.Add (tap);
		}

		void Tap_Tapped (object sender, EventArgs e)
		{
			vm.QuantityCommand.Execute (StyleId);
		}

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			vm = BindingContext as OrderDetailsViewModel;
		}

		OrderDetailsViewModel vm;
		readonly TapGestureRecognizer tap;
	}

	public class ImageButton : Image
	{
		public ImageButton ()
		{
			tap = new TapGestureRecognizer ();
			tap.Tapped += Tap_Tapped;

			GestureRecognizers.Add (tap);
		}

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			vm = BindingContext as FoodViewModel;
		}

		void Tap_Tapped (object sender, EventArgs e)
		{
			Debug.WriteLine ("Add item tapped");
			vm.AddToBasket.Execute (null);
		}

		readonly TapGestureRecognizer tap;
		FoodViewModel vm;
	}
}


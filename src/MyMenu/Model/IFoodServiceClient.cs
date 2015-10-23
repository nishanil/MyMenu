//
// IFoodServiceClient.cs
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
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyMenu
{
	public interface IFoodServiceClient
	{
		Task<List<Food>> GetFoodItems ();
	}

	//TODO: Replce with actual implementation later
	public class DummyFoodServiceClient : IFoodServiceClient
	{
		//Waffles - http://i.imgur.com/IfVirWF.jpg
		//Dosa -  http://i.imgur.com/q2BqGza.jpg
		//Pasta - http://i.imgur.com/rOPvbnl.jpg

		#region IFoodServiceClient implementation

		public Task<List<Food>> GetFoodItems ()
		{
			var tcs = new TaskCompletionSource<List<Food>> ();
			var foodItems = new List<Food>{ 
				new Food{
					Id = Guid.NewGuid(),
					Name = "Waffles",
					Description = "Fresh waffles with honey and strawberries",
					PictureLarge = "http://i.imgur.com/IfVirWF.jpg",
					Price = 80
				},
				new Food{
					Id = Guid.NewGuid(),
					Name = "Pasta",
					Description = "Italian pasta with garlic bread on the side",
					PictureLarge = "http://i.imgur.com/rOPvbnl.jpg",
					Price = 150
				},
				new Food{
					Id = Guid.NewGuid(),
					Name = "Dosa",
					Description = "Authentic South Indian masala dosa",
					PictureLarge = "http://i.imgur.com/q2BqGza.jpg",
					Price = 40
				}
			};

			tcs.SetResult (foodItems);

			return tcs.Task;
		}

		#endregion
		
	}
}


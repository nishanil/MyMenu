using System;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyMenu
{
	public interface ICheckoutViewModel
	{
		void UpdateTotalPrice ();

		string Price{ get; }

		string Coupon{ get; }

		Command ApplyCoupon { get; }
	}

	public class CheckoutViewModel : BaseViewModel, ICheckoutViewModel
	{
		public CheckoutViewModel (ICheckoutPage view)
		{
			this.view = view;
			CheckoutItems = new ObservableCollection<OrderDetailsViewModel> ();
			TotalPrice = 0;

			PopulateOrders ();
		}

		void PopulateOrders ()
		{
			if (App.CheckoutItems == null) {
				return;
			}

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

		string coupon;

		public string Coupon {
			get {
				return coupon;
			}
			set {
				coupon = value;
				RaisePropertyChanged ();
			}
		}

		double discount;

		public double Discount {
			get {
				return discount;
			}
			set {
				discount = value;
				RaisePropertyChanged ();
			}
		}

		public Command ApplyCoupon {
			get {
				return applyCoupon ?? (applyCoupon = new Command (async () => await ApplyCouponMethod ()));
			}
		}

		public Command AddCommand {
			get {
				return addCommand ?? (addCommand = new Command (() => Debug.WriteLine (DateTime.Now)));
			}
		}

		public void UpdateTotalPrice ()
		{
			TotalPrice = CheckoutItems.Sum (p => p.Details.TotalPrice);

			CalculateDiscount ();
		}

		Command addCommand;

		public string Price {
			get {
				return string.Format ("{0:C}", TotalPrice);
			}
		}

		public Command CheckOutCommand {
			get {
				return checkOutCommand ?? (checkOutCommand = new Command (CheckOutMethod));
			}
		}

		async Task ApplyCouponMethod ()
		{
			IsBusy = true;
			try {
				
				//var couponCode = await DependencyService.Get<IAzureDataManager<Coupon>>().GetAsync() (Coupon));
				//if (couponCode == null) {
				//	Discount = 0;
				//	UpdateTotalPrice(); //Recalculate the total price, so we don't apply multiple coupons

				//	return;
				//}

				//Discount = couponCode.Discount;

				//UpdateTotalPrice(); 

			} finally {
				IsBusy = false;
			}
		}

		void CalculateDiscount ()
		{
			var discountedValue = TotalPrice * (Discount / 100d);
			TotalPrice = TotalPrice - discountedValue;
		}

		void CheckOutMethod ()
		{
			if (string.IsNullOrEmpty (view.Address)) {
				return;
			}

			var order = new Order {

				SpecialInstruction = view.Instructions,
				Address = view.Address,
				UserId = Settings.CurrntUserId,
				UserEmail = "nishanil@outlook.com", //TODO: get user email address
				UserPhone = "0918892320619",
				Payment = "Cash On Delivery",
				UserName = "Nishant Anil", //TODO: get user name
				TotalAmount = totalPrice,
				Discount = 0,
				CouponId = string.Empty,
				Status = "Order Placed"
			};

				
			Debug.WriteLine (order.Id);
				
			foreach (var orderItem in CheckoutItems) {
				var detail = orderItem.Details;
				detail.OrderId = order.Id;
			}

			var orderItems = CheckoutItems.Select (x => x.Details).ToList ();

			var vm = DependencyService.Get<IOrderViewModel> (); 
			vm.SetOrderDetails (order, orderItems);
		}

		public ICheckoutViewModel PricingVm {
			get {
				return this;
			}
		}

		Command applyCoupon;

		Command checkOutCommand;

		double totalPrice;
		readonly ICheckoutPage view;

		public ObservableCollection<OrderDetailsViewModel> CheckoutItems { get; set; }
	}
}


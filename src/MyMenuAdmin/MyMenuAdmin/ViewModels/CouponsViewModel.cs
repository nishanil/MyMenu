using System;
using MyMenu;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MyMenuAdmin
{
	public class CouponsViewModel : BaseViewModel
	{
	    public CouponManager CouponManager { get; } = DependencyService.Get<IAzureDataManager<Coupon>>() as CouponManager;

        public CouponsViewModel ()
		{
			Title = "Coupons";
			Coupons = new ObservableCollection<Coupon> ();
            Device.BeginInvokeOnMainThread(async () => { await CouponManager.SyncAsync();  });
			LoadCoupons ();
		}

		public Command Refresh {
			get {
				return refresh ?? (refresh = new Command (async () => await LoadCoupons ()));
			}
		}

		async Task LoadCoupons ()
		{
			IsBusy = true;

			var coupons = await CouponManager.GetAsync();
			Coupons.Clear ();
			foreach (var coupon in coupons) {
				Coupons.Add (coupon);
			}

			IsBusy = false;
		}

		public ObservableCollection<Coupon> Coupons { get;  set; }

		Command refresh;
	}
}


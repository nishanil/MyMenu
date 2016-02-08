

using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;

namespace MyMenu
{
	public class App : Application
	{
        public static readonly string ApplicationURL = @"https://mymenu-demo.azurewebsites.net";
        public static readonly string GatewayURL = @"https://mymenu-demo8eb72f69d7c94309a2d73935e5784b92.azurewebsites.net";
        public static readonly string ApplicationKey = @"";

        public static MobileServiceClient Client { get; private set; }

		public static Size ScreenSize { get; set; }

		public static List<Food> CheckoutItems {
			get;
			set;
		}

		public App ()
		{
            var azureServiceClient = DependencyService.Get<IAzureClient>();
		    azureServiceClient.Init();
            Client = azureServiceClient.MobileService;

            var screen = DependencyService.Get<IScreenSize>();
            if (screen != null)
            {
                ScreenSize = screen.GetScreenSize();
            }
            else {
                ScreenSize = new Size(300, 600);
            }

            if (string.IsNullOrEmpty (Settings.CurrentUser)) {
				MainPage = new LoginPage ();
				return;
			}


			CheckoutItems = new List<Food> ();

			var user = new MobileServiceUser (Settings.CurrentUser) {
				MobileServiceAuthenticationToken = Settings.AccessToken
			};

			Client.CurrentUser = user;

			InitializeHomePage ();

			
		}

		void InitializeHomePage ()
		{
			
			MainPage = new NavigationPage (new HomePage ()) {
				BarBackgroundColor = Color.FromHex ("E91E63"),
				BarTextColor = Color.White
			};
            

        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}

}


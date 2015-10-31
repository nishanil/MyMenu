
using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using System.IO;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Android.Graphics.Drawables;

namespace MyMenu.Droid
{
	[Activity (Label = "My Menu", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		MobileServiceClient client;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			if ((int)Build.VERSION.SdkInt >= 21) {
				ActionBar.SetIcon (new ColorDrawable (Resources.GetColor (Android.Resource.Color.Transparent)));
			}

			CurrentPlatform.Init ();
			string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "test1.db");
			if (!File.Exists(path))
			{
				File.Create(path).Dispose();
			}

			client = new MobileServiceClient ("https://mymenu-ea.azure-mobile.net/", 
				"MCXpcoqnEmOwkDWhoAHAOJjxQtzMUa83");

			var store = new MobileServiceSQLiteStore(path);
			store.DefineTable<FavoriteItem> ();
			client.SyncContext.InitializeAsync(store).Wait();

			LoadApplication (new App (client));

		}
	}
}


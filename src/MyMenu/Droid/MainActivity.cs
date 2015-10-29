
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
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());

			if ((int)Build.VERSION.SdkInt >= 21) {
				ActionBar.SetIcon (new ColorDrawable (Resources.GetColor (Android.Resource.Color.Transparent)));
			}

			CurrentPlatform.Init ();
			string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "test1.db");
			if (!File.Exists(path))
			{
				File.Create(path).Dispose();
			}

			var store = new MobileServiceSQLiteStore(path);
			store.DefineTable<FavoriteItem> ();
			App.Client.SyncContext.InitializeAsync(store).Wait();

		}
	}
}


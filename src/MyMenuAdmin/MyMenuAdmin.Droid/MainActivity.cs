using System;

using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Android.Graphics.Drawables;

namespace MyMenuAdmin.Droid
{
    [Activity(Label = "MyMenu Admin", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            LoadApplication(new AdminApp());
            ImageCircleRenderer.Init();

            ColorDrawable colorDrawable = new ColorDrawable(Resources.GetColor(Resource.Color.material_blue_500));
            ActionBar.SetStackedBackgroundDrawable(colorDrawable);
            //ActionBar.SetSplitBackgroundDrawable(new ColorDrawable(Color.White));
        }
    }
}


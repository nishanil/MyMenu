using System;
using System.Collections.Generic;
using System.Text;
using MyMenuAdmin.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(AdminTabbedRenderer))]
namespace MyMenuAdmin.iOS.Renderers
{
    class AdminTabbedRenderer : TabbedRenderer
    {
        public AdminTabbedRenderer()
        {
            TabBar.TintColor = Color.FromHex("#E91E63").ToUIColor();
        }
    }
}

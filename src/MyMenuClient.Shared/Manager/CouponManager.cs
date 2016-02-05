using System;
using System.Collections.Generic;
using System.Text;
using MyMenu;
using Xamarin.Forms;

[assembly: Dependency(typeof(CouponManager))]

namespace MyMenu
{
    public class CouponManager : BaseDataManager<Coupon>
    {
    }
}

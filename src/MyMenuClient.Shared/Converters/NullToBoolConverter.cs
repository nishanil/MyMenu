using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace MyMenu
{
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PropertyInfo propertyInfo = value?.GetType().GetRuntimeProperty("Count");
            if (propertyInfo != null)
            {
                var count = (int)propertyInfo.GetValue(value, null);
                return count > 0;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

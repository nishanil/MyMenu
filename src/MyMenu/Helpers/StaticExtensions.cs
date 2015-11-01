using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMenu.Helpers
{
    public static class StaticExtensions
    {
        public static string ToRelativeDate(this DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan <= TimeSpan.FromSeconds(60))
                return $"{timeSpan.Seconds} seconds ago";

            if (timeSpan <= TimeSpan.FromMinutes(60))
                return timeSpan.Minutes > 1 ? $"about {timeSpan.Minutes} minutes ago" : "about a minute ago";

            if (timeSpan <= TimeSpan.FromHours(24))
                return timeSpan.Hours > 1 ? $"about {timeSpan.Hours} hours ago" : "about an hour ago";

            if (timeSpan <= TimeSpan.FromDays(30))
                return timeSpan.Days > 1 ? $"about {timeSpan.Days} days ago" : "yesterday";

            if (timeSpan <= TimeSpan.FromDays(365))
                return timeSpan.Days > 30 ? $"about {timeSpan.Days/30} months ago" : "about a month ago";

            return timeSpan.Days > 365 ? $"about {timeSpan.Days/365} years ago" : "about a year ago";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MyMenu;
using Xamarin.Forms;

[assembly: Dependency(typeof(FavoriteManager))]
namespace MyMenu
{
    public class FavoriteManager : BaseDataManager<FavoriteItem>
    {
        public async Task SyncAsync(string userId)
        {
            SetQuery("favorites", SyncTable.Where(p => p.UserId == userId));
            await base.SyncAsync();
        }
    }
}

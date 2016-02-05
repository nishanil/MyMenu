using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace MyMenu
{
    public interface IAzureClient
    {
        MobileServiceSQLiteStore SQLiteStore { get; }
        MobileServiceClient MobileService { get; }
        Task Init();
    }
}

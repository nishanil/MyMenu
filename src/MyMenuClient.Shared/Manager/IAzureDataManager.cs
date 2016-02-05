using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace MyMenu
{
    internal interface IAzureDataManager <T> where T : IAzureEntity
    {
        void Init(MobileServiceSQLiteStore store);
        IMobileServiceSyncTable<T> SyncTable { get; set; }
        Task SaveAsync(T item);
        Task DeleteAsync(T item);
        Task<List<T>> GetAsync();
        Task SyncAsync();
        Task HandleSyncErrors(ReadOnlyCollection<MobileServiceTableOperationError> syncErrors);
        Task<T> GetAsync(string id);
        void SetQuery(string queryId, IMobileServiceTableQuery<T> query);

    }
}

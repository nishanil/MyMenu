using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace MyMenu
{
    public class BaseDataManager<T> : IAzureDataManager<T> where T: IAzureEntity
    {
        private IAzureClient client;
        private const string defaultQueryId = "alldata";
        private string queryId;
        private IMobileServiceTableQuery<T> customQuery; 
        public void Init(MobileServiceSQLiteStore store)
        {
            client = Xamarin.Forms.DependencyService.Get<IAzureClient>();
            store.DefineTable<T>();
            SyncTable = client.MobileService.GetSyncTable<T>();
        }

        public IMobileServiceSyncTable<T> SyncTable { get; set; }
        
        public virtual async Task SyncAsync()
        {
            CheckForInit();
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;
            try
            {
                if (string.IsNullOrEmpty(queryId) && customQuery == null)
                    await SyncTable.PullAsync(defaultQueryId, SyncTable.CreateQuery());
                else
                    await SyncTable.PullAsync(queryId, customQuery);

            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }
            await HandleSyncErrors(syncErrors);
        }

        private void CheckForInit()
        {
            if (SyncTable == null)
            {
                throw new Exception("SyncTable not intialized. Call the Init() method first.");
            }
        }

        public virtual async Task HandleSyncErrors(ReadOnlyCollection<MobileServiceTableOperationError> syncErrors)
        {
            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }
                }
            }
        }

        public async Task<List<T>> GetAsync()
        {
            CheckForInit();
            try
            {
                var data = await SyncTable.ReadAsync();
                return data.ToList();
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return null;
        }

        public async Task SaveAsync(T item)
        {
            CheckForInit();
            var saveableEntity = item as IAzureEntity;
            if (saveableEntity == null)
                throw new ArgumentException("item should be of type IAzureEntity");
            if (saveableEntity.Id == null)
            {
                await SyncTable.InsertAsync(item);
            }
            else
                await SyncTable.UpdateAsync(item);
        }

        public async Task DeleteAsync(T item)
        {
            CheckForInit();
            try
            {
                await SyncTable.DeleteAsync(item);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
        }

        public Task<T> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public void SetQuery(string queryId, IMobileServiceTableQuery<T> query)
        {
            customQuery = query;
            this.queryId = queryId;
        }
    }
}

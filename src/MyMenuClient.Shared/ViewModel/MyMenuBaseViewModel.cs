using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyMenu
{
    public class MyMenuBaseViewModel<TAzureEntity> 
        : BaseViewModel where TAzureEntity : IAzureEntity
    {
        public IAzureDataManager<TAzureEntity> DataManager 
            => DependencyService.Get<IAzureDataManager<TAzureEntity>>();

        public virtual async Task SyncDataAsync()
        {
            await DataManager.SyncAsync();
        }
    }

    public abstract class MyMenuBaseListViewModel<TViewModel, TAzureEntity>
        : MyMenuBaseViewModel<TAzureEntity> where TViewModel : BaseViewModel
                                            where TAzureEntity : IAzureEntity
   
    {
        private ObservableCollection<TViewModel> items;

        public ObservableCollection<TViewModel> Items
        {
            get { return items; }
            set { items = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Implment this abstract class to populate Items
        /// </summary>
        /// <returns></returns>
        public abstract Task LoadItems();

        private Command refreshCommand;
        public Command RefreshCommand {
            get { return refreshCommand; }
            set { refreshCommand = value; RaisePropertyChanged(); }
        }

        protected MyMenuBaseListViewModel()
        {
            RefreshCommand = new Command(async () =>
            { 
                IsBusy = true;
                await SyncDataAsync();
                await LoadItems();
                IsBusy= false;
            });
        }

    }
}

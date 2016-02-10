using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyMenu
{
    public partial class QuantityControl : ContentView
    {
        public QuantityControl()
        {
            InitializeComponent();

            AddImageBehavior.PropertyChanged += ImageBehaviorOnPropertyChanged;
            RemoveImageBehavior.PropertyChanged += ImageBehaviorOnPropertyChanged;
        }
        

        private void ImageBehaviorOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var addRemoveQtyBehavior = sender as AddRemoveQtyBehavior;
            if (addRemoveQtyBehavior != null && propertyChangedEventArgs?.PropertyName == nameof(addRemoveQtyBehavior.Quantity))
            { 
                SetValue(SelectedQuantityPropertyKey, addRemoveQtyBehavior.Quantity);
                
            }
        }

        public static readonly BindableProperty GroupIdProperty =
            BindableProperty.Create("GroupId",
                                    typeof(string),
                                    typeof(QuantityControl),
                                    null);

        public string GroupId
        {
            set { SetValue(GroupIdProperty, value); }
            get { return (string)GetValue(GroupIdProperty); }
        }

        public static readonly BindablePropertyKey SelectedQuantityPropertyKey =
           BindableProperty.CreateReadOnly("SelectedQuantity",
                                   typeof(int),
                                   typeof(QuantityControl),
                                   default(int), propertyChanged:OnSelectedQuanityChanged);

        private static void OnSelectedQuanityChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as QuantityControl;
            if (control?.QuantityChangedCommand != null && control.QuantityChangedCommand.CanExecute(null))
            {
                var action = AddRemoveQtyAction.Add;
                if ((int) oldvalue > (int) newvalue)
                    action = AddRemoveQtyAction.Remove;

                control.QuantityChangedCommand.Execute(action);
            }
        }

        public static readonly BindableProperty SelectedQuantityProperty = SelectedQuantityPropertyKey.BindableProperty;

        public int SelectedQuantity => (int)GetValue(SelectedQuantityProperty);

        public static readonly BindableProperty QuantityChangedCommandProperty =
             BindableProperty.Create("QuantityChangedCommand", typeof(ICommand), typeof(QuantityControl), null);

        public ICommand QuantityChangedCommand
        {
            get { return (ICommand)GetValue(QuantityChangedCommandProperty); }
            set { SetValue(QuantityChangedCommandProperty, value); }
        }

        
    }
}

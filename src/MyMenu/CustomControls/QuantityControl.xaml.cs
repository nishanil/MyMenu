using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MyMenu
{
    public partial class QuantityControl : ContentView
    {
        public QuantityControl()
        {
            InitializeComponent();

            addImageBehavior.PropertyChanged += AddImageBehaviorOnPropertyChanged;
        }

        private void AddImageBehaviorOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var addRemoveQtyBehavior = sender as AddRemoveQtyBehavior;
            if (addRemoveQtyBehavior != null && propertyChangedEventArgs?.PropertyName == nameof(addRemoveQtyBehavior.Quantity))
                    SetValue(SelectedQuantityPropertyKey, addRemoveQtyBehavior.Quantity);
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
                                   default(int));

        public static readonly BindableProperty SelectedQuantityProperty = SelectedQuantityPropertyKey.BindableProperty;

        public int SelectedQuantity => (int)GetValue(SelectedQuantityProperty);
    }
}

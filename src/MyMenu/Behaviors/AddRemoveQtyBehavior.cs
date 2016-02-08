using System;
using System.Collections.Generic;
using Xamarin.Forms;
using static System.String;

namespace MyMenu
{
    public enum AddRemoveQtyAction
    {
        Add = 0,
        Remove =1
    }

    public class AddRemoveQtyBehavior : Behavior<Image>
    {
        TapGestureRecognizer tapRecognizer;
        private static readonly List<AddRemoveQtyBehavior> defaultBehaviors = new List<AddRemoveQtyBehavior>();
        private static readonly Dictionary<string, List<AddRemoveQtyBehavior>> imageGroup = new Dictionary<string, List<AddRemoveQtyBehavior>>();

        public static readonly BindableProperty GroupNameProperty =
            BindableProperty.Create("GroupId",
                                    typeof(string),
                                    typeof(AddRemoveQtyBehavior),
                                    null,
                                    propertyChanged: OnGroupNameChanged);

        private static void OnGroupNameChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            AddRemoveQtyBehavior behavior = (AddRemoveQtyBehavior)bindable;
            string oldGroupName = (string)oldvalue;
            string newGroupName = (string)newvalue;

            // Remove existing behavior from Group
            if (IsNullOrEmpty(oldGroupName))
            {
                defaultBehaviors.Remove(behavior);
            }
            else
            {
                List<AddRemoveQtyBehavior> behaviors = imageGroup[oldGroupName];
                behaviors.Remove(behavior);

                if (behaviors.Count == 0)
                {
                    imageGroup.Remove(oldGroupName);
                }
            }

            // Add New Behavior to the group
            if (IsNullOrEmpty(newGroupName))
            {
                defaultBehaviors.Add(behavior);
            }
            else
            {
                List<AddRemoveQtyBehavior> behaviors = null;

                if (imageGroup.ContainsKey(newGroupName))
                {
                    behaviors = imageGroup[newGroupName];
                }
                else
                {
                    behaviors = new List<AddRemoveQtyBehavior>();
                    imageGroup.Add(newGroupName, behaviors);
                }

                behaviors.Add(behavior);
            }
        }

        public string GroupName
        {
            set { SetValue(GroupNameProperty, value); }
            get { return (string)GetValue(GroupNameProperty); }
        }

        public static readonly BindableProperty ActionProperty =
           BindableProperty.Create("Action",
                                   typeof(AddRemoveQtyAction),
                                   typeof(AddRemoveQtyBehavior), AddRemoveQtyAction.Add);

        /// <summary>
        /// Add adds up and remove subtracts
        /// </summary>
        public AddRemoveQtyAction Action
        {
            set { SetValue(ActionProperty, value); }
            get { return (AddRemoveQtyAction)GetValue(ActionProperty); }
        }

        public static readonly BindableProperty QuantityProperty =
           BindableProperty.Create("Quantity",
                                   typeof(int),
                                   typeof(AddRemoveQtyBehavior), default(int), propertyChanged: OnQuantityChanged);

        private static void OnQuantityChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var behavior = bindable as AddRemoveQtyBehavior;
            string groupName = behavior.GroupName;
            List<AddRemoveQtyBehavior> behaviors = null;

            behaviors = IsNullOrEmpty(groupName) ? defaultBehaviors : imageGroup[groupName];
            foreach (var item in behaviors)
            {
                // Update SelectedQuantity of every behavior in the group to the latest value. 
                item.Quantity = behavior.Quantity;
            }
        }

        public int Quantity
        {
            set { SetValue(QuantityProperty, value); }
            get { return (int)GetValue(QuantityProperty); }
        }


        protected override void OnAttachedTo(Image view)
        {
            tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += OnTapRecognizerTapped;
            view.GestureRecognizers.Add(tapRecognizer);
        }

        private void OnTapRecognizerTapped(object sender, EventArgs e)
        {
            // TODO: Future - Update to custom a value
            if (Action == AddRemoveQtyAction.Add)
                Quantity += 1;
           else if(Quantity > 0)
                Quantity -= 1;
        }

        protected override void OnDetachingFrom(Image view)
        {
            view.GestureRecognizers.Remove(tapRecognizer);
            tapRecognizer.Tapped -= OnTapRecognizerTapped;
        }
    }
}

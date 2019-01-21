using System;
using System.Windows;

namespace TestLazyLoad.Model
{
    public class StateVisualBinder
    {
        public static readonly DependencyProperty CurrentStateProperty
            = DependencyProperty.RegisterAttached(
                "CurrentState",
                typeof(string),
                typeof(StateVisualBinder),
                new PropertyMetadata(OnCurrentStateChanged));

        public static string GetCurrentState(DependencyObject obj) => (string)obj.GetValue(CurrentStateProperty);

        public static void SetCurrentState(DependencyObject obj, string value)
        {
            obj.SetValue(CurrentStateProperty, value);
        }

        private static void OnCurrentStateChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            if (!(sender is FrameworkElement e))
                throw new Exception($"CurrentState is only supported on {nameof(FrameworkElement)}.");

            if (args.NewValue != null)
            {
                VisualStateManager.GoToElementState(e, (string)args.NewValue, useTransitions: true);
            }
        }
    }
}

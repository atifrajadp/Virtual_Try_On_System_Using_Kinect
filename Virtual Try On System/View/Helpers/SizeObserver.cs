using System.Windows;

namespace Virtual_Try_On_System.View.Helpers
{
    public static class SizeObserver
    {
       
        // The observe property
       
        public static readonly DependencyProperty ObserveProperty = DependencyProperty.RegisterAttached(
            "Observe", typeof(bool), typeof(SizeObserver), new FrameworkPropertyMetadata(OnObserveChanged));
        
        // The observed width property
        
        public static readonly DependencyProperty ObservedWidthProperty = DependencyProperty.RegisterAttached(
            "ObservedWidth", typeof(double), typeof(SizeObserver));
        
        // The observed height property
       
        public static readonly DependencyProperty ObservedHeightProperty = DependencyProperty.RegisterAttached(
            "ObservedHeight", typeof(double), typeof(SizeObserver));
    
        // Gets the observe property value.
       
        public static bool GetObserve(FrameworkElement frameworkElement)
        {
            return (bool)frameworkElement.GetValue(ObserveProperty);
        }
        
        // Sets the observe property value.
       
        public static void SetObserve(FrameworkElement frameworkElement, bool observe)
        {
            frameworkElement.SetValue(ObserveProperty, observe);
        }
      
        // Gets the width of the observed framework element.
       
        public static double GetObservedWidth(FrameworkElement frameworkElement)
        {
            return (double)frameworkElement.GetValue(ObservedWidthProperty);
        }
      
        // Sets the width of the observed framework element.
        
        public static void SetObservedWidth(FrameworkElement frameworkElement, double observedWidth)
        {
            frameworkElement.SetValue(ObservedWidthProperty, observedWidth);
        }
       
        // Gets the height of the observed framework element.
        
        public static double GetObservedHeight(FrameworkElement frameworkElement)
        {
            return (double)frameworkElement.GetValue(ObservedHeightProperty);
        }
      
        // Sets the height of the observed framework element.
        
        public static void SetObservedHeight(FrameworkElement frameworkElement, double observedHeight)
        {
            frameworkElement.SetValue(ObservedHeightProperty, observedHeight);
        }
        
        // Called when [observe changed].
        
        private static void OnObserveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var frameworkElement = (FrameworkElement)dependencyObject;

            if ((bool)e.NewValue)
            {
                frameworkElement.SizeChanged += OnFrameworkElementSizeChanged;
                UpdateObservedSizesForFrameworkElement(frameworkElement);
            }
            else
                frameworkElement.SizeChanged -= OnFrameworkElementSizeChanged;
        }
       
        // Called when [framework element size changed].
       
        private static void OnFrameworkElementSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateObservedSizesForFrameworkElement((FrameworkElement)sender);
        }
       
        // Updates the observed sizes for framework element.
        
        private static void UpdateObservedSizesForFrameworkElement(FrameworkElement frameworkElement)
        {
            frameworkElement.SetCurrentValue(ObservedWidthProperty, frameworkElement.ActualWidth);
            frameworkElement.SetCurrentValue(ObservedHeightProperty, frameworkElement.ActualHeight);
        }
    }
}

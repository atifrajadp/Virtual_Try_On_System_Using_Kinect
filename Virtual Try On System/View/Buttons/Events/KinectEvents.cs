using System.Windows;

namespace Virtual_Try_On_System.View.Buttons.Events
{
    public static class KinectEvents
    {

      
        // Hand cursor enter event
     
        public static readonly RoutedEvent HandCursorEnterEvent
            = EventManager.RegisterRoutedEvent("HandCursorEnter", RoutingStrategy.Bubble
            , typeof(HandCursorEventHandler), typeof(KinectEvents));
      
        // Hand cursor move event
       
        public static readonly RoutedEvent HandCursorMoveEvent
            = EventManager.RegisterRoutedEvent("HandCursorMove", RoutingStrategy.Bubble
            , typeof(HandCursorEventHandler), typeof(KinectEvents));
       
        // Hand cursor leave event
        
        public static readonly RoutedEvent HandCursorLeaveEvent
            = EventManager.RegisterRoutedEvent("HandCursorLeave", RoutingStrategy.Bubble
            , typeof(HandCursorEventHandler), typeof(KinectEvents));
       
        // Hand cursor click event
      
        public static readonly RoutedEvent HandCursorClickEvent
            = EventManager.RegisterRoutedEvent("HandCursorClick", RoutingStrategy.Bubble
            , typeof(HandCursorEventHandler), typeof(KinectEvents));
       
        // The clear3D items event
    
        public static readonly RoutedEvent Clear3DItemsEvent
            = EventManager.RegisterRoutedEvent("Clear3DItems", RoutingStrategy.Direct
            , typeof(RoutedEventHandler), typeof(KinectEvents));

  
        
        // Adds hand cursor enter event handler to the dependency object
       
        public static void AddHandCursorEnterHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(HandCursorEnterEvent, handler);
        }
       
        // Removes hand cursor enter event handler from the dependency object
       
        public static void RemoveHandCursorEnterHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(HandCursorEnterEvent, handler);
        }
      
        // Adds hand cursor move event handler to the dependency object
       
        public static void AddHandCursorMoveHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(HandCursorMoveEvent, handler);
        }
        
        // Removes hand cursor move event handler from the dependency object
        
        public static void RemoveHandCursorMoveHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(HandCursorMoveEvent, handler);
        }
      
        // Adds hand cursor leave event handler to the dependency object
      
        public static void AddHandCursorLeaveHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(HandCursorLeaveEvent, handler);
        }
      
        // Removes hand cursor leave event handler from the dependency object
       
        public static void RemoveHandCursorLeaveHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(HandCursorLeaveEvent, handler);
        }
       
        // Adds hand cursor click event handler to the dependency object
        
        public static void AddHandCursorClickHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(HandCursorClickEvent, handler);
        }
       
        // Removes hand cursor click event handler from the dependency object
      
        public static void RemoveHandCursorClickHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(HandCursorClickEvent, handler);
        }
       
        // Adds the clear3D items handler.
        
        public static void AddClear3DItemsHandler(DependencyObject dependencyObject, RoutedEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(Clear3DItemsEvent, handler);
        }
       
        // Removes the clear3D items handler.
       
        public static void RemoveClear3DItemsHandler(DependencyObject dependencyObject, RoutedEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(Clear3DItemsEvent, handler);
        }

    }
}

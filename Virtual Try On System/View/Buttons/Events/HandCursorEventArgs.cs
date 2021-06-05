using System.Windows;

namespace Virtual_Try_On_System.View.Buttons.Events
{
    public class HandCursorEventArgs : RoutedEventArgs
    {

     
        // X coordinate 
    
        public double X { get; set; }
       
        // Y coordinate 
      
        public double Y { get; set; }



        // Calls the constructor of RoutedEventArgs class
      
        public HandCursorEventArgs(RoutedEvent routedEvent, double x, double y)
            : base(routedEvent)
        {
            X = x;
            Y = y;
        }

        //Calls the constructor of RoutedEventArgs class
        
        public HandCursorEventArgs(RoutedEvent routedEvent, Point point)
            : base(routedEvent)
        {
            X = point.X;
            Y = point.Y;
        }

    }
}

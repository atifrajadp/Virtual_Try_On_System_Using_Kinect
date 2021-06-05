using System.Windows;

namespace Virtual_Try_On_System.View.Buttons.Events
{
    public class ButtonsManager
    {

        
        // Instance of the HandCursorManager
    
        private static ButtonsManager _instance;
       
        // Prevents from reinitializing the singleton
 
        private static bool _isInitialized;
        
        // Last element hit by cursor
        
        private IInputElement _lastElement;


       
        // Hand cursor manager instance
       
        public static ButtonsManager Instance
        {
            get
            {
                if (!_isInitialized)
                    _instance = Initialize();
                return _instance;
            }
        }


       
        // Initializes a new instance of the ButtonsManager class.
       
        private static ButtonsManager Initialize()
        {
            _isInitialized = true;
            return new ButtonsManager();
        }


        
        // Raises the cursor events.
        
        public void RaiseCursorEvents(IInputElement element, Point cursorPosition)
        {
            element.RaiseEvent(new HandCursorEventArgs(KinectEvents.HandCursorMoveEvent, cursorPosition));
            if (element != _lastElement)
            {
                if (_lastElement != null)
                    _lastElement.RaiseEvent(new HandCursorEventArgs(KinectEvents.HandCursorLeaveEvent, cursorPosition));
                element.RaiseEvent(new HandCursorEventArgs(KinectEvents.HandCursorEnterEvent, cursorPosition));
            }
            _lastElement = element;
        }
        
        // Raises the cursor leave event.
       
        public void RaiseCursorLeaveEvent(Point cursorPosition)
        {
            if (_lastElement == null) return;
            _lastElement.RaiseEvent(new HandCursorEventArgs(KinectEvents.HandCursorLeaveEvent, cursorPosition));
            _lastElement = null;
        }

    }
}

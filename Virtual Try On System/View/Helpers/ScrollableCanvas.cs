using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Virtual_Try_On_System.View.Buttons.Events;
namespace Virtual_Try_On_System.View.Helpers
{
    public class ScrollableCanvas : ItemsControl
    {

       
        // Number of seconds to check position of Hand
       
        private const int EnterTimeout = 4;
       
        // Translation of controls in panels
       
        private const int Distance = 10;
        
        // Number of milliseconds of animation
      
        private const int TimeOfAnimation = 10;
        
        // The minimum height factor
       
        private const double MinHeightFactor = 0.2;
       
        // The maximum height factor
       
        private const double MaxHeightFactor = 0.4;

        // Position of LeftPanel
        
        private Point _leftPanelPosition;
      
        // Determines how much time elapsed since hand position over canvas checked
       
        private readonly DispatcherTimer _enterTimer;
      
        // Number of elapsed ticks for _enterTimer
       
        private int _enterTimerTicks;
        
        // Determines if hand is over canvas
       
        private bool _isHandOverCanvas;
       
        // Actual hand position
       
        private Point _handPosition;
        
        // Position of last button in panel
        
        double _lastButtonPositionY;
       
        // Position of first button in panel
       
        double _firstButtonPositionY;
       
        // Start point of animation
       
        double _startAnimationPoint;
       
        // Defines if buttons are moving
        
        private bool _isMoved;
      
        // Top boundary to start scroll up
        
        private double _canvasMinHeight;
        
        // Bottom boundary to start scroll down
        
        private double _canvasMaxHeight;
       
        // Hand cursor enter event
       
        public static readonly RoutedEvent HandCursorEnterEvent
            = KinectEvents.HandCursorEnterEvent.AddOwner(typeof(ScrollableCanvas));
       
        // Hand cursor leave event
      
        public static readonly RoutedEvent HandCursorLeaveEvent
            = KinectEvents.HandCursorLeaveEvent.AddOwner(typeof(ScrollableCanvas));
        
        // Hand cursor move event
        
        public static readonly RoutedEvent HandCursorMoveEvent
            = KinectEvents.HandCursorMoveEvent.AddOwner(typeof(ScrollableCanvas));
      
        // Hand cursor enter event handler
       
        public event HandCursorEventHandler HandCursorEnter
        {
            add { AddHandler(HandCursorEnterEvent, value); }
            remove { RemoveHandler(HandCursorEnterEvent, value); }
        }
       
        // Hand cursor leave event handler
        
        public event HandCursorEventHandler HandCursorLeave
        {
            add { AddHandler(HandCursorLeaveEvent, value); }
            remove { RemoveHandler(HandCursorLeaveEvent, value); }
        }
      
        // Hand cursor move event handler
        
        public event HandCursorEventHandler HandCursorMove
        {
            add { AddHandler(HandCursorLeaveEvent, value); }
            remove { RemoveHandler(HandCursorLeaveEvent, value); }
        }
       
        // Initializes a new instance of the <see cref="ScrollableCanvas"/> class.
       
        public ScrollableCanvas()
        {
            HandCursorEnter += ScrollableCanvas_HandCursorEnter;
            HandCursorLeave += ScrollableCanvas_HandCursorLeave;
            HandCursorMove += ScrollableCanvas_HandCursorMove;

            _enterTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 1) };
            _enterTimerTicks = 0;
            _enterTimer.Tick += EnterTimer_Tick;

            Items.CurrentChanged += (sender, args) => { _startAnimationPoint = 0; };
        }
        
        // Counts the number of timer ticks of_enterTimer
       
        private void EnterTimer_Tick(object sender, EventArgs e)
        {
            _enterTimerTicks++;

            if (_enterTimerTicks < EnterTimeout)
                return;

            _enterTimer.Stop();
            _enterTimerTicks = 0;
            if (_isHandOverCanvas)
                RaiseEvent(new HandCursorEventArgs(HandCursorEnterEvent, _handPosition));
        }
   
        // Handles HandCursorMove event
       
        private void ScrollableCanvas_HandCursorMove(object sender, HandCursorEventArgs args)
        {
            if (_isHandOverCanvas)
                _handPosition = new Point(args.X, args.Y);
        }
        
        // Handles HandCursorLeave event
        
        private void ScrollableCanvas_HandCursorLeave(object sender, HandCursorEventArgs args)
        {
            _isHandOverCanvas = false;
        }
        
        // Handles HandCursorEnter event
      
        private void ScrollableCanvas_HandCursorEnter(object sender, HandCursorEventArgs args)
        {
            if (!_isHandOverCanvas)
                _handPosition = new Point(args.X, args.Y);
            _isHandOverCanvas = true;
            _isMoved = true;

            StackPanel stackPanel = (Name == "LeftScrollableCanvas") ? FindChild<StackPanel>(Application.Current.MainWindow, "LeftStackPanel") : FindChild<StackPanel>(Application.Current.MainWindow, "RightStackPanel");
            if (stackPanel.Children.Count == 0)
                return;

            SetPositions(stackPanel);
            if (!CheckHandPosition(stackPanel))
                return;

            if (_isHandOverCanvas)
                _enterTimer.Start();
        }
       
        // Checks hand position and runs MoveButtons method
        
        private bool CheckHandPosition(StackPanel stackPanel)
        {
            if (_handPosition.Y > _canvasMinHeight && _handPosition.Y < _canvasMaxHeight)
                return false;
            if (_handPosition.Y > _canvasMaxHeight)
                while (_isMoved && _lastButtonPositionY + _startAnimationPoint > _canvasMaxHeight)
                    MoveButtons(stackPanel, true);
            else if (_handPosition.Y < _canvasMinHeight)
                while (_isMoved && _firstButtonPositionY + _startAnimationPoint < _firstButtonPositionY)
                    MoveButtons(stackPanel, false);
            return true;
        }
        
        // Sets positions of first and last buttons in panel
        
        private void SetPositions(StackPanel stackPanel)
        {
            if (_firstButtonPositionY == 0)
                _firstButtonPositionY = stackPanel.Children[0].TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0)).Y;
            _lastButtonPositionY = stackPanel.Children[stackPanel.Children.Count - 1].TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0)).Y;

            if (_leftPanelPosition.X == 0 && _leftPanelPosition.Y == 0)
            {
                _leftPanelPosition = TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
                _canvasMinHeight = ActualHeight * MinHeightFactor + _leftPanelPosition.Y;
                _canvasMaxHeight = ActualHeight * MaxHeightFactor + _leftPanelPosition.Y;
            }
        }
      
        // Moves buttons in panels
       
        private void MoveButtons(StackPanel stackpanel, bool moveUp)
        {
            _startAnimationPoint = moveUp ? _startAnimationPoint - Distance : _startAnimationPoint + Distance;

            Button button;
            TranslateTransform translation = new TranslateTransform();
            DoubleAnimation animation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromMilliseconds(TimeOfAnimation),
                From = moveUp ? _startAnimationPoint + Distance : _startAnimationPoint,
                To = moveUp ? _startAnimationPoint : _startAnimationPoint + Distance
            };

            foreach (var control in stackpanel.Children)
            {
                button = FindChild<Button>(control as ContentPresenter, "");
                if (button != null)
                    button.RenderTransform = translation;
            }

            translation.BeginAnimation(TranslateTransform.YProperty, animation);
            _isMoved = !_isMoved;
        }
        
        // Find child control in Visual Tree Helper of parent control
        
        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null)
                return null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                T childType = child as T;
                if (childType == null)
                {
                    T foundChild = FindChild<T>(child, childName);
                    if (foundChild != null)
                        return foundChild;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    if (frameworkElement != null && frameworkElement.Name == childName)
                        return (T)child;
                }
                else
                    return (T)child;
            }
            return null;
        }

    }
}

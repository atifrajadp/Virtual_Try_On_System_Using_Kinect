using Virtual_Try_On_System.View.Buttons.Events;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Virtual_Try_On_System.View.Buttons
{
    public class KinectButton : Button
    {

      
        // Number of seconds that Click event occures
       
        private const int ClickTimeout = 40;
       
        // Number of seconds after Click event
        
        private const int AfterClickTimeout = 10;


       
        // Determines if hand is over button
       
        private bool _handIsOverButton;
       
        // The last hand position
      
        private Point _lastHandPosition;

        
        // Hand cursor enter event
       
        public static readonly RoutedEvent HandCursorEnterEvent
            = KinectEvents.HandCursorEnterEvent.AddOwner(typeof(KinectButton));
       
        // Hand cursor move event
       
        public static readonly RoutedEvent HandCursorMoveEvent
            = KinectEvents.HandCursorMoveEvent.AddOwner(typeof(KinectButton));
       
        // Hand cursor leave event
      
        public static readonly RoutedEvent HandCursorLeaveEvent
            = KinectEvents.HandCursorLeaveEvent.AddOwner(typeof(KinectButton));
       
        // Hand cursor click event
     
        public static readonly RoutedEvent HandCursorClickEvent
            = KinectEvents.HandCursorClickEvent.AddOwner(typeof(KinectButton));


       
        // Hand cursor enter event handler
       
        public event HandCursorEventHandler HandCursorEnter
        {
            add { AddHandler(HandCursorEnterEvent, value); }
            remove { RemoveHandler(HandCursorEnterEvent, value); }
        }
       
        // Hand cursor move event handler
     
        public event HandCursorEventHandler HandCursorMove
        {
            add { AddHandler(HandCursorMoveEvent, value); }
            remove { RemoveHandler(HandCursorMoveEvent, value); }
        }
        
        // Hand cursor leave event handler
       
        public event HandCursorEventHandler HandCursorLeave
        {
            add { AddHandler(HandCursorLeaveEvent, value); }
            remove { RemoveHandler(HandCursorLeaveEvent, value); }
        }
       
        // Hand cursor click event handler
        
        public event HandCursorEventHandler HandCursorClick
        {
            add { AddHandler(HandCursorClickEvent, value); }
            remove { RemoveHandler(HandCursorClickEvent, value); }
        }

     
        // Get information if hand is over button
       
        public bool HandIsOverButton
        {
            get { return _handIsOverButton; }
        }
       
        // Has Click event occured
      
        public bool IsClicked
        {
            get { return (bool)GetValue(IsClickedProperty); }
            set { SetValue(IsClickedProperty, value); }
        }
        
        // Gets or sets information about sounds.
        
        public bool AreSoundsOn
        {
            get { return (bool)GetValue(AreSoundsOnProperty); }
            set { SetValue(AreSoundsOnProperty, value); }
        }
       
        // Gets or sets the command to invoke when this button is pressed.
       
        public new ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
      
        // Number of elapsed ticks for _clickTimer
      
        protected int ClickTicks { get; set; }
        
        // Number of elapsed ticks for _afterClickTimer
      
        protected int AfterClickTicks { get; set; }
        
        // Determines how much time elapsed since HandCursorEnterEvent occured
       
        protected DispatcherTimer ClickTimer { get; private set; }
       
        // Determines how much time elapsed since HandCursorClickEvent occured
      
        protected DispatcherTimer AfterClickTimer { get; private set; }


       
        // IsClicked dependency property
     
        public static readonly DependencyProperty IsClickedProperty = DependencyProperty.Register(
            "IsClicked", typeof(bool), typeof(KinectButton), new PropertyMetadata(default(bool)));
      
        // AreSoundsOn dependency property
       
        public static readonly DependencyProperty AreSoundsOnProperty = DependencyProperty.Register(
            "AreSoundsOn", typeof(bool), typeof(KinectButton), new PropertyMetadata(default(bool)));


       
        // Initializes a new instance of the <see cref="KinectButton"/> class.
     
        public KinectButton()
        {
            SetValue(IsClickedProperty, false);
            _handIsOverButton = false;
            ClickTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 1) };
            ClickTicks = 0;
            ClickTimer.Tick += ClickTimer_Tick;
            AfterClickTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 1) };
            AfterClickTicks = 0;
            AfterClickTimer.Tick += AfterClickTimer_Tick;

            HandCursorEnter += KinectButton_HandCursorEnter;
            HandCursorMove += KinectButton_HandCursorMove;
            HandCursorLeave += KinectButton_HandCursorLeave;
            HandCursorClick += KinectButton_HandCursorClick;
        }

       
        // Handles HandCursorEnter event
        
        protected void KinectButton_HandCursorEnter(object sender, HandCursorEventArgs args)
        {
            _handIsOverButton = true;
            ClickTimer.Start();
        }
    
        // Handles HandCursorMove event
       
        protected void KinectButton_HandCursorMove(object sender, HandCursorEventArgs args)
        {
            _lastHandPosition = new Point(args.X, args.Y);
        }
       
        // Handles HandCursorLeave event
       
        protected void KinectButton_HandCursorLeave(object sender, HandCursorEventArgs args)
        {
            _handIsOverButton = false;
            if (IsClicked)
                SetValue(IsClickedProperty, false);
            ResetTimer(ClickTimer);
        }
       
        // Counts the number of timer ticks of_clickTimer
        
        private void ClickTimer_Tick(object sender, EventArgs e)
        {
            ClickTicks++;

            if (ClickTicks <= ClickTimeout)
                return;

            ResetTimer(ClickTimer);
            RaiseEvent(new HandCursorEventArgs(HandCursorClickEvent, _lastHandPosition));
        }
        
        // Counts the number of timer ticks of _afterClickTimer
       
        private void AfterClickTimer_Tick(object sender, EventArgs e)
        {
            AfterClickTicks++;

            if (AfterClickTicks <= AfterClickTimeout)
                return;

            ResetTimer(AfterClickTimer);
            SetValue(IsClickedProperty, false);
        }
       
        // Imitates the click event
      
        protected virtual void KinectButton_HandCursorClick(object sender, HandCursorEventArgs args)
        {
            SetValue(IsClickedProperty, true);
            AfterClickTimer.Start();
        }
        
        // Resets the timer
        
        protected virtual void ResetTimer(DispatcherTimer timer)
        {
            timer.Stop();
            if (timer == ClickTimer)
                ClickTicks = 0;
            else
                AfterClickTicks = 0;
        }

    }
}

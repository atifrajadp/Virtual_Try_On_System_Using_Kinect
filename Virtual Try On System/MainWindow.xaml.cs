using System.Windows;
using System.Windows.Controls;
using Virtual_Try_On_System.View.Buttons.Events;

namespace Virtual_Try_On_System
{

    public partial class MainWindow
    {
        
        // Gets or sets the position of the left hand.
        public Point LeftPosition
        {
            get { return (Point)GetValue(LeftPositionProperty); }
            set { SetValue(LeftPositionProperty, value); }
        }
        // Gets or sets the position of the right hand.
        
        public Point RightPosition
        {
            get { return (Point)GetValue(RightPositionProperty); }
            set { SetValue(RightPositionProperty, value); }
        }

       
        // The right hand position property
        public static readonly DependencyProperty RightPositionProperty =
            DependencyProperty.Register("RightPosition", typeof(Point), typeof(MainWindow)
            , new FrameworkPropertyMetadata(new Point(), Hand_PropertyChanged));

        // The left hand position property
        public static readonly DependencyProperty LeftPositionProperty =
            DependencyProperty.RegisterAttached("LeftPosition", typeof(Point), typeof(MainWindow)
            , new FrameworkPropertyMetadata(new Point(), Hand_PropertyChanged));
       
        //Constructor of Main Window
        public MainWindow()
        {
            InitializeComponent();
            Loaded += ((sender, e) => ClothesArea.SetTransformMatrix());
        }

        // Handles the PropertyChanged event of the Hand.
        private static void Hand_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainWindow window = d as MainWindow;
            if (window != null)
                window.HandleHandMoved(window.LeftPosition, window.RightPosition);
        }

        // Handles the hand moved event.   
        private void HandleHandMoved(Point leftHand, Point rightHand)
        {
            HandCursor.Visibility = Visibility.Collapsed;

            var element = (QuanityGrid.Visibility == Visibility.Visible) ? QuanityGrid.InputHitTest(leftHand) : ButtonPanelsCanvas.InputHitTest(leftHand);
            var hand = leftHand;

            if (!(element is UIElement))
            {
                element = (QuanityGrid.Visibility == Visibility.Visible) ? QuanityGrid.InputHitTest(rightHand) : ButtonPanelsCanvas.InputHitTest(rightHand);
                hand = rightHand;
                if (!(element is UIElement))
                {
                    ButtonsManager.Instance.RaiseCursorLeaveEvent(leftHand);
                    return;
                }
            }

            HandCursor.Visibility = Visibility.Visible;
            Canvas.SetLeft(HandCursor, hand.X - HandCursor.ActualWidth / 2.0);
            Canvas.SetTop(HandCursor, hand.Y - HandCursor.ActualHeight / 2.0);
            ButtonsManager.Instance.RaiseCursorEvents(element, hand);
        }
    }
}

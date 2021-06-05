using Virtual_Try_On_System.View.Buttons.Events;
using Virtual_Try_On_System.View_Model;
using System.Windows.Threading;

namespace Virtual_Try_On_System.View.Buttons
{
    class KinectRepeatableButton : KinectButton
    {
       
        // Imitates the click event for KinectSizeButton
        
        protected override void KinectButton_HandCursorClick(object sender, HandCursorEventArgs args)
        {
            SetValue(IsClickedProperty, true);
            AfterClickTimer.Start();
        }
       
        // Resets the timer
      
        protected override void ResetTimer(DispatcherTimer timer)
        {
            timer.Stop();
            if (timer == ClickTimer)
                ClickTicks = 0;
            else
            {
                AfterClickTicks = 0;
                if (HandIsOverButton)
                    ClickTimer.Start();
            }
        }
    

    }
}

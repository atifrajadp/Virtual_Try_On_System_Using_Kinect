using System.Drawing;
using System.Windows;
namespace Virtual_Try_On_System.View_Model.ButtonItems.TopMenuButtons
{
    public class MenuButtonViewModel : TopMenuButtonViewModel
    {

        // Calls the constructor of TopMenubuttonViewModel class
        
        public MenuButtonViewModel(Bitmap image)
            : base(image)
        { }
       
        // Shows or hides all top buttons
        
        public override void ClickExecuted()
        {
            PlaySound();
            if (TopMenuManager.Instance.ActualTopMenuButtons == TopMenuManager.Instance.AllButtons)
                ClearMenu();
            else
            {
                TopMenuManager.Instance.ActualTopMenuButtons = TopMenuManager.Instance.AllButtons;
                TopMenuManager.Instance.CameraButtonVisibility = Visibility.Visible;
                TopMenuManager.Instance.OrderButtonsVisibility = Visibility.Collapsed;
            }

        }
    }
}

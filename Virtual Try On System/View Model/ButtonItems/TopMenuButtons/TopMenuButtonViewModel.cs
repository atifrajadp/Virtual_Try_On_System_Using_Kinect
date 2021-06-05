using System.Drawing;
using System.Windows;

namespace Virtual_Try_On_System.View_Model.ButtonItems.TopMenuButtons
{
    public abstract class TopMenuButtonViewModel : ButtonViewModelBase
    {

        //  Calls the constructor of TopMenubuttonViewModel class
        
        public TopMenuButtonViewModel(Bitmap image)
        {
            Image = image;
        }
       
        // Clears additional buttons in top menu
        
        public void ClearMenu()
        {
            TopMenuManager.Instance.ActualTopMenuButtons = null;
            TopMenuManager.Instance.CameraButtonVisibility = Visibility.Collapsed;
            TopMenuManager.Instance.OrderButtonsVisibility = Visibility.Collapsed;
        }
    }
}

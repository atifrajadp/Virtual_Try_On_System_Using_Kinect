using System.Drawing;

namespace Virtual_Try_On_System.View_Model.ButtonItems.TopMenuButtons
{
    public class ScreenshotButtonViewModel : TopMenuButtonViewModel
    {


        //  Calls the constructor of TopMenubuttonViewModel class
        
        public ScreenshotButtonViewModel(Bitmap image)
            : base(image)
        { }
    

       
        // Hides all top buttons
        
        public override void ClickExecuted()
        {
            PlaySound();
            ClearMenu();
        }
  
    }
}

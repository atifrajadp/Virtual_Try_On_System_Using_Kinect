using System.Drawing;

namespace Virtual_Try_On_System.View_Model.ButtonItems.TopMenuButtons
{
    class SoundsButtonViewModel : TopMenuButtonViewModel
    {
        //  Calls the constructor of TopMenubuttonViewModel class
        
        public SoundsButtonViewModel(Bitmap image)
            : base(image)
        { }

       
        // Turns off/on sounds in application
        
        public override void ClickExecuted()
        {
            TopMenuManager.Instance.SoundsOn = !TopMenuManager.Instance.SoundsOn;
            PlaySound();
            ClearMenu();
        }
    }
}

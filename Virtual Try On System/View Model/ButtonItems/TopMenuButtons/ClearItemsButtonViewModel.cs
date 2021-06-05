using System.Drawing;

namespace Virtual_Try_On_System.View_Model.ButtonItems.TopMenuButtons
{
    class ClearItemsButtonViewModel : TopMenuButtonViewModel
    {
       
        // Calls the constructor of TopMenubuttonViewModel class
       
        public ClearItemsButtonViewModel(Bitmap image)
            : base(image)
        { }
       
        // Shows buttons to clear items
       
        public override void ClickExecuted()
        {
            PlaySound();
            ClearMenu();
            TopMenuManager.Instance.ActualTopMenuButtons = TopMenuManager.Instance.ClearButtons;
        }
    }
}

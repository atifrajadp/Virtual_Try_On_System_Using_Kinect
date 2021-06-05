using Virtual_Try_On_System.Model.ClothingItems;
using System.Drawing;
using Virtual_Try_On_System.View_Model.Helpers;
namespace Virtual_Try_On_System.View_Model.ButtonItems.TopMenuButtons
{
    class ClearSetButtonViewModel : TopMenuButtonViewModel
    {

        //  Calls the constructor of TopMenubuttonViewModel class
       
        public ClearSetButtonViewModel(Bitmap image)
            : base(image)
        { }
        
        // Clears chosen set
        
        public override void ClickExecuted()
        {
            PlaySound();
            ClothingManager.Instance.ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>();
            ClearMenu();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;

namespace Virtual_Try_On_System.View_Model.ButtonItems.TopMenuButtons
{
    class OrderButtonViewModel : TopMenuButtonViewModel
    {
       
        //  Calls the constructor of TopMenubuttonViewModel class
        public OrderButtonViewModel(Bitmap image)
            : base(image)
        { }


     
        // Shows the buttons to select size for ordering
        public override void ClickExecuted()
        {
            PlaySound();
            ClearMenu();

            TopMenuManager.Instance.OrderingButtons = TopMenuManager.Instance.OrderButtons;
            TopMenuManager.Instance.OrderButtonsVisibility = Visibility.Visible;
           
        }

    }
}

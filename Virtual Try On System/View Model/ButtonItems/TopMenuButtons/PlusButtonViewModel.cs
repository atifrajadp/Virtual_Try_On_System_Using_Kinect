using System.Drawing;
using System.Windows;

namespace Virtual_Try_On_System.View_Model.ButtonItems.TopMenuButtons
{
    public class PlusButtonViewModel : TopMenuButtonViewModel
    {
       
        public static int l=2;
        public static int a;
       
        
       //Calls the constructor of TopMenuButtonViewModel
        public PlusButtonViewModel(Bitmap image)
            : base(image)
        {  }
      

        //Click event for plus button
        public override void ClickExecuted()
        {
            PlaySound();
           
                a = l++;
                (Application.Current.MainWindow as MainWindow).QuantityText.Text = "Quantity: " + a;
           
            
        }
     
    }
}

using System.Drawing;
using System.Windows;
using System.Data.SqlClient;
using Microsoft.SqlServer;
using System.Timers;
using System.Windows.Threading;
using System;
using Virtual_Try_On_System.View.Buttons.Events;
using System.Threading;


namespace Virtual_Try_On_System.View_Model.ButtonItems.TopMenuButtons
{
    public class OkButtonViewModel : TopMenuButtonViewModel
    {
   
        public static int l;


        //  Calls the constructor of TopMenubuttonViewModel class
        public OkButtonViewModel(Bitmap image)
            : base(image)
        {
            
        }



       
        /// Confirms the order by clicking this button
        public override void ClickExecuted()
        {
            PlaySound();
            if ((Application.Current.MainWindow as MainWindow).QuantityText.Text != "You Have Successfully Ordered the last item you selected and " + Environment.NewLine + "your Order Id is \t" + l)
            {

                string ConStr = @"Data Source=ATIF;Initial Catalog=Inventory;Integrated Security=True";
            SqlConnection con = new SqlConnection(ConStr);
            con.Open();

            string query = "update Ordered_items set Item_quantity='" + PlusButtonViewModel.a + "' where Id=(select max(Id) from Ordered_items)";

            SqlCommand com = new SqlCommand(query, con);


            com.ExecuteNonQuery();
            string que = "select max(Id) from Ordered_items";
            SqlCommand cmd = new SqlCommand(que, con);
          
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                l = (reader.GetInt32(0));
            }
            reader.Close();


            (Application.Current.MainWindow as MainWindow).QuantityText.Text = "You Have Successfully Ordered the last item you selected and "+ Environment.NewLine + "your Order Id is \t"+ l;
            PlusButtonViewModel.l = 2;
            }else
            {
                TopMenuManager.Instance.QuantityGridVisibility = Visibility.Collapsed;
            }
            
            
           

            
            
        } 
    }
}

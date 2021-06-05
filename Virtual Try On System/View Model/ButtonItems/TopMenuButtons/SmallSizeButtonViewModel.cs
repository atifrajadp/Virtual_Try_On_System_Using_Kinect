using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data.SqlClient;
using Microsoft.SqlServer;
using System.Windows;

namespace Virtual_Try_On_System.View_Model.ButtonItems.TopMenuButtons
{
    class SmallSizeButtonViewModel : TopMenuButtonViewModel
    {


        public static string l;
        public static Bitmap img;

        //  Calls the constructor of TopMenubuttonViewModel class
        public SmallSizeButtonViewModel(Bitmap image)
            : base(image)
        { }



        //Click event for small size button
        public override void ClickExecuted()
        {
            
            PlaySound();
            string ConStr = @"Data Source=ATIF;Initial Catalog=Inventory;Integrated Security=True";
            SqlConnection con = new SqlConnection(ConStr);
            con.Open();

            string query = "insert into Ordered_items(Item_name,Item_image,Item_size) select Item_name, Item_image,'Small' from Selected_items where Id=(select max(Id) from Selected_items)";

            SqlCommand com = new SqlCommand(query, con);


            com.ExecuteNonQuery();

           TopMenuManager.Instance.QuantityGridVisibility = Visibility.Visible;
           (Application.Current.MainWindow as MainWindow).QuantityText.Text = "Quantity: 1 ";

          
        }
    }
}

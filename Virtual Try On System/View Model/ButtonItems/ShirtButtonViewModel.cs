using Virtual_Try_On_System.Model.ClothingItems;
using Microsoft.Kinect;
using System.Windows;
using System.Data.SqlClient;
using Microsoft.SqlServer;

namespace Virtual_Try_On_System.View_Model.ButtonItems
{
    public class ShirtButtonViewModel : ClothingButtonViewModel
    {

        // Tolerance of the width

        public string last_shirt;

        //instance of ShirtButtonViewModel class

        private static ShirtButtonViewModel _instance;
       
        // Gets or sets the bottom joint to track scale.
        
        public JointType BottomJointToTrackScale = JointType.Spine;

      
        
        // Calls the Constructor of CLothingButtonViewModel
      
        public ShirtButtonViewModel(ClothingItemBase.ClothingType type, 
            string pathToModel)
            : base(type,  pathToModel)
        {
            Ratio = 1.2;
            DeltaY = 0.95;
        }
     
        // Executes when the Category button was hit.
 
        public override void ClickExecuted()
        {
           
            PlaySound();
            ClothingManager.Instance.AddClothingItem<ShirtItem>(Category, ModelPath, BottomJointToTrackScale, Ratio, DeltaY);
             last_shirt = this.ModelPath;
            string last =  last_shirt.Substring(24);
            string l = last.Remove(last.Length - 4);
            var img = this.Image;
            string ConStr = @"Data Source=ATIF;Initial Catalog=Inventory;Integrated Security=True";
            SqlConnection con = new SqlConnection(ConStr);
            con.Open();
          string  query = "insert into Selected_items(Item_name, Item_image) values ('"+l+"', '"+img+"')";
          SqlCommand com = new SqlCommand(query, con);
          com.ExecuteNonQuery();


        }
    }
}

using Virtual_Try_On_System.Model.ClothingItems;
using Microsoft.Kinect;
using System.Data.SqlClient;
using Microsoft.SqlServer;



namespace Virtual_Try_On_System.View_Model.ButtonItems
{
    public class TrouserButtonViewModel : ClothingButtonViewModel
    {
        public string last;
       
      
        // Gets or sets the bottom joint to track scale.
       
       
        public JointType BottomJointToTrackScale = JointType.KneeRight;
       
        // Calls the Constructor of ClothingButtonViewModel
       
        public TrouserButtonViewModel(ClothingItemBase.ClothingType type, string pathToModel)
            : base(type, pathToModel)
        {
            Ratio = 0.9;
            DeltaY = 1;
        }

        
        // Executes when the Category button was hit.
      
        public override void ClickExecuted()
        {
            PlaySound();
            ClothingManager.Instance.AddClothingItem<ShirtItem>(Category, ModelPath, BottomJointToTrackScale, Ratio, DeltaY);
           var last_pant = this.ModelPath;
            string last = last_pant.Substring(23);
            string l = last.Remove(last.Length - 4);
            var img = this.Image;
            string ConStr = @"Data Source=ATIF;Initial Catalog=Inventory;Integrated Security=True";
            SqlConnection con = new SqlConnection(ConStr);
            con.Open();
            string query = "insert into Selected_items(Item_name, Item_image) values ('" + l + "', '" + img + "')";
            SqlCommand com = new SqlCommand(query, con);
            com.ExecuteNonQuery();
        }

    }
}

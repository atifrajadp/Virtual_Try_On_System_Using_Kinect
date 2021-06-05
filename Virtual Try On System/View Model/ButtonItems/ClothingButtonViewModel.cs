using Virtual_Try_On_System.Model.ClothingItems;
using Microsoft.Kinect;

namespace Virtual_Try_On_System.View_Model.ButtonItems
{
    public abstract class ClothingButtonViewModel : ButtonViewModelBase
    {
       
        // Category of item
       
        private ClothingItemBase.ClothingType _category;
        

       
        // Gets category of item
        
        public ClothingItemBase.ClothingType Category
        {
            get { return _category; }
        }
       
        // Gets type of item
       
       
        
        // Gets or sets the model path.
        
        public string ModelPath { get; set; }
        
        // Hips width with margins
       
        public double Ratio { get; set; }
        
        // The factor to move model in Y coordinate
      
        public double DeltaY { get; set; }
      
        // Initializes a new instance of the <see cref="ClothingButtonViewModel"/> class.
       
        protected ClothingButtonViewModel(ClothingItemBase.ClothingType category,  string pathToModel)
        {
            _category = category;
            ModelPath = pathToModel;
        }
    }
}


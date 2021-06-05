using System.Collections.Generic;
using System.Collections.ObjectModel;
using Virtual_Try_On_System.Model.ClothingItems;

namespace Virtual_Try_On_System.View_Model.ButtonItems
{
    public class ClothingCategoryButtonViewModel : ButtonViewModelBase
    {

        public ClothingItemBase.ClothingType _type;
      
   
        // List of clothes in current category
       
        private List<ClothingButtonViewModel> _clothes;
        
        // Gets or sets the clothes list.
       
        public List<ClothingButtonViewModel> Clothes
        {
            get { return _clothes; }
            set
            {
                if (_clothes == value)
                    return;
                _clothes = value;
                OnPropertyChanged("Clothes");
            }
        }
     
       
        // Executes when the Category button was hit.
      
        public override void ClickExecuted()
        {
            PlaySound();
            if (ClothingManager.Instance.Clothing != null && ClothingManager.Instance.Clothing.Count != 0
                && ClothingManager.Instance.Clothing[0].Category == Clothes[0].Category)
                return;
            ClothingManager.Instance.LastChosenCategory = this;
            ClothingManager.Instance.Clothing = new ObservableCollection<ClothingButtonViewModel>();
            foreach (var cloth in Clothes)
                    ClothingManager.Instance.Clothing.Add(cloth);
        }


        // Constructor of ClothingCategoryButtonViewModel
        
        public ClothingCategoryButtonViewModel(ClothingItemBase.ClothingType type)
        {
            _type = type;
        }
       
    }
}

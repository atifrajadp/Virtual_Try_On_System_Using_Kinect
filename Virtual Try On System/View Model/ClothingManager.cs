using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Virtual_Try_On_System.Model.ClothingItems;
using Virtual_Try_On_System.View_Model.ButtonItems;
using Virtual_Try_On_System.View_Model.Helpers;
using Microsoft.Kinect;
namespace Virtual_Try_On_System.View_Model
{
    public sealed class ClothingManager : ViewModelBase
    {
        private ObservableCollection<ClothingCategoryButtonViewModel> _actualClothingCategories;
       
        // Only instance of ClothingManager
       
        private static ClothingManager _instance;
      
        // The chosen clothing models collection
       
        private OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> _chosenClothesModels;
     
        // The clothing collection
       
        private ObservableCollection<ClothingButtonViewModel> _clothing;
       
        // The viewport transform
       
        private Matrix3D _viewportTransform;
        
        // The camera transform
       
        private Matrix3D _cameraTransform;
      
        // Gets or sets the model importer.
      
        private ModelImporter _importer;

       
        // Gets or sets the clothing categories collection.
       
        public ObservableCollection<ClothingCategoryButtonViewModel> ClothingCategories { get; set; }
       
        // Gets or sets the actually displayed collection of categories
      
        public ObservableCollection<ClothingCategoryButtonViewModel> ActualClothingCategories
        {
            get { return _actualClothingCategories; }
            set
            {
                if (_actualClothingCategories == value)
                    return;
                _actualClothingCategories = value;
                OnPropertyChanged("ActualClothingCategories");
            }
        }
        
        
       
       
        // Gets or sets last chosen category
        
        public ClothingCategoryButtonViewModel LastChosenCategory { get; set; }
        
        // Gets or sets the chosen clothing models collection.
       
        public OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> ChosenClothesModels
        {
            get { return _chosenClothesModels; }
            set
            {
                if (_chosenClothesModels == value)
                    return;
                _chosenClothesModels = value;
                OnPropertyChanged("ChosenClothesModels");
            }
        }
       
        // Gets or sets the available clothing collection.
       
        public ObservableCollection<ClothingButtonViewModel> Clothing
        {
            get { return _clothing; }
            set
            {
                if (_clothing == value)
                    return;
                _clothing = value;
                OnPropertyChanged("Clothing");
            }
        }
      
        // Method with access to only instance of ClothingManager
        
        public static ClothingManager Instance
        {
            get { return _instance ?? (_instance = new ClothingManager()); }
        }
        
        // Gets or sets the viewport transform.
        
        public Matrix3D ViewportTransform
        {
            get { return _viewportTransform; }
            set
            {
                if (_viewportTransform == value)
                    return;
                _viewportTransform = value;
                OnPropertyChanged("ViewportTransform");
            }
        }
       
        // Gets or sets the camera transform.
        
        public Matrix3D CameraTransform
        {
            get { return _cameraTransform; }
            set
            {
                if (_cameraTransform == value)
                    return;
                _cameraTransform = value;
                OnPropertyChanged("CameraTransform");
            }
        }


       
        // Private constructor of ClothingManager. 
      
        private ClothingManager()
        {
            ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>();
            _importer = new ModelImporter();
        }
        

       
        // Updates the item position.
       
        public void UpdateItemPosition(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            foreach (var model in ChosenClothesModels.Values)
                model.UpdateItemPosition(skeleton, sensor, width, height);
        }
       
        // Updates actually displayed clothing categories
       
        public void UpdateActualCategories()
        {
            if (ActualClothingCategories == null)
                ActualClothingCategories = new ObservableCollection<ClothingCategoryButtonViewModel>();

            ActualClothingCategories.Clear();
            foreach (var category in ClothingCategories)
                    ActualClothingCategories.Add(category);
        }
       
        // Adds the clothing item.
       
        public void AddClothingItem<T>(ClothingItemBase.ClothingType category, string modelPath, JointType bottomJoint, double ratio, double deltaY)
        {
            OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmpModels = ChosenClothesModels;
            tmpModels[category] = (ClothingItemBase)Activator.CreateInstance(typeof(T), _importer.Load(modelPath), bottomJoint, ratio, deltaY);
            ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmpModels);
        }
       
    }
}

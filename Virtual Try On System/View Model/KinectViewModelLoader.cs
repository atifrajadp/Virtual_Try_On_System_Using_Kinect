using Virtual_Try_On_System.View_Model;
namespace Virtual_Try_On_System.View_Model
{
    class KinectViewModelLoader
    {

       
        // Kinect view model
       
        static KinectViewModel _kinectViewModel;
      
        // Kinect service
        
        static KinectService _kinectService;
        
        // Gets the view model.
        
        public KinectViewModel KinectViewModel
        {
            get { return _kinectViewModel ?? (_kinectViewModel = new KinectViewModel(_kinectService)); }
        }

        // Constructor of KinectViewModelLoader class
       
        public KinectViewModelLoader()
        {
            _kinectService = new KinectService();
            _kinectService.Initialize();
        }
       
        // Cleanups this instance.
       
        public static void Cleanup()
        {
            _kinectService.Cleanup();
        }
    }
}

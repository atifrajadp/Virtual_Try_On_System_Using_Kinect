
using Virtual_Try_On_System.Model.Debug;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using Microsoft.Kinect;


namespace Virtual_Try_On_System.View_Model
{
    public class KinectService : ViewModelBase, IKinectService
    {

       
        // Captured skeletons
      
        private Skeleton[] _skeletons;
       
        // Current KinectSensor
       
        private KinectSensor _kinectSensor;
      
        // WritableBitmap that source from Kinect camera is written to
        
        private WriteableBitmap _kinectCameraImage;
       
        // Bounds of camera source
       
        private Int32Rect _cameraSourceBounds;
       
        // Number of bytes per line
        
        private int _colorStride;
       
        // User's Hand
        
        private HandTracking _hand;
       
        // The skeleton manager
       
        private SkeletonManager _skeletonManager;

        // Visibility of ErrorGrid 
       
        private Visibility _errorGridVisibility;
        
        // Visibility of ClothesArea 
       
        private Visibility _clothesAreaVisibility;
      
        // The image width
       
        private double _imageWidth;
      
        // The image height
       
        private double _imageHeight;
       
        // The error grid message
        
        private string _errorGridMessage;

       
        // Current KinectSensor
        
        public KinectSensor Kinect
        {
            get { return _kinectSensor; }
            set
            {
                if (_kinectSensor != null)
                {
                    UninitializeKinectSensor(_kinectSensor);
                    _kinectSensor = null;
                }
                if (value != null && value.Status == KinectStatus.Connected)
                {
                    _kinectSensor = value;
                    InitializeKinectSensor(_kinectSensor);
                }
            }
        }
       
        // Gets or sets the Kinect camera image.
        
        public WriteableBitmap KinectCameraImage
        {
            get { return _kinectCameraImage; }
            set
            {
                if (Equals(_kinectCameraImage, value))
                    return;
                _kinectCameraImage = value;
                OnPropertyChanged("KinectCameraImage");
            }
        }
       
        // Gets or sets the hand.
       
        public HandTracking Hand
        {
            get { return _hand; }
            set
            {
                if (_hand == value)
                    return;
                _hand = value;
                OnPropertyChanged("Hand");
            }
        }

        // Gets or sets the skeleton manager.
       
        public SkeletonManager SkeletonManager
        {
            get { return _skeletonManager; }
            set
            {
                if (_skeletonManager == value)
                    return;
                _skeletonManager = value;
                OnPropertyChanged("SkeletonManager");
            }
        }
       
        // Gets or sets the width.
       
        public double Width
        {
            get { return _imageWidth; }
            set
            {
                if (_imageWidth == value)
                    return;
                _imageWidth = value;
                OnPropertyChanged("Width");
            }
        }
       
        // Gets or sets the height.
       
        public double Height
        {
            get { return _imageHeight; }
            set
            {
                if (_imageHeight == value)
                    return;
                _imageHeight = value;
                OnPropertyChanged("Height");
            }
        }
       
        // Gets or sets visibility of ClothesArea
        
        public Visibility ClothesAreaVisibility
        {
            get { return _clothesAreaVisibility; }
            set
            {
                if (_clothesAreaVisibility == value)
                    return;
                _clothesAreaVisibility = value;
                OnPropertyChanged("ClothesAreaVisibility");
            }
        }
      
        // Gets or sets visibility of ErrorGrid
       
        public Visibility ErrorGridVisibility
        {
            get { return _errorGridVisibility; }
            set
            {
                if (_errorGridVisibility == value)
                    return;
                _errorGridVisibility = value;
                OnPropertyChanged("ErrorGridVisibility");
            }
        }
       
        // Gets or sets the error grid message.
        
        public string ErrorGridMessage
        {
            get { return _errorGridMessage; }
            set
            {
                if (_errorGridMessage == value)
                    return;
                _errorGridMessage = value;
                OnPropertyChanged("ErrorGridMessage");
            }
        }

       
        // Enables ColorStream from newly detected KinectSensor and sets output image
       
        private void InitializeKinectSensor(KinectSensor sensor)
        {
            if (sensor != null)
            {
                ColorImageStream colorStream = sensor.ColorStream;
                colorStream.Enable();

                KinectCameraImage = new WriteableBitmap(colorStream.FrameWidth, colorStream.FrameHeight
                    , 96, 96, PixelFormats.Bgr32, null);

                _cameraSourceBounds = new Int32Rect(0, 0, colorStream.FrameWidth, colorStream.FrameHeight);
                _colorStride = colorStream.FrameWidth * colorStream.FrameBytesPerPixel;
                sensor.ColorFrameReady += KinectSensor_ColorFrameReady;

                sensor.SkeletonStream.AppChoosesSkeletons = false;
                sensor.SkeletonStream.Enable();
                _skeletons = new Skeleton[sensor.SkeletonStream.FrameSkeletonArrayLength];
                sensor.SkeletonFrameReady += KinectSensor_SkeletonFrameReady;
                try
                {
                    sensor.Start();
                }
                catch (Exception)
                {
                    UninitializeKinectSensor(sensor);
                    Kinect = null;
                    ErrorGridVisibility = Visibility.Visible;
                    ErrorGridMessage = "Kinect is used by another process." + Environment.NewLine +
                        "Try unplugging and reconnecting the device to your computer." + Environment.NewLine +
                        "Make sure all programs using Kinect have been turned off.";
                }
            }
        }
      
        // Disables ColorStream from disconnected KinectSensor
       
        private void UninitializeKinectSensor(KinectSensor sensor)
        {
            if (sensor == null) return;
            sensor.Stop();
            sensor.ColorFrameReady -= KinectSensor_ColorFrameReady;
            sensor.SkeletonFrameReady -= KinectSensor_SkeletonFrameReady;
            sensor.SkeletonStream.Disable();
            _skeletons = null;
        }
      
        // Handles SkeletonFrameReady event
       
        private void KinectSensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (var frame = e.OpenSkeletonFrame())
            {
                if (frame == null || frame.SkeletonArrayLength == 0)
                    return;
                frame.CopySkeletonDataTo(_skeletons);

                var skeleton = GetPrimarySkeleton(_skeletons);
                if (skeleton == null)
                {
                    ErrorGridVisibility = Visibility.Visible;
                    ErrorGridMessage = "Skeleton not detected or its location lost." + Environment.NewLine +
                        "Wait a moment and check if you are standing at the right distance from the device.";
                    ClothesAreaVisibility = Visibility.Hidden;
                    return;
                }
                if (ClothesAreaVisibility == Visibility.Hidden)
                {
                    ErrorGridVisibility = Visibility.Collapsed;
                    ClothesAreaVisibility = Visibility.Visible;
                }
                Hand.UpdateHandCursor(skeleton, Kinect, Width, Height);
                ClothingManager.Instance.UpdateItemPosition(skeleton, Kinect, Width, Height);

                Brush brush = Brushes.Transparent;
                SkeletonManager.DrawSkeleton(_skeletons, brush, _kinectSensor, Width, Height);
            }
        }
       
        // Handles ColorFrameReady event
       
        private void KinectSensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                if (colorFrame == null) return;
                var pixels = new byte[colorFrame.PixelDataLength];
                colorFrame.CopyPixelDataTo(pixels);

                KinectCameraImage.WritePixels(_cameraSourceBounds, pixels, _colorStride, 0);
                OnPropertyChanged("KinectCameraImage");
            }
        }
       
        // Subscribes for StatusChanged event and initializes KinectSensor
       
        private void DiscoverKinectSensors()
        {
            KinectSensor.KinectSensors.StatusChanged += KinectSensor_StatusChanged;
            Kinect = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
            if (Kinect == null)
            {
                ErrorGridVisibility = Visibility.Visible;
                ErrorGridMessage = "Please connect Kinect";
            }
        }
        
        // Updates KinectSensor
       
        private void KinectSensor_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case KinectStatus.Initializing:
                    ErrorGridVisibility = Visibility.Visible;
                    ErrorGridMessage = "Kinect's Initiation ...";
                    break;
                case KinectStatus.Connected:
                    ErrorGridVisibility = Visibility.Hidden;
                    if (Kinect == null)
                        Kinect = e.Sensor;
                    break;
                case KinectStatus.Disconnected:
                    if (Kinect == e.Sensor)
                    {
                        Kinect = null;
                        Kinect = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
                        if (Kinect == null)
                        {
                            ErrorGridVisibility = Visibility.Visible;
                            ErrorGridMessage = "Connect Kinect to your computer.";
                        }
                    }
                    break;
                case KinectStatus.NotPowered:
                    ErrorGridVisibility = Visibility.Visible;
                    ErrorGridMessage = "Plug the power cord into the power outlet.";
                    break;
                default:
                    ErrorGridVisibility = Visibility.Visible;
                    ErrorGridMessage = "Kinect can not be started. Wait a moment or restart the program.";
                    break;
            }
        }
       
        // Initializes this instance.
       
        public void Initialize()
        {
            Hand = new HandTracking();
            SkeletonManager = new SkeletonManager();
            ErrorGridVisibility = Visibility.Hidden;
            ClothesAreaVisibility = Visibility.Visible;
            DiscoverKinectSensors();
        }
       
        // Looks for the closest skeleton
       
        public static Skeleton GetPrimarySkeleton(Skeleton[] skeletons)
        {
            Skeleton skeleton = null;

            if (skeletons != null)
                foreach (Skeleton skelet in skeletons.Where(s => s.TrackingState == SkeletonTrackingState.Tracked))
                    if (skeleton == null || skelet.Position.Z < skeleton.Position.Z)
                        skeleton = skelet;

            return skeleton;
        }
       
        // Mapps a point from Kinect space to canvas space
       
        public static Point3D GetJointPoint(Joint joint, KinectSensor sensor, double width, double height)
        {
            var point = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(joint.Position, sensor.DepthStream.Format);

            return new Point3D(point.X * (width / sensor.DepthStream.FrameWidth)
                , point.Y * (height / sensor.DepthStream.FrameHeight), point.Depth);
        }
      
        // Calculates the distance between joints.
       
        public static Point CalculateDistanceBetweenJoints(Point joint1, Point joint2)
        {
            return new Point(Math.Abs(joint1.X - joint2.X), Math.Abs(joint1.Y - joint2.Y));
        }
       
        // Cleanups this instance.
        
        public void Cleanup()
        {
            Kinect = null;
        }

    }
}

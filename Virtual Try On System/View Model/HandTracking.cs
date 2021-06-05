using System.Windows;
using Microsoft.Kinect;
namespace Virtual_Try_On_System.View_Model
{
    public class HandTracking : ViewModelBase
    {
       
        // The position of the left hand
       
        private Point _leftPosition;
       
        // The position of the right hand
       
        private Point _rightPosition;

        
        // Gets or sets the Position of the left hand.
       
        public Point LeftPosition
        {
            get { return _leftPosition; }
            set
            {
                if (_leftPosition == value)
                    return;
                _leftPosition = value;
                OnPropertyChanged("LeftPosition");
            }
        }
        
        // Gets or sets the Position of the right hand.
       
        public Point RightPosition
        {
            get { return _rightPosition; }
            set
            {
                if (_rightPosition == value)
                    return;
                _rightPosition = value;
                OnPropertyChanged("RightPosition");
            }
        }

        
        // Invokes setting the hand's position if skeleton is not null
        
        public void UpdateHandCursor(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            if (skeleton == null) return;

            TrackHand(skeleton.Joints[JointType.HandLeft], skeleton.Joints[JointType.HandRight], sensor, width, height);
        }
       
        // Mapps left and right hand cooridinates to the proper space
        
        private void TrackHand(Joint leftHand, Joint rightHand, KinectSensor sensor, double width, double height)
        {
            if (leftHand.TrackingState == JointTrackingState.NotTracked && rightHand.TrackingState == JointTrackingState.NotTracked)
                return;

            DepthImagePoint leftPoint = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(leftHand.Position
                , sensor.DepthStream.Format);
            int lx = (int)((leftPoint.X * width / sensor.DepthStream.FrameWidth));
            int ly = (int)((leftPoint.Y * height / sensor.DepthStream.FrameHeight));

            DepthImagePoint rightPoint = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(rightHand.Position
                , sensor.DepthStream.Format);
            int rx = (int)((rightPoint.X * width / sensor.DepthStream.FrameWidth));
            int ry = (int)((rightPoint.Y * height / sensor.DepthStream.FrameHeight));

            LeftPosition = new Point(lx, ly);
            RightPosition = new Point(rx, ry);
        }

    }
}

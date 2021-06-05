using System;
using System.Windows;
using System.Windows.Media.Media3D;
using Virtual_Try_On_System.View_Model;
using Microsoft.Kinect;


namespace Virtual_Try_On_System.Model.ClothingItems
{
      public abstract class ClothingItemBase : ViewModelBase
    {

       
        // Tolerance of the width
      
        protected double Tolerance;


      
        // The height scale
     
        private double _heightScale;

        // The width scale
     
        private double _widthScale;
        
        // The height model scale. Determined by the measurement of model.
     
        private double _heightModelScale;
      
        /// The width model scale. Determined by the measurement of model.
     
        private double _widthModelScale;
      
        // The clothing model
       
        private Model3DGroup _model;
  
        // The basic bounds of the model
       
        private Rect3D _basicBounds;

      
        // Gets or sets the rotation angle.
       
      
       
        public double Angle { get; set; }
       
        // Gets the matrix for scale transform.
        
        
      
        public Transform3D ScaleTransformation { get; protected set; }
        
        // Gets or sets the model.
        
     
        
        public Model3DGroup Model
        {
            get { return _model; }
            set
            {
                if (_model == value)
                    return;
                _model = value;
                OnPropertyChanged("Model");
            }
        }
        
        // Gets or sets the height scale.
       
       
       
        public double HeightScale
        {
            get { return _heightScale; }
            set
            {
                if (_heightScale == value)
                    return;
                _heightScale = value;
                SetScaleTransformation();
            }
        }
       
        // Gets or sets the width scale.
       
       
        public double WidthScale
        {
            get { return _widthScale; }
            set
            {
                if (_widthScale == value)
                    return;
                _widthScale = value;
                SetScaleTransformation();
            }
        }
       
        // Gets or sets the value to move the model in Y coordinate.
       
        
      
        public double DeltaPosition { get; set; }
       
        // Gets or sets the joint to track position.
        
        
        public JointType JointToTrackPosition { get; protected set; }
        
        // Gets or sets the left joint to track angle.
        
        public JointType LeftJointToTrackAngle { get; protected set; }
        
        // Gets or sets the right joint to track angle.
        
        public JointType RightJointToTrackAngle { get; protected set; }
        
        // Gets or sets the left joint to track scale.
       
        public JointType LeftJointToTrackScale { get; protected set; }

        // Gets or sets the right joint to track scale.
        
        public JointType RightJointToTrackScale { get; protected set; }



        // Protected Constructor of CLothingItemBase class
        
        protected ClothingItemBase(Model3DGroup model, double tolerance, double deltaPosition)
        {
            Model = model;
            _basicBounds = model.Bounds;
            Tolerance = tolerance;
            _widthScale = _heightScale = 1;
            DeltaPosition = deltaPosition;
        }


        
        // Invokes setting the item's position
        
        public void UpdateItemPosition(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            if (skeleton == null || skeleton.Joints[LeftJointToTrackAngle].TrackingState == JointTrackingState.NotTracked
                || skeleton.Joints[RightJointToTrackAngle].TrackingState == JointTrackingState.NotTracked
                || skeleton.Joints[JointToTrackPosition].TrackingState == JointTrackingState.NotTracked)
                return;

            TrackSkeletonParts(skeleton, sensor, width, height);
        }

       
        // Set position for part of set
       
        private void TrackSkeletonParts(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            Angle = TrackJointsRotation(sensor, skeleton.Joints[LeftJointToTrackAngle], skeleton.Joints[RightJointToTrackAngle]);

            var joint = KinectService.GetJointPoint(skeleton.Joints[JointToTrackPosition], sensor, width, height);
            var point3D = Point2DtoPoint3D(new Point(joint.X, joint.Y * DeltaPosition));

            FitModelToBody(skeleton.Joints[LeftJointToTrackScale], skeleton.Joints[RightJointToTrackScale], sensor, width, height);

            var transform = new Transform3DGroup();
            transform.Children.Add(ScaleTransformation);
            transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), Angle)));
            transform.Children.Add(new TranslateTransform3D(point3D.X, point3D.Y, point3D.Z));
            Model.Transform = transform;
        }
       
        // Maps the Point in 2D to point in 3D.
       
        private Point3D Point2DtoPoint3D(Point point2D)
        {
            Point3D point = new Point3D(point2D.X, point2D.Y, 0);
            Matrix3D matxViewport = ClothingManager.Instance.ViewportTransform;
            Matrix3D matxCamera = ClothingManager.Instance.CameraTransform;

            try
            {
                matxViewport.Invert();
                matxCamera.Invert();
            }
            catch (Exception)
            {
                return new Point3D(0, 0, 0);
            }

            Point3D pointNormalized = matxViewport.Transform(point);
            pointNormalized.Z = 0.01;
            Point3D pointNear = matxCamera.Transform(pointNormalized);
            pointNormalized.Z = 0.99;
            Point3D pointFar = matxCamera.Transform(pointNormalized);

            double factor = (0 - pointNear.Z) / (pointFar.Z - pointNear.Z);
            double x = pointNear.X + factor * (pointFar.X - pointNear.X);
            double y = pointNear.Y + factor * (pointFar.Y - pointNear.Y);
            return new Point3D(x, y, 0);
        }
       
        // Sets the base transformation.
       
        private void SetScaleTransformation()
        {
            Transform3DGroup transform = new Transform3DGroup();
            transform.Children.Add(new ScaleTransform3D(_widthScale * _widthModelScale, 1, _widthScale * _widthModelScale));
            transform.Children.Add(new ScaleTransform3D(1, _heightScale * _heightModelScale, 1));
            ScaleTransformation = transform;
        }
       
        // Maps the Point in 3D to point in 2D.
       
        private Point Point3DtoPoint2D(Point3D point)
        {
            Matrix3D matrix = ClothingManager.Instance.CameraTransform;
            matrix.Append(ClothingManager.Instance.ViewportTransform);

            Point3D pointTransformed = matrix.Transform(point);
            return new Point(pointTransformed.X, pointTransformed.Y);
        }
       
        // Tracks the rotation angle between two joints.
      
        private double TrackJointsRotation(KinectSensor sensor, Joint leftJoint, Joint rightJoint)
        {
            if (leftJoint.TrackingState == JointTrackingState.NotTracked
               || rightJoint.TrackingState == JointTrackingState.NotTracked)
                return double.NaN;

            var jointLeftPosition = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(leftJoint.Position, sensor.DepthStream.Format);
            var jointRightPosition = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(rightJoint.Position, sensor.DepthStream.Format);

            return -(Math.Atan(((double)jointLeftPosition.Depth - jointRightPosition.Depth)
                / ((double)jointRightPosition.X - jointLeftPosition.X)) * 180.0 / Math.PI);
        }
      
        // Fit width of model to width of body
       
        private void FitModelToBody(Joint joint1, Joint joint2, KinectSensor sensor, double width, double height)
        {
            if (joint1.TrackingState == JointTrackingState.NotTracked
                || joint2.TrackingState == JointTrackingState.NotTracked)
                return;

            var joint1Position = KinectService.GetJointPoint(joint1, sensor, width, height);
            var joint2Position = KinectService.GetJointPoint(joint2, sensor, width, height);

            var location = _basicBounds.Location;
            Point leftBound = Point3DtoPoint2D(location);
            Point rightBound =
                Point3DtoPoint2D(new Point3D(location.X + _basicBounds.SizeX, location.Y + _basicBounds.SizeY
                    , location.Z + _basicBounds.SizeZ));

            double ratio = (Math.Abs(joint1Position.Y - joint2Position.Y) / Math.Abs(leftBound.Y - rightBound.Y));
            _widthModelScale = _heightModelScale = ratio * Tolerance;
            SetScaleTransformation();
        }


        public enum ClothingType
        {
            TrouserItem,
            ShirtItem
        }


    }
}

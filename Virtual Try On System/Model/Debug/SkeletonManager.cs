
using Virtual_Try_On_System.View_Model;
using Microsoft.Kinect;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
namespace Virtual_Try_On_System.Model.Debug
{
    public class SkeletonManager : ViewModelBase
    {

       
        // Gets or sets the skeleton parts.
        
        public ObservableCollection<Polyline> SkeletonParts
        {
            get { return _skeletonModels; }
            set
            {
                if (_skeletonModels == value)
                    return;
                if (value.Count > 0)
                {
                    _skeletonModels = value;
                    OnPropertyChanged("SkeletonParts");
                }
            }
        }


        
        // Models of all of the skeletons
    
        private ObservableCollection<Polyline> _skeletonModels;


       
        // Creates skeleton models for each of the tracked skeleton in the array
      
        public void DrawSkeleton(Skeleton[] skeletons, Brush brush, KinectSensor sensor, double width, double height)
        {
            var skeletonModels = new ObservableCollection<Polyline>();
            foreach (var skeleton in skeletons.Where(skeleton => skeleton.TrackingState != SkeletonTrackingState.NotTracked))
            {
                skeletonModels.Add(CreateFigure(skeleton, brush, CreateBody(), sensor, width, height));
                skeletonModels.Add(CreateFigure(skeleton, brush, CreateLeftHand(), sensor, width, height));
                skeletonModels.Add(CreateFigure(skeleton, brush, CreateRightHand(), sensor, width, height));
                skeletonModels.Add(CreateFigure(skeleton, brush, CreateLeftLeg(), sensor, width, height));
                skeletonModels.Add(CreateFigure(skeleton, brush, CreateRightLeg(), sensor, width, height));
            }
            SkeletonParts = skeletonModels;
        }


       
        // Creates a body for skeleton
      
       
        private IEnumerable<JointType> CreateBody()
        {
            return new[]
                        {
                            JointType.Head
                            , JointType.ShoulderCenter
                            , JointType.ShoulderLeft
                            , JointType.Spine
                            , JointType.ShoulderRight
                            , JointType.ShoulderCenter
                            , JointType.HipCenter
                            , JointType.HipLeft
                            , JointType.Spine
                            , JointType.HipRight
                            , JointType.HipCenter
                        };
        }
        
        // Creates a right hand for skeleton
       
        private IEnumerable<JointType> CreateRightHand()
        {
            return new[]
                        {
                            JointType.ShoulderRight
                            , JointType.ElbowRight
                            , JointType.WristRight
                            , JointType.HandRight
                        };
        }
        
        // Creates a left hand for skeleton
      
        private IEnumerable<JointType> CreateLeftHand()
        {
            return new[]
                        {
                            JointType.ShoulderLeft
                            , JointType.ElbowLeft
                            , JointType.WristLeft
                            , JointType.HandLeft
                        };
        }
       
        // Creates a right leg for skeleton
       
        private IEnumerable<JointType> CreateRightLeg()
        {
            return new[]
                        {
                            JointType.HipRight
                            , JointType.KneeRight
                            , JointType.AnkleRight
                            , JointType.FootRight
                        };
        }
       
        // Creates a left leg for skeleton
        
        private IEnumerable<JointType> CreateLeftLeg()
        {
            return new[]
                        {
                            JointType.HipLeft
                            , JointType.KneeLeft
                            , JointType.AnkleLeft
                            , JointType.FootLeft
                        };
        }
       
        // Creates the skeleton model.
      
        private Polyline CreateFigure(Skeleton skeleton, Brush brush, IEnumerable<JointType> joints
            , KinectSensor sensor, double width, double height)
        {
            var figure = new Polyline { StrokeThickness = 8, Stroke = brush };

            foreach (var joint in joints)
            {
                var jointPoint = KinectService.GetJointPoint(skeleton.Joints[joint], sensor, width, height);
                figure.Points.Add(new Point(jointPoint.X, jointPoint.Y));
            }

            return figure;
        }

    
    }
}
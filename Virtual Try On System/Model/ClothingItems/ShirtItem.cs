using System.Windows.Media.Media3D;
using Microsoft.Kinect;


namespace Virtual_Try_On_System.Model.ClothingItems
{
    class ShirtItem : ClothingItemBase
    {
 
        
        // Constructor of Shirt object
       
        public ShirtItem(Model3DGroup model, JointType bottomJoint, double ratio, double deltaY)
            : base(model, ratio, deltaY)
        {
            JointToTrackPosition = JointType.HipCenter;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
            LeftJointToTrackScale = JointType.ShoulderCenter;
            RightJointToTrackScale = bottomJoint;
        }

    }
}

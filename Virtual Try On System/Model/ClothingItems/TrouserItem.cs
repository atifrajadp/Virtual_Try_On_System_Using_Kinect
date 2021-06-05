using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace Virtual_Try_On_System.Model.ClothingItems
{
    class TrouserItem : ClothingItemBase
    {

       
        // Constructor of Trouser object
       
        public TrouserItem(Model3DGroup model, JointType bottomJoint, double ratio, double deltaY)
            : base(model, ratio, deltaY)
        {
            JointToTrackPosition = JointType.HipCenter;
            LeftJointToTrackAngle = JointType.KneeLeft;
            RightJointToTrackAngle = JointType.KneeRight;
            LeftJointToTrackScale = JointType.AnkleLeft;
            RightJointToTrackScale = bottomJoint;
        }

    }
}

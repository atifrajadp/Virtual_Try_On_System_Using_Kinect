using System.Windows;
using Virtual_Try_On_System.View_Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Virtual_Try_On_System_UnitTests
{

    [TestClass]
    public class PointMapping
    {

        public void GetDistanceBetweenJoinPoints()
        {
            var leftJoint = new Point(0, 0);
            var rightJoint = new Point(SystemParameters.PrimaryScreenWidth, 0);

            var distanceFull = KinectService.CalculateDistanceBetweenJoints(rightJoint, leftJoint);
            var distanceFull2 = KinectService.CalculateDistanceBetweenJoints(rightJoint, leftJoint);
            var distanceZero = KinectService.CalculateDistanceBetweenJoints(leftJoint, leftJoint);

            Assert.AreNotEqual(new Point(0, 0), distanceFull);
            Assert.AreNotEqual(new Point(0, 0), distanceFull2);
            Assert.AreEqual(distanceFull2, distanceFull);
            Assert.AreEqual(new Point(0, 0), distanceZero);
        }
    }
}

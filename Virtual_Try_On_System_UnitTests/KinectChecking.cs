using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Virtual_Try_On_System.View_Model;
using Microsoft.Kinect;

namespace Virtual_Try_On_System_UnitTests
{

    [TestClass]
    public class KinectChecking
    {

        [TestMethod]
        public void CheckNoKinectExceptionHandlingText()
        {
            KinectService s = new KinectService();
            s.Initialize();
            string message = "Please connect Kinect";
            Assert.AreEqual(message, s.ErrorGridMessage);
        }
        [TestMethod]
        public void CheckNoSkeleton()
        {
            Skeleton[] skeletons = new Microsoft.Kinect.Skeleton[2];
            skeletons[0] = new Skeleton();
            skeletons[1] = new Skeleton();
            Skeleton skeleton = KinectService.GetPrimarySkeleton(skeletons);
            Assert.IsNull(skeleton);
  
        }
    }
}


using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bowling.Game;

namespace BowlingTests
{
    [TestClass]
    public class FrameTests
    {
        [TestMethod]
        public void SpareTest()
        {
            Frame frame = new Frame(1, "9,1");
            Assert.IsTrue(frame.Valid);
            Assert.IsTrue(frame.RollOne.Points < 10);
            Assert.IsTrue(frame.RollOne.Points + frame.RollTwo.Points == 10);
        }

        [TestMethod]
        public void SpareTestWithFoul()
        {
            // spare or strike?
            Frame frame = new Frame(1, "0F,10");
            Assert.IsTrue(frame.Valid);
            Assert.IsTrue(frame.RollOne.Points == 0);
            Assert.IsTrue(frame.RollOne.Points + frame.RollTwo.Points == 10);
        }

        [TestMethod]
        public void StrikeTest()
        {
            Frame frame = new Frame(1, "10X,#");
            Assert.IsTrue(frame.Valid);
            Assert.IsTrue(frame.RollOne.Points == 10);
            Assert.IsTrue((frame.RollTwo?.Points ?? -1) == -1);
        }

        [TestMethod]
        public void SuperFrameStrikeTest()
        {
            SuperFrame frame = new SuperFrame(10, "10X,#,10,10");
            Assert.IsTrue(frame.Valid);
            Assert.IsTrue(frame.FrameNumber == 10);
            Assert.IsTrue(frame.RollOne.Points == 10);
            Assert.IsTrue((frame.RollTwo?.Points ?? -1) == -1);
            Assert.IsTrue(frame.RollExtraOne.Points == 10);
            Assert.IsTrue(frame.RollExtraTwo.Points == 10);
        }

        [TestMethod]
        public void SuperFrameSpareTest()
        {
            SuperFrame frame = new SuperFrame(10, "7,3,10,#");
            Assert.IsTrue(frame.Valid);
            Assert.IsTrue(frame.FrameNumber == 10);
            Assert.IsTrue(frame.RollOne.Points < 10); // a spare
            Assert.IsTrue(frame.RollOne.Points + frame.RollTwo.Points == 10); // a spare
            Assert.IsTrue(frame.RollExtraOne.Points == 10);
            Assert.IsTrue((frame.RollExtraTwo?.Points ?? -1) == -1);
        }

        [TestMethod]
        public void ValidFrameNumber()
        {
            Frame frame = new Frame(1, "9,1");
            Assert.IsTrue(frame.Valid);
            Assert.IsTrue(frame.FrameNumber >= 1 && frame.FrameNumber <= 10);
        }

        [TestMethod]
        public void InvalidFrameNumber()
        {
            Frame frame = new Frame(11, "9,1");
            Assert.IsFalse(frame.Valid);
            Assert.IsFalse(frame.FrameNumber >= 1 && frame.FrameNumber <= 10);
        }

        [TestMethod]
        public void ValidFramePoints()
        {
            Frame frame = new Frame(1, "2,3");
            Assert.IsTrue(frame.Valid);
            Assert.IsTrue(frame.RollOne.Points + frame.RollTwo.Points <= 10);
        }

        [TestMethod]
        public void InvalidFramePoints()
        {
            Frame frame = new Frame(1, "9,9");
            Assert.IsFalse(frame.Valid);
            Assert.IsFalse(frame.RollOne.Points + frame.RollTwo.Points <= 10);
        }

        [TestMethod]
        public void MinFramePoints()
        {
            Frame frame = new Frame(1, "0,0");
            Assert.IsTrue(frame.Valid);
            Assert.IsTrue(frame.RollOne.Points + frame.RollTwo.Points == 0);
        }

        [TestMethod]
        public void MaxFramePoints()
        {
            Frame frame = new Frame(1, "10,0");
            Assert.IsTrue(frame.Valid);
            Assert.IsTrue(frame.RollOne.Points + frame.RollTwo.Points == 10);
        }
    }
}

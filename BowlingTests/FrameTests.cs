using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bowling.Game;

namespace BowlingTests
{
    [TestClass]
	public class FrameTests
    {
        [TestMethod]
		public void StrikeTest()
		{
			Frame frame = new Frame(1, "10X,#");
			Assert.IsTrue(frame.Valid);
			Assert.IsTrue(frame.FrameNumber == 1);
			Assert.IsTrue(frame.RollOne.Valid);
			Assert.IsTrue(frame.RollTwo.Valid);
			Assert.IsTrue(frame.RollOne.Points == 10);
			Assert.IsTrue((frame.RollTwo?.Points ?? -1) == -1);
		}
		[TestMethod]
		public void SuperFrameStrikeTest()
		{
			SuperFrame frame = new SuperFrame(10, "10X,#,10,10");
			Assert.IsTrue(frame.Valid);
			Assert.IsTrue(frame.FrameNumber == 10);
			Assert.IsTrue(frame.RollOne.Valid);
			Assert.IsTrue(frame.RollTwo.Valid);
			Assert.IsTrue(frame.RollExtraOne.Valid);
			Assert.IsTrue(frame.RollExtraTwo.Valid);
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
			Assert.IsTrue(frame.RollOne.Valid);
			Assert.IsTrue(frame.RollTwo.Valid);
			Assert.IsTrue(frame.RollOne.Points < 10); // a spare
			Assert.IsTrue(frame.RollOne.Points + frame.RollTwo.Points == 10); // a spare
			Assert.IsTrue(frame.RollExtraOne.Valid);
			Assert.IsTrue(frame.RollExtraTwo.Valid);
			Assert.IsTrue(frame.RollExtraOne.Points == 10);
			Assert.IsTrue((frame.RollExtraTwo?.Points ?? -1) == -1);
		}
	}
}

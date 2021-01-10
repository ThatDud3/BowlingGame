using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bowling.Game;

namespace BowlingTests
{
    [TestClass]
    public class RollTests
    {
        [TestMethod]
        public void StrikeTest()
        {
            Roll roll = new Roll(1, "10X");
            Assert.IsTrue(roll.Valid);
            Assert.IsTrue(roll.RollNumber == 1);
            Assert.IsTrue(roll.Points == 10);
        }

        [TestMethod]
        public void SpareTest()
        {
            Roll roll = new Roll(2, "6/");
            Assert.IsTrue(roll.Valid);
            Assert.IsTrue(roll.RollNumber == 2);
            Assert.IsTrue(roll.StatusSymbol == '/');
        }

        [TestMethod]
        public void ValidRollNumber()
        {
            Roll roll = new Roll(2, "1");
            Assert.IsTrue(roll.Valid);
            Assert.IsTrue(roll.RollNumber >= 1 && roll.RollNumber <= 2);
        }

        [TestMethod]
        public void InvalidRollNumber()
        {
            Roll roll = new Roll(11, "1");
            Assert.IsFalse(roll.Valid);
            Assert.IsFalse(roll.RollNumber >= 1 && roll.RollNumber <= 2);
        }

        [TestMethod]
        public void ValidRollPoints()
        {
            Roll roll = new Roll(1, "1");
            Assert.IsTrue(roll.Valid);
            Assert.IsTrue(roll.Points >= 0 && roll.Points <= 10);
        }

        [TestMethod]
        public void InvalidRollPoints()
        {
            Roll roll = new Roll(1, "11");
            Assert.IsFalse(roll.Valid);
            Assert.IsFalse(roll.Points >= 0 && roll.Points <= 10);
        }

        [TestMethod]
        public void MinRollPoints()
        {
            Roll roll = new Roll(1, "0");
            Assert.IsTrue(roll.Valid);
            Assert.IsTrue(roll.Points == 0);
        }

        [TestMethod]
        public void MaxRollPoints()
        {
            Roll roll = new Roll(1, "10");
            Assert.IsTrue(roll.Valid);
            Assert.IsTrue(roll.Points == 10);
        }
    }
}

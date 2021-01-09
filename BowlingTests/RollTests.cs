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
    }
}

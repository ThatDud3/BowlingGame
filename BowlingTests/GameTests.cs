using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bowling.Game;

namespace BowlingTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void ValidGame()
        {
            Game game = new Game(1, "10,#; 9,1; 5,5; 7,2; 10,#; 10,#; 10,#; 9,0; 8,2; 9,1,10");
            Assert.IsTrue(game.Valid);
            Assert.IsTrue(game.Score == 187);
        }

        [TestMethod]
        public void InvalidGame()
        {
            Game game = new Game(1, "10,9; 9,1; 5,5; 7,2; 10,#; 10,#; 10,#; 9,0; 8,2; 9,1,10");
            Assert.IsFalse(game.Valid);
        }

        [TestMethod]
        public void MinScoreGame()
        {
            Game game = new Game(1, "0,0; 0,0; 0,0; 0,0; 0,0; 0,0; 0,0; 0,0; 0,0; 0,0");
            Assert.IsTrue(game.Valid);
            Assert.IsTrue(game.Score == 0);
        }

        [TestMethod]
        public void MaxScoreGame()
        {
            Game game = new Game(1, "10,#; 10,#; 10,#; 10,#; 10,#; 10,#; 10,#; 10,#; 10,#; 10,#,10,10");
            Assert.IsTrue(game.Valid);
            Assert.IsTrue(game.Score == 300);
        }

        [TestMethod]
        public void GameScore187()
        {
            Game game = new Game(1, "10,#; 9,1; 5,5; 7,2; 10,#; 10,#; 10,#; 9,0; 8,2; 9,1,10");
            Assert.IsTrue(game.Valid);
            Assert.IsTrue(game.Score == 187);
        }
    }
}

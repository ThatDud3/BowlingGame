using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bowling.Game;
using Bowling.CustomRules;

namespace BowlingTests
{
    [TestClass]
    public class CustomRulesTests
    {
        [TestMethod]
        public void CustomRuleMatchFrameNumberTest()
        {
            CustomRulesProcessor customRules = new CustomRulesProcessor();
            customRules.AddCutomRule(new CustomRuleMatchFrameNumber());
            IGameScore gameScore = new Game(1, "10,#; 10,#; 10,#; 10,#; 10,#; 10,#; 10,#; 10,#; 10,#; 10,#,10,10", customRules);
            Assert.IsTrue(gameScore.Score == 310);
        }

        [TestMethod]
        public void CustomRuleMatchRollsTest()
        {
            CustomRulesProcessor customRules = new CustomRulesProcessor();
            customRules.AddCutomRule(new CustomRuleMatchRolls());
            IGameScore gameScore = new Game(1, "10,#; 9,1; 5,5; 7,2; 10,#; 10,#; 10,#; 9,0; 8,2; 9,1,10", customRules);
            Assert.IsTrue(gameScore.Score == 197);
        }
    }
}

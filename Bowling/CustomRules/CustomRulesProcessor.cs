using System.Collections.Generic;

namespace Bowling.CustomRules
{
    public class CustomRulesProcessor
    {
        private readonly List<ICustomRule> _customRules = new List<ICustomRule>();

        public void AddCutomRule(ICustomRule customRule)
        {
            if (customRule != null)
            {
                _customRules.Add(customRule);
            }
        }

        public void ApplyCustomRules(Game.Game game)
        {
            foreach (ICustomRule customRule in _customRules)
            {
                customRule.ApplyCustomRule(game);
            }
        }
    }
}

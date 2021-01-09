using System;
using System.IO;
using Bowling.CustomRules;

namespace Bowling.Game
{
    public class GameManager
    {
        public void Load(string path)
        {
            if (File.Exists(path))
            {
                CustomRulesProcessor customRules = new CustomRulesProcessor();
                customRules.AddCutomRule(new CustomRuleMatchFrameNumber());
                customRules.AddCutomRule(new CustomRuleMatchRolls());

                int lineNum = 0;
                foreach (string line in File.ReadLines(path))
                {
                    if (!string.IsNullOrWhiteSpace(line) && !line.Trim().StartsWith('#'))
                    {
                        customRules = null; // comment out to see custom rules in action
                        IGameScore gameScore = new Game(++lineNum, line, customRules);
                        Console.WriteLine($"Score: {gameScore.Score} ::: Running Score: {gameScore.RunningScore}");
                    }
                    else
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            else
            {
                Console.WriteLine($"File not found: {path}");
            }
        }
    }
}

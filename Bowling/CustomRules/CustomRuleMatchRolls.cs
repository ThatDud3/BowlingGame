using Bowling.Game;

namespace Bowling.CustomRules
{
    /// <summary>
    /// When first roll is not a foul and second roll is not a foul
    /// and first roll has same points as second roll
    /// add the sum (1st + 2nd) points to the frame score
    /// </summary>
    public class CustomRuleMatchRolls : ICustomRule
    {
        public void ApplyCustomRule(object data)
        {
            if (data is Game.Game game)
            {
                for (int i = 0; i < game.Frames.Count; i++)
                {
                    Frame frame = game.Frames[i];
                    if ((frame.RollOne?.Valid ?? false)
                        && frame.RollOne.Status != RollType.Foul
                        && frame.RollOne.Points.HasValue
                        && frame.RollOne.Points.Value > 0
                        && (frame.RollTwo?.Valid ?? false)
                        && frame.RollTwo.Status != RollType.Foul
                        && frame.RollTwo.Points.HasValue
                        && frame.RollTwo.Points.Value == frame.RollOne.Points.Value)
                    {
                        int bonusPoints = frame.RollOne.Points.Value + frame.RollTwo.Points.Value;
                        game.Score += bonusPoints;
                        frame.FrameScore += bonusPoints;
                        frame.RunningScore += bonusPoints;

                        // adjust remaining frames' running scores
                        for (int j = i + 1; j < game.Frames.Count; j++)
                        {
                            game.Frames[j].RunningScore += bonusPoints;
                        }
                    }
                }
            }
        }
    }
}

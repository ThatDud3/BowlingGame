using Bowling.Game;

namespace Bowling.CustomRules
{
    /// <summary>
    /// When first roll is not a foul and points match the frame number
    /// add 'frame number' points to the frame score
    /// </summary>
    public class CustomRuleMatchFrameNumber : ICustomRule
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
                        && frame.RollOne.Points.Value == frame.FrameNumber)
                    {
                        int bonusPoints = frame.FrameNumber;
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

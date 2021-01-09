using System.Collections.Generic;

namespace Bowling.Game
{
    public interface IGameScore
    {
        int Score { get; }
        string RunningScore { get; }
        event ScoreAdjustedHandler ScoreAdjusted;
    }
}

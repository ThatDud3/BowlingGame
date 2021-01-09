using System;
using System.Collections.Generic;

namespace Bowling.Game
{
    public class Game : IGameScore
    {
        public int GameNumber { get; init; }
        public string Text { get; init; }

        private int m_score = 0;
        public event ScoreAdjustedHandler ScoreAdjusted;

        public List<Frame> Frames = new List<Frame>();

        public Game(int gameNumber, string text)
        {
            GameNumber = gameNumber;
            Text = text;
            Reset();
            ScoreAdjusted += TargetScoreAdjusted;
            LoadText(text);
        }
        protected void Reset()
        {
            m_score = 0;
        }
        public int Score
        {
            get { return m_score; }
            set
            {
                if (m_score != value)
                {
                    int previousScore = m_score;
                    m_score = value;

                    OnScoreAdjusted(previousScore, m_score);
                }
            }
        }
        public string RunningScore
        {
            get { return string.Join(", ", Frames.ConvertAll<string>(x => x.RunningScore.ToString())); }
        }
        private void OnScoreAdjusted(int score, int points)
        {
            if (ScoreAdjusted != null)
            {
                ScoreAdjusted(score, points);
            }
        }

        private void TargetScoreAdjusted(int score, int points)
        {
            Console.WriteLine($"Score updated from {score} to {score + points}");
        }

        private void LoadText(string text)
        {
            Frames.Clear();
            if (!string.IsNullOrWhiteSpace(text))
            {
                string[] frames = text.Split(';');
                for(int i = 0; i < frames.Length; i++)
                {
                    int frameNumber = i + 1;
                    string frameText = frames[i];

                    Frame frame = (frameNumber == 10 ? new SuperFrame(frameNumber, frameText) : new Frame(frameNumber, frameText));
                    Frames.Add(frame);
                }
            }
            CalculateScore();
        }
        private void CalculateScore()
        {
            int runningScore = 0;
            int extraRollOnePoints = 0;
            int extraRollTwoPoints = 0;
            if (Frames[9].FrameNumber == 10 && Frames[9] is SuperFrame sf)
            {
                if ((sf.RollExtraOne?.Valid ?? false) && sf.RollExtraOne.Status != RollType.Foul && sf.RollExtraOne.Points.HasValue)
                    extraRollOnePoints = sf.RollExtraOne.Points.Value;
                if ((sf.RollExtraTwo?.Valid ?? false) && sf.RollExtraTwo.Status != RollType.Foul && sf.RollExtraTwo.Points.HasValue)
                    extraRollTwoPoints = sf.RollExtraTwo.Points.Value;
            }

            for (int i = 0; i < Frames.Count; i++)
            {
                Frame f = Frames[i];
                int score = 0;
                if (f.RollOne.Valid && f.RollOne.Status != RollType.Foul && f.RollOne.Points.HasValue)
                    score += f.RollOne.Points.Value;
                bool isStrike = (score == 10);

                if (f.RollTwo.Valid && f.RollTwo.Status != RollType.Foul && f.RollTwo.Points.HasValue)
                    score += f.RollTwo.Points.Value;
                bool isSpare = (!isStrike && score == 10);

                if (f.FrameNumber < 10)
                {
                    int[] ptsNext = { -1, -1 };
                    if ((isSpare || isStrike) && (i + 1) < Frames.Count)
                    {
                        Frame f1 = Frames[i + 1];
                        if (f1.RollOne.Valid && f1.RollOne.Points.HasValue)
                            ptsNext[0] = (f1.RollOne.Status == RollType.Foul ? 0 : f1.RollOne.Points.Value);
                        // if NOT a strike - both values come from Frames[i+1], otherwise check Frames[i+2]
                        if (ptsNext[0] != 10 && f1.RollTwo.Valid && f1.RollTwo.Points.HasValue)
                            ptsNext[1] = (f1.RollTwo.Status == RollType.Foul ? 0 : f1.RollTwo.Points.Value);
                        // in case of 3x strike at frame#9 -> values come from frame#10, frame#10 first extra roll
                        if (ptsNext[1] < 0 && f1.FrameNumber == 10)
                            ptsNext[1] = extraRollOnePoints;
                    }
                    // in case of 3x strike at frame#8 or before -> 2x values come from frame+1, frame+2
                    if (isStrike && ptsNext[1] < 0 && (i + 2) < Frames.Count)
                    {
                        Frame f2 = Frames[i + 2];
                        if (f2.RollOne.Valid && f2.RollOne.Points.HasValue)
                            ptsNext[1] = (f2.RollOne.Status == RollType.Foul ? 0 : f2.RollOne.Points.Value);
                    }
                    score += (ptsNext[0] > 0 && (isSpare || isStrike) ? ptsNext[0] : 0);
                    score += (ptsNext[1] > 0 && isStrike ? ptsNext[1] : 0);
                }
                else if (f.FrameNumber == 10)
                {
                    if (isSpare)
                        score += extraRollOnePoints;
                    else if (isStrike)
                        score += extraRollOnePoints + extraRollTwoPoints;
                }
                runningScore += score;
                f.FrameScore = score;
                f.RunningScore = runningScore;
            }
            Score = runningScore;
        }

        public bool Valid { get { return IsValid(); } }
        public bool IsValid()
        {
            Frame badFrame = Frames.Find(x => !x.Valid);
            bool isValid = (Frames.Count == 10 && badFrame == null);
            return isValid;
        }
        public override string ToString() =>
            $"Game# {GameNumber} - " + (Valid ? "Valid" : "INVALID") + $"! Text: {Text} " +
            $"Final Score: {Score} " +
            $"Running Scores: {RunningScore}";
    }
}

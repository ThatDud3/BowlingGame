namespace Bowling.Game
{
    public class Frame
    {
        public int FrameNumber { get; init; }
        public string Text { get; init; }
        public Roll RollOne { get; init; }
        public Roll RollTwo { get; init; }
        public int FrameScore { get; set; }
        public int RunningScore { get; set; }

        public bool Valid { get { return IsValid(); } }
        public virtual bool IsValid()
        {
            bool isValid = (FrameNumber >= 1 && FrameNumber <= 10 && RollOne.Points + (RollTwo.Points ?? 0) <= 10) ? true : false;
            return isValid;
        }

        public Frame(int frameNumber, string text)
        {
            FrameNumber = frameNumber;
            Text = text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                string[] rolls = text.Split(',');
                if (rolls.Length > 1)
                {
                    RollOne = new Roll(1, rolls[0]);
                    RollTwo = new Roll(2, rolls[1]);
                }
            }
        }

        public override string ToString() =>
            $"Frame# {FrameNumber}, Text: {Text}  " +
            $"RollOne: {RollOne} " +
            $"RollTwo: {RollTwo} " +
            $"FrameScore: {FrameScore} " +
            $"RunningScore: {RunningScore}";
    }
}

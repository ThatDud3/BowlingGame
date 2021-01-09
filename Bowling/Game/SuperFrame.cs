using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.Game
{
    /// <summary>
    /// Reperesents a super frame with up to two additinal rolls
    /// When last roll ends with 'X' player gets two additional rolls
    /// When last roll ends with '/' player gets one additional roll
    /// </summary>
    public class SuperFrame : Frame
    {
        public Roll RollExtraOne { get; init; }
        public Roll RollExtraTwo { get; init; }

        public SuperFrame(int frameNumber, string text) : base(frameNumber, text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                string[] rolls = text.Split(',');
                if (rolls.Length > 2)
                {
                    RollExtraOne = new Roll(1, rolls[2]);
                    if (rolls.Length > 3)
                    {
                        RollExtraTwo = new Roll(2, rolls[3]);
                    }
                }
            }
        }
        public override bool IsValid()
        {
            // 0, 1, or 2 extra rolls
            bool isValid = FrameNumber == 10
                && (RollExtraOne == null || RollExtraOne.Valid)
                && (RollExtraTwo == null || RollExtraTwo.Valid)
                && base.IsValid();
            return isValid;
        }

        public override string ToString() =>
            $"Frame# {FrameNumber}, Text: {Text}  " +
            $"RollOne: {RollOne} " +
            $"RollTwo: {RollTwo} " +
            $"RollExtraOne: {RollExtraOne} " +
            $"RollExtraTwo: {RollExtraTwo} " +
            $"FrameScore: {FrameScore} " +
            $"RunningScore: {RunningScore}";
    }
}

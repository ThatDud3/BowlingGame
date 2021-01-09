using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bowling.Game
{
    public enum RollType
    {
        Unknown,
        Strike,
        Spare,
        Miss,
        Foul,
        Split,
        Skipped
    }

    public class Roll
    {
        private static readonly Dictionary<RollType, char> RollSymbols = new Dictionary<RollType, char>
        {
            { RollType.Unknown, 'U' },
            { RollType.Strike, 'X' },
            { RollType.Spare, '/' },
            { RollType.Miss, '-' },
            { RollType.Foul, 'F' },
            { RollType.Split, 'S' },
            { RollType.Skipped, '#' }
        };

        private static readonly Dictionary<RollType, string> RollDescriptions = new Dictionary<RollType, string>
        {
            { RollType.Unknown, "Unknown" },
            { RollType.Strike, "Strike" },
            { RollType.Spare, "Spare" },
            { RollType.Miss, "Miss" },
            { RollType.Foul, "Foul" },
            { RollType.Split, "Split" },
            { RollType.Skipped, "Skipped" }
        };

        public int RollNumber { get; init; }
        public string Text { get; init; }
        public int? Points { get; init; }

        public RollType? Status { get; init; }
        public char StatusSymbol { get { return (Status.HasValue && RollSymbols.TryGetValue(Status.Value, out char symbol)) ? symbol : '?'; } }
        public string StatusDescription
        {
            get
            {
                return (Status.HasValue && RollDescriptions.TryGetValue(Status.Value, out string description))
                    ? description
                    : "Unknown Status";
            }
        }

        public bool Valid { get { return IsValid(); } }
        public bool IsValid()
        {
            bool isValid = ((RollNumber == 1 || RollNumber == 2)
                            && ((RollNumber == 2
                                    && (string.IsNullOrWhiteSpace(Text) || $"{RollSymbols[RollType.Skipped]}".Equals(Text.Trim())))
                                || (!string.IsNullOrWhiteSpace(Text)
                                    && Points.HasValue
                                    && Points.Value >= 0
                                    && Points.Value <= 10
                                    )
                                )
                            );
            return isValid;
        }

        private static readonly Regex rxRollText = new Regex(@"^(?<value>\d\d?)(?<symbol>[X/\-FS])?$", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
        public Roll(int rollNumber, string text)
        {
            RollNumber = rollNumber;
            Text = text;
            Status = RollType.Unknown;
            if (!string.IsNullOrWhiteSpace(text))
            {
                if ($"{RollSymbols[RollType.Skipped]}".Equals(text.Trim()))
                {
                    Status = RollType.Skipped;
                }
                else
                {
                    Match match = rxRollText.Match(text.Trim());
                    if (match.Success)
                    {
                        string value = match.Groups["value"]?.Value ?? string.Empty;
                        if (int.TryParse(value, out int points))
                        {
                            Points = points;
                        }
                        string symbol = match.Groups["symbol"]?.Value ?? string.Empty;
                        symbol = symbol.Trim();
                        if (!string.IsNullOrEmpty(symbol))
                        {
                            Status = RollSymbols.FirstOrDefault(x => x.Value == symbol[0]).Key;
                        }
                    }
                }
            }
        }

        public override string ToString() =>
            $"Roll# {RollNumber}, Text: {Text} " +
            $"Points: {(Points.HasValue ? Points.Value.ToString() : string.Empty)}";
    }
}

using System;
using System.IO;
using Bowling.Game;

namespace Bowling.Game
{
    public class GameManager
    {
        public void Load(string path)
        {
            if (File.Exists(path))
            {
                int lineNum = 0;
                foreach (string line in File.ReadLines(path))
                {
                    if (!string.IsNullOrWhiteSpace(line) && !line.Trim().StartsWith('#'))
                    {
                        IGameScore gameScore = new Game(++lineNum, line);
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

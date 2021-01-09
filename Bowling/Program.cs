using System;
using System.IO;
using System.Reflection;
using Bowling.Game;

/// <summary>
/// Simple Bowling Scorer - Console Application
/// Reads any number of games from text file
/// and prints running scores and final score
/// </summary>
namespace Bowling
{
    class Program
    {
        static void Main(string[] args)
        {
            bool fileExists = false;
            string filePath = string.Empty;
            string executableName = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);

            if (args.Length > 0)
            {
                filePath = args[0];
                bool isValidPath = (filePath.IndexOfAny(Path.GetInvalidPathChars()) == -1);
                if (isValidPath)
                {
                    bool isAbsolutePath = Path.IsPathRooted(filePath);
                    if (isAbsolutePath)
                    {
                        fileExists = File.Exists(filePath);
                    }
                    else
                    {
                        // first check in current dir (per context - OS, shell terminal)
                        fileExists = File.Exists(filePath);
                        if (!fileExists)
                        {
                            // second check in directory where executable is
                            string executableLocation = Assembly.GetExecutingAssembly().Location;
                            string currentDir = Path.GetDirectoryName(executableLocation);
                            filePath = Path.Combine(currentDir, filePath);
                            fileExists = File.Exists(filePath);
                        }
                    }
                }
            }

            if (fileExists)
            {
                Console.WriteLine("Loading games...");
                GameManager gameManager = new GameManager();
                gameManager.Load(filePath);
                Console.WriteLine();
                Console.WriteLine("All Done.");
            }
            else
            {
                Console.WriteLine($"Error: Input file not found!");
                Console.WriteLine($"Usage: {executableName} FilePath");
            }

            // return proper exit code to OS
            int exitCode = (fileExists ? 0 : 1);
            Environment.Exit(exitCode);
        }
    }
}

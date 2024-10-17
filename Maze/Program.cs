using System;
using System.Diagnostics;

namespace Maze
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] maze = new char[10, 10]
            {
                { 'W', 'C', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W' },
                { 'W', ' ', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W' },
                { 'W', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'W' },
                { 'W', ' ', 'W', 'W', 'W', 'W', 'W', 'W', ' ', 'W' },
                { 'W', ' ', 'W', ' ', ' ', ' ', ' ', 'W', ' ', 'W' },
                { 'W', ' ', 'W', ' ', 'W', 'W', ' ', 'W', ' ', 'W' },
                { 'W', ' ', 'W', ' ', 'W', 'K', ' ', 'W', ' ', 'W' },
                { 'W', ' ', 'W', ' ', 'W', 'W', 'W', 'W', ' ', 'W' },
                { 'W', ' ', ' ', ' ', ' ', ' ', ' ', 'D', ' ', 'W' },
                { 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'E', 'W' }
            };

            int playerX = 0, playerY = 1; // Starting position of the player
            bool hasKey = false;
            int score = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                Console.Clear();
                PrintMaze(maze, playerX, playerY);
                Console.WriteLine($"Score: {score}");
                Console.WriteLine($"Time: {stopwatch.Elapsed}");

                if (maze[playerX, playerY] == 'E')
                {
                    stopwatch.Stop();
                    Console.WriteLine("You reached the end!");
                    Console.WriteLine($"Final Score: {score}");
                    Console.WriteLine($"Total Time: {stopwatch.Elapsed}");
                    break;
                }

                if (maze[playerX, playerY] == 'K')
                {
                    hasKey = true;
                    maze[playerX, playerY] = ' '; // Remove the key from the maze
                    maze[8, 7] = ' '; // Remove the door from the maze
                    score += 10;
                    Console.WriteLine("You picked up the key and the door is now open!");
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                int newX = playerX, newY = playerY;

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        newX = playerX - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        newX = playerX + 1;
                        break;
                    case ConsoleKey.LeftArrow:
                        newY = playerY - 1;
                        break;
                    case ConsoleKey.RightArrow:
                        newY = playerY + 1;
                        break;
                }

                if (newX >= 0 && newX < 10 && newY >= 0 && newY < 10)
                {
                    if (maze[newX, newY] == 'W')
                    {
                        score -= 5;
                    }
                    else
                    {
                        playerX = newX;
                        playerY = newY;
                        score += 5;
                    }
                }
            }
        }

        static void PrintMaze(char[,] maze, int playerX, int playerY)
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (i == playerX && j == playerY)
                    {
                        Console.Write('P'); // Player token
                    }
                    else
                    {
                        Console.Write(maze[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}

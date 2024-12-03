using System;
using System.Collections.Generic;
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

            int startX = 0, startY = 1; // Starting position of the player
            int endX = 9, endY = 8; // Ending position

            Random random = new Random();
            bool findKeyFirst = random.Next(2) == 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<(int, int)> path;
            int points = 0;

            if (findKeyFirst)
            {
                Console.WriteLine("AI decided to find the key first.");
                path = FindPath(maze, startX, startY, 6, 5); // Path to the key
                if (path != null)
                {
                    points += path.Count * 5;
                    foreach (var (x, y) in path)
                    {
                        Console.Clear();
                        PrintMaze(maze, x, y);
                        System.Threading.Thread.Sleep(500); // Pause for half a second to visualize the path
                    }
                    Console.WriteLine("AI collected the key!");
                    points += 10; // Collecting the key
                    maze[8, 7] = ' '; // Remove the door from the maze
                    path = FindPath(maze, 6, 5, endX, endY); // Path to the end after collecting the key
                }
            }
            else
            {
                Console.WriteLine("AI decided to go directly to the end.");
                path = FindPath(maze, startX, startY, endX, endY);
            }

            stopwatch.Stop();

            if (path != null)
            {
                points += path.Count * 5;
                foreach (var (x, y) in path)
                {
                    Console.Clear();
                    PrintMaze(maze, x, y);
                    System.Threading.Thread.Sleep(500); // Pause for half a second to visualize the path
                }
                Console.WriteLine("AI reached the end!");
                Console.WriteLine($"Time taken: {stopwatch.Elapsed}");
                Console.WriteLine($"Points: {points}");
            }
            else
            {
                Console.WriteLine("No path found!");
            }
        }

        static List<(int, int)> FindPath(char[,] maze, int startX, int startY, int endX, int endY)
        {
            int rows = maze.GetLength(0);
            int cols = maze.GetLength(1);
            bool[,] visited = new bool[rows, cols];
            Queue<(int, int, List<(int, int)>, bool)> queue = new Queue<(int, int, List<(int, int)>, bool)>();
            queue.Enqueue((startX, startY, new List<(int, int)> { (startX, startY) }, false));

            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            while (queue.Count > 0)
            {
                var (x, y, path, hasKey) = queue.Dequeue();

                if (x == endX && y == endY)
                {
                    return path;
                }

                for (int i = 0; i < 4; i++)
                {
                    int newX = x + dx[i];
                    int newY = y + dy[i];

                    if (newX >= 0 && newX < rows && newY >= 0 && newY < cols && !visited[newX, newY])
                    {
                        if (maze[newX, newY] == 'W' || (maze[newX, newY] == 'D' && !hasKey))
                        {
                            continue;
                        }

                        visited[newX, newY] = true;
                        bool newHasKey = hasKey || maze[newX, newY] == 'K';
                        var newPath = new List<(int, int)>(path) { (newX, newY) };
                        queue.Enqueue((newX, newY, newPath, newHasKey));
                    }
                }
            }

            return null;
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;

namespace Day3
{
    public class Program
    {
        int[,] grid = new int[32768,32768];
        private int gridx = 16384, gridy = 16384;

        private List<int> xcoordinates = new List<int>();
        private List<int> ycoordinates = new List<int>();

        private List<Stepper> stepsTaken = new List<Stepper>();

        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            var lines = File.ReadAllLines("input.txt");

            var count = 1;

            long steps = 0;

            LoopThroughGrid(lines, count, steps);

            for (var x = 0; x < grid.GetLength(0); x++)
            {
                for (var y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] == 3)
                    {
                        //maxDistance = MaxDistance(x, y, maxDistance);
                        Console.WriteLine($"x: {x}, y: {y}");
                        xcoordinates.Add(x);
                        ycoordinates.Add(y);
                    }

                    grid[x, y] = 0;
                }
            }

            count = 1;

            steps = 0;

            LoopThroughGrid(lines, count, steps);

            foreach (var str in stepsTaken)
            {
                Console.WriteLine(str);
            }

            var line1 = stepsTaken.Where(s => s.Line == 1 && s.Steps > 1).OrderBy(s => s.Steps);

            var line2 = stepsTaken.Where(s => s.Line == 2 && s.Steps > 1).OrderBy(s => s.Steps); 

            long minvalues = 5000000;

            foreach (var stepper in line1)
            {
                var lines2 = line2.Where(s => s.X == stepper.X && s.Y == stepper.Y);

                foreach (var line2stepper in lines2)
                {
                    if (stepper.Steps + line2stepper.Steps < minvalues)
                    {
                        minvalues = stepper.Steps + line2stepper.Steps;
                    }
                }
            }

            Console.WriteLine(minvalues);
            
            var maxDistance = 32768;

          
            Console.WriteLine(maxDistance);

            Console.ReadLine();
        }

        private void LoopThroughGrid(string[] lines, int count, long steps)
        {
            foreach (var line in lines)
            {
                var input = line.Split(',');

                gridx = 16384;
                gridy = 16384;

                foreach (var s in input)
                {
                    var reg = new Regex("(\\w)(\\d*)");

                    var match = reg.Match(s);
                    var dir = match.Groups[1].Value;

                    var amount = int.Parse(match.Groups[2].Value);

                    switch (dir)
                    {
                        case "R":
                        {
                            for (var x = gridx; x < gridx + amount; x++)
                            {
                                if (grid[x, gridy] < count)
                                {
                                    if (xcoordinates.Contains(x) && ycoordinates.Contains(gridy))
                                    {
                                        stepsTaken.Add(new Stepper(count, steps, x, gridy));
                                    }

                                    grid[x, gridy] += count;
                                    steps++;
                                }
                            }

                            gridx += amount;
                            break;
                        }
                        case "L":
                        {
                            for (var x = gridx; x > gridx - amount; x--)
                            {
                                if (grid[x, gridy] < count)
                                {
                                    if (xcoordinates.Contains(x) && ycoordinates.Contains(gridy))
                                    {
                                        stepsTaken.Add(new Stepper(count, steps, x, gridy));
                                    }

                                    grid[x, gridy] += count;
                                    steps++;
                                }
                            }

                            gridx -= amount;
                            break;
                        }
                        case "U":
                        {
                            for (var y = gridy; y > gridy - amount; y--)
                            {
                                if (grid[gridx, y] < count)
                                {
                                    if (xcoordinates.Contains(gridx) && ycoordinates.Contains(y))
                                    {
                                        stepsTaken.Add(new Stepper(count, steps, gridx, y));
                                    }

                                    grid[gridx, y] += count;
                                    steps++;
                                }
                            }

                            gridy -= amount;
                            break;
                        }
                        case "D":
                        {
                            for (var y = gridy; y < gridy + amount; y++)
                            {
                                if (grid[gridx, y] < count)
                                {
                                    if (xcoordinates.Contains(gridx) && ycoordinates.Contains(y))
                                    {
                                        stepsTaken.Add(new Stepper(count, steps, gridx, y));
                                    }

                                    grid[gridx, y] += count;
                                    steps++;
                                }
                            }

                            gridy += amount;
                            break;
                        }
                    }
                }

                count++;
                steps = 0;
            }
        }

        private int MaxDistance(int x, int y, int maxDistance)
        {
            var manhattanDistance = ManhattanDistance(16384, x, 16384, y);
            if (manhattanDistance < maxDistance && manhattanDistance != 0)
            {
                Console.WriteLine($"x: {x}, y: {y}");
                maxDistance = manhattanDistance;
            }

            return maxDistance;
        }

        int ManhattanDistance(int x1, int x2, int y1, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }
    }

    public class Stepper
    {
        public readonly int Line;
        public readonly long Steps;
        public readonly int X;
        public readonly int Y;

        public Stepper(int line, long steps, int x, int y)
        {
            Line = line;
            Steps = steps;
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"line {Line} took {Steps} to be add x:{X},  y:{Y}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;

namespace Day3
{
    public class Program
    {
        int[,] grid = new int[32768,32768];
        private int gridx = 16384, gridy = 16384;

        private List<int> xcoordinates = new List<int>
        {
            14550,
            14800,
            15251,
            15268
        };

        private List<int> ycoordinates = new List<int>
        {
            14966,
            15302,
            16225,
            16289
        };

        private List<string> stepsTaken = new List<string>();

        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            var lines = File.ReadAllLines("input.txt");

            var count = 1;

            foreach (var line in lines)
            {
                var input = line.Split(',');

                var steps = 0;

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
                            for(var x = gridx; x < gridx+amount; x++)
                            {
                                if (grid[x, gridy] < count)
                                {
                                    grid[x, gridy] += count;
                                    steps++;
                                    if (xcoordinates.Contains(x) && ycoordinates.Contains(gridy))
                                    {
                                        stepsTaken.Add($"lineL {count} took {steps} to be add x:{x},  y:{gridy}");
                                    }
                                }
                            }
                            gridx = gridx += amount;
                            break;
                        }
                        case "L":
                        {
                            for (var x = gridx; x > gridx-amount; x--)
                            {
                                if (grid[x, gridy] < count)
                                {
                                    grid[x, gridy] += count;
                                    steps++;
                                    if (xcoordinates.Contains(x) && ycoordinates.Contains(gridy))
                                    {
                                        stepsTaken.Add($"lineL {count} took {steps} to be add x:{x},  y:{gridy}");
                                    }
                                }
                            }
                            gridx = gridx -= amount;
                            break;
                        }
                        case "U":
                        {
                            for (var y = gridy; y > gridy - amount; y--)
                            {
                                if (grid[gridx, y] < count)
                                {
                                    grid[gridx, y] += count;
                                    steps++;
                                    if (xcoordinates.Contains(gridx) && ycoordinates.Contains(y))
                                    {
                                        stepsTaken.Add($"lineL {count} took {steps} to be add x:{gridx}, y:{y}");
                                    }
                                  
                                }
                            }
                            gridy = gridy -= amount;
                            break;
                        }
                        case "D":
                        {
                            for (var y = gridy; y < gridy + amount; y++)
                            {
                                if (grid[gridx, y] < count)
                                {
                                    grid[gridx, y] += count;
                                    steps++;
                                    if (xcoordinates.Contains(gridx) && ycoordinates.Contains(y))
                                    {
                                        stepsTaken.Add($"lineL {count} took {steps} to be add x:{gridx}, y:{y}");
                                    }
                                }
                            }
                            gridy = gridy += amount;
                            break;
                        }
                    }

                 
                }

                count++;
            }

            foreach (var str in stepsTaken)
            {
                Console.WriteLine(str);
            }

            var maxDistance = 32768;

            for (var x = 0; x < grid.GetLength(0); x++)
            {
                for (var y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] == 3)
                    {
                        var manhattanDistance = ManhattanDistance(16384, x, 16384, y);
                        if (manhattanDistance < maxDistance && manhattanDistance != 0)
                        {
                            Console.WriteLine($"x: {x}, y: {y}");
                            maxDistance = manhattanDistance;
                        }
                    }
                }
            }

            Console.WriteLine(maxDistance);

            Console.ReadLine();
        }

        int ManhattanDistance(int x1, int x2, int y1, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }
    }
}

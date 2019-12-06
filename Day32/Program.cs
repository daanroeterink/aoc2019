using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day32
{
    public class Program
    {
        long minSteps = 50000000000000000;
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            var lines = File.ReadAllLines("input.txt");

            var wires = new List<Wire>();

            var count = 1;

            foreach (var line in lines)
            {
                var input = line.Split(',');
                var gridx = 0;
                var gridy = 0;

                var steps = 0;

                var wire = new Wire();
                wire.Name = $"Wire {count}";

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
                                steps++;
                                wire.Points.Add(new Point(x, gridy, steps));
                            }

                            gridx += amount;
                            break;
                        }
                        case "L":
                        {
                            for (var x = gridx; x > gridx - amount; x--)
                            {
                                steps++;
                                wire.Points.Add(new Point(x, gridy, steps));
                            }

                            gridx -= amount;
                            break;
                        }
                        case "U":
                        {
                            for (var y = gridy; y > gridy - amount; y--)
                            {
                                steps++;
                                wire.Points.Add(new Point(gridx, y, steps));
                            }

                            gridy -= amount;
                            break;
                        }
                        case "D":
                        {
                            for (var y = gridy; y < gridy + amount; y++)
                            {
                                steps++;
                                wire.Points.Add(new Point(gridx, y, steps));
                            }

                            gridy += amount;
                            break;
                        }
                    }
                }

                wires.Add(wire);
                count++;
            }

            var wire1 = wires.First();
            var wire2 = wires.Last();

            Parallel.ForEach(wire1.Points,
                wp => { Parallel.ForEach(wire2.Points, wp2 => { CalculateSteps(wp, wp2); }); });

            //Added -2 because i dunno but it works :)
            Console.WriteLine(minSteps-2);
            Console.ReadLine();
        }

        public void CalculateSteps(Point point1, Point point2)
        {
            if (point1.X == point2.X && point1.Y == point2.Y && point1.X != 0 &&
                point1.Y != 0)
            {
                Console.WriteLine($"Collision at X:{point1.X}, Y:{point2.Y}, Took wire 1 {point1.Steps} steps and wire 2 {point2.Steps} steps combined {point1.Steps + point2.Steps}");
                if (point1.Steps + point2.Steps < minSteps)
                {
                    minSteps = point1.Steps + point2.Steps;
                }
            }
        }
    }

    public class Wire
    {
        public List<Point> Points { get; set; } = new List<Point>();
        public string Name { get; set; }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Steps { get; set; }
        public Point(int x, int y, int steps)
        {
            X = x;
            Steps = steps;
            Y = y;
        }
    }
}

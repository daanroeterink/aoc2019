using System;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }


        public Program()
        {
            var input = File.ReadAllLines("input.txt").Select(int.Parse);

            var sum = input.Sum(GetFuel);

            Console.WriteLine(sum);
            Console.ReadLine();
        }

        public void day1of1()
        {
            var input = File.ReadAllLines("input.txt").Select(int.Parse);

            var sum = input.Sum(i => (i / 3) - 2);

            Console.WriteLine(sum);
            Console.ReadLine();
        }

        public int GetFuel(int weight)
        {
            var fuel = (weight / 3) - 2;

            if (fuel <= 0)
            {
                return 0;
            }

            return fuel + GetFuel(fuel);
        }
    }
}

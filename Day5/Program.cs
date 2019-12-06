using System;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        private int[] Array;

        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            Array = File.ReadAllText("input.txt").Split(',').Select(int.Parse).ToArray();

            var halt = false;
            var i = 0;

            var firstParam = Mode.Position;
            var secondParam = Mode.Position;
            var thirdParam = Mode.Position;

            while (!halt)
            {
                var input = Array[i];

                var inputString = input.ToString();

                int opcode = inputString.Length > 1 ? int.Parse($"{inputString[inputString.Length - 2]}{inputString[inputString.Length - 1]}") : int.Parse(inputString);

                firstParam = inputString.Length > 2
                        ? inputString[inputString.Length - 3] == '1' ? Mode.Immediate : Mode.Position
                        : Mode.Position;
                secondParam = inputString.Length > 3
                    ? inputString[inputString.Length - 4] == '1' ? Mode.Immediate : Mode.Position
                    : Mode.Position;
                thirdParam = inputString.Length > 4
                    ? inputString[inputString.Length - 5] == '1' ? Mode.Immediate : Mode.Position
                    : Mode.Position;
                

                if (opcode == 1)
                {
                    Array[Array[i + 3]] = GetValue(firstParam, Array[i + 1]) + GetValue(secondParam, Array[i + 2]);
                    i += 4;
                }
                else if (opcode == 2)
                {
                    Array[Array[i + 3]] = GetValue(firstParam, Array[i + 1]) * GetValue(secondParam, Array[i + 2]);
                    i += 4;
                }
                else if (opcode == 3)
                {
                    Console.WriteLine("Give input please");
                    var inputVariable = int.Parse(Console.ReadLine());
                    Array[Array[i + 1]] = inputVariable;
                    i += 2;
                }
                else if (opcode == 4)
                {
                    Console.WriteLine($"Value at: {Array[i + 1]} with mode {firstParam} is: {GetValue(firstParam, Array[i + 1])}");
                    i += 2;
                }
                else if (opcode == 99)
                {
                    Console.WriteLine("HALT");
                    halt = true;
                }
            } 
            Console.ReadLine();
        }

        public int GetValue(Mode mode, int value)
        {
            switch (mode)
            {
                case Mode.Immediate:
                    return value;
                    break;
                case Mode.Position:
                    return Array[value];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum Mode
    {
        Immediate,
        Position
    }
}
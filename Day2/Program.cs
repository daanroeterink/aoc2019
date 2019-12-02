using System;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            var array = File.ReadAllText("input.txt").Split(',').Select(int.Parse).ToArray();

            for (var noun = 0; noun < 99; noun++)
            {
                for (var verb = 0; verb < 99; verb++)
                {
                    var destArray = new int[array.Length];

                    array.CopyTo(destArray, 0);

                    destArray[1] = noun;
                    destArray[2] = verb;

                    for (var i = 0; i < destArray.Length; i += 4)
                    {
                        switch (destArray[i])
                        {
                            case 1:
                            {
                                var firstItem = destArray[destArray[i + 1]];
                                var secondItem = destArray[destArray[i + 2]];
                                var dest = destArray[i + 3];

                                destArray[dest] = firstItem + secondItem;
                                break;
                            }
                            case 2:
                            {
                                var firstItem = destArray[destArray[i + 1]];
                                var secondItem = destArray[destArray[i + 2]];
                                var dest = destArray[i + 3];

                                destArray[dest] = firstItem * secondItem;
                                break;
                            }
                            case 99:
                            {
                                if (destArray[0] == 19690720)
                                {
                                    Console.WriteLine(destArray[0]);
                                    Console.WriteLine($"noun {noun}");
                                    Console.WriteLine($"verb {verb}");
                                    Console.ReadLine();
                                }

                                break;
                            }
                        }
                    }
                }
            }

            Console.ReadLine();
        }

        public void day1of2()
        {
            var array = File.ReadAllText("input.txt").Split(',').Select(int.Parse).ToArray();

            for (var i = 0; i < array.Length; i += 4)
            {
                switch (array[i])
                {
                    case 1:
                    {
                        var firstItem = array[array[i + 1]];
                        var secondItem = array[array[i + 2]];
                        var dest = array[i + 3];

                        array[dest] = firstItem + secondItem;
                        break;
                    }
                    case 2:
                    {
                        var firstItem = array[array[i + 1]];
                        var secondItem = array[array[i + 2]];
                        var dest = array[i + 3];

                        array[dest] = firstItem * secondItem;
                        break;
                    }
                    case 99:
                        Console.WriteLine(array[0]);
                        break;
                }
            }
        }
    }
}

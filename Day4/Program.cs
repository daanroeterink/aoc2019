using System;
using System.IO;
using System.Linq;

namespace Day4
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            var numberOfpasswords = 0;
            using (var writer = File.AppendText("output.txt"))
            {
                for (int i = 235741; i < 706948; i++)
                {
                    var inputString = i.ToString();

                    if (ContainsPair(inputString))
                    {
                        bool okpassword = true;
                        for (int j = 0; j < inputString.Length - 1; j++)
                        {
                            if (int.Parse(inputString[j].ToString()) > int.Parse(inputString[j + 1].ToString()))
                            {
                                okpassword = false;
                            }
                        }

                        if (okpassword)
                        {
                            
                                writer.WriteLine(inputString);
                                numberOfpasswords++;
                           
                        }
                    }
                }
            }

            Console.WriteLine(numberOfpasswords);
            Console.ReadLine();
        }

        public bool ContainsPair(string input)
        {
            if (input[0] == input[1] && input[1] != input[2])
            {
                return true;
            }

            if (input[1] == input[2] && input[2] != input[3] && input[0] != input[1])
            {
                return true;
            }

            if (input[2] == input[3] && input[3] != input[4] && input[1] != input[2])
            {
                return true;
            }

            if(input[3] == input[4] && input[4] != input[5] && input[2] != input[3])
            {
                return true;
            }

            if (input[4] == input[5] && input[3] != input[4])
            {
                return true;
            }

            return false;
        }
    }
}

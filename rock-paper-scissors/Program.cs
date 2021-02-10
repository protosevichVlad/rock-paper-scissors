using System;
using System.Collections.Generic;
using System.Threading;

namespace rock_paper_scissors
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            if (!CheckArguments(args))
            {
                Console.WriteLine("You need to enter an odd number greater than and equal to 3 non-repeating lines.\nExample:");
                Console.WriteLine("rock-paper-scissors.exe rock paper scissors");
                return;
            }
            var game = new Game(args);
            game.Play();
        }

        private static bool CheckArguments(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine($"You entered less than 3 lines");
                return false;
            }
            
            if (args.Length % 2 != 1)
            {
                Console.WriteLine($"You passed an even number of lines");
                return false;
            }

            for (int i = 0; i < args.Length - 1; i++)
            {
                for (int j = i + 1; j < args.Length; j++)
                {
                    if (args[i] == args[j])
                    {
                        Console.WriteLine($"You have entered duplicate arguments.");
                        return false;
                    }
                }
            }
            
            return true;
        }
    }
}
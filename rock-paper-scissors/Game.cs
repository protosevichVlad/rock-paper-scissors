using System;
using System.Security.Cryptography;
using System.Text;

namespace rock_paper_scissors
{
    public class Game
    {
        private string[] args;

        public Game(string[] args)
        {
            this.args = args;
        }

        public void Play()
        {
            var secretKey = GetSecretKey();
            var computer = ComputerMove();
            var hmac = GetHmac(secretKey, Encoding.ASCII.GetBytes(args[computer]));

            Console.WriteLine($"HMAC: {ByteArrayToString(hmac)}");  
            PrintMenu();

            var man = ManMove();
            if (man == -1)
            {
                return;
            }

            Console.WriteLine($"Computer move: {args[computer]}");
            Console.WriteLine(DetermineTheWinner(man, computer));
            
            Console.WriteLine($"HMAC key: {ByteArrayToString(secretKey)}");
        }

        private byte[] GetSecretKey()
        {
            byte[] key = new byte[16];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(key);
            return key;
        }

        private byte[] GetHmac(byte[] key, byte[] value)
        {
            var hmac = new HMACSHA256(key);
            return  hmac.ComputeHash(value);
        }

        private string ByteArrayToString(byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", "");
        }

        private void PrintMenu()
        {
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {args[i]}");
            }
            Console.WriteLine("0 - Exit");
        }

        private int ComputerMove()
        {
            Random rnd = new Random();
            return rnd.Next(args.Length);
        }

        private int ManMove()
        {
            Console.Write("Enter your move: ");
            if (Int32.TryParse(Console.ReadLine(), out int index))
            {
                index--;
                if (index == -1)
                {
                    Console.WriteLine("Bye");
                    return index;
                }

                if (index < args.Length && index >= 0)
                {
                    Console.WriteLine($"Your move: {args[index]}");
                }
                else
                {
                    Console.WriteLine($"Error: you need to enter a number from 1 to {args.Length}. You entered: {index}.");
                    return -1;
                }
            }
            else
            {
                Console.WriteLine("Error: you need to enter a number.");
                return -1;
            }

            return index;
        }

        private string DetermineTheWinner(int man, int computer)
        {
            var d = man - computer;
            if (d == 0)
            {
                return "Draw.";
            }

            var half = (args.Length - 1) / 2;
            if ((d <= half && d > 0) || -half > d)
            {
                return "You win!";
            }
            return "You lose.";  
        }
    }
}
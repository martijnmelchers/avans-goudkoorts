using GoudKoorts.Model;
using System;

namespace GoudKoorts.View
{
    class InputView
    {
        public int GetInput()
        {
            var validInput = false;
            ConsoleKey key = new ConsoleKey();

            while (!validInput)
            {
                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                        validInput = true;
                        continue;
                    default:
                        Console.WriteLine("> Onbekende actie!");
                        continue;
                }
            }


            return int.Parse(key.ToString().Replace("D","")) -1 ;
        }

        public bool KeyAvailable()
        {
            return Console.KeyAvailable;
        }
    }
}

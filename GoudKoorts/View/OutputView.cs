using GoudKoorts.Model;
using System;


namespace GoudKoorts.View
{
    class OutputView
    {
        public void Render(PlacableObject Origin, int score)
        {
            Console.Clear();
            int height = 12;
            int width = 12;
            PlacableObject originTile = Origin;
            PlacableObject fieldBelow = originTile.PODown;
            for (int index1 = 0; index1 < height; ++index1)
            {
                for (int index2 = 0; index2 < width; ++index2)
                {

                    if (originTile is Brake)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    if (originTile is Switch)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(originTile.ToChar());
                    Console.ResetColor();
                    originTile = originTile.PORight;
                }
                originTile = fieldBelow;
                if (fieldBelow != null)
                    fieldBelow = originTile.PODown;
                Console.WriteLine();
            }
            Console.WriteLine("─────────────────────────────────────────────────────────────────────────");

            Console.WriteLine("Score: " + score);
        }

        public void ShowGameOver(int points)
        {
            Console.Clear();
            Console.WriteLine("Je hebt het spel verloren, het aantal punten dat je hebt verzameld zijn: ");
            Console.WriteLine(points);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 20;
            CLogger clg = new CLogger(size, size);
            CDrawer cdr = new CDrawer(size, size);
            clg.AddOffset(size + 6, 0);

            Level.FormateWindow();

            Level l = new Level(cdr, clg, TerrainGenerator.testLevelTerrain());
            l.Paint();
            for (; ; )
            {
                l.Step();
                l.Repaint();
            }
        }
        //public static void Main()
        //{
        //    // Get an array with the values of ConsoleColor enumeration members.
        //    ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
        //    // Save the current background and foreground colors.
        //    ConsoleColor currentBackground = Console.BackgroundColor;
        //    ConsoleColor currentForeground = Console.ForegroundColor;

        //    // Display all foreground colors except the one that matches the background.
        //    Console.WriteLine("All the foreground colors except {0}, the background color:",
        //                      currentBackground);
        //    foreach (var color in colors)
        //    {
        //        if (color == currentBackground) continue;

        //        Console.ForegroundColor = color;
        //        Console.WriteLine("   The foreground color is {0}.", color);
        //    }
        //    Console.WriteLine();
        //    // Restore the foreground color.
        //    Console.ForegroundColor = currentForeground;

        //    // Display each background color except the one that matches the current foreground color.
        //    Console.WriteLine("All the background colors except {0}, the foreground color:",
        //                      currentForeground);
        //    foreach (var color in colors)
        //    {
        //        if (color == currentForeground) continue;

        //        Console.BackgroundColor = color;
        //        Console.WriteLine("   The background color is {0}.", color);
        //    }

        //    // Restore the original console colors.
        //    Console.ResetColor();
        //    Console.WriteLine("\nOriginal colors restored...");
        //    Console.ReadKey();
        //}
    }
}

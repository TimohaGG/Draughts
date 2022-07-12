using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraughtsComponents;
namespace Draughts_v2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleKey consoleKey = (ConsoleKey.LeftArrow);
            int code = (int)consoleKey;
            Console.WriteLine(code);
            Console.ReadLine();
            Player player = new Player("Tim", 'w', 1, 0);
            //player.PrintCoords();
            //Console.WriteLine();
            Player player2 = new Player("Makar", 'b', 0, 5);
            //player2.PrintCoords();
            Field field = new Field(player.arr, player2.arr);
            while (true)
            {
                field.PrintField();
                ConsoleKey key = Console.ReadKey().Key;
                
            }
            
           
            Console.Read();
        }
    }
}

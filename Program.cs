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
            
            Player player = new Player("Tim", 'w', 1, 0);
            //player.PrintCoords();
            //Console.WriteLine();
            Player player2 = new Player("Makar", 'b', 0, 5);
            //player2.PrintCoords();
            Field field = new Field(player.arr, player2.arr);
            ConsoleKey key;
            while (true)
            {
                field.PrintField(player.arr[player.GetChoosenDraughtIndex()]);
                 key= Console.ReadKey().Key;
                if(key == ConsoleKey.Enter)
                {

                }
                else
                {
                    
                    player.MoveSelection(key);
                    Console.SetCursorPosition(0,0);
                }
                
            }
            
           
            Console.Read();
        }
    }
}

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
        
        static bool PlayerTurn(Player player, Player player2, Field field)
        {
            ConsoleKey key;
            field.PrintField(player.arr[player.GetChoosenDraughtIndex()]);
            key = Console.ReadKey().Key;
            if (key == ConsoleKey.Enter)
            {
                Console.Clear();
                return false;
            }
            else
            {

                player.MoveSelection(key);
                Console.SetCursorPosition(0, 0);
                return true;
            }
        }
        static void Main(string[] args)
        {
            
            Player player = new Player("Tim", 'w', 1, 0);
           
            Player player2 = new Player("Makar", 'b', 0, 5);
           
            Field field = new Field(player.arr, player2.arr);
           
            while (true)
            {
                while(PlayerTurn(player,player2, field))
                {
                    
                }
                while (PlayerTurn(player2, player, field))
                {

                }
            }
            
           
            Console.Read();
        }
    }
}

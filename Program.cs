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
        
        static int ChooseDirection()
        {
            Console.WriteLine("1. Вверх вправо");
            Console.WriteLine("2. Вверх влево");
            Console.WriteLine("3. Вниз вправо");
            Console.WriteLine("4. Вниз влево");
            int choise;
            do
            {
                Console.WriteLine("Введите цыфру");
                int.TryParse(Console.ReadLine(), out choise);
            } while (choise <= 0 || choise > 4);
            return choise;
        }
        static bool PlayerTurn(Player player, Player player2)
        {
            ConsoleKey key;
            int choosenIndex = player.GetChoosenDraughtIndex();
            Player.PrintField(player.arr[choosenIndex]);
            key = Console.ReadKey().Key;
            if (key == ConsoleKey.Enter)
            {
                int choise=ChooseDirection();
                if (player.arr[choosenIndex].isMovable((Player.Draught._direction)choise - 1, player, player2.skin))
                {
                    player.MooveDraught((Player.Draught._direction)choise - 1, ref player.arr[choosenIndex]);
                    Console.Clear();
                    Player.PlayingField.DeployDraughts(player.arr, player2.arr);
                    Player.PrintField(player.arr[choosenIndex]);
                    Console.WriteLine("Нажмите Enter что бы продолжить!");
                    Console.ReadLine();
                    Console.Clear();
                    return false;
                }
                else
                {
                    Console.WriteLine("Этот путь недоступен!");
                    Console.WriteLine("Нажмите Enter что бы продолжить!");
                    Console.ReadLine();
                    Console.Clear();
                    return true;
                }
                
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
            
            Player player1 = new Player("Tim", 'w', 0, 5);
           
            Player player2 = new Player("Makar", 'b', 1, 0);
           
            Player.PlayingField = new Field(player1.arr, player2.arr);
            Player.PlayingField.DeployDraughts(player1.arr, player2.arr);
            while (true)
            {
                while(PlayerTurn(player1,player2))
                {
                    
                }
                
                player1.Reverse();
                player2.Reverse();
                Player.PlayingField.DeployDraughts(player1.arr,player2.arr);
                player2.ReverseArr();
                while (PlayerTurn(player2, player1))
                {
                   
                }
                player1.Reverse();
                player2.Reverse();
                player2.ReverseArr();
                Player.PlayingField.DeployDraughts(player1.arr, player2.arr);
                
            }
            
           
            Console.Read();
        }
    }
}

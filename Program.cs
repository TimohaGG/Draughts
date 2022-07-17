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
        static void ReverseAll(ref Player player1, ref Player player2)
        {
            player1.Reverse();
            player2.Reverse();
            player2.ReverseArr();
            player1.ReverseArr();
            Player.PlayingField.DeployDraughts(player1.arr, player2.arr);
        }
        static void Pause()
        {
            Console.WriteLine("Нажмите Enter что бы продолжить!");
            Console.ReadLine();
            Console.Clear();
        }
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
            return choise-1;
        }
        static bool UseDraught(ref Player player1,ref Player player2, int choosenIndex, Player.Draught._direction choise)
        {
            
            if (player1.arr[choosenIndex].isMovable(choise, player1, player2.skin))
            {

                player1.MooveDraught(choise, ref player1.arr[choosenIndex]);
                if (player1.isKill(player2.arr))
                {
                    player2.DeleteDraughtFromArr(player1.arr[choosenIndex]);
                    player1.MooveDraught(choise, ref player1.arr[choosenIndex]);
                    Player.PlayingField.DeployDraughts(player1.arr, player2.arr);
                    if (player1.arr[choosenIndex].killIsNear(player2.skin, ref choise))
                    {
                        UseDraught(ref player1, ref player2, choosenIndex, choise);
                        //player1.MooveDraught(choise, ref player1.arr[choosenIndex]);
                    }
                    //else
                    //{
                    //    player1.MooveDraught(choise, ref player1.arr[choosenIndex]);
                    //}

                }
                Console.Clear();
                Player.PlayingField.DeployDraughts(player1.arr, player2.arr);
                Player.PrintField(player1.arr[choosenIndex]);


               
                return false;
            }
            else
            {
                Console.WriteLine("Этот путь недоступен!");
                Pause();
                return true;
            }

        }

        static bool PlayerTurn(Player player1, Player player2)
        {
            ConsoleKey key;
            int choosenIndex = player1.GetChoosenDraughtIndex();
            Player.PrintField(player1.arr[choosenIndex]);
            key = Console.ReadKey().Key;
            if (key == ConsoleKey.Enter)
            {
                //int choise=ChooseDirection();
                Player.Draught._direction choise = (Player.Draught._direction)ChooseDirection();
                return UseDraught(ref player1,ref player2, choosenIndex, choise);
                
            }
            else
            {

                player1.MoveSelection(key);
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
                Pause();
                ReverseAll(ref player1, ref player2);
                while (PlayerTurn(player2, player1))
                {
                    

                }
                Pause();
                ReverseAll(ref player1, ref player2);
                
            }
            
           
            Console.Read();
        }
    }
}

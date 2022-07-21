using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraughtsComponents;
using System.Diagnostics;
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
        static void Save(in Player player1, in Player player2)
        {
            player1.SaveGame("player1.xml");
            player2.SaveGame("player2.xml");
            
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
        static bool UseDraught(ref Player player1,ref Player player2, int choosenIndex, ref Player.Draught.Direction choise)
        {
            if (player1.arr[choosenIndex].IsMovable(choise, player2.skin))
            {
                player1.MooveDraught(choise, ref player1.arr[choosenIndex]);
                if (player1.IsKill(player2.arr))
                {
                    if (!player2.DeleteDraughtFromArr(player1.arr[choosenIndex]))
                    {

                        return false;
                    }
                    player1.MooveDraught(choise, ref player1.arr[choosenIndex]);
                    Player.PlayingField.DeployDraughts(player1.arr, player2.arr);
                    if (player1.arr[choosenIndex].KillIsNear(player2.skin, ref choise))
                    {
                        return true;
                    }
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
                Player.Draught.Direction choise = (Player.Draught.Direction)ChooseDirection();

                while( UseDraught(ref player1,ref player2, choosenIndex, ref choise))
                {
                }
                return false;
                
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
            Player player1 = new Player("unknown", 'w', 0, 5);
            Player player2 = new Player("unknown", 'b', 1, 0);
            Player.PlayingField = new Field(player1.arr, player2.arr);
            Console.WriteLine("----------ШАШКИ----------");
            Console.WriteLine("1. Новая игра");
            Console.WriteLine("2. Продолжить");

            int choise= int.Parse(Console.ReadLine());
            switch (choise)
            {
                case 1: {
                        player1.SetName();
                        player2.SetName();
                    }break;
                case 2: {
                        player1 = Player.LoadGame("player1.xml");
                        player2 = Player.LoadGame("player2.xml");
                    }
                    break;
            }
           
            Console.Clear();

            Player.PlayingField.DeployDraughts(player1.arr, player2.arr);
           

            while (!player1.isDefeeted()&&!player2.isDefeeted())
            {
                if (!player1.isDefeeted())
                {
                    do
                    {

                        Save(in player1, in player2);
                    } while (PlayerTurn(player1, player2));
                    Pause();
                    ReverseAll(ref player1, ref player2);
                }

                if (!player2.isDefeeted())
                {
                    do
                    {
                        Save(in player1, in player2);
                    } while (PlayerTurn(player2, player1));

                    Pause();

                    ReverseAll(ref player1, ref player2);
                }              
                
            }
            string winerName = !player1.isDefeeted() ? player1.name : player2.name;
            Console.WriteLine("Победитель: "+ winerName);
            Console.Read();
        }
    }
}

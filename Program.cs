﻿using System;
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
            Player.PlayingField.PrintField(player.arr[choosenIndex]);
            key = Console.ReadKey().Key;
            if (key == ConsoleKey.Enter)
            {
                int choise=ChooseDirection();
                player.MooveDraught((Player.Draught._direction)choise-1, ref player.arr[choosenIndex]);
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
            
            Player player = new Player("Tim", 'w', 0, 5);
           
            Player player2 = new Player("Makar", 'b', 1, 0);
           
            Player.PlayingField = new Field(player.arr, player2.arr);
            Player.PlayingField.DeployDraughts(player.arr, player2.arr);
            while (true)
            {
                while(PlayerTurn(player,player2))
                {
                    
                }
                //Player.PlayingField.Reverse();
                player.Reverse();
                player2.Reverse();
                Player.PlayingField.DeployDraughts(player.arr,player2.arr);
                while (PlayerTurn(player2, player))
                {
                   
                }
                player.Reverse();
                player2.Reverse();
                Player.PlayingField.DeployDraughts(player.arr, player2.arr);
                //Player.PlayingField.Reverse();
            }
            
           
            Console.Read();
        }
    }
}

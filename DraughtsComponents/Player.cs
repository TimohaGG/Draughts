using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtsComponents
{
    public partial class Player
    {
        //---------inserted class---------
        public partial class Draught{}

        //----------fields/props----------
        public int draughtsAmount=12;
        public Draught[] arr;
        public string name;
        public char skin;
        static public Field PlayingField;

        //----------constructor----------
        public Player(string name, char skin, int x, int y) {
            arr=new Draught[draughtsAmount];
            bool isEven;
            if (x == 0) isEven = true;
            else isEven = false;
            for (int i = 0; i < draughtsAmount; i++)
            {
                arr[i] = new Draught(x,y,skin);
                if (x + 2 < 8)
                    x += 2;
                else
                {
                    y++;
                    if (isEven)
                    {
                        x = 1;
                        isEven = false;
                    }
                    else
                    {
                        x = 0;
                        isEven=true;
                    }   
                }
            }
            this.name = name;
            this.skin = skin;
            arr[0].isChoosen = true;
        }
        //---------methods----------

        static public void PrintField(Draught choosen)
        {
            for (int i = 0; i < Field.fieldSize; i++)
            {
                for (int j = 0; j < Field.fieldSize; j++)
                {
                    if (i == choosen.y && j == choosen.x)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(PlayingField.fieldArr[i, j] + " ");
                        Console.ResetColor();
                    }
                    else
                        Console.Write(PlayingField.fieldArr[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        public bool isKill(Draught[] arrEnemy)
        {
            for (int i = 0; i < draughtsAmount; i++)
            {
                foreach (var item in arrEnemy)
                {
                    if (item.x == arr[i].x && item.y == arr[i].y)
                        return true;
                }
            }
            return false;
        }
        public void PrintCoords()
        {
            
            for (int i = 0; i < draughtsAmount; i++)
            {
                Console.WriteLine(arr[i].x + " " + arr[i].y);
            }
        }

        public int GetChoosenDraughtIndex()
        {
            for (int i = 0; i < draughtsAmount; i++)
            {
                if(arr[i].isChoosen)
                    return i;
            }
            arr[0].isChoosen = true;
            return 0;
        }

        public Draught[] MoveSelection(ConsoleKey direction)
        {
            int indexTmp=GetChoosenDraughtIndex();

            switch (direction)
            {
                case ConsoleKey.DownArrow:
                    {
                        if (indexTmp + 4 < draughtsAmount)
                        {
                            (arr[indexTmp].isChoosen, arr[indexTmp + 4].isChoosen) =
                               (arr[indexTmp + 4].isChoosen, arr[indexTmp].isChoosen);
                        }
                    }break;
                case ConsoleKey.UpArrow:
                    {
                        if(indexTmp - 4 >= 0)
                        {
                            (arr[indexTmp].isChoosen, arr[indexTmp - 4].isChoosen) =
                               (arr[indexTmp - 4].isChoosen, arr[indexTmp].isChoosen);
                        }
                    }break;
                case ConsoleKey.LeftArrow:
                    {
                        if (indexTmp - 1 >= 0)
                        {
                            (arr[indexTmp].isChoosen, arr[indexTmp - 1].isChoosen) =
                                (arr[indexTmp - 1].isChoosen, arr[indexTmp].isChoosen);
                        }
                    }break;
                case ConsoleKey.RightArrow:
                    {
                        if (indexTmp + 1 < draughtsAmount)
                        {
                            (arr[indexTmp].isChoosen, arr[indexTmp + 1].isChoosen) =
                                (arr[indexTmp + 1].isChoosen, arr[indexTmp].isChoosen);
                        }
                    }break;
                
            }
            return arr;
        }

        public void MooveDraught(Draught._direction direction, ref Draught draught)
        {
            switch (direction)
            {
                case Draught._direction.UP_R:
                    draught.y--;
                    draught.x++;
                    break;
                case Draught._direction.UP_L:
                    draught.y--;
                    draught.x--;
                    break;
                case Draught._direction.DOWN_R:
                    draught.y++;
                    draught.x++;
                    break;
                case Draught._direction.DOWN_L:
                    draught.y++;
                    draught.x--;
                    break;
            }
        }

        public void Reverse()
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].x = 7 - arr[i].x;
                arr[i].y = 7 - arr[i].y;
            }
        }
        public void ReverseArr()
        {
            Array.Reverse(arr);
        }

        public void DeleteDraughtFromArr( Draught draught)
        {
            int index;
            for (index = 0; index < draughtsAmount; index++)
            {
                if (arr[index].x == draught.x && arr[index].y == draught.y)
                {
                    Console.WriteLine("Нашел кого удалять!");
                    break;
                }
            }
            var tmp = new List<Draught>(arr); // Преобразование в список
            tmp.RemoveAt(index); // Удаление элемента
            arr = tmp.ToArray(); // Преобразование в массив
            draughtsAmount--;
            if (draught.isChoosen == true)
                arr[0].isChoosen = true;
        }
        public void ResetChoosenIndex()
        {
            int currentIndex = GetChoosenDraughtIndex();
            if (currentIndex == -1)
            {
                arr[0].isChoosen = true;
            }

        }
    }
}


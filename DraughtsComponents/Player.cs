using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DraughtsComponents
{
    [DataContractAttribute]
    public partial class Player
    {
        //---------inserted class---------
        public partial class Draught{}

        //----------fields/props----------
        [DataMemberAttribute]
        public int draughtsAmount=12;
        [DataMemberAttribute]
        public Draught[] arr;
        [DataMemberAttribute]
        public string name;
        [DataMemberAttribute]
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
            arr[0].IsChoosen = true;
        }

        public Player() { }
        //---------methods----------
        public void SetName()
        {
            Console.WriteLine("Введите имя игрока 1");
            name=Console.ReadLine();
        }
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
        public bool IsKill(Draught[] arrEnemy)
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
                if(arr[i].IsChoosen)
                    return i;
            }
            arr[0].IsChoosen = true;
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
                            (arr[indexTmp].IsChoosen, arr[indexTmp + 4].IsChoosen) =
                               (arr[indexTmp + 4].IsChoosen, arr[indexTmp].IsChoosen);
                        }
                    }break;
                case ConsoleKey.UpArrow:
                    {
                        if(indexTmp - 4 >= 0)
                        {
                            (arr[indexTmp].IsChoosen, arr[indexTmp - 4].IsChoosen) =
                               (arr[indexTmp - 4].IsChoosen, arr[indexTmp].IsChoosen);
                        }
                    }break;
                case ConsoleKey.LeftArrow:
                    {
                        if (indexTmp - 1 >= 0)
                        {
                            (arr[indexTmp].IsChoosen, arr[indexTmp - 1].IsChoosen) =
                                (arr[indexTmp - 1].IsChoosen, arr[indexTmp].IsChoosen);
                        }
                    }break;
                case ConsoleKey.RightArrow:
                    {
                        if (indexTmp + 1 < draughtsAmount)
                        {
                            (arr[indexTmp].IsChoosen, arr[indexTmp + 1].IsChoosen) =
                                (arr[indexTmp + 1].IsChoosen, arr[indexTmp].IsChoosen);
                        }
                    }break;
                
            }
            return arr;
        }

        public void MooveDraught(Draught.Direction direction, ref Draught draught)
        {
            switch (direction)
            {
                case Draught.Direction.UP_R:
                    draught.y--;
                    draught.x++;
                    break;
                case Draught.Direction.UP_L:
                    draught.y--;
                    draught.x--;
                    break;
                case Draught.Direction.DOWN_R:
                    draught.y++;
                    draught.x++;
                    break;
                case Draught.Direction.DOWN_L:
                    draught.y++;
                    draught.x--;
                    break;
            }
            if(draught.y == 0 || draught.y == 7)
            {
                draught.isQueen = true;
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

        public bool DeleteDraughtFromArr( Draught draught)
        {
            int index;
            for (index = 0; index < draughtsAmount; index++)
            {
                if (arr[index].x == draught.x && arr[index].y == draught.y)
                {
                    
                    break;
                }
            }
            var tmp = new List<Draught>(arr); // Преобразование в список
            tmp.RemoveAt(index); // Удаление элемента
            arr = tmp.ToArray(); // Преобразование в массив
            draughtsAmount--;
            if(draughtsAmount == 0)
            {
                return false;
            }
            if (draught.IsChoosen == true)
                arr[0].IsChoosen = true;
            return true;
        }
        
        public void SaveGame(string filename)
        {
            var playerList = new List<Player>();
            playerList.Add(this);
            DataContractSerializer serializer = new DataContractSerializer(typeof(List<Player>));

            XmlWriter writer = XmlWriter.Create(@"../../"+ filename);
            serializer.WriteObject(writer, playerList);
            writer.Close();
        }

        static public Player LoadGame(string filename)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(List<Player>));
            XmlReader reader = XmlReader.Create(@"../../"+filename);
            List<Player> PlayerReady= (List<Player>)serializer.ReadObject(reader);

            Player[] player=new Player[1];
            PlayerReady.CopyTo(player);
            reader.Close();
            return player[0];
        }

        public bool isDefeeted()
        {
            return draughtsAmount == 0;
        }
    }
}


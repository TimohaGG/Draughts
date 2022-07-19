using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DraughtsComponents
{
    public partial class Player
    {
        [DataContractAttribute]
        public partial class Draught
        {
            //----------fields\props---------
            [DataMemberAttribute]
            internal char skin;
           [DataMemberAttribute]
            internal int x;
            [DataMemberAttribute]
            internal int y;
            [DataMemberAttribute]
            public bool IsChoosen { get; internal set; }

            //----------constructor----------
            public Draught(int x, int y, char skin)
            {
                this.x = x;
                this.y = y;
                this.skin = skin;
                IsChoosen = false;
            }
            //---------enums----------
            public enum Direction
            {
                UP_R,
                UP_L,
                DOWN_R,
                DOWN_L
            }

            //----------methods----------
           
            public bool IsMovable(Direction direction, char EnemySymbol)
            {
                
                char[,] field = PlayingField.fieldArr;
                switch (direction)
                {
                    case Direction.UP_R:
                        {

                            if (y - 1 < 0 || x + 1 > Field.fieldSize)
                                return false;
                            else if (field[y - 1, x + 1] == '-')
                                return true;
                            else if (field[y - 1, x + 1] == EnemySymbol && field[y - 2, x + 2] == '-')
                                return true;
                            else
                                return false;
                        }
                    case Direction.UP_L:
                        {
                            if (y - 1 < 0 || x - 1 < 0)
                                return false;
                            else if (field[y - 1, x - 1] == '-')
                                return true;
                            else if (field[y - 1, x - 1] == EnemySymbol && field[y - 2, x - 2] == '-')
                                return true;
                            else
                                return false;
                        }
                        break;
                    case Direction.DOWN_R:
                        {
                            if (y + 2 >= Field.fieldSize || x + 2 >= Field.fieldSize)
                                return false;
                            else if (field[y + 1, x + 1] == EnemySymbol && field[y + 2, x + 2] == '-')
                                return true;
                            else return false;
                        }
                        
                    case Direction.DOWN_L:
                        {
                            if(y+2 >= Field.fieldSize || x - 2<0)
                                return false;
                            if (field[y + 1, x - 1] == EnemySymbol && field[y + 2, x - 1] == '-')
                                return true;
                            else return false;
                        }
                        
                    default:
                        break;
                }
                return true;
            }
             public bool KillIsNear(char enemySymbol, ref Direction direction)
             {
                char[,] fieldTmp = PlayingField.fieldArr;
                if (y - 2 >= 0 && x + 2 < Field.fieldSize)
                {
                    if (fieldTmp[y - 1, x + 1] == enemySymbol && fieldTmp[y - 2, x + 2] == '-')
                    {
                        direction = Direction.UP_R;
                        return true;
                    }
                }
                if (y - 2 >= 0 && x - 2 >= 0)
                {
                    if (fieldTmp[y - 1, x - 1] == enemySymbol && fieldTmp[y - 2, x - 2] == '-')
                    {
                        direction = Direction.UP_L;
                        return true;
                    }
                }
                if (y + 2 < Field.fieldSize && x + 2 < Field.fieldSize)
                {
                    if (fieldTmp[y + 1, x + 1] == enemySymbol && fieldTmp[y + 2, x + 2] == '-')
                    {
                        direction = Direction.DOWN_R;
                        return true;
                    }
                }
                if (y + 2 < Field.fieldSize && x - 2 >= 0)
                {
                    if (fieldTmp[y + 1, x - 1] == enemySymbol && fieldTmp[y + 2, x - 1] == '-')
                    {
                        direction = Direction.DOWN_L;
                        return true;
                    }
                }
               
                return false;
            }
            
        }
    }
}

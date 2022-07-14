﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtsComponents
{
    public partial class Player
    {
        public partial class Draught
        {
            //----------fields\props---------
            internal char skin;
            internal int x;
            internal int y;
            public bool isChoosen { get; internal set; }

            //----------constructor----------
            public Draught(int x, int y, char skin)
            {
                this.x = x;
                this.y = y;
                this.skin = skin;
                isChoosen = false;
            }
            //---------enums----------
            public enum _direction
            {
                UP_R,
                UP_L,
                DOWN_R,
                DOWN_L
            }

            //----------methods----------
           
            public bool isMovable(_direction direction, Player player, char EnemySymbol)
            {
                int xTmp = x, yTmp=y;
                char[,] field = Player.PlayingField.fieldArr;
                switch (direction)
                {
                    case _direction.UP_R:
                        {
                            

                            if (field[y - 1, x + 1] == '-')
                                return true;
                            else if (field[y - 1, x + 1] == EnemySymbol && field[y - 2, x + 2] == '-')
                                return true;
                            else
                                return false;
                        }
                    case _direction.UP_L:
                        {

                        }
                        break;
                    case _direction.DOWN_R:
                        break;
                    case _direction.DOWN_L:
                        break;
                    default:
                        break;
                }
                return true;
            }
           
            
        }
    }
}

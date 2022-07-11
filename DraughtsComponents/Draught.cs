using System;
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
            public void Use(_direction direction)
            {
                switch (direction)
                {
                    case _direction.UP_R:
                        y--;
                        x++;
                        break;
                    case _direction.UP_L:
                        y--;
                        x--;
                        break;
                    case _direction.DOWN_R:
                        y++;
                        x++;
                        break;
                    case _direction.DOWN_L:
                        y++;
                        x--;
                        break;
                }
            }
           
            
        }
    }
}

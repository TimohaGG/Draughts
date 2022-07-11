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
        private const int draughtsAmount=12;
        public Draught[] arr=new Draught[draughtsAmount];
        internal string name;

        //----------constructor----------
        public Player(string name, char skin, int xFirst, int yFirst) {
            for (int i = 0; i < draughtsAmount; i++)
            {
                arr[i] = new Draught(0,0,skin);
                //paste code here
            }
        }
        //---------methods----------
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

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtsComponents
{
    public class Field
    {
        //----------fields----------
        internal char[,] fieldArr;
        internal const int fieldSize=8;

        //----------constructor----------
        public Field(Player.Draught[] arr1, Player.Draught[] arr2)
        {
            fieldArr=new char[fieldSize,fieldSize];
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    fieldArr[i, j] = '-';
                }
            }
           
        }

        //----------methods----------

        public void DeployDraughts(Player.Draught[] arr1, Player.Draught[] arr2)
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    fieldArr[i, j] = '-';
                }
            }
            for (int i = 0; i < arr1.Length; i++)
            {
                fieldArr[arr1[i].y, arr1[i].x] = arr1[i].skin;
                
            }
            for (int i = 0; i < arr2.Length; i++)
            {
                fieldArr[arr2[i].y, arr2[i].x] = arr2[i].skin;
            }
        }
       
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projettaquin
{
    class NodeEnrepot:GenericNode
    {
        public static int[,] tabEntrepot = new int[25, 25];
        int NBC;

        void InitialiserEntrepot()
        {
            for(int i=2; i<22; i+=2)
            {

                for (int premiereLigne = 4; premiereLigne < 13; premiereLigne++) { tabEntrepot[premiereLigne, i] = -1; }
                for (int deuxiemeLigne = 16; deuxiemeLigne < 25; deuxiemeLigne++) { tabEntrepot[deuxiemeLigne, i] = -1; }

            }

            for(int j=0; j<25;j++)
            {
                tabEntrepot[0, j] = -2;
            }
        }
    }
}

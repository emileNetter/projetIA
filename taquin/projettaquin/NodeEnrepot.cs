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
            for (int l = 0; l < tabEntrepot.GetLength(0); l++)
                for (int m = 0; m < tabEntrepot.GetLength(1); m++) { tabEntrepot[l, m] = 0; }

                    for (int i = 2; i < 22; i += 2)
                    {

                        for (int premiereLigne = 4; premiereLigne < 13; premiereLigne++) { tabEntrepot[premiereLigne, i] = -1; }
                        for (int deuxiemeLigne = 16; deuxiemeLigne < 25; deuxiemeLigne++) { tabEntrepot[deuxiemeLigne, i] = -1; }

                    }

            for(int j=0; j<25;j++)
            {
                tabEntrepot[0, j] = -2;
            }
        }

        public override bool IsEqual(projettaquin.GenericNode N2)
        {
            throw new NotImplementedException();
        }
        public override double GetArcCost(GenericNode N2)
        {
            throw new NotImplementedException();
        }
        public override bool EndState()
        {
            throw new NotImplementedException();
        }
        public override List<GenericNode> GetListSucc()
        {
            throw new NotImplementedException();
        }
        public override void CalculeHCost()
        {
            throw new NotImplementedException();
        }

    }
}

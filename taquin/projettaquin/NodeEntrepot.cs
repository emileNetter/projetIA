using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace projettaquin
{
    class NodeEntrepot:GenericNode
    {
        public static int[,] tabEntrepot = new int[25, 25];
        public int posX;
        public int posY; 
        int NBC;

        public NodeEntrepot(int posX, int posY)
        {
            // retirer 1 puisque indice du tableau commence à 0
            this.posX = posX;
            this.posY = posY;
        }

        public static int[,] InitialiserEntrepot()
        {
            for (int l = 0; l < tabEntrepot.GetLength(0); l++)
                for (int m = 0; m < tabEntrepot.GetLength(1); m++) { tabEntrepot[l, m] = 0; }

                    for (int i = 2; i < 24; i += 2)
                    {

                        for (int premiereLigne = 2; premiereLigne < 11; premiereLigne++) { tabEntrepot[premiereLigne, i] = -1; }
                        for (int deuxiemeLigne = 14; deuxiemeLigne < 23; deuxiemeLigne++) { tabEntrepot[deuxiemeLigne, i] = -1; }

                    }

            for(int j=0; j<25;j++)
            {
                tabEntrepot[0, j] = -2;
            }
            return tabEntrepot;
        }

        public override bool IsEqual(GenericNode N2)
        {
            NodeEntrepot NE = (NodeEntrepot)N2;
            return (NE.posX == posX && NE.posY == posY);
        }
        public override double GetArcCost(GenericNode N2)
        {
            return (1);
        }
        public override bool EndState(Objet objet)
        {
            int positionFinaleY;
            Objet.Orientation orientation = objet.orientation;
            if (orientation == Objet.Orientation.Nord)
            {
                positionFinaleY = objet.posY -1; 
            }
            else
            {
                positionFinaleY = objet.posY + 1;
            }

            if(this.posX == 3 && this.posY == 3)
            {
                return true;
            } else
            {
                return false;
            }
        }
        public override List<GenericNode> GetListSucc()
        {
            List<GenericNode> lsucc = new List<GenericNode>();
            if(posY<24)
            {
                if (tabEntrepot[posX, posY + 1] != -1)
                {
                    lsucc.Add(new NodeEntrepot(posX , posY + 1));
                }
            }
           if(posY>0)
            {
                if (tabEntrepot[posX, posY - 1] != -1)
                {
                    lsucc.Add(new NodeEntrepot(posX , posY -1));
                }
            }
            if(posX<24)
            {
                if (tabEntrepot[posX + 1, posY] != -1 && posX < 24)
                {
                    lsucc.Add(new NodeEntrepot(posX + 1, posY ));
                }
            }
            
            if(posX>0)
            {
                if (tabEntrepot[posX - 1, posY] != -1)
                {
                    lsucc.Add(new NodeEntrepot(posX-1, posY));
                }
            }
            
            return lsucc;

        }
        public override void CalculeHCost(Objet objet)
        {
            int distX;
            int distY;
            double distF; 
            distX = Math.Abs(this.posX - objet.posX);
            distY = Math.Abs(this.posY - objet.posY);

            distF = Math.Sqrt(Math.Pow(distX,2) + Math.Pow(distY,2));
            HCost = distF;
            SetEstimation(distF);
        }

    }
}

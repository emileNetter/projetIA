using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
namespace projettaquin
{
    class NodeTemps : GenericNode
    {
        public static int[,] tabEntrepot = new int[25, 25];
        public int posX;
        public int posY;
        public bool directionH;
        public Point direction;
        public int cout;
        

        public NodeTemps(int posX, int posY, bool b)
        {
            // retirer 1 puisque indice du tableau commence à 0
            this.posX = posX;
            this.posY = posY;
            directionH = b;
        }
        public NodeTemps(int posX, int posY,Point direction)
        {
            // retirer 1 puisque indice du tableau commence à 0
            this.posX = posX;
            this.posY = posY;
            this.direction = direction;
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

            for (int j = 0; j < 25; j++)
            {
                tabEntrepot[0, j] = -2;
            }
            return tabEntrepot;
        }

        public override bool IsEqual(GenericNode N2)
        {
            NodeTemps NE = (NodeTemps)N2;
            return (NE.posX == posX && NE.posY == posY);
        }
        public override double GetArcCost(GenericNode N2)
        {
            //NodeTemps Nres = (NodeTemps)N2;
            //cout = CalculeDirectionH(Nres);
            return 4;

        }
        public override bool EndState(Objet objet)
        {
            int positionFinaleY;
            Objet.Orientation orientation = objet.orientation;
            if (orientation == Objet.Orientation.Nord)
            {
                positionFinaleY = objet.posY - 1;
            }
            else
            {
                positionFinaleY = objet.posY + 1;
            }

            if (this.posX == objet.posX && this.posY == positionFinaleY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override List<GenericNode> GetListSucc()
        {
            List<GenericNode> lsucc = new List<GenericNode>();
            if (posY < 24)
            {
                if (tabEntrepot[posX, posY + 1] != -1)
                {
                    lsucc.Add(new NodeTemps(posX, posY + 1,direction));
                }
            }
            if (posY > 0)
            {
                if (tabEntrepot[posX, posY - 1] != -1)
                {
                    lsucc.Add(new NodeTemps(posX, posY - 1,direction));
                }
            }
            if (posX < 24)
            {
                if (tabEntrepot[posX + 1, posY] != -1 && posX < 24)
                {
                    lsucc.Add(new NodeTemps(posX + 1, posY, direction));
                }
            }

            if (posX > 0)
            {
                if (tabEntrepot[posX - 1, posY] != -1)
                {
                    lsucc.Add(new NodeTemps(posX - 1, posY,direction));
                }
            }

            return lsucc;

        }

        public void setDirection(Point p)
        {
            direction = p;
        }
        public int CalculeDirectionH(NodeTemps N2)
        {
            // This = noeud prcdt
            bool res=false;
            var signX = Math.Sign(N2.posX - this.posX);
            var signY = Math.Sign(N2.posY - this.posY);
            if(signX!= direction.X || signY != direction.Y)
            {
                direction = new Point(signX, signY);
                return 4;
            }
            else
            {
                direction = new Point(signX, signY);
                return 1;
            }                       
        }
        
        public override void CalculeHCost(Objet objet)
        {
            //int distX;
            //int distY;
            //double distF;
            //distX = Math.Abs(this.posX - objet.posX);
            //distY = Math.Abs(this.posY - objet.posY);

            //distF = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));
            //HCost = distF;
            //SetEstimation(distF);
        }

    }
}

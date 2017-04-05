using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace projettaquin
{
    class NodeDistance:GenericNode
    { 
        public int posX;
        public int posY; 
        int NBC;

        public NodeDistance(int posX, int posY)
        {
            // retirer 1 puisque indice du tableau commence à 0
            this.posX = posX;
            this.posY = posY;
        }

        public override bool IsEqual(GenericNode N2)
        {
            NodeDistance NE = (NodeDistance)N2;
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

            if(this.posX == objet.posX && this.posY == positionFinaleY)
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
                if (GenericNode.tabEntrepot[posX, posY + 1] != -1)
                {
                    lsucc.Add(new NodeDistance(posX , posY + 1));
                }
            }
           if(posY>0)
            {
                if (GenericNode.tabEntrepot[posX, posY - 1] != -1)
                {
                    lsucc.Add(new NodeDistance(posX , posY -1));
                }
            }
            if(posX<24)
            {
                if (GenericNode.tabEntrepot[posX + 1, posY] != -1 )
                {
                    lsucc.Add(new NodeDistance(posX + 1, posY ));
                }
            }
            
            if(posX>0)
            {
                if (GenericNode.tabEntrepot[posX - 1, posY] != -1)
                {
                    lsucc.Add(new NodeDistance(posX-1, posY));
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

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
        public int posX;
        public int posY;
        public bool directionH;
        public Point direction;
        //public int cout;
        

       
        public NodeTemps(int posX, int posY,Point direction)
        {
            // retirer 1 puisque indice du tableau commence à 0
            this.posX = posX;
            this.posY = posY;
            this.direction = direction;
        }


        public override bool IsEqual(GenericNode N2)
        {
            NodeTemps NE = (NodeTemps)N2;
            return (NE.posX == posX && NE.posY == posY);
        }
        public override double GetArcCost(GenericNode N2)
        {
            double cout = 0;
            NodeTemps Nres = (NodeTemps)N2;
            cout = CalculeDirectionH(Nres);
            return cout ;

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
                if (GenericNode.tabEntrepot[posX, posY + 1] != -1)
                {
                    lsucc.Add(new NodeTemps(posX, posY + 1,new Point(0,1)));
                }
            }
            if (posY > 0)
            {
                if (GenericNode.tabEntrepot[posX, posY - 1] != -1)
                {
                    lsucc.Add(new NodeTemps(posX, posY - 1,new Point(0,-1)));
                }
            }
            if (posX < 24)
            {
                if (GenericNode.tabEntrepot[posX + 1, posY] != -1 && posX < 24)
                {
                    lsucc.Add(new NodeTemps(posX + 1, posY, new Point(1,0)));
                }
            }

            if (posX > 0)
            {
                if (GenericNode.tabEntrepot[posX - 1, posY] != -1)
                {
                    lsucc.Add(new NodeTemps(posX - 1, posY,new Point(-1,0)));
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
           
           
            if(this.direction!=N2.direction)
            {
                
                return 3;
            }
            else
            {
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

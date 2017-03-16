using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projettaquin
{
    class NodeTaquin : GenericNode
    {
        string Name;
        public NodeTaquin( string newname ) : base() 
        {
            Name = newname;
        }

        public override bool IsEqual (GenericNode N2)
        {
            NodeTaquin NT = (NodeTaquin)(N2);
            return (NT.Name == Name);
        }

        public override double GetArcCost(GenericNode N2)
        {
            return (1);
        }

        public override bool EndState()
        {
            return (this.Name == "12345678?") ;
        }

        public override List<GenericNode> GetListSucc()
        {
            // On reconstruit le carré 3x3 à partir du nom et on mémorise la position du ?
            int posx=-1; int posy=-1;
            char[,] tab = new char[3,3];
            for (int j=0;j<=2; j++)
                for (int i=0;i<=2;i++)
                {
                    tab[i,j] = Name[j*3+i];

                    if (tab[i,j] == '?')
                    {
                        posx = i;
                        posy = j;
                    }
                }

            List<GenericNode> lsucc = new List<GenericNode>();
            if (posx > 0)
            {
                // Successeur à gauche
                // recopie du tableau
                char[,] tab2 = new char[3, 3];
                for (int j = 0; j <= 2; j++)
                    for (int i = 0; i <= 2; i++)
                    {
                        tab2[i, j] = tab[i, j];
                    }
                // MAJ de la position du ?
                tab2[posx, posy] = tab2[posx - 1, posy];
                tab2[posx - 1, posy] = '?';
                // Ajout à listsucc
                lsucc.Add(new NodeTaquin(GetStringFromTab(tab2)));
            }
            if (posx < 2)
            {
                // Successeur à droite
                // recopie du tableau
                char[,] tab2 = new char[3, 3];
                for (int j = 0; j <= 2; j++)
                    for (int i = 0; i <= 2; i++)
                    {
                        tab2[i, j] = tab[i, j];
                    }
                // MAJ de la position du ?
                tab2[posx, posy] = tab2[posx + 1, posy];
                tab2[posx + 1, posy] = '?';
                // Ajout à listsucc
                lsucc.Add(new NodeTaquin(GetStringFromTab(tab2)));
            }

            if (posy > 0)
            {
                // Successeur en haut
                // recopie du tableau
                char[,] tab2 = new char[3, 3];
                for (int j = 0; j <= 2; j++)
                    for (int i = 0; i <= 2; i++)
                    {
                        tab2[i, j] = tab[i, j];
                    }
                // MAJ de la position du ?
                tab2[posx, posy] = tab2[posx, posy-1];
                tab2[posx, posy-1] = '?';
                // Ajout à listsucc
                lsucc.Add(new NodeTaquin(GetStringFromTab(tab2)));
            }
            if (posy < 2)
            {
                // Successeur en bas
                // recopie du tableau
                char[,] tab2 = new char[3, 3];
                for (int j = 0; j <= 2; j++)
                    for (int i = 0; i <= 2; i++)
                    {
                        tab2[i, j] = tab[i, j];
                    }
                // MAJ de la position du ?
                tab2[posx, posy] = tab2[posx, posy + 1];
                tab2[posx, posy + 1] = '?';
                // Ajout à listsucc
                lsucc.Add(new NodeTaquin(GetStringFromTab(tab2)));
            }

            return lsucc;
        }

        public override void CalculeHCost()
        {
             // on compte les mal placés 
              int nb=8;
              for (int i = 0; i < 8; i++)
                  if (Name[i] == Convert.ToChar(i + 49)) // En code ASCII 1 est le 49ème caractère
                      nb--;
              SetEstimation(nb);
            // SetEstimation(0);
        }

        private string GetStringFromTab ( char [,] tab )
        {
            string newname = "";
            for (int j=0;j<=2; j++)
                for (int i=0;i<=2;i++)
                    {
                       newname = newname + tab[i,j]; 
                   }  
            return newname;
        }

        public override string ToString()
        {
            return Name; 
        }
    }
}

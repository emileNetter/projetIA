using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace projettaquin
{
    //Classe permettant de calculer la durée en sec d'un chemin ( liste de nodeEntrepot)
    //
    public class Trajectoire
    {
        private List<GenericNode> noeuds;
        public int temps;
        public Objet objet;

        public Trajectoire(List<GenericNode> noeuds,Objet objet)
        {
            this.noeuds = noeuds;
            this.objet = objet;
        }

        public int GetTemps()
        {
            return temps;
        }

        public void SetTemps(int i)
        {
            temps = i;
        }

        public void calculeTemps()
        {
            temps = 0;
            Point direction = new Point(0, 0);
            if(noeuds.Count == 0)
            {
                SetTemps(0);
            }

            for(int i =1; i < noeuds.Count; i++)
            {
                NodeDistance noeudEntrepot = (NodeDistance)noeuds[i];
                NodeDistance noeudPrecedent = (NodeDistance)noeuds[i - 1];
                var signX = Math.Sign(noeudEntrepot.posX - noeudPrecedent.posX);
                var signY = Math.Sign(noeudEntrepot.posY - noeudPrecedent.posY);               

                if (i == 1)
                {
                    temps += 1;
                    direction = new Point(signX, signY);
                }

                else
                {
                    if (signX != direction.X || signY != direction.Y) // s'il change de direction, 3 secondes sur place + 1 seconde
                    {
                        temps += 4;
                    }
                    else
                    {
                        temps += 1; // s'il ne change pas de direction
                    }
                    direction = new Point(signX, signY); //nouveau vecteur direction

                }
            }
            temps += 10; // Orientation + 10 secondes;
            temps += 2 * objet.hauteur; // Hauteur de l'objet fois 2
            SetTemps(temps); 
        }

        public override string ToString()
        {
            return temps.ToString();
        }

    }
}

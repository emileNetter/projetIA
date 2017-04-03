using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsApplication1
{
    public class CarteSOM
    {
        private Neurone[,] tab;
        private int nbcol, nblignes;

        public CarteSOM( int nbcol, int nblignes, int nbpoids, int valeurmax)
        {
            this.nbcol = nbcol;
            this.nblignes = nblignes;
            tab = new Neurone[nbcol, nblignes];
            for (int i=0; i<nbcol; i++)
                for (int j = 0; j < nblignes; j++)
                {
                    tab[i, j] = new Neurone(nbpoids, valeurmax);
                }
        }
        public Neurone GetNeurone(int i, int j)
        { return tab[i, j]; }

        public void AlgoKohonen(List<Observation> lobs, double alpha)
        {
            int distmax = 1;
            // pour chaque observation de lobs
            foreach (Observation obs in lobs)
            {
                //    pour chaque neurone de la carte
                //        recherche des indices besti, bestj du neurone qui minimise l'erreur
                double minerreur = 1000;
                alpha = alpha - 0.00001; 
                int besti=0; int bestj=0;
                for (int i = 0; i < nbcol; i++)
                    for (int j = 0; j < nblignes; j++)
                    {
                        double erreur = tab[i, j].CalculeErreur(obs);
                        if (erreur < minerreur)
                        {
                            besti = i; bestj = j;
                            minerreur = erreur;
                        }
                    }
                //    pour chaque neurone dans le voisinage de besti, bestj
                //        mettre à jour les poids du neurone
                for (int i = besti - distmax; i <= besti + distmax; i++)
                    for (int j = bestj - distmax; j <= bestj + distmax; j++)
                        if (i >= 0 && i < nbcol &&
                            j >= 0 && j < nblignes)
                            tab[i, j].ModifiePoids(obs, 
                                
                                alpha);

            }
        }
        public void regroupement(List<Observation> lobs, int nbclasses)
        {
            // Recherche des neurones qui ne gagnent jamais ou presque jamais
            int[,] comptage = new int[nblignes,nbcol];
            for (int i=0; i<nbcol; i++)
                for (int j=0; j<nblignes; j++)
                    comptage[i,j]=0;

            foreach (Observation obs in lobs)
            {   
                double minerreur = 100000;
                int besti=0; int bestj=0;
                for (int i = 0; i < nbcol; i++)
                    for (int j = 0; j < nblignes; j++)
                    {
                        double erreur = tab[i, j].CalculeErreur(obs);
                        if (erreur < minerreur)
                        {
                            besti = i; bestj = j;
                            minerreur = erreur;
                        }
                    }
                comptage[besti, bestj]++;
            }


            // Initialisation des classes
            for (int i=0; i<nbcol; i++)
                for (int j=0; j<nblignes; j++)
                    if (comptage[i,j]>5)
                      Form1.listclasses.Add( new Classe( tab[i,j]));


            // Fusion des classes; critère le plus simple : distance interclasse
            do {
                Classe bestc1 = Form1.listclasses[0];
                Classe bestc2 = Form1.listclasses[1];
                double distmin = 1000000;
                foreach (Classe c1 in Form1.listclasses)
                {
                    foreach (Classe c2 in Form1.listclasses)
                    {
                        if (c1 != c2)
                        {
                            double dist = CalculeDistInterClasse(c1, c2);
                            if (dist < distmin)
                            {
                                distmin = dist;
                                bestc1 = c1;
                                bestc2 = c2;
                            }
                        }
                    }

                }
                // Fusion des 2 classes les plus proches
                bestc1.FusionAvec(bestc2);
                Form1.listclasses.Remove(bestc2);
            }
            while (Form1.listclasses.Count > nbclasses);
        }

        private double CalculeDistInterClasse(Classe c1, Classe c2)
        {
            double bestdist = 1000;
            foreach (Neurone n1 in c1.GetNeurones())
                foreach (Neurone n2 in c2.GetNeurones())
                {
                    double dist = n1.DistInterNeurone(n2);
                    if (dist < bestdist)
                    { bestdist = dist; }
                }
            return bestdist;
        }
    }
}

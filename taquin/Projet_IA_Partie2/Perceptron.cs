using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace Projet_IA_Partie2
{
    class Perceptron
    {
        public double entreeX;
        public double entreeY;
        public const int biais = 1; //le biais qui a son propre poids
        public double sortie = 0;
        private double[] poids;
        public int nbErreurs;
        int nbpoids = 3;
        int iteration = 0;
        int seuil = 1;
        double coeff = 0.2;

        //entrees tests
        static double[,] inputs = new double[,]
        {
            { 36.1, 17}, { 41.1, 16.5 }, { 47.9, 18 },
            { 47, 18.5 }, { 49.8, 22 }, { 48.5, 21 },
            { 47.8, 11.7 }, { 55.5, 16.4 }, { 62.0, 13.0 },
            { 64.1, 22 }, { 64.3, 18.5 }, { 65.3, 2 }
        };

        int nbLignes = inputs.GetUpperBound(0) + 1;
        // sorties tests
        static int[] outputs = new int[]
        {
            1,1,1,1,1,1,
            0,0,0,0,0,0
        };

        public Perceptron()
        {

        }
        public Perceptron(double entreeX, double entreeY)
        {
            this.entreeX = entreeX;
            this.entreeY = entreeY;
            
        }

        public double GetPoids(int i)
        { return poids[i]; }

        public double Seuillage(double d)
        {
            return 1 / (1 + Math.Exp(-d));
        }

        public int CalculeSortie(double[] poids,double x, double y)
        {
            double somme = x * poids[0]*coeff + y * poids[1]*coeff +biais*poids[2];
            somme = Seuillage(somme);
            return (somme >= seuil) ? 1 : 0;
         
        }

        public void Traitement()
        {
            // initialisation des poids
            Random random = new Random();
            poids = new double[3];
            for (int i = 0; i < nbpoids; i++)
                poids[i] = random.NextDouble();
            do
            {
                //initialisation du nombre d'erreurs
                this.nbErreurs = 0;

                //boucle sur chacun des couples d'entrées
                for (int i = 0; i < nbLignes; i++)
                {
                    this.entreeX = inputs[i, 0];
                    this.entreeY = inputs[i, 1];
                    sortie = CalculeSortie(poids, entreeX, entreeY);
                    //sortie = Seuillage(sortie);
                    if (sortie == 0 && sortie != outputs[i])
                    {                       
                        for (int j = 0; j < 2; j++)
                        {
                            poids[j] += inputs[i, j];
                        }
                        nbErreurs++;
                    }
                    else if (sortie == 1 && sortie != outputs[i])
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            poids[k] -= inputs[i, k];
                        }
                        nbErreurs++;
                    }
                    iteration++;
                }
                
            } while (iteration < 500);

        }
    }
}

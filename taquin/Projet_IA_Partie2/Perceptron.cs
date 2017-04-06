using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projet_IA_Partie2
{
    class Perceptron
    {
        public List<object> entrees;
        public PointF coupleXY; //couple de données (entrées)
        public const int biais = 1; //le biais qui a son propre poids
        public float sortie;
        private List<double> poids;
        Random random;
        public int nbErreurs;
        int nbpoids = 3;

        public Perceptron(PointF coupleXY)
        {
            sortie = 0;
            this.coupleXY = coupleXY;
            poids = new List<double>();
            for (int i = 0; i < nbpoids; i++)
                poids.Add(random.NextDouble());
        }

        public double GetPoids(int i)
        { return poids[i]; }

        public double Seuillage(double d)
        {
            return 1 / (1 + Math.Exp(-d));
        }
        public void CalculeSortie(List<double> poids)
        {

        }
    }
}

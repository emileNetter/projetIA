using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification
{
    public class Neurone
    {
        private int numero;
        public List<Neurone> entrees;
        public List<Neurone> sorties;
        public int numerocouche;
        public double sortie;
        public double somme = 0;
        public double delta;

        private List<double> poids;

        public Neurone(int num, int numcouche)
        {
            numero = num;
            numerocouche = numcouche;
            sortie = 0;
            entrees = new List<Neurone>();
            sorties = new List<Neurone>();
        }

        public int GetNumero() { return numero; }

        public double GetSortie()
        {
            return sortie;
        }

        public int GetNumeroCouche()
        {
            return numerocouche;
        }

        public double GetSomme() { return somme; }

        public double Sigmoide(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        public double Derivee(double x)
        {
            // Dérivée de la sigmoïde, on en a bseoin dans backprop
            return Math.Exp(-x) / Math.Pow(1 + Math.Exp(-x), 2);
        }      
        
        public void ResetSortie()
        {
            sortie = 0;
        }

        public void CalculeSortie(double[,] tabpoids)
        {
            int i;
            double w;
            Neurone neuronepred;
            // A remplir
            somme = 0;
            for (i = 0; i < entrees.Count; i++)
            {
                neuronepred = entrees[i];
                w = tabpoids[neuronepred.numero, numero];
                somme = somme + w * neuronepred.GetSortie();
            }
            sortie = Sigmoide(somme);
        }
        public void Setdelta(double d)
        {
            delta = d;
        }
        public double Getdelta() { return delta; }

        public void ImposeSortie(double s) { sortie = s; }

        public void ModifiePoids(Observation obs, double alpha)
        {
            for (int i = 0; i < poids.Count; i++)
                poids[i] = poids[i] - alpha * (poids[i] - obs.GetValue(i));
        }

        public double DistInterNeurone(Neurone n2)
        {
            double dist = 0;
            for (int i = 0; i < poids.Count; i++)
            {
                dist = dist + (poids[i] - n2.poids[i]) * (poids[i] - n2.poids[i]);
            }
            return Math.Sqrt(dist);
        }

        public double CalculeErreur(Observation obs)
        {
            double somme = 0;
            for (int i = 0; i < poids.Count; i++)
                somme = somme + (poids[i] - obs.GetValue(i))
                               * (poids[i] - obs.GetValue(i));
            return Math.Sqrt(somme);
        }
    }
    
}


using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsApplication1
{
    public class Neurone
    {
        private List<double> poids;

        public Neurone( int nbpoids, int valeurmax)
        {
            poids = new List<double>();
            for (int i=0; i<nbpoids; i++)
                poids.Add( Form1.random.NextDouble()*valeurmax);
        }
        public double GetPoids( int i)
        { return poids[i]; }

       
       public double CalculeErreur( Observation obs )
       {
           double somme = 0;
           for (int i = 0; i < poids.Count; i++)
               somme = somme + (poids[i] - obs.GetValue(i))
                              * (poids[i] - obs.GetValue(i));
           return Math.Sqrt(somme);
       }

       public void ModifiePoids( Observation obs, double alpha)
        {
            for (int i=0; i< poids.Count; i++)
                poids[i]=poids[i]-alpha*(poids[i]-obs.GetValue(i));
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
    }
}

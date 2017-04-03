using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsApplication1
{
    public class Classe
    {
        private List<Neurone> ListNeurones = new List<Neurone>();

        public Classe( Neurone neurone)
        {
            ListNeurones.Add(neurone);
        }
        public List<Neurone> GetNeurones() { return ListNeurones; }

        public void FusionAvec(Classe c2)
        {
            foreach (Neurone n in c2.GetNeurones())
                ListNeurones.Add(n);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Classification
{
    class Perceptron
    {
        public double[,] inputs = new double[3000, 3];

        public double[,] outputs = new double[3000, 1];

        private double maxInputs;
        private double minInputs;
        Random rnd;

        public Perceptron()
        {

        }

        public void InitialiseInputs()
        {
            maxInputs = 0;
            minInputs = 1000; // valeur max = 785,...
           
            using(StreamReader sr = new StreamReader("datasetclassif.txt"))
            {
                for (int i = 0; i < inputs.GetLength(0); i++)
                {
                    for (int j=0; j < inputs.GetLength(1); j++)
                    {
                        // Read the stream to a string, and write the string to the console.
                        inputs[i,j] = Convert.ToDouble(sr.ReadLine());
                        if (j!=0 && inputs[i, j] > maxInputs) maxInputs = inputs[i, j];
                        if (j != 0 && inputs[i, j] < minInputs) minInputs = inputs[i, j];
                    }
                }
                sr.Close();
                RandomizeInputs();
            }
            
        } 

        public void InitialiseOutputsSupervise()
        {
            for(int i =0; i < outputs.GetLength(0); i++)
            {
                if (inputs[i,0]<1500) outputs[i, 0] = 0.9;
                else outputs[i, 0] = 0.1;
            }
        }

        public void NormaliseEntrees()
        {
            for (int i = 0; i < inputs.GetLength(0); i++)
            {
                for (int j = 1; j < inputs.GetLength(1); j++)
                {
                    inputs[i, j] = inputs[i, j].Remap(minInputs, maxInputs, 0, 1);
                }
            }

        }

        private void RandomizeInputs()
        {
            rnd = new Random();
            int k = 0;
            foreach (int i in Enumerable.Range(0, 3000).OrderBy(x => rnd.Next()))
            {
                for(int j = 0; j < 3; j++)
                {
                    inputs[k, j] = inputs[i, j];
                }
                k++;
            }
        }

    }

    public static class ExtensionMethods
    {
        // Map des valeurs d'un interval sur un autre
        public static double Remap(this double value, double from1, double to1, double from2, double to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

    }
}

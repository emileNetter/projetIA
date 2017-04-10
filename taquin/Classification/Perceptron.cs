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

        public double maxInputs;

        public Perceptron()
        {

        }

        public void InitialiseInputs()
        {
            maxInputs = 0;
           
            using(StreamReader sr = new StreamReader("datasetclassif.txt"))
            {
                for(int i =0; i < inputs.GetLength(0); i++)
                {
                    for (int j=0; j < inputs.GetLength(1); j++)
                    {
                        // Read the stream to a string, and write the string to the console.
                        inputs[i,j] = Convert.ToDouble(sr.ReadLine());
                        if (j!=0 && inputs[i, j] > maxInputs) maxInputs = inputs[i, j];
                    }
                }
                sr.Close();
            }
            
        } 

        public void InitialiseOutputsSupervise()
        {
            for(int i =0; i < outputs.GetLength(0); i++)
            {
                if (i < 1500) outputs[i, 0] = 0.9;
                else outputs[i, 0] = 0.1;
            }
        }

        public void Supervise()
        {

        }

        public void NormaliseEntrees()
        {
            for (int i = 0; i < inputs.GetLength(0); i++)
            {
                for (int j = 1; j < inputs.GetLength(1); j++)
                {
                    inputs[i, j] = inputs[i, j].Remap(0, maxInputs, 0, 1);
                }
            }
        }

    }

    public static class ExtensionMethods
    {

        public static double Remap(this double value, double from1, double to1, double from2, double to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

    }
}

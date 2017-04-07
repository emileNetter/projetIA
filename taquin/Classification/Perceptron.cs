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

        public Perceptron()
        {

        }

        public void InitialiseInputs()
        {
            using(StreamReader sr = new StreamReader("datasetclassif.txt"))
            {
                for(int i =0; i < inputs.GetLength(0); i++)
                {
                    for (int j=0; j < inputs.GetLength(1); j++)
                    {
                        // Read the stream to a string, and write the string to the console.
                        inputs[i,j] = Convert.ToDouble(sr.ReadLine());
                    }
                }
                sr.Close();
            }
            
        } 

        public void InitialiseOutputsSupervise()
        {
            for(int i =0; i < outputs.GetLength(0); i++)
            {
                if (i < 1500) outputs[i, 0] = 1;
                else outputs[i, 0] = 0;
            }
        }

        public void Supervise()
        {

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Classification
{
    class Perceptron
    {
        private double[,] inputs = new double[3000, 3];

        private void readFromFile()
        {
            using(StreamReader sr = new StreamReader("datasetclassif.txt"))
            {
                for(int i =0; i < inputs.GetLength(0); i++)
                {
                    for (int j=0; j < inputs.GetLength(1); j++)
                    {
                        // Read the stream to a string, and write the string to the console.
                        inputs[i,j] = Convert.ToDouble(sr.ReadToEnd());                      
                    }
                }               
            }
        } 

    }
}

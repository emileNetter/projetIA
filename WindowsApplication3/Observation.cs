using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsApplication1
{
    public class Observation
    {
        private List<double> e;

        public Observation(double newx, double newy)
        {
            e = new List<double>();
            e.Add(newx);
            e.Add(newy);
        }

        public double Getx()
        { return e[0]; }

        public double Gety()
        { return e[1]; }

        public double GetValue(int i)
        {
            return e[i];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlobImageJava
{
    public class BlobImageJava
    {
        public int xMin;
        public int xMax;
        public int yMin;
        public int yMax;
        public int mass;

        public BlobImageJava(int xMin, int xMax, int yMin, int yMax, int mass)
        {
            this.xMin = xMin;
            this.xMax = xMax;
            this.yMin = yMin;
            this.yMax = yMax;
            this.mass = mass;
        }

        public String toString()
        {
            return String.Format("X: %4d -> %4d, Y: %4d -> %4d, mass: %6d", xMin, xMax, yMin, yMax, mass);
        }
    }
}

using System;

namespace Geometry
{
    class SquareShape : Shape
    {
        private double sideLength;

        public SquareShape(double sideLength)
        {
            this.sideLength = sideLength;
        }

        public override double Square()
        {
            return sideLength * sideLength;
        }
    }
}
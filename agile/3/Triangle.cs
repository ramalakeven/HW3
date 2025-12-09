using System;

namespace Geometry
{
    class TriangleShape : Shape
    {
        private double baseLength;
        private double height;

        public TriangleShape(double baseLength, double height)
        {
            this.baseLength = baseLength;
            this.height = height;
        }

        public override double Square()
        {
            return 0.5 * baseLength * height;
        }
    }
}

using System;

namespace Geometry 
{
    class Program
    {
        static void Main()
        {
            Console.Write("Введите сторону квадрата: ");
            double squareSide = double.Parse(Console.ReadLine());

            Console.Write("Введите основание треугольника: ");
            double triangleBase = double.Parse(Console.ReadLine());

            Console.Write("Введите высоту треугольника: ");
            double triangleHeight = double.Parse(Console.ReadLine());

            Shape square = new SquareShape(squareSide);
            Shape triangle = new TriangleShape(triangleBase, triangleHeight);

            Console.WriteLine($"\nПлощадь квадрата = {square.Square()}");
            Console.WriteLine($"Площадь треугольника = {triangle.Square()}");
        }
    }
}







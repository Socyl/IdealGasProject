using System;

namespace IdealGasProject
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCelsiusToKelvin();
            
        }
        static double CelsiusToKelvin(double celsius)
        {
            return celsius + 273.15;
        }

        static void TestCelsiusToKelvin()
        {
            double kelvin = CelsiusToKelvin(0.0);
            Console.WriteLine($"CelsiusToKelvin => {kelvin}");
        }
    }
}

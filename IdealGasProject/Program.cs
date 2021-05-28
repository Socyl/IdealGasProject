using System;

namespace IdealGasProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //VARIABLE DECLARATIONS/INITS
            int gasCount;
            string[] gasNames = new string[100];
            double[] moleWeights = new double[100];


            //MAIN 

            DisplayHeader();
            GetMolecularWeights(ref gasNames, ref moleWeights, out gasCount);
            DisplayGasNames(gasNames, gasCount);



            //TESTS



        }

        static void DisplayHeader()
        {
            //DISPLAYS PROGRAM HEADER
            Console.WriteLine("B. Corbitt");
            Console.WriteLine("Ideal Gas Calculator");
            Console.WriteLine("This program calculates pressure exerted by a gas in a container given the following inputs:");
            Console.WriteLine("\tName of the gas");
            Console.WriteLine("\tVolume of the container (in cubic meters");
            Console.WriteLine("\tWeight of the gas (in grams)");
            Console.WriteLine("\tTemperature of the gas (celsius)");
            Console.WriteLine();
            Console.WriteLine();
        }

        static void GetMolecularWeights(ref string[] gasNames, ref double[] moleWeights, out int counter)
        {
            counter = 0;  //elements in array
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@".\MolecularWeightsGasesAndVapors.csv");    //init new Streamreader

            file.ReadLine();                                                            //read and throw away the header line
            while ((line = file.ReadLine()) != null)                                    //fill arrays gasNames[] and moleWeights[]
            {
                string[] result = line.Split(",");
                gasNames[counter] = result[0];
                moleWeights[counter] = Convert.ToDouble(result[1]);
                counter++;
            }
            file.Close();
        }

        private static void DisplayGasNames(string[] gasNames, int countGases)
        {
            for (int i = 0; i < countGases; i++)
            {
                System.Console.Write("{0,-20}",gasNames[i]);
                if ((i + 1) % 3 == 0)
                {
                    System.Console.WriteLine();
                }
            }
        }

        static double CelsiusToKelvin(double celsius)
        {
            return celsius + 273.15;
        }
    }
}

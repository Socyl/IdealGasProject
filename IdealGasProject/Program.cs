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


            //MAIN SECTION
            DisplayHeader();
            GetMolecularWeights(ref gasNames, ref moleWeights, out gasCount);
            DisplayGasNames(gasNames, gasCount);

            //TESTS  
            Console.WriteLine(GetMolecularWeightFromName("Isobutane", gasNames, moleWeights, gasCount));
        }

        static void DisplayHeader()
        {
            //DISPLAYS PROGRAM HEADER
            Console.WriteLine("B. C.");
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
            //READS CSV FILE AND POPULATES ARRAYS FOR NAMES/MOL.WTS
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
            //DISPLAYS GAS NAMES IN 3 COLUMNS
            for (int i = 0; i < countGases; i++)
            {
                System.Console.Write("{0,-20}",gasNames[i]);
                if ((i + 1) % 3 == 0)                               //line feed after every third item 
                {
                    System.Console.WriteLine();
                }
            }
        }
        private static double GetMolecularWeightFromName(string gasName, string[] gasNames, double[] molecularWeights, int countGases)
        {
            //GETS MOLECULAR WEIGHT FROM GAS NAME.  RETURNS 0 IF NAME NOT FOUND          
            double response = 0;
            for (int i = 0; i < countGases; i++)
            {
                if (gasName.Equals(gasNames[i]))
                {
                    response = molecularWeights[i];
                }
            }
            return response;
        }



       private static double CelsiusToKelvin(double celsius)
        {
            //CONVERTS CELSIUS TO KELVIN
            return celsius + 273.15;
        }
    }
}

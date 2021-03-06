//CIS1280 P1
//IDEAL GAS CALCULATOR

using System;

namespace IdealGasProject
{
    class Program
    {
        public const double R=8.3145;  //declare constant for value of R

        static void Main(string[] args)
        {

            //VARIABLE DECLARATIONS/INITS
            int gasCount;
            string another = "no";
            string[] gasNames = new string[100];
            double[] moleWeights = new double[100];
            double molecularWeight;  //molecular weight of user selected gas
            double gasVolume;   //user input volume in m^3
            double gasMass;     //user input mass in g
            double gasTemp;     //user input temp in C
            string gasSelection;  //user gas selection entry
            double gasPressure;   //calculated pressure in Pa

          


            //MAIN SECTION
            DisplayHeader();  //not required but already written by time document changed so left as is
            GetMolecularWeights(ref gasNames, ref moleWeights, out gasCount);  //fill arrays & get element count
            DisplayGasNames(gasNames, gasCount);                               //display gases to user
            do
            {
                //get gas name from user
                Console.WriteLine("Please type in a gas name from the list above (must be exact match): ");
                gasSelection = Console.ReadLine();
                //get the molecular weight (and count of elements)
                molecularWeight = GetMolecularWeightFromName(gasSelection, gasNames, moleWeights, gasCount);
                if (molecularWeight != 0)
                {
                    // GLENN: (suggestion) Be a little careful here, comparing == with doubles is hazardous.
                    // Doubles can sometimes be slightly inaccurate, especially when division is involved.
                    // A more fail-safe way to do this would be to return either -1 on failure and check if < 0,
                    // or return NaN and check for NaN on failure.
                    // In this case, the code works, but just be aware of this for the future.

                     //if gas is found:
                     //get volume from user
                    Console.WriteLine("Please input the volume of the gas in cubic meters: ");
                    gasVolume = Convert.ToDouble(Console.ReadLine());
                    //get mass from user
                    Console.WriteLine("Please input the mass of the gas in grams: ");
                    gasMass = Convert.ToDouble(Console.ReadLine());
                    //get temp from user
                    Console.WriteLine("Please input the temperature of the gas in Celsius: ");
                    gasTemp = Convert.ToDouble(Console.ReadLine());
                    //calculate and display pressure
                    gasPressure = Pressure(gasMass, gasVolume, gasTemp, molecularWeight);
                    DisplayPressure(gasPressure);   
                }
                else
                {
                    //gas not found
                    Console.WriteLine("Gas Name not found!  Please make sure you typed the name correctly.\n"); 
                }
               

                //ask for another?
                Console.WriteLine("Would you like to calculate another pressure? (enter yes to continue): ");
                another =Console.ReadLine();

            } while (another.ToLower() == "yes");  //ignore case on input (small attempt at validation)
            // GLENN: Be careful, use String.Equals(another.ToLower(), "yes") instead.

            //exit message
            Console.WriteLine("\n\nHave a great day!  Goodbye!\n\n");
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

        static void GetMolecularWeights(ref string[] gasNames, ref double[] molecularWeights, out int count)
        {
            //READS CSV FILE AND POPULATES ARRAYS FOR NAMES/MOL.WTS
            count = 0;  //elements in array
            string line;

            // GLENN: (suggestion) use using with IDisposable types
            // We haven't talked about this, but to ensure your file always always gets closed, you can use this construct
            // called using.
            //
            // using(StreamReader file = new StreamReader(...)) {
            //    // read the file etc
            // }
            //
            // When the code exits this block, it will automagically call Dispose() on the file (which also closes the file).

            // GLENN: (suggestion) If you put a using System.IO at the top, you can just say StreamReader file = ...
            System.IO.StreamReader file = new System.IO.StreamReader(@".\MolecularWeightsGasesAndVapors.csv");    //init new Streamreader

            file.ReadLine();                                                            //read and throw away the header line
            while ((line = file.ReadLine()) != null)                                    //fill arrays gasNames[] and moleWeights[]
            {
                string[] result = line.Split(",");
                gasNames[count] = result[0];
                molecularWeights[count] = Convert.ToDouble(result[1]);
                count++;
            }
            file.Close();
        }

        private static void DisplayGasNames(string[] gasNames, int countGases)
        {
            //DISPLAYS GAS NAMES IN 3 COLUMNS
            for (int i = 0; i < countGases; i++)
            {
                System.Console.Write("{0,-20}", gasNames[i]);
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

        static double Pressure(double mass, double vol, double temp, double molecularWeight)
        {
            double numMoles = NumberOfMoles(mass, molecularWeight);     //get n
            double tempKelvin = CelsiusToKelvin(temp);                  //get temp in K
            return (numMoles * tempKelvin * R) / vol;                   //return Pressure in Pa
        }

        static double NumberOfMoles(double mass, double molecularWeight)
        {
            return mass / molecularWeight;
        }

        private static void DisplayPressure(double pressure)
        {
            Console.WriteLine("\nThe pressure is {0} Pascals, which is {1} PSI.\n", pressure, PaToPSI(pressure));
        }

        static double PaToPSI(double pascals)
        {
            return pascals * 0.0001450377;  
            //conversion factor taken from www.unitconverters.net/pressure/pascal-to-psi.htm
        }


        private static double CelsiusToKelvin(double celsius)
        {
            //CONVERTS CELSIUS TO KELVIN
            return celsius + 273.15;
        }
    }
}

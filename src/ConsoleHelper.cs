using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CarCatalog.Domain;

namespace CarCatalog
{
    internal sealed class ConsoleHelper
    {
        #region Public methods
        public static void HelloMessage()
        {
            Console.WriteLine("Car catalog system is starting...");
            Wait();
        }

        public static string RequestCarSelection(string[] carNames)
        {
            Console.Write("Our system contains information about\r\nthe following users:\r\n {0}.\r\n" +
                          "Please select car name to retriew an information\r\n(type exit for quit):", carNames.Aggregate((sum, item) => sum + "; " + item));
            
            var input = Console.ReadLine();
            EmptyLine();

            return input;
        }

        public static void ByeMessage()
        {
            Console.WriteLine("Exiting..");
            Wait();
        }

        public static string RequestIfNeedAdditionalInfo()
        {
            Console.Write("Would you like to knot additional information? (type yes)");

            var input = Console.ReadLine();
            EmptyLine();

            return input;
        }

        public static void PrintCarInformation(CarInformation carInfo)
        {
            Console.WriteLine("Car information\r\nName: {0};\r\nMileage: {1}\r\nOwner: {2}\r\nCurrent position: {3}", carInfo.Name, carInfo.Mileage, carInfo.OwnerName, carInfo.CarCurrentPosition ?? "Exception thrown.");
            EmptyLine();

            Wait();
        }

        public static void PressAnyKeyToContinue()
        {
            Console.WriteLine("Please press any key to continue...");
            Console.ReadKey();
        }

        public static void PrintCarAdditionalInformation(AdditionalInformation additionalInfo)
        {
            Console.WriteLine("Additional information\r\nBrand: {0};\r\nDescription:\r\n{1}", additionalInfo.Brand, additionalInfo.Description);
            EmptyLine();

            Wait();
        }

        public static void CarInformationDoNotExists()
        {
            Console.WriteLine("Car information do not exists");
            EmptyLine();

            Wait();
        }

        #endregion

        #region Helper members
        private static void Wait()
        {
            Thread.Sleep(500);
        }

        private static void EmptyLine()
        {
            Console.WriteLine();
        }

        #endregion
    }
}

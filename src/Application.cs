using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CarCatalog.Domain;
using CarCatalog.Services;

namespace CarCatalog
{
    public class Application
    {
        #region Fields
        private ICarFinder CarFinder { get; set; }
        private readonly Random _random = new Random();
        #endregion

        #region Constructors
        public Application(ICarFinder carFinder)
        {
            CarFinder = carFinder;
        }
        #endregion

        public void Run()
        {
            ConsoleHelper.HelloMessage();

            while (true)
            {
                IDictionary<string, Guid> minInfos = CarFinder.GetCarMinInformations();

                //--//
                string input = ConsoleHelper.RequestCarSelection(minInfos.Keys.ToArray()).ToLowerInvariant();
                if (input == "exit")
                {
                    ConsoleHelper.ByeMessage();
                    break;
                }

                if (!minInfos.Keys.Contains(input))
                {
                    ConsoleHelper.CarInformationDoNotExists();
                    continue;
                }

                //Take car here (do not actual get from database)
                Lazy<CarInformation> carInformation = CarFinder.GetCarInformation(minInfos[input]);

                //Check if car exists without retrieving actual data
                if (carInformation == null)
                {
                    ConsoleHelper.CarInformationDoNotExists();
                }
                else
                {
                    void SetCarCurrentPosition()
                    {
                        //carInformation.Value throws exception when to run on another thread
                        carInformation.Value.CarCurrentPosition = _random.Next(0, 100).ToString();
                    }

                    //SetCarCurrentPosition();
                    RunFromAnotherThread(SetCarCurrentPosition);
                                       
                    //Do not throw exception because of LazyThreadSafetyMode.PublicationOnly setted for Lazy<CarInformation> in CarsFinder.
                    CarInformation car = carInformation.GetValueProgressBarAware();
                    ConsoleHelper.PrintCarInformation(car);

                    //Check if car has additional info without retrieving actual additional info data
                    if (car.AdditionalInfo != null)
                    {
                        input = ConsoleHelper.RequestIfNeedAdditionalInfo();
                        if (input.ToLowerInvariant() == "yes")
                        {
                            ConsoleHelper.PrintCarAdditionalInformation(car.AdditionalInfo.GetValueProgressBarAware());
                        }
                    }
                }

                ConsoleHelper.PressAnyKeyToContinue();
            }
        }

        private void RunFromAnotherThread(Action codeToRun)
        {
            var thread = new Thread(() =>
                            {
                                Thread.Sleep(300);

                                try
                                {
                                    codeToRun();
                                }
                                catch {}
                            });

            thread.Start();

            while (thread.IsAlive)
            {
                Thread.Sleep(10);
            }
        }
    }
}

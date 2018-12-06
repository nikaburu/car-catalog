using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CarCatalog.Domain;

namespace CarCatalog.Dal
{
    internal sealed class CarDatabase
    {
        #region Fields
        private readonly IEnumerable<CarInformation> _carInformations;
        #endregion

        #region Constructors
        public CarDatabase()
        {
            _carInformations = DefaultCarInfos();
        }

        #endregion

        #region Properties
        /// <summary>
        /// Retrieve car informations with time delay
        /// </summary>
        public IEnumerable<CarInformation> CarInformations
        {
            get
            {
                //Emulate long lime request
                Thread.Sleep(1500);
                return _carInformations;
            }
        }
        #endregion

        #region Private members
        private IEnumerable<CarInformation> DefaultCarInfos()
        {
            return new List<CarInformation>
                       {
                           new CarInformation()
                               {
                                   CarId = Guid.NewGuid(),
                                   Mileage = 14323,
                                   Name = "Red diablo",
                                   OwnerName = "Iva Grue",
                                   AdditionalInfo = new Lazy<AdditionalInformation>(() =>
                                                                                        {
                                                                                            //Emulate long lime request
                                                                                            Thread.Sleep(1500);
                                                                                            return new AdditionalInformation
                                                                                                       {
                                                                                                           Brand = "BMV",
                                                                                                           Description =
                                                                                                               "Typically, BMW introduces many of their innovations first in the 7 Series, such as the somewhat controversial iDrive system."
                                                                                                       };
                                                                                        })
                               },
                               new CarInformation()
                               {
                                   CarId = Guid.NewGuid(),
                                   Mileage = 5433,
                                   Name = "Parrot",
                                   OwnerName = "John I.",
                                   AdditionalInfo = new Lazy<AdditionalInformation>(() =>
                                                                                        {
                                                                                            //Emulate long lime request
                                                                                            Thread.Sleep(1500);
                                                                                            return new AdditionalInformation
                                                                                                       {
                                                                                                           Brand =
                                                                                                               "Toyota",
                                                                                                           Description =
                                                                                                               "Toyota established the Toyota Technological Institute in 1981, as Sakichi Toyoda had planned to establish a university as soon as he and Toyota became successful."
                                                                                                       };
                                                                                        })
                               },
                               new CarInformation()
                               {
                                   CarId = Guid.NewGuid(),
                                   Mileage = 565,
                                   Name = "Lazy car",
                                   OwnerName = "Craig V.",
                               }
                       };
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CarCatalog.Domain;
using CarCatalog.Services;

namespace CarCatalog.Dal
{
    class CarsFinder : ICarFinder
    {
        #region Fields
        private readonly CarDatabase _dataBase;

        // Lazy<T> helps to make truly thread safe and lazy loaded implementation of Singleton pattern
        // Also we don't need empty static constructor because of http://www.yoda.arachsys.com/csharp/beforefieldinit.html
        private static readonly Lazy<ICarFinder> _instance = new Lazy<ICarFinder>(() => new CarsFinder(new CarDatabase()));
        #endregion

        #region Properties
        public static ICarFinder Instance { get { return _instance.Value; } }
        #endregion

        #region Constructors
        private CarsFinder()
        {

        }

        private CarsFinder(CarDatabase dataBase)
        {
            _dataBase = dataBase;
        }
        #endregion

        #region Implementation of ICarFinder

        public IDictionary<string, Guid> GetCarMinInformations()
        {
            return _dataBase.CarInformations.ToDictionary(car => car.Name.ToLowerInvariant(), car => car.CarId);
        }

        /// <summary>
        /// Returns and Lazy car information that throw exception if .Value called from no another thread. 
        /// LazyThreadSafetyMode.PublicationOnly - says do not cache exception from another thread.
        /// </summary>
        /// <param name="carId">Car's information id</param>
        /// <returns></returns>
        public Lazy<CarInformation> GetCarInformation(Guid carId)
        {
            var creationTreadId = Thread.CurrentThread.ManagedThreadId;
            return new Lazy<CarInformation>(() =>
                                                {
                                                    if (creationTreadId != Thread.CurrentThread.ManagedThreadId)
                                                    {
                                                        throw new Exception("Called from another thread!");
                                                    }
                                                    
                                                    return _dataBase.CarInformations.FirstOrDefault(car => car.CarId == carId);
                                                //});
                                                }, LazyThreadSafetyMode.PublicationOnly);
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using CarCatalog.Domain;

namespace CarCatalog.Services
{
    public interface ICarFinder
    {
        /// <summary>
        /// Retrieves cars information list
        /// </summary>
        /// <returns>IDictionary: key - lower invariant car name, value - car id</returns>
        IDictionary<string, Guid> GetCarMinInformations();

        /// <summary>
        /// Returns car information by car id
        /// </summary>
        /// <param name="carId">Car id</param>
        /// <returns></returns>
        Lazy<CarInformation> GetCarInformation(Guid carId);
    }
}

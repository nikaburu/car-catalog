using System;

namespace CarCatalog.Domain
{
    public sealed class CarInformation
    {
        public Guid CarId { get; set; }

        public string Name { get; set; }
        public int Mileage { get; set; }
        public string OwnerName { get; set; }
        public string CarCurrentPosition { get; set; }

        public Lazy<AdditionalInformation> AdditionalInfo { get; set; }
    }
}

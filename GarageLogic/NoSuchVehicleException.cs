using System;

namespace GarageLogic
{
    public class NoSuchVehicleException : Exception
    {
        private const string k_NoSuchVehicleExceptionMessage = "Could not find a matching vehicle";
        public NoSuchVehicleException() : base(k_NoSuchVehicleExceptionMessage) { }
    }
}

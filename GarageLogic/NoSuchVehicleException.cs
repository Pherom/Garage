using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class NoSuchVehicleException : Exception
    {
        private const string k_NoSuchVehicleExceptionMessage = "Could not find a matching vehicle";
        public NoSuchVehicleException() : base(k_NoSuchVehicleExceptionMessage) { }
    }
}

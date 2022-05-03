using System;

namespace Engine
{
    public class VehicleUtils
    {
        private enum eTypeOfVehicle
        {
            REGULAR_MOTORBIKE,
            ELECTRIC_MOTORBIKE,

        }
        private const float k_MaxFuelInLitersForRegularMotorbike = 1;
        private const float k_MaxEnergyInHoursForElectricMotorbike = 2.5f;
        private const float k_MaxFuelInLitersForRegularCar = 1;
        private const float k_MaxEnergyInHoursForElectricCar = 3.3f;
        private const float k_MaxFuelInLitersForTruck = 1;

        private const string k_InvalidLicensePlateExceptionMessage = "License plate number must consists of 7-8 numbers only";
        private VehicleUtils() { }
        public static void validateLicensePlate(string i_LicensePlateNumber)
        {
            if (i_LicensePlateNumber == null)
            {
                throw new ArgumentNullException(nameof(i_LicensePlateNumber));
            }

            if (checkIfStringConsistsOfDigitsOnly(i_LicensePlateNumber) && (i_LicensePlateNumber.Length == 7 || i_LicensePlateNumber.Length == 8))
            {
                throw new FormatException(k_InvalidLicensePlateExceptionMessage);
            }
        }

        private static bool checkIfStringConsistsOfDigitsOnly(string i_LicensePlate)
        {
            foreach (char currentChar in i_LicensePlate)
            {
                if (currentChar < '0' || currentChar > '9')
                    return false;
            }

            return true;
        }

        public static void validateEnergyForSpecificVehicle(float i_Energy, )
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Engine
{
    public class Garage
    {
        private const string k_ExpectedGasolineFueledVehicleLicensePlateNumberExceptionMessage = "The provided license plate number does not match with a gasoline fueled vehicle";
        private const string k_ExpectedElectricVehicleLicensePlateNumberExceptionMessage = "The provided license plate number does not match with an electric vehicle";
        private const string k_NonMatchingFuelTypeExceptionMessage = "The provided fuel type does not match the vehicle's required fuel type";
        private readonly List<Vehicle> m_Vehicles = new List<Vehicle>();
        private readonly List<VehicleFactory.VehicleTypeStruct> r_VehicleTypesList;

        public ReadOnlyCollection<VehicleFactory.VehicleTypeStruct> VehicleTypesList
        {
            get
            {
                return r_VehicleTypesList.AsReadOnly();
            }
        }

        public Garage()
        {
            r_VehicleTypesList = VehicleUtils.initVehicleTypeListFromClassVehicleTypes();
        }

        public Vehicle getVehicleByLicensePlateNumber(string i_LicensePlateNumber)
        {
            Vehicle foundVehicle = null;

            foreach (Vehicle vehicle in m_Vehicles)
            {
                if (vehicle.LicensePlateNumber == i_LicensePlateNumber)
                {
                    foundVehicle = vehicle;
                    break;
                }
            }

            if (foundVehicle == null)
            {
                throw new NoSuchVehicleException();
            }

            return foundVehicle;
        }
        
        private Vehicle getOrAddVehicle(Vehicle i_Vehicle)
        {
            Vehicle foundOrAddedVehicle = null;

            foreach (Vehicle vehicle in m_Vehicles)
            {
                if (vehicle == i_Vehicle)
                {
                    foundOrAddedVehicle = vehicle;
                    break;
                }
            }

            if (foundOrAddedVehicle == null)
            {
                foundOrAddedVehicle = i_Vehicle;
                m_Vehicles.Add(foundOrAddedVehicle);
            }

            return foundOrAddedVehicle;
        }

        public Vehicle AddVehicle(Vehicle i_Vehicle)
        {
            Vehicle addedVehicle;

            if (i_Vehicle == null)
            {
                throw new ArgumentNullException(nameof(i_Vehicle));
            }

            addedVehicle = getOrAddVehicle(i_Vehicle);
            addedVehicle.RepairStatus = Vehicle.eRepairStatus.IN_PROGRESS;
            return addedVehicle;
        }

        public List<string> GetLicensePlateNumbers()
        {
            List<string> licensePlateNumbers = new List<string>();

            foreach (Vehicle vehicle in m_Vehicles)
            {
                licensePlateNumbers.Add(vehicle.LicensePlateNumber);
            }

            return licensePlateNumbers;
        }

        public List<string> GetLicensePlateNumbersFilteredByRepairStatus(Vehicle.eRepairStatus i_RepairStatus)
        {
            List<string> filteredLicensePlateNumbers = new List<string>();

            foreach (Vehicle vehicle in m_Vehicles)
            {
                if (vehicle.RepairStatus == i_RepairStatus)
                {
                    filteredLicensePlateNumbers.Add(vehicle.LicensePlateNumber);
                }
            }

            return filteredLicensePlateNumbers;
        }

        public void SetVehicleRepairStatus(string i_LicensePlateNumber, Vehicle.eRepairStatus i_NewRepairStatus)
        {
            if (i_LicensePlateNumber == null)
            {
                throw new ArgumentNullException(nameof(i_LicensePlateNumber));
            }
            getVehicleByLicensePlateNumber(i_LicensePlateNumber).RepairStatus = i_NewRepairStatus;
        }

        public void InflateVehicleTiresToMaximumPressure(string i_LicensePlateNumber)
        {
            if (i_LicensePlateNumber == null)
            {
                throw new ArgumentNullException(i_LicensePlateNumber);
            }
            getVehicleByLicensePlateNumber(i_LicensePlateNumber).InflateAllTiresToMaximumPressure();
        }

        public void FuelVehicle(string i_LicensePlateNumber, GasolineFueledVehicle.eFuelType i_FuelType, float i_AmountInLiters)
        {
            GasolineFueledVehicle gasolineFueledVehicle;

            if (i_LicensePlateNumber == null)
            {
                throw new ArgumentNullException(nameof(i_LicensePlateNumber));
            }

            gasolineFueledVehicle = getVehicleByLicensePlateNumber(i_LicensePlateNumber) as GasolineFueledVehicle;

            if (gasolineFueledVehicle == null)
            {
                throw new ArgumentException(k_ExpectedGasolineFueledVehicleLicensePlateNumberExceptionMessage);
            }

            if (gasolineFueledVehicle.FuelType != i_FuelType)
            {
                throw new ArgumentException(k_NonMatchingFuelTypeExceptionMessage);
            }

            gasolineFueledVehicle.Fuel(i_AmountInLiters);
        }

        public void ChargeVehicle(string i_LicensePlateNumber, float i_AmountInMinutes)
        {
            ElectricVehicle electricVehicle;

            if (i_LicensePlateNumber == null)
            {
                throw new ArgumentNullException(nameof(i_LicensePlateNumber));
            }

            electricVehicle = getVehicleByLicensePlateNumber(i_LicensePlateNumber) as ElectricVehicle;

            if (electricVehicle == null)
            {
                throw new ArgumentException(k_ExpectedElectricVehicleLicensePlateNumberExceptionMessage);
            }

            electricVehicle.Charge(i_AmountInMinutes);
        }
    }
}

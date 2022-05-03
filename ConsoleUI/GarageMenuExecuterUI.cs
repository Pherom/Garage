using System;
using System.Collections.Generic;
using Engine;

namespace UI
{           

    internal class GarageMenuExecuterUI
    {
        Garage m_Garage = new Garage();
        VehicleFactory m_VehicleFactory = new VehicleFactory();
        VehicleTypeForm m_VehicleTypeForm = new VehicleTypeForm();
        LicensePlateForm m_LicensePlateForm = new LicensePlateForm();
        WheelsForm m_WheelsForm = new WheelsForm();
        RemainingEnergyOrFuelForm m_RemainingEnergyOrFuelForm = new RemainingEnergyOrFuelForm();

        public void Execute(GarageMenuForm.eGarageMenuOption i_OptionPicked)
        {
            switch (i_OptionPicked)
            {
                case GarageMenuForm.eGarageMenuOption.ADD_NEW_VEHICLE:
                   AddNewVehicle();
                   break;
                case GarageMenuForm.eGarageMenuOption.DISPLAY_LICENSE_PLATES_OF_ALL_VEHICLES:
                   DisplayLicensePlates();
                   break;
                case GarageMenuForm.eGarageMenuOption.CHANGE_REPAIR_STATUS_OF_SPECIFIC_VEHICLE:
                   ChangeRepairStatusOfSpecificVehicle();
                   break;
                case GarageMenuForm.eGarageMenuOption.FUEL_VEHICLE:
                   FuelVehicle();
                   break;
                case GarageMenuForm.eGarageMenuOption.INFLATE_VEHICLE_TIRES_TO_MAX:
                   InflateVehicleTirexToMax();
                   break;
                case GarageMenuForm.eGarageMenuOption.CHARGE_ELECTRIC_VEHICLE:
                   ChargeElectricVehicle();
                   break;
                case GarageMenuForm.eGarageMenuOption.DISPLAY_ALL_INFORMATION_ABOUT_VEHICLE:
                   DisplayAllInfoAboutSpecificVehicle();
                   break;
            }

            m_LicensePlateForm.ResetForm();
        }

        public void AddNewVehicle()
        {
            float remainingBatteryOrFuelTimeInHours = 0;
            bool isLocatedInGarageAlready = false;

            Console.WriteLine("--Add a new vehicle picked--");
            VehicleFactory.VehicleTypeStruct vehiclePicked = m_VehicleTypeForm.DisplayAndGetResult(m_Garage.VehicleTypesList);
            string licensePlate = m_LicensePlateForm.DisplayAndGetResult();

            try
            {
                Vehicle foundVehicle = m_Garage.getVehicleByLicensePlateNumber(licensePlate);
                Console.WriteLine(String.Format("A {0} was found with this license plate number: {1}"), 
                    foundVehicle.Specifications.VehicleType, foundVehicle.ModelName);
                m_Garage.SetVehicleRepairStatus(licensePlate, Vehicle.eRepairStatus.IN_PROGRESS);
            }
            catch (Exception ex)
            {
                List<Vehicle.Wheel> wheels = m_WheelsForm.DisplayAndGetResult(vehiclePicked);
                //remainingBatteryOrFuelTimeInHours = m_RemainingEnergyOrFuelForm.DisplayAndGetResult(vehiclePicked);

                // Call to factory add new vehicle
                //Vehicle vehicleToAdd = m_VehicleFactory.createCar(licensePlate);
                //getOrAddVehicle();
            }
        }
        public void DisplayLicensePlates()
        {
            Console.WriteLine("--Add a new vehicle picked--");
            Console.WriteLine("List of all vehicles currently in the garage:");
            //
        }
        public void ChangeRepairStatusOfSpecificVehicle()
        {
            Console.WriteLine("-- Changing the hange the repair status of a specific vehicle");
            Console.WriteLine("Please enter license plate ID");
        }

        public void FuelVehicle()
        {

        }
        public void InflateVehicleTirexToMax()
        {

        }
        public void ChargeElectricVehicle()
        {

        }
        public void DisplayAllInfoAboutSpecificVehicle()
        {

        }
    }
}

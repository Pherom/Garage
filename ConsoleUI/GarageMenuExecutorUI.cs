using System;
using System.Collections.Generic;
using GarageLogic;
using System.Reflection;
using System.Text;

namespace UI
{           

    internal class GarageMenuExecutorUI
    {
        private GarageMenuForm m_GarageMenuForm = new GarageMenuForm();
        private Garage m_Garage = new Garage();
        private VehicleFactory m_VehicleFactory = new VehicleFactory();
        private VehicleTypeForm m_VehicleTypeForm = new VehicleTypeForm();
        private LicensePlateForm m_LicensePlateForm = new LicensePlateForm();
        private WheelsForm m_WheelsForm = new WheelsForm();
        private RemainingEnergyOrFuelForm m_RemainingEnergyOrFuelForm = new RemainingEnergyOrFuelForm();
        private SpecificationForm m_SpecificationForm = new SpecificationForm();
        private OwnerDataForm m_OwnerDataForm = new OwnerDataForm();
        private EnumForm m_EnumForm = new EnumForm();
        private YesNoForm m_YesNoForm = new YesNoForm();

        public void Execute()
        {
            GarageMenuForm.eGarageMenuOption i_OptionPicked;
            bool runningMenu = true;
            while (runningMenu == true)
            {
                i_OptionPicked = m_GarageMenuForm.DisplayAndGetResult();
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
                    case GarageMenuForm.eGarageMenuOption.INFLATE_VEHICLE_TIRES_TO_MAX:
                       InflateVehicleTirexToMax();
                       break;
                    case GarageMenuForm.eGarageMenuOption.FUEL_VEHICLE:
                       FuelVehicle();
                       break;
                    case GarageMenuForm.eGarageMenuOption.CHARGE_ELECTRIC_VEHICLE:
                       ChargeElectricVehicle();
                       break;
                    case GarageMenuForm.eGarageMenuOption.DISPLAY_ALL_INFORMATION_ABOUT_VEHICLE:
                       DisplayAllInfoAboutSpecificVehicle();
                       break;
                    case GarageMenuForm.eGarageMenuOption.EXIT:
                       runningMenu = false;
                       break;
                }

                resetForms();
                Console.WriteLine();
            }
        }

        private void resetForms()
        {
            m_GarageMenuForm.ResetForm();
            m_LicensePlateForm.ResetForm();
            m_VehicleTypeForm.ResetForm();
            m_WheelsForm.ResetForm();
            m_RemainingEnergyOrFuelForm.ResetForm();
            m_SpecificationForm.ResetForm();
            m_OwnerDataForm.ResetForm();
            m_EnumForm.ResetForm();
            m_YesNoForm.ResetForm();
        }

        public void AddNewVehicle()
        {
            float remainingBatteryOrFuelTimeInHours = 0;
            string modelName;
            Console.WriteLine("--Add a new vehicle option picked--");
            VehicleFactory.VehicleTypeStruct vehiclePicked = m_VehicleTypeForm.DisplayAndGetResult(m_Garage.VehicleTypesList);
            string licensePlate = m_LicensePlateForm.DisplayAndGetResult();

            try
            {
                Vehicle foundVehicle = m_Garage.GetVehicleByLicensePlateNumber(licensePlate);
                Console.WriteLine(String.Format("A {0}({1}) was found with this license plate number: {2}{3}Changed this vehicle status to repair in progress", 
                    foundVehicle.ModelName, foundVehicle.Specifications.VehicleType, licensePlate, Environment.NewLine));
                m_Garage.SetVehicleRepairStatus(licensePlate, Vehicle.eRepairStatus.IN_PROGRESS);
            }
            catch
            {
                List<Vehicle.Wheel> wheels = m_WheelsForm.DisplayAndGetResult(vehiclePicked);
                Console.WriteLine("Enter model name:");
                modelName = Console.ReadLine();
                remainingBatteryOrFuelTimeInHours = m_RemainingEnergyOrFuelForm.DisplayAndGetResult(vehiclePicked);
                Specifications specifications = m_SpecificationForm.DisplayAndGetResult(vehiclePicked);
                Vehicle.VehicleOwnerData ownerData = m_OwnerDataForm.DisplayAndGetResult();
                // Call to factory add new vehicle
                Vehicle vehicleToBeAdded = m_VehicleFactory.CreateVehicle(vehiclePicked, modelName, licensePlate, wheels, ownerData, specifications, remainingBatteryOrFuelTimeInHours);
                m_Garage.AddVehicle(vehicleToBeAdded);
                Console.WriteLine("Successfully added a new vehicle");
            }
        }
        public void DisplayLicensePlates()
        {
            Vehicle.eRepairStatus? filterBy = null;
            bool repairStatusFilterInput;

            Console.WriteLine("-- Display license plates option picked --");
            repairStatusFilterInput = m_YesNoForm.DisplayAndGetResult("Would you like to filter by repair status? (y/n):");

            if (repairStatusFilterInput == true)
            {
                filterBy = (Vehicle.eRepairStatus)m_EnumForm.DisplayAndGetResult("Please enter repair status to filter by:", typeof(Vehicle.eRepairStatus).GetEnumValues());
            }

            Console.WriteLine("List of all license plates of vehicles currently in the garage:");
            foreach (string licensePlateNumber in m_Garage.GetLicensePlateNumbers())
            {
                if (repairStatusFilterInput == false || filterBy.Value == m_Garage.GetVehicleByLicensePlateNumber(licensePlateNumber).RepairStatus)
                {
                    Console.WriteLine(licensePlateNumber);
                }
            }
        }
        public void ChangeRepairStatusOfSpecificVehicle()
        {
            string licensePlateNumber;
            Vehicle.eRepairStatus repairStatus;

            Console.WriteLine("-- Change repair status of specific vehicle option picked --");
            licensePlateNumber = m_LicensePlateForm.DisplayAndGetResult();

            repairStatus = (Vehicle.eRepairStatus)m_EnumForm.DisplayAndGetResult("Please enter repair status to update to:", typeof(Vehicle.eRepairStatus).GetEnumValues());
            try
            {
                m_Garage.SetVehicleRepairStatus(licensePlateNumber, repairStatus);
                Console.WriteLine(String.Format("Successfully changed repair status to {0}", repairStatus.ToString()));
            }
            catch (NoSuchVehicleException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void FuelVehicle()
        {
            string licensePlateNumber;
            GasolineFueledVehicle.eFuelType fuelType;
            float fuelAmountInLiters;

            Console.WriteLine("-- Fuel vehicle option picked --");
            licensePlateNumber = m_LicensePlateForm.DisplayAndGetResult();
            fuelType = (GasolineFueledVehicle.eFuelType)m_EnumForm.DisplayAndGetResult("Please enter fuel type:", typeof(GasolineFueledVehicle.eFuelType).GetEnumValues());
            Console.WriteLine("Please enter amount of fuel in liters:");
            if (float.TryParse(Console.ReadLine(), out fuelAmountInLiters))
            {
                try
                {
                    m_Garage.FuelVehicle(licensePlateNumber, fuelType, fuelAmountInLiters);
                    Console.WriteLine("Succesfully added fuel to vehicle");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void InflateVehicleTirexToMax()
        {
            string licensePlateNumber;
            Console.WriteLine("-- Inflate vehicle tires to max picked --");
            licensePlateNumber = m_LicensePlateForm.DisplayAndGetResult();
            try
            {
                m_Garage.InflateVehicleTiresToMaximumPressure(licensePlateNumber);
                Console.WriteLine("Fully inflated wheels");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ChargeElectricVehicle()
        {
            string licensePlateNumber;
            float chargeAmountInMinutes;
            Console.WriteLine("-- Charge electric vehicle picked --");
            licensePlateNumber = m_LicensePlateForm.DisplayAndGetResult();
            Console.WriteLine("Please enter amount of minutes to charge:");
            if (float.TryParse(Console.ReadLine(), out chargeAmountInMinutes))
            {
                try
                {
                    m_Garage.ChargeVehicle(licensePlateNumber, chargeAmountInMinutes);
                    Console.WriteLine("Vehicle charged successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void DisplayAllInfoAboutSpecificVehicle()
        {
            string licensePlateNumber = m_LicensePlateForm.DisplayAndGetResult();
            try
            {
                Vehicle foundVehicle = m_Garage.GetVehicleByLicensePlateNumber(licensePlateNumber);
                Console.WriteLine(String.Format(@"---------
Vehicle type: {0}
Model name: {1}
Owner name: {2}
Repair status: {3}

Wheels:
{4}
Energy:
{5}

Specifications:
{6}
---------", foundVehicle.Specifications.VehicleType, foundVehicle.ModelName, foundVehicle.OwnerData.Name,
            foundVehicle.RepairStatus.ToString(), getWheelsInfoAsString(foundVehicle),
            getEnergySourceInfoAsString(foundVehicle), getSpecificiationsAsString(foundVehicle)
            ));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private string getWheelsInfoAsString(Vehicle i_Vehicle)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 1; i <= i_Vehicle.Wheels.Count; i++)
            {
                stringBuilder.Append(String.Format("Wheel #{0}:{1}Current tire pressure: {2}{1}Manufacturer: {3}{1}",
                    i, Environment.NewLine, i_Vehicle.Wheels[i - 1].CurrentTirePressure, i_Vehicle.Wheels[i - 1].ManufacturerName));
            }
            return stringBuilder.ToString();
        }
        private string getEnergySourceInfoAsString(Vehicle i_Vehicle)
        {
            GasolineFueledVehicle gasolineFueledVehicle = i_Vehicle as GasolineFueledVehicle;
            ElectricVehicle electricVehicle;
            string res;
            if (ReferenceEquals(gasolineFueledVehicle, null) == false)
            {
                res = String.Format("Fuel Type: {0}{1}Current fuel amount in liters: {2}",
                    gasolineFueledVehicle.FuelType.ToString(), Environment.NewLine, gasolineFueledVehicle.CurrentFuelAmountInLiters);
            }
            else
            {
                electricVehicle = i_Vehicle as ElectricVehicle;
                res = String.Format("Remaining battery time in hours: {0}", electricVehicle.RemainingBatteryTimeInHours);
            }

            return res;
        }

        private string getSpecificiationsAsString(Vehicle i_Vehicle)
        {
            StringBuilder res = new StringBuilder();
            Type currentSpecificationType = (i_Vehicle.Specifications).GetType();
            FieldInfo[] fieldsList = (FieldInfo[])currentSpecificationType.GetRuntimeFields();
            string nameOfField;
            object valueOfField;
            int lastIndexOfNewLine;

            foreach (FieldInfo currentField in fieldsList)
            {
                nameOfField = currentField.Name.Substring(2, currentField.Name.Length - 2);
                valueOfField = currentField.GetValue(i_Vehicle.Specifications).ToString();
                res.Append(string.Format("{0}: {1}{2}", nameOfField, valueOfField, Environment.NewLine));
            }

            lastIndexOfNewLine = res.ToString().LastIndexOf(Environment.NewLine);
            if (lastIndexOfNewLine >= 0)
            {
                res.Remove(lastIndexOfNewLine, res.Length - lastIndexOfNewLine);
            }

            return res.ToString();
        }
    }
}

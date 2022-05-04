using System;
using GarageLogic;

namespace UI
{
    internal class RemainingEnergyOrFuelForm
    {
        private const string k_InputIsNotANumberErrorMessage = "You must enter a number";
        private const string k_NoEnergyWasSetYetErrorMessage = "No energy was set yet";
        private float m_Input;
        private float? m_Result = null;

        public float Result
        {
            get
            {
                if (m_Result == null)
                {
                    throw new NullReferenceException(k_NoEnergyWasSetYetErrorMessage);
                }

                return m_Result.Value;
            }
        }

        public void Display(VehicleFactory.VehicleTypeStruct i_VehiclePicked)
        {
            bool validFloatEntered = false;
            float maxEnergy = getProperMaxBatteryOrFuel(i_VehiclePicked);
            while (m_Result == null)
            {
                try
                {
                    Console.WriteLine(String.Format("Enter remaining {0} in your {1} (Maximum: {2}):", 
                        getProperAnnotationBatteryOrFuel(i_VehiclePicked), 
                        VehicleTypeForm.getProperVehicleName(i_VehiclePicked.m_Name),
                        maxEnergy));
                    validFloatEntered = float.TryParse(Console.ReadLine(), out m_Input);
                    if (validFloatEntered == false)
                    {
                        throw new FormatException(k_InputIsNotANumberErrorMessage);
                    }
                    VehicleUtils.ValidateEnergyForSpecificVehicle(m_Input, maxEnergy);
                    m_Result = m_Input;
                    Console.WriteLine(String.Format("Successully picked {0}/{1} {2}", 
                        m_Result, maxEnergy, getFuelTypeIfUsingFuelAndNotBattery(i_VehiclePicked)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public string getFuelTypeIfUsingFuelAndNotBattery(VehicleFactory.VehicleTypeStruct i_VehiclePicked)
        {
            string res;
            if (i_VehiclePicked.m_IsFueled == true)
            {
                res = i_VehiclePicked.m_CurrentFuelType.ToString();
            }
            else
            {
                res = "battery";
            }

            return res;
        }

        public float DisplayAndGetResult(VehicleFactory.VehicleTypeStruct i_VehiclePicked)
        {
            Display(i_VehiclePicked);
            return m_Result.Value;
        }

        public void ResetForm()
        {
            m_Result = null;
        }

        private string getProperAnnotationBatteryOrFuel(VehicleFactory.VehicleTypeStruct i_VehiclePicked)
        {
            return i_VehiclePicked.m_IsFueled == true ? "Fuel" : "Battery";
        }

        private float getProperMaxBatteryOrFuel(VehicleFactory.VehicleTypeStruct i_VehiclePicked)
        {
            return (i_VehiclePicked.m_IsFueled == true) ? i_VehiclePicked.m_FuelTankCapacityInLiters : i_VehiclePicked.m_MaxBatteryTimeInHours;
        }
    }
}
using System;
using System.Collections.Generic;
using GarageLogic;

namespace UI
{
    internal class WheelsForm
    {
        private YesNoForm m_YesNoForm = new YesNoForm();
        private const string k_InputIsNotANumberErrorMessage = "You must enter a number";
        private const string k_NoLicensePlateCreatedErrorMessage = "License plate was not created yet";
        private string m_ManufacturerName = String.Empty;
        private float m_CurrentTirePressure;
        private List<Vehicle.Wheel> m_Result = null;

        public List<Vehicle.Wheel> Result
        {
            get
            {
                if (m_Result == null)
                {
                    throw new NullReferenceException(k_NoLicensePlateCreatedErrorMessage);
                }

                return m_Result;
            }
        }

        public void Display(VehicleFactory.VehicleTypeStruct i_VehiclePicked)
        {
            bool validFloatEntered = false;
            int counterSuccessfullyLoadedWheels = 0;
            m_Result = new List<Vehicle.Wheel>(i_VehiclePicked.m_WheelCount);
            while (counterSuccessfullyLoadedWheels < i_VehiclePicked.m_WheelCount)
            {
                try
                {
                    Console.WriteLine(string.Format("Wheel #{0}:", counterSuccessfullyLoadedWheels + 1));
                    Console.WriteLine("Enter wheel manufacturer name:");
                    m_ManufacturerName = Console.ReadLine();
                    Console.WriteLine(string.Format("Enter wheel's current tire pressure (maximum: {0}):", i_VehiclePicked.m_MaxTirePressure));
                    validFloatEntered = float.TryParse(Console.ReadLine(), out m_CurrentTirePressure);
                    if (validFloatEntered == false)
                    {
                        throw new FormatException(k_InputIsNotANumberErrorMessage);
                    }

                    VehicleUtils.ValidateTirePressureForThisVehicle(m_CurrentTirePressure, i_VehiclePicked.m_MaxTirePressure);
                    if (counterSuccessfullyLoadedWheels == 0)
                    {
                        bool wantsToApplyAllInfoOnAllWheels = m_YesNoForm.DisplayAndGetResult("Would you like to apply this data to all wheels? (y/n)");
                        if (wantsToApplyAllInfoOnAllWheels == true)
                        {
                            for (int i = 0; i < i_VehiclePicked.m_WheelCount; i++)
                            {
                                m_Result.Add(new Vehicle.Wheel(m_ManufacturerName, m_CurrentTirePressure, i_VehiclePicked.m_MaxTirePressure));
                                counterSuccessfullyLoadedWheels++;
                            }

                            Console.WriteLine(string.Format("Wheel data successfuly applied to all {0} wheels", i_VehiclePicked.m_WheelCount));
                        }
                        else
                        {
                            m_Result.Add(new Vehicle.Wheel(m_ManufacturerName, m_CurrentTirePressure, i_VehiclePicked.m_MaxTirePressure));
                        }
                    }
                    else
                    {
                        m_Result.Add(new Vehicle.Wheel(m_ManufacturerName, m_CurrentTirePressure, i_VehiclePicked.m_MaxTirePressure));
                    }
                    counterSuccessfullyLoadedWheels++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public List<Vehicle.Wheel> DisplayAndGetResult(VehicleFactory.VehicleTypeStruct i_VehiclePicked)
        {
            Display(i_VehiclePicked);
            return m_Result;
        }

        public void ResetForm()
        {
            m_Result = null;
        }
    }
}
using System;
using System.Collections.ObjectModel;
using Engine;

namespace UI
{
    internal class VehicleTypeForm
    {
        private const string k_InputIsNotANumberErrorMessage = "You must enter a number";
        private const string k_NoVehicleTypeSelectedYetErrorMessage = "Vehicle type was not selected yet";
        private int m_Input;
        private VehicleFactory.VehicleTypeStruct? m_Result = null;

        public VehicleFactory.VehicleTypeStruct Result
        {
            get
            {
                if (m_Result == null)
                {
                    throw new NullReferenceException(k_NoVehicleTypeSelectedYetErrorMessage);
                }

                return m_Result.Value;
            }
        }

        public void Display(ReadOnlyCollection<VehicleFactory.VehicleTypeStruct> i_VehicleTypesList)
        {
            int counter = 0;
            int minVehicleTypeNumberToBePicked = 1;
            int maxVehicleTypeNumberToBePicked = i_VehicleTypesList.Count;
            bool validIntEntered = false;
            while (m_Result == null)
            {
                try
                {
                    counter = 0;
                    Console.WriteLine("Please pick the type of vehicle you'd like to add:");
                    foreach (VehicleFactory.VehicleTypeStruct vehicleType in i_VehicleTypesList)
                    {
                        counter++;
                        Console.WriteLine(String.Format("{0}. {1}", counter, getProperVehicleName(vehicleType.m_Name)));
                    }

                    validIntEntered = int.TryParse(Console.ReadLine(), out m_Input);
                    if (validIntEntered == false)
                    {
                        throw new FormatException(k_InputIsNotANumberErrorMessage);
                    }

                    if (m_Input < minVehicleTypeNumberToBePicked || m_Input > maxVehicleTypeNumberToBePicked)
                    {
                        throw new ValueOutOfRangeException(minVehicleTypeNumberToBePicked, maxVehicleTypeNumberToBePicked);
                    }

                    m_Result = i_VehicleTypesList[m_Input - 1];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public VehicleFactory.VehicleTypeStruct DisplayAndGetResult(ReadOnlyCollection<VehicleFactory.VehicleTypeStruct> i_VehicleTypesList)
        {
            Display(i_VehicleTypesList);
            return m_Result.Value;
        }

        public void ResetForm()
        {
            m_Result = null;
        }

        public static string captializeString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }

        public static string getProperVehicleName(string i_Name)
        {
            string[] subs = i_Name.Split('.');
            string vehicleName = subs[0];
            string energyTypeName = subs[1];

            return captializeString(energyTypeName) + " " + captializeString(vehicleName);
        }
    }
}
using System;
using GarageLogic;

namespace UI
{
    internal class GarageMenuForm
    {
        private const int k_MinGarageMenuOption = 1;
        public enum eGarageMenuOption
        {
            ADD_NEW_VEHICLE = k_MinGarageMenuOption,
            DISPLAY_LICENSE_PLATES_OF_ALL_VEHICLES,
            CHANGE_REPAIR_STATUS_OF_SPECIFIC_VEHICLE,
            FUEL_VEHICLE,
            INFLATE_VEHICLE_TIRES_TO_MAX,
            CHARGE_ELECTRIC_VEHICLE,
            DISPLAY_ALL_INFORMATION_ABOUT_VEHICLE,
            EXIT
        }

        private readonly int r_MaxGarageMenuOption = Enum.GetNames(typeof(eGarageMenuOption)).Length;
        private const string k_InputIsNotANumberErrorMessage = "You must enter a number";
        private const string k_NoOptionPickedInMenuYetErrorMessage = "You didn't pick any option from the garage menu yet";
        private int m_Input;
        private eGarageMenuOption? m_Result = null;

        public eGarageMenuOption Result
        {
            get
            {
                if (m_Result == null)
                {
                    throw new NullReferenceException(k_NoOptionPickedInMenuYetErrorMessage);
                }

                return m_Result.Value;
            }
        }

        public void Display()
        {
            bool validIntEntered = false;
            while (m_Result == null)
            {
                try
                {
                    Console.WriteLine(string.Format(@"Welcome to Garage!
Pick one of the following options:
1. Add a new vehicle to the garage
2. Display license plate numbers of all the vehicles in the garage
3. Change condition/repair status of a vehicle
4. Inflate vehicle tires to maximum pressure
5. Fuel vehicle
6. Charge electric vehicle
7. Display all available information about specific vehicle according to license plate
8. Exit menu"));
                    validIntEntered = int.TryParse(Console.ReadLine(), out m_Input);
                    if (validIntEntered == false)
                    {
                        throw new FormatException(k_InputIsNotANumberErrorMessage);
                    }

                    if (m_Input < k_MinGarageMenuOption || m_Input > r_MaxGarageMenuOption)
                    {
                        throw new ValueOutOfRangeException(k_MinGarageMenuOption, r_MaxGarageMenuOption);
                    }

                    m_Result = (eGarageMenuOption)m_Input;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public eGarageMenuOption DisplayAndGetResult()
        {
            Display();
            return m_Result.Value;
        }

        public void ResetForm()
        {
            m_Result = null;
        }
    }
}
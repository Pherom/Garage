using System;
using GarageLogic;

namespace UI
{
    internal class OwnerDataForm
    {
        private const string k_OwnerDataWasNotFilledYetErrorMessage = "Owner data was not filled yet";
        private string m_InputName = String.Empty;
        private string m_InputPhoneNumber;
        private Vehicle.VehicleOwnerData m_Result = null;

        public Vehicle.VehicleOwnerData Result
        {
            get
            {
                if (m_Result == null)
                {
                    throw new NullReferenceException(k_OwnerDataWasNotFilledYetErrorMessage);
                }

                return m_Result;
            }
        }

        public void Display()
        {
            while (m_Result == null)
            {
                try
                {
                    if (m_InputName == String.Empty)
                    {
                        Console.WriteLine("Enter owner's name:");
                        m_InputName = Console.ReadLine();
                    }
                    Console.WriteLine("Enter owner's phone number:");
                    m_InputPhoneNumber = Console.ReadLine();
                    m_Result = new Vehicle.VehicleOwnerData(m_InputName, m_InputPhoneNumber);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public Vehicle.VehicleOwnerData DisplayAndGetResult()
        {
            Display();
            return m_Result;
        }

        public void ResetForm()
        {
            m_InputName = String.Empty;
            m_InputPhoneNumber = String.Empty;
            m_Result = null;
        }
    }
}
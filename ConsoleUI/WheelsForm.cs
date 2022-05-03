using System;
using Engine;

namespace UI
{
    internal class WheelsForm
    {
        private const string k_NoLicensePlateCreatedErrorMessage = "License plate was not created yet";
        private string m_Input;
        private Vehicle.Wheel m_Result = null;

        public string Result
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

        public void Display()
        {
            while (m_Result == null)
            {
                try
                {
                    Console.WriteLine("Please enter license plate number:");
                    m_Input = Console.ReadLine();
                    VehicleUtils.validateLicensePlate(m_Input);
                    m_Result = m_Input;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public string DisplayAndGetResult()
        {
            Display();
            return m_Result;
        }

        public void ResetForm()
        {
            m_Result = null;
        }
    }
}
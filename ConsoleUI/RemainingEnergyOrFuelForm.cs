using System;
using Engine;

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

        public void Display(bool i_IsGasolineFueledVehicle)
        {
            float energy = 0;
            bool validFloatEntered = false;
            while (m_Result == null)
            {
                try
                {
                    Console.WriteLine(String.Format("Please enter remaining {0} in your vehicle:", i_IsGasolineFueledVehicle == true ? "Fuel" : "Gasoline"));
                    validFloatEntered = float.TryParse(Console.ReadLine(), out m_Input);
                    if (validFloatEntered == false)
                    {
                        throw new FormatException(k_InputIsNotANumberErrorMessage);
                    }
                    validateEnergyForSpecificVehicle(m_Input, i_IsGasolineFueledVehicle,);

                    m_Result = m_Input;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public float DisplayAndGetResult(bool i_IsGasolineFueledVehicle)
        {
            Display(i_IsGasolineFueledVehicle);
            return m_Result.Value;
        }

        public void ResetForm()
        {
            m_Result = null;
        }
    }
}
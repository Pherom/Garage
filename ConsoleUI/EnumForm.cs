using System;
using GarageLogic;

namespace UI
{
    internal class EnumForm
    {
        private const string k_InputIsNotANumberErrorMessage = "You must enter a number";
        private const string k_EnumOptionNotSelectedYetErrorMessage = "Enum option was not selected yet";
        private int m_Input;
        private object m_Result = null;

        public object Result
        {
            get
            {
                if (m_Result == null)
                {
                    throw new NullReferenceException(k_EnumOptionNotSelectedYetErrorMessage);
                }

                return m_Result;
            }
        }

        public void Display(string i_MsgToShowBeforeForm, Array i_ArrayOfEnumValues)
        {
            bool validIntEntered = false;
            while (m_Result == null)
            {
                try
                {
                    Console.WriteLine(i_MsgToShowBeforeForm);
                    // Need to create here instead enum form File that returns the picked enum value instead of all this:
                    int counter = 0;
                    foreach (object enumValue in i_ArrayOfEnumValues)
                    {
                        counter++;
                        Console.WriteLine(String.Format("{0}. {1}", counter, enumValue));
                    }
                    validIntEntered = int.TryParse(Console.ReadLine(), out m_Input);
                    if (validIntEntered == false)
                    {
                        throw new FormatException(k_InputIsNotANumberErrorMessage);
                    }
                    VehicleUtils.ValidateValueIsInRange(m_Input, 1, i_ArrayOfEnumValues.Length);
                    setResultValueAccordingToInput(i_ArrayOfEnumValues);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public object DisplayAndGetResult(string i_MsgToShowBeforeForm, Array i_ArrayOfEnumValues)
        {
            Display(i_MsgToShowBeforeForm, i_ArrayOfEnumValues);
            return m_Result;
        }

        public void ResetForm()
        {
            m_Result = null;
        }

        private void setResultValueAccordingToInput(Array i_ArrayOfEnumValues)
        {
            int counter = 0;
            foreach (object enumValue in i_ArrayOfEnumValues)
            {
                counter++;
                if (m_Input == counter)
                {
                    m_Result = enumValue;
                    break;
                }
            }
        }
    }
}
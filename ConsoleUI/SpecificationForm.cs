using System;
using GarageLogic;
using System.Collections.Generic;

namespace UI
{
    internal class SpecificationForm
    {
        private YesNoForm m_YesNoForm = new YesNoForm();
        private EnumForm m_EnumForm = new EnumForm();
        private const string k_SpecificationsNotFilledYetErrorMessage = "Specifications are not filled yet";
        private Specifications m_Result = null;

        public Specifications Result
        {
            get
            {
                if (m_Result == null)
                {
                    throw new NullReferenceException(k_SpecificationsNotFilledYetErrorMessage);
                }

                return m_Result;
            }
        }

        public void Display(VehicleFactory.VehicleTypeStruct i_VehiclePicked)
        {
            while (m_Result == null)
            {
                try
                {
                    List<object> specificationsAnswers = readAndAskUserForInputFromSpecificationsList(i_VehiclePicked.m_SpecificationStructList);
                    Specifications specifications = (Specifications)Activator.CreateInstance(i_VehiclePicked.m_SpecificationType, VehicleTypeForm.getProperVehicleName(i_VehiclePicked.m_Name));
                    specifications.InitSpecifications(specificationsAnswers);
                    m_Result = specifications;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public Specifications DisplayAndGetResult(VehicleFactory.VehicleTypeStruct i_VehiclePicked)
        {
            Display(i_VehiclePicked);
            return m_Result;
        }

        public void ResetForm()
        {
            m_Result = null;
        }

        private int inputInteger()
        {
            string input;
            int parsed;
            bool? parsedSuccessfuly = null;

            do
            {
                if (parsedSuccessfuly.HasValue)
                {
                    Console.WriteLine(string.Format("Error! The input received must be an integer{0}Try again: ", Environment.NewLine));
                }
                input = Console.ReadLine();
                parsedSuccessfuly = int.TryParse(input, out parsed);
            } while (parsedSuccessfuly.Value == false);

            return parsed;
        }

        private float inputFloat()
        {
            string input;
            float parsed;
            bool? parsedSuccessfuly = null;

            do
            {
                if (parsedSuccessfuly.HasValue)
                {
                    Console.WriteLine(string.Format("Error! The input received must be a floating point number{0}Try again: ", Environment.NewLine));
                }
                input = Console.ReadLine();
                parsedSuccessfuly = float.TryParse(input, out parsed);
            } while (parsedSuccessfuly.Value == false);

            return parsed;
        }

        // The paramater i_SpecificationsList is from the problem world and it contains in each index the FieldName that needs to be field and its type
        // Creating list of objects from the solution world, so that's why both lists are same size
        // For every index in i_SpecificationList[index] there's the corresponding value that the user filled in res[index]
        private List<object> readAndAskUserForInputFromSpecificationsList(List<VehicleFactory.SpecificationStruct> i_SpecificationsList)
        {
            List<object> res = new List<object>(i_SpecificationsList.Count);

            foreach (VehicleFactory.SpecificationStruct specification in i_SpecificationsList)
            {
                if (specification.m_ValueType.IsPrimitive == true)
                {
                    switch (specification.m_ValueType.Name)
                    {
                        case ("Int32"):
                            Console.WriteLine(String.Format("Enter an integer to select {0}", specification.m_NameOfField));
                            res.Add(inputInteger());
                            break;
                        case ("Single"):
                            Console.WriteLine(String.Format("Enter a floating point number to select {0}", specification.m_NameOfField));
                            res.Add(inputFloat());
                            break;
                        case ("Boolean"):
                            res.Add(m_YesNoForm.DisplayAndGetResult(String.Format("Enter y/n to select {0}", specification.m_NameOfField)));
                            break;
                        default:
                            Console.WriteLine(String.Format("Enter a string to select {0}", specification.m_NameOfField));
                            res.Add(Console.ReadLine());
                            break;
                    }
                }
                else if (specification.m_ValueType.BaseType.Name == "Enum")
                {
                    Array arrayOfEnumValues = specification.m_ValueType.GetEnumValues();
                    string messageToShowForEnumForm = String.Format("Enter a number to select {0}", specification.m_NameOfField);
                    res.Add(m_EnumForm.DisplayAndGetResult(messageToShowForEnumForm, arrayOfEnumValues));
                    m_EnumForm.ResetForm();
                }
            }

            return res;
        }
    }
}

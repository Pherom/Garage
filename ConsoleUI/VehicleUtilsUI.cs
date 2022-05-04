using System;
using Engine;
using System.Collections.Generic;
using System.Reflection;

namespace UI
{
    internal class VehicleUtilsUI
    {
        // The paramater i_SpecificationsList is from the problem world
        // Creating list of objects from the solution world, so that's why both lists are same size
        // For every index in i_SpecificationList[index] there's the corresponding value that the user filled in res[index]
        public static List<object> readAndAskUserForInputFromSpecificationsList(List<VehicleFactory.SpecificationStruct> i_SpecificationsList)
        {
            List<object> res = new List<object>(i_SpecificationsList.Count);
            foreach (VehicleFactory.SpecificationStruct specification in i_SpecificationsList)
            {
                Console.WriteLine(String.Format("Enter a number to select {0}", specification.m_NameOfField));
                switch (specification.m_ValueType.BaseType.Name)
                {
                    case ("Enum"):
                        Array arrayOfEnumValues = specification.m_ValueType.GetEnumValues();
                        // Need to create here instead enum form File that returns the picked enum value instead of all this:
                        int counter = 0;
                        foreach (object enumValue in arrayOfEnumValues)
                        {
                            counter++;
                            Console.WriteLine(String.Format("{0}. {1}", counter, enumValue));
                        }
                        int input = int.Parse(Console.ReadLine());

                        // Getting the proper enum value and adding to the list
                        counter = 0;
                        foreach (object enumValue in arrayOfEnumValues)
                        {
                            counter++;
                            if (input == counter)
                            {
                                res.Add(enumValue);
                                break;
                            }
                        }

                        break;
                    case ("Int32"):
                        res.Add(int.Parse(Console.ReadLine()));
                        break;
                    case ("Single"):
                        res.Add(float.Parse(Console.ReadLine()));
                        break;
                    default:
                        res.Add(Console.ReadLine());
                        break;
                }
            }

            return res;
        }
    }
}

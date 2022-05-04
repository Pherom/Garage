using System;
using System.Collections.Generic;
using System.Reflection;

namespace GarageLogic
{
    public class VehicleUtils
    {
        private const string k_InvalidLicensePlateExceptionMessage = "License plate number must consists of 7-8 numbers only";
        private VehicleUtils() { }
        public static void ValidateLicensePlate(string i_LicensePlateNumber)
        {
            if (i_LicensePlateNumber == null)
            {
                throw new ArgumentNullException(nameof(i_LicensePlateNumber));
            }

            if (!checkIfStringConsistsOfDigitsOnly(i_LicensePlateNumber) || (!(i_LicensePlateNumber.Length == 7 || i_LicensePlateNumber.Length == 8)))
            {
                throw new FormatException(k_InvalidLicensePlateExceptionMessage);
            }
        }

        private static bool checkIfStringConsistsOfDigitsOnly(string i_LicensePlate)
        {
            foreach (char currentChar in i_LicensePlate)
            {
                if (currentChar < '0' || currentChar > '9')
                    return false;
            }

            return true;
        }

        public static void ValidateValueIsInRange(float i_Value, float i_Min, float i_Max)
        {
            if (i_Value < i_Min || i_Value > i_Max)
            {
                throw new ValueOutOfRangeException(i_Min, i_Max);
            }
        }

        public static void ValidateTirePressureForThisVehicle(float i_CurrentTirePressure, float i_MaxTirePressure)
        {
            ValidateValueIsInRange(i_CurrentTirePressure, 0, i_MaxTirePressure);
        }

        public static void ValidateEnergyForSpecificVehicle(float i_CurrentEnergy, float i_MaxEnergy)
        {
            ValidateValueIsInRange(i_CurrentEnergy, 0, i_MaxEnergy);
        }

        public static List<VehicleFactory.SpecificationStruct> getSpecificationsFieldsListBasedOnTypeOfVehicle(Type i_TypeOfSpecificSpecifiationVehicle)
        {
            FieldInfo[] fieldsList = (FieldInfo[])i_TypeOfSpecificSpecifiationVehicle.GetRuntimeFields();
            List<VehicleFactory.SpecificationStruct> res = new List<VehicleFactory.SpecificationStruct>(fieldsList.Length); // Number of specifications
            VehicleFactory.SpecificationStruct currentSpecification;
            foreach (FieldInfo currentField in fieldsList)
            {
                currentSpecification = new VehicleFactory.SpecificationStruct();
                currentSpecification.m_NameOfField = currentField.Name.Substring(2, currentField.Name.Length - 2);
                currentSpecification.m_ValueType = currentField.FieldType;
                res.Add(currentSpecification);
            }

            return res;
        }

        public static List<VehicleFactory.VehicleTypeStruct> initVehicleTypeListFromClassVehicleTypes()
        {
            List<VehicleFactory.VehicleTypeStruct> typeOfVehiclesList = new List<VehicleFactory.VehicleTypeStruct>();
            VehicleFactory.VehicleTypeStruct specificVehicle;
            Type typeofVehicleTypeStruct = typeof(VehicleFactory.VehicleTypeStruct);
            FieldInfo myFieldInfo;

            Type[] vehicleTypes = typeof(VehicleFactory.VehicleTypes).GetNestedTypes();
            foreach (Type currentVehicleType in vehicleTypes)
            {
                foreach (Type energySystem in currentVehicleType.GetNestedTypes())
                {
                    specificVehicle = new VehicleFactory.VehicleTypeStruct();
                    specificVehicle.m_Name = currentVehicleType.Name.ToUpper() + "." + energySystem.Name.ToUpper();

                    // Adding the fields outside of Fueled/Electric class, that belong to same class (MOTORBIKE/CAR/TRUCK)
                    foreach (FieldInfo currentField in currentVehicleType.GetFields())
                    {
                        string nameOfProperty = "m" + currentField.Name.Substring(1, currentField.Name.Length - 1);
                        object valueOfProperty = currentField.GetValue(currentVehicleType);
                        myFieldInfo = typeofVehicleTypeStruct.GetField(nameOfProperty);

                        if (currentField.FieldType.Name == "Int32")
                        {
                            myFieldInfo.SetValueDirect(__makeref(specificVehicle), int.Parse(valueOfProperty.ToString()));
                        }
                        else if (currentField.FieldType.Name == "Single")
                        {
                            myFieldInfo.SetValueDirect(__makeref(specificVehicle), float.Parse(valueOfProperty.ToString()));
                        }
                        else if (currentField.FieldType.Name == "Type")
                        {
                            specificVehicle.m_SpecificationObjectType = (Type)valueOfProperty;
                            specificVehicle.m_SpecificationsStruct = getSpecificationsFieldsListBasedOnTypeOfVehicle(specificVehicle.m_SpecificationObjectType);
                        }
                    }

                    // Adding the fields inside the Fueled/Electric class
                    foreach (FieldInfo currentField in energySystem.GetFields())
                    {
                        string nameOfProperty = "m" + currentField.Name.Substring(1, currentField.Name.Length - 1);
                        object valueOfProperty = currentField.GetValue(energySystem);
                        myFieldInfo = typeofVehicleTypeStruct.GetField(nameOfProperty);

                        if (currentField.FieldType.Name == "Single")
                        {
                            myFieldInfo.SetValueDirect(__makeref(specificVehicle), float.Parse(valueOfProperty.ToString()));
                        }
                        else if (currentField.FieldType.Name == "eFuelType")
                        {
                            myFieldInfo.SetValueDirect(__makeref(specificVehicle), valueOfProperty);
                        }
                    }

                    specificVehicle.m_IsFueled = energySystem.Name.ToUpper() == "FUELED" ? true : false;
                    typeOfVehiclesList.Add(specificVehicle);
                }
            }

            return typeOfVehiclesList;
            //foreach (VehicleFactory.VehicleTypeStruct s in typeOfVehiclesList)
            //{
            //    Console.Write(s.m_Name);
            //    Console.Write(s.m_WheelsNumber);
            //    if (s.m_IsFueled == true)
            //    {
            //        Console.Write(s.m_FuelTankCapacityInLiters);
            //    }
            //    else
            //    {
            //        Console.Write(s.m_MaxBatteryTimeInHours);
            //    }
            //    Console.WriteLine();
            //}
        }
    }
}

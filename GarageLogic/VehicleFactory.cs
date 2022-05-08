using System;
using System.Collections.Generic;

namespace GarageLogic
{
    public class VehicleFactory
    {
        public struct SpecificationStruct
        {
            public string m_NameOfField;
            public Type m_ValueType;
        }

        public struct VehicleTypeStruct
        {
            public string m_Name;
            public int m_WheelCount;
            public float m_MaxTirePressure;
            public bool m_IsGasolineFueledVehicle;
            public float m_FuelTankCapacityInLiters;
            public GasolineFueledVehicle.eFuelType m_FuelType;
            public float m_MaxBatteryTimeInHours;
            public List<SpecificationStruct> m_SpecificationStructList;
            public Type m_SpecificationType;
        }

        public class VehicleTypes
        {
            private VehicleTypes() { }
            public class Motorbike
            {
                public const int k_WheelCount = 2;
                public const float k_MaxTirePressure = 31f;
                public static readonly Type sr_SpecificationType = typeof(BikeSpecifications);

                private Motorbike() { }

                public class Fueled
                {
                    public const GasolineFueledVehicle.eFuelType k_FuelType = GasolineFueledVehicle.eFuelType.OCTAN98;
                    public const float k_FuelTankCapacityInLiters = 6.2f;
                    private Fueled() { }
                }
                public class Electric
                {
                    public const float k_MaxBatteryTimeInHours = 2.5f;
                    private Electric() { }
                }
            }
            public class Car
            {
                public const int k_WheelCount = 4;
                public const float k_MaxTirePressure = 29f;
                public static readonly Type sr_SpecificationType = typeof(CarSpecifications);

                private Car() { }
                public class Fueled
                {
                    public const GasolineFueledVehicle.eFuelType k_FuelType = GasolineFueledVehicle.eFuelType.OCTAN95;
                    public const float k_FuelTankCapacityInLiters = 38f;

                    private Fueled() { }
                }
                public class Electric
                {
                    public const float k_MaxBatteryTimeInHours = 3.3f;
                    private Electric() { }
                }
            }
            public class Truck
            {
                public const int k_WheelCount = 4;
                public const float k_MaxTirePressure = 29f;
                public static readonly Type sr_SpecificationType = typeof(TruckSpecifications);


                private Truck() { }
                public class Fueled
                {
                    public const GasolineFueledVehicle.eFuelType k_FuelType = GasolineFueledVehicle.eFuelType.SOLER;
                    public const float m_FuelTankCapacityInLiters = 120f;
                    private Fueled() { }
                }
            }
        }


        public Vehicle CreateVehicle(VehicleFactory.VehicleTypeStruct i_VehiclePicked, string i_ModelName,
            string i_LicensePlateNumber, List<Vehicle.Wheel> i_Wheels, Vehicle.VehicleOwnerData i_OwnerData,
            Specifications i_Specifications, float i_RemainingBatteryTimeInHoursOrFuelAmountInLiters)
        {
            Vehicle res = null;
            if (i_VehiclePicked.m_IsGasolineFueledVehicle == true)
            {
                res = new GasolineFueledVehicle(i_ModelName, i_LicensePlateNumber, i_Wheels, i_OwnerData, i_Specifications,
                    i_VehiclePicked.m_FuelType, i_VehiclePicked.m_FuelTankCapacityInLiters, i_RemainingBatteryTimeInHoursOrFuelAmountInLiters);
            }
            else
            {
                res = new ElectricVehicle(i_ModelName, i_LicensePlateNumber, i_Wheels, i_OwnerData, i_Specifications,
                    i_VehiclePicked.m_MaxBatteryTimeInHours, i_RemainingBatteryTimeInHoursOrFuelAmountInLiters);
            }

            return res;
        }

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class VehicleFactory
    {
        public enum eVehicleType
        {
            BIKE,
            CAR,
            TRUCK
        }

        public struct VehicleTypeStruct
        {
            public string m_Name;
            public int m_WheelsNumber;
            public float m_MaxTirePressure;
            public bool m_IsFueled;
            public float m_FuelTankCapacityInLiters;
            public GasolineFueledVehicle.eFuelType m_CurrentFuelType;
            public float m_MaxBatteryTimeInHours;
        }

        public class VehicleTypes
        {
            private VehicleTypes() { }
            public class Motorbike
            {
                public const int k_WheelsNumber = 2;
                public const float m_MaxTirePressure = 31f;
                private Motorbike() { }
                public class Fueled
                {
                    public const GasolineFueledVehicle.eFuelType k_CurrentFuelType = GasolineFueledVehicle.eFuelType.OCTAN98;
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
                public const int k_WheelsNumber = 4;
                public const float m_MaxTirePressure = 29f;
                private Car() { }
                public class Fueled
                {
                    public const GasolineFueledVehicle.eFuelType k_CurrentFuelType = GasolineFueledVehicle.eFuelType.OCTAN95;
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
                private int m_WheelsNumber = 4;
                private float m_MaxTirePressure = 29f;

                private Truck() { }
                public class Fueled
                {
                    private const GasolineFueledVehicle.eFuelType m_CurrentFuelType = GasolineFueledVehicle.eFuelType.SOLER;
                    private const float m_FuelTankCapacityInLiters = 120f;
                    private Fueled() { }
                }
            }
        }


        public Vehicle createCar()
        {
            Vehicle res = null;


            return res;
        }
    }
}

